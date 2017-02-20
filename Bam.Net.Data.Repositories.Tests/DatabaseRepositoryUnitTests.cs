using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Schema;
using Bam.Net.Schema.Org;
using Bam.Net.Data.SQLite;
using Bam.Net.Data.Tests.Integration;
using System.Data.Common;

namespace Bam.Net.Data.Repositories.Tests
{
    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }

    public class Employee: Person
    {
        public decimal Salary { get; set; }
    }

    public class TestTypeInheritanceSchemaGenerator: TypeInheritanceSchemaGenerator
    {
        public void TestAddSchemaTables(SchemaManager mgr)
        {
            TypeSchema typeSchema = CreateTypeSchema(typeof(Employee));
            AddSchemaTables(typeSchema, mgr);            
        }
    }

    [Serializable]
    public class DatabaseRepositoryUnitTests: CommandLineTestInterface
    {
        // Determine from type if it extends something other than object
        // if so create a TableTypeInheritanceChain
        // 
        [ConsoleAction]
        public void OutputTypeInheritanceDescriptor()
        {
            TypeInheritanceDescriptor descriptor = new TypeInheritanceDescriptor(typeof(Employee));
            OutLine(descriptor.ToString(), ConsoleColor.Cyan);
        }
        
        [UnitTest]
        public void TypeInheritanceSchemaGeneratorShouldAddTablesForInheritedTypes()
        {
            SchemaManager mgr = new SchemaManager { AutoSave = false };
            TestTypeInheritanceSchemaGenerator gen = new TestTypeInheritanceSchemaGenerator();
            gen.TestAddSchemaTables(mgr);
            Expect.AreEqual(2, mgr.CurrentSchema.Tables.Length);
            Expect.IsNotNull(mgr.GetTable("Employee"), "Employee table wasn't added");
            Expect.IsNotNull(mgr.GetTable("Person"), "Person table wasn't added");
            Expect.AreEqual(3, mgr.GetTable("Person").Columns.Length); // plus cuid
            Expect.AreEqual(2, mgr.GetTable("Employee").Columns.Length);
            Expect.AreEqual(1, mgr.CurrentSchema.ForeignKeys.Length);
            ForeignKeyColumn fk = mgr.CurrentSchema.ForeignKeys[0];
            Expect.AreEqual("Employee", fk.ReferencingClass);
            Expect.AreEqual("Id", fk.ReferencedKey);
        }

        [UnitTest]
        public void TypeInheritanceWriterWritesToMultipleTables()
        {
            SQLiteDatabase db = new SQLiteDatabase($".\\{nameof(TypeInheritanceSchemaGeneratorShouldAddTablesForInheritedTypes)}", "Data");
            TryDrop(db, typeof(Employee));
            TypeSchemaScriptWriter schemaWriter = new TypeSchemaScriptWriter();
            schemaWriter.CommitSchema(db, typeof(Employee));

            TypeInheritanceSqlWriter writer = new TypeInheritanceSqlWriter();
            List<SqlStringBuilder> sqls = writer.GetInsertStatements(new Employee { Name = "test", Salary = 500 }, db);
            IParameterBuilder pb = db.GetService<IParameterBuilder>();
            long id =  db.QuerySingle<long>(sqls[0]);
            sqls.Rest(1, sql =>
            {
                List<DbParameter> dbParams = new List<DbParameter>();
                dbParams.Add(db.CreateParameter("Id", id));
                dbParams.AddRange(pb.GetParameters(sql));
                db.ExecuteSql(sql, dbParams.ToArray());
            });
            OutLine("yay");
        }

        [UnitTest]
        public void WriteSchemaScriptTest()
        {
            HashSet<Database> databases = DataTools.Setup(db =>
            {
                TryDrop(db, "Employee");
                TryDrop(db, "Person");
            }, "ExtendedDao");
            TypeSchemaScriptWriter tssw = new TypeSchemaScriptWriter();
            List<UnitTestResult> failures = new List<UnitTestResult>();
            foreach(Database db in databases)
            {
                try
                {
                    SqlStringBuilder sql = tssw.WriteSchemaScript(db, typeof(Employee));
                    db.ExecuteSql(sql);
                }
                catch (Exception ex)
                {
                    failures.Add(new UnitTestResult { Exception = ex.Message, Description = $"Database type: {db.GetType().Name}" });
                }              
            }

            Expect.IsTrue(failures.Count == 0, string.Join("\r\n", failures.Select(r => $"{r.Description}\r\n{r.Exception}\r\n").ToArray()));
        }

        [UnitTest]
        public void SchemaManagerIdAsForeignKeyTest()
        {
            SchemaManager mgr = GetTestSchemaManager();
            OutLine(mgr.CurrentSchema.ToJson(true));
        }

        private static SchemaManager GetTestSchemaManager()
        {
            SchemaManager mgr = new SchemaManager();
            mgr.AddTable("Person");
            mgr.AddColumn("Person", "Id", DataTypes.Long);
            mgr.AddColumn("Person", "Name", DataTypes.String);
            mgr.AddTable("Employee");
            mgr.AddColumn("Employee", "Id", DataTypes.Long);
            mgr.AddColumn("Employee", "Salary", DataTypes.Decimal);
            SchemaResult result = mgr.SetForeignKey("Person", "Employee", "Id", "Id");
            Expect.IsTrue(result.Success, $"Message: {result.Message}\r\nException: {result.ExceptionMessage}");
            return mgr;
        }
        
        private void TryDrop(Database db, params Type[] types)
        {
            HashSet<Type> unique = new HashSet<Type>();
            types.Each(type =>
            {
                TypeInheritanceDescriptor inheritance = new TypeInheritanceDescriptor(type);
                inheritance.Chain.Each(t =>
                {
                    unique.Add(t.Type);
                });
            });
            unique.Each(type =>
            {
                TryDrop(db, type.Name);
            });
        }

        private void TryDrop(Database db, string tableName)
        {
            try
            {
                db.ExecuteSql(db.GetService<SchemaWriter>().WriteDropTable(tableName));
            }
            catch (Exception ex)
            {
                OutLineFormat("Attempt to drop table {0} threw exception: {1}", ConsoleColor.Yellow, tableName, ex.Message);
            }
        }
    }
}
