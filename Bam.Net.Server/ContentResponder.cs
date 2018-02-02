/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Caching.File;
using Bam.Net.Javascript;
using Bam.Net.Logging;
using Bam.Net.Server.Renderers;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.UserAccounts.Data;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Bam.Net.Server
{
    /// <summary>
    /// The primary responder for all content files found in ~s:/ (defined as BamServer.ContentRoot)
    /// </summary>
    public class ContentResponder : Responder, IInitialize<ContentResponder>
    {
        public const string IncludeFileName = "include.js";
        public const string LayoutFileExtension = ".layout";
        public ContentResponder(BamConf conf)
            : base(conf)
        {
            CommonTemplateRenderer = new CommonDustRenderer(this);
            FileCachesByExtension = new Dictionary<string, FileCache>();
            HostAppMappings = new Dictionary<string, HostAppMapping>();
            InitializeFileExtensions();
            InitializeCaches();
        }

        public ContentResponder(BamConf conf, ILogger logger)
            : base(conf, logger)
        {
            CommonTemplateRenderer = new CommonDustRenderer(this);
            FileCachesByExtension = new Dictionary<string, FileCache>();
            InitializeFileExtensions();
            InitializeCaches();
        }

        public List<string> FileExtensions { get; set; }
        public List<string> TextFileExtensions { get; set; }
        protected Dictionary<string, FileCache> FileCachesByExtension { get; set; }
        protected void InitializeFileExtensions()
        {
            FileExtensions = new List<string> { ".html", ".htm", ".js", ".json", ".css", ".yml", ".yaml", ".txt", ".md", ".layout", ".png", ".jpg", ".jpeg", ".gif", ".woff" };
            TextFileExtensions = new List<string> { ".html", ".htm", ".js", ".json", ".css", ".yml", ".yaml", ".layout", ".txt", ".md" };
        }
        protected void InitializeCaches()
        {
            foreach(string ext in FileExtensions)
            {
                FileCachesByExtension.AddMissing(ext, CreateCache(ext));
            }
        }

        /// <summary>
        /// The server content root path, not to be confused with the 
        /// application root which should be [Root]\apps\[appName]
        /// </summary>
        public string Root
        {
            get
            {
                return ServerRoot.Root;
            }
        }

        public override bool MayRespond(IHttpContext context)
        {
            return !WillIgnore(context);
        }

        public AppConf[] AppConfigs
        {
            get;
            internal set;
        }

        Dictionary<string, AppContentResponder> _appContentResponders;
        protected internal Dictionary<string, AppContentResponder> AppContentResponders
        {
            get
            {
                if (_appContentResponders == null)
                {
                    _appContentResponders = new Dictionary<string, AppContentResponder>();
                }

                return _appContentResponders;
            }
        }

        public bool IsAppsInitialized
        {
            get;
            private set;
        }

        /// <summary>
        /// The event that fires when templates are being initialized.
        /// This occurs after file system initialization
        /// </summary>
        public event Action<ContentResponder> CommonTemplateRendererInitializing;

        /// <summary>
        /// The event that fires when templates have completed initialization.
        /// </summary>
        public event Action<ContentResponder> CommonTemplateRendererInitialized;

        protected internal void OnCommonTemplateRendererInitializing()
        {
            CommonTemplateRendererInitializing?.Invoke(this);
        }

        protected internal void OnCommonTemplateRendererInitialized()
        {
            CommonTemplateRendererInitialized?.Invoke(this);
        }

        public ITemplateRenderer CommonTemplateRenderer
        {
            get;
            set;
        }
        public override void Subscribe(ILogger logger)
        {            
            if (!IsSubscribed(logger))
            {
                base.Subscribe(logger);
                string className = typeof(ContentResponder).Name;
                this.AppContentRespondersInitializing += (c) =>
                {
                    logger.AddEntry("{0}::AppContentRespondersInitializ(ING)", className);
                };
                this.AppContentRespondersInitialized += (c) =>
                {
                    logger.AddEntry("{0}::AppContentRespondersInitializ(ED)", className);
                };
                this.AppContentResponderInitializing += (c, a) =>
                {
                    logger.AddEntry("{0}::AppContentResponderInitializ(ING):{1}", className, a.Name);
                };
                this.AppContentResponderInitialized += (c, a) =>
                {
                    logger.AddEntry("{0}::AppContentResponderInitializ(ED):{1}", className, a.Name);
                };
                this.Initializing += (c) =>
                {
                    logger.AddEntry("{0}::Initializ(ING)", className);
                };
                this.Initialized += (c) =>
                {
                    logger.AddEntry("{0}::Initializ(ED)", className);
                };
                this.CommonTemplateRendererInitializing += (c) =>
                {
                    logger.AddEntry("{0}::TemplatesInitializ(ING)", className);
                };
                this.CommonTemplateRendererInitialized += (c) =>
                {
                    logger.AddEntry("{0}::TemplatesInitializ(ED)", className);
                };
            }
        }

        protected void SetEtag(IResponse response, string path, byte[] content)
        {
            string etag = content.Sha1();
            response.AddHeader("ETag", etag);
            Etags.Values.AddOrUpdate(path, etag, (p, v) => etag);
        }

        protected void SetLastModified(IResponse response, string path, DateTime lastModified)
        {
            response.AddHeader("Last-Modified", lastModified.ToUniversalTime().ToString("r"));
            Etags.LastModified.AddOrUpdate(path, lastModified, (p, v) => lastModified);
        }

        protected bool CheckEtags(IHttpContext context)
        {
            IRequest request = context.Request;
            IResponse response = context.Response;
            string path = request.Url.ToString();
            string etag = request.Headers["If-None-Match"];
            if (!string.IsNullOrEmpty(etag))
            {
                if (Etags.Values.ContainsKey(path) && Etags.Values[path].Equals(etag))
                {
                    response.StatusCode = 304;
                    return true;
                }
            }
            string lastModifiedString = request.Headers["If-Modified-Since"];
            if (!string.IsNullOrEmpty(lastModifiedString) && Etags.LastModified.ContainsKey(path))
            {
                DateTime modifiedSince = DateTime.Parse(lastModifiedString);
                if (Etags.LastModified[path] > modifiedSince)
                {
                    response.StatusCode = 304;
                    return true;
                }
            }
            return false;
        }

        protected virtual void SetBaseIgnorePrefixes()
        {
            AddIgnorPrefix("dao");
            AddIgnorPrefix("serviceproxy");
            AddIgnorPrefix("api");
            AddIgnorPrefix("bam");
            AddIgnorPrefix("get");
            AddIgnorPrefix("post");
            AddIgnorPrefix("securechannel");
        }

        protected internal void InitializeCommonTemplateRenderer()
        {
            OnCommonTemplateRendererInitializing();

            string viewRoot = Path.Combine(Root, "common", "views");
            DirectoryInfo dir = new DirectoryInfo(viewRoot);
            if (dir.Exists)
            {
                CommonTemplateRenderer = new CommonDustRenderer(this);
            }

            OnCommonTemplateRendererInitialized();
        }

        public event Action<ContentResponder> AppContentRespondersInitializing;
        public event Action<ContentResponder> AppContentRespondersInitialized;

        protected internal void OnAppContentRespondersInitializing()
        {
            AppContentRespondersInitializing?.Invoke(this);
        }

        public event Action<ContentResponder, AppConf> AppContentResponderInitializing;
        public event Action<ContentResponder, AppConf> AppContentResponderInitialized;

        protected internal void OnAppContentResponderInitializing(AppConf appConf)
        {
            AppContentResponderInitializing?.Invoke(this, appConf);
        }

        protected internal void OnAppContentResponderInitialized(AppConf appConf)
        {
            AppContentResponderInitialized?.Invoke(this, appConf);
        }

        protected internal void OnAppRespondersInitialized()
        {
            AppContentRespondersInitialized?.Invoke(this);
        }

        public Dictionary<string, HostAppMapping> HostAppMappings
        {
            get;
            set;
        }

        object _initAppsLock = new object();
        /// <summary>
        /// Initialize all the AppContentResponders for the 
        /// apps found in the ~s:/apps folder
        /// </summary>
        protected internal void InitializeAppResponders()
        {
            OnAppContentRespondersInitializing();
            lock (_initAppsLock)
            {
                if (!IsAppsInitialized)
                {
                    InitializeHostAppMap(BamConf);
                    InitializeAppResponders(BamConf.AppConfigs);

                    AppConfigs = BamConf.AppConfigs;

                    IsAppsInitialized = true;
                }
            }
            OnAppRespondersInitialized();
        }

        private void InitializeHostAppMap(BamConf bamConf)
        {
            string jsonFile = Path.Combine(bamConf.ContentRoot, "apps", "hostAppMap.json");
            HashSet<HostAppMapping> temp = new HashSet<HostAppMapping>();
            foreach(AppConf appConf in bamConf.AppConfigs)
            {
                temp.Add(new HostAppMapping { Host = appConf.Name, AppName = appConf.Name });
            }
            if (File.Exists(jsonFile))
            {
                HostAppMapping[] fromFile = jsonFile.FromJsonFile<HostAppMapping[]>();
                if(fromFile != null)
                {
                    foreach (HostAppMapping mapping in fromFile)
                    {
                        temp.Add(mapping);
                    }
                }
            }
            temp.ToJsonFile(jsonFile);
            HostAppMappings = temp.ToDictionary(ham=> ham.Host);
        }

        public event EventHandler FileUploading;
        public event EventHandler FileUploaded;
        
        private void InitializeAppResponders(AppConf[] configs)
        {
            string currentMode = ProcessMode.Current.Mode.ToString();
            configs.Where(c=> c.ProcessModes.Contains(currentMode)).Each(ac =>
            {
                OnAppContentResponderInitializing(ac);
                Logger.RestartLoggingThread();
                AppContentResponder responder = new AppContentResponder(this, ac)
                {
                    Logger = Logger
                };
                Subscribers.Each(logger =>
                {
                    logger.RestartLoggingThread();
                    responder.Subscribe(logger);
                });
                string appName = ac.Name.ToLowerInvariant();
                responder.Initialize();
                responder.FileUploading += (o, a) => FileUploading?.Invoke(o, a);
                responder.FileUploaded += (o, a) => FileUploaded?.Invoke(o, a);
                AppContentResponders[appName] = responder;

                OnAppContentResponderInitialized(ac);
            });
        }

        protected internal Includes GetCommonIncludes()
        {
            return GetCommonIncludes(Root);
        }

        protected static internal Includes GetCommonIncludes(string root)
        {
            string includeJs = Path.Combine(root, "apps", IncludeFileName);
            return GetIncludesFromIncludeJs(includeJs);
        }

        /// <summary>
        /// Gets the Includes for the specified AppConf.  Also adds
        /// the init.js and all viewModel .js files.
        /// </summary>
        /// <param name="appConf"></param>
        /// <returns></returns>
        protected static internal Includes GetAppIncludes(AppConf appConf)
        {
            string includeJs = Path.Combine(appConf.AppRoot.Root, IncludeFileName);
            string appRoot = Path.DirectorySeparatorChar.ToString();
            Includes includes = GetIncludesFromIncludeJs(includeJs);
            includes.Scripts.Each((scr, i) =>
            {
                includes.Scripts[i] = Path.Combine(appRoot, scr).Replace("\\", "/");
            });
            includes.Css.Each((css, i) =>
            {
                includes.Css[i] = Path.Combine(appRoot, css).Replace("\\", "/");
            });

            GetPageScripts(appConf).Each(script =>
            {
                includes.AddScript(Path.Combine(appRoot, script).Replace("\\", "/"));
            });

            DirectoryInfo viewModelsDir = appConf.AppRoot.GetDirectory("viewModels");
            if (!Directory.Exists(viewModelsDir.FullName))
            {
                Directory.CreateDirectory(viewModelsDir.FullName);
            }
            FileInfo[] viewModels = viewModelsDir.GetFiles("*.js");
            viewModels.Each(fi =>
            {
                includes.AddScript(Path.Combine(appRoot, "viewModels", fi.Name).Replace("\\", "/"));
            });

            includes.AddScript(Path.Combine(appRoot, "init.js").Replace("\\", "/"));

            return includes;
        }

        protected static internal string[] GetPageScripts(AppConf appConf)
        {
            BamApplicationManager manager = new BamApplicationManager(appConf.BamConf);
            string[] pageNames = manager.GetPageNames(appConf.Name);
            List<string> results = new List<string>();
            pageNames.Each(pageName =>
            {
                string script = "/" + Fs.CleanPath(Path.Combine("pages", pageName + ".js")).Replace("\\", "/"); // for use in html
                if (appConf.AppRoot.FileExists(script))
                {
                    results.Add(script);
                }
            });

            return results.ToArray();
        }

        static Dictionary<string, Includes> _includesCache;
        static object _includesCacheLock = new object();
        protected static internal Dictionary<string, Includes> IncludesCache
        {
            get
            {
                return _includesCacheLock.DoubleCheckLock(ref _includesCache, () => new Dictionary<string, Includes>());
            }
        }

        ConcurrentDictionary<string, byte[]> _pageMinCache;
        object _pageMinCacheLock = new object();
        protected ConcurrentDictionary<string, byte[]> MinCache
        {
            get
            {
                return _pageMinCacheLock.DoubleCheckLock(ref _pageMinCache, () => new ConcurrentDictionary<string, byte[]>());
            }
        }

        ConcurrentDictionary<string, byte[]> _zippedPageMinCache;
        object _zippedPageMinCacheLock = new object();
        protected ConcurrentDictionary<string, byte[]> ZippedMinCache
        {
            get
            {
                return _zippedPageMinCacheLock.DoubleCheckLock(ref _zippedPageMinCache, () => new ConcurrentDictionary<string, byte[]>());
            }
        }

        protected static internal Includes GetIncludesFromIncludeJs(string includeJs)
        {
            Includes returnValue = new Includes();
            string[] result = new string[] { };
            if (IncludesCache.ContainsKey(includeJs))
            {
                returnValue = IncludesCache[includeJs];
            }
            else if (File.Exists(includeJs))
            {
                lock (_includesCacheLock)
                {
                    dynamic include = includeJs.JsonFromJsLiteralFile("include").JsonToDynamic();
                    returnValue.Css = ((JArray)include["css"]).Select(v => (string)v).ToArray();
                    returnValue.Scripts = ((JArray)include["scripts"]).Select(v => (string)v).ToArray();
                    IncludesCache[includeJs] = returnValue;
                }
            }

            return returnValue;
        }
        #region IResponder Members

        /// <summary>
        /// If true, TryRespond will send 404 and close the connection
        /// if no content is found.  Otherwise, nothing will be done
        /// explicitly to close the connection or end the request.
        /// </summary>
        public bool EndResponse { get; set; }

        public override bool TryRespond(IHttpContext context)
        {
            return TryRespond(context, EndResponse);
        }

        public bool TryRespond(IHttpContext context, bool final = false)
        {
            try
            {
                if (CheckEtags(context))
                {
                    return true;
                }
                if (!IsInitialized)
                {
                    Initialize();
                }

                IRequest request = context.Request;
                IResponse response = context.Response;
                Session.Init(context);
                SecureSession.Init(context);

                bool handled = false;
                string path = request.Url.AbsolutePath;
                string commonPath = Path.Combine("/common", path.TruncateFront(1));

                byte[] content = new byte[] { };
                string appName = ResolveApplicationName(context);
                string[] checkedPaths = new string[] { };
                if (AppContentResponders.ContainsKey(appName))
                {
                    handled = AppContentResponders[appName].TryRespond(context, out checkedPaths);
                }

                if (!handled && !ShouldIgnore(path))
                {
                    bool exists;
                    exists = ServerRoot.FileExists(path, out string absoluteFileSystemPath);
                    if (!exists)
                    {
                        exists = ServerRoot.FileExists(commonPath, out absoluteFileSystemPath);
                    }

                    if (exists)
                    {
                        string ext = Path.GetExtension(absoluteFileSystemPath);
                        if (FileCachesByExtension.ContainsKey(ext))
                        {
                            FileCache cache = FileCachesByExtension[ext];
                            if (ShouldZip(request))
                            {
                                SetGzipContentEncodingHeader(response);
                                content = cache.GetZippedContent(absoluteFileSystemPath);
                            }
                            else
                            {
                                content = cache.GetContent(absoluteFileSystemPath);
                            }
                            handled = true;
                            SetLastModified(response, request.Url.ToString(), new FileInfo(absoluteFileSystemPath).LastWriteTime);
                        }
                    }

                    if (handled)
                    {
                        SetContentType(response, path);
                        SetEtag(response, path, content);
                        SendResponse(response, content);
                        OnResponded(context);
                    }
                    else
                    {
                        LogContentNotFound(path, appName, checkedPaths);
                        OnNotResponded(context);
                    }
                }

                if(!handled && final)
                {
                    SendResponse(response, "Not Found", 404);
                }
                return handled;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("An error occurred in {0}.{1}: {2}", ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                OnNotResponded(context);
                return false;
            }
        }

        private string ResolveApplicationName(IHttpContext context)
        {
            if (HostAppMappings.ContainsKey(context.Request.Url.Host))
            {
                return HostAppMappings[context.Request.Url.Authority].AppName;
            }
            return ApplicationNameResolver.ResolveApplicationName(context);
        }

        private void LogContentNotFound(string path, string appName, string[] checkedPaths)
        {
            // Not Sure what this is checking for???
            string[] svcNames = BamConf?.Server?.ServiceProxyResponder?.AppServices(appName).Select(s => s.ToLowerInvariant()).ToArray();
            List<string> serviceNames = new List<string>();
            if(svcNames != null)
            {
                serviceNames.AddRange(svcNames);
            }
            string[] splitPath = path.DelimitSplit("/", "\\");
            string firstPart = splitPath.Length > 0 ? splitPath[0] : path;
            // / -- ???

            if(!ShouldIgnore(path) && !serviceNames.Contains(firstPart.ToLowerInvariant()))
            {
                StringBuilder checkedPathString = new StringBuilder();
                checkedPaths.Each(p =>
                {
                    checkedPathString.AppendLine(p);
                });

                Logger.AddEntry(
                  "App[{0}]::Path='{1}'::Content Not Found\r\nChecked Paths\r\n{2}",
                  LogEventType.Warning,
                  appName,
                  path,
                  checkedPathString.ToString()
                );
            }
        }

        private static void ConditionallySetGzipHeader(IResponse response, bool shouldZip)
        {
            if (shouldZip)
            {
                SetGzipContentEncodingHeader(response);
            }
        }

        static HashSet<string> _cachedScripts = new HashSet<string>();
        static object _cachedScriptLock = new object();
        protected internal void SetScriptCache(
            string path, string script)
        {
            if (!_cachedScripts.Contains(path))
            {
                lock (_cachedScriptLock)
                {
                    if (!_cachedScripts.Contains(path))
                    {
                        _cachedScripts.Add(path);
                    }
                    else
                    {
                        return;
                    }
                }

                Logger.AddEntry("Minification of ({0}) STARTED", LogEventType.Information, path);
                script.MinifyAsync().ContinueWith(t =>
                {
                    MinifyResult compression = t.Result;
                    if (compression.Success)
                    {
                        Logger.AddEntry("Minification of ({0}) COMPLETED", LogEventType.Information, path);
                        byte[] minBytes = Encoding.UTF8.GetBytes(compression.MinScript);
                        SetMinCacheBytes(path, minBytes);

                        string fileName = Path.GetFileName(path);
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                        string pathWithoutFileName = path.TruncateFront(fileName.Length);
                        string minPath = Path.Combine(pathWithoutFileName, "{0}.min.js"._Format(fileNameWithoutExtension));
                        SetMinCacheBytes(minPath, minBytes);

                        Logger.AddEntry("GZipping the minified bytes of ({0}) STARTED", LogEventType.Information, path);
                        minBytes.GZipAsync().ContinueWith(g =>
                        {
                            Logger.AddEntry("GZipping the minified bytes of ({0}) COMPLETED", LogEventType.Information, path);
                            byte[] zippedMinBytes = g.Result;
                            SetZippedMinCacheBytes(minPath, zippedMinBytes);
                        });
                    }
                    else
                    {
                        string message = compression.Exception != null ? compression.Exception.Message : string.Empty;
                        string stack = string.Empty;
                        if (!string.IsNullOrEmpty(compression.Exception.StackTrace))
                        {
                            stack = compression.Exception.StackTrace;
                        }
                        Logger.AddEntry("Compression of ({0}) failed: {1}\r\n{2}", LogEventType.Warning, path, message, stack);
                    }
                });

                Task.Run(() =>
                {
                    byte[] scriptBytes = Encoding.UTF8.GetBytes(script);
                    SetCacheBytes(path, scriptBytes);
                    Logger.AddEntry("GZipping the bytes of ({0}) STARTED", LogEventType.Information, path);
                    scriptBytes.GZipAsync().ContinueWith(g =>
                    {
                        Logger.AddEntry("GZipping the minified bytes of ({0}) COMPLETED", LogEventType.Information, path);
                        SetZippedCacheBytes(path, g.Result);
                    });
                });
            }          
        }
        #endregion

        public override bool IsInitialized
        {
            get
            {
                return IsAppsInitialized;
            }
        }

        public override void Initialize()
        {
            if (!IsInitialized)
            {
                OnInitializing();
                InitializeCommonTemplateRenderer();
                InitializeAppResponders();

                OnInitialized();
            }
        }

        public event Action<ContentResponder> Initializing;

        protected void OnInitializing()
        {
            Initializing?.Invoke(this);
        }

        public event Action<ContentResponder> Initialized;
        protected void OnInitialized()
        {
            Initialized?.Invoke(this);
        }

        protected FileCache CreateCache(string fileExtension)
        {
            if (fileExtension.Equals(".js"))
            {
                return new JsFileCache(); 
            }
            else if (fileExtension.In(TextFileExtensions))
            {
                return new TextFileCache(fileExtension);
            }
            else
            {
                return new BinaryFileCache();
            }
        }

        protected bool ReadCache(IRequest request, string path, out byte[] content)
        {
            if (ShouldZip(request))
            {
                if (!ZippedMinCache.TryGetValue(path, out content))
                {
                    return ZippedCache.TryGetValue(path, out content);
                }
            }
            else
            {
                if (!MinCache.TryGetValue(path, out content))
                {
                    return Cache.TryGetValue(path, out content);
                }
            }
            return true;
        }
        static object cacheLock = new object();
        static object zippedCacheLock = new object();
        static object minCacheLock = new object();
        static object zippedMinCacheLock = new object();
        private void SetCacheBytes(string path, byte[] content)
        {
            Cache.AddOrUpdate(path, content, (s, b) => content);
        }
        private void SetZippedCacheBytes(string path, byte[] content)
        {
            ZippedCache.AddOrUpdate(path, content, (s, b) => content);
        }
        private void SetMinCacheBytes(string path, byte[] content)
        {
            MinCache.AddOrUpdate(path, content, (s, b) => content);
        }

        private void SetZippedMinCacheBytes(string path, byte[] content)
        {
            ZippedCache.AddOrUpdate(path, content, (s, b) => content);
        }
    }
}
