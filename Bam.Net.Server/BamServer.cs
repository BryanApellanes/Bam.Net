/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using Bam.Net.Html;
using Bam.Net.Logging;
using Bam.Net;
using Bam.Net.Incubation;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Bam.Net.Data;
using Bam.Net.Configuration;
using Bam.Net.ServiceProxy;
using Bam.Net.Web;
using System.IO;
using Bam.Net.UserAccounts;
using Bam.Net.Server;
using Bam.Net.Server.Listeners;
using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy.Secure;
using System.Reflection;
using Bam.Net.Server.Renderers;

namespace Bam.Net.Server
{
    /// <summary>
    /// The core BamServer
    /// </summary>
    public class BamServer : IInitialize<BamServer>
    {
        HashSet<IResponder> _responders;
        HttpServer _server;

        public BamServer(BamConf conf)
        {
            this._responders = new HashSet<IResponder>();
            this.Initialized += HandleInitialization;
            this.SetConf(conf);
            this.BindEventListeners(conf);
            this.EnableDao = true;
            this.EnableServiceProxy = true;

            SQLiteRegistrar.RegisterFallback();

            AppDomain.CurrentDomain.DomainUnload += (s, a) =>
            {
                this.Stop();
            };
        }
        
        /// <summary>
        /// The event that fires when server initialization begins
        /// </summary>
        public event Action<BamServer> Initializing;
        /// <summary>
        /// The event that fires when server initialization is complete
        /// </summary>
        public event Action<BamServer> Initialized;
        /// <summary>
        /// The event that fires when a schema is about to be initialized
        /// </summary>
        public event Action<BamServer, SchemaInitializer> SchemaInitializing;
        /// <summary>
        /// The event that fires when a schema is done initializing
        /// </summary>
        public event Action<BamServer, SchemaInitializer> SchemaInitialized;        
        /// <summary>
        /// The event that fires before beginning any schema initialization
        /// </summary>
        public event Action<BamServer> SchemasInitializing;
        /// <summary>
        /// The event that fires when all schemas have completed initialization
        /// </summary>
        public event Action<BamServer> SchemasInitialized;
        /// <summary>
        /// The event that fires before loading the server configuration
        /// </summary>
        public event Action<BamServer, BamConf> LoadingConf;
        /// <summary>
        /// the event that fires when loading the server configuration is complete
        /// </summary>
        public event Action<BamServer, BamConf> LoadedConf; 
        /// <summary>
        /// The event that fires before creating an application
        /// </summary>
        public event Action<BamServer, AppConf> CreatingApp;
        /// <summary>
        /// The event that fires when creating an application is complete
        /// </summary>
        public event Action<BamServer, AppConf> CreatedApp;
        /// <summary>
        /// The event that fires when a response has been sent
        /// </summary>
        public event Action<BamServer, IResponder, IResponse> Responding;
        /// <summary>
        /// The event that fires when a response has been sent
        /// </summary>
        public event Action<BamServer, IResponder, IRequest> Responded;
        /// <summary>
        /// The event that fires when a repsonse is not sent
        /// </summary>
        public event Action<BamServer, IRequest> NotResponded;
        /// <summary>
        /// The event that fires when a responder is added
        /// </summary>
        public event Action<BamServer, IResponder> ResponderAdded;
        /// <summary>
        /// The event that fires before setting the configuration
        /// </summary>
        public event Action<BamServer, BamConf> SettingConf;
        /// <summary>
        /// The event that fires when setting the configuration is complete
        /// </summary>
        public event Action<BamServer, BamConf> SettedConf;        
        /// <summary>
        /// The event that fires when the configuration is saved
        /// </summary>
        public event Action<BamServer, BamConf> SavedConf;
        /// <summary>
        /// The event that fires before starting the server
        /// </summary>
        public event Action<BamServer> Starting;
        /// <summary>
        /// The event that fires when the server has started
        /// </summary>
        public event Action<BamServer> Started;
        /// <summary>
        /// The event that fires before the server is stopped
        /// </summary>
        public event Action<BamServer> Stopping;
        /// <summary>
        /// The event that fires when the server has stopped
        /// </summary>
        public event Action<BamServer> Stopped;        

        private string ServerWorkspace { get { return Path.Combine("common", "workspace"); } }

        protected void BindEventListeners(BamConf conf)
        {
            BamServerEventListenerBinder binder = new BamServerEventListenerBinder(conf);
            binder.Bind();
        }

