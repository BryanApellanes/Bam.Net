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
using Bam.Net.ServiceProxy;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;

namespace Bam.Net.CoreServices
{
    [Proxy("oauthSettingsSvc")]    
    [ApiKeyRequired]
    [Authenticated]
    public class OAuthSettingsService : ApplicationProxyableService
    {
        protected OAuthSettingsService() { }

        public OAuthSettingsService(OAuthSettingsRepository oauthRepo, ApplicationRegistrationRepository applicationRegistrationRepo)
        {
            OAuthSettingsRepository = oauthRepo;
            ApplicationRegistrationRepository = applicationRegistrationRepo;
        }

        public OAuthSettingsRepository OAuthSettingsRepository { get; set; }

        [RoleRequired("/", "Admin")]
        public virtual CoreServiceResponse<List<OAuthClientSettings>> GetClientSettings(bool includeSecret = false)
        {
            try
            {
                ApplicationRegistration.Data.Application app = ClientApplication;
                return new CoreServiceResponse<List<OAuthClientSettings>>
                    (
                        OAuthSettingsRepository
                            .OAuthProviderSettingsDatasWhere(c => c.ApplicationIdentifier == app.Cuid && c.ApplicationName == app.Name)
                            .Select(sd => OAuthProviderSettings.FromData(sd))
                            .Select(os =>
                            {
                                OAuthClientSettings setting = os.CopyAs<OAuthClientSettings>();
                                if (!includeSecret)
                                {
                                    setting.ClientSecret = string.Empty;
                                }
                                return setting;
                            })
                            .ToList()
                    )
                {
                    Success = true
                };
            }
            catch(Exception ex)
            {
                return new CoreServiceResponse<List<OAuthClientSettings>> { Success = false, Message = ex.Message };
            }
        }

        [RoleRequired("/", "Admin")]
        public virtual CoreServiceResponse<OAuthClientSettings> SetProvider(string providerName, string clientId, string clientSecret)
        {
            try
            {
                ApplicationRegistration.Data.Application app = GetClientApplicationOrDie();

                OAuthProviderSettingsData data = new OAuthProviderSettingsData()
                {
                    ApplicationName = app.Name,
                    ApplicationIdentifier = app.Cuid,
                    ProviderName = providerName,
                    ClientId = clientId,
                    ClientSecret = clientSecret
                };
                OAuthClientSettings settings = OAuthSettingsRepository.Save(data).CopyAs<OAuthClientSettings>();
                return new CoreServiceResponse<OAuthClientSettings> { Success = true, Data = settings };
            }
            catch (Exception ex)
            {
                return new CoreServiceResponse<OAuthClientSettings> { Success = false, Message = ex.Message };
            }
        }

        [RoleRequired("/", "Admin")]
        public virtual CoreServiceResponse RemoveProvider(string providerName)
        {
            try
            {
                ApplicationRegistration.Data.Application app = GetClientApplicationOrDie();
                OAuthProviderSettingsData data = OAuthSettingsRepository.OneOAuthProviderSettingsDataWhere(c => c.ApplicationIdentifier == app.Cuid && c.ApplicationName == app.Name && c.ProviderName == providerName);
                if(data != null)
                {
                    bool success = OAuthSettingsRepository.Delete(data);
                    if (!success)
                    {
                        throw OAuthSettingsRepository.LastException;
                    }
                    return new CoreServiceResponse { Success = success };
                }
                throw new InvalidOperationException($"OAuthSettings not found: AppId={app.Cuid}, AppName={app.Name}, Provider={providerName}");
            }
            catch (Exception ex)
            {
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
        }

        public override object Clone()
        {
            OAuthSettingsService clone = new OAuthSettingsService(OAuthSettingsRepository, ApplicationRegistrationRepository);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
