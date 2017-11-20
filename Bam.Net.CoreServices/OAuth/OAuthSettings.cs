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
    public class OAuthSettings: OAuthSettingsData
    {
        static Dictionary<string, Type> _settingsTypeMap;
        static OAuthSettings()
        {
            _settingsTypeMap = new Dictionary<string, Type>
            {
                { "bamapps.net", typeof(OAuthSettings) },
                { "facebook", typeof(FacebookOAuthSettings) }
            };
        }

        public OAuthSettings() : base()
        {
            ClientId = "1282272511809831";
            AuthorizationUrlFormat = "https://bamapps.net/oauth/authorize?clientId={ClientId}&callbackUrl={CallbackUrl}&code={Code}&state={State}";
            AuthorizationCallbackUrlFormat = "https://bamapps.net/oauth/setaccesstoken?clientId={ClientId}&callbackUrl={TokenCallbackUrl}&clientSecret={ClientSecret}&code={Code}&state={State}";
        }

        public OAuthSettings(string clientId, string clientSecret) : this()
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public static OAuthSettings FromData(OAuthSettingsData data)
        {
            OAuthSettings settings = null;
            if (_settingsTypeMap.ContainsKey(data.ProviderName))
            {
                settings = _settingsTypeMap[data.ProviderName].Construct<OAuthSettings>();
                settings.CopyProperties(data);
            }
            return settings;
        }

        /// <summary>
        /// The providers user login endpoint 
        /// </summary>
        public string AuthorizationUrl
        {
            get
            {
                return AuthorizationUrlFormat.NamedFormat(this);
            }
        }

        public string AuthorizationCallbackUrl
        {
            get
            {
                return AuthorizationCallbackUrlFormat.NamedFormat(this);
            }
        }

        public string Version { get; set; }
        

        public string AccessToken { get; set; }

        public OAuthSettings WithAccessToken(string accessToken)
        {
            OAuthSettings result = GetType().Construct<OAuthSettings>();
            result.CopyProperties(this);
            result.AccessToken = accessToken;
            return result;
        }
    }
}
