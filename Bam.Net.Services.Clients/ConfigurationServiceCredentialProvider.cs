using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.CoreServices.Configuration;
using Bam.Net.Encryption;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Clients
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bam.Net.Encryption.ICredentialProvider" />
    public class ConfigurationServiceCredentialProvider : ICredentialProvider
    {
        Dictionary<SettingSource, Func<string>> _passwordRetrievers;
        public ConfigurationServiceCredentialProvider(string coreConfigurationServerHost, int port = 80, ILogger logger = null)
        {
            ConfigurationServerHost = coreConfigurationServerHost;
            Logger = logger ?? Log.Default;
            _passwordRetrievers = new Dictionary<SettingSource, Func<string>>()
            {
                { SettingSource.Invalid, ()=> throw new InvalidOperationException() },
                { SettingSource.DefaultConfigurationFile, ()=> throw new InvalidOperationException("Default configuration file is not supported by this ICredentialProvider, use DefaultConfigurationCredentialProvider instead.") },
                //{ SettingSource.CommonSetting, ()=> ConfigurationService.GetCommonConfiguration()["Password"]}
            };
        }
        //CommonSetting,
        //MachineSetting,
        //ApplicationSetting
        protected string ConfigurationServerHost { get; set; }
        protected int ConfigurationServerPort { get; set; }

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
            throw new NotImplementedException();
        }

        public string GetUserName()
        {
            throw new NotImplementedException();
        }
    }
}
