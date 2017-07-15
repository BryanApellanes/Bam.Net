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
using System.Collections.Concurrent;

namespace Bam.Net.Server
{
    public class HttpServer : IDisposable
    {
        private static ConcurrentDictionary<HostPrefix, HttpServer> _listening = new ConcurrentDictionary<HostPrefix, HttpServer>();
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
        
        /// <summary>
        /// If true will attempt to stop
        /// other HttpServers that are listening on 
        /// the same port and hostname as what
        /// the current HttpServer is configured for
        /// when it is started
        /// </summary>
        public bool Usurped
        {
            get;
            set;
        }

        public void Start()
        {
            Start(HostPrefixes);
        }
        public void Start(params HostPrefix[] hostPrefixes)
        {
            Start(Usurped, hostPrefixes);
        }

        static object _startLock = new object();
        public void Start(bool usurped, params HostPrefix[] hostPrefixes)
        {
            lock (_startLock)
            {
                hostPrefixes.Each(hp =>
                {
                    if (!_listening.ContainsKey(hp))
                    {
                        AddHostPrefix(hp);
                    }
                    else if (usurped && _listening.ContainsKey(hp))
                    {
                        _listening[hp].Stop();
                        _listening.TryRemove(hp, out HttpServer ignore);
                        AddHostPrefix(hp);
                    }
                    else
                    {
                        _logger.AddEntry("HttpServer: Another HttpServer is already listening for host {0}", LogEventType.Warning, hp.ToString());
                    }
                });

                _listener.Start();
                _handlerThread.Start();
            }
        }

        private void AddHostPrefix(HostPrefix hp)
        {
            _listening.TryAdd(hp, this);
            string path = hp.ToString();
            _logger.AddEntry("HttpServer: {0}", path);
            _listener.Prefixes.Add(path);
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

            foreach (HostPrefix hp in HostPrefixes)
            {
                if (_listening.ContainsKey(hp))
                {
                    if(_listening[hp] == this)
                    {
                        if (_listening.TryRemove(hp, out HttpServer server))
                        {
                            server.Stop();
                        }
                    }
                }
            }
            
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
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    Task.Run(() => ProcessRequest(context));
                }
                catch { }
            }
        }

        public event Action<HttpListenerContext> ProcessRequest;
    }
}
