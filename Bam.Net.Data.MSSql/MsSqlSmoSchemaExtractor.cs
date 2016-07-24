/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Net.Data.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.SqlServer.Management;
    using Microsoft.SqlServer.Management.Common;
    using BAS = Bam.Net.Data.Schema;
    using Smo = Microsoft.SqlServer.Management.Smo;
    using System.Data.SqlClient;
    using System.IO;
    using System.Data;
    using System.Linq.Expressions;
    using System.Configuration;
    using Bam.Net;
    using Bd = Bam.Net.Data;
    using Bam.Net.Data.Schema;
    // TODO: update this to allow for specifying a connection string without looking in the config, so it can be specified in the UI  
    // TODO: Allow LaoTzU.exe (note the U) to specify Xref tables when extracting schema definition
    public class MsSqlSmoSchemaExtractor : BAS.SchemaExtractor
    {
        SqlConnection _connection; 
        ServerConnection _serverConnection;
        Microsoft.SqlServer.Management.Smo.Server _server;
        Microsoft.SqlServer.Management.Smo.Database _database;
        Bd.Database _daoDatabase;

        public MsSqlSmoSchemaExtractor(string connectionName)
            : base()
        {
            this._daoDatabase = Db.For(connectionName);
            string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            ConnectionString = connectionString;
        }

        public MsSqlSmoSchemaExtractor(MsSqlDatabase database)
            : base()
        {
            this._daoDatabase = database;
            ConnectionString = database.ConnectionString;
        }

        public MsSqlSmoSchemaExtractor(MsSqlDatabase database, INameFormatter nameFormatter): this(database)
        {
            this.NameFormatter = nameFormatter;
        }

        public override BAS.ForeignKeyColumn[] GetForeignKeyColumns()
        {
            DataTable foreignKeyData = GetForeignKeyData(_daoDatabase);
            List<BAS.ForeignKeyColumn> results = new List<BAS.ForeignKeyColumn>();
            foreach (DataRow row in foreignKeyData.Rows)
            {
                BAS.ForeignKeyColumn fk = new BAS.ForeignKeyColumn();
                fk.TableName = row["ForeignKeyTable"].ToString();
                fk.ReferenceName = row["ForeignKeyName"].ToString();
                fk.Name = row["ForeignKeyColumn"].ToString();
                fk.ReferencedKey = row["PrimaryKeyColumn"].ToString();
                
                fk.ReferencedTable = row["PrimaryKeyTable"].ToString();
                results.Add(fk);
            }

            return results.ToArray();
        }

        internal static DataTable GetForeignKeyData(Bd.Database db)
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

        public override string GetSchemaName()
        {
            return _daoDatabase.ConnectionName;
        }

        public override string[] GetTableNames()
        {
            List<string> tableNames = new List<string>();
            foreach (Smo.Table table in _database.Tables)
            {
                tableNames.Add(table.Name);
            }

            return tableNames.ToArray();
        }
        
        public override string GetKeyColumnName(string tableName)
        {
            foreach(Smo.Column column in _database.Tables[tableName].Columns)
            {
                if (column.InPrimaryKey)
                {
                    return column.Name;
                }
            }
            return "";
        }

        public override string[] GetColumnNames(string tableName)
        {
            List<string> columnNames = new List<string>();
            foreach (Smo.Column column in _database.Tables[tableName].Columns)
            {
                columnNames.Add(column.Name);
            }

            return columnNames.ToArray();
        }

        public override BAS.DataTypes GetColumnDataType(string tableName, string columnName)
        {
            Smo.SqlDataType dbDataType = (Smo.SqlDataType)Enum.Parse(typeof(Smo.SqlDataType), GetColumnDbDataType(tableName, columnName));
            return TranslateDataType(dbDataType);
        }

        public override string GetColumnDbDataType(string tableName, string columnName)
        {
            Smo.Column column = _database.Tables[tableName].Columns[columnName];
            string dbDataType = column.DataType.SqlDataType.ToString();
            if (column.DataType.SqlDataType == Smo.SqlDataType.VarCharMax)
            {
                dbDataType = "VarChar";
            }
            else if (column.DataType.SqlDataType == Smo.SqlDataType.NVarCharMax)
            {
                dbDataType = "NVarChar";
            }
            else if (column.DataType.SqlDataType == Smo.SqlDataType.VarBinaryMax)
            {
                dbDataType = "VarBinary";
            }
            return dbDataType;
        }

        /// <summary>
        /// Get the max column length for the specified table and column
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public override string GetColumnMaxLength(string tableName, string columnName)
        {
            Smo.Column column = _database.Tables[tableName].Columns[columnName];
            return column.DataType.MaximumLength == -1 ? "MAX" : column.DataType.MaximumLength.ToString();
        }

        public override bool GetColumnNullable(string tableName, string columnName)
        {
            return _database.Tables[tableName].Columns[columnName].Nullable;
        }

        protected internal BAS.DataTypes TranslateDataType(Smo.SqlDataType sqlDataType)
        {
            switch (sqlDataType)
            {
                case Smo.SqlDataType.BigInt:
                    return BAS.DataTypes.Long;
                case Smo.SqlDataType.Binary:
                    return BAS.DataTypes.ByteArray;
                case Smo.SqlDataType.Bit:
                    return BAS.DataTypes.Boolean;
                case Smo.SqlDataType.Char:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.DateTime:
                    return BAS.DataTypes.DateTime;
                case Smo.SqlDataType.Decimal:
                    return BAS.DataTypes.Decimal;
                case Smo.SqlDataType.Float:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.Image:
                    return BAS.DataTypes.ByteArray;
                case Smo.SqlDataType.Int:
                    return BAS.DataTypes.Int;
                case Smo.SqlDataType.Money:
                    return BAS.DataTypes.Decimal;
                case Smo.SqlDataType.NChar:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.NText:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.NVarChar:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.NVarCharMax:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.None:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.Numeric:
                    return BAS.DataTypes.Long;
                case Smo.SqlDataType.Real:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.SmallDateTime:
                    return BAS.DataTypes.DateTime;
                case Smo.SqlDataType.SmallInt:
                    return BAS.DataTypes.Int;
                case Smo.SqlDataType.SmallMoney:
                    return BAS.DataTypes.Decimal;
                case Smo.SqlDataType.SysName:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.Text:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.Timestamp:
                    return BAS.DataTypes.DateTime;
                case Smo.SqlDataType.TinyInt:
                    return BAS.DataTypes.Int;
                case Smo.SqlDataType.UniqueIdentifier:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.UserDefinedDataType:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.UserDefinedType:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.VarBinary:
                    return BAS.DataTypes.ByteArray;
                case Smo.SqlDataType.VarBinaryMax:
                    return BAS.DataTypes.ByteArray;
                case Smo.SqlDataType.VarChar:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.VarCharMax:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.Variant:
                    return BAS.DataTypes.String;
                case Smo.SqlDataType.Xml:
                    return BAS.DataTypes.String;
                default:
                    return BAS.DataTypes.String;
            }
        }
        
        protected override void SetConnectionName(string connectionString)
        {
            _connection = new SqlConnection(ConnectionString);
            _serverConnection = new ServerConnection(_connection);
            _server = new Smo.Server(_serverConnection);

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
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

            _database = _server.Databases[databaseName];
        }
    }
}