        TemplateInitializerBase _templateInitializer;
        object _templateInitializerLock = new object();
        // The initializer used to initialize templates 
        // after full server initialization
        public TemplateInitializerBase TemplateInitializer
        {
            get
            {
                return _templateInitializerLock.DoubleCheckLock(ref _templateInitializer, () => new DustTemplateInitializer(this));
            }
            set
            {
                _templateInitializer = value;
            }
        }

        WebBookInitializer _webBookInitializer;
        object _webBookInitializerLock = new object();
        public WebBookInitializer WebBookInitializer
        {
            get
            {
                return _webBookInitializerLock.DoubleCheckLock(ref _webBookInitializer, () => new WebBookInitializer(this));
            }
            set
            {
                _webBookInitializer = value;
            }
        }

        public bool IsInitialized
        {
            get;
            private set;
        }

        public SchemaInitializer[] SchemaInitializers // gets set by CopyProperties in SetConf
        {
            get;
            set;
        }

        protected void OnInitializing()
        {
            if (Initializing != null)
            {
                Initializing(this);
            }
        }

        protected void OnInitialized()
        {
            if (Initialized != null)
            {
                Initialized(this);
            }
        }

        protected void OnSchemasInitializing()
        {
            if (SchemasInitializing != null)
            {
                SchemasInitializing(this);
            }
        }

        protected void OnSchemasInitialized()
        {
            if (SchemasInitialized != null)
            {
                SchemasInitialized(this);
            }
        }

        protected void OnSchemaInitializing(SchemaInitializer initializer)
        {
            if (SchemaInitializing != null)
            {
                SchemaInitializing(this, initializer);
            }
        }
        protected void OnSchemaInitialized(SchemaInitializer initializer)
        {
            if (SchemaInitialized != null)
            {
                SchemaInitialized(this, initializer);
            }
        }

        public virtual void Initialize()
        {
            if (!this.IsInitialized)
            {
                OnInitializing();
                LoadConf();

                Subscribe(MainLogger);
                SubscribeResponders(MainLogger);

                EnsureDefaults();
                MainLogger.AddEntry("{0} initializing: \r\n{1}", this.GetType().Name, this.PropertiesToString());

                InitializeCommonSchemas();

                InitializeResponders();

                InitializeUserManagers();

                InitializeApps();

                ConfigureHttpServer();

                RegisterWorkspaceDaos();

                OnInitialized();
            }
            else
            {
                MainLogger.AddEntry("Initialize called but the {0} was already initialized", LogEventType.Warning, this.GetType().Name);
            }
        }

        public event Action<BamServer, AppConf> AppInitializing;
        protected void OnAppInitializing(AppConf conf)
        {
            if (AppInitializing != null)
            {
                AppInitializing(this, conf);
            }
        }
        public event Action<BamServer, AppConf> AppInitialized;
        protected void OnAppInitialized(AppConf conf)
        {
            if (AppInitialized != null)
            {
                AppInitialized(this, conf);
            }
        }
        protected internal void InitializeApps()
        {
            InitializeApps(_conf.AppConfigs);
        }

        private void InitializeApps(AppConf[] configs)
        {
            configs.Each(ac =>
            {
                OnAppInitializing(ac);
                if (!string.IsNullOrEmpty(ac.AppInitializer))
                {
                    Type appInitializer = null;
                    if (!string.IsNullOrEmpty(ac.AppInitializerAssemblyPath))
                    {
                        Assembly assembly = Assembly.LoadFrom(ac.AppInitializerAssemblyPath);
                        appInitializer = assembly.GetType(ac.AppInitializer);
                        if (appInitializer == null)
                        {
                            appInitializer = assembly.GetTypes().FirstOrDefault(t => t.AssemblyQualifiedName.Equals(ac.AppInitializer));
                        }

                        if (appInitializer == null)
                        {
                            Args.Throw<InvalidOperationException>("The specified AppInitializer type ({0}) wasn't found in the specified assembly ({1})", ac.AppInitializer, ac.AppInitializerAssemblyPath);
                        }
                    }
                    else
                    {
                        appInitializer = Type.GetType(ac.AppInitializer);
                        if (appInitializer == null)
                        {
                            Args.Throw<InvalidOperationException>("The specified AppInitializer type ({0}) wasn't found", ac.AppInitializer);
                        }
                    }

                    IAppInitializer initializer = appInitializer.Construct<IAppInitializer>();
                    initializer.Subscribe(MainLogger);
                    initializer.Initialize(ac);
                }
                OnAppInitialized(ac);
            });
        }
        /// <summary>
        /// Initialize server level schemas
        /// </summary>
        protected virtual void InitializeCommonSchemas()
        {
            OnSchemasInitializing();
            SchemaInitializers.Each(schemaInitializer =>
            {
                OnSchemaInitializing(schemaInitializer);
                Exception ex;
                if (!schemaInitializer.Initialize(MainLogger, out ex))
                {
                    MainLogger.AddEntry("An error occurred initializing schema ({0}): {1}", ex, schemaInitializer.SchemaName, ex.Message);
                }
                OnSchemaInitialized(schemaInitializer);
            });
            OnSchemasInitialized();
        }

