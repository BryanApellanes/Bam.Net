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
using Bam.Net.CoreServices.Data.Daos.Repository;
using Bam.Net.CoreServices.Services;
using Bam.Net.Server.Tvg;
using Bam.Net.CoreServices.Data;
using System.Collections.Specialized;

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

        public class TestEventSourceLoggable: EventSourceService
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
            await src.Test();
            Thread.Sleep(300);
            Expect.IsTrue(fired.Value);
            OutLineFormat("fire named event test ran to completion", ConsoleColor.Green);
            OutLineFormat("done", ConsoleColor.Green);
        }

        private static TestEventSourceLoggable GetTestEventSource()
        {
            Database db = new SQLiteDatabase(".\\EventSourceTests", "UserAccounts");
            db.TryEnsureSchema(typeof(UserAccounts.Data.User));
            Db.For<UserAccounts.Data.User>(db);
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

        [UnitTest]
        public void ApplicationRegistryRepositoryGetOneUserShouldHaveNoOrganization()
        {
            TimeSpan elapsed = Timed.Execution(() =>
            {
                ApplicationRegistryRepository repo = new ApplicationRegistryRepository();
                repo.Database = new SQLiteDatabase($"{nameof(ApplicationRegistryRepositoryGetOneUserShouldHaveNoOrganization)}", nameof(ApplicationRegistryRepositoryGetOneUserShouldHaveNoOrganization));
                var user = repo.GetOneUserWhere(c => c.UserName == "bryan");
                Expect.IsNotNull(user);
                Expect.AreEqual(0, user.Organizations.Count);
                Expect.IsGreaterThan(user.Id, 0);
                OutLine(user.PropertiesToString(), ConsoleColor.Cyan);
                repo.Delete(user);
                user = repo.Retrieve<Data.User>(user.Id);
                Expect.IsNull(user);
            });
            OutLine(elapsed.ToString(), ConsoleColor.Cyan);
        }

        // CoreApplciationRegistryService
        // ** register application
        //      - must be logged in
        [UnitTest]
        public void CoreApplicationRegistryServiceNotLoggedInIsAnonymous()
        {
            CoreApplicationRegistryService svc = GetTestService();
            Expect.AreSame(UserAccounts.Data.User.Anonymous, svc.CurrentUser);
        }

        [UnitTest]
        public void CoreApplicationRegistryServiceMustBeLoggedInToRegister()
        {
            CoreApplicationRegistryService svc = GetTestService();
            string orgName = 5.RandomLetters();
            string appName = 8.RandomLetters();
            ServiceResponse response = svc.RegisterApplication(orgName, appName);
            Expect.IsFalse(response.Success);
            Expect.IsNotNull(response.Data);
            Expect.IsInstanceOfType<ApplicationRegistrationResult>(response.Data);
            Expect.AreEqual(ApplicationRegistrationStatus.Unauthorized, ((ApplicationRegistrationResult)response.Data).Status);
        }

        //      - if organization doesn't exist gets created
        [UnitTest]
        public void OrganizationGetsCreated()
        {
            Log.Default = new ConsoleLogger();
            Log.Default.StartLoggingThread();
            string userName = 4.RandomLetters();
            string orgName = 5.RandomLetters();
            string appName = 8.RandomLetters();
            CoreApplicationRegistryService svc = GetTestServiceWithUser(userName);
            ServiceResponse response = svc.RegisterApplication(orgName, appName);
            Expect.IsTrue(response.Success);
            var user = svc.ApplicationRegistryRepository.OneUserWhere(c => c.UserName == userName);
            user = svc.ApplicationRegistryRepository.Retrieve<Data.User>(user.Id);
            Expect.IsNotNull(user);
            Expect.AreEqual(1, user.Organizations.Count);
            Thread.Sleep(1000);
        }
        
        [UnitTest]
        public void MustBeLoggedInToRegister()
        {
            After.Setup(ctx =>
            {
                ctx.CopyFrom(CoreRegistry.Get());
            })
            .WhenA<CoreApplicationRegistryService>("tries to register application when not logged in", cars =>
            {
                return cars.RegisterApplication("testorg", "testapp");
            })
            .TheTest
            .ShouldPass(because =>
            {
                ServiceResponse result = because.ResultAs<ServiceResponse>();
                because.ItsTrue("the response was not successful", !result.Success, "request should have failed");
                because.ItsTrue("the message says 'You must be logged in to do that'", result.Message.Equals("You must be logged in to do that"));
                because.IllLookAtIt(result.Message);
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        [UnitTest]
        public void CanSaveUserToCompositeRepo()
        {
            CompositeRepository repo = CoreRegistry.Get().Get<CompositeRepository>();
            Data.User user = new Data.User();
            user.UserName = 9.RandomLetters();
            user = repo.Save(user);
            Data.User retrieved = repo.Retrieve<Data.User>(user.Uuid);
            Expect.AreEqual(user.UserName, retrieved.UserName);
            Expect.AreEqual(user.Id, retrieved.Id);
            Expect.AreEqual(user.Uuid, retrieved.Uuid);
        }

        [UnitTest]
        public void CanListCoreServices()
        {
            Assembly coreServicesAssembly = typeof(CoreRegistry).Assembly;
            bool foundOne = false;
            foreach(Type type in coreServicesAssembly.GetTypes())
            {
                string typeName = type.Name;
                if(typeName.StartsWith("Core") && typeName.EndsWith("Service") && type.IsSubclassOf(typeof(ProxyableService)))
                {
                    foundOne = true;
                    OutLine(type.FullName, ConsoleColor.Cyan);
                }
            }
            Expect.IsTrue(foundOne);
        }

        //      - if more than one organization for a user then fail if not premium
        [UnitTest]
        public void IfMoreThanOneOrganizationForAUserThenFailIfNoSubscription()
        {
            // create a user 
            // create an organization
            // save the user to the organization
            // try to register app for same user with a different organization
            // should fail
        }
        //      - if app doesn't exist create it

        //      - if more than 5 apps for an organization then fail if not premium

        // CoreApiKeyResolverClient
        //      - uses CoreApplicationRegistryService client
        [UnitTest]
        public void TestTheSetup()
        {
            CoreApplicationRegistryService svc = GetTestService();
            string userName = 8.RandomLetters();
            UserAccounts.Data.User user = SetCurrentUser(userName, svc);
            Expect.AreEqual(userName, user.UserName);
            UserAccounts.Data.User sessionUser = Session.Get(svc.HttpContext).UserOfUserId;
            Expect.IsNotNull(sessionUser);
            Expect.AreEqual(userName, sessionUser.UserName, "UserName didn't match");
            Expect.AreEqual(sessionUser, svc.CurrentUser, "Users didn't match");
        }
 
        private CoreApplicationRegistryService GetTestServiceWithUser(string userName)
        {
            CoreApplicationRegistryService svc = GetTestService();
            SetCurrentUser(userName, svc);
            return svc;
        }

        private CoreApplicationRegistryService GetTestService()
        {
            GlooRegistry registry = CoreRegistry.Get();
            CoreApplicationRegistryService svc = registry.Get<CoreApplicationRegistryService>();
            registry.SetProperties(svc);
            return svc;
        }

        private static UserAccounts.Data.User SetCurrentUser(string userName, CoreApplicationRegistryService svc)
        {
            IHttpContext ctx = Substitute.For<IHttpContext>();
            ctx.Request = Substitute.For<IRequest>();
            ctx.Request.Url.Returns(new Uri("http://test.cxm"));
            NameValueCollection headers = new NameValueCollection();
            headers["REMOTE_ADDR"] = "127.0.0.1";
            ctx.Request.Headers.Returns(headers);
            ctx.Response = Substitute.For<IResponse>();
            CookieCollection Cookies = new CookieCollection();
            Cookie sessionCookie = new Cookie(Session.CookieName, "0368c7fde0a40272d42e14e224d37761dbccef665116ccb063ae31aaa7708d72");
            Cookies.Add(sessionCookie);
            ctx.Request.Cookies.Returns(Cookies);
            ctx.Response.Cookies.Returns(Cookies);
            svc.HttpContext = ctx;
            SessionCollection sessions = Session.LoadAll();
            sessions.Delete();
            UserCollection users = UserAccounts.Data.User.LoadAll();
            users.Delete();
            UserAccounts.Data.User result = UserAccounts.Data.User.Create(userName);
            Session session = Session.Get(ctx);
            session.UserId = result.Id;
            session.Save();
            IEnumerable<Organization> organizations = svc.ApplicationRegistryRepository.RetrieveAll<Organization>();
            organizations.Each(o => svc.ApplicationRegistryRepository.Delete(o));
            Expect.AreEqual(0, svc.ApplicationRegistryRepository.RetrieveAll<Organization>().Count());
            IEnumerable<Data.Application> apps = svc.ApplicationRegistryRepository.RetrieveAll<Data.Application>();
            apps.Each(a => svc.ApplicationRegistryRepository.Delete(a));
            Expect.AreEqual(0, svc.ApplicationRegistryRepository.RetrieveAll<Data.Application>().Count());
            svc.ApplicationRegistryRepository.RetrieveAll<Data.HostName>().Each(h => svc.ApplicationRegistryRepository.Delete(h));
            return result;
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
