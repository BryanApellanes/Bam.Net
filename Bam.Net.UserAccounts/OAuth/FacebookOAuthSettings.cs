using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts.OAuth
{
    public class FacebookOAuthSettings: OAuthSettings
    {
        public FacebookOAuthSettings(string clientId, string clientSecret) : base(clientId, clientSecret)
        {
            ProviderName = "facebook";
            AuthorizationEndpointFormat = "https://www.facebook.com/v2.8/dialog/oauth?client_id={ClientId}&redirect_uri={AuthCallbackUrl}&state={State}";
            TokenEndpointFormat = "https://graph.facebook.com/v2.8/oauth/access_token?client_id={ClientId}&redirect_uri={TokenCallbackUrl}&client_secret={ClientSecret}&code={Code}&state={State}";
        }
    }
}
