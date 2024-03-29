/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Bam.Net.UserAccounts;
using Bam.Net.Data;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using Bam.Net.Server;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Bam.Net.Incubation;
using Bam.Net.Configuration;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.Server
{
    /// <summary>
    /// Configuration for a Bam Application
    /// </summary>
    public class AppConf
    {
		public const string DefaultPageConst = "start";
		public const string DefaultLayoutConst = "basic";

        public AppConf()
        {
            _serviceTypeNames = new List<string>();
            _schemaInitializers = new List<SchemaInitializer>();
            _serviceTypeNames.Add(typeof(Echo).AssemblyQualifiedName);
            _serviceTypeNames.Add(typeof(EncryptedEcho).AssemblyQualifiedName);

            AppSettings = new AppSetting[] { };
			RenderLayoutBody = true;
			DefaultLayout = DefaultLayoutConst;
			DefaultPage = DefaultPageConst;
			ServiceSearchPattern = new string[] { "*Services.dll", "*Proxyables.dll" };
            ProcessMode = "Dev";
        }

        public AppConf(string name, int port = 8080, bool ssl = false)
            : this()
        {
            Name = name;
            GenerateDao = true;
            Bindings = new HostPrefix[] { new HostPrefix { HostName = name, Port = port, Ssl = ssl } };
        }

        public AppConf(BamConf serverConf, string name)
            : this(name)
        {
            BamConf = serverConf;
        }

        public static AppConf FromDefaultConfig()
        {
            return new AppConf { Name = DefaultConfiguration.GetAppSetting("ApplicationName", ServiceProxy.Secure.Application.Unknown.Name) };
        }

        Fs _appRoot;
        object _appRootLock = new object();
        [JsonIgnore]
        [XmlIgnore]
        public Fs AppRoot
        {
            get
            {
                return _appRootLock.DoubleCheckLock(ref _appRoot, () =>
                {
                    return new Fs(Path.Combine(BamConf.ContentRoot, "apps", Name));
                });
            }
        }

        public BamServer GetServer()
        {
            return BamConf.Server;
        }

        public void AddServices(Incubator incubator)
        {
            BamConf.Server.AddAppServies(Name, incubator);
        }

        /// <summary>
        /// Add a service of the specified type to be 
        /// instanciated by the specified function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceInstanciator"></param>
        public void AddService<T>(Func<T> serviceInstanciator)
        {
            BamConf.Server.AddAppService<T>(Name, serviceInstanciator);
        }

        /// <summary>
        /// Add the specified instance as a service to 
        /// be exposed to the client
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void AddService<T>(T instance)
        {
            BamConf.Server.AddAppService<T>(Name);
        }

        /// <summary>
        /// The name of the application.  This will typically be the name of the 
        /// folder being served from the "apps" folder of the root
        /// of the BamServer depending on how the AppConf was instantiated.
        /// </summary>
        public string Name { get; set; }

        string _displayName;
        public string DisplayName 
        {
            get 
            {
                if (string.IsNullOrEmpty(_displayName)) 
                {
                    _displayName = Name?.PascalSplit(" ") ?? "AppConf.DisplayName not specified";
                }

                return _displayName;
            }
            set 
            {
                _displayName = value;
            }
        }

        public bool IsProd => ProcessMode.Equals("Prod");
        public bool IsTest => ProcessMode.Equals("Test");

        public string ProcessMode { get; set; } 

        List<HostPrefix> _bindings;
        object _bindingsLock = new object();
        public HostPrefix[] Bindings
        {
            get
            {
                return _bindingsLock.DoubleCheckLock(ref _bindings, () =>
                {
                    List<HostPrefix> result = new List<HostPrefix>
                    {
                        new HostPrefix
                        {
                            HostName = $"{Name}.bamapps.com",
                            Port = 80,
                            Ssl = false
                        },
                        new HostPrefix
                        {
                            HostName = Name,
                            Port = 8080,
                            Ssl = false
                        }
                    };

                    return result;
                }).ToArray();
            }
            set
            {
                _bindings = new List<HostPrefix>(value);
            }
        }

        public AppSetting[] AppSettings { get; set; }
        /// <summary>
        /// The name of the default layout 
        /// </summary>
        public string DefaultLayout { get; set; }

		string _defaultPage;
		/// <summary>
		/// The name of the page to serve when the 
		/// root is requested "/".
		/// </summary>
		public string DefaultPage
		{
			get
			{
				return _defaultPage;
			}
			set
			{
				_defaultPage = value.Or(DefaultPageConst);
			}
		}

        public bool CompileTemplates { get; set; }
        public bool GenerateDao { get; set; }

        public bool CheckDaoHashes { get; set; }

		public bool RenderLayoutBody { get; set; }

		public string[] ServiceSearchPattern { get; set; }

        public bool ExtractBaseApp { get; set; }

        List<string> _serviceTypeNames;
        public string[] ServiceTypeNames
        {
            get
            {
                return _serviceTypeNames.ToArray();
            }
            set
            {
                _serviceTypeNames = new List<string>(value);
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
                _schemaInitializers = new List<SchemaInitializer>(value);
            }
        }

        /// <summary>
        /// The assembly qualified name of an IAppInitializer
        /// implementation that will be called on application 
        /// initialization
        /// </summary>
        public string AppInitializer
        {
            get;
            set;
        }

		/// <summary>
		/// The file path to the assembly that contains
		/// the type specified by AppInitializer
		/// </summary>
        public string AppInitializerAssemblyPath
        {
            get;
            set;
        }

        UserManagerConfig _userManagerConfig;
        public UserManagerConfig UserManagerConfig
        {
            get
            {
                if (_userManagerConfig == null)
                {
                    _userManagerConfig = new UserManagerConfig();
                }

                return _userManagerConfig;
            }
            set
            {
                _userManagerConfig = value;
            }
        }

        UserManager _userManager;
        public UserManager GetUserManager()
        {
            if(_userManager == null)
            {
                _userManager = UserManagerConfig.Create(Logger);
            }
            return _userManager;
        }

        public string Setting(string settingName, string insteadIfNullOrEmpty)
        {
            AppSetting setting = AppSettings.Where(s => s.Name.Equals(settingName)).FirstOrDefault();
            if(setting != null)
            {
                return setting.Value;
            }
            Logger.AddEntry("AppConf.AppSettings[{0}]: Setting Name ({0}), not found; using value ({1}) instead", LogEventType.Warning, settingName, insteadIfNullOrEmpty);
            return insteadIfNullOrEmpty;
        }

        /// <summary>
        /// Get the application id used in the dom by parsing the appName.
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static string DomApplicationIdFromAppName(string appName)
        {
            string[] split = appName.DelimitSplit(".");
            string result = split[0];
            if (split.Length == 3)
            {
                result = split[1];
            }

            AppNamesByDomAppId[result] = appName;

            return result;
        }

        public ILogger GetLogger() // methods don't serialize
        {
            return Logger;
        }

        internal BamConf BamConf
        {
            get;
            set;
        }

        internal ILogger Logger
        {
            get
            {
                if (BamConf != null &&
                    BamConf.Server != null &&
                    BamConf.Server.MainLogger != null)
                {
                    return BamConf.Server.MainLogger;
                }

                return new NullLogger();
            }
        }

        static Dictionary<string, string> _appNamesByDomAppId;
        static object _domAppIdsSync = new object();
        protected internal static Dictionary<string, string> AppNamesByDomAppId
        {
            get
            {
                return _domAppIdsSync.DoubleCheckLock(ref _appNamesByDomAppId, () => new Dictionary<string, string>());
            }
        }
    }
}