        protected virtual void InitializeUserManagers()
        {
            ContentResponder.AppConfigs.Each(appConfig =>
            {
                try
                {
                    UserManager mgr = appConfig.UserManager.Create(MainLogger);
                    mgr.ApplicationNameProvider = new BamApplicationNameProvider(appConfig);
                    AddAppService<UserManager>(appConfig.Name, mgr);
                }
                catch (Exception ex)
                {
                    MainLogger.AddEntry("An error occurred initializing user manager for app ({0}): {1}", ex, appConfig.Name, ex.Message);
                }
            });
        }

        protected virtual void RegisterWorkspaceDaos()
        {
            DirectoryInfo workspaceDir = new DirectoryInfo(Workspace);
            DaoResponder.RegisterCommonDaoFromDirectory(workspaceDir);
        }

        protected virtual void InitializeResponders()
        {
            foreach (IResponder responder in _responders)
            {
                responder.Subscribe(MainLogger);
                responder.Initialize();
            }
        }

        /// <summary>
        /// Subscribe the specified logger to the events of the
        /// ContentResponder.  Will also subscribe to the DaoResponder
        /// if EnableDao is true and the ServiceProxyReponder if
        /// EnableServiceProxy is true.  Additionally, will subscribe to
        /// any other responders that have been added using AddResponder
        /// </summary>
        /// <param name="logger"></param>
        protected virtual void SubscribeResponders(ILogger logger)
        {
            foreach (IResponder responder in _responders)
            {
                responder.Subscribe(logger);
            }
        }

        List<ILogger> _subscribers = new List<ILogger>();
        object _subscriberLock = new object();
        public ILogger[] Subscribers
        {
            get
            {
                if (_subscribers == null)
                {
                    _subscribers = new List<ILogger>();
                }
                lock (_subscriberLock)
                {
                    return _subscribers.ToArray();
                }
            }
        }

        public bool IsSubscribed(ILogger logger)
        {
            lock (_subscriberLock)
            {
                return _subscribers.Contains(logger);
            }
        }

        /// <summary>
        /// Subscribe the specified logger to the 
        /// events of the current BamServer
        /// </summary>
        /// <param name="logger"></param>
        public void Subscribe(ILogger logger)
        {
            if (!IsSubscribed(logger))
            {
                lock (_subscriberLock)
                {
                    _subscribers.Add(logger);
                }
                string className = typeof(BamServer).Name;
                this.Initializing += (s) =>
                {
                    logger.AddEntry("{0}::Initializ(ING)", className);
                };
                this.Initialized += (s) =>
                {
                    logger.AddEntry("{0}::Initializ(ED)", className);
                };
                this.LoadingConf += (s, c) =>
                {
                    logger.AddEntry("{0}::Load(ING) configuration, current config: \r\n{1}", className, c.PropertiesToString());
                };
                this.LoadedConf += (s, c) =>
                {
                    logger.AddEntry("{0}::Load(ED) configuration, current config: \r\n{1}", className, c.PropertiesToString());
                };
                this.SettingConf += (s, c) =>
                {
                    logger.AddEntry("{0}::Sett(ING) configuration, current config: \r\n{1}", className, c.PropertiesToString());
                };
                this.SettedConf += (s, c) =>
                {
                    logger.AddEntry("{0}::Sett(ED) configuration, current config: \r\n{1}", className, c.PropertiesToString());
                };
                this.SchemasInitializing += (s) =>
                {
                    logger.AddEntry("{0}::Initializ(ING) schemas", className);
                };
                this.SchemasInitialized += (s) =>
                {
                    logger.AddEntry("{0}::Initializ(ED) schemas", className);
                };
                this.Starting += (s) =>
                {
                    logger.AddEntry("{0}::Start(ING)", className);
                };

                this.Started += (s) =>
                {
                    logger.AddEntry("{0}::Start(ED)", className);
                };

                this.Stopping += (s) =>
                {
                    logger.AddEntry("{0}::stopping", className);
                };

                this.Stopped += (s) =>
                {
                    logger.AddEntry("{0}::stopped", className);
                };
            }
        }

