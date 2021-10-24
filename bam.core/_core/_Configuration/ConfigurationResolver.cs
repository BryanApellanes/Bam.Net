using Bam.Net.Logging;
using Bam.Net.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;

namespace Bam.Net.Configuration
{
    public partial class ConfigurationResolver: Loggable
    {
        public ConfigurationResolver(ILogger logger = null)
        {
            DefaultConfiguration = ConfigurationManager.AppSettings;
            ConfigurationProvider = new DefaultConfigurationProvider();
            Config = Config.Current;
            AppSettings = Config.AppSettings;
        }

        public ConfigurationResolver(IConfiguration configuration, ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
            NetCoreConfiguration = configuration;
            DefaultConfiguration = ConfigurationManager.AppSettings;
            Config = Config.Current;
            AppSettings = Config.AppSettings;
        }
        
        public Config Config { get; }
        
        public Dictionary<string, string> AppSettings { get; }

        public IConfiguration NetCoreConfiguration { get; set; }
        public NameValueCollection DefaultConfiguration { get; set; }

        [Inject]
        public ILogger Logger { get; set; }

        [Inject]
        public IConfigurationProvider ConfigurationProvider { get; set; }

        public ConfigurationValue this[string key, string defaultValue = null, bool callConfigService = false]
        {
            get
            {
                ConfigurationSources source = ConfigurationSources.NotFound;

                string value = NetCoreConfiguration?[key];
                if(!string.IsNullOrEmpty(value))
                {
                    source = ConfigurationSources.NetCoreConfiguration;
                }
                else
                {
                    value = DefaultConfiguration[key];
                    source = ConfigurationSources.DefaultConfiguration;
                }

                if (string.IsNullOrEmpty(value) && AppSettings.ContainsKey(key))
                {
                    value = AppSettings[key];
                    source = ConfigurationSources.ConfigAppSettings;
                }

                if (string.IsNullOrEmpty(value))
                {
                    value = BamEnvironmentVariables.GetBamVariable(key);
                    source = ConfigurationSources.BamEnvironmentVariable;
                }

                if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(defaultValue))
                {
                    value = defaultValue;
                    source = ConfigurationSources.DefaultValue;
                }

                if (string.IsNullOrEmpty(value) && callConfigService)
                {
                    value = FromService(key);
                    source = ConfigurationSources.ConfigurationService;
                }

                if (string.IsNullOrEmpty(value))
                {
                    FireEvent(ConfigurationValueNotFound, new ConfigurationEventArgs { Key = key });
                }

                if (!string.IsNullOrEmpty(value))
                {
                    Config.AppSettings.AddMissing(key, value);
                    Config.Save();
                }

                return new ConfigurationValue(value)
                {
                    Key = key,
                    DefaultValue = defaultValue,
                    CallConfigService = callConfigService,
                    ConfigurationSource = source
                };
            }       
        }

        public static void Startup(IConfiguration configuration)
        {
            Current = new ConfigurationResolver(configuration);
        }
        
        protected string ApplicationName
        {
            get
            {
                return this["ApplicationName"];
            }
        }

        public event EventHandler CallingConfigService;
        public event EventHandler CalledConfigService;

        public event EventHandler RetrievingFromService;
        public event EventHandler RetrievedFromCache;
        public event EventHandler RetrievedFromService;

        public event EventHandler ConfigurationValueNotFound;

        Dictionary<string, string> _config;
        private string FromService(string key)
        {
            FireEvent(CallingConfigService, new ConfigurationEventArgs { Key = key });
            if (_config != null)
            {
                if (_config.ContainsKey(key))
                {
                    string value = _config[key];
                    FireEvent(RetrievedFromCache, new ConfigurationEventArgs { Key = key });
                    return value;
                }
            }
            if (ConfigurationProvider != null && !string.IsNullOrEmpty(ApplicationName)) // TODO: account for UNKOWN application
            {
                FireEvent(RetrievingFromService, new ConfigurationEventArgs { Key = key });
                _config = ConfigurationProvider.GetApplicationConfiguration(ApplicationName);
                if(_config != null && _config.ContainsKey(key))
                {
                    string value = _config[key];
                    FireEvent(RetrievedFromService, new ConfigurationEventArgs { Key = key, Value = value });
                    return value;
                }
            }            
            FireEvent(CalledConfigService, new ConfigurationEventArgs{Key = key});
            return string.Empty;
        }
    }
}
