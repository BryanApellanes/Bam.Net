using System;
using System.Collections.Generic;
using System.Linq;
using Bam.Net.CommandLine;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.Services.DataReplication.Data;
using Bam.Net.Testing;
using Bam.Net.Caching;
using Bam.Net.Testing.Unit;
using Bam.Net.Services.OpenApi;
using Bam.Net.Services.DataReplication;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public partial class UnitTests : CommandLineTool
    {
        static UnitTests()
        {
            Log.DebugOut = false;
            Log.TraceOut = false;
        }

        [UnitTest]
        public void CanSerializeAndDeserializeDataPropertyList()
        {
            DataPropertySet propList = new DataPropertySet();
            propList.Add("Prop1", true);
            propList.Add("Prop2", false);
            propList.Add("Name", "Banana");
            byte[] serialized = propList.ToBinaryBytes();

            DataPropertySet deserialized = serialized.FromBinaryBytes<DataPropertySet>();
            Expect.AreEqual(3, deserialized.Count);
            Expect.IsTrue(deserialized.Value<bool>("Prop1"));
            Expect.IsFalse(deserialized.Value<bool>("Prop2"));
            Expect.AreEqual("Banana", deserialized.Value<string>("Name"));
        }

        [UnitTest]
        public void ThrowsOnDuplicateName()
        {
            DataPropertySet propList = new DataPropertySet
            {
                { "Name", "some value" }
            };
            Expect.Throws(() =>
            {
                propList.Add("Name", "bad bad");
            },
            (ex) =>
            {
                Message.PrintLine("{0}", ConsoleColor.Yellow, ex.Message);
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
            DataPropertySet dpc = value.ToDataPropertyCollection();
            Expect.AreEqual(value.Monkey, dpc.Value<string>("Monkey"));
            Expect.IsFalse(dpc.Value<bool>("HasTail"));

            TestMonkey valueAgain = dpc.ToInstance<TestMonkey>();
            Expect.AreEqual(valueAgain.Monkey, value.Monkey);
            Expect.AreEqual(valueAgain.HasTail, value.HasTail);
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
        public void RequestRouterTest()
        {
            RequestRouter router = new RequestRouter("api");
            RequestRoute route = router.ToRequestRoute("bam://bamapps.net/api/v1/monkey/5?blah=one&blah2=two");
            Expect.AreEqual("bam", route.Protocol);
            Expect.AreEqual("bamapps.net", route.Domain);
            Expect.AreEqual("api", route.PathName);
            Expect.AreEqual("v1/monkey/5?blah=one&blah2=two", route.PathAndQuery);
        }

        [UnitTest]
        public void RequestParserCanTakeJustADomain()
        {
            RouteParser parser = new RouteParser("{Protocol}://{Domain}");
            Dictionary<string, string> values = parser.ParseRouteInstance("http://v-o.bamapps.net");
            RequestRoute route = values.ToInstance<RequestRoute>();
            Expect.AreEqual(2, values.Count, $"Expected 2 but got {values.Count}");
            Expect.AreEqual("http", route.Protocol);
            Expect.AreEqual("v-o.bamapps.net", route.Domain);
        }
        
        [UnitTest]
        public void RequestParserCanTakeEmptyPath()
        {
            RouteParser parser = new RouteParser("{Protocol}://{Domain}/{PathAndQuery}");
            Dictionary<string, string> values = parser.ParseRouteInstance("http://v-o.bamapps.net/");
            RequestRoute route = values.ToInstance<RequestRoute>();
            Expect.AreEqual(3, values.Count, $"Expected 3 but got {values.Count}");
            Expect.AreEqual("http", route.Protocol);
            Expect.AreEqual("v-o.bamapps.net", route.Domain);
            Expect.AreEqual("", route.PathAndQuery);
        }

        [UnitTest]
        public void ServiceRequestRouterTest()
        {
            RequestRouter router = new RequestRouter("api");
            RequestRoute route = router.ToRequestRoute("http://service.bamapps.net/api/echo/send");
            Expect.AreEqual("http", route.Protocol);
            Expect.AreEqual("service.bamapps.net", route.Domain);
            Expect.AreEqual("echo/send", route.PathAndQuery);
        }
        
        [UnitTest]
        public void GetHashThrowsIfNoKeyProperties()
        {
            Expect.Throws(() => new CompositeKeyRepoThrowsData().GetHashCode(), (ex) => Message.PrintLine("Exception thrown as expected: {0}", ConsoleColor.Green, ex.Message));
            Expect.Throws(() => new CompositeKeyRepoThrowsData().GetLongKeyHash(), (ex) => Message.PrintLine("Exception thrown as expected: {0}", ConsoleColor.Green, ex.Message));
        }

        [UnitTest]
        public void GetHashReturnsSameValueForDifferentInstances()
        {
            string name = 16.RandomLetters();
            string otherProp = 32.RandomLetters();
            CompositeKeyRepoTestData one = new CompositeKeyRepoTestData { Name = name, SomeOtherUniqueProperty = otherProp };
            CompositeKeyRepoTestData two = new CompositeKeyRepoTestData { Name = name, SomeOtherUniqueProperty = otherProp };
            CompositeKeyRepoTestData three = new CompositeKeyRepoTestData { Name = name, SomeOtherUniqueProperty = "different" };
            Expect.IsFalse(one == two);
            Expect.AreEqual(one.GetHashCode(), two.GetHashCode());
            Expect.AreEqual(one.GetLongKeyHash(), two.GetLongKeyHash());

            Expect.IsFalse(one.GetHashCode().Equals(three.GetHashCode()));
            Expect.IsFalse(two.GetHashCode().Equals(three.GetHashCode()));

            OutLine(one.GetLongKeyHash().ToString());
        }
    }
}
