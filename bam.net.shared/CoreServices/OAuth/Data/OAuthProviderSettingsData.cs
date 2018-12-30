using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.OAuth.Data
{
    public class OAuthProviderSettingsData: AuditRepoData
    {
        public OAuthProviderSettingsData()
        {
            ProviderName = "bamapps.net";
        }
        public string ApplicationName { get; set; }
        public string ApplicationIdentifier { get; set; }
        
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
        /// The url of the provider's authorization endpoint;
        /// this is typically a login page
        /// </summary>
        public string AuthorizationUrl { get; set; }

        /// <summary>
        /// The url of our app that will receive the
        /// authenticated redirect
        /// </summary>
        public string AuthorizationCallbackEndpoint { get; set; }
        

        public string AuthorizationEndpointFormat
        {
            get;
            set;
        }

        public string AuthorizationCallbackEndpointFormat
        {
            get;
            set;
        }

    }
}
