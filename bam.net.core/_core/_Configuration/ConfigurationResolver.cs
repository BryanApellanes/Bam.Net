using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;

namespace Bam.Net.Configuration
{
    public partial class ConfigurationResolver
    {
        public ConfigurationResolver()
        {
            DefaultConfiguration = ConfigurationManager.AppSettings;
        }

        public ConfigurationResolver(IConfiguration configuration)
        {
            NetCoreConfiguration = configuration;
            DefaultConfiguration = ConfigurationManager.AppSettings;
        }

        public IConfiguration NetCoreConfiguration { get; set; }
        public NameValueCollection DefaultConfiguration { get; set; }

        public string this[string key]
        {
            get
            {
                string value = NetCoreConfiguration?[key];
                if (string.IsNullOrEmpty(value))
                {
                    value = DefaultConfiguration[key];
                }
                return value;
            }       
        }

        public static void Startup(IConfiguration configuration)
        {
            Current = new ConfigurationResolver(configuration);
        }

        // TODO: add configurationService
    }
}
