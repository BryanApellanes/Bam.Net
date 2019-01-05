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

        public NameValueCollection DefaultConfiguration { get; set; }

        public string this[string key]
        {
            get
            {
                return DefaultConfiguration[key];
            }       
        }

        // TODO: add configurationService
    }
}
