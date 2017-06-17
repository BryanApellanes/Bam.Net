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

namespace Bam.Net.Server
{
    public abstract class ResponderBase : Loggable, IResponder
    {
        Dictionary<string, string> _contentTypes;        
        public ResponderBase(BamConf conf)
        {
            this.BamConf = conf;
            this.Logger = Log.Default;

            this._contentTypes = new Dictionary<string, string>();
            this._contentTypes.Add(".json", "application/json");
            this._contentTypes.Add(".js", "application/javascript");
            this._contentTypes.Add(".css", "text/css");
            this._contentTypes.Add(".jpg", "image/jpg");
            this._contentTypes.Add(".gif", "image/gif");
            this._contentTypes.Add(".png", "image/png");
            this._contentTypes.Add(".html", "text/html");
            
            this._respondToPrefixes = new List<string>();
            this._ignorePrefixes = new List<string>();

            this.AddRespondToPrefix(ResponderSignificantName);
        }

        public ResponderBase(BamConf conf, ILogger logger)
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
            private set;
        }

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
            string path = context.Request.Url.AbsolutePath;
            if (MayRespond(context))
            {
                result = TryRespond(context);
                if (result)
                {
                    OnResponded(context);
                }
                else
                {
                    OnNotResponded(context);
                }
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
        protected void OnResponded(IHttpContext context)
        {
            if (Responded != null)
            {
                Responded(this, context);
            }
        }

        protected void OnNotResponded(IHttpContext context)
        {
            if (NotResponded != null)
            {
                NotResponded(this, context);
            }
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
