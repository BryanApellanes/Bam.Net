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

namespace Bam.Net.Server
{
    public class AppConf
    {
		public const string DefaultPageConst = "start";
		public const string DefaultLayoutConst = "basic";

        public AppConf()
        {
            this._serviceTypeNames = new List<string>();
            this._schemaInitializers = new List<SchemaInitializer>();
            this._serviceTypeNames.Add(typeof(Echo).AssemblyQualifiedName);
            this._serviceTypeNames.Add(typeof(EncryptedEcho).AssemblyQualifiedName);

            this.AppSettings = new AppSetting[] { };
			this.RenderLayoutBody = true;
			this.DefaultLayout = DefaultLayoutConst;
			this.DefaultPage = DefaultPageConst;
			this.ServiceSearchPattern = new string[] { "*Services.dll", "*Proxyables.dll" };
        }

        public AppConf(string name, int port = 8080, bool ssl = false)
            : this()
        {
            this.Name = name;
            this.GenerateDao = true;
            this.Bindings = new HostPrefix[] { new HostPrefix { HostName = name, Port = port, Ssl = ssl } };
        }

        public AppConf(BamConf serverConf, string name)
            : this(name)
        {
            this.BamConf = serverConf;
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
        /// The name of the application.  This will be the name of the 
        /// folder being served from the "apps" folder of the root
        /// of the BamServer
        /// </summary>
        public string Name { get; set; }

        string _displayName;
        public string DisplayName 
        {
            get 
            {
                if (string.IsNullOrEmpty(_displayName)) 
                {
                    _displayName = Name.PascalSplit(" ");
                }

                return _displayName;
            }
            set 
            {
                _displayName = value;
            }
        }

        List<HostPrefix> _bindings;
        object _bindingsLock = new object();
        public HostPrefix[] Bindings
        {
            get
            {
                return _bindingsLock.DoubleCheckLock(ref _bindings, () =>
                {
                    List<HostPrefix> result = new List<HostPrefix>();
                    result.Add(new HostPrefix
                    {
                        HostName = "www.{0}.com"._Format(Name),
                        Port = 80,
                        Ssl = false
                    });
                    result.Add(new HostPrefix
                    {
                        HostName = Name,
                        Port = 8080,
                        Ssl = false
                    });

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

        public static string AppNameFromUri(Uri uri, AppConf[] appConfigs = null)
        {
            string result = AppNameFromBinding(uri, appConfigs);
            if (string.IsNullOrEmpty(result))
            {
                string fullDomainName = uri.Authority.DelimitSplit(":")[0].ToLowerInvariant();
                string[] splitOnDots = fullDomainName.DelimitSplit(".");
                result = fullDomainName;
                if (splitOnDots.Length == 2)
                {
                    result = splitOnDots[0];
                }
                else if (splitOnDots.Length == 3)
                {
                    result = splitOnDots[1];
                }
            }

            return result;
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

        private static string AppNameFromBinding(Uri uri, AppConf[] configs)
        {
            AppConf conf = configs.Where(c => c.Bindings.Any(h => h.HostName.Equals(uri.Authority))).FirstOrDefault();
            if (conf != null)
            {
                return conf.Name;
            }
            return string.Empty;
        }
    }
}
