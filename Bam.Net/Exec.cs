/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net
{
    public static class Exec
    {
        static volatile Dictionary<string, Thread> _threads;
        static Exec()
        {
            _threads = new Dictionary<string, Thread>(500);
        }

        public static ProcessThread GetThread(int managedThreadId)
        {
            
            Process currentProcess = Process.GetCurrentProcess();
            foreach(ProcessThread thread in currentProcess.Threads)
            {
                if(thread.Id == managedThreadId)
                {
                    return thread;
                }
            }
            return null;
        }

        /// <summary>
        /// Sleeps until the specified checkCondition is true.
        /// </summary>
        /// <param name="checkCondition">The check condition.</param>
        /// <param name="sleep">The number of milliseconds to sleep between condition checks.</param>
        /// <param name="timeout">The amount of time after which the condition is no longer checked and 
        /// the method returns.</param>
        /// <returns>The time slept, not completely acurate as this value is the result of sleep * number of cycles</returns>
        public static int SleepUntil(Func<bool> checkCondition, int sleep = 300, int timeout = 5000)
        {
            SleepUntil(checkCondition, out int returnValue, sleep, timeout);
            return returnValue;
        }

        /// <summary>
        /// Sleeps until the specified checkCondition is true.
        /// </summary>
        /// <param name="checkCondition">The check condition.</param>
        /// <param name="slept">The time slept.</param>
        /// <param name="sleep">The number of milliseconds to sleep between condition checks.</param>
        /// <param name="timeout">The amount of time after which the condition is no longer checked and 
        /// the method returns.</param>
        /// <returns>True if the condition was met, false otherwise.</returns>
        public static bool SleepUntil(Func<bool> checkCondition, out int slept, int sleep = 300, int timeout = 5000)
        {
            slept = 0;
            Thread.Sleep(sleep);
            while (!checkCondition())
            {
                Thread.Sleep(sleep);
                slept += sleep;
                if(slept >= timeout)
                {
                    return false;
                }
            }
            return true;
        }

        public static NamedThread GetThread(string name)
        {
            if (_threads.ContainsKey(name))
            {
                return new NamedThread(name, _threads[name]);
            }
            else
            {
                return null;
            }
        }
        public static void Try(Action action, Action<Exception> onException = null)
        {
            Action<Exception> exceptionHandler = onException ?? ((ex) => { });
            try
            {
                action();
            }
            catch (Exception e)
            {
                exceptionHandler(e);
            }
        }

        public static void TryAsync(Action action, Action<Exception> onException = null)
        {
            Action<Exception> exceptionHandler = onException ?? ((ex) => { });

            Task t = Task.Run(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    exceptionHandler(ex);
                }
            });
        }

        /// <summary>
        /// Create a NamedThread with the specified name to run
        /// the specified action when it is started.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static NamedThread SetThread(string name, Action action)
        {
            NamedThread thread = new NamedThread(name, action);
            _threads[name] = thread;

            return thread;
        }

        /// <summary>
        /// Execute the specified action after sleeping for the specified milliseconds.
        /// </summary>
        /// <param name="millisecondsBeforeStarting">The milliseconds before starting.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static NamedThread After(int millisecondsBeforeStarting, Action action)
        {
            if(millisecondsBeforeStarting > 0)
            {
                Thread.Sleep(millisecondsBeforeStarting);
            }
            return Start(action);
        }

        public static NamedThread Start(Action action)
        {
            return Start(Guid.NewGuid().ToString(), action);
        }

        public static NamedThread Start(string name, Action action)
        {
            Thread thread = new Thread(() =>
            {
                action();
            });

            if (_threads.ContainsKey(name))
            {
                try
                {
                    _threads[name].Abort();
                    _threads[name].Join(3000);
                }
                catch { }
                _threads[name] = thread;
            }
            else
            {
                _threads.Add(name, thread);
            }
            thread.Start();
            return new NamedThread(name, thread);
        }

        public static void Kill(string name, int joinBlock = 3000)
        {
            if (_threads.ContainsKey(name))
            {
                Thread victim = _threads[name];
                if (victim.ThreadState == System.Threading.ThreadState.Running)
                {
                    try
                    {
                        victim.Abort();
                        victim.Join(joinBlock);
                    }
                    catch { }
                }
                _threads.Remove(name);
            }
        }

        /// <summary>
        /// Returns true if the specified action takes longer than the
        /// specified number of millisecondsToWait to finish executing
        /// </summary>
        /// <param name="action"></param>
        /// <param name="millisecondsToWait"></param>
        /// <returns></returns>
        public static bool TakesTooLong(this Action action, int millisecondsToWait)
        {
            return TakesTooLong(() =>
            {
                action();
                return false;
            }, millisecondsToWait);
        }

        public static bool TakesTooLong<TResult>(this Func<TResult> function, int millisecondsToWait)
        {
            return TakesTooLong(function, new TimeSpan(0, 0, 0, 0, millisecondsToWait));
        }

        public static bool TakesTooLong<TResult>(this Func<TResult> function, TimeSpan timeToWait)
        {
            return TakesTooLong(function, null, timeToWait);
        }

        public static bool TakesTooLong<TResult>(this Func<TResult> function, Func<TResult, TResult> callBack, string threadName, int millisecondsToWait = 300)
        {
            return function.TakesTooLong(callBack, new TimeSpan(0, 0, 0, 0, millisecondsToWait), threadName);
        }

        public static bool TakesTooLong<TResult>(this Func<object, TResult> function, Func<TResult, TResult> callBack, string threadName, object state = null, int millisecondsToWait = 300)
        {
            return function.TakesTooLong(callBack, new TimeSpan(0, 0, 0, 0, millisecondsToWait), state, threadName);
        }

        /// <summary>
        /// Returns true if the specified function takes longer to execute than the specified secondsToWait.
        /// </summary>
        /// <typeparam name="TResult">The Type returned by the sepcified function, also the return and parameter type of the
        /// specified callBack.</typeparam>
        /// <param name="function">The function to execute and time</param>
        /// <param name="callBack">The callBack to execute when function completes</param>
        /// <param name="millisecondsToWait">The number of seconds to allow the function to execute before returning true</param>
        /// <returns>boolean</returns>
        public static bool TakesTooLong<TResult>(this Func<TResult> function, Func<TResult, TResult> callBack, int millisecondsToWait)
        {
            return function.TakesTooLong(callBack, new TimeSpan(0, 0, 0, 0, millisecondsToWait));
        }

        public static bool TakesTooLong<TResult>(this Func<TResult> function, Func<TResult, TResult> callBack, TimeSpan timeToWait, string threadName = null)
        {
            return TakesTooLong((o) =>
            {
                return function();
            }, callBack, timeToWait, null, threadName);
        }

        /// <summary>
        /// Executes the specified function in a separate thread waiting the specified timeToWait.  If
        /// the function is not done executing in the specified timeToWait returns true otherwise false.
        /// Will return true if the function throws an exception with the logic being that the 
        /// function was not able to complete its work
        /// </summary>
        /// <typeparam name="TResult">The Type returned by the sepcified function, also the return and parameter type of the
        /// specified callBack.</typeparam>
        /// <param name="function">The function to execute and time</param>
        /// <param name="callBack">The callBack to execute when function completes</param>
        /// <param name="timeToWait">The ammount of time to allow the function to execute before returning true</param>
        /// <returns>boolean</returns>
        public static bool TakesTooLong<TResult>(this Func<object, TResult> function, Func<TResult, TResult> callBack, TimeSpan timeToWait, object state = null, string threadName = null)
        {
            try
            {
                // blocks thread until execution completion or timeToWait expires, see below .WaitOne()
                AutoResetEvent returnThreadController = new AutoResetEvent(false);
                int millisecondsToWait = (int)timeToWait.TotalMilliseconds;

                bool? tookTooLong = false;
                if (string.IsNullOrEmpty(threadName))
                {
                    threadName = "Bam.Net.Thread_".RandomString(8);
                }

                int suffix = 1;
                while (_threads.ContainsKey(threadName))
                {
                    threadName = string.Format("{0}_{1}", threadName, suffix.ToString());
                    suffix++;
                }

                Thread functionThread = new Thread(() =>
                {
                    if (callBack != null)
                    {
                        callBack(function(state));
                    }
                    else
                    {
                        function(state);
                    }
                    returnThreadController.Set();
                    _threads.Remove(threadName);
                });

                functionThread.IsBackground = true;
                _threads.Add(threadName, functionThread); // make sure the thread doesn't get garbage collected
                                                          // give the function a head start
                functionThread.Start();

                Timer timer = new Timer((o) =>
                {
                    tookTooLong = true;
                    returnThreadController.Set(); // execution took too long
                }, null, millisecondsToWait, Timeout.Infinite);

                returnThreadController.WaitOne();

                return tookTooLong.Value;
            }
            catch (Exception ex)
            {
                Log.Default.AddEntry("Exception occurred in Exec.TakesTooLong: {0}", ex, ex.Message);
                return true;
            }
        }

		/// <summary>
		/// Execute the specified action and return a TimeSpan
		/// representing how much time it took to execute
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public static TimeSpan TimeExecution(this Action action)
		{
			return Time(action);
		}

		/// <summary>
		/// Execute the specified action and return a TimeSpan
		/// representing how much time it took to execute
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
        public static TimeSpan Time(this Action action)
        {
            DateTime start = DateTime.Now;
            action();
            DateTime end = DateTime.Now;
            return end.Subtract(start);
        }

		/// <summary>
		/// Execute the specified Func and return a TimeSpan
		/// representing how much time it took to execute
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="func"></param>
		/// <param name="result"></param>
		/// <returns></returns>
        public static TimeSpan Time<TResult>(this Func<TResult> func, out TResult result)
        {
            return func.TimeExecution(out result);
        }

        /// <summary>
        /// Time the execution of the specified function returning a TimeSpan
        /// instance representing the ammount of time it took for the function
        /// to run
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static TimeSpan TimeExecution<TResult>(this Func<TResult> func, out TResult result)
        {
            DateTime start = DateTime.Now;
            result = func();
            DateTime end = DateTime.Now;
            return end.Subtract(start);
        }

        /// <summary>
        /// Execute the specified Func and return a TimeSpan
        /// representing how much time it took to execute
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="input"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static TimeSpan TimeExecution<TInput, TResult>(this Func<TInput, TResult> func, TInput input, out TResult result)
		{
			DateTime? start = DateTime.Now;
			result = func(input);
			DateTime? end = DateTime.Now;
			return end.Value.Subtract(start.Value);
		}

        public static void InThread<TResult>(this Func<TResult> func, Func<TResult, TResult> callBack)
        {
            TakesTooLong<TResult>(func, callBack, 0);
        }

		public static Thread ExecuteInThread(this Action action)
		{
            Thread thread = new Thread(() => action())
            {
                IsBackground = true
            };
            thread.Start();
			return thread;
		}

		public static Thread ExecuteInThread(this Action<dynamic> action, dynamic argContext)
		{
            Thread thread = new Thread((o) => action(o))
            {
                IsBackground = true
            };
            thread.Start(argContext);
			return thread;
		}

        public static Thread InThread(this ThreadStart method)
        {
            Thread thread = new Thread(method)
            {
                IsBackground = true
            };
            thread.Start();
            return thread;
        }

        public static Thread InThread(this ParameterizedThreadStart method, object parameter)
        {
            Thread thread = new Thread(method)
            {
                IsBackground = true
            };
            thread.Start(parameter);
            return thread;
        }
    }
}
