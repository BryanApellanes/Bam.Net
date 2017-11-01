/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Web;
using Bam.Net.Yaml;
using Bam.Net.Configuration;
using Bam.Net.Server;
using Bam.Net.Data;
using Bam.Net.CommandLine;
using Bam.Net.UserAccounts.Data;
using Bam.Net.UserAccounts;
using System.IO;
using System.Reflection;

namespace Bam.Net.Server
{
    /// <summary>
    /// Configuration for the BamServer
    /// </summary>
    public class BamConf
    {
        /// <summary>
        /// The key in the app.config to look for the ContentRoot value
        /// to use for the server
        /// </summary>
        public const string ContentRootConfigKey = "ContentRoot";

        public BamConf()
        {
            this.Fs = new Fs(RuntimeSettings.AppDataFolder);
            this.MaxThreads = 50;
            this.GenerateDao = true;
            this.DaoConfigs = new DaoConf[] { };
            this.InitializeTemplates = true;
            this.InitializeWebBooks = true;
            this.DaoSearchPattern = "*Dao.dll";
            this.LoggerPaths = new string[] { "." };
            this.LoggerSearchPattern = "*Logging.dll";
            this.ServiceSearchPattern = "*Services.dll,*Proxyables.dll";
            this.ServerEventListenerSearchPath = "c:\\bam\\content\\server-listeners,c:\\bam\\content\\server-listeners-temp";
            this.ServerEventListenerAssemblySearchPattern = "*ServerListeners.dll,*ServerEventListeners.dll";
            this.MainLoggerName = "ConsoleLogger";
            this.InitializeFileSystemFromEnum = InitializeFrom.Resource;
            this.ZipPath = "~/bkg/content.root";

            List<SchemaInitializer> schemaInitInfos = new List<SchemaInitializer>();
            schemaInitInfos.Add(new SchemaInitializer(typeof(UserAccountsContext), typeof(SQLiteRegistrarCaller)));

            this._schemaInitializers = schemaInitInfos;
        }

        internal string LoadedFrom { get; set; }

        internal ConfFormat Format
        {
            get;
            set;
        }

        internal BamServer Server
        {
            get;
            set;
        }

        /// <summary>
        /// Server content fs root
        /// </summary>
        internal Fs Fs
        {
            get;
            set;
        }

        internal Fs AppFs(string appName)
        {
            Fs result = null;
            AppConf conf = AppConfigs.Where(ac => ac.Name.Equals(appName) || ac.Name.Equals(appName.ToLowerInvariant())).FirstOrDefault();
            if (conf != null)
            {
                result = conf.AppRoot;
            }

            return result;
        }

        /// <summary>
        /// If true the BamServer will generate Data Access Objects 
        /// for any *.db.js files it finds
        /// </summary>
        public bool GenerateDao
        {
            get;
            set;
        }

        /// <summary>
        /// The search pattern used to find assemblies
        /// that contain services that should be loaded
        /// </summary>
        public string ServiceSearchPattern
        {
            get;
            set;
        }

        InitializeFrom _initializeFrom;
        protected internal InitializeFrom InitializeFileSystemFromEnum
        {
            get
            {
                return _initializeFrom;
            }
            set
            {
                _initializeFrom = value;
            }
        }

        public string InitializeFileSystemFrom
        {
            get
            {
                return InitializeFileSystemFromEnum.ToString();
            }
            set
            {
                Enum.TryParse<InitializeFrom>(value, out _initializeFrom);
            }
        }

        string _zipPath;
        public string ZipPath
        {
            get
            {
                return _zipPath;
            }
            set
            {
                _zipPath = Fs.GetAbsolutePath(value);
            }
        }

        public bool InitializeTemplates
        {
            get;
            set;
        }

        public bool InitializeWebBooks
        {
            get;
            set;
        }
        
        /// <summary>
        /// The file search pattern used to filter 
        /// assemblies for Dao registration
        /// </summary>
        public string DaoSearchPattern
        {
            get;
            set;
        }

        /// <summary>
        /// The root of the filesystem that will be served
        /// </summary>
        public string ContentRoot
        {
            get
            {
                return Fs.Root;
            }
            set
            {
                Fs.Root = value;
            }
        }

        public DaoConf[] DaoConfigs
        {
            get;
            set;
        }

        /// <summary>
        /// Add an alias for the specified Type
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="typeToAlias"></param>
        public void AddProxyAlias(string alias, Type typeToAlias)
        {
            _proxyAliases.Add(new ProxyAlias(alias, typeToAlias));
        }

