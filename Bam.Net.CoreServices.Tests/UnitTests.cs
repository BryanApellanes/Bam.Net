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
using NSubstitute;
using Bam.Net.Logging;
using Bam.Net.Data.Repositories;
using System.Net;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.UserAccounts.Data;
using System.Threading;

namespace Bam.Net.CoreServices.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
        interface ITestInterface
        {
            void Go();
            void GoAgain();
        }

        class TestEventSourceLoggable: EventSource
        {
            public TestEventSourceLoggable(DaoRepository daoRepository, ILogger logger) : base(daoRepository, new AppConf("Test"), logger)
            {
                IHttpContext context = Substitute.For<IHttpContext>();
                context.Request = Substitute.For<IRequest>();
                context.Request.Cookies.Returns(new CookieCollection());
                context.Response = Substitute.For<IResponse>();
                context.Response.Cookies.Returns(new CookieCollection());
                HttpContext = context;
            }

            public event EventHandler TestEvent;
            public async Task Test()
            {
                await Trigger("TestEvent");
            }
        }

        [UnitTest]
        public async void CopyEventHandlersTest()
        {
            TestEventSourceLoggable src = GetTestEventSource();
            TestEventSourceLoggable src2 = GetTestEventSource();
            bool? fired = false;
            int expectCount = 0;
            src.TestEvent += (o, a) => { fired = true; };
            src2.CopyEventHandlers(src);
            await src2.Test().ContinueWith(t =>
            {
                expectCount++;
                Expect.IsTrue(fired.Value);
            });
            Expect.IsTrue(expectCount == 1);
        }

        [UnitTest]
        public void GetEventSubscriptionsTest()
        {
            TestEventSourceLoggable src = GetTestEventSource();
            bool? fired = false;
            src.TestEvent += (o, a) => { fired = true; };
            List<EventSubscription> subs = src.GetEventSubscriptions().ToList();
            Expect.AreEqual(1, subs.Count);
            subs.First().Invoke(src, EventArgs.Empty);
            Expect.IsTrue(fired.Value);
        }

        [UnitTest]
        public async void FireNamedEventTest()
        {
            TestEventSourceLoggable src = GetTestEventSource();
            bool? fired = false;
            GlobalEvents.ClearSubscribers<TestEventSourceLoggable>("TestEvent");
            GlobalEvents.Subscribe<TestEventSourceLoggable>("TestEvent", (em, c) =>
            {
                fired = true;
            });
            Task test = src.Test();
            await test.ConfigureAwait(true);
            await test;
            Thread.Sleep(100);
            Expect.IsTrue(fired.Value);
        }

        private static TestEventSourceLoggable GetTestEventSource()
        {
            Database db = new SQLiteDatabase(".\\EventSourceTests", "UserAccounts");
            db.TryEnsureSchema(typeof(User));
            Db.For<User>(db);
            TestEventSourceLoggable src = new TestEventSourceLoggable(new DaoRepository(db), new ConsoleLogger());
            return src;
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
            ProxyModel model = new ProxyModel(typeof(Echo));
            OutLine(model.Render(), ConsoleColor.Cyan);
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
        public void ShouldBeAbleToUseGeneratedClient()
        {
            BamServer server;
            SecureServiceProxyClient<EncryptedEcho> sspc;
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetEncryptedEchoClient(out server, out sspc);
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
                EncryptedEcho echo = serviceFactory.GetProxy<EncryptedEcho>("localhost");
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
