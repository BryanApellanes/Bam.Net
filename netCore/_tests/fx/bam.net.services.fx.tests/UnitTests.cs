using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.Services.DataReplication.Data;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bam.Net.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
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
    }
}
