using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts.OAuth
{
    public class OAuthProviders
    {
        Dictionary<string, OAuthSettings> _oauthSettings;
        public OAuthProviders()
        {
            _oauthSettings = new Dictionary<string, OAuthSettings>();
        }

        public void AddProvider(string name, OAuthSettings settings)
        {
            _oauthSettings.AddMissing(name, settings);
        }
    }
}
