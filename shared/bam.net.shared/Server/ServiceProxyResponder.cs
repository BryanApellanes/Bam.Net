/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using Bam.Net.Yaml;
using System.IO;
using System.Reflection;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Web;
using Bam.Net.Server.Renderers;
using Bam.Net.Presentation.Html;
using Bam.Net.Configuration;

using System.Threading.Tasks;
using Bam.Net.Presentation;
using Bam.Net.Server.Meta;
using Bam.Net.Services;

namespace Bam.Net.Server
{
    /// <summary>
    /// Responder responsible for generating service proxies
    /// and responding to service proxy requests
    /// </summary>
    public partial class ServiceProxyResponder : Responder, IInitialize<ServiceProxyResponder>
    {
        public const string ServiceProxyRelativePath = "~/services";
        const string MethodFormPrefixFormat = "/{0}/MethodForm";

        public ServiceProxyResponder(BamConf conf, ILogger logger)
            : base(conf, logger)
        {
            _commonServiceProvider = new Incubator();
            _appServiceProviders = new Dictionary<string, Incubator>();
            _appSecureChannels = new Dictionary<string, SecureChannel>();
            _commonSecureChannel = new SecureChannel();
            _clientProxyGenerators = new Dictionary<string, IClientProxyGenerator>();
            RendererFactory = new RendererFactory(logger);
            ExecutionRequestResolver = new ExecutionRequestResolver();

            AddCommonService(_commonSecureChannel);
            AddClientProxyGenerator(new CsClientProxyGenerator(), "proxies.cs", "csproxies", "csharpproxies");
            AddClientProxyGenerator(new JsClientProxyGenerator(), "proxies.js", "jsproxies", "javascriptproxies");

            CommonServiceAdded += (type, obj) =>
            {
                CommonSecureChannel.ServiceProvider.Set(type, obj);
            };
            CommonServiceRemoved += (type) =>
            {
                CommonSecureChannel.ServiceProvider.Remove(type);
            };
            AppServiceAdded += (appName, type, instance) =>
            {
                if (!AppSecureChannels.ContainsKey(appName))
                {
                    SecureChannel channel = new SecureChannel();
                    channel.ServiceProvider.CopyFrom(CommonServiceProvider, true);
                    AppSecureChannels.Add(appName, channel);
                }

                AppSecureChannels[appName].ServiceProvider.Set(type, instance, false);
            };
        }

        public IExecutionRequestResolver ExecutionRequestResolver
        {
            get;
            set;
        }

        public ContentResponder ContentResponder
        {
            get;
            set;
        }

        Incubator _commonServiceProvider;
        /// <summary>
        /// Services available to all applications
        /// </summary>
        public Incubator CommonServiceProvider
        {
            get
            {
                return _commonServiceProvider;
            }
        }

        SecureChannel _commonSecureChannel;
        public SecureChannel CommonSecureChannel
        {
            get
            {
                return _commonSecureChannel;
            }
        }

        Dictionary<string, Incubator> _appServiceProviders;
        /// <summary>
        /// Incubators keyed by application name
        /// </summary>
        public Dictionary<string, Incubator> AppServiceProviders
        {
            get
            {
                return _appServiceProviders;
            }
        }

        Dictionary<string, SecureChannel> _appSecureChannels;
        public Dictionary<string, SecureChannel> AppSecureChannels
        {
            get
            {
                return _appSecureChannels;
            }
        }

        public void SetCommonWebServices(WebServiceRegistry webServiceRegistry)
        {
            webServiceRegistry.Set(typeof(SecureChannel), CommonSecureChannel);
            _commonServiceProvider = webServiceRegistry;
            CommonSecureChannel.ServiceProvider = webServiceRegistry;
        }

        public void SetApplicationWebServices(string applicationName, WebServiceRegistry webServiceRegistry)
        {
            webServiceRegistry.Set(typeof(SecureChannel), _appSecureChannels[applicationName]);
            _appServiceProviders[applicationName] = webServiceRegistry;
            _appSecureChannels[applicationName].ServiceProvider = webServiceRegistry;
        }

