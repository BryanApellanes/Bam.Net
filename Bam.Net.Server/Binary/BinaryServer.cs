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
using System.Threading.Tasks;

namespace Bam.Net.Server.Binary
{
    public abstract class BinaryServer<T>: BinaryServer
    {
        protected internal override void ReadRequest(TcpClient client)
        {
            while (client.Connected)
            {
                try
                {
                    GetStreamData(client, out NetworkStream stream, out byte[] requestData);

                    BinaryRequest<T> request = requestData.FromBinaryBytes<BinaryRequest<T>>();
                    ProcessRequest(new BinaryContext<T>
                    {
                        Request = request,
                        ResponseStream = stream,
                        Encoding = Encoding
                    });
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Error reading request: {0}", ex, ex.Message);
                }
            }
        }

        public virtual void WriteResponse(BinaryContext context, T message)
        {
            BinaryResponse<T> msg = new BinaryResponse<T> { Message = message };
            WriteResponse(context, msg);
        }
    }

    public abstract class BinaryServer: Loggable, IConfigurable, IDisposable
    {
        public BinaryServer(ILogger logger = null, int port = 8888)
        {
            Logger = logger ?? Log.Default;
            Port = port;
            Name = 6.RandomLetters();

            Started += (o, a) =>
            {
                Subscribe(logger);
            };
        }

        public string Name { get; set; }

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

        public int Port
        {
            get;
            set;
        }

        public string[] RequiredProperties
        {
            get { return new string[] { "Port", "MaxThreads", "ReadBufferSize" }; }
        }
        [Verbosity(VerbosityLevel.Information, MessageFormat = "\r\nBinaryServer={Name};Port={Port};Started")]
        public event EventHandler Starting;

        [Verbosity(VerbosityLevel.Information, MessageFormat = "\r\nBinaryServer={Name};Port={Port};Started")]
        public event EventHandler Started;

        protected void OnStarting()
        {
            Starting?.Invoke(this, new EventArgs());
        }

        protected void OnStarted()
        {
            Started?.Invoke(this, new EventArgs());
        }

		[Verbosity(VerbosityLevel.Information, MessageFormat = "\r\nBinaryServer={Name};Port={Port};Stopped")]
        public event EventHandler Stopped;

        protected void OnStopped()
        {
            Stopped?.Invoke(this, new EventArgs());
        }

        [Verbosity(LogEventType.Error, MessageFormat = "\r\nLastMessage: {LastExceptionMessage}")]
        public event EventHandler ExceptionThrown;

        protected void OnException(Exception ex)
        {
            ExceptionThrown?.Invoke(this, new ErrorEventArgs(ex));
        }

        [Verbosity(LogEventType.Error, MessageFormat = "\r\nLastMessage: {LastExceptionMessage}")]
        public event EventHandler InitializationException;

        protected void OnInitializationException(Exception ex)
        {
            InitializationException?.Invoke(this, new ErrorEventArgs(ex));
        }               

        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            Listener.Stop();
            OnStopped();
        }

        public void Start()
        {
            try
            {
                OnStarting();
                Listener = new TcpListener(IPAddress.Any, Port);

                try
                {
                    Listener.Start();

                    Task.Run(() => Listen());                    
                }
                catch (Exception ex)
                {
                    LastExceptionMessage = ex.Message;
                    OnException(ex);
                }
                OnStarted();
            }
            catch (Exception ex)
            {
                LastExceptionMessage = ex.Message;
                OnInitializationException(ex);
            }
        }
        protected TcpListener Listener { get; set; }
        protected ILogger Logger { get; set; }

        protected virtual void Listen()
        {
            while (true)
            {
                TcpClient client = Listener.AcceptTcpClient();
                Logger.Info("Client Connected");
                Task.Run(() => ReadRequest(client));
            }
        }

        protected internal virtual void ReadRequest(TcpClient client)
        {
            while (client.Connected)
            {
                try
                {
                    GetStreamData(client, out NetworkStream stream, out byte[] requestData);

                    BinaryRequest request = requestData.FromBinaryBytes<BinaryRequest>();
                    ProcessRequest(new BinaryContext
                    {
                        Request = request,
                        ResponseStream = stream,
                        Encoding = Encoding
                    });
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Error reading request: {0}", ex, ex.Message);
                }
            }            
        }

        protected void GetStreamData(TcpClient client, out NetworkStream stream, out byte[] requestData)
        {
            try
            {
                stream = client.GetStream();
                byte[] msgLength = new byte[4]; // int length is in the first 4 bytes
                stream.Read(msgLength, 0, 4);
                int length = BitConverter.ToInt32(msgLength, 0);
                requestData = new byte[length];
                stream.Read(requestData, 0, length);
            }
            catch (Exception ex)
            {
                stream = null;
                requestData = new byte[] { };
                Logger.AddEntry("Error reading stream data: {0}", ex, ex.Message);
            }
        }

        public abstract void ProcessRequest(BinaryContext context);
        
        protected virtual void WriteResponse(BinaryContext context, object message)
        {
            BinaryResponse msg = new BinaryResponse { Message = message };
            WriteResponse(context, msg);
        }

        protected static void WriteResponse(BinaryContext context, BinaryResponse msg)
        {
            byte[] binMsg = msg.ToBinaryBytes();
            List<byte> sendMsg = new List<byte>();
            sendMsg.AddRange(BitConverter.GetBytes(binMsg.Length)); // put the total length of the stream in the first 4 bytes of the response
            sendMsg.AddRange(binMsg);
            byte[] sendBytes = sendMsg.ToArray();
            context.ResponseStream.Write(sendBytes, 0, sendBytes.Length);
        }

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