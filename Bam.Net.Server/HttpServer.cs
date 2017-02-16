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
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class HttpServer : IDisposable
    {
        private readonly HttpListener _listener;
        private readonly Thread _handlerThread;
        private ILogger _logger;

        public HttpServer(ILogger logger = null)
            : this(50, logger)
        { }

        public HttpServer(int maxThreads, ILogger logger)
        {
            logger = logger ?? Log.Default;
            
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
        }

        public void Dispose()
        {
			IsDisposed = true;
            Stop();
        }

		public bool IsDisposed { get; private set; }

        public void Stop()
        {
            _listener.Stop();
            if (_handlerThread.ThreadState == ThreadState.Running)
			{
				_handlerThread.Abort();
				_handlerThread.Join();
			}
        }
        
        private void HandleRequests()
        {
            while (_listener.IsListening)
            {
                HttpListenerContext context = _listener.GetContext();
                Task.Run(() => ProcessRequest(context));
            }
        }

        public event Action<HttpListenerContext> ProcessRequest;
    }
}