        public void AddClientProxyGenerator<T>(T proxyGenerator, params string[] fileNames) where T : IClientProxyGenerator
        {
            foreach(string fileName in fileNames)
            {
                AddClientProxyGenerator<T>(fileName, proxyGenerator);
            }
        }

        public void AddClientProxyGenerator<T>(string fileNameKey, T proxyGenerator) where T: IClientProxyGenerator
        {
            _clientProxyGenerators.Set(fileNameKey, proxyGenerator);
        }

        /// <summary>
        /// Add the specified instance to the specified appName
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appName"></param>
        /// <param name="instance"></param>
        public void AddAppService<T>(string appName, T instance)
        {
            AddAppService(appName, typeof(T), instance);
        }

        /// <summary>
        /// Add the specified instance as a service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void AddCommonService<T>(T instance)
        {
            AddCommonService(typeof(T), instance);
        }

        public void AddAppService(string appName, object instance)
        {
            AddAppService(appName, instance.GetType(), instance);
        }

        public event Action<string, Type, object> AppServiceAdded;
        protected void OnAppServiceAdded(string appName, Type type, object instance)
        {
            AppServiceAdded?.Invoke(appName, type, instance);
        }
        public void AddAppService<T>(string appName, Func<T> instanciator, bool throwIfSet = false)
        {
            if (_appServiceProviders.ContainsKey(appName))
            {
                _appServiceProviders[appName].Set(instanciator, throwIfSet);
                OnAppServiceAdded(appName, typeof(T), instanciator);
            }
        }

        public void AddAppService(string appName, Type type, Func<object> instanciator, bool throwIfSet = false)
        {
            if (_appServiceProviders.ContainsKey(appName))
            {
                _appServiceProviders[appName].Set(type, instanciator, throwIfSet);
                OnAppServiceAdded(appName, type, instanciator);
            }
        }

        public void AddAppService<T>(string appName, Func<Type, T> instanciator, bool throwIfSet = false)
        {
            if (_appServiceProviders.ContainsKey(appName))
            {
                _appServiceProviders[appName].Set(instanciator, throwIfSet);
                OnAppServiceAdded(appName, typeof(T), instanciator);
            }
        }

        public void AddAppService(string appName, Type type, object instance)
        {
            if (_appServiceProviders.ContainsKey(appName))
            {
                _appServiceProviders[appName].Set(type, instance);
                OnAppServiceAdded(appName, type, instance);
            }
        }

        public void AddAppServices(string appName, Incubator incubator)
        {
            if (_appServiceProviders.ContainsKey(appName))
            {
                _appServiceProviders[appName].CombineWith(incubator);
                Parallel.ForEach(incubator.ClassNameTypes, type =>
                {
                    Task.Run(() => OnAppServiceAdded(appName, type, incubator[type]));
                });
            }
        }

        public void ClearAppServices()
        {
            _appServiceProviders.Clear();
        }

        public void ClearCommonServices()
        {
            _commonServiceProvider = new Incubator();
            AddCommonService(_commonSecureChannel);
        }

        public void ClearServices()
        {
            ClearAppServices();
            ClearCommonServices();
        }

        /// <summary>
        /// Add the specified instance as an executor
        /// </summary>
        public void AddCommonService(object instance)
        {
            AddCommonService(instance.GetType(), instance);
        }

        public event Action<Type, object> CommonServiceAdded;
        protected void OnCommonServiceAdded(Type type, object instance)
        {
            CommonServiceAdded?.Invoke(type, instance);
        }

        public void AddCommonService(Type type, Func<object> instanciator)
        {
            _commonServiceProvider.Set(type, instanciator);
            OnCommonServiceAdded(type, instanciator);
        }
        /// <summary>
        /// Add the specified instance as an executor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        public void AddCommonService(Type type, object instance)
        {
            _commonServiceProvider.Set(type, instance);
            OnCommonServiceAdded(type, instance);
        }

        public event Action<Type> CommonServiceRemoved;
        protected void OnCommonServiceRemoved(Type type)
        {
            CommonServiceRemoved?.Invoke(type);
        }

        /// <summary>
        /// Remove the executor of the specified generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveCommonService<T>()
        {
            _commonServiceProvider.Remove<T>();
            OnCommonServiceRemoved(typeof(T));
        }

