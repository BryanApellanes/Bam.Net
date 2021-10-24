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
using Bam.Net.Data.Npgsql;
using Bam.Net.Logging.Counters;

namespace Bam.Net.Data.Integration.Core.Tests
{
	[IntegrationTestContainer]
	public class DaoQueryTests : CommandLineTool
	{
		static HashSet<Database> _testDatabases;
		public DaoQueryTests()
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
		public void InQueryTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			_testDatabases.Each(db =>
			{
				Message.PrintLine("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				List<TestTable> tables = new List<TestTable>();
				8.Times(i =>
				{
					tables.Add(DataTools.CreateTestTable(4.RandomLetters(), db));
				});

				Expect.AreEqual(8, tables.Count);
				List<ulong> ids = new List<ulong>(tables.Select(t => t.Id.Value).ToArray());
				TestTableCollection retrieved = TestTable.Where(c => c.Id.In(ids.ToArray()), db);
				Expect.AreEqual(tables.Count, retrieved.Count);
			});
		}

		[IntegrationTest]
		public void TopQueryTest()
		{		
			Expect.IsTrue(_testDatabases.Count > 0);
			string startsWith = 8.RandomLetters();
			string methodName = MethodBase.GetCurrentMethod().Name;
			_testDatabases.Each(db =>
			{
				Message.PrintLine("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				8.Times(i =>
				{
					DataTools.CreateTestTable("{0}_{1}"._Format(startsWith, 6.RandomLetters()), db);
				});

				3.Times(i =>
				{
					DataTools.CreateTestTable(5.RandomLetters(), db);
				});

				TestTableCollection top4 = TestTable.Top(4, c => c.Name.StartsWith(startsWith), db);
				Expect.AreEqual(4, top4.Count);
			});
		}

		[IntegrationTest]
		public void EndsWithTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			string endsWith = 7.RandomLetters();
			_testDatabases.Each(db =>
			{
				Message.PrintLine("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				3.Times(i =>
				{
					DataTools.CreateTestTable("{0}{1}"._Format(4.RandomLetters(), endsWith), db);
				});

				5.Times(i =>
				{
					DataTools.CreateTestTable(11.RandomLetters(), db);
				});

				TestTableCollection endsWithResults = TestTable.Where(c => c.Name.EndsWith(endsWith), db);
				Expect.AreEqual(3, endsWithResults.Count);

				TestTableCollection doesntEndWithResults = TestTable.Where(c => c.Name.DoesntEndWith(endsWith), db);
				Expect.IsTrue(doesntEndWithResults.Count > 0);
				doesntEndWithResults.Each(endsWithResults.ToList(), (ewr, tt) =>
				{
					Expect.IsFalse(ewr.Contains(tt));
				});
			});
		}

