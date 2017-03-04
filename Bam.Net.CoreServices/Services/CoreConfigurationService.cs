/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bam.Net.Data;
using Bam.Net.Configuration;

namespace Bam.Net.CoreServices
{
    using System.IO;
    using Net.Data.SQLite;
    using Server;
    using ServiceProxySecure = ServiceProxy.Secure;

    [Proxy("configSvc")]
    [ServiceProxySecure.ApiKeyRequired]
    public class CoreConfigurationService : ProxyableService, IConfigurationService
    {
        protected CoreConfigurationService() { }
        public CoreConfigurationService(AppConf conf, string userDbPath)
        {
            AppConf = conf;
            UserDatabasesPath = userDbPath;
        }
        public string UserDatabasesPath { get; set; }
        [Exclude]
        public override object Clone()
        {
            CoreConfigurationService clone = new CoreConfigurationService(AppConf, UserDatabasesPath);
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
            config.Save(GetConfigurationDatabase());
        }
        
        private ServiceProxySecure.Configuration GetConfigurationInstance(string applicationName, string configurationName)
        {
            ServiceProxySecure.Application app = ServiceProxySecure.Application.OneWhere(a => a.Name == applicationName, GetConfigurationDatabase());
            if (app == null)
            {
                app = new ServiceProxySecure.Application();
                app.Name = applicationName;
                app.Save(GetConfigurationDatabase());
            }
            
            ServiceProxySecure.Configuration config = app.ConfigurationsByApplicationId.Where(c => c.Name.Equals(configurationName)).FirstOrDefault();
            if (config == null)
            {
                config = app.ConfigurationsByApplicationId.AddNew();
                config.Name = configurationName;
                app.Save(GetConfigurationDatabase());
            }
            return config;
        }

        public Database GetConfigurationDatabase()
        {
            string userDatabasePath = Path.Combine(UserDatabasesPath, UserName);
            return new SQLiteDatabase(userDatabasePath, nameof(CoreConfigurationService));
        }
    }
}
