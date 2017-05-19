using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using Bam.Net.Services.DistributedService.Data;
using Bam.Net.Services.DistributedService.Files;
using Bam.Net.Testing;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
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
        public void CanCopyLoadedAssembly()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            FileInfo currentFile = currentAssembly.GetFileInfo();
            FileInfo copyTo = currentFile.CopyFile(nameof(CanCopyLoadedAssembly));
            Expect.IsTrue(copyTo.Exists);            
            OutLine(copyTo.FullName, ConsoleColor.Cyan);
            copyTo.Delete();
        }

        
        [UnitTest]
        public void FileServiceTest()
        {
            SQLiteDatabase db = new SQLiteDatabase(".\\", nameof(FileServiceTest));            
            FileService fmSvc = new FileService(new DaoRepository(db));
            fmSvc.ChunkLength = 111299;
            FileInfo testDataFile = new FileInfo("C:\\testData\\TestDataFile.dll");            
            ChunkedFileDescriptor chunkedFile = fmSvc.StoreFileChunksInRepo(testDataFile);
            FileInfo writeTo = new FileInfo($".\\{nameof(FileServiceTest)}_restored");
            DateTime start = DateTime.UtcNow;
            FileInfo written = fmSvc.RestoreFile(chunkedFile.FileHash, writeTo.FullName, true);
            TimeSpan took = DateTime.UtcNow.Subtract(start);
            OutLine(took.ToString(), ConsoleColor.Cyan);
            Expect.IsTrue(written.Exists);
            Expect.AreEqual(testDataFile.Md5(), written.Md5(), "file content didn't match");
        }
    }
}
