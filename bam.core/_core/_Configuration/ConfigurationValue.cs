using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Configuration
{
    /// <summary>
    /// Represents the settings used to resolve a configuration value and 
    /// the resulting configuration value.
    /// </summary>
    public class ConfigurationValue
    {
        public static implicit operator string(ConfigurationValue configurationValue)
        {
            return configurationValue.Value;
        }

        public ConfigurationValue(string value)
        {
            this.Value = value;
        }

        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the default value specified when this ConfigurationValue was resolved.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the callConfigService argument when this ConfigurationValue was resolved.
        /// </summary>
        public bool CallConfigService { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the source this ConfigurationValue was resolved from.
        /// </summary>
        public ConfigurationSources ConfigurationSource { get; set; }
    }
}
