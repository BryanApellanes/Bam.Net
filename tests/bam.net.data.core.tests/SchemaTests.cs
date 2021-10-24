/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Bam.Net.CommandLine;
using Bam.Net.Data.Schema;
using Bam.Net.Javascript;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Tests
{
    [Serializable]
    public class SchemaTests : CommandLineTool
    {
        [UnitTest]
        public static void SchemaManagerGetSchema()
        {
            SchemaManager manager = new SchemaManager();
            SchemaDefinition schema = manager.SetSchema("test");
            Expect.IsNotNull(schema);
            Expect.IsTrue(File.Exists(schema.File));
            DeleteSchema(schema);
        }

        internal static void DeleteSchema(SchemaDefinition schema)
        {
            try
            {
                File.Delete(schema.File);
            }
            catch (Exception ex)
            {
                OutLineFormat("An error occurred deleting schema file: {0}", ConsoleColor.Red, ex.Message);
            }
        }

        [UnitTest]
        public static void SchemaManagerAddColumnShouldAddColumn()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.SetSchema("test");
            string tableName = "Babboons";
            mgr.AddTable(tableName);
            mgr.AddColumn(tableName, new Column("PutColumnName", DataTypes.Boolean));

            Table table = mgr.GetTable(tableName);
            Expect.IsTrue(table.Columns.Length == 1);
            DeleteSchema(schema);
        }

        [UnitTest]
        public static void AddTableShouldSetConnectionNameToNameOfSchema()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.SetSchema("test");
            Table testTable = new Table("".RandomString(5));
            Expect.IsNullOrEmpty(testTable.ConnectionName);

            schema.AddTable(testTable);

            Expect.IsNotNullOrEmpty(testTable.ConnectionName);
            Expect.AreEqual(schema.Name, testTable.ConnectionName);
        }

        [UnitTest]
        public static void SettingTablesShouldSetConnectionNameForTables()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.SetSchema("test");
            Table testTable = new Table("".RandomString(5));
            Expect.IsNullOrEmpty(testTable.ConnectionName);

            List<Table> tables = new List<Table>();
            tables.Add(testTable);

            schema.Tables = tables.ToArray();

            Expect.IsNotNullOrEmpty(testTable.ConnectionName);
            Expect.AreEqual(schema.Name, testTable.ConnectionName);
        }

        [UnitTest]
        public static void ColumnWithSameNameAsFKShouldBeEqual()
        {
            ForeignKeyColumn fk = new ForeignKeyColumn();
            fk.TableName = "test";
            fk.Name = "columnName";

            Column col = new Column
            {
                TableName = fk.TableName,
                Name = fk.Name
            };

            Expect.IsTrue(fk.Equals(col));
            Expect.IsFalse(fk == col);
        }

        [UnitTest]
        public static void ListContainsShouldBeTrueForSameNameAndTable()
        {
            List<ForeignKeyColumn> fks = new List<ForeignKeyColumn>
            {
                new ForeignKeyColumn("columnName", "test", "target")
            };

            Expect.IsTrue(fks.Contains(new ForeignKeyColumn("columnName", "test", "target")));
        }

        [UnitTest]
        public static void ForeignKeyReferencedTableMustNotBeNull()
        {
            ForeignKeyColumn col = new ForeignKeyColumn("ignore", "ignore", "ReferencedTable");
            Expect.IsNotNullOrEmpty(col.ReferencedTable);
        }

        class TestSchemaManager : SchemaManager
        {
            public SchemaManagerResult TestAddForeignKey(Table table, Table target, ForeignKeyColumn fk)
            {
                return SetForeignKey(table, target, fk);
            }
        }

        [UnitTest]
        public static void AddForeignKeyShouldIncrementReferencingFKsForTargetTable()
        {
            TestSchemaManager tsm = new TestSchemaManager();
            string ingTable = "referencing";
            string edTable = "referred";
            Table referencing = new Table { Name = ingTable };
            Table referred = new Table { Name = edTable };

            int initial = referred.ReferencingForeignKeys.Length;
            ForeignKeyColumn fk = new ForeignKeyColumn("referredId", ingTable, edTable);

            tsm.TestAddForeignKey(referencing, referred, fk);
            Expect.AreEqual(initial + 1, referred.ReferencingForeignKeys.Length);
            Expect.AreEqual(0, referencing.ReferencingForeignKeys.Length);
        }

        [UnitTest]
        public static void SchemaManagerSetFKShouldAddFK()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.SetSchema("test");
            string tableName = "Referencee";
            mgr.AddTable(tableName);
            mgr.AddColumn(tableName, new Column("PutColumnName", DataTypes.Boolean));

            string refering = "Referencer";
            mgr.AddTable(refering);
            mgr.AddColumn(refering, new Column("fk", DataTypes.ULong));

            int initCount = schema.ForeignKeys.Length;
            mgr.SetForeignKey(tableName, refering, "fk");

            Expect.AreEqual(initCount + 1, schema.ForeignKeys.Length);

            Table referenced = mgr.GetTable(tableName);
            Table referencer = mgr.GetTable(refering);

            Expect.AreEqual(schema.ForeignKeys.Length, referenced.ReferencingForeignKeys.Length);
            Expect.AreEqual(0, referencer.ReferencingForeignKeys.Length);
            DeleteSchema(schema);
        }

        [UnitTest]
        public static void AddForeignKeyShouldFailIfColumnNotDefined()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition s = mgr.SetSchema("test");
            mgr.AddTable("TableOne");
            mgr.AddTable("ReferringTable");
            SchemaManagerResult r = mgr.SetForeignKey("TableOne", "ReferringTable", "TableOneID");

            Expect.IsFalse(r.Success);
            OutLine(r.Message, ConsoleColor.Yellow);

            TryDeleteSchema(s);
        }

        [UnitTest]
        public void SchemaMgrSetPropertyShouldSetPropertyNameOfColumn()
        {
            string tableName = "Test";
            string columnName = "ColumnName";
            string propertyName = "PropertyName";
            FileInfo schemFile = new FileInfo(".\\{0}.json"._Format(MethodBase.GetCurrentMethod().Name));
            SchemaDefinition def = new SchemaDefinition();
            def.ToJsonFile(schemFile);
            SchemaManager mgr = new SchemaManager(schemFile);
            mgr.AddTable(tableName);
            mgr.AddColumn(tableName, columnName);

            Table testTable = mgr.GetTable(tableName);
            Column column = testTable.Columns[0];
            Expect.AreEqual(1, testTable.Columns.Length);
            Expect.AreEqual(columnName, column.Name);
            Expect.AreEqual(column.Name, column.PropertyName);

            mgr.SetColumnPropertyName(tableName, columnName, propertyName);

            testTable = mgr.GetTable(tableName);
            column = testTable.Columns[0];
            Expect.AreEqual(1, testTable.Columns.Length);
            Expect.AreEqual(columnName, column.Name);
            Expect.AreEqual(propertyName, column.PropertyName);
        }
        [UnitTest]
        public void SchemaMgrSetClassNameShouldSetClassNameOfTable()
        {
            string tableName = "Test";
            string className = "ClassName";
            FileInfo schemFile = new FileInfo(".\\{0}.json"._Format(MethodBase.GetCurrentMethod().Name));
            SchemaDefinition def = new SchemaDefinition();
            def.ToJsonFile(schemFile);
            SchemaManager mgr = new SchemaManager(schemFile);
            mgr.AddTable(tableName);

            Table testTable = mgr.GetTable(tableName);
            Expect.AreEqual(tableName, testTable.Name);
            Expect.AreEqual(tableName, testTable.ClassName);

            mgr.SetTableClassName(tableName, className);

            testTable = mgr.GetTable(tableName);

            Expect.AreEqual(tableName, testTable.Name);
            Expect.AreEqual(className, testTable.ClassName);
        }

        [UnitTest]
        public void SchemaManagerConstructorShouldSetCurrent()
        {
            FileInfo schemaFile = new System.IO.FileInfo(".\\{0}.json"._Format(MethodBase.GetCurrentMethod().Name));
            SchemaDefinition def = new Schema.SchemaDefinition();
            def.ToJsonFile(schemaFile);
            SchemaManager mgr = new Schema.SchemaManager(def);
            Expect.AreEqual(mgr.CurrentSchema.File, def.File);
        }

        [UnitTest]
        public static void SchemaMgrAddTableShouldSuccess()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition s = mgr.SetSchema("test");
            SchemaManagerResult r = mgr.AddTable("tableOne");
            Expect.IsTrue(r.Success);

            TryDeleteSchema(s);
        }

        private static void TryDeleteSchema(SchemaDefinition s)
        {
            try
            {
                DeleteSchema(s);
            }
            catch (Exception ex)
            {
                Message.PrintLine("An error occurred deleting test data. {0}", ConsoleColor.Red, ex.Message);
            }
        }

        [UnitTest]
        public static void SchemaMgrAddColumnShouldSuccess()
        {
            string tableName = "tableOne";
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition s = mgr.SetSchema("test");
            SchemaManagerResult r = mgr.AddTable(tableName);
            r = mgr.AddColumn(tableName, new Column("ColumnOne", DataTypes.ULong, false));
            Expect.IsTrue(r.Success);
        }

        private static SchemaDefinition CreateTestTables(string targetTable, string referencingTable)
        {
            SchemaManager mgr = new SchemaManager();
            return CreateTestTables(targetTable, referencingTable, mgr);
        }

        private static SchemaDefinition CreateTestTables(string targetTable, string referencingTable, SchemaManager mgr)
        {
            SchemaDefinition schema = mgr.SetSchema("fkTest");
            DeleteSchema(schema);
            schema = mgr.SetSchema("fkTest");

            mgr.AddTable(targetTable);
            mgr.AddColumn(targetTable, new Column("PutColumnName", DataTypes.ULong));

            mgr.AddTable(referencingTable);
            mgr.AddColumn(referencingTable, new Column("PutColumnName", DataTypes.ULong));

            mgr.SetForeignKey(targetTable, referencingTable, "fk");
            return schema;
        }

        [UnitTest]
        public static void SetNewSchemaShouldThrowExceptionIfSchemaExists()
        {
            SchemaManager mgr = new SchemaManager();
            mgr.SetSchema("test");
            bool thrown = false;
            try
            {
                mgr.SetNewSchema("test");
            }
            catch (Exception)
            {
                thrown = true;
                TryDeleteSchema(mgr.SetSchema("test"));
            }

            Expect.IsTrue(thrown);
        }


        [UnitTest]
        public void SchemaShouldExistAfterSetSchema()
        {
            string schemaName = "XrefTest";
            SchemaManager sm = new SchemaManager();
            sm.SetSchema(schemaName);

            Expect.IsTrue(sm.SchemaExists(schemaName));
        }

        [UnitTest]
        public void GetTableShouldReturnTable()
        {
            string table = "TableName";
            SchemaManager sm = new SchemaManager();
            sm.SetSchema("test_schema".RandomString(4));

            sm.AddTable(table);

            Table t = sm.GetTable(table);
            Expect.IsNotNull(t);
            Expect.AreEqual(t.Name, table);
        }

        [UnitTest]
        public void SetAndGetXrefTableTest()
        {
            string table = "TableName".RandomString(4);
            SchemaManager sm = new SchemaManager();
            sm.SetSchema("test_schema".RandomString(4));

            sm.AddXref("Left", "Right");

            Table t = sm.GetXref("LeftRight");
            Expect.IsNotNull(t);
            Expect.AreEqual("LeftRight", t.Name);
        }

        [UnitTest]
        public void SetAndGetXrefTableShouldBeXrefTableType()
        {
            string table = "TableName".RandomString(4);
            SchemaManager sm = new SchemaManager();
            sm.SetSchema("test_schema".RandomString(4));

            sm.AddXref("Left", "Right");

            Table t = sm.GetXref("LeftRight");
            Expect.IsNotNull(t);
            Expect.AreEqual("LeftRight", t.Name);
            Expect.IsInstanceOfType<XrefTable>(t);
        }

        [UnitTest]
        public void SetAndGetXrefTableAsXrefTableType()
        {
            string table = "TableName".RandomString(4);
            SchemaManager sm = new SchemaManager();
            sm.SetSchema("test_schema".RandomString(4));

            sm.AddXref("Left", "Right");

            SchemaManager sm2 = new SchemaManager();
            sm2.SetSchema(sm.CurrentSchema.Name);

            Table t = sm2.GetXref("LeftRight");
            Expect.IsNotNull(t);
            XrefTable x = t as XrefTable;
            Expect.IsNotNull(x);
            Expect.AreEqual("Left", x.Left);
            Expect.AreEqual("Right", x.Right);
        }



        [UnitTest]
        public void ReadJson2FromResource()
        {
            string json = ResourceScripts.Get("json2.js");
            Out(json);
        }
    }
}
