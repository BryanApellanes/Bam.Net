using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.CoreServices.OAuth.Data;
using Bam.Net.CoreServices.OAuth.Data.Dao.Repository;
using Bam.Net.CoreServices.OAuth;

namespace Bam.Net.CoreServices
{
    [Proxy("oauthSettingsSvc")]
    [ApiKeyRequired]
    public class CoreOAuthSettingsService : CoreProxyableService
    {
        public CoreOAuthSettingsService(OAuthSettingsRepository oauthRepo)
        {
            OAuthSettingsRepository = oauthRepo;
        }
        public OAuthSettingsRepository OAuthSettingsRepository { get; set; }

        [RoleRequired("/", "Admin")]
        public OAuthClientSettings[] GetClientSettings()
        {
            throw new NotImplementedException();
        }

        [RoleRequired("/", "Admin")]
        public bool SetProviders(OAuthClientSettings[] providers)
        {
            throw new NotImplementedException();
        }

        [RoleRequired("/", "Admin")]
        public bool AddProvider(OAuthSettingsData settings)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            CoreOAuthSettingsService clone = new CoreOAuthSettingsService(OAuthSettingsRepository);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