        ILogger _logger;
        object _loggerLock = new object();
        public ILogger MainLogger
        {
            get
            {
                return _loggerLock.DoubleCheckLock(ref _logger, () =>
                {
                    Log.Start();
                    return Log.Default;
                });
            }
            set
            {
                if (_logger != null)
                {
                    _logger.StopLoggingThread();
                }

                _logger = value;
                _logger.RestartLoggingThread();
                if (IsRunning)
                {
                    Restart();
                }
            }
        }

        public ILogger[] AdditionalLoggers { get; set; }

        public HostPrefix[] GetHostPrefixes()
        {
            BamConf serverConfig = GetCurrentConf(false);
            List<HostPrefix> results = new List<HostPrefix>();
            serverConfig.AppConfigs.Each(appConf =>
            {
                results.AddRange(appConf.Bindings);
            });

            return results.ToArray();
        }

        // config values here to ensure proper sync
        public DaoConf[] DaoConfigs { get; set; }
        public ProxyAlias[] ProxyAliases { get; set; }
        public bool GenerateDao { get; set; }
        public bool UseCache { get; set; }
        public bool InitializeTemplates { get; set; }
        public bool InitializeWebBooks { get; set; }
        public string DaoSearchPattern { get; set; }
        public string ServiceSearchPattern { get; set; }
        public string MainLoggerName { get; set; }
        public string InitializeFileSystemFrom { get; set; }
        public string[] ServerEventListenerSearchPaths { get; set; }
        public string ServerEventListenerAssemblySearchPattern { get; set; }
        // -end config values

        int _maxThreads;
        public int MaxThreads
        {
            get
            {
                return _maxThreads;
            }
            set
            {
                _maxThreads = value;
            }
        }

        string _contentRoot;
        public string ContentRoot
        {
            get
            {
                return _contentRoot;
            }
            set
            {
                _contentRoot = new Fs(value).Root;
                ContentResponder.BamConf = GetCurrentConf();
            }
        }

        protected void OnLoadingConf()
        {
            if (LoadingConf != null)
            {
                LoadingConf(this, GetCurrentConf());
            }
        }

        protected void OnLoadedConf(BamConf conf)
        {
            if (LoadedConf != null)
            {
                LoadedConf(this, conf);
            }
        }

        /// <summary>
        /// Loads the server configuration from either a json file, yaml file
        /// or the default config depending on which is found first in that 
        /// order.
        /// </summary>
        public BamConf LoadConf()
        {
            OnLoadingConf();
            BamConf conf = BamConf.Load(ContentRoot);
            SetConf(conf);
            OnLoadedConf(conf);
            return conf;
        }

        protected void OnCreatingApp(AppConf conf)
        {
            if (CreatingApp != null)
            {
                CreatingApp(this, conf);
            }
        }

        protected void OnCreatedApp(AppConf conf)
        {
            if (CreatedApp != null)
            {
                CreatedApp(this, conf);
            }
        }

        public AppContentResponder CreateApp(string appName, string defaultLayout = null, int port = 8080, bool ssl = false)
        {
            AppConf conf = new AppConf(appName, port, ssl);
            if (!string.IsNullOrEmpty(defaultLayout))
            {
                conf.DefaultLayout = defaultLayout;
            }
            OnCreatingApp(conf);

            AppContentResponder responder = new AppContentResponder(ContentResponder, conf);
            responder.Initialize();

            OnCreatedApp(conf);
            return responder;
        }

        protected void OnSettingConf(BamConf conf)
        {
            if (SettingConf != null)
            {
                SettingConf(this, conf);
            }
        }

        protected void OnSettedConf(BamConf conf)
        {
            if (SettedConf != null)
            {
                SettedConf(this, conf);
            }
        }

