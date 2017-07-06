using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.ServiceProxy;
using Bam.Net.Services.AssemblyManagement.Data.Dao;
using Bam.Net.Services.AssemblyManagement.Data.Dao.Repository;
using Bam.Net.Services.Files;
using Bam.Net.Services.ServiceRegistry.Data;
using Bam.Net.Services.ServiceRegistry.Data.Dao.Repository;
using Bam.Net.Testing;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.CommandLine;
using NSubstitute;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class ServiceRegistryServiceTests: CommandLineTestInterface
    {
        public class TestProxy
        {
            [Exclude]
            public void MethodExclude() { }
            [Local]
            public void MethodLocal() { }
        }

        [UnitTest]
        public void WillProxyTest()
        {
            MethodInfo method1 = typeof(TestProxy).GetMethod("MethodExclude");
            MethodInfo method2 = typeof(TestProxy).GetMethod("MethodLocal");
            Expect.IsFalse(ServiceProxySystem.WillProxyMethod(method1));
            Expect.IsFalse(ServiceProxySystem.WillProxyMethod(method2));
        }

        public class TestMonkey
        {
            public TestMonkey(string name)
            {
                Name = name;
            }

            public string Name { get; set; }

            public ILogger Logger { get; set; }
        }

        public class TestServiceRegistryContainer
        {
            public static CoreServices.ServiceRegistry Create()
            {
                ConsoleLogger logger = new ConsoleLogger();
                logger.StartLoggingThread();
                return (CoreServices.ServiceRegistry)(new CoreServices.ServiceRegistry())
                    .For<ILogger>().Use(logger)
                    .ForCtor<TestMonkey>("name").Use("SomeValue");
            }
        }

        [UnitTest]
        public void CanRegisterContainer()
        {
            ServiceRegistryService svc = GetServiceRegistryService(nameof(ServiceRegistryLoaderTest));
            List<RegisterServiceRegistryContainerResult> results = svc.RegisterServiceRegistryContainers(Assembly.GetExecutingAssembly());
            Expect.AreEqual(1, results.Count);
            CoreServices.ServiceRegistry registry = svc.GetServiceRegistry(results[0].Name);
            TestRegistryClass instance = registry.Get<TestRegistryClass>();
            Expect.AreEqual(instance.SetByCtor, TestRegistryContainer.TestValue);
            Expect.AreEqual(typeof(ConsoleLogger), instance.Logger.GetType());
        }

        [UnitTest]
        public void ServiceRegistryLoaderTest()
        {
            ServiceRegistryService svc = GetServiceRegistryService(nameof(ServiceRegistryLoaderTest));
            string name = nameof(ServiceRegistryLoaderTest);
            svc.RegisterServiceRegistryLoader(name, typeof(TestServiceRegistryContainer).GetMethod("Create"), true);

            CoreServices.ServiceRegistry reg = svc.GetServiceRegistry(name);
            TestMonkey value = reg.Get<TestMonkey>();
            Expect.AreEqual("SomeValue", value.Name);
            Expect.IsNull(value.Logger);
            reg.SetProperties(value);
            Expect.IsObjectOfType<ConsoleLogger>(value.Logger);
        }

        private ServiceRegistryService GetServiceRegistryService(string databaseName)
        {
            Database db = GetDatabase(databaseName);
            return new ServiceRegistryService(GetAssemblyService(db), GetServiceRegistryRepository(db), GetDaoRepository(db), new Server.AppConf());
        }

        private AssemblyService GetAssemblyService(Database db)
        {
            FileService fmSvc = new FileService(GetDaoRepository(db));
            AssemblyServiceRepository assManRepo = new AssemblyServiceRepository() { Database = db };
            return new AssemblyService(fmSvc, assManRepo, DefaultConfigurationApplicationNameProvider.Instance);
        }

        private Database GetDatabase(string databaseName)
        {
            SQLiteDatabase db = new SQLiteDatabase(".\\", databaseName);
            db.TryEnsureSchema<AssemblyDescriptor>();
            db.TryEnsureSchema<ServiceRegistry.Data.Dao.ServiceDescriptor>();
            return db;
        }

        private ServiceRegistryRepository GetServiceRegistryRepository(Database db)
        {
            return new ServiceRegistryRepository() { Database = db };
        }

        private FileService GetFileService(Database db)
        {
            return new FileService(GetDaoRepository(db));
        }

        private DaoRepository GetDaoRepository(Database db)
        {
            return new DaoRepository(db);
        }
    }
}
