using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.CoreServices
{
    [Serializable]
    public class DefaultConfigurationServiceUrlProvider : IServiceUrlProvider
    {
        static DefaultConfigurationServiceUrlProvider()
        {
            Instance = new DefaultConfigurationServiceUrlProvider();
        }

        public string GetServiceUrl<T>()
        {
            return GetServiceUrl(typeof(T));
        }

        public string GetServiceUrl(Type type)
        {
            return DefaultConfiguration.GetAppSetting($"{type.Name}Url", Defaults.BaseUrl);
        }

        public static DefaultConfigurationServiceUrlProvider Instance
        {
            get;
        }
    }
}
