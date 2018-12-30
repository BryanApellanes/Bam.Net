/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy.Secure
{
    public class StaticApiKeyProvider: ApiKeyProvider
    {
        public StaticApiKeyProvider(string clientId, string apiKey)
        {
            this.ClientId = clientId;
            this.ApiKey = apiKey;
        }

        protected string ClientId { get; set; }
        protected string ApiKey { get; set; }

        public override string GetApplicationClientId(IApplicationNameProvider nameProvider)
        {
            return ClientId;
        }

        public override string GetApplicationApiKey(string applicationClientId, int index)
        {
            return ApiKey;
        }
    }
}
