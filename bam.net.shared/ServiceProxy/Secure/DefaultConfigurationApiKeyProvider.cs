/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;

namespace Bam.Net.ServiceProxy.Secure
{
    public class DefaultConfigurationApiKeyProvider: ApiKeyProvider
    {
        static DefaultConfigurationApiKeyProvider _defaultProvider;
        static object _sync = new object();
        public static DefaultConfigurationApiKeyProvider Instance
        {
            get
            {
                return _sync.DoubleCheckLock(ref _defaultProvider, () => new DefaultConfigurationApiKeyProvider());
            }
        }

        public override string GetApplicationClientId(IApplicationNameProvider nameProvider)
        {
            return DefaultConfiguration.GetAppSetting("ClientId", true);
        }

        public override string GetApplicationApiKey(string applicationClientId, int index)
        {
            return DefaultConfiguration.GetAppSetting("ApiKey", true);
        }
    }
}
