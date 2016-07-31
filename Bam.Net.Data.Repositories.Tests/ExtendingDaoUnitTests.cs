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
    public class ExtendingDaoUnitTests: CommandLineTestInterface
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

        //[UnitTest]
        //public void TypeInheritanceWriterWritesToMultipleTables()
        //{
        //    TypeInheritanceWriter writer = new TypeInheritanceWriter();
        //}

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
    }
}
