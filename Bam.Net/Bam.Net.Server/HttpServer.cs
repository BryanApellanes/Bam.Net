/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using Bam.Net;
using Bam.Net.Logging;

namespace Bam.Net.Server
{
    public class HttpServer : IDisposable
    {
        private readonly HttpListener _listener;
        private readonly Thread _handlerThread;
        private readonly Thread[] _workers;
        private readonly ManualResetEvent _stop, _ready;
        private Queue<HttpListenerContext> _queue;
        private int _port;
        private ILogger _logger;

        public HttpServer(ILogger logger = null)
            : this(50, logger)
        { }

        public HttpServer(int maxThreads, ILogger logger)
        {
            logger = logger ?? Log.Default;
            //_port = port;
            _workers = new Thread[maxThreads];
            _queue = new Queue<HttpListenerContext>();
            _stop = new ManualResetEvent(false);
            _ready = new ManualResetEvent(false);
            _listener = new HttpListener();
            _handlerThread = new Thread(HandleRequests);
            _logger = logger;
            _hostPrefixes = new List<HostPrefix>();
        }

        List<HostPrefix> _hostPrefixes;
        public HostPrefix[] HostPrefixes
        {
            get
            {
                return _hostPrefixes.ToArray();
            }
            set
            {
                _hostPrefixes = new List<HostPrefix>(value);
            }
        }
        

        public void Start()
        {
            Start(HostPrefixes);
        }
        
        public void Start(params HostPrefix[] hostName)
        {
            hostName.Each(hp =>
            {
                string path = hp.ToString();
                _logger.AddEntry("HttpServer: {0}", path);
                _listener.Prefixes.Add(path);
            });
            
            _listener.Start();
            _handlerThread.Start();

            for (int i = 0; i < _workers.Length; i++)
            {
                _workers[i] = new Thread(Worker);
                _workers[i].Start();
            }
        }

        public void Dispose()
        {
			IsDisposed = true;
            Stop();
        }

		public bool IsDisposed { get; private set; }

        public void Stop()
        {
            _stop.Set();
			if (_handlerThread.ThreadState == ThreadState.Running)
			{
				_handlerThread.Abort();
				_handlerThread.Join();
			}
            foreach (Thread worker in _workers)
            {
				if (worker != null)
				{
					worker.Join(300);
				}
            }
            _listener.Stop();
        }

        private void HandleRequests()
        {
            while (_listener.IsListening)
            {
                IAsyncResult context = _listener.BeginGetContext(ContextReady, null);

                if (0 == WaitHandle.WaitAny(new[] { _stop, context.AsyncWaitHandle }))
                    return;
            }
        }

        private void ContextReady(IAsyncResult ar)
        {
            try
            {
                lock (_queue)
                {
                    _queue.Enqueue(_listener.EndGetContext(ar));
                    _ready.Set();
                }
            }
            catch { return; }
        }

        private void Worker()
        {
            WaitHandle[] wait = new[] { _ready, _stop };
            while (0 == WaitHandle.WaitAny(wait))
            {
                HttpListenerContext context;
                lock (_queue)
                {
                    if (_queue.Count > 0)
                        context = _queue.Dequeue();
                    else
                    {
                        _ready.Reset();
                        continue;
                    }
                }

                try
                {
                    if (ProcessRequest != null)
                    {
                        ProcessRequest(context);
                    }
                }
                catch (Exception ex)
                {
                    _logger.AddEntry("An error occurred processing HTTP request: {0}", ex, ex.Message);
                }
            }
        }

        public event Action<HttpListenerContext> ProcessRequest;
    }
}
