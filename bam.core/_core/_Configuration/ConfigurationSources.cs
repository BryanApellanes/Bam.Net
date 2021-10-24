using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Configuration
{
    public enum ConfigurationSources
    {
        NotFound,
        DefaultValue,
        NetCoreConfiguration,
        DefaultConfiguration,
        ConfigAppSettings, // Config.Current.AppSettings
        BamEnvironmentVariable,
        ConfigurationService
    }
}
