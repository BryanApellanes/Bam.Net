using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.CoreServices.OAuth.Data;

namespace Bam.Net.CoreServices.OAuth
{
    public class OAuthProviderSettings: OAuthProviderSettingsData
    {
        static Dictionary<string, Type> _settingsTypeMap;
        static OAuthProviderSettings()
        {
            _settingsTypeMap = new Dictionary<string, Type>
            {
                { "bamapps.net", typeof(OAuthProviderSettings) },
                { "facebook", typeof(FacebookOAuthSettings) }
            };
        }

        public OAuthProviderSettings() : base()
        {
            ClientId = "1282272511809831";
            AuthorizationEndpointFormat = "https://bamapps.net/oauth/authorize?clientId={ClientId}&callbackUrl={CallbackUrl}&code={Code}&state={State}";
            AuthorizationCallbackEndpointFormat = "https://bamapps.net/oauth/setaccesstoken?clientId={ClientId}&callbackUrl={TokenCallbackUrl}&clientSecret={ClientSecret}&code={Code}&state={State}";
        }

        public OAuthProviderSettings(string clientId, string clientSecret) : this()
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public static OAuthProviderSettings FromData(OAuthProviderSettingsData data)
        {
            OAuthProviderSettings settings = null;
            if (_settingsTypeMap.ContainsKey(data.ProviderName))
            {
                settings = _settingsTypeMap[data.ProviderName].Construct<OAuthProviderSettings>();
                settings.CopyProperties(data);
            }
            return settings;
        }

        /// <summary>
        /// The providers user login endpoint 
        /// </summary>
        public string GetAuthorizationUrl()
        {
            return AuthorizationEndpointFormat.NamedFormat(this);
        }

        public string GetAuthorizationCallbackUrl()
        {
            return AuthorizationCallbackEndpointFormat.NamedFormat(this);
        }

        public string Version { get; set; }        

        public string AccessToken { get; set; }

        public OAuthProviderSettings WithAccessToken(string accessToken)
        {
            OAuthProviderSettings result = GetType().Construct<OAuthProviderSettings>();
            result.CopyProperties(this);
            result.AccessToken = accessToken;
            return result;
        }
    }
}