        public void SetConf(BamConf conf)
        {
            OnSettingConf(conf);
            
            Type loggerType;
            this.MainLogger = Log.Default = conf.GetMainLogger(out loggerType);
            this.MainLogger.RestartLoggingThread();
            if (!loggerType.Name.Equals(conf.MainLoggerName))
            {
                MainLogger.AddEntry("Configured MainLogger was ({0}) but the Logger found was ({1})", LogEventType.Warning, conf.MainLoggerName, loggerType.Name);
            }
            this.TryAddAdditionalLoggers(conf);
            conf.Server = this;

            DefaultConfiguration.CopyProperties(conf, this);
            SetWorkspace();

            OnSettedConf(conf);
        }
        
        protected void OnSavedConf(BamConf conf)
        {
            if (SavedConf != null)
            {
                SavedConf(this, conf);
            }
        }

        /// <summary>
        /// Saves the current configuration if the config 
        /// file doesn't currently exist
        /// </summary>
        /// <param name="format">The format to save the configuration in</param>
        /// <param name="overwrite">If true overwrite the existing cofig file</param>
        /// <returns>The BamConf</returns>
        public BamConf SaveConf(bool overwrite = false, ConfFormat format = ConfFormat.Json)
        {
            BamConf conf = GetCurrentConf();
            conf.Save(ContentRoot, overwrite, format);
            OnSavedConf(conf);
            return conf;
        }

        ContentResponder _contentResponder;
        /// <summary>
        /// The primary responder for all content files
        /// </summary>
        public ContentResponder ContentResponder
        {
            get
            {
                if (_contentResponder == null)
                {
                    SetContentResponder();
                }
                return _contentResponder;
            }
        }


        DaoResponder _daoResponder;
        public DaoResponder DaoResponder
        {
            get
            {
                if (_daoResponder == null)
                {
                    SetDaoResponder();
                }
                return _daoResponder;
            }
        }

        ServiceProxyResponder _serviceProxyResponder;
        public ServiceProxyResponder ServiceProxyResponder
        {
            get
            {
                if (_serviceProxyResponder == null)
                {
                    SetServiceProxyResponder();
                }
                return _serviceProxyResponder;
            }
        }

        public void SubscribeToResponded<T>(ResponderEventHandler subscriber) where T : class, IResponder
        {
            Responders.Each(r =>
            {
                T responder = r as T;
                if (responder != null)
                {
                    responder.Responded += subscriber;
                }
            });
        }

        public void SubscribeToNotResponded<T>(ResponderEventHandler subscriber) where T : class, IResponder
        {
            Responders.Each(r =>
            {
                T responder = r as T;
                if (responder != null)
                {
                    responder.NotResponded += subscriber;
                }
            });
        }

        public void SubscribeToResponded(ResponderEventHandler subscriber)
        {
            Responders.Each(r =>
            {
                r.Responded += subscriber;
            });
        }

        public void SubscribeToNotResponded(ResponderEventHandler subscriber)
        {
            Responders.Each(r =>
            {
                r.NotResponded += subscriber;
            });
        }
        
        public void Start()
        {
            SetWorkspace();
            ListenForDaoGenServices();

            Initialize();

            OnStarting();
            _server.Start();
            IsRunning = true;
            OnStarted();
        }

        public void Stop()
        {
            if (IsInitialized)
            {
                SaveConf();

                OnStopping();
                _server.Stop();
                IsRunning = false;
                OnStopped();
            }
        }

        public void Restart()
        {
            Stop();
            this.IsInitialized = false;
            Start();
        }

        public static BamServer Serve(string contentRoot)
        {
            BamConf conf = BamConf.Load(contentRoot);
            BamServer server = new BamServer(conf);
            server.Start();
            return server;
        }

        public Incubator CommonServiceProvider
        {
            get
            {
                return ServiceProxyResponder.CommonServiceProvider;
            }
        }

        public Dictionary<string, Incubator> AppServiceProviders
        {
            get
            {
                return ServiceProxyResponder.AppServiceProviders;
            }
        }

        public Dictionary<string, AppContentResponder> AppContentResponders
        {
            get
            {
                return ContentResponder.AppContentResponders;
            }
        }

        string _workspace;
        public string Workspace
        {
            get
            {
                return Fs.CleanPath(_workspace);
            }
        }

        public void AddCommonDaoFromDirectory(DirectoryInfo daoDir)
        {
            DaoResponder.RegisterCommonDaoFromDirectory(daoDir);
        }

