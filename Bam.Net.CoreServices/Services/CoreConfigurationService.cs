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
    using Bam.Net.CoreServices.Data;
    using Bam.Net.CoreServices.Data.Dao.Repository;
    using Net.Data.SQLite;
    using Server;
    using ServiceProxySecure = ServiceProxy.Secure;

    [Proxy("configSvc")]
    [ServiceProxySecure.ApiKeyRequired]
    public class CoreConfigurationService : CoreProxyableService
    {
        public const string CommonConfigName = "Common";
        protected CoreConfigurationService() { }
        public CoreConfigurationService(CoreRegistryRepository coreRepo, AppConf conf, string databaseRoot)
        {
            AppConf = conf;
            DatabaseRoot = databaseRoot;
            CoreRegistryRepository = coreRepo;
        }
        public string DatabaseRoot { get; set; }
        [Exclude]
        public override object Clone()
        {
            CoreConfigurationService clone = new CoreConfigurationService(CoreRegistryRepository, AppConf, DatabaseRoot);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
        
        public virtual ApplicationConfiguration GetConfiguration(string applicationName = null, string machineName = null, string configurationName = null)
        {
            applicationName = applicationName ?? ApplicationName;
            ValidAppOrDie(applicationName);
            configurationName = configurationName ?? CommonConfigName;
            Dictionary<string, string> commonConfig = GetCommonConfiguration();
            Dictionary<string, string> machineConfig = GetMachineConfiguration(machineName, configurationName);
            Dictionary<string, string> appConfig = GetApplicationConfiguration(applicationName, configurationName);
            ApplicationConfiguration result = new ApplicationConfiguration { Name = configurationName };
            foreach(string key in commonConfig.Keys)
            {
                result[key] = new SourcedConfigurationSetting { SettingSource = Services.SettingSource.CommonSetting, Key = key, Value = commonConfig[key] };
            }
            foreach(string key in machineConfig.Keys)
            {
                result[key] = new SourcedConfigurationSetting { SettingSource = Services.SettingSource.MachineSetting, Key = key, Value = machineConfig[key] };
            }
            foreach(string key in appConfig.Keys)
            {
                result[key] = new SourcedConfigurationSetting { SettingSource = Services.SettingSource.ApplicationSetting, Key = key, Value = appConfig[key] };
            }
            return result;
        }

        static object _commonLock = new object();

        [RoleRequired("Admin")]
        public virtual void SetCommonConfiguration(Dictionary<string, string> settings)
        {
            lock (_commonLock)
            {
                Configuration config = CoreRegistryRepository.GetOneConfigurationWhere(c => c.Name == CommonConfigName && c.ApplicationId == Application.Unknown.Id);
                config.Settings.Each(s => CoreRegistryRepository.Delete(s));
                config.Settings = DictionaryToSettings(settings, config).ToList();
                CoreRegistryRepository.Save(config);
            }
        }
        
        [RoleRequired("Admin")]
        public virtual Dictionary<string, string> GetCommonConfiguration()
        {
            lock (_commonLock)
            {
                Configuration config = CoreRegistryRepository.GetOneConfigurationWhere(c => c.Name == CommonConfigName && c.ApplicationId == Application.Unknown.Id);
                if (config != null)
                {
                    Dictionary<string, string> result = new Dictionary<string, string>();
                    foreach(ConfigurationSetting setting in config.Settings)
                    {
                        if (!result.ContainsKey(setting.Key))
                        {
                            result.Add(setting.Key, setting.Value);
                        }
                        else
                        {
                            Logger.Warning("Configuration ({0}) has duplicate keys specified: {1} with values {2} and {3} using {3}", CommonConfigName, setting.Key, setting.Value, result[setting.Key]);
                        }
                    }
                    return result;
                }
                return new Dictionary<string, string>();
            }
        }
        
        [RoleRequired("Admin")]
        public virtual void SetApplicationConfiguration(Dictionary<string, string> settings, string applicationName = null, string configurationName = null)
        {
            applicationName = applicationName ?? ApplicationName;
            ValidAppOrDie(applicationName);
            configurationName = configurationName ?? CommonConfigName;
            Application application = CoreRegistryRepository.GetOneApplicationWhere(c => c.Name == applicationName);
            lock (Application.ConfigurationLock)
            {
                Configuration config = application.Configurations.FirstOrDefault(c => c.Name.Equals(configurationName));
                if (config == null)
                {
                    config = new Configuration { Name = configurationName };
                    config.ApplicationId = application.Id;
                    config = CoreRegistryRepository.Save(config);
                }
                config.Settings = DictionaryToSettings(settings, config).ToList();
                CoreRegistryRepository.Save(config);
            }
        }

        public virtual void SetMachineConfiguration(string machineName, Dictionary<string, string> settings, string configurationName = null)
        {
            configurationName = configurationName ?? CommonConfigName;
            Machine machine = CoreRegistryRepository.GetOneMachineWhere(c => c.Name == machineName);
            lock (Machine.ConfigurationLock)
            {
                Configuration config = machine.Configurations.FirstOrDefault(c => c.Name.Equals(configurationName));
                if (config == null)
                {
                    config = new Configuration { Name = configurationName };
                    config.MachineId = machine.Id;
                    config = CoreRegistryRepository.Save(config);
                }
                config.Settings = DictionaryToSettings(settings, config).ToList();
                CoreRegistryRepository.Save(config);
            }
        }

        public virtual Dictionary<string, string> GetApplicationConfiguration(string applicationName = null, string configurationName = null)
        {
            applicationName = applicationName ?? ApplicationName;
            ValidAppOrDie(applicationName);
            configurationName = configurationName ?? CommonConfigName;
            Application application = CoreRegistryRepository.GetOneApplicationWhere(c => c.Name == applicationName);
            Configuration config = application.Configurations.FirstOrDefault(c => c.Name.Equals(configurationName));
            if(config != null)
            {
                return SettingsToDictionary(config.Settings);
            }
            return new Dictionary<string, string>();
        }
        
        public virtual Dictionary<string, string> GetMachineConfiguration(string machineName, string configurationName = null)
        {
            configurationName = configurationName ?? CommonConfigName;
            Machine machine = CoreRegistryRepository.GetOneMachineWhere(c => c.Name == machineName);
            Configuration config = machine.Configurations.FirstOrDefault(c => c.Name.Equals(configurationName));
            if (config != null)
            {
                return SettingsToDictionary(config.Settings);
            }
            return new Dictionary<string, string>();
        }

        private Dictionary<string, string> SettingsToDictionary(IEnumerable<ConfigurationSetting> settings)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach(ConfigurationSetting setting in settings)
            {
                result.AddMissing(setting.Key, setting.Value);
            }
            return result;
        }

        private IEnumerable<ConfigurationSetting> DictionaryToSettings(Dictionary<string, string> settings, Configuration config = null)
        {
            foreach(string key in settings.Keys)
            {
                yield return new ConfigurationSetting { Key = key, Value = settings[key], Configuration = config };
            }
        }

        private void ValidAppOrDie(string applicationName)
        {
            Args.ThrowIf(!applicationName.Equals(ApplicationName), "Invalid application name specified: ({0})", applicationName);
        }
    }
}
