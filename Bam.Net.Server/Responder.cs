/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Logging;
using System.IO;
using Bam.Net.ServiceProxy;
using Bam.Net.Server;
using Bam.Net.Server.Renderers;
using Bam.Net.Configuration;
using System.IO.Compression;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public abstract class Responder : Loggable, IResponder
    {
        Dictionary<string, string> _contentTypes;        
        public Responder(BamConf conf)
        {
            BamConf = conf;
            Logger = Log.Default;
            UriApplicationNameResolver = new UriApplicationNameResolver(conf);

            _contentTypes = new Dictionary<string, string>
            {
                { ".json", "application/json" },
                { ".js", "application/javascript" },
                { ".css", "text/css" },
                { ".jpg", "image/jpg" },
                { ".gif", "image/gif" },
                { ".png", "image/png" },
                { ".html", "text/html" }
            };
            _respondToPrefixes = new List<string>();
            _ignorePrefixes = new List<string>();

            AddRespondToPrefix(ResponderSignificantName);
        }

        public Responder(BamConf conf, ILogger logger)
            : this(conf)
        {
            this.Logger = logger;
        }

        ILogger _logger;
        object _loggerLock = new object();
        public ILogger Logger
        {
            get
            {
                return _loggerLock.DoubleCheckLock(ref _logger, () => Log.Default);

            }
            internal set
            {
                _logger = value;
            }
        }


        public virtual void Initialize()
        {
            IsInitialized = true;
        }

        public virtual bool IsInitialized
        {
            get;
            protected set;
        }

        public UriApplicationNameResolver UriApplicationNameResolver { get; set; }

        /// <summary>
        /// The event that fires when a response is sent
        /// </summary>
        public event ResponderEventHandler Responded;
        /// <summary>
        /// The event that fires when a response is not sent
        /// </summary>
        public event ResponderEventHandler NotResponded;

        public BamConf BamConf
        {
            get;
            set;
        }

        public Fs ServerRoot
        {
            get
            {
                return BamConf.Fs;
            }
        }

        public Fs AppFs(string appName)
        {
            return BamConf.AppFs(appName);
        }

        /// <summary>
        /// Returns true if the AbsolutePath of the requested
        /// Url starts with the name of the current class.  Extenders
        /// will provide different implementations based on their
        /// requirements
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool MayRespond(IHttpContext context)
        {
            string lowered = context.Request.Url.AbsolutePath.ToLowerInvariant();
            bool result = false;

            if (!ShouldIgnore(lowered))
            {
                _respondToPrefixes.Each(prefix =>
                {
                    if (!result)
                    {
                        result = lowered.StartsWith(string.Format("/{0}", prefix.ToLowerInvariant()));
                    }
                });
            }

            return result;
        }

        public virtual bool Respond(IHttpContext context)
        {
            bool result = false;
            if (MayRespond(context))
            {
                result = TryRespond(context);
            }
            return result;
        }

        public abstract bool TryRespond(IHttpContext context);

        public static void SendResponse(IHttpContext context, string output, int statusCode = 200, Encoding encoding = null, Dictionary<string, string> headers = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            SendResponse(context.Response, output, statusCode, encoding, headers);
        }

        public static void SendResponse(IResponse response, string output, int statusCode = 200, Encoding encoding = null, Dictionary<string, string> headers = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            if (headers != null)
            {
                headers.Keys.Each(key =>
                {
                    response.Headers.Add(key, headers[key]);
                });
            }
            byte[] data = encoding.GetBytes(output);
            response.OutputStream.Write(data, 0, data.Length);
            response.StatusCode = statusCode;
            response.Close();
        }
        
        protected string GetContentTypeForExtension(string ext)
        {
            string contentType = string.Empty;
            if (this._contentTypes.ContainsKey(ext))
            {
                contentType = this._contentTypes[ext];
            }
            return contentType;
        }

        protected string GetContentTypeForPath(string path)
        {
            string contentType = string.Empty;
            string ext = Path.GetExtension(path);
            return GetContentTypeForExtension(ext);
        }

        protected void SetContentDisposition(IResponse response, string path)
        {
            if(Path.GetExtension(path).Equals(".pdf", StringComparison.InvariantCultureIgnoreCase))
            {
                response.AddHeader("Content-Disposition", $"attachment; filename={Path.GetFileName(path)}");
            }
        }

        protected void SetContentType(IResponse response, string path)
        {
            string contentType = GetContentTypeForPath(path);
            if (!string.IsNullOrEmpty(contentType))
            {
                response.ContentType = contentType;
            }
        }

        ConcurrentDictionary<string, byte[]> _pageCache;
        object _pageCacheLock = new object();
        protected ConcurrentDictionary<string, byte[]> Cache
        {
            get
            {
                return _pageCacheLock.DoubleCheckLock(ref _pageCache, () => new ConcurrentDictionary<string, byte[]>());
            }
        }

        ConcurrentDictionary<string, byte[]> _zippedPageCache;
        object _zippedPageCacheLock = new object();
        protected ConcurrentDictionary<string, byte[]> ZippedCache
        {
            get
            {
                return _zippedPageCacheLock.DoubleCheckLock(ref _zippedPageCache, () => new ConcurrentDictionary<string, byte[]>());
            }
        }
            

        protected Dictionary<string, string> ContentTypes
        {
            get
            {
                return this._contentTypes;
            }
        }

        public virtual string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Set the status code, flush the response and close the output 
        /// stream
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        public static void FlushResponse(IHttpContext context, int statusCode = 200)
        {
            context.Response.StatusCode = statusCode;
            context.Response.OutputStream.Flush();
            context.Response.OutputStream.Close();
        }

        protected static void WireResponseLogging(IResponder responder, ILogger logger)
        {
            responder.Responded += (r, context) =>
            {
                logger.AddEntry("*** ({0}) Responded ***\r\n{1}", LogEventType.Information, responder.Name, context.Request.PropertiesToString());
            };
            responder.NotResponded += (r, context) =>
            {
                logger.AddEntry("*** Didn't Respond ***\r\n{0}", LogEventType.Warning, context.Request.PropertiesToString());
            };
        }

        protected void WireResponseLogging(ILogger logger)
        {
            WireResponseLogging(this, logger);
        }

        protected internal void OnResponded(IHttpContext context)
        {
            Task.Run(() => Responded?.Invoke(this, context));
        }

        protected internal void OnNotResponded(IHttpContext context)
        {
            Task.Run(() => NotResponded?.Invoke(this, context));
        }

        List<string> _respondToPrefixes;
        protected internal void AddRespondToPrefix(string prefix)
        {
            prefix = prefix.ToLowerInvariant();
            if (_ignorePrefixes.Contains(prefix))
            {
                _ignorePrefixes.Remove(prefix);
            }

            _respondToPrefixes.Add(prefix);
        }

        List<string> _ignorePrefixes;
        protected internal void AddIgnorPrefix(string prefix)
        {
            prefix = prefix.ToLowerInvariant();
            if (_respondToPrefixes.Contains(prefix))
            {
                _respondToPrefixes.Remove(prefix);
            }

            _ignorePrefixes.Add(prefix);
        }

        protected internal bool WillIgnore(IHttpContext context)
        {
            return ShouldIgnore(context.Request.Url.AbsolutePath.ToLowerInvariant());
        }

        protected internal bool ShouldIgnore(string path)
        {
            bool result = false;
            _ignorePrefixes.Each(ignore =>
            {
                if (!result)
                {
                    result = path.ToLowerInvariant().StartsWith(string.Format("/{0}", ignore));
                }
            });

            return result;
        }

        protected static void SendResponse(IResponse response, byte[] data)
        {
            using (BinaryWriter bw = new BinaryWriter(response.OutputStream))
            {
                bw.Write(data);
                bw.Flush();
            }
        }

        protected static void SendResponse(IResponse response, string content)
        {
            SendResponse(response, Encoding.UTF8.GetBytes(content));
        }
        protected RendererFactory RendererFactory
        {
            get;
            set;
        }

        protected internal virtual string ResponderSignificantName
        {
            get
            {
                string responderSignificantName = this.Name;
                if (responderSignificantName.EndsWith("Responder", StringComparison.InvariantCultureIgnoreCase))
                {
                    responderSignificantName = responderSignificantName.Truncate(9);
                }
                return responderSignificantName;
            }
        }

        protected static bool ShouldZip(IRequest request)
        {
            if (request.Headers["Accept-Encoding"].DelimitSplit(",").ToList().Contains("gzip"))
            {
                return true;
            }
            return false;
        }

        protected static void SetGzipContentEncodingHeader(IResponse response)
        {
            response.AddHeader("Content-Encoding", "gzip");
        }

    }
}