        public void AddAppDaoFromDirectory(string appName, DirectoryInfo daoDir)
        {
            DaoResponder.RegisterAppDaoFromDirectory(appName, daoDir);
        }

        public void AddCommonService<T>()
        {
            ServiceProxyResponder.AddCommonService<T>((T)typeof(T).Construct());
        }

        public void AddCommonService<T>(T instance)
        {
            ServiceProxyResponder.AddCommonService<T>(instance);
        }

        public T GetAppService<T>(string appName)
        {
            if (ServiceProxyResponder.AppServiceProviders.ContainsKey(appName))
            {
                return ServiceProxyResponder.AppServiceProviders[appName].Get<T>();
            }
            return default(T);
        }

        public void AddAppService<T>(string appName)
        {
            ServiceProxyResponder.AddAppService<T>(appName, (T)typeof(T).Construct());
        }

        public void AddAppService<T>(string appName, T instance)
        {
            ServiceProxyResponder.AddAppService<T>(appName, instance);
        }

        public void AddAppService<T>(string appName, Func<T> instanciator)
        {
            ServiceProxyResponder.AddAppService<T>(appName, instanciator);
        }

        public void AddAppService(string appName, Type type, Func<object> instanciator)
        {
            ServiceProxyResponder.AddAppService(appName, type, instanciator);
        }

        /// <summary>
        /// Add or update the app service using the specified instanciator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appName"></param>
        /// <param name="instanciator"></param>
        public void AddAppService<T>(string appName, Func<Type, T> instanciator)
        {
            ServiceProxyResponder.AddAppService<T>(appName, instanciator);
        }

        public void AddLogger(ILogger logger)
        {
            MultiTargetLogger mtl = new MultiTargetLogger();
            if (MainLogger != null)
            {
                if (MainLogger.GetType() == typeof(MultiTargetLogger))
                {
                    mtl = (MultiTargetLogger)MainLogger;
                }
                else
                {
                    mtl.AddLogger(MainLogger);
                }
            }

            mtl.AddLogger(logger);
            MainLogger = mtl;
        }

        /// <summary>
        /// Add an IResponder implementation to this
        /// request handler
        /// </summary>
        /// <param name="responder"></param>
        public void AddResponder(IResponder responder)
        {
            this._responders.Add(responder);
            if (ResponderAdded != null)
            {
                ResponderAdded(this, responder);
            }
        }

        public void RemoveResponder(IResponder responder)
        {
            if (_responders.Contains(responder))
            {
                _responders.Remove(responder);
            }
        }

        public IResponder[] Responders
        {
            get
            {
                return _responders.ToArray();
            }
        }

        Action<IHttpContext> _responderNotFoundHandler;
        object _responderNotFoundHandlerLock = new object();
        /// <summary>
        /// Get or set the default handler used when no appropriate
        /// responder is found for a given request.  This is the 
        /// Action responsible for responding with a 404 status code
        /// and supplying any additional information to the client.
        /// </summary>
        public Action<IHttpContext> ResponderNotFoundHandler
        {
            get
            {
                return _responderNotFoundHandlerLock.DoubleCheckLock(ref _responderNotFoundHandler, () => HandleResponderNotFound);
            }
            set
            {
                _responderNotFoundHandler = value;
            }
        }

        Action<IHttpContext, Exception> _exceptionHandler;
        object _exceptionHandlerLock = new object();
        /// <summary>
        /// Get or set the default exception handler.  This is the
        /// Action responsible for responding with a 500 status code
        /// and supplying any additional information to the client
        /// pertaining to exceptions that may occur on the server.
        /// </summary>
        public Action<IHttpContext, Exception> ExceptionHandler
        {
            get
            {
                return _exceptionHandlerLock.DoubleCheckLock(ref _exceptionHandler, () => HandleException);
            }
            set
            {
                _exceptionHandler = value;
            }
        }

