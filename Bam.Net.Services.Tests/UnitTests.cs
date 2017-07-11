using System;
using System.Collections.Generic;
using System.Linq;
using Bam.Net.CommandLine;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.CoreServices.DistributedHashTable.Data;
using Bam.Net.Testing;
using Bam.Net.Caching;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public partial class UnitTests : CommandLineTestInterface
    {
        [UnitTest]
        public void CanSerializeAndDeserializeDataPropertyList()
        {
            DataPropertyCollection propList = new DataPropertyCollection();
            propList.Add("Prop1", true);
            propList.Add("Prop2", false);
            propList.Add("Name", "Banana");
            byte[] serialized = propList.ToBinaryBytes();

            DataPropertyCollection deserialized = serialized.FromBinaryBytes<DataPropertyCollection>();
            Expect.AreEqual(3, deserialized.Count);
            Expect.IsTrue(deserialized.Value<bool>("Prop1"));
            Expect.IsFalse(deserialized.Value<bool>("Prop2"));
            Expect.AreEqual("Banana", deserialized.Value<string>("Name"));
        }

        [UnitTest]
        public void ThrowsOnDuplicateName()
        {
            DataPropertyCollection propList = new DataPropertyCollection();
            propList.Add("Name", "some value");
            Expect.Throws(() =>
            {
                propList.Add("Name", "bad bad");
            },
            (ex) =>
            {
                OutLineFormat("{0}", ConsoleColor.Yellow, ex.Message);
            }, "Should have thrown exception");
        }
        
        public class TestMonkey
        {
            public string Monkey { get; set; }
            public bool HasTail { get; set; }
        }
        [UnitTest]
        public void ToAndFromDataPropertyCollection()
        {
            TestMonkey value = new TestMonkey { Monkey = "Yay", HasTail = false };
            DataPropertyCollection dpc = value.ToDataPropertyCollection();
            Expect.AreEqual(value.Monkey, dpc.Value<string>("Monkey"));
            Expect.IsFalse(dpc.Value<bool>("HasTail"));

            TestMonkey valueAgain = dpc.ToInstance<TestMonkey>();
            Expect.AreEqual(valueAgain.Monkey, value.Monkey);
            Expect.AreEqual(valueAgain.HasTail, value.HasTail);
        }

        [UnitTest]
        public void CanSaveToRepo()
        {
            DataPoint point = new DataPoint();
            point.Description = "Description_".RandomLetters(5);
            DaoRepository repo = new DaoRepository();
            repo.Database = new SQLiteDatabase(".\\", nameof(CanSaveToRepo));
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

        [UnitTest]
        public void LoggingStartStopTest()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            OutLine("starting", ConsoleColor.DarkYellow);
            logger.StartLoggingThread();
            OutLine("started", ConsoleColor.DarkCyan);
            for (int i = 0; i < 100; i++)
            {
                logger.AddEntry("Message before restart {0}", LogEventType.Information, i.ToString());
            }
            OutLine("stopping", ConsoleColor.Yellow);
            logger.StopLoggingThread();
            OutLine("stopped", ConsoleColor.Cyan);
            logger.RestartLoggingThread();
            OutLine("re-started", ConsoleColor.Blue);
            for (int i = 0; i < 150; i++)
            {
                logger.AddEntry("Message after restart {0}", LogEventType.Information, i.ToString());
            }
            logger.StopLoggingThread();
            OutLine("stopped again", ConsoleColor.Cyan);
        }

        [UnitTest]
        public void TypeSchemaCheck()
        {
            TypeSchemaGenerator gen = new TypeSchemaGenerator();
            TypeSchema ts = gen.CreateTypeSchema(new[] { typeof(Parent) });
            ts.Tables.Each(t => OutFormat("{0}\r\n", ConsoleColor.Blue, t.Name));
            ts.ForeignKeys.Each(fk => OutFormat("{0}->{1}\r\n", ConsoleColor.Cyan, fk.ForeignKeyType.Name, fk.PrimaryKeyType.Name));
            ts.Xrefs.Each(x => OutFormat("{0}<->{1}\r\n", ConsoleColor.DarkGreen, x.Left.Name, x.Right.Name));
            Expect.AreEqual(1, ts.ForeignKeys.Count);
            Expect.AreEqual(2, ts.Xrefs.Count);
        }

        [UnitTest]
        public void DataRelationshipTests()
        {
            Parent p = new Parent { ParentName = "Parent Name Value" };
            Child c = new Child { ChildName = "Child Name Value" };
            House h = new House { HouseName = "House Name Value" };
            p.Children = new List<Child>();
            p.Children.Add(c);
            p.Houses = new List<House>();
            p.Houses.Add(h);

            HashSet<DataRelationship> rels = DataRelationship.FromInstance(p);
            foreach(DataRelationship rel in rels)
            {
                OutFormat(rel.PropertiesToString());
            }
            Expect.AreEqual(2, rels.Count);
        }


        [UnitTest]
        public void ApiRouteParserGetsProtocolAndHostTest()
        {
            RouteParser parser = new RouteParser("{Protocol}://{Domain}/api/{PathAndQuery}");
            Dictionary<string, string> values = parser.ParseRouteInstance("bam://bamapps.net/api/v1/monkey/5");
            Expect.AreEqual(3, values.Count, $"Expected 3 but got {values.Count}");
            RequestRoute requestDescriptor = values.ToInstance<RequestRoute>();
            Expect.AreEqual("bam", requestDescriptor.Protocol);
            Expect.AreEqual("bamapps.net", requestDescriptor.Domain);
            Expect.AreEqual("v1/monkey/5", requestDescriptor.PathAndQuery);
        }

        [UnitTest]
        public void ApiRouteDescriptorParseTest()
        {
            RouteParser parser = new RouteParser("{Protocol}://{Domain}/api/{PathAndQuery}");
            Dictionary<string, string> values = parser.ParseRouteInstance("bam://bamapps.net/api/v1/monkey/5");
            Expect.AreEqual(3, values.Count, $"Expected 3 but got {values.Count}");
            RequestRoute route = values.ToInstance<RequestRoute>();
            Expect.AreEqual("bam", route.Protocol);
            Expect.AreEqual("bamapps.net", route.Domain);
            Expect.AreEqual("v1/monkey/5", route.PathAndQuery);
        }

        [UnitTest]
        public void RequestDescriptorTest()
        {
            RequestRouter router = new RequestRouter("api");
            RequestRoute route = router.ParseUrl("bam://bamapps.net/api/v1/monkey/5?blah=one&blah2=two");
            Expect.AreEqual("bam", route.Protocol);
            Expect.AreEqual("bamapps.net", route.Domain);
            Expect.AreEqual("v1/monkey/5?blah=one&blah2=two", route.PathAndQuery);
        }
       
        [UnitTest]
        public void SavingKeyHashRepoDataShouldntDuplicate()
        {
            ConsoleLogger logger = new ConsoleLogger { AddDetails = false, UseColors = true };
            logger.StartLoggingThread();
            string schemaName = "TheSchemaName_".RandomLetters(5);
            DaoRepository repo = new DaoRepository(new SQLiteDatabase(".", nameof(SavingKeyHashRepoDataShouldntDuplicate)), logger, schemaName);
            repo.WarningsAsErrors = false;
            repo.AddType(typeof(KeyHashRepoTestData));
            CachingRepository cachingRepo = new CachingRepository(repo, logger);

            string nameOne = 32.RandomLetters();
            string valueOne = 16.RandomLetters();
            KeyHashRepoTestData one = new KeyHashRepoTestData { Name = nameOne, SomeOtherUniqueProperty = valueOne };
            KeyHashRepoTestData two = new KeyHashRepoTestData { Name = nameOne, SomeOtherUniqueProperty = valueOne };
            Expect.AreEqual(one, two);

            one.Save<KeyHashRepoTestData>(cachingRepo);
            var queryParams = new { Name = nameOne };
            cachingRepo.Cache<KeyHashRepoTestData>(queryParams);
            List<KeyHashRepoTestData> retrieved = cachingRepo.Query<KeyHashRepoTestData>(queryParams).ToList();
            Expect.AreEqual(1, retrieved.Count);
            two.Save<KeyHashRepoTestData>(cachingRepo);
            retrieved = cachingRepo.Query<KeyHashRepoTestData>(queryParams).ToList();
            Expect.AreEqual(1, retrieved.Count);
        }

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
    }
}
