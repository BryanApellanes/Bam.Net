using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing.Integration;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.UserAccounts;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.CoreServices.Services;
using System.IO;

namespace Bam.Net.CoreServices.Tests
{
    [IntegrationTestContainer]
    public class IntegrationTests: CommandLineTestInterface
    {
        [IntegrationTestSetup]
        public void Setup()
        {
            
        }

        [IntegrationTestCleanup]
        public void CleanUp()
        {
            
        }
        
        [ConsoleAction]
        [IntegrationTest]
        public void CoreUsermanagerClientProxyTest()
        {
            OutLineFormat("This test requires a gloo server to be running on port 9100 of the localhost", ConsoleColor.Yellow);
            ConsoleLogger logger = new ConsoleLogger() { AddDetails = false };
            logger.StartLoggingThread();
            // get proxies
            ProxyFactory factory = new ProxyFactory();
            CoreClient coreClient = new CoreClient("ThreeHeadz", "CoreServicesTestApp", "localhost", 9100, logger);

            CoreUserRegistryService userService = coreClient.UserRegistryService;
            Expect.IsNotNull(userService);
            Expect.AreSame(coreClient, userService.Property("ApiKeyResolver"));
            Expect.AreSame(coreClient, userService.Property("ClientApplicationNameProvider"));

            bool? initFailed = false;
            coreClient.InitializationFailed += (o, a) =>
            {
                initFailed = true;
            };
            coreClient.Initialized += (o, a) =>
            {
                initFailed = false;
            };
            coreClient.Initialize();
            Expect.IsTrue(initFailed.Value);
            Expect.AreEqual("You must be logged in to do that", coreClient.Message);

            // sign up
            string email = $"{8.RandomLetters()}@threeheadz.com";
            string userName = 5.RandomLetters();
            string passHash = "password".Sha1();
            SignUpResponse signupResponse = userService.SignUp(email, userName, passHash, false);
            Expect.IsTrue(signupResponse.Success, signupResponse.Message);
            if (!signupResponse.Success)
            {
                OutLineFormat("Message: {0}", signupResponse.Message);
            }
            else
            {
                OutLine(signupResponse.TryPropertiesToString(), ConsoleColor.Cyan);
            }
            LoginResponse loginResponse = userService.Login(userName, passHash);
            Expect.IsTrue(loginResponse.Success, "Unable to login to userService");

            string youSayIAm = userService.WhoAmI();
            Expect.AreEqual(userName, youSayIAm);

            loginResponse = coreClient.ApplicationRegistryService.Login(userName, passHash);
            Expect.IsTrue(loginResponse.Success, "Unable to login to application registry service");
            Expect.IsTrue(coreClient.Initialize(), coreClient.Message);
            Expect.IsFalse(initFailed.Value);
            Expect.IsTrue(File.Exists(coreClient.ApiKeyFilePath));
            Pass("No exceptions were thrown and all assertions passed");
        }
    }
}
