/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bam.Net.Data;


namespace Bam.Net.CoreServices
{
    using Server;
    using ServiceProxySecure = ServiceProxy.Secure;

    [Proxy("configSvc")]
    [ServiceProxySecure.ApiKeyRequired]
    public class CoreConfigurationService: ProxyableService
    {
        protected CoreConfigurationService() { }
        public CoreConfigurationService(IDatabaseProvider dbProvider, AppConf conf)
        {
            dbProvider.SetDatabases(this);
            DatabaseProvider = dbProvider;
            AppConf = conf;
        }
        [Exclude]
        public override object Clone()
        {
            CoreConfigurationService clone = new CoreConfigurationService(DatabaseProvider, AppConf);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
        public virtual Dictionary<string, string> GetConfiguration(string applicationName, string configurationName = "")
        {
            ServiceProxySecure.Configuration config = GetConfigurationInstance(applicationName, configurationName);

            Dictionary<string, string> result = new Dictionary<string, string>();
            config.ConfigSettingsByConfigurationId.Each(cs =>
            {
                result.Add(cs.Key, cs.Value);
            });

            return result;
        }

        public virtual void SetConfiguration(string applicationName, string configurationName, Dictionary<string, string> configuration)
        {
            ServiceProxySecure.Configuration config = GetConfigurationInstance(applicationName, configurationName);
            config.ConfigSettingsByConfigurationId.Delete();
            configuration.Keys.Each(key =>
            {
                ServiceProxySecure.ConfigSetting setting = config.ConfigSettingsByConfigurationId.AddNew();
                setting.Key = key;
                setting.Value = configuration[key];
            });
            config.Save(this.ConfigurationDatabase);
        }
        
        private ServiceProxySecure.Configuration GetConfigurationInstance(string applicationName, string configurationName)
        {
            ServiceProxySecure.Application app = ServiceProxySecure.Application.OneWhere(a => a.Name == applicationName);
            if (app == null)
            {
                app = new ServiceProxySecure.Application();
                app.Name = applicationName;
                app.Save(this.ConfigurationDatabase);
            }

            ServiceProxySecure.Configuration config = app.ConfigurationsByApplicationId.Where(c => c.Name.Equals(configurationName)).FirstOrDefault();
            if (config == null)
            {
                config = app.ConfigurationsByApplicationId.AddNew();
                config.Name = configurationName;
                app.Save(this.ConfigurationDatabase);
            }
            return config;
        }

        public Database ConfigurationDatabase { get; set; }
    }
}
