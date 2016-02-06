/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
{
    public class RemoteApiKeyProvider: ApiKeyProvider
    {
        public RemoteApiKeyProvider()
        {
            this.Behavior = RemoteApiKeyProviderBehavior.AddNewKey;
        }

        /// <summary>
        /// Gets or sets the behavior for the current ApiKeyProvider
        /// which determines what will be done if a request is made for
        /// an api key and the specified applicationClientId is not 
        /// found
        /// </summary>
        public RemoteApiKeyProviderBehavior Behavior
        {
            get;
            set;
        }

        public override string GetApplicationClientId(IApplicationNameProvider nameProvider)
        {
            return ApiKeyManager.Default.GetClientId(nameProvider);
        }

        public override string GetApplicationApiKey(string applicationClientId, int index)
        {
            ApiKeyCollection keys = ApiKey.Where(c => c.ClientId == applicationClientId, Order.By<ApiKeyColumns>(c => c.CreatedAt, SortOrder.Descending));
            ApiKey key = null;
            string result = string.Empty;
            if (keys.Count == 0)
            {
                switch (Behavior)
                {
                    case RemoteApiKeyProviderBehavior.Invalid:
                        throw new InvalidOperationException("Invalid Behavior specified");
                    case RemoteApiKeyProviderBehavior.AddNewKey:
                        key = AddNewApiKey(applicationClientId);
                        break;
                    case RemoteApiKeyProviderBehavior.ReturnEmptyString:
                        key = ApiKey.Blank;
                        break;
                    case RemoteApiKeyProviderBehavior.Throw:
                        throw new ApiKeyNotFoundException(applicationClientId);
                }
            }
            else if (keys.Count == 1)
            {
                key = keys[0];
            }
            else if (keys.Count > 0 && index <= keys.Count - 1)
            {
                key = keys[index];
            }

            if (key != null)
            {
                result = key.SharedSecret;
            }

            return result;
        }

        private ApiKey AddNewApiKey(string applicationClientId)
        {
            throw new NotImplementedException("This should make a ServiceProxy call to the ApiKeyManager");
            //return ApiKeyManager.Default.AddKey(applicationClientId);
        }

    }
}
