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

namespace Bam.Net.Server
{
    public class AppConf
    {
		public const string DefaultPageConst = "start";
		public const string DefaultLayoutConst = "basic";

        public AppConf()
        {
            this._serviceProxyTypeNames = new List<string>();
            this._schemaInitializers = new List<SchemaInitializer>();
            this._serviceProxyTypeNames.Add(typeof(Echo).AssemblyQualifiedName);
            this._serviceProxyTypeNames.Add(typeof(EncryptedEcho).AssemblyQualifiedName);

			this.RenderLayoutBody = true;
			this.DefaultLayout = DefaultLayoutConst;
			this.DefaultPage = DefaultPageConst;
			this.ServiceProxySearchPatterns = new string[] { "*Services.dll", "*Proxyables.dll" };
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

        internal BamConf BamConf
        {
            get;
            set;
        }

        internal ILogger Logger
        {
            get
            {
                if(BamConf != null &&
                    BamConf.Server != null &&
                    BamConf.Server.MainLogger != null)
                {
                    return BamConf.Server.MainLogger;
                }

                return new NullLogger();
            }
        }

        Fs _appRoot;
        object _appRootLock = new object();
        internal Fs AppRoot
        {
            get
            {
                return _appRootLock.DoubleCheckLock(ref _appRoot, () =>
                {
                    return new Fs(Path.Combine(BamConf.ContentRoot, "apps", Name));
                });
            }
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

		public string[] ServiceProxySearchPatterns
		{
			get;
			set;
		}

        List<string> _serviceProxyTypeNames;
        public string[] ServiceProxyTypeNames
        {
            get
            {
                return _serviceProxyTypeNames.ToArray();
            }
            set
            {
                _serviceProxyTypeNames = new List<string>(value);
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
		/// The assembly qualified name of an IInitialize
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

        UserManagerConfig _userManager;
        public UserManagerConfig UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = new UserManagerConfig();
                }

                return _userManager;
            }
            set
            {
                _userManager = value;
            }
        }
        
        public static string AppNameFromUri(Uri uri)
        {
            string fullDomainName = uri.Authority.DelimitSplit(":")[0].ToLowerInvariant();
            string[] splitOnDots = fullDomainName.DelimitSplit(".");
            string result = fullDomainName;
            if (splitOnDots.Length == 2)
            {
                result = splitOnDots[0];
            }
            else if (splitOnDots.Length == 3)
            {
                result = splitOnDots[1];
            }

            return result;
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
    }
}
