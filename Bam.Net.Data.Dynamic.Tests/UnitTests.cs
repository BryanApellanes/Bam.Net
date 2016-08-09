/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Data.SQLite;
using Bam.Net.Data.Dynamic;
using Bam.Net.Data.MsSql;
using Bam.Net.Data.Schema;
using System.IO;
using System.Reflection;

namespace Bam.Net.Data.Dynamic.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
        [UnitTest]
        public void ExtensionExecuteSqlShouldInsert()
        {
            FileInfo inputFile = new FileInfo("c:\\testData\\Db_SillydatabaseNameMap_NormalizEDOnTypes.json");
            SchemaNameMap map = SchemaNameMap.Load(inputFile.FullName);
            Database db = GetDatabase();
            "INSERT INTO {Customer} ({FirstName}, {LastName}, {Birthday}, {Age}) VALUES (@FirstName, @LastName, @Birthday, @Age)"
            .NamedFormat(new 
            { 
                Customer = map.GetTableName("Customer"),
                FirstName = map.GetColumnName("Customer", "FirstName"),
                LastName = map.GetColumnName("Customer", "LastName"),
                Birthday = map.GetColumnName("Customer", "Birthday"),
                Age = map.GetColumnName("Customer", "Age")
            })
            .ExecuteSql(db, new Dictionary<string, object>
            {
                {"FirstName", "Yan"},
                {"LastName", "Ipa"},
                {"Birthday", new DateTime(1976, 11, 1)},
                {"Age", 39}
            });

            DynamicDatabase ddb = new DynamicDatabase(db, map);
            var queryFilter = new
            {
                From = "Customer",
                Where = new
                {
                    FirstName = "Yan",
                    And = new
                    {
                        LastName = "Ipa"
                    }
                }
            };
            IEnumerable<dynamic> results = ddb.Retrieve(queryFilter);
            Expect.IsTrue(results.Count() > 0);
            results.Each(d =>
            {
                Type type = d.GetType();
                PropertyInfo[] properties = type.GetProperties();
                properties.Each(prop =>
                {
                    OutLineFormat("{0}: {1}", ConsoleColor.DarkCyan, prop.Name, prop.GetValue(d));
                });
            });
            ddb.Delete(queryFilter);
            results = ddb.Retrieve(queryFilter);
            Expect.AreEqual(0, results.Count());
        }
        
        [UnitTest]
        public void DynamicCrudTest()
        {
            FileInfo inputFile = new FileInfo("c:\\testData\\Db_SillydatabaseNameMap_NormalizEDOnTypes.json");
            SchemaNameMap map = SchemaNameMap.Load(inputFile.FullName);
            DynamicDatabase db = new DynamicDatabase(GetDatabase(), map);
            var bryan = new
            {
                Type = "Customer",
                Where = new
                {
                    FirstName = "Bam"
                }
            };
            db.Delete(bryan);
            var retrieved = db.RetrieveFirst(bryan);
            Expect.IsNull(retrieved);
            db.Create(Reflect.Combine(bryan, new
            {
                FirstName = "Bam",
                LastName = "Apellanes",
                Birthday = new DateTime(1976, 11, 1),
                Age = 38
            }));
            db.Create(Reflect.Combine(bryan, new
            {
                FirstName = "Bam",
                LastName = "Banana",
                Birthday = new DateTime(1974, 1, 1),
                Age = 10
            }));
            var query = new
            {
                TableName = "Customer",
                Where = new
                {
                    FirstName = "Bam"
                }
            };
            dynamic[] results = db.Retrieve(query).ToArray();
            Expect.AreEqual(2, results.Length);
            OutLine("Results after insert", ConsoleColor.Cyan);
            foreach (object result in results)
            {
                OutLine(result.PropertiesToString(), ConsoleColor.Cyan);
            }
            db.Delete(new
            {
                Type = "Customer",
                Where = new
                {
                    LastName = "Apellanes",
                    Or = new
                    {
                        LastName = "Banana"
                    }
                }
            });
            results = db.Retrieve(query).ToArray();
            Expect.AreEqual(0, results.Length, "Should have got no results");
        }

        private MsSqlDatabase GetDatabase()
        {
            return new MsSqlDatabase("chumsql2", "Db_Sillydatabase", new MsSqlCredentials { UserName = "mssqluser", Password = "mssqlP455w0rd" });
        }
    }
}
