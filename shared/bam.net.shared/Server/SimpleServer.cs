using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bam.Net.Server
{
    public abstract class SimpleServer<R> where R: IResponder
    {
        HttpServer _server;
        public SimpleServer(R responder, ILogger logger)
        {
            Responder = responder;
            Logger = logger ?? Log.Default;
            CreatedOrChangedHandler = (o, a) => { };
            RenamedHandler = (o, a) => { };
            HostPrefixes = new HashSet<HostPrefix>
            {
                new HostPrefix { Port = 80, HostName = "localhost", Ssl = false }
            };
            MonitorDirectories = new string[] { Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) };
        }

        /// <summary>
        /// An array of hosts that this server will respond to
        /// </summary>
        public HashSet<HostPrefix> HostPrefixes { get; set; }
        
        /// <summary>
        /// The responder
        /// </summary>
        public R Responder { get; set; }
        
        /// <summary>
        /// The logger
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// The FileSystemWatchers; one each for create, changed and renamed
        /// </summary>
        public List<FileSystemWatcher> FileSystemWatchers { get; protected set; }

        /// <summary>
        /// An array of directories to monitor for
        /// created, changed or renamed files
        /// </summary>
        public string[] MonitorDirectories { get; set; }

        /// <summary>
        /// The delegate that will be subscribed to the Create
        /// and Changed handler of the underlying FileSystemWatcher(s)
        /// </summary>
        public FileSystemEventHandler CreatedOrChangedHandler { get; set; }

        public virtual void Start()
        {
            Logger.RestartLoggingThread();
            this.FileSystemWatchers = new List<FileSystemWatcher>();
            this.WireEventHandlers();
            _server.Start(HostPrefixes.ToArray());
        }

        public virtual void Stop()
        {
            Logger.StopLoggingThread();
            _server.Stop();
        }
        /// <summary>
        /// The delegate that will be subscribed to the renamed event of the underlying
        /// FileSystemWatcher(s)
        /// </summary>
        public RenamedEventHandler RenamedHandler { get; set; }

        /// <summary>
        /// Wire the event handlers
        /// </summary>
        protected void WireEventHandlers()
        {
            _server = new HttpServer(Logger ?? Log.Default);
            WireServerRequestHandler();
            WireResponderEventHandlers();
            MonitorDirectories.Each(directory =>
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                FileSystemWatchers.Add(directoryInfo.OnChange(CreatedOrChangedHandler));
                FileSystemWatchers.Add(directoryInfo.OnCreated(CreatedOrChangedHandler));
                FileSystemWatchers.Add(directoryInfo.OnRenamed(RenamedHandler));
            });
        }

        private void WireServerRequestHandler()
        {
            _server.ProcessRequest += (context) =>
            {
                Responder.Respond(new HttpContextWrapper(context));
            };
        }

        private void WireResponderEventHandlers()
        {
            Responder.Responded += (r, context) =>
            {
                Server.Responder.FlushResponse(context);
                Logger.AddEntry("*** ({0}) Responded ***\r\n{1}", LogEventType.Information, r.Name, context.Request.PropertiesToString());
            };
            Responder.NotResponded += (r, context) =>
            {
                Server.Responder.FlushResponse(context);
                Logger.AddEntry("*** ({0}) Didn't Respond ***\r\n{1}", LogEventType.Warning, r.Name, context.Request.PropertiesToString());
            };
        }
    }
}