        /// <summary>
        /// Remove the executor of the specified type
        /// </summary>
        /// <param name="type"></param>
        public void RemoveCommonService(Type type)
        {
            _commonServiceProvider.Remove(type);
            OnCommonServiceRemoved(type);
        }

        /// <summary>
        /// Remove the executor with the specified className
        /// </summary>
        /// <param name="className"></param>
        public void RemoveCommonService(string className)
        {
            _commonServiceProvider.Remove(className, out Type type);
            OnCommonServiceRemoved(type);
        }

        /// <summary>
        /// Returns true if the specified generic type has 
        /// been added as an executor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Contains<T>()
        {
            return _commonServiceProvider.Contains<T>();
        }

        /// <summary>
        /// Returns true if the specified type has been 
        /// added as an executor
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Contains(Type type)
        {
            return _commonServiceProvider.Contains(type);
        }

        /// <summary>
        /// List of service class names
        /// </summary>
        public string[] CommonServices
        {
            get
            {
                return _commonServiceProvider.ClassNames;
            }
        }

        public string[] AppServices(string appName)
        {
            List<string> services = new List<string>();
            if (_appServiceProviders.ContainsKey(appName))
            {
                services.AddRange(_appServiceProviders[appName].ClassNames);
            }
            services.AddRange(CommonServices);
            return services.ToArray();
        }

        /// <summary>
        /// Always returns true for a ServiceProxyResponder as
        /// this responder is last in line.
        /// </summary>
        /// <param name="context"></param>
        ///// <returns></returns>
        public override bool MayRespond(IHttpContext context)
        {
            return true;
        }
        
        public void RegisterProxiedClasses()
        {
            string serviceProxyRelativePath = ServiceProxyRelativePath;
            List<string> registered = new List<string>();
            ForEachProxiedClass((type) =>
            {
                this.AddCommonService(type, type.Construct());
            });

            BamConf.AppConfigs.Each(appConf =>
            {
                string name = appConf.Name.ToLowerInvariant();
                Incubator serviceProvider = new Incubator();

                AppServiceProviders[name] = new Incubator();

                DirectoryInfo appServicesDir = new DirectoryInfo(appConf.AppRoot.GetAbsolutePath(serviceProxyRelativePath));
                if (appServicesDir.Exists)
                {
                    Action<Type> serviceAdder = (type) =>
                    {
                        if (type.TryConstruct(out object instance, ex => Logger.AddEntry("RegisterProxiedClasses: Unable to construct instance of type {0}: {1}", ex, type.Name, ex.Message)))
                        {
                            SubscribeIfLoggable(instance);
                            AddAppService(appConf.Name, instance);
                        }
                    };
                    ForEachProxiedClass(appServicesDir, serviceAdder);
                    ForEachProxiedClass(appConf, appServicesDir, serviceAdder);
                }
                else
                {
                    Logger.AddEntry("{0} directory not found", LogEventType.Warning, appServicesDir.FullName);
                }
                AddConfiguredServiceProxyTypes(appConf);
            });
        }

        private void AddConfiguredServiceProxyTypes(AppConf appConf)
        {
            appConf.ServiceTypeNames.Each(typeName =>
            {
                Type type = Type.GetType(typeName);
                if (type != null)
                {
                    if (type.TryConstruct(out object instance, ex => Logger.AddEntry("AddConfiguredServiceProxyTypes: Unable to construct instance of type {0}: {1}", ex, type.Name, ex.Message)))
                    {
                        SubscribeIfLoggable(instance);
                        AddAppService(appConf.Name, instance);
                    }
                }
            });
        }

        private void SubscribeIfLoggable(object instance)
        {
            if (instance is Loggable loggable)
            {
                loggable.Subscribe(Logger);
            }
        }

        private void ForEachProxiedClass(Action<Type> doForEachProxiedType)
        {
            string serviceProxyRelativePath = ServiceProxyRelativePath;
            DirectoryInfo serviceDir = new DirectoryInfo(ServerRoot.GetAbsolutePath(serviceProxyRelativePath));
            if (serviceDir.Exists)
            {
                ForEachProxiedClass(serviceDir, doForEachProxiedType);
            }
            else
            {
                Logger.AddEntry("{0}:{1} directory was not found", LogEventType.Warning, this.Name, serviceDir.FullName);
            }
        }

