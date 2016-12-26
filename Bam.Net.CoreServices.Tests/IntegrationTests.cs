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
using Bam.Net.CoreServices.Data;
using Bam.Net.DaoRef;
using Bam.Net.CoreServices.ProtoBuf;
using Bam.Net.Data.Repositories;
using System.Reflection;
using Google.Protobuf;
using Bam.Net.CoreServices.Data.Daos.Repository;
using Bam.Net.UserAccounts.Data;

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
        public void ProtoFileGeneratorCanGenerateFiles()
        {
            ProtoFileGenerator protoFileGenerator = new ProtoFileGenerator(new TypeSchemaGenerator(), new InMemoryPropertyNumberer());
            protoFileGenerator.GenerateProtoFile(typeof(Left));
            OutLine(new DirectoryInfo(protoFileGenerator.OutputDirectory).FullName);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void DaoProtoFileGeneratorCanGenerateFiles()
        {
            ProtoFileGenerator protoFileGenerator = new DaoProtoFileGenerator(new TypeSchemaGenerator(), new InMemoryPropertyNumberer());
            protoFileGenerator.GenerateProtoFile(typeof(Left));
            OutLine(new DirectoryInfo(protoFileGenerator.OutputDirectory).FullName);
        }

        [ConsoleAction]
        public void ProtocolBufferAssemblyGeneratorCanGenerateCs()
        {
            ProtocolBuffersAssemblyGenerator generator = new ProtocolBuffersAssemblyGenerator();
            DirectoryInfo dir = generator.GenerateCsFiles(typeof(Left));
            OutLine(dir.FullName);
        }


        [ConsoleAction]
        [IntegrationTest]
        public void ProtocolBufferAssemblyGeneratorCanCreateAssembly()
        {
            string filePathFile = ".\\thefile.txt";
            if (File.Exists(filePathFile))
            {
                string theFilePath = File.ReadAllText(filePathFile);
                if (File.Exists(theFilePath))
                {
                    File.Delete(theFilePath);
                }
            }
            ProtocolBuffersAssemblyGenerator generator = new ProtocolBuffersAssemblyGenerator("TestProtoBuf.dll", typeof(Left));
            Assembly ass = generator.GetAssembly();
            Expect.IsNotNull(ass);
            string filePath = ass.GetFileInfo().FullName;
            OutLine(filePath);
            Expect.IsTrue(File.Exists(filePath));
            filePath.SafeWriteToFile(filePathFile, true);
        }

        [ConsoleAction]
        public void WhoAmITest()
        {
            OutLineFormat("This test requires a gloo server to be running on port 9100 of the localhost", ConsoleColor.Yellow);
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            const string server  = "localhost";
            const int port = 9100;
            logger.StartLoggingThread();
            CoreRegistryRepository repo = CoreRegistry.GetGlooRegistry().Get<CoreRegistryRepository>();
            CoreClient client = new CoreClient("TestOrg", "TestApp", server, port, logger);
            client.LocalCoreRegistryRepository = repo;
            ServiceResponse registrationResponse = client.Register();
            Machine current = Machine.ClientOf(client.LocalCoreRegistryRepository, server, port);

            ServiceResponse response = client.Connect();

            string whoAmI = client.UserRegistryService.WhoAmI();
            Expect.AreEqual(current.ToString(), whoAmI);

            whoAmI = client.ApplicationRegistryService.WhoAmI();
            Expect.AreEqual(current.ToString(), whoAmI);

            whoAmI = client.ConfigurationService.WhoAmI();
            Expect.AreEqual(current.ToString(), whoAmI);

            whoAmI = client.EventHubService.WhoAmI();
            Expect.AreEqual(current.ToString(), whoAmI);

            whoAmI = client.LoggerService.WhoAmI();
            Expect.AreEqual(current.ToString(), whoAmI);

            whoAmI = client.TranslationService.WhoAmI();
            Expect.AreEqual(current.ToString(), whoAmI);

            whoAmI = client.DiagnosticService.WhoAmI();
            Expect.AreEqual(current.ToString(), whoAmI);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanSaveMachineInCoreRegistryRepo()
        {
            CoreRegistryRepository repo = CoreRegistry.GetGlooRegistry().Get<CoreRegistryRepository>();
            Machine test = Machine.ClientOf(repo, "test", 80);
            test = repo.Save(test);
            Expect.IsTrue(test.Id > 0);
            Machine test2 = Machine.ClientOf(repo, "test", 90);
            test2 = repo.Save(test2);
            Expect.IsTrue(test2.Id > 0);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CoreClientCanRegisterAndConnectClient()
        {
            OutLineFormat("This test requires a gloo server to be running on port 9100 of the localhost", ConsoleColor.Yellow);
            CoreRegistryRepository repo = CoreRegistry.GetGlooRegistry().Get<CoreRegistryRepository>();
            ConsoleLogger logger = new ConsoleLogger() { AddDetails = false };
            logger.StartLoggingThread();
            CoreClient client = new CoreClient("ThreeHeadz", "CoreServicesTestApp", "localhost", 9100, logger);
            client.LocalCoreRegistryRepository = repo;
            ServiceResponse registrationResponse = client.Register();
            Expect.IsTrue(registrationResponse.Success, registrationResponse.Message);
            ServiceResponse response = client.Connect();
            List<ServiceResponse> responses = response.Data.FromJObject<List<ServiceResponse>>();
            Expect.IsTrue(response.Success, string.Join("\r\n", responses.Select(r => r.Message).ToArray()));
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
