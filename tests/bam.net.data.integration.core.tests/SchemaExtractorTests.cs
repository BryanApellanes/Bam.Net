/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bam.Net;
using Bam.Net.Data.MsSql;
using Bam.Net.Data.MySql;
using Bam.Net.Data.Schema;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;

namespace Bam.Net.Data.Integration.Core.Tests
{
    class TestNameFormatter : INameFormatter
    {
        public string FormatClassName(string tableName)
        {
            return "Test_{0}"._Format(tableName);
        }

        public string FormatPropertyName(string tableName, string columnName)
        {
            return "Test_{0}"._Format(columnName);
        }
    }
    [IntegrationTestContainer]
    public class SchemaExtractorTests : CommandLineTool
    {
        HashSet<Database> _testDatabases;
        [IntegrationTestSetup]
        public void SetupTests()
        {
            OutLine("Setting up tests...", ConsoleColor.Green);
            _testDatabases = DataTools.Setup();
        }

        [IntegrationTestCleanup]
        public void CleanupTests()
        {
            DataTools.Cleanup(_testDatabases);
        }

        [IntegrationTest("SchemaExtractor: Get Table Names")]
        public void GetTableNamesShouldReturnTableNames()
        {
            GetSchemaExtractors().Each(extractor =>
            {
                string[] tableNames = extractor.GetTableNames();
                Expect.IsNotNull(tableNames);
                Expect.AreEqual(7, tableNames.Length);
            });
        }

        [IntegrationTest("SchemaExtractor: Get Column Names")]
        public void GetColumnNamesShouldReturnColumnNames()
        {
            GetSchemaExtractors().Each(extractor =>
            {
                string[] columnNames = extractor.GetColumnNames("DaoReferenceObject");
                Expect.IsNotNull(columnNames);
                Expect.AreEqual(9, columnNames.Length);
            });
        }

        [IntegrationTest("SchemaExtractor: Get Column Data type")]
        public void GetColumnDataTypeShouldReturnColumnDataType()
        {
            GetSchemaExtractors().Each(extractor =>
            {
                DataTypes dataType = extractor.GetColumnDataType("DaoReferenceObject", "DateTimeProperty");
                Expect.AreEqual(DataTypes.DateTime, dataType);
            });
        }

        [IntegrationTest("SchemaExtractor: Get Foreign Key Columns")]
        public void GetForeignKeyColumnsShouldReturnForeignKeyColumns()
        {
            GetSchemaExtractors().Each(extractor =>
            {
                ForeignKeyColumn[] fks = extractor.GetForeignKeyColumns();
                Expect.IsNotNull(fks);
                Expect.IsGreaterThan(fks.Length, 0);
                fks.Each(fk =>
                {
                    OutLine(fk.PropertiesToString(), ConsoleColor.Cyan);
                });
            });
        }

        [IntegrationTest]
        public void TransformTableNameShouldTransformTableName()
        {
            GetSchemaExtractors().Each(extractor =>
            {
                extractor.NameFormatter = new TestNameFormatter();
                string className = extractor.GetClassName("Monkey");
                Expect.AreEqual("Test_Monkey", className);
            });
        }

        [IntegrationTest]
        public void TransformColumnNameShouldTransformColumnName()
        {
            GetSchemaExtractors().Each(extractor =>
            {
                extractor.NameFormatter = new TestNameFormatter();
                string className = extractor.GetPropertyName("Table", "Monkey");
                Expect.AreEqual("Test_Monkey", className);
            });
        }

        [IntegrationTest]
        public void ExtractShouldPopulateNameMap()
        {
            GetSchemaExtractors().Each(extractor =>
            {
                Expect.AreEqual(0, extractor.NameMap.ColumnNamesToPropertyNames.Count);
                Expect.AreEqual(0, extractor.NameMap.TableNamesToClassNames.Count);
                extractor.Extract();
                Expect.IsTrue(extractor.NameMap.ColumnNamesToPropertyNames.Count > 0);
                Expect.IsTrue(extractor.NameMap.TableNamesToClassNames.Count > 0);
                Out(extractor.NameMap.ToJson(), ConsoleColor.DarkBlue);
                extractor.NameMap.Save($"c:\\Bam\\Data\\Test\\{extractor.GetType().Name}_NameMap_test_output.json");
            });
        }

        public IEnumerable<SchemaExtractor> GetSchemaExtractors()
        {
            //yield return new MsSqlSmoSchemaExtractor(GetDatabase<MsSqlDatabase>());
            yield return new MsSqlSchemaExtractor(GetDatabase<MsSqlDatabase>());
            yield return new SQLiteSchemaExtractor(GetDatabase<SQLiteDatabase>());
            yield return new MySqlSchemaExtractor(GetDatabase<MySqlDatabase>());
            //yield return new NpgsqlSchemaExtractor(GetDatabase<NpgsqlDatabase>()); // not fully implemented
        }        

        private T GetDatabase<T>() where T : Database
        {
            return (T)_testDatabases.Where(db => db.GetType() == typeof(T)).FirstOrDefault();
        }
    }
}