		[IntegrationTest]
		public void StartsWithTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			string startsWith = 7.RandomLetters();
			_testDatabases.Each(db =>
			{
				Message.PrintLine("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				3.Times(i =>
				{
					DataTools.CreateTestTable("{0}{1}"._Format(startsWith, 4.RandomLetters()), db);
				});

				5.Times(i =>
				{
					DataTools.CreateTestTable(11.RandomLetters(), db);
				});

				TestTableCollection startsWithResults = TestTable.Where(c => c.Name.StartsWith(startsWith), db);
				Expect.AreEqual(3, startsWithResults.Count);

				TestTableCollection doesntEndWithResults = TestTable.Where(c => c.Name.DoesntStartWith(startsWith), db);
				Expect.IsTrue(doesntEndWithResults.Count > 0);
				doesntEndWithResults.Each(startsWithResults.ToList(), (swr, tt) =>
				{
					Expect.IsFalse(swr.Contains(tt));
				});
			});
		}

		[IntegrationTest]
		public void ContainsTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			string contains = 7.RandomLetters();
			_testDatabases.Each(db =>
			{
				Message.PrintLine("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				3.Times(i =>
				{
					DataTools.CreateTestTable("{0}{1}{2}"._Format(4.RandomLetters(), contains, 4.RandomLetters()), db);
				});

				5.Times(i =>
				{
					DataTools.CreateTestTable(11.RandomLetters(), db);
				});

				TestTableCollection containsResults = TestTable.Where(c => c.Name.Contains(contains), db);
				Expect.AreEqual(3, containsResults.Count);

				TestTableCollection doesntContainResults = TestTable.Where(c => c.Name.DoesntContain(contains), db);
				Expect.IsTrue(doesntContainResults.Count > 0);
				doesntContainResults.Each(containsResults.ToList(), (cr, tt) =>
				{
					Expect.IsFalse(cr.Contains(tt));
				});
			});
		}

		[IntegrationTest]
		public void AndTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod()?.Name;
			string nameStartsWith = 4.RandomLetters();
			string descriptionStartsWith = 4.RandomLetters();
			_testDatabases.Each(db =>
			{
				Message.PrintLine("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				TestTable one = DataTools.CreateTestTable("{0}_{1}"._Format(nameStartsWith, 4.RandomLetters()), "{0}_{1}"._Format(descriptionStartsWith, 4.RandomLetters()), db);
				TestTable two = DataTools.CreateTestTable("{0}_{1}"._Format(nameStartsWith, 4.RandomLetters()), "{0}_{1}"._Format(descriptionStartsWith, 4.RandomLetters()), db);
				TestTable three = DataTools.CreateTestTable(nameStartsWith, db);

				TestTableCollection results = TestTable.Where(c => c.Name.StartsWith(nameStartsWith).And(c.Description.StartsWith(descriptionStartsWith)), db);
				TestTableCollection results2 = TestTable.Where(c => c.Name.StartsWith(nameStartsWith) && c.Description.StartsWith(descriptionStartsWith), db);
				Expect.AreEqual(2, results.Count);
				Expect.AreEqual(2, results2.Count);
			});
		}

		[IntegrationTest]
		public void OrTest()
		{
			Expect.IsTrue(_testDatabases.Count > 0);
			string methodName = MethodBase.GetCurrentMethod().Name;
			string one = 4.RandomLetters();
			string two = 4.RandomLetters();
			_testDatabases.Each(db =>
			{
				Message.PrintLine("{0}.{1}: {2}", ConsoleColor.DarkYellow, this.GetType().Name, methodName, db.GetType().Name);
				TestTable first = DataTools.CreateTestTable("{0}_{1}"._Format(one, 4.RandomLetters()), db);
				TestTable second = DataTools.CreateTestTable("{0}_{1}"._Format(two, 4.RandomLetters()), db);
				TestTable third = DataTools.CreateTestTable(8.RandomLetters(), db);

				TestTableCollection results = TestTable.Where(c => c.Name.StartsWith(one).Or(c.Name.StartsWith(two)), db);
				Expect.AreEqual(2, results.Count);
				List<TestTable> list = results.ToList();
				Expect.IsTrue(list.Contains(first));
				Expect.IsTrue(list.Contains(second));
				Expect.IsFalse(list.Contains(third));

				results = TestTable.Where(c => c.Name.StartsWith(one) || c.Name.StartsWith(two), db);
				Expect.AreEqual(2, results.Count);
				list = results.ToList();
				Expect.IsTrue(list.Contains(first));
				Expect.IsTrue(list.Contains(second));
				Expect.IsFalse(list.Contains(third));
			});
		}

		[IntegrationTest]
		public void PagedQueryTest()
		{
			(_testDatabases.Count > 0).IsTrue();
			string name = MethodBase.GetCurrentMethod().Name;
			_testDatabases.Each(db =>
			{
				TestTableCollection testTables = CreateTestTableEntries(name, db);
				testTables = TestTable.Where(c => c.Name.StartsWith(name), db);
				Expect.AreEqual(8, testTables.Count, "There should have been 8 records but there were {0}"._Format(testTables.Count));
				PagedQuery<TestTableColumns, TestTable> q = new PagedQuery<TestTableColumns, TestTable>(new TestTableColumns().Id, testTables.Query, db);
				q.LoadMeta();
				Expect.IsGreaterThan(q.PageCount, 0, "Page count should have been greater than 0");
				Message.PrintLine("Page count was {0} for {1}", ConsoleColor.Cyan, q.PageCount, db.GetType().Name);
				CheckExpectations(q);
			});
		}

        [ConsoleAction]
        [IntegrationTest]
        public void AsyncQueryManualTest()
        {
            _testDatabases = DataTools.Setup();
            Expect.IsTrue(_testDatabases.Count > 0);
            string methodName = MethodBase.GetCurrentMethod()?.Name;
            string one = 4.RandomLetters();
            string two = 4.RandomLetters();
            foreach(Database db in _testDatabases)
            {
                Counter connectionCounter = Stats.Count("ConnectionCount", GetConnectionCount);
                ulong initialCount = connectionCounter.Count;
                foreach (IDbConnectionManager conMan in new IDbConnectionManager[] 
                    {
                        new PerThreadDbConnectionManager(db),
                        new DefaultDbConnectionManager(db)
                    }
                )
                {
                    db.ConnectionManager = conMan;
                    conMan.StateChangeEventHandler = (o, sce) => Message.PrintLine("***** {0} Current state {1}, Original state {2} *****", ConsoleColor.DarkYellow, o.GetType().Name, sce.CurrentState.ToString(), sce.OriginalState.ToString());
                    db.IsNotNull();
                    
                    Message.PrintLine("{0} Connection Count before write {1}", ConsoleColor.Yellow, db.GetType().Name, connectionCounter.Count);

                    List<TestTable> entries = new List<TestTable>();
                    string name = $"{nameof(AsyncQueryManualTest)}.CreateTestData";
                    Timer timer = Stats.Start(name);
                    100.Times((i) => entries.Add(DataTools.CreateTestTable("{0}_{1}"._Format(one, 4.RandomLetters()), db)));
                    timer = Stats.End(name);
                    Message.PrintLine("{0} Writes took {1}", ConsoleColor.Cyan, db.GetType().Name, timer.Duration);

                    Message.PrintLine("Count after write {0}", ConsoleColor.Yellow, connectionCounter.Count);

                    Console.WriteLine(connectionCounter.Count);                    

                    Message.PrintLine("Count before read {0}", ConsoleColor.Yellow, connectionCounter.Count);
                    name = $"{nameof(AsyncQueryManualTest)}.ReadTestData";
                    timer = Stats.Start(name);
                    Parallel.ForEach(entries, (tt) =>
                    {
                        TestTable val = TestTable.FirstOneWhere(c => c.Name == tt.Name, db);
                        Message.PrintLine("{0}", ConsoleColor.Cyan, val.Name);
                    });
                    timer = Stats.End(name);
                    Message.PrintLine("Parallel reads took {0}", ConsoleColor.Cyan, timer.Duration);
                    Message.PrintLine("Count after read {0}", ConsoleColor.Yellow, connectionCounter.Count);
                    Console.WriteLine("{0}: {1}", db.GetType().Name, connectionCounter.Count);
                    
                    Stats.Start("Deleting");
                    TestTable.LoadAll(db).ToList().ForEach(tt => tt.Delete(db));
                    Message.PrintLine("{0}: Deletes took {1} milliseconds", ConsoleColor.Yellow, db.GetType().Name, Stats.End("Deleting").Value);
                    (connectionCounter.Count <= initialCount + ((ulong)(conMan.MaxConnections * 2))).IsTrue("Connection count higher than expected");
                }
            }            
        }

        private ulong GetConnectionCount()
        {
            return (ulong)"netstat".ToStartInfo("-an", "C:\\Windows\\System32").Run().StandardOutput.DelimitSplit("\r\n").Length;
        }

		public class TestTableQuery : Query<TestTableColumns, TestTable>
		{
			public TestTableQuery(WhereDelegate<TestTableColumns> where, OrderBy<TestTableColumns> orderBy = null, Database db = null)
				: base(where, orderBy, db)
			{ }
		}
        
		private static TestTableCollection CreateTestTableEntries(string name, Database db)
		{
			TestTableCollection testTables = new TestTableCollection();
			8.Times(i =>
			{
				testTables.Add(DataTools.CreateTestTable("{0}_{1}"._Format(name, 10.RandomLetters()), "Description ".RandomLetters(6), db, false));
			});
			4.Times(i =>
			{
				testTables.Add(DataTools.CreateTestTable("{0}_{1}"._Format(8.RandomLetters(), name), "Different Description ".RandomLetters(4), db, false));
			});
			testTables.Save(db);
			return testTables;
		}

		private static void CheckExpectations(PagedQuery<TestTableColumns, TestTable> q)
		{
            q.NextPage(out IEnumerable<TestTable> results).IsTrue();
            Expect.AreEqual(q.PageSize, results.ToArray().Length);
			OutLine("**** First Page: ", ConsoleColor.Yellow);
			results.Each(r =>
			{
				OutLine(r.PropertiesToString(), ConsoleColor.Cyan);
			});
			q.NextPage(out results).IsTrue();
			Expect.AreEqual(3, results.ToArray().Length);
			OutLine("**** Second Page: ", ConsoleColor.Yellow);
			results.Each(r =>
			{
				OutLine(r.PropertiesToString(), ConsoleColor.DarkCyan);
			});
		}
	}
}
