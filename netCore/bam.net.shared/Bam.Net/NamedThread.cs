/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net
{
    public class NamedThread<T>: NamedThread
    {
        public NamedThread(string name, Thread thread) : base(name, thread) { }

        public NamedThread(string name, Action action) : base(name, action) { }
        public NamedThread(string name, Func<object> function) : base(name, function) { }
        public NamedThread(string name, Func<T> function)
            : base(name)
        {
            this.TypeSafeFunction = function;
            this.Thread = new Thread(() =>
            {
                Object = function();
            });
        }

        protected internal Func<T> TypeSafeFunction
        {
            get;
            set;
        }

        public T Result
        {
            get
            {
                if (Object != null)
                {
                    return (T)Object;
                }
                return default(T);
            }
        }
    }

    public class NamedThread
    {
        internal NamedThread(string name)
        {
            this.Name = name;
        }

        public NamedThread(string name, Thread thread)
        {
            this.Name = name;
            if (thread.ThreadState == ThreadState.Unstarted)
            {
                thread.IsBackground = true;
            }

            this.Thread = thread;
        }

        public NamedThread(string name, Action action)
        {
            this.Name = name;
            this.Action = action;
            this.Thread = new Thread(() =>
            {
                action();
            })
            {
                IsBackground = true
            };
        }

        public NamedThread(string name, Func<object> function)
        {
            this.Name = name;
            this.Function = function;
            this.Thread = new Thread(() =>
            {
                Object = function();
            })
            {
                IsBackground = true
            };
        }

        /// <summary>
        /// The Action originally passed to the constructor of 
        /// the current NamedThread.  This may be null
        /// if the Action specific constructor was not used
        /// </summary>
        protected internal Action Action
        {
            get;
            set;
        }

        /// <summary>
        /// The Function originally passed to the constructor 
        /// of the current NamedThread.  This may be null if
        /// the Function specific constructor was not used
        /// </summary>
        protected internal Func<object> Function
        {
            get;
            set;
        }

        /// <summary>
        /// Implicitly convert NamedThread to Thread
        /// </summary>
        /// <param name="thread"></param>
        /// <returns></returns>
        public static implicit operator Thread(NamedThread thread)
        {
            return thread.Thread;
        }

        public ThreadState ThreadState
        {
            get
            {
                return Thread.ThreadState;
            }
        }

        public object Object { get; set; }

        public string Name { get; set; }
        public Thread Thread { get; set; }

        public void Start()
        {
            Thread.Start();
        }

		public void Abort()
		{
			if (Thread != null && Thread.ThreadState == System.Threading.ThreadState.Running)
			{
				Thread.Abort();
			}
		}

        static ConcurrentDictionary<string, NamedThread> _namedThreads = new ConcurrentDictionary<string, NamedThread>();

        public static NamedThread Start(string name, Action action, bool abortExisting = false)
        {
            NamedThread thread = new NamedThread(name, action);
            return SetNamedThread(name, thread, abortExisting);
        }

        public static NamedThread Start(string name, Func<object> function, bool abortExisting = false)
        {
            NamedThread thread = new NamedThread(name, function);
            return SetNamedThread(name, thread, abortExisting);
        }

        public static NamedThread<T> Start<T>(string name, Func<T> function, bool abortExisting = false)
        {
            NamedThread<T> thread = new NamedThread<T>(name, function);
            SetNamedThread(name, thread, abortExisting);
            return thread;
        }

        public static void Abort(string name)
        {
            if (_namedThreads.ContainsKey(name))
            {
                try
                {
                    _namedThreads[name].Abort();
                }
                catch (Exception ex)
                {
                    Log.Trace("Exception aborting named thread ({0}): {1}", ex, name, ex.Message);
                }
            }
        }

        private static NamedThread SetNamedThread(string name, NamedThread thread, bool abortExisting)
        {
            if (_namedThreads.ContainsKey(name))
            {
                if (abortExisting)
                {
                    _namedThreads[name].Abort();
                }
                _namedThreads[name] = thread;
            }
            else
            {
                _namedThreads.TryAdd(name, thread);
            }
            thread.Start();
            return thread;
        }
    }
}
