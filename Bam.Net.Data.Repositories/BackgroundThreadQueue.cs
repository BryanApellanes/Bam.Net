using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
    public class BackgroundThreadQueue<T>
    {
        bool _warned;
        public BackgroundThreadQueue()
        {
            Process = (o) => 
            {
                if (!_warned)
                {
                    _warned = true;
                    Log.Warn("No queue processor defined");
                }
            };
        }

        public int WriteQueueCount
        {
            get
            {
                return _processQueue.Count;
            }
        }

        Queue<T> _processQueue = new Queue<T>();
        object _processQueueLock = new object();
        public void Enqueue(T data)
        {
            if (ProcessThread.ThreadState != ThreadState.Running)
            {
                _processThread = null;
                ProcessThread.Start();
            }

            lock (_processQueueLock)
            {
                _processQueue.Enqueue(data);
                _waitSignal.Set();
            }
        }

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
                        while (true)
                        {
                            _waitSignal.WaitOne();
                            lock (_processQueueLock)
                            {
                                while (_processQueue.Count > 0)
                                {
                                    Process(_processQueue.Dequeue());
                                }
                            }
                        }
                    });

                    _processThread.IsBackground = true;

                    return _processThread;
                });
            }
        }

        public Action<T> Process { get; set; }
    }
}