        private void ForEachProxiedClass(AppConf appConf, DirectoryInfo serviceDir, Action<Type> doForEachProxiedType)
        {
            foreach (string searchPattern in appConf.ServiceSearchPattern)
            {
                ForEachProxiedClass(searchPattern, serviceDir, doForEachProxiedType);
            }
        }

        private void ForEachProxiedClass(DirectoryInfo serviceDir, Action<Type> doForEachProxiedType)
        {
            foreach (string searchPattern in BamConf.ServiceSearchPattern.DelimitSplit(",", "|"))
            {
                ForEachProxiedClass(searchPattern, serviceDir, doForEachProxiedType);
            }
        }

        private void ForEachProxiedClass(string searchPattern, DirectoryInfo ctrlrDir, Action<Type> doForEachProxiedType)
        {
            if (ctrlrDir.Exists)
            {
                FileInfo[] files = ctrlrDir.GetFiles(searchPattern);
                int ol = files.Length;
                for (int i = 0; i < ol; i++)
                {
                    FileInfo file = files[i];
                    Assembly.LoadFrom(file.FullName)
                        .GetTypes()
                        .Where(type => type.HasCustomAttributeOfType<ProxyAttribute>())
                        .Each(t =>
                        {
                            ProxyAttribute attr = t.GetCustomAttributeOfType<ProxyAttribute>();
                            if (!string.IsNullOrEmpty(attr.VarName))
                            {
                                BamConf.AddProxyAlias(attr.VarName, t);
                            }
                            doForEachProxiedType(t);
                        });
                }
            }
        }
        
        public override bool TryRespond(IHttpContext context)
        {
            try
            {
                RequestWrapper request = context.Request as RequestWrapper;
                ResponseWrapper response = context.Response as ResponseWrapper;
                string appName = ApplicationNameResolver.ResolveApplicationName(context);

                bool responded = false;

                if (request != null && response != null)
                {
                    string path = request.Url.AbsolutePath.ToLowerInvariant();

                    if (path.StartsWith("/{0}"._Format(ResponderSignificantName.ToLowerInvariant())))
                    {
                        if (path.StartsWith(MethodFormPrefixFormat._Format(ResponderSignificantName).ToLowerInvariant()))
                        {
                            responded = SendMethodForm(context);
                        }
                        else
                        {
                            responded = SendProxyCode(context);
                        }
                    }
                    else
                    {
                        ExecutionRequest execRequest = ResolveExecutionRequest(context, appName);
                        responded = execRequest.Execute();
                        if (responded)
                        {
                            // TODO: make this configurable
                            response.AddHeader("Access-Control-Allow-Origin", "*");
                            response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");
                            response.AddHeader("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,OPTIONS");
                            // ---
                            RenderResult(appName, path, execRequest);
                        }
                    }
                }
                if (responded)
                {
                    OnResponded(context);
                }
                else
                {
                    OnNotResponded(context);
                }
                return responded;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("An error occurred in {0}.{1}: {2}", ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                OnNotResponded(context);
                return false;
            }
        }

        public event Action<ServiceProxyResponder> Initializing;
        protected void OnInitializing()
        {
            Initializing?.Invoke(this);
        }

        public event Action<ServiceProxyResponder> Initialized;
        protected void OnInitialized()
        {
            Initialized?.Invoke(this);
        }

        public override bool IsInitialized
        {
            get;
            protected set;
        }

        object _initializeLock = new object();
        public override void Initialize()
        {
            OnInitializing();

            if (!IsInitialized)
            {
                IsInitialized = true;
                lock (_initializeLock)
                {
                    AddCommonService(new AppMetaManager(BamConf));
                    RegisterProxiedClasses();
                }
            }
            OnInitialized();
        }
        List<ILogger> _subscribers = new List<ILogger>();
        object _subscriberLock = new object();
        public override ILogger[] Subscribers
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

        public override bool IsSubscribed(ILogger logger)
        {
            lock (_subscriberLock)
            {
                return _subscribers.Contains(logger);
            }
        }

        public override void Subscribe(ILogger logger)
        {
            if (!IsSubscribed(logger))
            {
                this.Logger = logger;
                lock (_subscriberLock)
                {
                    _subscribers.Add(logger);
                }

                string className = typeof(ServiceProxyResponder).Name;
                Initialized += (sp) =>
                {
                    logger.AddEntry("{0}::Initializ(ED)", className);
                };
                Initializing += (sp) =>
                {
                    logger.AddEntry("{0}::Initializ(ING)", className);
                };
            }
        }

        public virtual ExecutionRequest ResolveExecutionRequest(IHttpContext httpContext, string appName)
        {
            GetServiceProxies(appName, out Incubator proxiedClasses, out List<ProxyAlias> aliases);

            return ExecutionRequestResolver.ResolveExecutionRequest(httpContext, proxiedClasses, aliases.ToArray());
        }

        private void RenderResult(string appName, string path, ExecutionRequest execRequest)
        {
            string ext = Path.GetExtension(path).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext))
            {
                AppConf appConf = this.BamConf[appName];
                if (appConf != null)
                {
                    LayoutConf pageConf = new LayoutConf(appConf);
                    string fileName = Path.GetFileName(path);
                    string json = pageConf.ToJson(true);
                    appConf.AppRoot.WriteFile("~/pages/{0}.layout"._Format(fileName), json);
                }
            }

