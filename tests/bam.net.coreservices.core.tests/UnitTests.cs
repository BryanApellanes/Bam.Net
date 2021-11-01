/*
	Copyright © Bryan Apellanes 2015  
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using Bam.Net.CommandLine;
using Bam.Net.CoreServices.AccessControl;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;
using Bam.Net.Testing.Unit;
using Bam.Net.UserAccounts.Data;
using NSubstitute;
using HostAddress = Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostAddress;
using Machine = Bam.Net.CoreServices.ApplicationRegistration.Data.Machine;
using Nic = Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Nic;
using Organization = Bam.Net.CoreServices.ApplicationRegistration.Data.Organization;
using ProcessDescriptor = Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor;
using User = Bam.Net.UserAccounts.Data.User;
using UserCollection = Bam.Net.UserAccounts.Data.UserCollection;

namespace Bam.Net.CoreServices.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTool
    {
        [UnitTest]
        public void EnvironmentVariableHeaderProviderShouldReadEnvironment()
        {
            string value = "TestValue";
            Environment.SetEnvironmentVariable(EnvironmentVariableAuthorizationHeaderProvider.DefaultEnvironmentVariableName, value);

            EnvironmentVariableAuthorizationHeaderProvider provider = new EnvironmentVariableAuthorizationHeaderProvider();
            Expect.AreEqual(value, provider.Value);
        }

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
        public void ApplicationRegistryRepositoryGetOneUserShouldHaveNoOrganization()
        {
            TimeSpan elapsed = Timed.Execution(() =>
            {
                ApplicationRegistrationRepository repo = new ApplicationRegistrationRepository();
                repo.Database = new SQLiteDatabase($"{nameof(ApplicationRegistryRepositoryGetOneUserShouldHaveNoOrganization)}", nameof(ApplicationRegistryRepositoryGetOneUserShouldHaveNoOrganization));
                var user = repo.GetOneUserWhere(c => c.UserName == "bryan");
                Expect.IsNotNull(user);
                Expect.AreEqual(0, user.Organizations.Count);
                Expect.IsGreaterThan(user.Id, 0);
                OutLine(user.PropertiesToString(), ConsoleColor.Cyan);
                repo.Delete(user);
                user = repo.Retrieve<ApplicationRegistration.Data.User>(user.Id);
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
            ApplicationRegistryService svc = GetTestService();
            Expect.AreSame(User.Anonymous, svc.CurrentUser);
        }

        [UnitTest]
        public void CoreApplicationRegistryServiceMustBeLoggedInToRegister()
        {
            ApplicationRegistryService svc = GetTestService();
            string orgName = 5.RandomLetters();
            string appName = 8.RandomLetters();
            ProcessDescriptor descriptor = ProcessDescriptor.ForApplicationRegistration(svc.ApplicationRegistrationRepository, "localhost", 8080, appName, orgName);
            CoreServiceResponse response = svc.RegisterApplicationProcess(descriptor);
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
            ApplicationRegistryService svc = GetTestServiceWithUser(userName);
            ProcessDescriptor descriptor = ProcessDescriptor.ForApplicationRegistration(svc.ApplicationRegistrationRepository, "localhost", 8080, appName, orgName);
            CoreServiceResponse response = svc.RegisterApplicationProcess(descriptor);
            Expect.IsTrue(response.Success);
            var user = svc.ApplicationRegistrationRepository.OneUserWhere(c => c.UserName == userName);
            user = svc.ApplicationRegistrationRepository.Retrieve<ApplicationRegistration.Data.User>(user.Id);
            Expect.IsNotNull(user);
            Expect.AreEqual(1, user.Organizations.Count);
            Thread.Sleep(1000);
            Pass($"{nameof(OrganizationGetsCreated)} Test Passed");
        }
        
        [UnitTest]
        public void MustBeLoggedInToRegister()
        {
            After.Setup(setupContext =>
            {
                setupContext.CopyFrom((CoreServiceRegistryContainer.GetServiceRegistry()));
            })
            .WhenA<ApplicationRegistryService>("tries to register application when not logged in", applicationRegistryService =>
            {
                ProcessDescriptor descriptor = ProcessDescriptor.ForApplicationRegistration(applicationRegistryService.ApplicationRegistrationRepository,"localhost", 8080, "testApp", "testOrg");
                return applicationRegistryService.RegisterApplicationProcess(descriptor);
            })
            .TheTest
            .ShouldPass((because, assertionProvider) =>
            {
                because.ItsTrue("the object under test is not null", assertionProvider.Value != null);
                because.ItsTrue($"object under test is of type {nameof(ApplicationRegistryService)}", assertionProvider.Value.GetType() == typeof(ApplicationRegistryService));
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
            ApplicationRegistration.Data.User user = new ApplicationRegistration.Data.User
            {
                UserName = 9.RandomLetters()
            };
            user = repo.Save(user);
            ApplicationRegistration.Data.User retrieved = repo.Retrieve<ApplicationRegistration.Data.User>(user.Uuid);
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
                if(typeName.EndsWith("Service") && type.IsSubclassOf(typeof(ProxyableService)))
                {
                    foundOne = true;
                    OutLine(type.FullName, ConsoleColor.Cyan);
                }
            }
            foundOne.IsTrue();
        }

        [UnitTest]
        public void ProcessDescriptorWillSerialize()
        {
            Message.PrintLine("{0}", ConsoleColor.Cyan, ProcessDescriptor.Current.ToJson());
        }

        [UnitTest]
        public void MachineHasNics()
        {
            Machine machine = new Machine();
            machine.NetworkInterfaces.IsNotNull($"{nameof(machine.NetworkInterfaces)} was null");
            Expect.IsGreaterThan(machine.NetworkInterfaces.Count, 0, "No IpAddress entries were found");
            machine.NetworkInterfaces.Each(n => OutLine(n.PropertiesToString(), ConsoleColor.Cyan));
        }

        [UnitTest]
        public void MachineHasHostAddresses()
        {
            Machine machine = new Machine();
            machine.HostAddresses.IsNotNull($"{nameof(machine.HostAddresses)} was null");
            Expect.IsGreaterThan(machine.HostAddresses.Count, 0, "No IpAddress entries were found");
            machine.HostAddresses.Each(h => OutLine(h.PropertiesToString(), ConsoleColor.Blue));
        }

        [UnitTest]
        public void SavingMachineSavesNicsAndHostAddresses()
        {
            Machine machine = new Machine();
            Database db = new SQLiteDatabase($"./{nameof(SavingMachineSavesNicsAndHostAddresses)}", "CoreRegistryRepository");
            db.TryEnsureSchema<Nic>();
            ApplicationRegistrationRepository repo = new ApplicationRegistrationRepository() { Database = db };

            Nic.LoadAll(repo.Database).Delete(repo.Database);
            HostAddress.LoadAll(repo.Database).Delete(repo.Database);
            ApplicationRegistration.Data.Dao.Machine.LoadAll(repo.Database).Delete(repo.Database);

            machine = machine.Save(repo) as Machine;
            machine.NetworkInterfaces.Each(n => OutLine(n.PropertiesToString(), ConsoleColor.Cyan));
            machine.HostAddresses.Each(h => OutLine(h.PropertiesToString(), ConsoleColor.Blue));

            NicCollection nics = Nic.LoadAll(repo.Database);
            HostAddressCollection hosts = HostAddress.LoadAll(repo.Database);

            Machine machineAgain = machine.Save(repo) as Machine;
            Expect.AreEqual(machine.Id, machineAgain.Id, "Id didn't match");
            Expect.AreEqual(machine.Uuid, machineAgain.Uuid, "Uuid didn't match");
            Expect.AreEqual(machine.Cuid, machineAgain.Cuid, "Cuid didn't match");

            NicCollection nicsAgain = Nic.LoadAll(repo.Database);
            HostAddressCollection hostsAgain = HostAddress.LoadAll(repo.Database);

            Expect.AreEqual(nics.Count, nicsAgain.Count, "Nic count didn't match");
            Expect.AreEqual(hosts.Count, hostsAgain.Count, "Host address count didn't match");
        }

        [UnitTest]
        public void ProcessDescriptorHasMachine()
        {
            ProcessDescriptor process = ProcessDescriptor.Current;
            Expect.IsNotNull(process.Machine, $"{nameof(process.Machine)} was null");
        }

        [UnitTest]
        public void NicsWillSerialize()
        {
            Message.Print(Machine.Current.NetworkInterfaces.ToJson(true));
        }

        [UnitTest]
        public void EnsureSingleDoesntDuplicate()
        {
            ServiceRegistry glooRegistry = CoreServiceRegistryContainer.GetServiceRegistry();
            ApplicationRegistrationRepository repo = glooRegistry.Get<ApplicationRegistrationRepository>();
            CompositeRepository compositeRepo = glooRegistry.Get<CompositeRepository>();
            compositeRepo.UnwireBackup();
            Machine machine = Machine.Current;
            repo.Delete(machine);
            Machine retrieved = repo.Query<Machine>(Filter.Where("Name") == machine.Name && Filter.Where("Cuid") == machine.Cuid).FirstOrDefault();
            if(retrieved != null)
            {
                repo.Delete(retrieved);
            }
            retrieved = repo.Query<Machine>(Filter.Where("Name") == machine.Name && Filter.Where("Cuid") == machine.Cuid).FirstOrDefault();
            Expect.IsNull(retrieved);
            Machine ensured = machine.EnsureSingle<Machine>(repo, "Test UserName of modifier", "Cuid");
            Expect.IsNotNull(ensured, "Ensured was null");
            Machine ensuredAgain = ensured.EnsureSingle<Machine>(repo, "Test UserName of modifier", "Cuid");
            Expect.AreEqual(ensured.Id, ensuredAgain.Id);
            Expect.AreEqual(ensured.Uuid, ensuredAgain.Uuid);
            Expect.AreEqual(ensured.Cuid, ensuredAgain.Cuid);

            Expect.AreEqual(1, repo.Query<Machine>(new { Name = machine.Name, Cuid = machine.Cuid }).Count());
            repo.Delete(machine);
        }

        /*[UnitTest]
        public void CoreClientCanSignup()
        {
            //throw new NotImplementedException();
        }

        [UnitTest]
        public void CoreClientCanRegisterCurrentApp()
        {
            //throw new NotImplementedException();
        }*/

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
            ApplicationRegistryService svc = GetTestService();
            string userName = 8.RandomLetters();
            User user = SetCurrentUser(userName, svc);
            Expect.AreEqual(userName, user.UserName);
            User sessionUser = Session.Get(svc.HttpContext).UserOfUserId;
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

        private ApplicationRegistryService GetTestServiceWithUser(string userName)
        {
            ApplicationRegistryService svc = GetTestService();
            SetCurrentUser(userName, svc);
            return svc;
        }

        private ApplicationRegistryService GetTestService()
        {
            ServiceRegistry registry = CoreServiceRegistryContainer.GetServiceRegistry();
            ApplicationRegistryService svc = registry.Get<ApplicationRegistryService>();
            registry.SetProperties(svc);
            return svc;
        }

        private static User SetCurrentUser(string userName, ApplicationRegistryService svc)
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
            UserCollection users = User.LoadAll();
            users.Delete();
            User result = User.Create(userName);
            Session session = Session.Get(ctx);
            session.UserId = result.Id;
            session.Save();
            IEnumerable<Organization> organizations = svc.ApplicationRegistrationRepository.RetrieveAll<Organization>();
            organizations.Each(o => svc.ApplicationRegistrationRepository.Delete(o));
            Expect.AreEqual(0, svc.ApplicationRegistrationRepository.RetrieveAll<Organization>().Count());
            IEnumerable<ApplicationRegistration.Data.Application> apps = svc.ApplicationRegistrationRepository.RetrieveAll<ApplicationRegistration.Data.Application>();
            apps.Each(a => svc.ApplicationRegistrationRepository.Delete(a));
            Expect.AreEqual(0, svc.ApplicationRegistrationRepository.RetrieveAll<ApplicationRegistration.Data.Application>().Count());
            svc.ApplicationRegistrationRepository.RetrieveAll<Machine>().Each(h => svc.ApplicationRegistrationRepository.Delete(h));
            return result;
        }
    }
}
