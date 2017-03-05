using System.Collections.Generic;

namespace Bam.Net.Configuration
{
    public interface IConfigurationService
    {
        Dictionary<string, string> GetConfiguration(string applicationName, string configurationName = "");
        void SetConfiguration(string applicationName, string configurationName, Dictionary<string, string> configuration);
    }
}