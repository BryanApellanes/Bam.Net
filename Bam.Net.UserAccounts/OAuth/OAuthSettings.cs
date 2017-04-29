using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Logging;

namespace Bam.Net.UserAccounts.OAuth
{
    public class OAuthSettings
    {
        public OAuthSettings()
        {
            ClientId = "1282272511809831";
            AuthorizationEndpointFormat = "https://bamapps.net/oauth/authorize?clientId={ClientId}&callbackUrl={CallbackUrl}&code={Code}&state={State}";
            TokenEndpointFormat = "https://bamapps.net/oauth/accesstoken?clientId={ClientId}&callbackUrl={TokenCallbackUrl}&clientSecret={ClientSecret}&code={Code}&state={State}";
            TokenCallbackUrl = "https://{ApplicationName}.bamapps.net/oauth/settoken?accesstoken={AccessToken}";
        }

        public OAuthSettings(string clientId, string clientSecret, IApplicationNameProvider appNameProvider = null) : this()
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            ApplicationNameProvider = appNameProvider ?? DefaultConfigurationApplicationNameProvider.Instance;
        }
        public IApplicationNameProvider ApplicationNameProvider
        {
            get;
            set;
        }
        string _appName;
        public string ApplicationName
        {
            get
            {
                if(string.IsNullOrEmpty(_appName) || _appName.Equals(ApplicationDiagnosticInfo.Unknown))
                {
                    _appName = ApplicationNameProvider?.GetApplicationName();
                    if (string.IsNullOrEmpty(_appName) || _appName.Equals(ApplicationDiagnosticInfo.Unknown))
                    {
                        Log.Warn("Unable to determine application name");
                    }
                }
                return _appName;
            }
        }

        public string ProviderName { get; set; }

        public string State { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// The identifier used by the provider's system
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The secret assigned by the provider's system
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// The url of our app that will receive the
        /// authenticated redirect
        /// </summary>
        public string AuthCallbackUrl { get; set; }

        /// <summary>
        /// The url of our app that will receive the
        /// token redirect
        /// </summary>
        public string TokenCallbackUrl { get; set; }

        public string AuthorizationEndpointFormat
        {
            get;
            set;
        }
        /// <summary>
        /// The providers user login endpoint 
        /// </summary>
        public string AuthorizationEndpoint
        {
            get
            {
                return AuthorizationEndpointFormat.NamedFormat(this);
            }
        }

        public string TokenEndpointFormat
        {
            get;
            set;
        }
        /// <summary>
        /// The providers endpoint used to acquire
        /// an access token for the current user
        /// </summary>
        public string TokenEndpoint
        {
            get
            {
                return TokenEndpointFormat.NamedFormat(this);
            }
        }

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
