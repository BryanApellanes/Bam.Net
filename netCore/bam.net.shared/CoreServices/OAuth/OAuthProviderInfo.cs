using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.CoreServices.OAuth
{
    public class OAuthProviderInfo
    {
        public string ProviderName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthorizationEndpoint { get; set; }
        public override int GetHashCode()
        {
            return $"{nameof(OAuthProviderInfo)}.{ProviderName}".GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is OAuthProviderInfo provider)
            {
                return provider.ProviderName.Equals(ProviderName);
            }
            return false;
        }
    }
}
