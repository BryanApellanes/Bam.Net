using Bam.Net.Logging;
using Bam.Net.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;

namespace Bam.Net.Configuration
{
    public partial class ConfigurationResolver: Loggable
    {
        public ConfigurationResolver(ILogger logger = null)
        {
            DefaultConfiguration = ConfigurationManager.AppSettings;
        }

        public ConfigurationResolver(IConfiguration configuration, ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
            NetCoreConfiguration = configuration;
            DefaultConfiguration = ConfigurationManager.AppSettings;
        }

        public IConfiguration NetCoreConfiguration { get; set; }
        public NameValueCollection DefaultConfiguration { get; set; }

        [Inject]
        public ILogger Logger { get; set; }

        [Inject]
        public IConfigurationService ConfigurationService { get; set; }

        public string this[string key, bool callConfigService = false]
        {
            get
            {
                string value = NetCoreConfiguration?[key];
                if (string.IsNullOrEmpty(value))
                {
                    value = DefaultConfiguration[key];
                }
                if(string.IsNullOrEmpty(value) && callConfigService)
                {
                    value = FromService(key);
                }
                if (string.IsNullOrEmpty(value))
                {
                    FireEvent(ConfigurationValueNotFound, new ConfigurationEventArgs { Key = key });
                }
                return value;
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

        public EventHandler CallingConfigService;
        public EventHandler CalledConfigService;

        public EventHandler RetrievingFromService;
        public EventHandler RetrievedFromCache;
        public EventHandler RetrievedFromService;

        public EventHandler ConfigurationValueNotFound;

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
            if (ConfigurationService != null && !string.IsNullOrEmpty(ApplicationName))
            {
                FireEvent(RetrievingFromService, new ConfigurationEventArgs { Key = key });
                _config = ConfigurationService.GetApplicationConfiguration(ApplicationName);
                if(_config != null && _config.ContainsKey(key))
                {
                    string value = _config[key];
                    FireEvent(RetrievedFromCache, new ConfigurationEventArgs { Key = key, Value = value });
                    return value;
                }
            }            
            return string.Empty;
        }
    }
}
