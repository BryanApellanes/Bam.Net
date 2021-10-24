using Bam.Net.CoreServices;
using Bam.Net.CoreServices.Auth;
using Bam.Net.CoreServices.Tests;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using Bam.Net.CommandLine;

namespace Bam.Net.Tests
{
    [Serializable]
    public class OAuthSettingsServiceUnitTests : CommandLineTool
    {
        [UnitTest]
        public void CanSaveSupportedOAuthProviders()
        {
            SupportedAuthProviders oauthProviders = new SupportedAuthProviders();
            oauthProviders.AddProvider(new AuthProviderInfo() { ProviderName = "bamapps", AuthorizationEndpoint = "testendpoint", ClientId = "testclientId", ClientSecret = "testsecret" });
            string jsonFile = ".\\oauthTest.json";

            oauthProviders.Save(jsonFile);

            string jsonContent = jsonFile.SafeReadFile();
            Expect.IsNotNull(jsonContent);
            OutLine(jsonContent);
        }

        [UnitTest]
        public void CanSetGetAndDeleteProviders()
        {
            ServiceRegistry registry = CoreServicesTestRegistry.GetServiceRegistry();
            AuthSettingsService svc = registry.Get<AuthSettingsService>();
            svc.HttpContext = registry.Get<IHttpContext>();
            Expect.AreEqual("CoreServicesTests", svc.ClientApplicationName);

            CoreServiceResponse<List<AuthClientSettings>> settingsResponse = svc.GetClientSettings();
            Expect.IsTrue(settingsResponse.Success, "Failed to get client settings");

            // this is technically clean up code
            Message.PrintLine("there are currently {0} oauthsettings", settingsResponse.TypedData().Count);
            foreach(AuthClientSettings setting in settingsResponse.Data)
            {
                CoreServiceResponse removeResponse = svc.RemoveProvider(setting.ProviderName);
                Expect.IsTrue(removeResponse.Success, "failed to delete test provider");
            }

            string testProvider = "testprovider";
            CoreServiceResponse<AuthClientSettings> response = svc.SetProvider(testProvider, "testclientid", "test-secret");
            Expect.IsTrue(response.Success, "failed to set test provider");

            CoreServiceResponse check = svc.RemoveProvider(testProvider);
            Expect.IsTrue(check.Success, "failed to remove test provider");
        }

        [UnitTest]
        public void GetClientAppShouldGetTheSameCuid()
        {            
            ServiceRegistry registry = CoreServicesTestRegistry.GetServiceRegistry();
            AuthSettingsService svc = registry.Get<AuthSettingsService>();
            svc.HttpContext = registry.Get<IHttpContext>();
            CoreServices.ApplicationRegistration.Data.Application check = svc.ApplicationRegistrationRepository.OneApplicationWhere(c => c.Name == svc.ClientApplicationName);
            if(check != null)
            {
                svc.ApplicationRegistrationRepository.Delete(check);
            }
            CoreServices.ApplicationRegistration.Data.Application toSave = new CoreServices.ApplicationRegistration.Data.Application { Name = svc.ClientApplicationName };
            svc.ApplicationRegistrationRepository.Save(toSave);

            CoreServices.ApplicationRegistration.Data.Application app = svc.GetClientApplicationOrDie();
            CoreServices.ApplicationRegistration.Data.Application app2 = svc.GetClientApplicationOrDie();
            Expect.AreEqual(app.Name, app2.Name, "Names didn't match");
            Expect.AreEqual(app.Cuid, app2.Cuid, "Cuids didn't match");
            Expect.AreEqual(app.Uuid, app2.Uuid, "Uuids didn't match");
        }
    }
}
