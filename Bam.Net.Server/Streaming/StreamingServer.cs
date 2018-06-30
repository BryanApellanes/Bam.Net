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

namespace Bam.Net.Server.Streaming
{
    public abstract class StreamingServer<TRequest, TResponse>: StreamingServer
    {
        protected internal override void ReadRequest(TcpClient client)
        {
            while (client.Connected)
            {
                try
                {
                    GetStreamData(client, out NetworkStream stream, out byte[] requestData);

                    StreamingRequest<TRequest> request = requestData.FromBinaryBytes<StreamingRequest<TRequest>>();
                    StreamingContext<TRequest> ctx = new StreamingContext<TRequest>
                    {
                        Request = request,
                        ResponseStream = stream,
                        Encoding = Encoding
                    };
                    TResponse response = ProcessRequest(ctx);
                    WriteResponse(ctx, response);
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Error reading request: {0}", ex, ex.Message);
                }
            }
        }

        public virtual void WriteResponse(StreamingContext context, TResponse message)
        {
            StreamingResponse<TResponse> msg = new StreamingResponse<TResponse> { Data = message };
            WriteResponse(context, msg);
        }

        public override void ProcessRequest(StreamingContext context)
        {
            ProcessRequest((StreamingContext<TRequest>)context);
        }

        public abstract TResponse ProcessRequest(StreamingContext<TRequest> context);
    }

    public abstract class StreamingServer: Loggable, IConfigurable, IDisposable
    {
        public StreamingServer(ILogger logger = null, int port = 8888)
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


		[Verbosity(VerbosityLevel.Information, MessageFormat = "\r\nBinaryServer={Name};Port={Port};Stopped")]
        public event EventHandler Stopped;

        [Verbosity(LogEventType.Error, MessageFormat = "\r\nLastMessage: {LastExceptionMessage}")]
        public event EventHandler StartExceptionThrown;

        [Verbosity(LogEventType.Error, MessageFormat = "\r\nLastMessage: {LastExceptionMessage}")]
        public event EventHandler RequestExceptionThrown;
        
        [Verbosity(LogEventType.Error, MessageFormat = "\r\nLastMessage: {LastExceptionMessage}")]
        public event EventHandler InitializationException;             

        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            Listener.Stop();
            FireEvent(Stopped);
        }

        public void Start()
        {
            try
            {
                FireEvent(Starting);
                Listener = new TcpListener(IPAddress.Any, Port);

                try
                {
                    Listener.Start();

                    Task.Run(() => Listen());                    
                }
                catch (Exception ex)
                {
                    LastExceptionMessage = ex.Message;
                    FireEvent(StartExceptionThrown, new ErrorEventArgs(ex));
                }
                FireEvent(Started);
            }
            catch (Exception ex)
            {
                LastExceptionMessage = ex.Message;
                FireEvent(InitializationException, new ErrorEventArgs(ex));
            }
        }

        protected TcpListener Listener { get; set; }
        protected ILogger Logger { get; set; }

        protected virtual void Listen()
        {
            while (true)
            {
                TcpClient client = Listener.AcceptTcpClient();
                try
                {
                    Logger.Info("Client Connected: LocalEndpoint={0}, RemoteEndpoint={1}", client.Client.LocalEndPoint.ToString(), client.Client.RemoteEndPoint.ToString());
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Error logging connection: {0}", ex, ex.Message);
                }
                Task.Run(() => ReadRequest(client));
            }
        }

        protected internal virtual void ReadRequest(TcpClient client)
        {
            int retryCount = 3;
            while (client.Connected)
            {
                try
                {
                    if(retryCount > 0)
                    {
                        GetStreamData(client, out NetworkStream stream, out byte[] requestData);

                        StreamingRequest request = requestData.FromBinaryBytes<StreamingRequest>();
                        ProcessRequest(new StreamingContext
                        {
                            Request = request,
                            ResponseStream = stream,
                            Encoding = Encoding
                        });
                    }
                }
                catch (Exception ex)
                {
                    FireEvent(RequestExceptionThrown, new ErrorEventArgs(ex));
                    Logger.AddEntry("Error reading request (retryCount={0}): {1}", ex, retryCount.ToString(), ex.Message);
                    --retryCount;
                }
            }            
        }

        protected virtual void GetStreamData(TcpClient client, out NetworkStream stream, out byte[] requestData)
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
                throw;
            }
        }

        public abstract void ProcessRequest(StreamingContext context);
        
        protected virtual void WriteResponse(StreamingContext context, object message)
        {
            StreamingResponse msg = new StreamingResponse { Data = message };
            WriteResponse(context, msg);
        }

        protected static void WriteResponse(StreamingContext context, StreamingResponse msg)
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