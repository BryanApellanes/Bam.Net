using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Configuration;
using Bam.Net.CoreServices;

namespace gloo.services
{
    public class ApplicationConfigurationService : IConfigurationService
    {
        public ApplicationConfigurationService(CoreClient client)
        {
            Args.ThrowIfNull(client, "client");
            Args.ThrowIfNull(client.ConfigurationService, "client.ConfigurationService");
            CoreClient = client;
            Configurations = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            SetDefault();
        }
        protected CoreClient CoreClient { get; set; }
        protected void SetDefault()
        {
            string appNameKey = "ApplicationName";
            NameValueCollection defaultConfig = DefaultConfiguration.GetAppSettings();
            string appName = DefaultConfiguration.GetAppSetting(appNameKey, "UNKNOWN");
            Dictionary<string, string> config = new Dictionary<string, string>();
            foreach(string key in defaultConfig.AllKeys)
            {
                if (!key.Equals(appNameKey))
                {
                    config.Add(key, defaultConfig[key]);
                }
            }
            SetConfiguration(appName, "Default", config);
        }
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> Configurations { get; set; }
       
        public Dictionary<string, string> GetConfiguration(string applicationName, string configurationName = "")
        {
            if (!Configurations.ContainsKey(applicationName))
            {
                Configurations.Add(applicationName, new Dictionary<string, Dictionary<string, string>>());
            }
            if (!Configurations[applicationName].ContainsKey(configurationName))
            {
                Configurations[applicationName].Add(configurationName, CoreClient.ConfigurationService.GetConfiguration(applicationName, configurationName));
            }
            return Configurations[applicationName][configurationName];
        }

        public void SetConfiguration(string applicationName, string configurationName, Dictionary<string, string> configuration)
        {
            CoreClient.ConfigurationService.SetConfiguration(applicationName, configurationName, configuration);
        }

        public void Reload()
        {
            foreach(string appName in Configurations.Keys)
            {
                foreach(string configName in Configurations[appName].Keys)
                {
                    Configurations[appName][configName] = CoreClient.ConfigurationService.GetConfiguration(appName, configName);
                }
            }
        }
    }
}
