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
            ConsoleLogger logger = new ConsoleLogger() { AddDetails = false };
            logger.StartLoggingThread();
            // get proxies
            ProxyFactory factory = new ProxyFactory();

            CoreUserManagerService userService = factory.GetProxy<CoreUserManagerService>("localhost", 9100);
            Expect.IsNotNull(userService);

            CoreClient coreClient = new CoreClient("ThreeHeadz", "CoreServicesTestApp", "localhost", 9100, logger);
            bool? initFailed = false;
            coreClient.InitializationFailed += (o, a) =>
            {
                initFailed = true;
            };
            coreClient.Initialize();
            Expect.IsTrue(initFailed.Value);
            Expect.AreEqual("You must be logged in to do that", coreClient.Message);
            userService.Property("ApiKeyResolver", coreClient);
            userService.Property("ClientApplicationNameProvider", coreClient);
            OutLineFormat(userService.GetType().FullName);

            // sign up
            string email = $"{8.RandomLetters()}@threeheadz.com";
            string userName = 5.RandomLetters();
            string passHash = "password".Sha1();
            SignUpResponse signupResponse = userService.SignUp(email, userName, passHash, false);
            Expect.IsTrue(signupResponse.Success, signupResponse.Message);
            if (!signupResponse.Success)
            {
                OutLineFormat("Message: {0}", signupResponse.Message);
            }else
            {
                OutLine(signupResponse.TryPropertiesToString(), ConsoleColor.Cyan);
            }
            LoginResponse loginResponse = userService.Login(userName, passHash);
            Expect.IsTrue(loginResponse.Success, "Unable to login");
        }

        //[IntegrationTest]
        //public void CanSignup()
        //{
        //    "gloo "
        //}
    }
}
