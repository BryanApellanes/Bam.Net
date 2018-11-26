using Bam.Net.CoreServices;
using Bam.Net.CoreServices.OAuth;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;

namespace Bam.Net.Tests
{
    [Serializable]
    public class OAuthSettingsServiceUnitTests : CommandLineTestInterface
    {
        [UnitTest]
        public void CanSaveSupportedOAuthProviders()
        {
            SupportedOAuthProviders oauthProviders = new SupportedOAuthProviders();
            oauthProviders.AddProvider(new OAuthProviderInfo() { ProviderName = "bamapps", AuthorizationEndpoint = "testendpoint", ClientId = "testclientId", ClientSecret = "testsecret" });
            string jsonFile = ".\\oauthTest.json";

            oauthProviders.Save(jsonFile);

            string jsonContent = jsonFile.SafeReadFile();
            Expect.IsNotNull(jsonContent);
            OutLine(jsonContent);
        }

        [UnitTest]
        public void CanSetGetAndDeleteProviders()
        {
            CoreServices.ApplicationRegistration.Data.Dao.Repository.ApplicationRegistrationRepository appRepo = new CoreServices.ApplicationRegistration.Data.Dao.Repository.ApplicationRegistrationRepository();
            appRepo.EnsureDaoAssemblyAndSchema();
            CoreServices.OAuth.Data.Dao.Repository.OAuthSettingsRepository oauthRepo = new CoreServices.OAuth.Data.Dao.Repository.OAuthSettingsRepository();
            oauthRepo.EnsureDaoAssemblyAndSchema();
            OAuthSettingsService svc = new OAuthSettingsService(oauthRepo, appRepo);

            CoreServiceResponse<List<OAuthClientSettings>> settingsResponse = svc.GetClientSettings();
            Expect.IsTrue(settingsResponse.Success, "Failed to get client settings");

            OutLineFormat("there are currently {0} oauthsettings", settingsResponse.TypedData().Count);
            foreach(OAuthClientSettings setting in settingsResponse.Data)
            {
                CoreServiceResponse removeResponse = svc.RemoveProvider(setting.ProviderName);
                Expect.IsTrue(removeResponse.Success, "failed to delete test provider");
            }

            string testProvider = "testprovider";
            CoreServiceResponse<OAuthClientSettings> response = svc.SetProvider(testProvider, "testclientid", "test-secret");
            Expect.IsTrue(response.Success, "failed to set test provider");

            CoreServiceResponse check = svc.RemoveProvider(testProvider);
            Expect.IsTrue(check.Success, "failed to remove test provider");
        }
    }
}
