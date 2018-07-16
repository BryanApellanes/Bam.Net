using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using Bam.Net.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Bam.Net.Data.MsSql
{
    public class MsSqlSchemaExtractor : SchemaExtractor
    {
        // TODO: update this to retrieve all meta data using fewer queries along the lines of  
        //SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM {GetSchemaName()}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TestTable'
      

        public MsSqlSchemaExtractor(MsSqlDatabase database)
            : base()
        {
            Database = database;
            ConnectionString = database.ConnectionString;
        }

        public MsSqlSchemaExtractor(MsSqlDatabase database, INameFormatter nameFormatter): this(database)
        {
            this.NameFormatter = nameFormatter;
        }

        public override DataTypes GetColumnDataType(string tableName, string columnName)
        {
            return TranslateDataType(GetColumnDbDataType(tableName, columnName).ToLowerInvariant());
        }

        public override string GetColumnDbDataType(string tableName, string columnName)
        {
            return GetColumnAttribute(tableName, columnName, "DATA_TYPE").ToString();            
        }

        public override string GetColumnMaxLength(string tableName, string columnName)
        {
            object value = GetColumnAttribute(tableName, columnName, "CHARACTER_MAXIMUM_LENGTH");
            return value == null ? "MAX" : value.ToString();
        }

        public override string[] GetColumnNames(string tableName)
        {
            string sql = $"SELECT COLUMN_NAME FROM {GetSchemaName()}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
            return Database.QuerySingleColumn<string>(sql, new { TableName = tableName }.ToDbParameters(Database).ToArray()).ToArray();
        }

        public override bool GetColumnNullable(string tableName, string columnName)
        {
            string sql = "SELECT COLUMNPROPERTY(OBJECT_ID(@TableName, 'U'), @ColumnName, 'AllowsNull')";
            return Database.QuerySingle<int>(sql, new { TableName = tableName, ColumnName = columnName }.ToDbParameters(Database).ToArray()) == 1;
        }

        public override ForeignKeyColumn[] GetForeignKeyColumns()
        {
            DataTable foreignKeyData = GetForeignKeyData(Database);
            List<ForeignKeyColumn> results = new List<ForeignKeyColumn>();
            foreach (DataRow row in foreignKeyData.Rows)
            {
                ForeignKeyColumn fk = new ForeignKeyColumn();
                fk.TableName = row["ForeignKeyTable"].ToString();
                fk.ReferenceName = row["ForeignKeyName"].ToString();
                fk.Name = row["ForeignKeyColumn"].ToString();
                fk.ReferencedKey = row["PrimaryKeyColumn"].ToString();
                fk.ReferencedTable = row["PrimaryKeyTable"].ToString();
                results.Add(fk);
            }

            return results.ToArray();
        }

        public override string GetKeyColumnName(string tableName)
        {
            string sql = $@"SELECT COLUMN_NAME
FROM {GetSchemaName()}.INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + CONSTRAINT_NAME), 'IsPrimaryKey') = 1
AND TABLE_NAME = @TableName";            
            return Database.QuerySingle<string>(sql, new { TableName = tableName }.ToDbParameters(Database).ToArray());            
        }

        public override string GetSchemaName()
        {
            return Database.ConnectionName;
        }

        public override string[] GetTableNames()
        {
            string sql = $"SELECT TABLE_NAME FROM {GetSchemaName()}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            DataTable results = Database.GetDataTable(sql);
            List<string> tableNames = new List<string>();
            foreach(DataRow row in results.Rows)
            {
                tableNames.Add(row["TABLE_NAME"].ToString());
            }
            return tableNames.ToArray();
        }

        protected internal DataTypes TranslateDataType(string sqlDataType)
        {
            return Database.GetDataTypeTranslator().TranslateDataType(sqlDataType);
        }


        internal static DataTable GetForeignKeyData(Database db)
        {
            #region sql
            string sql = @"SELECT FK.constraint_name as ForeignKeyName, 
                FK.table_name as ForeignKeyTable, 
                FKU.column_name as ForeignKeyColumn,
                UK.constraint_name as PrimaryKeyName, 
                UK.table_name as PrimaryKeyTable, 
                UKU.column_name as PrimaryKeyColumn
                FROM Information_Schema.Table_Constraints AS FK
                INNER JOIN
                Information_Schema.Key_Column_Usage AS FKU
                ON FK.constraint_type = 'FOREIGN KEY' AND
                FKU.constraint_name = FK.constraint_name
                INNER JOIN
                Information_Schema.Referential_Constraints AS RC
                ON RC.constraint_name = FK.constraint_name
                INNER JOIN
                Information_Schema.Table_Constraints AS UK
                ON UK.constraint_name = RC.unique_constraint_name
                INNER JOIN
                Information_Schema.Key_Column_Usage AS UKU
                ON UKU.constraint_name = UK.constraint_name AND
                UKU.ordinal_position =FKU.ordinal_position";
            #endregion
            DataTable foreignKeyData = db.GetDataTable(sql, CommandType.Text);
            return foreignKeyData;
        }
        protected override void SetConnectionName(string connectionString)
        {
            SqlConnectionStringBuilder connectionStringBuilder = Database.CreateConnectionStringBuilder<SqlConnectionStringBuilder>();
            connectionStringBuilder.ConnectionString = connectionString;
            string databaseName = connectionStringBuilder["Initial Catalog"] as string;
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                databaseName = connectionStringBuilder["Database"] as string;
            }

            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new InvalidOperationException(
                    string.Format("Unable to determine database name from the specified connection string: {0}",
                    connectionString));
            }

            Database.ConnectionName = databaseName;
        }
        Dictionary<string, DataTable> _tableColumnInfo = new Dictionary<string, DataTable>();
        private object GetColumnAttribute(string tableName, string columnName, string attributeName)
        {
            if (!_tableColumnInfo.ContainsKey(tableName))
            {
                string sql = $"SELECT * FROM {GetSchemaName()}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
                _tableColumnInfo.Add(tableName, Database.GetDataTable(sql, new { TableName = tableName }));
            }
            foreach(DataRow row in _tableColumnInfo[tableName].Rows)
            {
                if (row["COLUMN_NAME"].Equals(columnName))
                {
                    return row[attributeName];
                }
            }
            return null;
        }

    }
}
