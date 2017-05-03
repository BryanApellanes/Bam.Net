using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.DaoRef;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;

namespace Bam.Net.Data.Tests.Integration
{
    [IntegrationTestContainer]
    [Serializable]
    public class SqlStringBuilderCrudTests: CommandLineTestInterface
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

        [IntegrationTest]
        public void CreateTest()
        {
        }

        [ConsoleAction]
        [IntegrationTest]
        public void UpdateWithDynamicParameterTests()
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
                    SqlStringBuilder sql = db.Sql();
                    sql.Select(nameof(TestTable), "*").Where(new { Name = instance.Name });
                    TestTable retrieved = db.ExecuteReader<TestTable>(sql).SingleOrDefault();
                    Expect.IsNotNull(retrieved);
                    Expect.AreEqual(retrieved.Id, instance.Id);
                    Expect.AreEqual(retrieved.Uuid, instance.Uuid);
                    Expect.AreEqual(retrieved.Cuid, instance.Cuid);
                    Expect.AreEqual(retrieved.Name, instance.Name);

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

                }catch(Exception ex)
                {
                    Error($"{db.GetType().Name}: failed: {ex.Message}", ex);
                }
            });
            DataTools.Cleanup(dbs);
        }
    }
}
