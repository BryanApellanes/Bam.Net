/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using Bam.Net.Logging;
using Bam.Net.Configuration;

namespace Bam.Net.Server
{
    public class TcpServer: Loggable, IConfigurable, IDisposable
    {
        TcpListener _listener;
        readonly Thread _handlerThread;
        readonly Thread[] _workers;
        readonly ManualResetEvent _stop, _ready;
        Queue<TcpClient> _queue;
        ILogger _logger;
        byte[] _readBuffer;

        public TcpServer(int maxThreads, ILogger logger, int port = 8888, int readBufferSize = 1024)
        {
            _ready = new ManualResetEvent(false);
            _stop = new ManualResetEvent(false);
            _workers = new Thread[maxThreads];
            _queue = new Queue<TcpClient>();
            _handlerThread = new Thread(Accept);
            _logger = logger;

            // properties are strings so they can be configured by the configurers
            this.MaxThreads = maxThreads.ToString(); 
            this.Port = port.ToString();
            this.ReadBufferSize = readBufferSize.ToString();

            this.Started += (o, a) =>
            {
                this.Subscribe(logger);
            };
        }

        Encoding _encoding;
        object _encodingLock = new object();
        public Encoding Encoding
        {
            get
            {
                return _encodingLock.DoubleCheckLock(ref _encoding, () => Encoding.UTF8);
            }
            set
            {
                _encoding = value;
            }
        }

        public string LastExceptionMessage
        {
            get;
            set;
        }

        public string Port
        {
            get;
            set;
        }

        public string MaxThreads
        {
            get;
            set;
        }

        public string ReadBufferSize
        {
            get;
            set;
        }

        public string[] RequiredProperties
        {
            get { return new string[] { "Port", "MaxThreads", "ReadBufferSize" }; }
        }

		[Verbosity(VerbosityLevel.Information, MessageFormat = "\r\nPort={Port};MaxThreads={MaxThreads};ReadBufferSize={ReadBufferSize}")]
        public event EventHandler Started;

        protected void OnStarted()
        {
            if (Started != null)
            {
                Started(this, new EventArgs());
            }
        }

		[Verbosity(VerbosityLevel.Information, MessageFormat = "\r\nPort={Port};MaxThreads={MaxThreads};ReadBufferSize={ReadBufferSize}")]
        public event EventHandler Stopped;

        protected void OnStopped()
        {
            if (Stopped != null)
            {
                Stopped(this, new EventArgs());
            }
        }

        [Verbosity(LogEventType.Error, MessageFormat = "\r\nLastMessage: {LastExceptionMessage}")]
        public event EventHandler ExceptionThrown;

        protected void OnException(Exception ex)
        {
            if (ExceptionThrown != null)
            {
                ExceptionThrown(this, new ErrorEventArgs(ex));
            }
        }

        [Verbosity(LogEventType.Error, MessageFormat = "\r\nLastMessage: {LastExceptionMessage}")]
        public event EventHandler InitializationException;

        protected void OnInitializationException(Exception ex)
        {
            if (InitializationException != null)
            {
                InitializationException(this, new ErrorEventArgs(ex));
            }
        }
               

        private void Validate(out int port, out int threadCount, out int readBufferSize)
        {
            this.CheckRequiredProperties();
            
            if (!int.TryParse(Port, out port))
            {
                throw new ArgumentException("The specified Port ({0}) couldn't be parsed as an Integer"._Format(Port));
            }

            if (!int.TryParse(MaxThreads, out threadCount))
            {
                throw new ArgumentException("The specified thread count ({0}) couldn't be parsed as an Integer"._Format(MaxThreads));
            }

            if(!int.TryParse(ReadBufferSize, out readBufferSize))
            {
                throw new ArgumentException("The specified read buffer size ({0}) couldn't be parsed as an Integer"._Format(ReadBufferSize));
            }
        }

        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            _stop.Set();
            _handlerThread.Join();
            _workers.Each(worker =>
            {
                worker.Join();
            });
            _listener.Stop();
            OnStopped();
        }

        protected internal IPEndPoint LocalEndPoint
        {
            get;
            set;
        }

        public void Start()
        {
            try
            {
                OnStarted();

                int port;
                int threadCount;
                int readBufferSize;
                Validate(out port, out threadCount, out readBufferSize);
                LocalEndPoint = new IPEndPoint(IPAddress.Any, port);

                _listener = new TcpListener(LocalEndPoint);
                _readBuffer = new byte[readBufferSize];

                try
                {
                    // start the listener
                    _listener.Start();
                    // start the Accept thread
                    _handlerThread.Start();
                    // start a thread for every worker
                    for (int i = 0; i < threadCount; i++)
                    {
                        _workers[i] = new Thread(Worker);
                        _workers[i].Start();
                    }
                }
                catch (Exception ex)
                {
                    LastExceptionMessage = ex.Message;
                    OnException(ex);
                }

            }
            catch (Exception ex)
            {
                LastExceptionMessage = ex.Message;
                OnInitializationException(ex);
            }
        }

        private void Accept()
        {
            while (true)
            {
                try
                {
                    lock (_queue)
                    {
                        TcpClient client = _listener.AcceptTcpClient();
                        _queue.Enqueue(client);
                        _ready.Set();
                    }
                }
                catch { };
            }
        }

        private void Worker()
        {
            WaitHandle[] wait = new[] { _ready, _stop };
            while (0 == WaitHandle.WaitAny(wait))
            {
                TcpClient client;
                lock (_queue)
                {
                    if (_queue.Count > 0)
                        client = _queue.Dequeue();
                    else
                    {
                        _ready.Reset();
                        continue;
                    }
                }

                try
                {
                    ReadRequest(client);
                }
                catch (Exception ex)
                {
                    _logger.AddEntry("An error occurred processing TCP request: {0}", ex, ex.Message);
                    OnException(ex);
                }
            }
        }

        protected internal void ReadRequest(TcpClient client)
        {
            // get a stream for communication
            NetworkStream stream = client.GetStream();

            int i;
            while ((i = stream.Read(_readBuffer, 0, _readBuffer.Length)) != 0)
            {
                TcpContext state = new TcpContext();
                state.RequestData = _readBuffer;
                state.ResponseStream = stream;
                state.Encoding = Encoding;
                if (ProcessRequest != null)
                {
                    ProcessRequest(state);
                }
            }
        }

        public Action<TcpContext> ProcessRequest;

        public void Configure(IConfigurer configurer)
        {
            configurer.Configure(this);
            this.CheckRequiredProperties();
        }

        public void Configure(object configuration)
        {
            this.CopyProperties(configuration);
            this.CheckRequiredProperties();
        }

    }
}