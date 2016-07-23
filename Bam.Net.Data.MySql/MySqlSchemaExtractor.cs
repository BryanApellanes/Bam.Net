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
            MySqlConnectionStringBuilder conn = database.CreateConnectionStringBuilder<MySqlConnectionStringBuilder>();
            conn.ConnectionString = database.ConnectionString;
            ConnectionString = database.ConnectionString;
            _tableIndexes = new Dictionary<string, DataTable>();
        }
        public override DataTypes GetColumnDataType(string tableName, string columnName)
        {
            return TranslateDataType(GetColumnDbDataType(tableName, columnName));
        }

        public override string GetColumnDbDataType(string tableName, string columnName)
        {
            string sql = "SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName AND COLUMN_NAME = @ColumnName";
            return Database.GetValue<string>(sql, new { TableName = tableName, ColumnName = columnName });
        }

        public override string GetColumnMaxLength(string tableName, string columnName)
        {
            string sql = "SELECT CHARACTER_MAX_LENGTH FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName AND COLUMN_NAME = @ColumnName";
            return Database.GetValue<object>(sql, new { TableName = tableName, ColumnName = columnName }).ToString();
        }

        public override string[] GetColumnNames(string tableName)
        {
            string sql = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
            return Database.GetSingleColumnResults<string>(sql, new { TableName = tableName }).ToArray();
        }

        public override bool GetColumnNullable(string tableName, string columnName)
        {
            string sql = "SELECT IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName and COLUMN_NAME = @ColumnName";
            return Database.GetValue<string>(sql, new { TableName = tableName, ColumnName = columnName }).IsAffirmative();
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
            return Database.GetSingleColumnResults<string>(sql).ToArray();
        }

        protected override void SetConnectionName(string connectionString)
        {
            MySqlConnectionStringBuilder conn = Database.CreateConnectionStringBuilder<MySqlConnectionStringBuilder>();
            conn.ConnectionString = Database.ConnectionString;
            Database.ConnectionName = (string)conn["Database"];
        }

        protected internal DataTypes TranslateDataType(string sqlDataType)
        {
            string dataType = sqlDataType.ToLowerInvariant();
            switch (dataType)
            {
                case "bigint":
                    return DataTypes.Long;
                case "binary":
                    return DataTypes.ByteArray;
                case "bit":
                    return DataTypes.Boolean;
                case "blob":
                    return DataTypes.ByteArray;
                case "char":
                    return DataTypes.String;
                case "date":
                case "datetime":
                    return DataTypes.DateTime;
                case "decimal":
                    return DataTypes.Decimal;
                case "double":
                    return DataTypes.Decimal;
                case "enum":
                    return DataTypes.String;
                case "float":
                    return DataTypes.Decimal;
                case "int":
                    return DataTypes.Int;
                case "smallint":
                    return DataTypes.Int;
                case "text":
                    return DataTypes.String;
                case "time":
                    return DataTypes.DateTime;
                case "timestamp":
                    return DataTypes.String;
                case "tinyblob":
                    return DataTypes.ByteArray;
                case "tinyint":
                    return DataTypes.Int;
                case "tinytext":
                    return DataTypes.String;
                case "varbinary":
                    return DataTypes.ByteArray;
                case "varchar":
                    return DataTypes.String;
                case "year":
                    return DataTypes.String;
                default:
                    return DataTypes.String;
            }
        }

        Dictionary<string, DataTable> _tableIndexes;
        private DataTable GetTableIndex(string tableName)
        {
            if (!_tableIndexes.ContainsKey(tableName))
            {
                _tableIndexes.Add(tableName, Database.GetDataTable("SHOW INDEX FROM @TableName", new { TableName = tableName }));
            }

            return _tableIndexes[tableName];
        }
    }
}