        public void HandleRequest(IHttpContext context)
        {
            IRequest request = context.Request;
            IResponse response = context.Response;
            ResponderList responder = new ResponderList(_conf, _responders);
            try
            {
                if (!responder.Respond(context))
                {
                    if (NotResponded != null)
                    {
                        NotResponded(this, request);
                    }
                    ResponderNotFoundHandler(context);
                }
                else
                {
                    TriggerResponding(response, responder);
                    Respond(response);
                    TriggerResponded(request, responder);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(context, ex);                
            }
        }

        public ITemplateRenderer GetAppTemplateRenderer(string appName)
        {
            Dictionary<string, AppContentResponder> container = ContentResponder.AppContentResponders;
            if (container.ContainsKey(appName))
            {
                return container[appName].AppTemplateRenderer;
            }
            else
            {
                MainLogger.AddEntry("Unable to retrieve AppDustRenderer for the specified app name: {0}", LogEventType.Warning, appName);
                return null;
            }
        }

        private static void Respond(IResponse response)
        {
            response.StatusCode = (int)HttpStatusCode.OK;
            response.OutputStream.Flush();
            response.OutputStream.Close();
        }

        private void TriggerResponded(IRequest request, ResponderList responder)
        {
            if (Responded != null)
            {
                Responded(this, responder.HandlingResponder, request);
            }
        }

        private void TriggerResponding(IResponse response, ResponderList responder)
        {
            if (Responding != null)
            {
                Responding(this, responder.HandlingResponder, response);
            }
        }

        BamConf _conf;
        object _confLock = new object();
        /// <summary>
        /// Get a BamConf instance which represents the current
        /// state of the BamServer
        /// </summary>
        /// <returns></returns>
        internal protected BamConf GetCurrentConf(bool reload = true)
        {
            lock (_confLock)
            {
                if (reload || _conf == null)
                {
                    BamConf conf = new BamConf();
                    DefaultConfiguration.CopyProperties(this, conf);
                    conf.Server = this;
                    _conf = conf;
                }
            }
            return _conf;
        }

        protected void TryAddAdditionalLoggers(BamConf conf)
        {
            Type[] loggerTypes;
            ILogger[] loggers = new ILogger[] { };
            try
            {
                loggers = conf.GetAdditionalLoggers(out loggerTypes);
            }
            catch (Exception ex)
            {
                MainLogger.AddEntry("An error occurred getting additional loggers: {0}", ex, ex.Message);
            }

            loggers.Each(logger =>
            {
                try
                {
                    AddLogger(logger);
                }
                catch (Exception ex)
                {
                    MainLogger.AddEntry("An error occurred trying to add a logger: {0}", ex, ex.Message);
                }
            });

            AdditionalLoggers = loggers;
        }

        protected HttpServer HttpServer
        {
            get { return _server; }
        }

        bool _enableDao;
        /// <summary>
        /// If true will cause the initialization of the 
        /// DaoResponder which will process all *.db.js
        /// and *.db.json files.  See http://breviteedocs.wordpress.com/dao/
        /// for information about the expected format 
        /// of a *.db.js file.  The format of *db.json 
        /// would be the json equivalent of the referenced
        /// database object (see link).  See
        /// Bam.Net.Data.Schema.DataTypes for valid
        /// data types.
        /// </summary>
        protected bool EnableDao
        {
            get
            {
                return _enableDao;
            }
            set
            {
                _enableDao = value;
                RemoveResponder(_daoResponder);
                if (_enableDao)
                {
                    SetDaoResponder();
                }
            }
        }

        bool _enableServiceProxy;
        /// <summary>
        /// If true will cause the initialization of the
        /// ServiceProxyResponder which will register
        /// all classes addorned with the Proxy attribute
        /// as service proxy executors
        /// </summary>
        protected bool EnableServiceProxy
        {
            get
            {
                return _enableServiceProxy;
            }
            set
            {
                _enableServiceProxy = value;
                if (_enableServiceProxy)
                {
                    SetServiceProxyResponder();
                }
                else
                {
                    RemoveResponder(_serviceProxyResponder);
                }
            }
        }

        protected void SetDaoResponder()
        {
            _daoResponder = new DaoResponder(GetCurrentConf(true), MainLogger);
            AddResponder(_daoResponder);
        }

        protected void SetServiceProxyResponder()
        {
            _serviceProxyResponder = new ServiceProxyResponder(GetCurrentConf(true), MainLogger);
            _serviceProxyResponder.ContentResponder = ContentResponder;
            AddResponder(_serviceProxyResponder);
        }

        public event EventHandler FileUploading;
        public event EventHandler FileUploaded;

        protected void SetContentResponder()
        {
            _contentResponder = new ContentResponder(GetCurrentConf(true), MainLogger);
            _contentResponder.FileUploading += (o, a) => FileUploading?.Invoke(o, a);
            _contentResponder.FileUploaded += (o, a) => FileUploaded?.Invoke(o, a);
            AddResponder(_contentResponder);
        }

        protected void OnStopping()
        {
            if (Stopping != null)
            {
                Stopping(this);
            }
        }

        protected void OnStopped()
        {
            if (Stopped != null)
            {
                Stopped(this);
            }
        }
        protected void OnStarting()
        {
            if (Starting != null)
            {
                Starting(this);
            }
        }

        protected void OnStarted()
        {
            if (Started != null)
            {
                Started(this);
            }
        }

        protected internal bool IsRunning
        {
            get;
            private set;
        }
        protected void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            HandleRequest(new HttpContextWrapper(new RequestWrapper(request), new ResponseWrapper(response)));
        }