            RendererFactory.Respond(execRequest, ContentResponder);
        }

        private void GetServiceProxies(string appName, out Incubator proxiedClasses, out List<ProxyAlias> aliases)
        {
            proxiedClasses = new Incubator();

            aliases = new List<ProxyAlias>(GetProxyAliases(ServiceProxySystem.Incubator));
            proxiedClasses.CopyFrom(ServiceProxySystem.Incubator, true);

            aliases.AddRange(GetProxyAliases(CommonServiceProvider));
            proxiedClasses.CopyFrom(CommonServiceProvider, true);

            if (AppServiceProviders.ContainsKey(appName))
            {
                Incubator appIncubator = AppServiceProviders[appName];
                aliases.AddRange(GetProxyAliases(appIncubator));
                proxiedClasses.CopyFrom(appIncubator, true);
            }
        }

        private ProxyAlias[] GetProxyAliases(Incubator incubator)
        {
            List<ProxyAlias> results = new List<ProxyAlias>();
            results.AddRange(BamConf.ProxyAliases);
            incubator.ClassNames.Each(cn =>
            {
                Type currentType = incubator[cn];
                ProxyAttribute attr;
                if (currentType.HasCustomAttributeOfType<ProxyAttribute>(out attr))
                {
                    if (!string.IsNullOrEmpty(attr.VarName) && !attr.VarName.Equals(currentType.Name))
                    {
                        results.Add(new ProxyAlias(attr.VarName, currentType));
                    }
                }
            });

            return results.ToArray();
        }

        Dictionary<string, IClientProxyGenerator> _clientProxyGenerators;
        private bool SendProxyCode(IHttpContext context)
        {
            bool result = false;
            IRequest request = context.Request;
            string path = request.Url.AbsolutePath.ToLowerInvariant();
            string appName = ApplicationNameResolver.ResolveApplicationName(context);
            bool includeLocalMethods = request.UserHostAddress.StartsWith("127.0.0.1");
            string[] split = path.DelimitSplit("/", ".");

            if (split.Length >= 2)
            {                
                string fileName = Path.GetFileName(path);
                if (_clientProxyGenerators.ContainsKey(fileName))
                {
                    Incubator combined = new Incubator();
                    combined.CopyFrom(CommonServiceProvider);
                    if (AppServiceProviders.ContainsKey(appName))
                    {
                        Incubator appProviders = AppServiceProviders[appName];
                        combined.CopyFrom(appProviders, true);
                    }
                    _clientProxyGenerators[fileName].SendProxyCode(combined, context);
                    result = true;
                }
            }
            return result;
        }

        private LayoutModel GetLayoutModel(string appName)
        {
            AppConf conf = BamConf.AppConfigs.FirstOrDefault(c => c.Name.Equals(appName));
            LayoutConf defaultLayoutConf = new LayoutConf(conf);
            LayoutModel layoutModel = defaultLayoutConf.CreateLayoutModel();
            return layoutModel;
        }
    }
}
