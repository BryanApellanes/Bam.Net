using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.OAuth
{
    public class OAuthClientSettings
    {
        public OAuthClientSettings() { }
        public string ProviderName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthorizationEndpoint { get; set; }
    }
}