        List<ProxyAlias> _proxyAliases = new List<ProxyAlias>();
        public ProxyAlias[] ProxyAliases
        {
            get
            {
                return _proxyAliases.ToArray();
            }
            set
            {
                _proxyAliases = new List<ProxyAlias>();
                if (value != null)
                {
                    _proxyAliases.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Directory paths to search for ILogger implementations
        /// </summary>
        public string[] LoggerPaths
        {
            get;
            set;
        }

        /// <summary>
        /// The file search pattern used to 
        /// load assemblies that contain ILogger implementations
        /// </summary>
        public string LoggerSearchPattern
        {
            get;
            set;
        }

        public string MainLoggerName
        {
            get;
            set;
        }

        public string[] AdditionalLoggerNames
        {
            get;
            set;
        }

        public string[] ServerEventListenerNames
        {
            get;
            set;
        }

        public string ServerEventListenerSearchPath
        {
            get;
            set;
        }

        public string ServerEventListenerAssemblySearchPattern
        {
            get;
            set;
        }

        Type[] _availableLoggers;
        object _availableLoggersLock = new object();
        protected internal Type[] AvailableLoggers
        {
            get
            {
                return _availableLoggersLock.DoubleCheckLock(ref _availableLoggers, () =>
                {
                    return LoadLoggers();
                });
            }
        }

        internal protected ILogger GetMainLogger(out Type loggerType)
        {
            return GetLogger(out loggerType, MainLoggerName);
        }

        internal protected ILogger[] GetAdditionalLoggers(out Type[] loggerTypes)
        {
            List<Type> types = new List<Type>();
            List<ILogger> loggers = new List<ILogger>();
            AdditionalLoggerNames.Each(loggerName =>
            {
                Type type;
                loggers.Add(GetLogger(out type, loggerName));
                types.Add(type);
            });
            loggerTypes = types.ToArray();
            return loggers.ToArray();
        }

        internal protected ILogger GetLogger(out Type loggerType, string loggerName)
        {
            loggerType = null;
            if (!string.IsNullOrEmpty(loggerName))
            {
                loggerType = AvailableLoggers.FirstOrDefault(type => type.Name.Equals(MainLoggerName));
            }

            ILogger logger = null;
            if (loggerType == null)
            {
                loggerType = typeof(ConsoleLogger);
                ConsoleLogger tmp = new ConsoleLogger();
                tmp.AddDetails = false;
                tmp.UseColors = true;
                logger = tmp;
            }
            else
            {
                logger = (ILogger)loggerType.Construct();
            }
            logger.StartLoggingThread();
            return logger;
        }

        protected internal BamServerEventListener[] GetServerEventListeners(ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            ReflectionLoader loader = new ReflectionLoader { AssemblySearchPatterns = ServerEventListenerAssemblySearchPattern, Paths = ServerEventListenerSearchPath.DelimitSplit(",", "|"), SearchClassNames = true, TypesToLoad = ServerEventListenerNames };
            List<BamServerEventListener> listeners = new List<BamServerEventListener>();
            loader.Load(logger).Each(t =>
            {
                if (t.IsSubclassOf(typeof(BamServerEventListener)))
                {
                    listeners.Add(t.Construct<BamServerEventListener>());
                    logger.AddEntry("Found {0}: Type = {1}, Assembly = {2}", typeof(BamServerEventListener).Name, t.FullName, t.Assembly.FullName);
                }
                else
                {
                    logger.AddEntry("{0} specified does not extend BamServerEventListener: Type = {1}, Assembly = {2}", typeof(BamServerEventListener).Name, t.FullName, t.Assembly.FullName);
                }
            });

            return listeners.ToArray();
        }

        private Type[] LoadLoggers()
        {
            List<Type> results = new List<Type>();
            Assembly baseAssembly = typeof(ILogger).Assembly;
            results.AddRange(baseAssembly.GetTypes().Where(type => type.ImplementsInterface<ILogger>()).ToArray());

            LoggerPaths.Each(path =>
            {
                DirectoryInfo curDir = new DirectoryInfo(path);
                FileInfo[] files = curDir.GetFiles(LoggerSearchPattern);
                files.Each(file =>
                {
                    try
                    {
                        Assembly currentAssembly = Assembly.LoadFrom(file.FullName);
                        Type[] types = currentAssembly.GetTypes().Where(type => type.ImplementsInterface<ILogger>()).ToArray();
                        results.AddRange(types);
                    }
                    catch //(Exception ex)
                    {
                        // failed
                        // this is acceptable, we're just looking for loggers
                    }
                });
            });

            return results.ToArray();
        }

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

        List<SchemaInitializer> _schemaInitializers;
        public SchemaInitializer[] SchemaInitializers
        {
            get
            {
                return _schemaInitializers.ToArray();
            }
            set
            {
                _schemaInitializers = new List<SchemaInitializer>(value ?? new SchemaInitializer[] { });
            }
        }

        List<AppConf> _appConfigs;
        object _appConfigsLock = new object();
        /// <summary>
        /// Represents the configs for each application found in ~s:/apps 
        /// (where each subdirectory is assumed to be a Bam application)
        /// </summary>
        protected internal AppConf[] AppConfigs
        {
            get
            {
                return _appConfigsLock.DoubleCheckLock(ref _appConfigs, () => InitializeAppConfigs()).ToArray();
            }
        }

        protected internal AppConf[] ReloadAppConfigs()
        {
            lock (_appConfigsLock)
            {
                _appConfigs = null;
                return AppConfigs;
            }
        }
        Dictionary<string, AppConf> _appConfigsByAppName;
        object _appConfigsByAppNameLock = new object();
        /// <summary>
        /// Get the AppConf for the specified appName
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public AppConf this[string appName]
        {
            get
            {
                Dictionary<string, AppConf> dictionary = _appConfigsByAppNameLock.DoubleCheckLock(ref _appConfigsByAppName, () => AppConfigs.ToDictionary(conf => conf.Name));
                if (dictionary.ContainsKey(appName))
                {
                    return dictionary[appName];
                }
                else
                {
                    return null;
                }
            }
        }

        object _initAppConfigsLock = new object();
        /// <summary>
        /// Deserializes each appConf found in subdirectories of
        /// the ~s:/apps folder.  For example, if there is a subfolder named
        /// Monkey in ~s:/apps then this method will search for ~s:/apps/Monkey/appConf.json
        /// then ~s:/apps/Monkey/appConf.yaml if the json file isn't found.  If neither
        /// is found a new AppConf is created and serialized to the json file
        /// specified above.
        /// </summary>
        /// <returns></returns>
        public List<AppConf> InitializeAppConfigs()
        {
            lock (_initAppConfigsLock)
            {
                List<AppConf> configs = new List<AppConf>();
                DirectoryInfo appRoot = new DirectoryInfo(Path.Combine(ContentRoot, "apps"));
                if (!appRoot.Exists)
                {
                    appRoot.Create();
                }
                DirectoryInfo[] appDirs = appRoot.GetDirectories();
                appDirs.Each(appDir =>
                {
                    if (!appDir.Name.StartsWith("."))
                    {
                        bool configFound = false;
                        FileInfo jsonConfig = new FileInfo(Path.Combine(appDir.FullName, "appConf.json"));
                        FileInfo appsConf = jsonConfig;
                        if (appsConf.Exists)
                        {
                            configFound = true;
                            AddJsonConfig(configs, appDir, appsConf);
                        }
                        else
                        {
                            appsConf = new FileInfo(Path.Combine(appDir.FullName, "appConf.yaml"));
                            if (appsConf.Exists)
                            {
                                configFound = true;
                                AddYamlConfig(configs, appDir, appsConf);
                            }
                        }

                        if (!configFound)
                        {
                            AppConf conf = new AppConf(this, appDir.Name);
                            conf.GenerateDao = this.GenerateDao;
                            conf.ToJsonFile(jsonConfig);
                            configs.Add(conf);
                        }
                    }
                });

                return configs;
            }
        }

        private void AddYamlConfig(List<AppConf> configs, DirectoryInfo appDir, FileInfo appsConf)
        {
            AppConf conf = SetAppNameInYaml(appDir, appsConf);
            conf.BamConf = this;
            configs.Add(conf);
        }

        private void AddJsonConfig(List<AppConf> configs, DirectoryInfo appDir, FileInfo appsConf)
        {
            AppConf conf = SetAppNameInJson(appDir, appsConf);
            conf.BamConf = this;
            configs.Add(conf);
        }

        private static AppConf SetAppNameInYaml(DirectoryInfo appDir, FileInfo appsConf)
        {
            AppConf conf = appsConf.FromYamlFile<AppConf>();
            conf.Name = appDir.Name;
            conf.ToYamlFile(appsConf);
            return conf;
        }

        private static AppConf SetAppNameInJson(DirectoryInfo appDir, FileInfo appsConf)
        {
            AppConf conf = appsConf.FromJson<AppConf>();
            if (conf == null)
            {
                conf = new AppConf();
            }
            conf.Name = appDir.Name;
            conf.ToJsonFile(appsConf);
            return conf;
        }

        public static BamConf Load()
        {
            return Load(DefaultConfiguration.GetAppSetting(ContentRootConfigKey, RuntimeSettings.AppDataFolder));
        }

        /// <summary>
        /// Load the BamConf from one of BamConf.json, BamConf.yaml, BamConf.xml
        /// or the Default configuration file whichever is found first in that order.  Default 
        /// will always be provided and will never return null.  A json config will be created
        /// if no config is found of any of the formats json, yaml or xml.
        /// </summary>
        /// <returns></returns>
        public static BamConf Load(string contentRootDir)
        {
            BamConf config = null;

            string jsonConfig = Path.Combine(contentRootDir, string.Format("{0}.json", typeof(BamConf).Name));

            if (File.Exists(jsonConfig))
            {
                BamConf temp = LoadJsonConfig(jsonConfig);
                config = temp;
            }

            if (config == null)
            {
                string yamlConfig = Path.Combine(contentRootDir, string.Format("{0}.yaml", typeof(BamConf).Name));
                if (File.Exists(yamlConfig))
                {
                    BamConf temp = LoadYamlConfig(yamlConfig);
                    config = temp;
                }
            }

            if (config == null)
            {
                string xmlConfig = Path.Combine(contentRootDir, string.Format("{0}.xml", typeof(BamConf).Name));
                if (File.Exists(xmlConfig))
                {
                    BamConf temp = LoadXmlConfig(xmlConfig);
                    config = temp;
                }
            }

            if (config == null)
            {
                config = new BamConf();
                DefaultConfiguration.SetProperties(config);
                config.LoadedFrom = new FileInfo(jsonConfig).FullName;
                config.Save();
            }

            config.ContentRoot = contentRootDir;

            if (config.DaoConfigs.Length == 0)
            {
                config.DaoConfigs = new DaoConf[] { DaoConf.GetDefault("DaoConf", config) };
            }
            return config;
        }

        private static BamConf LoadXmlConfig(string xmlConfig)
        {
            BamConf temp = null;
            temp = xmlConfig.FromXmlFile<BamConf>();
            temp.LoadedFrom = new FileInfo(xmlConfig).FullName;
            temp.Format = ConfFormat.Xml;
            return temp;
        }

        private static BamConf LoadYamlConfig(string yamlConfig)
        {
            BamConf temp = null;
            temp = (BamConf)(yamlConfig.FromYamlFile().FirstOrDefault());
            temp.LoadedFrom = new FileInfo(yamlConfig).FullName;
            temp.Format = ConfFormat.Yaml;
            return temp;
        }

        private static BamConf LoadJsonConfig(string jsonConfig)
        {
            BamConf temp = null;
            temp = jsonConfig.FromJsonFile<BamConf>();
            temp.LoadedFrom = new FileInfo(jsonConfig).FullName;
            temp.Format = ConfFormat.Json;
            return temp;
        }

        public void Save()
        {
            FileInfo file = new FileInfo(LoadedFrom);
            Save(file.Directory.FullName, true, Format);
        }

        public void Save(bool overwrite = false, ConfFormat format = ConfFormat.Json)
        {
            Save(ContentRoot, overwrite, format);
        }

        /// <summary>
        /// Saves the current BamConf into the specified root
        /// </summary>
        /// <param name="rootDir">The directory to save into</param>
        /// <param name="overwrite">If true, overwrite any existing file</param>
        /// <param name="format">The file format to use, will affect the resulting file extension</param>
        public void Save(string rootDir, bool overwrite = false, ConfFormat format = ConfFormat.Json)
        {
            string filePath = ".";
            switch (format)
            {
                case ConfFormat.Yaml:
                    filePath = Path.Combine(rootDir, "{0}.yaml"._Format(typeof(BamConf).Name));
                    if (overwrite || !File.Exists(filePath))
                    {
                        this.ToYaml().SafeWriteToFile(filePath, overwrite);
                    }
                    break;
                case ConfFormat.Json:
                    filePath = Path.Combine(rootDir, "{0}.json"._Format(typeof(BamConf).Name));
                    if (overwrite || !File.Exists(filePath))
                    {
                        this.ToJson(true).SafeWriteToFile(filePath, overwrite);
                    }
                    break;
                case ConfFormat.Xml:
                    filePath = Path.Combine(rootDir, "{0}.xml"._Format(typeof(BamConf).Name));
                    if (overwrite || !File.Exists(filePath))
                    {
                        this.ToXml().SafeWriteToFile(filePath, overwrite);
                    }
                    break;
            }
        }
    }
}
