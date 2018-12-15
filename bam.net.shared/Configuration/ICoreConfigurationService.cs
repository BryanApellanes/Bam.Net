using System.Collections.Generic;
using Bam.Net.Configuration;

namespace Bam.Net.CoreServices
{
    public interface ICoreConfigurationService: IConfigurationService
    {
        Dictionary<string, string> GetApplicationConfiguration(string applicationName = null, string configurationName = null);
        Dictionary<string, string> GetCommonConfiguration();
        ApplicationConfiguration GetConfiguration(string applicationName = null, string machineName = null, string configurationName = null);
        Dictionary<string, string> GetMachineConfiguration(string machineName, string configurationName = null);
        void SetApplicationConfiguration(Dictionary<string, string> settings, string applicationName = null, string configurationName = null);
        void SetCommonConfiguration(Dictionary<string, string> settings);
        void SetMachineConfiguration(string machineName, Dictionary<string, string> settings, string configurationName = null);
    }
}