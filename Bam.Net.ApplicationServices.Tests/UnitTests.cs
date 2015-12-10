/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.ServiceProxy.Tests;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.ServiceProxy;
using System.Reflection;
using System.IO;

namespace Bam.Net.ApplicationServices.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
        interface ITestInterface
        {
            void Go();
            void GoAgain();
        }

        [UnitTest]
        public void TestGetInterfaceMethods()
        {
            string methods = string.Join(",", typeof(ITestInterface).GetMethods().Select(m => m.Name));
            string spsMethods = string.Join(",", ServiceProxySystem.GetProxiedMethods(typeof(ITestInterface)).Select(m => m.Name));
            OutLine(methods);
            Expect.AreEqual(methods, spsMethods);
        }
        
        [UnitTest]
        public void ShouldBeAbleToRenderService()
        {
            ServiceModel model = new ServiceModel(typeof(Echo));
            OutLine(model.Render(), ConsoleColor.Cyan);
        }

        [UnitTest]
        public void ShouldBeAbleToGetGeneratedAssembly()
        {
            ServiceFactory serviceFactory = new ServiceFactory();
            Assembly assembly = serviceFactory.GetAssembly<EncryptedEcho>();
            OutLine(assembly.FullName);
        }
 
        [UnitTest]
        public void ShouldBeAbleToInstanciateProxy()
        {
            ServiceFactory serviceFactory = new ServiceFactory();
            EncryptedEcho echoProvider = serviceFactory.GetService<EncryptedEcho>();
            Type echoType = echoProvider.GetType();
            Expect.IsTrue(echoType.Name.Contains("Proxy"), "Expected 'Proxy' to be in the type name: {0}"._Format(echoType.Name));
        }

        [UnitTest]
        public void ShouldBeAbleToSetApiKeyResolver()
        {
            ServiceFactory serviceFactory = new ServiceFactory(".\\workspace_".RandomLetters(4), logger);
            EncryptedEcho echo = serviceFactory.GetService<EncryptedEcho>();
            ApiKeyResolver resolver = new ApiKeyResolver();
            echo.Property("ApiKeyResolver", resolver);
            ApiKeyResolver got = echo.Property<ApiKeyResolver>("ApiKeyResolver");
            Expect.AreSame(resolver, got);
        }

        [UnitTest]
        public void ShouldBeAbleToUseGeneratedClient()
        {
            BamServer server;
            SecureServiceProxyClient<EncryptedEcho> sspc;
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetEncryptedEchoClient(out server, out sspc);
            ConsoleLogger logger = GetTestConsoleLogger();
            ServiceFactory serviceFactory = new ServiceFactory(".\\workspace_".RandomLetters(4), logger);
            try
            {                
                IResponder responder = null;
                int responseCount = 0;
                server.Responded += (srvr, resp, req) =>
                {
                    OutLineFormat("Responded to url: {0}", ConsoleColor.DarkGreen, req.Url.ToString());
                    responder = resp;
                    responseCount++;
                };
                EncryptedEcho echo = serviceFactory.GetService<EncryptedEcho>("localhost");
                string value = "A random string: ".RandomLetters(8);
                string response = echo.Send(value);
                Expect.IsNotNull(response, "response was null");
                Expect.AreEqual(value, response);
                Expect.IsTrue(responseCount > 0); // download, init session, set key, invoke
                Expect.IsObjectOfType<ServiceProxyResponder>(responder);
            }
            finally
            {
                server.Stop();
            }
        }

        private static ConsoleLogger GetTestConsoleLogger()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.StartLoggingThread();
            return logger;
        }
    }
}
