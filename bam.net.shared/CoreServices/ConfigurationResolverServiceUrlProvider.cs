using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.CoreServices
{
    public class ConfigurationResolverServiceUrlProvider : IServiceUrlProvider
    {
        static ConfigurationResolverServiceUrlProvider()
        {
            Instance = new ConfigurationResolverServiceUrlProvider();
        }

        public ConfigurationResolverServiceUrlProvider(ConfigurationResolver configurationResolver)
        {
            ConfigurationResolver = configurationResolver;
        }

        public ConfigurationResolverServiceUrlProvider() : this(ConfigurationResolver.Current)
        { }

        public ConfigurationResolver ConfigurationResolver { get; set; }

        public string GetServiceUrl<T>()
        {
            return GetServiceUrl(typeof(T));
        }

        public string GetServiceUrl(Type type)
        {
            string serviceUrl = ConfigurationResolver[$"{type.Name}Url"];
            return serviceUrl.Or(Defaults.BaseUrl);
        }

        public static ConfigurationResolverServiceUrlProvider Instance
        {
            get;
        }
    }
}
