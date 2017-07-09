using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.CoreServices.OAuth.Data;
using Bam.Net.CoreServices.OAuth.Data.Dao.Repository;

namespace Bam.Net.CoreServices
{
    [Proxy("oauthSettings")]
    [ApiKeyRequired]
    public class CoreOAuthSettingsService : CoreProxyableService
    {
        public CoreOAuthSettingsService(OAuthSettingsRepository oauthRepo)
        {
            OAuthSettingsRepository = oauthRepo;
        }
        public OAuthSettingsRepository OAuthSettingsRepository { get; set; }

        [RoleRequired("/", "Admin")]
        public string[] GetProviders()
        {
            throw new NotImplementedException();
        }

        [RoleRequired("/", "Admin")]
        public bool SetProviders(string[] providers)
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
