using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.DaoRef;
using Bam.Net.Data.Repositories;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;
using Bam.Net.Services.DataReplication;

namespace Bam.Net.Data.Integration.Core.Tests
{
    [IntegrationTestContainer]
    [Serializable]
    public class SqlStringBuilderCrudTests: CommandLineTool
    {
        static HashSet<Database> _testDatabases;
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

        [ConsoleAction]
        [IntegrationTest]
        public void SqlStringBuilderCreateTest()
        {
            HashSet<Database> dbs = DataTools.Setup();
            dbs.Each(db =>
            {
                try
                {
                    ObjectIdentifier id = new ObjectIdentifier();
                    string name = "Name_".RandomLetters(10);
                    SqlStringBuilder sql = db.Sql().Insert(nameof(TestTable), new { Name = name, Uuid = id.Uuid, Cuid = id.Cuid });
                    db.ExecuteSql(sql);
                    RetrieveByNameAndValidate(db, TestTable.OneWhere(c => c.Name == name, db));
                    Pass($"{db.GetType().Name} passed");
                }
                catch (Exception ex)
                {
                    Error($"{db.GetType().Name}: failed: {ex.Message}", ex);
                }
            });
            DataTools.Cleanup(dbs);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void ShouldBeAbleToRetrieveWithDynamicParameter()
        {
            HashSet<Database> dbs = DataTools.Setup();
            dbs.Each(db =>
            {
                try
                {
                    string name = "Name_".RandomLetters(8);
                    TestTable instance = new TestTable();
                    instance.Name = name;
                    instance.Save(db);
                    TestTable retrieved = RetrieveByNameAndValidate(db, instance);

                    SqlStringBuilder sql = db.Sql().Select(nameof(TestTable)).Where(new { Name = name });
                    TestTable queried = sql.ExecuteReader<TestTable>(db).SingleOrDefault();
                    Message.PrintLine("Result type is ({0})", queried.GetType().Name, ConsoleColor.Cyan);

                    Expect.AreEqual(retrieved.Id, queried.Id);
                    Expect.AreEqual(retrieved.Uuid, queried.Uuid);
                    Expect.AreEqual(retrieved.Cuid, queried.Cuid);
                    Expect.AreEqual(retrieved.Name, queried.Name);

                    Pass($"{db.GetType().Name} passed");
                }
                catch (Exception ex)
                {
                    Error($"{db.GetType().Name}: failed: {ex.Message}", ex);
                }
            });
            DataTools.Cleanup(dbs);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void ShouldBeAbleToRetrieveWithDynamicReturn()
        {
            HashSet<Database> dbs = DataTools.Setup();
            dbs.Each(db =>
            {
                try
                {
                    string name = "Name_".RandomLetters(8);
                    TestTable instance = new TestTable();
                    instance.Name = name;
                    instance.Save(db);
                    TestTable retrieved = RetrieveByNameAndValidate(db, instance);

                    SqlStringBuilder sql = db.Sql().Select(nameof(TestTable)).Where(new { Name = name });
                    dynamic queried = sql.ExecuteDynamicReader(db).SingleOrDefault();
                    OutLineFormat("Result type is ({0})", queried.GetType().Name, ConsoleColor.Cyan);

                    Expect.AreEqual(retrieved.Id, (long)queried.Id);
                    Expect.AreEqual(retrieved.Uuid, queried.Uuid);
                    Expect.AreEqual(retrieved.Name, queried.Name);

                    Pass($"{db.GetType().Name} passed");
                }
                catch (Exception ex)
                {
                    Error($"{db.GetType().Name}: failed: {ex.Message}", ex);
                }
            });
            DataTools.Cleanup(dbs);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void ShouldBeAbleToUpdateWithDynamicParameters()
        {
            HashSet<Database> dbs = DataTools.Setup();
            dbs.Each(db =>
            {
                try
                {
                    string name = "Name_".RandomLetters(8);
                    TestTable instance = new TestTable();
                    instance.Name = name;
                    instance.Save(db);
                    TestTable retrieved = RetrieveByNameAndValidate(db, instance);

                    string updatedName = "Updated+".RandomLetters(5);
                    SqlStringBuilder sql2 = db.Sql().Update(nameof(TestTable), new { Name = updatedName }).Where(new { Id = retrieved.Id });
                    db.ExecuteSql(sql2);

                    SqlStringBuilder sql3 = db.Sql();
                    sql3.Select(nameof(TestTable), "*").Where(new { Name = updatedName });
                    TestTable retrieved2 = db.ExecuteReader<TestTable>(sql3).SingleOrDefault();
                    Expect.IsNotNull(retrieved2);
                    Expect.AreEqual(instance.Id, retrieved2.Id);
                    Expect.AreEqual(instance.Uuid, retrieved2.Uuid);
                    Expect.AreEqual(instance.Cuid, retrieved2.Cuid);
                    Expect.AreEqual(updatedName, retrieved2.Name);

                    Pass($"{db.GetType().Name} passed");

                }
                catch (Exception ex)
                {
                    Error($"{db.GetType().Name}: failed: {ex.Message}", ex);
                }
            });
            DataTools.Cleanup(dbs);
        }

        private static TestTable RetrieveByNameAndValidate(Database db, dynamic example)
        {
            SqlStringBuilder sql = db.Sql();
            sql.Select(nameof(TestTable), "*").Where(new { Name = example.Name });
            TestTable retrieved = db.ExecuteReader<TestTable>(sql).SingleOrDefault();
            Expect.IsNotNull(retrieved);
            Expect.AreEqual(retrieved.Id, example.Id);
            Expect.AreEqual(retrieved.Uuid, example.Uuid);
            Expect.AreEqual(retrieved.Cuid, example.Cuid);
            Expect.AreEqual(retrieved.Name, example.Name);
            return retrieved;
        }
    }
}
