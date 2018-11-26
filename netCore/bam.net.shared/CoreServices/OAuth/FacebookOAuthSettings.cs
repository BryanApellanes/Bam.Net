using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.OAuth
{
    public class FacebookOAuthSettings: OAuthProviderSettings
    {
        public FacebookOAuthSettings(string clientId, string clientSecret, string version = "v2.10") : base(clientId, clientSecret)
        {
            ProviderName = "facebook";
            AuthorizationEndpointFormat = "https://www.facebook.com/{Version}/dialog/oauth?client_id={ClientId}&redirect_uri={AuthCallbackUrl}&state={State}";
            AuthorizationCallbackEndpointFormat = "https://graph.facebook.com/{Version}/oauth/access_token?client_id={ClientId}&redirect_uri={TokenCallbackUrl}&client_secret={ClientSecret}&code={Code}&state={State}";
            Version = version;
        }
    }
}
