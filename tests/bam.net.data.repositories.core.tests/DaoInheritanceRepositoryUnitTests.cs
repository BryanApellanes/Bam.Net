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
using System.Data.Common;
using Bam.Net.Testing.Unit;

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
    public class DaoInheritanceRepositoryUnitTests: CommandLineTool
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

        private static SchemaManager GetTestSchemaManager()
        {
            SchemaManager mgr = new SchemaManager();
            mgr.AddTable("Person");
            mgr.AddColumn("Person", "Id", DataTypes.ULong);
            mgr.AddColumn("Person", "Name", DataTypes.String);
            mgr.AddTable("Employee");
            mgr.AddColumn("Employee", "Id", DataTypes.ULong);
            mgr.AddColumn("Employee", "Salary", DataTypes.Decimal);
            SchemaManagerResult managerResult = mgr.SetForeignKey("Person", "Employee", "Id", "Id");
            Expect.IsTrue(managerResult.Success, $"Message: {managerResult.Message}\r\nException: {managerResult.ExceptionMessage}");
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
                Message.PrintLine("Attempt to drop table {0} threw exception: {1}", ConsoleColor.Yellow, tableName, ex.Message);
            }
        }
    }
}
