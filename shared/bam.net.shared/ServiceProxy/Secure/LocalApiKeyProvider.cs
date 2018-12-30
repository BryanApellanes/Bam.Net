/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;

namespace Bam.Net.ServiceProxy.Secure
{
    public class LocalApiKeyProvider : ApiKeyProvider
    {
        public override string GetApplicationClientId(IApplicationNameProvider nameProvider)
        {
            return LocalApiKeyManager.Default.GetClientId(nameProvider);
        }

        public override string GetApplicationApiKey(string applicationClientId, int index)
        {
            return LocalApiKeyManager.Default.GetApplicationApiKey(applicationClientId, index);
        }
    }
}
