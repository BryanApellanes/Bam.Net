using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.CoreServices.Configuration;
using Bam.Net.Encryption;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Clients
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bam.Net.Encryption.ICredentialProvider" />
    public class ConfigurationServiceCredentialProvider : ICredentialProvider, INamedCredentialProvider, IServiceCredentialProvider
    {
        Dictionary<SettingSource, Func<string>> _passwordRetrievers;
        Dictionary<SettingSource, Func<string>> _userNameRetrievers;
        Dictionary<SettingSource, Func<string, string>> _namedPasswordRetrievers;
        Dictionary<SettingSource, Func<string, string>> _namedUserNameRetrievers;

        public ConfigurationServiceCredentialProvider(string coreConfigurationServerHost, IApplicationNameProvider appNameProvider, int port = 80, ILogger logger = null)
        {
            SettingSource = SettingSource.DefaultConfigurationFile;
            ConfigurationServerHost = coreConfigurationServerHost;
            ConfigurationServerPort = port;
            Logger = logger ?? Log.Default;
            ApplicationNameProvider = appNameProvider;
            _passwordRetrievers = new Dictionary<SettingSource, Func<string>>()
            {
                { SettingSource.Invalid, () => throw new InvalidOperationException() },
                { SettingSource.DefaultConfigurationFile, () => DefaultConfigurationCredentialProvider.Instance.GetPassword()},
                { SettingSource.CommonSetting, () => GetCommonSetting("Password") },
                { SettingSource.MachineSetting, () => GetMachineSetting("Password") },
                { SettingSource.ApplicationSetting, () => GetApplicationSetting("Password") }
            };
            _userNameRetrievers = new Dictionary<SettingSource, Func<string>>()
            {
                { SettingSource.Invalid, () => throw new InvalidOperationException() },
                { SettingSource.DefaultConfigurationFile, () => DefaultConfigurationCredentialProvider.Instance.GetUserName() },
                { SettingSource.CommonSetting, () => GetCommonSetting("User") },
                { SettingSource.MachineSetting, () => GetMachineSetting("User") },
                { SettingSource.ApplicationSetting, () => GetApplicationSetting("User") }
            };
            _namedPasswordRetrievers = new Dictionary<SettingSource, Func<string, string>>()
            {
                { SettingSource.Invalid, (name) => throw new InvalidOperationException() },
                { SettingSource.DefaultConfigurationFile, (name) => DefaultConfigurationCredentialProvider.Instance.GetPasswordFor(name)},
                { SettingSource.CommonSetting, (name) => GetCommonSetting($"{name}.Password") },
                { SettingSource.MachineSetting, (name) => GetMachineSetting($"{name}.Password") },
                { SettingSource.ApplicationSetting, (name) => GetApplicationSetting($"{name}.Password") }
            };
            _namedUserNameRetrievers = new Dictionary<SettingSource, Func<string, string>>()
            {
                { SettingSource.Invalid, (name) => throw new InvalidOperationException() },
                { SettingSource.DefaultConfigurationFile, (name) => DefaultConfigurationCredentialProvider.Instance.GetUserNameFor(name) },
                { SettingSource.CommonSetting, (name) => GetCommonSetting($"{name}.User") },
                { SettingSource.MachineSetting, (name) => GetMachineSetting($"{name}.User") },
                { SettingSource.ApplicationSetting, (name) => GetApplicationSetting($"{name}.User") }
            };
        }

        public ConfigurationServiceCredentialProvider(string coreConfigurationServerHost, int port = 80, ILogger logger = null) 
            : this(coreConfigurationServerHost, DefaultConfigurationApplicationNameProvider.Instance, port, logger)
        { }

        public IApplicationNameProvider ApplicationNameProvider { get; set; }

        protected string ConfigurationServerHost { get; set; }
        protected int ConfigurationServerPort { get; set; }

        protected string GetCommonSetting(string key)
        {
            return ConfigurationService.GetCommonConfiguration()[key];
        }

        protected string GetMachineSetting(string key)
        {
            return ConfigurationService.GetMachineConfiguration(Environment.MachineName)[key];
        }

        protected string GetApplicationSetting(string key)
        {
            return ConfigurationService.GetApplicationConfiguration(ApplicationNameProvider.GetApplicationName())[key];
        }

        public ILogger Logger { get; set; }

        public SettingSource SettingSource { get; set; }

        ConfigurationService _configurationService;
        object _configLock = new object();
        public ConfigurationService ConfigurationService
        {
            get
            {
                return _configLock.DoubleCheckLock(ref _configurationService, () => new ProxyFactory().GetProxy<ConfigurationService>(ConfigurationServerHost, ConfigurationServerPort, Logger));
            }
        }

        public string GetPassword()
        {
            return _passwordRetrievers[SettingSource]();
        }

        public string GetUserName()
        {
            return _userNameRetrievers[SettingSource]();
        }

        public string GetUserNameFor(string targetIdentifier)
        {
            return _namedUserNameRetrievers[SettingSource](targetIdentifier);
        }

        public string GetPasswordFor(string targetIdentifier)
        {
            return _namedUserNameRetrievers[SettingSource](targetIdentifier);
        }

        public string GetUserNameFor(string machineName, string serviceName)
        {
            return ConfigurationService.GetMachineConfiguration(machineName)[$"{serviceName}.User"];
        }

        public string GetPasswordFor(string machineName, string serviceName)
        {
            return ConfigurationService.GetMachineConfiguration(machineName)[$"{serviceName}.Password"];
        }
    }
}
