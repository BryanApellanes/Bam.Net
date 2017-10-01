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
        public CoreServiceResponse GetClientSettings()
        {
            try
            {
                ApplicationRegistration.Application app = ApplicationRegistrationRepository.GetOneApplicationWhere(c => c.Name == ApplicationName);
                return new CoreServiceResponse(OAuthSettingsRepository
                    .OAuthSettingsDatasWhere(c => c.ApplicationIdentifier == app.Cuid && c.ApplicationName == app.Name)
                    .Select(sd => OAuthSettings.FromData(sd))
                    .Select(os => os.CopyAs<OAuthClientSettings>())
                    .ToArray());
            }
            catch(Exception ex)
            {
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
        }

        [RoleRequired("/", "Admin")]
        public CoreServiceResponse AddProvider(string providerName, string clientId, string clientSecret)
        {
            try
            {
                ApplicationRegistration.Application app = ApplicationRegistrationRepository.GetOneApplicationWhere(c => c.Name == ApplicationName);
                OAuthSettingsData data = new OAuthSettingsData()
                {
                    ApplicationName = app.Name,
                    ApplicationIdentifier = app.Cuid,
                    ProviderName = providerName,
                    ClientId = clientId,
                    ClientSecret = clientSecret
                };
                OAuthClientSettings settings = ApplicationRegistrationRepository.Save(data).CopyAs<OAuthClientSettings>();
                return new CoreServiceResponse { Success = true, Data = settings };
            }
            catch (Exception ex)
            {
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
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
