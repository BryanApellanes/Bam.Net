using System;
using System.Collections.Generic;
using System.Reflection;
using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.CommandLine;
using Bam.Net.CoreServices.ServiceRegistration;
using Bam.Net.CoreServices.Files;
using Bam.Net.CoreServices.AssemblyManagement.Data.Dao.Repository;
using Bam.Net.CoreServices.AssemblyManagement.Data.Dao;
using Bam.Net.CoreServices.ServiceRegistration.Data.Dao;
using Bam.Net.CoreServices.ServiceRegistration.Data.Dao.Repository;
using Bam.Net.Testing.Unit;
using Bam.Net.Services.Documentation.Data;
using Bam.Net.Services.OpenApi;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class ServiceRegistryServiceTests: CommandLineTool
    {
        public class TestProxy
        {
            [Exclude]
            public void MethodExclude() { }
            [Local]
            public void MethodLocal() { }
        }

        //[UnitTest]
        //public void CanCallBatchAllWithReflection()
        //{
        //    //Bam.Net.Services.OpenApi.ObjectDescriptor.BatchAll
        //    MethodInfo method = typeof(Bam.Net.Services.OpenApi.ObjectDescriptor).GetMethod("BatchAll");
        //    var delegateType = typeof(Action<>).MakeGenericType(typeof(IEnumerable<>));
        //    var del = Delegate.CreateDelegate(method.GetParameters()[1].ParameterType, typeof(ServiceRegistryServiceTests).GetMethod("EnumerableAction"));
        //    method.Invoke(null, new object[] { 5, del, new OpenApiObjectDatabase() });
        //}

        public void EnumerableAction(IEnumerable<object> values)
        {
            foreach(object value in values)
            {
                OutLine(value.PropertiesToLine());
            }
        }

        [UnitTest]
        public void WillProxyTest()
        {
            MethodInfo method1 = typeof(TestProxy).GetMethod("MethodExclude");
            MethodInfo method2 = typeof(TestProxy).GetMethod("MethodLocal");
            Expect.IsFalse(method1.WillProxy());
            Expect.IsFalse(method2.WillProxy());
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
            ServiceRegistryService svc = GetServiceRegistrationService(nameof(ServiceRegistryLoaderTest));
            List<ServiceRegistryContainerRegistrationResult> results = svc.RegisterServiceRegistryContainers(Assembly.GetExecutingAssembly());
            Expect.AreEqual(1, results.Count);
            CoreServices.ServiceRegistry registry = svc.GetServiceRegistry(results[0].Name);
            TestRegistryClass instance = registry.Get<TestRegistryClass>();
            Expect.AreEqual(instance.SetByCtor, TestRegistryContainer.TestValue);
            Expect.AreEqual(typeof(ConsoleLogger), instance.Logger.GetType());
        }

        [UnitTest]
        public void ServiceRegistryLoaderTest()
        {
            ServiceRegistryService svc = GetServiceRegistrationService(nameof(ServiceRegistryLoaderTest));
            string name = nameof(ServiceRegistryLoaderTest);
            svc.RegisterServiceRegistryLoader(name, typeof(TestServiceRegistryContainer).GetMethod("Create"), true);

            CoreServices.ServiceRegistry reg = svc.GetServiceRegistry(name);
            TestMonkey value = reg.Get<TestMonkey>();
            Expect.AreEqual("SomeValue", value.Name);
            Expect.IsNull(value.Logger);
            reg.SetProperties(value);
            Expect.IsObjectOfType<ConsoleLogger>(value.Logger);
        }

        private ServiceRegistryService GetServiceRegistrationService(string databaseName)
        {
            Database db = GetDatabase(databaseName);
            return new ServiceRegistryService(
                GetFileService(db),
                GetAssemblyService(db),
                GetServiceRegistryRepository(db),
                GetDaoRepository(db),
                new Server.AppConf(),
                DataProvider.Instance);
        }

        private AssemblyService GetAssemblyService(Database db)
        {
            FileService fmSvc = GetFileService(db);
            AssemblyManagementRepository assManRepo = new AssemblyManagementRepository() { Database = db };
            return new AssemblyService(DataProvider.Instance, fmSvc, assManRepo, DefaultConfigurationApplicationNameProvider.Instance);
        }

        private Database GetDatabase(string databaseName)
        {
            SQLiteDatabase db = new SQLiteDatabase(".\\", databaseName);
            db.TryEnsureSchema<AssemblyDescriptor>();
            db.TryEnsureSchema<Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor>();
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
