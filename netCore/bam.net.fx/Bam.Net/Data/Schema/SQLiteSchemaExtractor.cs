using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace Bam.Net.Data.SQLite
{
    public class SQLiteSchemaExtractor : SchemaExtractor
    {
        const string pragmaFormat = "PRAGMA TABLE_INFO('{0}')";
        
        public SQLiteSchemaExtractor(SQLiteDatabase database)
            : base()
        {
            Database = database;
            ConnectionString = database.ConnectionString;
            _pragmaTables = new Dictionary<string, DataTable>();
        }

        public override DataTypes GetColumnDataType(string tableName, string columnName)
        {
            return TranslateDataType(GetColumnDbDataType(tableName, columnName));
        }

        public override string GetColumnDbDataType(string tableName, string columnName)
        {
            DataTable pragmaTable = GetPragmaTable(tableName);            
            foreach(DataRow row in pragmaTable.Rows)
            {
                string rowName = (string)row["name"];
                if (rowName.Equals(columnName))
                {
                    return (string)row["type"];
                }
            }
            return "text";
        }

        public override string GetColumnMaxLength(string tableName, string columnName)
        {
            return string.Empty;
        }

        public override string[] GetColumnNames(string tableName)
        {
            DataTable pragmaTable = GetPragmaTable(tableName);
            List<string> columnNames = new List<string>();
            foreach(DataRow row in pragmaTable.Rows)
            {
                columnNames.Add(row["name"].ToString());
            }
            return columnNames.ToArray();
        }

        public override bool GetColumnNullable(string tableName, string columnName)
        {
            DataTable pragmaTable = GetPragmaTable(tableName);
            foreach(DataRow row in pragmaTable.Rows)
            {
                if (((string)row["name"]).Equals(columnName))
                {
                    return (long)row["notnull"] == 0;
                }
            }
            return true;
        }

        public override ForeignKeyColumn[] GetForeignKeyColumns()
        {
            string sql = @"SELECT sql 
	FROM(
		SELECT sql sql, type type, tbl_name tbl_name, name name from SQLITE_MASTER union all
		SELECT sql, type, tbl_name, name from SQLITE_TEMP_MASTER
	)
WHERE type != 'meta'
	AND sql NOTNULL
	AND name NOT LIKE 'sqlite_%'
ORDER BY SUBSTR(type, 2, 1), name";
            List<ForeignKeyColumn> results = new List<ForeignKeyColumn>();
            Database.QuerySingleColumn<string>(sql).Each(createStatement =>
            {
                string foreignKey = "FOREIGN KEY";
                string createTableStatement = createStatement.ReadUntil('(', out string columnDefinitions);
                string tableName = createTableStatement.DelimitSplit("[", "]")[1];
                columnDefinitions.DelimitSplit(",").Each(columnDefinition =>
                {
                    if(columnDefinition.StartsWith(foreignKey))
                    {
                        string[] segments = columnDefinition.TruncateFront(foreignKey.Length).DelimitSplit("(", ")", " ", "[", "]", "REFERENCES");
                        string columnName = segments[0];
                        string referencedTable = segments[1];
                        string referencedColumn = segments[2];
                        ForeignKeyColumn fk = new ForeignKeyColumn();
                        fk.TableName = tableName;
                        fk.ReferenceName = $"FK_{tableName}_{referencedTable}".RandomLetters(4);
                        fk.Name = columnName;
                        fk.ReferencedKey = referencedColumn;
                        fk.ReferencedTable = referencedTable;
                        results.Add(fk);
                    }
                });
            });
            return results.ToArray();
        }

        public override string GetKeyColumnName(string tableName)
        {
            DataTable pragmaTable = GetPragmaTable(tableName);
            foreach (DataRow row in pragmaTable.Rows)
            {
                if((long)row["pk"] == 1)
                {
                    return (string)row["name"];
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
            string sql = "SELECT Name from SQLITE_MASTER where Type = 'table' and Name <> 'sqlite_sequence'";
            return Database.QuerySingleColumn<string>(sql).ToArray();
        }

        protected override void SetConnectionName(string connectionString)
        {
            SQLiteConnectionStringBuilder conn = new SQLiteConnectionStringBuilder(connectionString);
            Database.ConnectionName = Path.GetFileNameWithoutExtension(conn["Data Source"].ToString());
        }

        protected internal DataTypes TranslateDataType(string sqlDataType)
        {
            string dataType = sqlDataType.ToLowerInvariant();
            switch (dataType)
            {
                case "integer":
                case "int":
                case "smallint":
                case "mediumint":
                    return DataTypes.Int;
                case "text":
                    return DataTypes.String;
                case "blob":
                    return DataTypes.ByteArray;
                case "real":
                    return DataTypes.Decimal;
                case "bigint":
                    return DataTypes.ULong;
                case "datetime":
                    return DataTypes.DateTime;
                case "varchar":
                    return DataTypes.String;
                default:
                    return DataTypes.String;
            }
        }

        Dictionary<string, DataTable> _pragmaTables;
        private DataTable GetPragmaTable(string tableName)
        {
            if (!_pragmaTables.ContainsKey(tableName))
            {
                _pragmaTables.Add(tableName, Database.GetDataTable(pragmaFormat._Format(tableName)));
            }
            return _pragmaTables[tableName];            
        }
    }
}
