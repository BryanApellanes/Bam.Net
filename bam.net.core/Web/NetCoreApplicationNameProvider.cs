using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Web
{
    public class NetCoreApplicationNameProvider : IApplicationNameProvider
    {
        public NetCoreApplicationNameProvider(ConfigurationResolver configurationResolver)
        {
            ConfigurationResolver = configurationResolver;
        }

        public ConfigurationResolver ConfigurationResolver { get; set; }

        public string GetApplicationName()
        {
            return ConfigurationResolver["ApplicationName"];
        }
    }
}
