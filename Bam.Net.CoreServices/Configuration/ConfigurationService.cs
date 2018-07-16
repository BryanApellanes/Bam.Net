using System.Collections.Generic;
using System.Linq;

namespace Bam.Net.CoreServices
{
    using Bam.Net.CoreServices.ApplicationRegistration.Data;
    using Server;
    using ServiceProxySecure = ServiceProxy.Secure;
    using Bam.Net.ServiceProxy;
    using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;
    using Bam.Net.CoreServices.Configuration;

    [Proxy("configSvc")]
    [ServiceProxySecure.ApiKeyRequired]
    [ServiceSubdomain("config")]
    public class ConfigurationService : ApplicationProxyableService, ICoreConfigurationService
    {
        public const string CommonConfigName = "Common";
        protected ConfigurationService() { }
        public ConfigurationService(ApplicationRegistrationRepository coreRepo, AppConf conf, string databaseRoot)
        {
            AppConf = conf;
            DatabaseRoot = databaseRoot;
            ApplicationRegistrationRepository = coreRepo;
        }
        public string DatabaseRoot { get; set; }
        [Exclude]
        public override object Clone()
        {
            ConfigurationService clone = new ConfigurationService(ApplicationRegistrationRepository, AppConf, DatabaseRoot);
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
                result[key] = new SourcedConfigurationSetting { SettingSource = SettingSource.CommonSetting, Key = key, Value = commonConfig[key] };
            }
            foreach(string key in machineConfig.Keys)
            {
                result[key] = new SourcedConfigurationSetting { SettingSource = SettingSource.MachineSetting, Key = key, Value = machineConfig[key] };
            }
            foreach(string key in appConfig.Keys)
            {
                result[key] = new SourcedConfigurationSetting { SettingSource = SettingSource.ApplicationSetting, Key = key, Value = appConfig[key] };
            }
            return result;
        }

        static object _commonLock = new object();

        [RoleRequired("/", "Admin")]
        public virtual void SetCommonConfiguration(Dictionary<string, string> settings)
        {
            lock (_commonLock)
            {
                ApplicationRegistration.Data.Configuration config = ApplicationRegistrationRepository.GetOneConfigurationWhere(c => c.Name == CommonConfigName && c.ApplicationId == Application.Unknown.Id);
                config.Settings.Each(s => ApplicationRegistrationRepository.Delete(s));
                config.Settings = DictionaryToSettings(settings, config).ToList();
                ApplicationRegistrationRepository.Save(config);
            }
        }
        
        [RoleRequired("/", "Admin")]
        public virtual Dictionary<string, string> GetCommonConfiguration()
        {
            lock (_commonLock)
            {
                ApplicationRegistration.Data.Configuration config = ApplicationRegistrationRepository.GetOneConfigurationWhere(c => c.Name == CommonConfigName && c.ApplicationId == Application.Unknown.Id);
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

        [RoleRequired("/", "Admin")]
        public virtual void SetApplicationConfiguration(string applicationName, Dictionary<string, string> configuration, string configurationName)
        {
            SetApplicationConfiguration(configuration, applicationName, configurationName);
        }

        [RoleRequired("/", "Admin")]
        public virtual void SetApplicationConfiguration(Dictionary<string, string> settings, string applicationName = null, string configurationName = null)
        {
            applicationName = applicationName ?? ApplicationName;
            ValidAppOrDie(applicationName);
            configurationName = configurationName ?? CommonConfigName;
            Application application = ApplicationRegistrationRepository.GetOneApplicationWhere(c => c.Name == applicationName);
            lock (Application.ConfigurationLock)
            {
                ApplicationRegistration.Data.Configuration config = application.Configurations.FirstOrDefault(c => c.Name.Equals(configurationName));
                if (config == null)
                {
                    config = new ApplicationRegistration.Data.Configuration { Name = configurationName };
                    config.ApplicationId = application.Id;
                    config = ApplicationRegistrationRepository.Save(config);
                }
                config.Settings = DictionaryToSettings(settings, config).ToList();
                ApplicationRegistrationRepository.Save(config);
            }
        }

        [RoleRequired("/", "Admin")]
        public virtual void SetMachineConfiguration(string machineName, Dictionary<string, string> settings, string configurationName = null)
        {
            configurationName = configurationName ?? CommonConfigName;
            Machine machine = ApplicationRegistrationRepository.GetOneMachineWhere(c => c.Name == machineName);
            lock (Machine.ConfigurationLock)
            {
                ApplicationRegistration.Data.Configuration config = machine.Configurations.FirstOrDefault(c => c.Name.Equals(configurationName));
                if (config == null)
                {
                    config = new ApplicationRegistration.Data.Configuration { Name = configurationName };
                    config.MachineId = machine.Id;
                    config = ApplicationRegistrationRepository.Save(config);
                }
                config.Settings = DictionaryToSettings(settings, config).ToList();
                ApplicationRegistrationRepository.Save(config);
            }
        }

        public virtual Dictionary<string, string> GetApplicationConfiguration(string applicationName = null, string configurationName = null)
        {
            applicationName = applicationName ?? ApplicationName;
            ValidAppOrDie(applicationName);
            configurationName = configurationName ?? CommonConfigName;
            Application application = ApplicationRegistrationRepository.GetOneApplicationWhere(c => c.Name == applicationName);
            ApplicationRegistration.Data.Configuration config = application.Configurations.FirstOrDefault(c => c.Name.Equals(configurationName));
            if(config != null)
            {
                return SettingsToDictionary(config.Settings);
            }
            return new Dictionary<string, string>();
        }
        
        public virtual Dictionary<string, string> GetMachineConfiguration(string machineName, string configurationName = null)
        {
            configurationName = configurationName ?? CommonConfigName;
            Machine machine = ApplicationRegistrationRepository.GetOneMachineWhere(c => c.Name == machineName);
            ApplicationRegistration.Data.Configuration config = machine.Configurations.FirstOrDefault(c => c.Name.Equals(configurationName));
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

        private IEnumerable<ConfigurationSetting> DictionaryToSettings(Dictionary<string, string> settings, ApplicationRegistration.Data.Configuration config = null)
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
