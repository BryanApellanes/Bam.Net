using System.Collections.Generic;
using System.Collections.Specialized;
using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.Services.Clients;
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using System.Threading.Tasks;

namespace Bam.Net.Services.Clients
{
    /// <summary>
    /// A class that manages local application configuration using
    /// a remote ConfigurationService
    /// </summary>
    public class ApplicationConfigurationProvider : IConfigurationService
    {
        public ApplicationConfigurationProvider(CoreClient client)
        {
            Args.ThrowIfNull(client, "client");
            Args.ThrowIfNull(client.ConfigurationService, "client.ConfigurationService");
            CoreClient = client;
            Configurations = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            SaveDefaultConfiguration();
        }

        protected Dictionary<string, Dictionary<string, Dictionary<string, string>>> Configurations { get; set; }

        object _getAppConfigLock = new object();
        public Dictionary<string, string> GetApplicationConfiguration(string applicationName, string configurationName = "")
        {
            lock (_getAppConfigLock)
            {
                if (!Configurations.ContainsKey(applicationName))
                {
                    Configurations.Add(applicationName, new Dictionary<string, Dictionary<string, string>>());
                }
                if (!Configurations[applicationName].ContainsKey(configurationName))
                {
                    ApplicationConfiguration config = GetConfiguration(configurationName);
                    Configurations[applicationName].Add(configurationName, config.ToDictionary());
                }
                return Configurations[applicationName][configurationName];
            }
        }

        public void SetApplicationConfiguration(string applicationName, Dictionary<string, string> settings, string configurationName)
        {
            CoreClient.ConfigurationService.SetApplicationConfiguration(settings, applicationName, configurationName);
        }

        public ApplicationConfiguration GetConfiguration(string configurationName = "Default")
        {
            return CoreClient.ConfigurationService.GetConfiguration(ApplicationName, Machine.Current.Name, configurationName);
        }

        bool _defaultConfigurationSaved;
        protected void SaveDefaultConfiguration()
        {
            if (!_defaultConfigurationSaved)
            {
                _defaultConfigurationSaved = true;
                Task.Run(() =>
                {
                    string appNameKey = "ApplicationName";
                    NameValueCollection defaultConfig = DefaultConfiguration.GetAppSettings();
                    string appName = DefaultConfiguration.GetAppSetting(appNameKey, "UNKNOWN");
                    Dictionary<string, string> settings = new Dictionary<string, string>();
                    foreach (string key in defaultConfig.AllKeys)
                    {
                        if (!key.Equals(appNameKey))
                        {
                            settings.Add(key, defaultConfig[key]);
                        }
                    }
                    SetApplicationConfiguration(appName, settings, "Default");
                });
            }
        }

        /// <summary>
        /// Gets the default configuration from the Core
        /// </summary>
        /// <returns></returns>
        public ApplicationConfiguration LoadConfiguration(string configName = "Default", bool inject = false)
        {
            ApplicationConfiguration config = CoreClient.ConfigurationService.GetConfiguration(ApplicationName, Machine.Current.Name, configName);
            if (inject)
            {
                config.Inject();
            }
            return config;
        }

        public void Reload()
        {
            foreach(string appName in Configurations.Keys)
            {
                foreach(string configName in Configurations[appName].Keys)
                {
                    GetApplicationConfiguration(appName, configName);//Configurations[appName][configName] = CoreClient.ConfigurationService.GetApplicationConfiguration(appName, configName);
                }
            }
        }

        public string ApplicationName
        {
            get
            {
                return CoreClient.GetApplicationName();
            }
        }

        protected CoreClient CoreClient { get; set; }

    }
}