        private void HandleResponderNotFound(IHttpContext context)
        {
            IResponse response = context.Response;
            IRequest request = context.Request;

            string path = request.Url.ToString();
            string messageFormat = "No responder was found for the path: {0}";
            string description = "Responder not found";

            using (StreamWriter sw = new StreamWriter(response.OutputStream))
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.StatusDescription = description;
                sw.WriteLine("<!DOCTYPE html>");
                Tag html = new Tag("html");
                html.Child(new Tag("body")
                    .Child(new Tag("h1").Text(description))
                    .Child(new Tag("p").Text(string.Format(messageFormat, path)))
                );
                sw.WriteLine(html.ToHtmlString());
                sw.Flush();
                sw.Close();
            }

            MainLogger.AddEntry(messageFormat, LogEventType.Warning, path);
        }

        private void HandleException(IHttpContext context, Exception ex)
        {
            IResponse response = context.Response;
            IRequest request = context.Request;
            if(response.OutputStream != null)
            {
                using (StreamWriter sw = new StreamWriter(response.OutputStream))
                {
                    string description = "({0})"._Format(ex.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusDescription = description;
                    sw.WriteLine("<!DOCTYPE html>");
                    Tag html = new Tag("html");
                    html.Child(new Tag("body")
                        .Child(new Tag("h1").Text("Internal Server Exception"))
                        .Child(new Tag("p").Text(description))
                    );
                    sw.WriteLine(html.ToHtmlString());
                    sw.Flush();
                    sw.Close();
                }

            }
            MainLogger.AddEntry("An error occurred handling the request: ({0})\r\n*** Request Details ***\r\n{1}",
                    ex,
                    ex.Message,
                    request.PropertiesToString());
        }

        private void HandleInitialization(BamServer server)
        {
            if (server.InitializeTemplates)
            {
                TemplateInitializer.Subscribe(MainLogger);
                TemplateInitializer.Initialize();
            }

            if (server.InitializeWebBooks)
            {
                WebBookInitializer.Subscribe(MainLogger);
                WebBookInitializer.Initialize();
            }

            this.IsInitialized = true;
        }

        private void ConfigureHttpServer()
        {
            int maxThreads = this.MaxThreads;
            if (maxThreads < 50)
            {
                maxThreads = 50;
            }

            _server = new HttpServer(maxThreads, MainLogger);
            _server.HostPrefixes = GetHostPrefixes();
            _server.ProcessRequest += ProcessRequest;
        }

        private void EnsureDefaults()
        {
            if (this.MaxThreads <= 0)
            {
                this.MaxThreads = 50;
                MainLogger.AddEntry("Set MaxThreads to default value {0}", this.MaxThreads);
            }
        }

        private void ListenForDaoGenServices()
        {
            ServiceProxyResponder.CommonServiceAdded += (t, o) =>
            {
                IGeneratesDaoAssembly daoGen = o as IGeneratesDaoAssembly;
                if (daoGen != null)
                {
                    daoGen.GenerateDaoAssemblySucceeded += (io, a) =>
                    {
                        GenerateDaoAssemblyEventArgs args = (GenerateDaoAssemblyEventArgs)a;
                        DaoResponder.RegisterCommonDaoFromDirectory(args.GeneratedAssemblyInfo.GetAssembly().GetFileInfo().Directory);
                    };
                }
            };
        }

        private void SetWorkspace()
        {
            _workspace = Path.Combine(ContentRoot, ServerWorkspace);
            if (!Directory.Exists(_workspace))
            {
                Directory.CreateDirectory(_workspace);
            }
            Directory.SetCurrentDirectory(_workspace);
        }
    }

}