/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Naizari.Helpers;
using Naizari.Extensions;

namespace Naizari.Helpers
{
    public static class ThreadHelper
    {
        static Dictionary<string, Thread> threads;
        static ThreadHelper()
        {
            threads = new Dictionary<string, Thread>();
        }     

        /// <summary>
        /// Returns true if the specified function takes longer to execute than the specified secondsToWait.
        /// </summary>
        /// <typeparam name="TResult">The Type returned by the sepcified function, also the return and parameter type of the
        /// specified callBack.</typeparam>
        /// <param name="function">The function to execute and time</param>
        /// <param name="callBack">The callBack to execute when function completes</param>
        /// <param name="secondsToWait">The number of seconds to allow the function to execute before returning true</param>
        /// <returns>boolean</returns>
        public static bool TakesTooLong<TResult>(this Func<TResult> function, Func<TResult, TResult> callBack, int secondsToWait)
        {
            return TakesTooLong<TResult>(function, callBack, new TimeSpan(0, 0, secondsToWait));
        }

        /// <summary>
        /// Executes the specified function in a separate thread waiting the specified timeToWait.  If
        /// the function is not done executing in the specified timeToWait 
        /// </summary>
        /// <typeparam name="TResult">The Type returned by the sepcified function, also the return and parameter type of the
        /// specified callBack.</typeparam>
        /// <param name="function">The function to execute and time</param>
        /// <param name="callBack">The callBack to execute when function completes</param>
        /// <param name="timeToWait">The ammount of time to allow the function to execute before returning true</param>
        /// <returns>boolean</returns>
        public static bool TakesTooLong<TResult>(this Func<TResult> function, Func<TResult, TResult> callBack, TimeSpan timeToWait)
        {
            // blocks thread until execution completion or timeToWait expires, see below .WaitOne()
            AutoResetEvent returnThreadController = new AutoResetEvent(false);

            int millisecondsToWait = (int)timeToWait.TotalMilliseconds;

            bool? tookTooLong = false; 
            string customThreadId = "".RandomString(8);

            Thread functionThread = new Thread(() =>
            {
                if (callBack != null)
                {
                    callBack(function());
                }
                else
                {
                    function();
                }
                returnThreadController.Set();
                threads.Remove(customThreadId);
            });            
            
            functionThread.IsBackground = true;
            threads.Add(customThreadId, functionThread); // make sure the thread doesn't get garbage collected
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

        public static TimeSpan TimeExecution<TResult>(this Func<TResult> func, out TResult result)
        {
            DateTime start = DateTime.Now;
            result = func();
            DateTime end = DateTime.Now;
            return end.Subtract(start);
        }

        public static void InThread<TResult>(this Func<TResult> func, Func<TResult, TResult> callBack)
        {
            TakesTooLong<TResult>(func, callBack, 0);
        }

        public static Thread InThread(this ThreadStart method)
        {
            Thread thread = new Thread(method);
            thread.Start();
            return thread;
        }
    }
}
