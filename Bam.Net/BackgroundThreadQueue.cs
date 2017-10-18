﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net
{
    /// <summary>
    /// A queue processing facility that processes
    /// enqueued items in a background thread
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BackgroundThreadQueue<T>: Loggable
    {
        bool _warned;
        public BackgroundThreadQueue()
        {
            Continue = true;
            Process = (o) => 
            {
                if (!_warned)
                {
                    _warned = true;
                    FireEvent(Exception, new BackgroundThreadQueueEventArgs { Exception = new InvalidOperationException("No processor defined") });
                }
            };
        }

        public BackgroundThreadQueue(Action<T> process)
        {
            Continue = true;
            Process = process;
        }

        public int WriteQueueCount
        {
            get
            {
                return _processQueue.Count;
            }
        }

        ConcurrentQueue<T> _processQueue = new ConcurrentQueue<T>();
        public void Enqueue(T data)
        {
            if (ProcessThread.ThreadState != ThreadState.Running)
            {
                _processThread = null;
                ProcessThread.Start();
            }
            
            _processQueue.Enqueue(data);
            _waitSignal.Set();
        }

        public event EventHandler Exception;
        protected bool Continue { get; set; }
        public event EventHandler Waiting;
        public event EventHandler Processing;
        public event EventHandler QueueEmptied;
        Thread _processThread;
        AutoResetEvent _waitSignal = new AutoResetEvent(false);
        object _processThreadLock = new object();
        public Thread ProcessThread
        {
            get
            {
                return _processThreadLock.DoubleCheckLock(ref _processThread, () =>
                {
                    _processThread = new Thread(() =>
                    {
                        while (Continue)
                        {
                            try
                            {
                                FireEvent(Waiting, new BackgroundThreadQueueEventArgs());
                                _waitSignal.WaitOne();
                                FireEvent(Processing, new BackgroundThreadQueueEventArgs());
                                while (_processQueue.Count > 0)
                                {
                                    if (_processQueue.TryDequeue(out T val))
                                    {
                                        Process(val);
                                    }
                                }
                                FireEvent(QueueEmptied, new BackgroundThreadQueueEventArgs());
                            }
                            catch (Exception ex)
                            {
                                FireEvent(Exception, new BackgroundThreadQueueEventArgs { Exception = ex });
                            }
                        }
                    })
                    {
                        IsBackground = true
                    };
                    return _processThread;
                });
            }
        }

        public Action<T> Process { get; set; }
    }
}
