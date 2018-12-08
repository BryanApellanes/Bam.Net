using Bam.Net.CommandLine;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.ServiceProxy.Tests;
using Bam.Net.Services.Clients;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Reflection;

namespace Bam.Net.CoreServices.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
        [UnitTest]
        public void ShouldBeAbleToSaveGeneratedAssemblies()
        {
            CoreClient client = new CoreClient();
            
            client.SaveProxyAssemblies();
        }

        [UnitTest]
        public void ShouldBeAbleToSaveGeneratedSource()
        {
            CoreClient client = new CoreClient();
            client.SaveProxySource();
        }

        [UnitTest]
        public void ShouldBeAbleToGetGeneratedAssembly()
        {
            ProxyFactory serviceFactory = new ProxyFactory();
            Assembly assembly = serviceFactory.GetAssembly<EncryptedEcho>();
            OutLine(assembly.FullName);
        }

        [UnitTest]
        public void ShouldBeAbleToInstanciateProxy()
        {
            ProxyFactory serviceFactory = new ProxyFactory();
            EncryptedEcho echoProvider = serviceFactory.GetProxy<EncryptedEcho>();
            Type echoType = echoProvider.GetType();
            Expect.IsTrue(echoType.Name.Contains("Proxy"), "Expected 'Proxy' to be in the type name: {0}"._Format(echoType.Name));
        }

        [UnitTest]
        public void ShouldBeAbleToSetApiKeyResolver()
        {
            ProxyFactory serviceFactory = new ProxyFactory(".\\workspace_".RandomLetters(4), Logger);
            EncryptedEcho echo = serviceFactory.GetProxy<EncryptedEcho>();
            ApiKeyResolver resolver = new ApiKeyResolver();
            echo.Property("ApiKeyResolver", resolver);
            ApiKeyResolver got = echo.Property<ApiKeyResolver>("ApiKeyResolver");
            Expect.AreSame(resolver, got);
        }

        [UnitTest]
        public void ShouldBeAbleToRenderService()
        {
            ProxyModel model = new ProxyModel(typeof(Echo));
            OutLine(model.Render(), ConsoleColor.Cyan);
        }

        [UnitTest]
        public void ShouldBeAbleToUseGeneratedClient()
        {
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetEncryptedEchoClient(out BamServer server, out SecureServiceProxyClient<EncryptedEcho> sspc);
            ConsoleLogger logger = GetTestConsoleLogger();
            ProxyFactory serviceFactory = new ProxyFactory(".\\workspace_".RandomLetters(4), logger);
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
                EncryptedEcho echo = serviceFactory.GetProxy<EncryptedEcho>(server.DefaultHostPrefix.HostName, server.DefaultHostPrefix.Port, logger);
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

        [UnitTest]
        public void MachineWillSerialize()
        {
            Out(Machine.Current.ToJson(), ConsoleColor.Cyan);
            Out(Machine.Current.ToDynamicData().ToJson(), ConsoleColor.Blue);
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
