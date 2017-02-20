/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Testing;
using Bam.Net.Incubation;
using Bam.Net.DaoRef;
using Bam.Net.CommandLine;
using Bam.Net.Data.MsSql;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing.Integration;

namespace Bam.Net.Data.Tests.Integration
{
	[IntegrationTestContainer]
	public class DaoCrudTests: CommandLineTestInterface
	{
		static HashSet<Database> _testDatabases;
		public DaoCrudTests()
		{
		}
		
		[IntegrationTestSetup]
		public void Setup()
		{
			_testDatabases = DataTools.Setup();
		}

		[IntegrationTestCleanup]
		public void CleanUp()
		{
			DataTools.Cleanup(_testDatabases);
		}

		[IntegrationTest]
		public void CreateTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			_testDatabases.Each(db =>
			{
				OutLineFormat("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				TestTable test = new TestTable(db);
				test.Name = 8.RandomLetters();
				test.Save(db);

				Expect.IsGreaterThan(test.Id.Value, 0, "Id should have been greater than 0");
				OutLineFormat("{0}", ConsoleColor.Cyan, test.PropertiesToString());
			});
		}

		[IntegrationTest]
		public void RetrieveTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			_testDatabases.Each(db =>
			{
				OutLineFormat("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				string name = 6.RandomLetters();
				TestTable test = new TestTable(db);
				test.Name = name;
				test.Save(db);

				TestTable check = TestTable.OneWhere(t => t.Name == name, db);
				Expect.IsNotNull(check);
				Expect.AreEqual(test.Id, check.Id);
				Expect.AreEqual(name, check.Name);

				TestTable check2 = TestTable.OneWhere(t => t.Id == test.Id, db);
				Expect.IsNotNull(check2);
				Expect.AreEqual(test.Id, check2.Id);
				Expect.AreEqual(name, check2.Name);
			});
		}

		[IntegrationTest]
		public void UpdateTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			_testDatabases.Each(db =>
			{
				OutLineFormat("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				string name = 6.RandomLetters();
				TestTable test = new TestTable(db);
				test.Name = name;
				test.Save(db);

				TestTable toUpdate = TestTable.OneWhere(t => t.Name == name, db);
				string changeTo = 8.RandomLetters();
				toUpdate.Name = changeTo;
				toUpdate.Save(db);

				TestTable check = TestTable.OneWhere(t => t.Name == changeTo, db);
				Expect.AreEqual(test.Id, check.Id);
				Expect.AreEqual(toUpdate.Id, check.Id);
				Expect.AreEqual(changeTo, check.Name);
			});
		}

		[IntegrationTest]
		public void DeleteTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			_testDatabases.Each(db =>
			{
				OutLineFormat("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				string name = 6.RandomLetters();
				TestTable test = new TestTable(db);
				test.Name = name;
				test.Save(db);

				TestTable toDelete = TestTable.OneWhere(t => t.Id == test.Id, db);
				Expect.IsNotNull(toDelete);
				toDelete.Delete(db);

				TestTable shouldBeNull = TestTable.OneWhere(t => t.Id == test.Id, db);
				Expect.IsNull(shouldBeNull);
			});				
		}

		[Serializable]
		public class TestSerializable
		{
			public TestSerializable(string name)
			{
				this.Name = name;
			}
			public string Name { get; set; }
		}

		[IntegrationTest]
		public void PropertyTypeTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			_testDatabases.Each(db =>
			{
				OutLineFormat("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				DateTime utc = DateTime.UtcNow;
				DaoReferenceObject obj = new DaoReferenceObject(db);
				obj.IntProperty = RandomNumber.Between(0, 100);
				obj.DecimalProperty = 10.15m;
				obj.LongProperty = 99999;
				obj.DateTimeProperty = utc;
				obj.BoolProperty = true;
				string testName = 16.RandomLetters();
				obj.ByteArrayProperty = new TestSerializable(testName).ToBinaryBytes();
				Instant now = new Instant();
				obj.StringProperty = now.ToString();

				obj.Save(db);

				DaoReferenceObject retrieved = DaoReferenceObject.OneWhere(c => c.Id == obj.Id, db);
				Expect.AreEqual(obj.Id, retrieved.Id, "Ids didn't match");
				Expect.AreEqual(obj.IntProperty, retrieved.IntProperty, "IntProperty didn't match");
				Expect.AreEqual(obj.DecimalProperty, retrieved.DecimalProperty, "DecimalProperty didn't match");
				Expect.AreEqual(obj.LongProperty, retrieved.LongProperty, "LongProperty didn't match");
				DateTime shouldBe = obj.DateTimeProperty.Value.DropMilliseconds();
				DateTime _is = retrieved.DateTimeProperty.Value.DropMilliseconds();
				Expect.AreEqual(shouldBe, _is, "DateTimeProperty didn't match");
				Expect.AreEqual(obj.BoolProperty, retrieved.BoolProperty, "BoolProperty didn't match");
				Expect.AreEqual(obj.StringProperty, retrieved.StringProperty, "StringProperty didn't match");
				TestSerializable deserialized = obj.ByteArrayProperty.FromBinaryBytes<TestSerializable>();
				Expect.AreEqual(testName, deserialized.Name);
				Instant then = Instant.FromString(retrieved.StringProperty);
				Expect.AreEqual(now.ToDateTime(), then.ToDateTime());
			});
		}

		[IntegrationTest]
		public void ChildListTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			_testDatabases.Each(db =>
			{
				OutLineFormat("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				TestTable parent = new TestTable(db);
				parent.Name = "Parent Test";
				parent.Save(db);
				TestFkTable child = parent.TestFkTablesByTestTableId.AddNew();
				child.Name = "Child ({0})"._Format(6.RandomLetters());

				parent.Save(db);

				TestTable retrieved = TestTable.OneWhere(c => c.Id == parent.Id, db);
				Expect.AreEqual(1, retrieved.TestFkTablesByTestTableId.Count);
				Expect.AreEqual(child.Name, retrieved.TestFkTablesByTestTableId[0].Name);

				TestFkTable child2 = new TestFkTable();
				child2.Name = "Child 2 ({0})"._Format(6.RandomLetters());
				parent.TestFkTablesByTestTableId.Add(child2);
				parent.Save(db);

				retrieved = TestTable.OneWhere(c => c.Id == parent.Id, db);
				Expect.AreEqual(2, retrieved.TestFkTablesByTestTableId.Count);
				List<string> names = retrieved.TestFkTablesByTestTableId.Select(c => c.Name).ToList();
				Expect.IsTrue(names.Contains(child.Name));
				Expect.IsTrue(names.Contains(child2.Name));
			});
		}

		[IntegrationTest]
		public void XrefListTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			_testDatabases.Each(db =>
			{
				OutLineFormat("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				List<Left> lefts = new List<Left>();
				4.Times(i =>
				{
					lefts.Add(DataTools.CreateLeft("Left ".RandomLetters(6), db));
				});
				List<Right> rights = new List<Right>();
				3.Times(i =>
				{
					rights.Add(DataTools.CreateRight("Right ".RandomLetters(5), db));
				});

				lefts.Each(l =>
				{
					rights.Each(r =>
					{
						l.Rights.Add(r);
					});

					l.Save(db);
				});

				Expect.AreEqual(3, rights.Count);
				Expect.AreEqual(4, lefts.Count);
				rights.Each(r =>
				{
					Expect.AreEqual(4, r.Lefts.Count);
				});

				lefts.Each(l =>
				{
					Expect.AreEqual(3, l.Rights.Count);
				});
			});
		}

	}
}
