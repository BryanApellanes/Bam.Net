/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Bam.Net.Data;
using Bam.Net;
using Bam.Net.Testing;
using System.Configuration;
using Bam.Net.Incubation;
using System.Data.OleDb;
using Bam.Net.CommandLine;
using Bam.Net.Data.Schema;
using Bam.Net.Data.MsSql;
using Bam.Net.Logging;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Configuration;
using Bam.Net.Data.Tests;
using Bam.Net.Analytics;
using Bam.Net.Testing.Integration;

namespace Bam.Net.Data.Tests.Integration
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
    public class SchemaExtractorTests : CommandLineTestInterface
    {
        [IntegrationTest("SchemaExtractor: Get Table Names")]
        public void GetTableNamesShouldReturnTableNames()
        {
            GetSchemaExtractors().Each(extractor =>
            {
                string[] tableNames = extractor.GetTableNames();
                Expect.IsNotNull(tableNames);
                Expect.AreEqual(2, tableNames.Length);
            });
        }

        [IntegrationTest("SchemaExtractor: Get Column Names")]
        public void GetColumnNamesShouldReturnColumnNames()
        {
            GetSchemaExtractors().Each(extractor =>
            {
                string[] columnNames = extractor.GetColumnNames("t_cust");
                Expect.IsNotNull(columnNames);
                Expect.AreEqual(5, columnNames.Length);
            });
        }

        [IntegrationTest("SchemaExtractor: Get Column Data type")]
        public void GetColumnDataTypeShouldReturnColumnDataType()
        {
            GetSchemaExtractors().Each(extractor =>
            {
                DataTypes dataType = extractor.GetColumnDataType("t_cust", "b_day");
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
                extractor.NameMap.Save("c:\\testData\\Db_SillydatabaseNameMap_test_output.json");
            });
        }

        public IEnumerable<SchemaExtractor> GetSchemaExtractors()
        {
            yield return GetSmoSchemExtractor();
            yield return GetMsSqlSchemaExtractor();
        }

        private SchemaExtractor GetSmoSchemExtractor()
        {
            return DataTools.GetMsSqlSmoSchemaExtractor(GetMsSqlDatabase());
        }

        private SchemaExtractor GetMsSqlSchemaExtractor()
        {
            return DataTools.GetMsSqlSchemaExtractor(GetMsSqlDatabase());
        }

        private MsSqlDatabase GetMsSqlDatabase()
        {
            return new MsSqlDatabase("chumsql2", "Db_Sillydatabase", new MsSqlCredentials { UserName = "mssqluser", Password = "mssqlP455w0rd" });
        }
    }
}
