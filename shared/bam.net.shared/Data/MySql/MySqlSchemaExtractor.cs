using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using MySql.Data.MySqlClient;

namespace Bam.Net.Data.MySql
{
    public class MySqlSchemaExtractor : SchemaExtractor
    {
        public MySqlSchemaExtractor(MySqlDatabase database)
            : base()
        {
            Database = database;
            ConnectionString = database.ConnectionString;
            _tableIndexes = new Dictionary<string, DataTable>();
        }
        public override DataTypes GetColumnDataType(string tableName, string columnName)
        {
            return TranslateDataType(GetColumnDbDataType(tableName, columnName));
        }

        public override string GetColumnDbDataType(string tableName, string columnName)
        {
            return GetColumnAttribute(tableName, columnName, "DATA_TYPE");
        }

        public override string GetColumnMaxLength(string tableName, string columnName)
        {
            return GetColumnAttribute(tableName, columnName, "CHARACTER_MAXIMUM_LENGTH");
        }

        public override bool GetColumnNullable(string tableName, string columnName)
        {
            return GetColumnAttribute(tableName, columnName, "IS_NULLABLE").IsAffirmative();
        }

        public override string[] GetColumnNames(string tableName)
        {
            string sql = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = @SchemaName AND TABLE_NAME = @TableName";
            return Database.QuerySingleColumn<string>(sql, new { SchemaName = GetSchemaName(), TableName = tableName }).ToArray();
        }

        public override ForeignKeyColumn[] GetForeignKeyColumns()
        {
            string sql = @"SELECT 
  TABLE_NAME,COLUMN_NAME,CONSTRAINT_NAME, REFERENCED_TABLE_NAME,REFERENCED_COLUMN_NAME
FROM
  INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE
  REFERENCED_TABLE_SCHEMA = @DatabaseName";
            DataTable fkData = Database.GetDataTable(sql, new { DatabaseName = GetSchemaName() });
            List<ForeignKeyColumn> results = new List<ForeignKeyColumn>();
            foreach(DataRow row in fkData.Rows)
            {
                ForeignKeyColumn fk = new ForeignKeyColumn();
                fk.TableName = row["TABLE_NAME"].ToString();
                fk.ReferenceName = row["CONSTRAINT_NAME"].ToString();
                fk.Name = row["COLUMN_NAME"].ToString();
                fk.ReferencedKey = row["REFERENCED_COLUMN_NAME"].ToString();
                fk.ReferencedTable = row["REFERENCED_TABLE_NAME"].ToString();
                results.Add(fk);
            }
            return results.ToArray();
        }

        public override string GetKeyColumnName(string tableName)
        {
            DataTable indexes = GetTableIndex(tableName);
            foreach(DataRow row in indexes.Rows)
            {
                if(((string)row["Key_name"]).Equals("PRIMARY", StringComparison.InvariantCultureIgnoreCase))
                {
                    return (string)row["Column_name"];
                }
            }
            return string.Empty;
        }

        public override string GetSchemaName()
        {
            return Database.ConnectionName;
        }

        public override string[] GetTableNames()
        {
            string sql = $"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{GetSchemaName()}'";
            return Database.QuerySingleColumn<string>(sql).ToArray();
        }

        protected override void SetConnectionName(string connectionString)
        {
            MySqlConnectionStringBuilder conn = Database.CreateConnectionStringBuilder<MySqlConnectionStringBuilder>();
            conn.ConnectionString = Database.ConnectionString;
            Database.ConnectionName = (string)conn["Database"];
        }

        protected internal DataTypes TranslateDataType(string sqlDataType)
        {
            return Database.GetDataTypeTranslator().TranslateDataType(sqlDataType);
        }

        Dictionary<string, DataTable> _tableIndexes;
        private DataTable GetTableIndex(string tableName)
        {
            if (!_tableIndexes.ContainsKey(tableName))
            {
                _tableIndexes.Add(tableName, Database.GetDataTable($"SHOW INDEX FROM `{tableName}`"));
            }

            return _tableIndexes[tableName];
        }

        private string GetColumnAttribute(string tableName, string columnName, string attributeName)
        {
            string sql = $"SELECT {attributeName} FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = @SchemaName AND TABLE_NAME = @TableName AND COLUMN_NAME = @ColumnName";
            object result = Database.QuerySingle<object>(sql, new { SchemaName = GetSchemaName(), TableName = tableName, ColumnName = columnName });
            return result == null ? string.Empty : result.ToString();
        }

    }
}
