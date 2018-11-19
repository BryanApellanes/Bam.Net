using Bam.Net.Caching;
using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.ServiceProxy.Tests;
using Bam.Net.Services;
using Bam.Net.Services.DataReplication.Data;
using Bam.Net.Services.Tests;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
        [UnitTest]
        public void RepoQueryTest()
        {
            string nameOne = 32.RandomLetters();
            foreach (Repository repo in GetTestRepositories())
            {
                KeyHashRepoTestData one = new KeyHashRepoTestData { Name = nameOne };
                one.Save<KeyHashRepoTestData>(repo);

                List<KeyHashRepoTestData> retrieved = repo.Query<KeyHashRepoTestData>(new { Name = nameOne }).ToList();
                retrieved = repo.Query<KeyHashRepoTestData>(d => d.Name.Equals(nameOne)).ToList();
                Expect.AreEqual(1, retrieved.Count);
                retrieved = repo.Query(typeof(KeyHashRepoTestData), o => o.Property("Name").Equals(nameOne)).CopyAs<KeyHashRepoTestData>().ToList();
                Expect.AreEqual(1, retrieved.Count);
                retrieved = repo.Query<KeyHashRepoTestData>(new Dictionary<string, object> { { "Name", nameOne } }).ToList();
                Expect.AreEqual(1, retrieved.Count);
                retrieved = repo.Query(typeof(KeyHashRepoTestData), new Dictionary<string, object> { { "Name", nameOne } }).CopyAs<KeyHashRepoTestData>().ToList();
                Expect.AreEqual(1, retrieved.Count);
                retrieved = repo.Query(typeof(KeyHashRepoTestData), new { Name = nameOne }).CopyAs<KeyHashRepoTestData>().ToList();
                Expect.AreEqual(1, retrieved.Count);
                retrieved = repo.Query("Name", nameOne).CopyAs<KeyHashRepoTestData>().ToList();
                Expect.AreEqual(1, retrieved.Count);
                retrieved = repo.Query(new { Type = typeof(KeyHashRepoTestData), Name = nameOne }).CopyAs<KeyHashRepoTestData>().ToList();
                Expect.AreEqual(1, retrieved.Count);

                Pass(repo.GetType().Name);
            }
        }

        private IEnumerable<Repository> GetTestRepositories()
        {
            ConsoleLogger logger = new ConsoleLogger { AddDetails = false, UseColors = true };
            logger.StartLoggingThread();
            string schemaName = "TheSchemaName_".RandomLetters(5);
            DaoRepository daoRepo = new DaoRepository(new SQLiteDatabase(".", nameof(RepoQueryTest)), logger, schemaName);
            daoRepo.WarningsAsErrors = false;
            daoRepo.AddType(typeof(KeyHashRepoTestData));
            yield return daoRepo;
            CachingRepository cachingRepo = new CachingRepository(daoRepo, logger);
            yield return cachingRepo;
        }

        [UnitTest]
        public void CanSaveToRepo()
        {
            DataPoint point = new DataPoint()
            {
                Description = "Description_".RandomLetters(5)
            };
            DaoRepository repo = new DaoRepository()
            {
                Database = new SQLiteDatabase(".\\", nameof(CanSaveToRepo))
            };
            repo.AddType<DataPoint>();
            repo.AddType<DataRelationship>();

            string prop1 = "Hello_".RandomLetters(8);
            string prop2 = "banana".RandomLetters(4);
            bool gender = false;
            point.Property("Name", prop1);
            point.Property("LastName", prop2);
            point.Property("Gender", gender);

            point = repo.Save(point);
            Expect.IsTrue(point.Cuid.Length > 0);
            OutLine(point.Cuid);

            DataPoint retrieved = repo.Query<DataPoint>(dp => dp.Cuid == point.Cuid).FirstOrDefault();

            Expect.AreEqual(point.Description, retrieved.Description);
            Expect.AreEqual(prop1, retrieved.Property("Name").Value);
            Expect.AreEqual(prop2, retrieved.Property("LastName").Value);
            Expect.AreEqual(false, retrieved.Property("Gender").Value);
            Expect.AreEqual(prop1, retrieved.Property<string>("Name"));
            Expect.AreEqual(prop2, retrieved.Property<string>("LastName"));
            Expect.AreEqual(gender, retrieved.Property<bool>("Gender"));
        }

        public class Child
        {
            public Child() { Cuid = NCuid.Cuid.Generate(); }
            public string Cuid { get; set; }
            public string ChildName { get; set; }
            public virtual List<Parent> Parents { get; set; }
            public virtual List<Toy> Toys { get; set; }
        }

        public class Toy
        {
            public Toy() { Cuid = NCuid.Cuid.Generate(); }
            public string Cuid { get; set; }
            public virtual Child Child { get; set; }
            public long ChildId { get; set; }
        }

        public class House
        {
            public House() { Cuid = NCuid.Cuid.Generate(); }
            public string Cuid { get; set; }
            public string HouseName { get; set; }
            public virtual List<Parent> Parents { get; set; }
        }

        public class Parent
        {
            public Parent() { Cuid = NCuid.Cuid.Generate(); }
            public string Cuid { get; set; }
            public string ParentName { get; set; }
            public virtual List<Child> Children { get; set; }
            public virtual List<House> Houses { get; set; }
        }

        private static ConsoleLogger GetTestConsoleLogger()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.StartLoggingThread();
            return logger;
        }

        private static AsyncProxyableEcho GetTestAsyncProxyable()
        {
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetClient(out BamServer server, out SecureServiceProxyClient<AsyncProxyableEcho> sspc);
            ConsoleLogger logger = GetTestConsoleLogger();
            ProxyFactory serviceFactory = new ProxyFactory(".\\workspace_".RandomLetters(4), logger);
            AsyncCallbackService callbackService = new AsyncCallbackService(new Bam.Net.Services.AsyncCallback.Data.Dao.Repository.AsyncCallbackRepository(), new AppConf());
            AsyncProxyableEcho testObj = serviceFactory.GetProxy<AsyncProxyableEcho>(server.DefaultHostPrefix.HostName, server.DefaultHostPrefix.Port, logger); // the "server"
            testObj.CallbackService = callbackService;
            return testObj;
        }

        private static void StopServers()
        {
            ServiceProxyTestHelpers.Servers.Each(s => s.Stop());
        }
    }
}
