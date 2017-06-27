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
using Bam.Net.CoreServices.Data.Dao.Repository;
using Bam.Net.CoreServices.Services;
using Bam.Net.CoreServices.Data;
using System.Collections.Specialized;
using Bam.Net.Data.Dynamic;
using Bam.Net.Services.Distributed;
using Bam.Net.Incubation;
using Bam.Net.Testing.Integration;

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
                EncryptedEcho echo = serviceFactory.GetProxy<EncryptedEcho>("localhost", 8080);
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
                CoreRegistryRepository repo = new CoreRegistryRepository();
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
            ProcessDescriptor descriptor = ProcessDescriptor.ForApplicationRegistration(svc.CoreRegistryRepository, "localhost", 8080, appName, orgName);
            CoreServiceResponse response = svc.RegisterApplication(descriptor);
            Expect.IsFalse(response.Success);
            Expect.IsNotNull(response.Data);
            Expect.IsInstanceOfType<ApplicationRegistrationResult>(response.Data);
            Expect.AreEqual(ApplicationRegistrationStatus.Unauthorized, ((ApplicationRegistrationResult)response.Data).Status);
        }

        //      - if organization doesn't exist gets created
        [IntegrationTest]
        public void OrganizationGetsCreated()
        {
            Log.Default = new ConsoleLogger();
            Log.Default.StartLoggingThread();
            string userName = 4.RandomLetters();
            string orgName = 5.RandomLetters();
            string appName = 8.RandomLetters();
            CoreApplicationRegistryService svc = GetTestServiceWithUser(userName);
            ProcessDescriptor descriptor = ProcessDescriptor.ForApplicationRegistration(svc.CoreRegistryRepository, "localhost", 8080, appName, orgName);
            CoreServiceResponse response = svc.RegisterApplication(descriptor);
            Expect.IsTrue(response.Success);
            var user = svc.CoreRegistryRepository.OneUserWhere(c => c.UserName == userName);
            user = svc.CoreRegistryRepository.Retrieve<Data.User>(user.Id);
            Expect.IsNotNull(user);
            Expect.AreEqual(1, user.Organizations.Count);
            Thread.Sleep(1000);
            Pass($"{nameof(OrganizationGetsCreated)} Test Passed");
        }
        
        [UnitTest]
        public void MustBeLoggedInToRegister()
        {
            After.Setup((Action<SetupContext>)(ctx =>
            {
                ctx.CopyFrom((Incubation.Incubator)CoreServiceRegistryContainer.GetServiceRegistry());
            }))
            .WhenA<CoreApplicationRegistryService>("tries to register application when not logged in", cars =>
            {
                ProcessDescriptor descriptor = ProcessDescriptor.ForApplicationRegistration(cars.CoreRegistryRepository,"localhost", 8080, "testApp", "testOrg");
                return cars.RegisterApplication(descriptor);
            })
            .TheTest
            .ShouldPass(because =>
            {
                CoreServiceResponse result = because.ResultAs<CoreServiceResponse>();
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
            CompositeRepository repo = CoreServiceRegistryContainer.GetServiceRegistry().Get<CompositeRepository>();
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
            Assembly coreServicesAssembly = typeof(CoreServiceRegistryContainer).Assembly;
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

        [UnitTest]
        public void ProcessDescriptorWillSerialize()
        {
            OutLineFormat("{0}", ConsoleColor.Cyan, ProcessDescriptor.Current.ToJson());
        }

        [UnitTest]
        public void MachineHasNics()
        {
            Machine machine = new Machine();
            Expect.IsNotNull(machine.NetworkInterfaces, $"{nameof(machine.NetworkInterfaces)} was null");
            Expect.IsGreaterThan(machine.NetworkInterfaces.Count, 0, "No IpAddress entries were found");
            machine.NetworkInterfaces.Each(n => OutLine(n.PropertiesToString(), ConsoleColor.Cyan));
        }

        [UnitTest]
        public void MachineHasHostAddresses()
        {
            Machine machine = new Machine();
            Expect.IsNotNull(machine.HostAddresses, $"{nameof(machine.HostAddresses)} was null");
            Expect.IsGreaterThan(machine.HostAddresses.Count, 0, "No IpAddress entries were found");
            machine.HostAddresses.Each(h => OutLine(h.PropertiesToString(), ConsoleColor.Blue));
        }

        [UnitTest]
        public void SavingMachineSavesNicsAndHostAddresses()
        {
            Machine machine = new Machine();
            Database db = new SQLiteDatabase($".\\{nameof(SavingMachineSavesNicsAndHostAddresses)}", "CoreRegistryRepository");
            db.TryEnsureSchema<Data.Dao.Nic>();
            CoreRegistryRepository repo = new CoreRegistryRepository() { Database = db };

            Data.Dao.Nic.LoadAll(repo.Database).Delete(repo.Database);
            Data.Dao.HostAddress.LoadAll(repo.Database).Delete(repo.Database);
            Data.Dao.Machine.LoadAll(repo.Database).Delete(repo.Database);

            machine = machine.Save(repo) as Machine;
            machine.NetworkInterfaces.Each(n => OutLine(n.PropertiesToString(), ConsoleColor.Cyan));
            machine.HostAddresses.Each(h => OutLine(h.PropertiesToString(), ConsoleColor.Blue));

            Data.Dao.NicCollection nics = Data.Dao.Nic.LoadAll(repo.Database);
            Data.Dao.HostAddressCollection hosts = Data.Dao.HostAddress.LoadAll(repo.Database);

            Machine machineAgain = machine.Save(repo) as Machine;
            Expect.AreEqual(machine.Id, machineAgain.Id, "Id didn't match");
            Expect.AreEqual(machine.Uuid, machineAgain.Uuid, "Uuid didn't match");
            Expect.AreEqual(machine.Cuid, machineAgain.Cuid, "Cuid didn't match");

            Data.Dao.NicCollection nicsAgain = Data.Dao.Nic.LoadAll(repo.Database);
            Data.Dao.HostAddressCollection hostsAgain = Data.Dao.HostAddress.LoadAll(repo.Database);

            Expect.AreEqual(nics.Count, nicsAgain.Count, "Nic count didn't match");
            Expect.AreEqual(hosts.Count, hostsAgain.Count, "Host address count didn't match");
        }

        [UnitTest]
        public void ProcessDescriptorHasMachine()
        {
            ProcessDescriptor process = ProcessDescriptor.Current;
            Expect.IsNotNull(process.LocalMachine, $"{nameof(process.LocalMachine)} was null");
        }

        [UnitTest]
        public void MachineWillSerialize()
        {
            Out(Machine.Current.ToJson(), ConsoleColor.Cyan);
            Out(Machine.Current.ToDynamicData().ToJson(), ConsoleColor.Blue);
        }

        [UnitTest]
        public void NicsWillSerialize()
        {
            Out(Machine.Current.NetworkInterfaces.ToJson(true));
        }

        [UnitTest]
        public void EnsureSingleDoesntDuplicate()
        {
            ServiceRegistry glooRegistry = CoreServiceRegistryContainer.GetServiceRegistry();
            CoreRegistryRepository repo = glooRegistry.Get<CoreRegistryRepository>();
            CompositeRepository compositeRepo = glooRegistry.Get<CompositeRepository>();
            compositeRepo.UnwireBackup();
            Machine machine = Machine.Current;
            repo.Delete(machine);
            Machine retrieved = repo.Query<Machine>(Filter.Where("Name") == machine.Name && Filter.Where("Cuid") == machine.Cuid).FirstOrDefault();
            Expect.IsNull(retrieved);
            Machine ensured = machine.EnsureSingle<Machine>(repo, "Name", "Cuid");
            Expect.IsNotNull(ensured, "Ensured was null");
            Machine ensuredAgain = ensured.EnsureSingle<Machine>(repo, "Name", "Cuid");
            Expect.AreEqual(ensured.Id, ensuredAgain.Id);
            Expect.AreEqual(ensured.Uuid, ensuredAgain.Uuid);
            Expect.AreEqual(ensured.Cuid, ensuredAgain.Cuid);

            Expect.AreEqual(1, repo.Query<Machine>(new { Name = machine.Name, Cuid = machine.Cuid }).Count());
            repo.Delete(machine);
        }

        [UnitTest]
        public void CoreClientCanSignup()
        {
            //throw new NotImplementedException();
        }

        [UnitTest]
        public void CoreClientCanRegisterCurrentApp()
        {
            //throw new NotImplementedException();
        }

        //      - if more than one organization for a user then fail if not premium
        [UnitTest]
        public void IfMoreThanOneOrganizationForAUserThenFailIfNoSubscription()
        {
            //GlooRegistry registry = CoreRegistry.GetGlooRegistry();
            //CoreApplicationRegistryService appRegistry = registry.Get<CoreApplicationRegistryService>();
            //CoreRegistryRepository coreRepo = appRegistry.CoreRegistryRepository;
            //// signup a random user
            //Expect.Fail("This test isn't fully implemented");
            //// log in            
            //// register an application 
            //ServiceResponse response = appRegistry.RegisterApplication("TestOrg", "TestApp1");
            //Expect.IsTrue(response.Success, "Failed to register application");
            //// try to register app for same user with a different organization
            //response = appRegistry.RegisterApplication("TestOrg", "TestApp2");
            //// should fail
            //Expect.IsFalse(response.Success, "Should have failed to register application but was successful");            
        }
        //      - if app doesn't exist create it

        //      - if more than 5 apps for an organization then fail if not premium

        // CoreApiKeyResolverClient
        //      - uses CoreApplicationRegistryService client
        [IntegrationTest]
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
 
        [UnitTest]
        public void CoreServiceRegistryTest()
        {
            ServiceRegistry reg = CoreServiceRegistryContainer.Create();
            IUserResolver userResolver = reg.Get<IUserResolver>();
            Expect.IsNotNull(userResolver);
        }

        [UnitTest]
        public void CoreServiceRegistryCopyTest()
        {
            ServiceRegistry reg = CoreServiceRegistryContainer.Create();
            Incubator copy = new Incubator();
            copy.CopyFrom(reg);
            IUserResolver userResolver = copy.Get<IUserResolver>();
            Expect.IsNotNull(userResolver);
        }

        private CoreApplicationRegistryService GetTestServiceWithUser(string userName)
        {
            CoreApplicationRegistryService svc = GetTestService();
            SetCurrentUser(userName, svc);
            return svc;
        }

        private CoreApplicationRegistryService GetTestService()
        {
            ServiceRegistry registry = CoreServiceRegistryContainer.GetServiceRegistry();
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
            IEnumerable<Organization> organizations = svc.CoreRegistryRepository.RetrieveAll<Organization>();
            organizations.Each(o => svc.CoreRegistryRepository.Delete(o));
            Expect.AreEqual(0, svc.CoreRegistryRepository.RetrieveAll<Organization>().Count());
            IEnumerable<Data.Application> apps = svc.CoreRegistryRepository.RetrieveAll<Data.Application>();
            apps.Each(a => svc.CoreRegistryRepository.Delete(a));
            Expect.AreEqual(0, svc.CoreRegistryRepository.RetrieveAll<Data.Application>().Count());
            svc.CoreRegistryRepository.RetrieveAll<Data.Machine>().Each(h => svc.CoreRegistryRepository.Delete(h));
            return result;
        }

        private static ConsoleLogger GetTestConsoleLogger()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.StartLoggingThread();
            return logger;
        }

        private static TestEventSourceLoggable GetTestEventSource()
        {
            Database db = new SQLiteDatabase(".\\EventSourceTests", "UserAccounts");
            db.TryEnsureSchema(typeof(UserAccounts.Data.User));
            Db.For<UserAccounts.Data.User>(db);
            TestEventSourceLoggable src = new TestEventSourceLoggable(new DaoRepository(db), new ConsoleLogger());
            return src;
        }

    }
}
