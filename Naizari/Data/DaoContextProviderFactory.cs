/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;
using FirebirdSql.Data.FirebirdClient;
using Naizari.Helpers;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SQLite;
using Naizari.Testing;
using Naizari.Extensions;
using System.IO;
using System.Data;
using System.Web;

namespace Naizari.Data
{
    public class DaoContextProviderFactory : DbProviderFactory
    {
        DaoDbType type;
        public DaoContextProviderFactory()
        {
        }

        public DaoContextProviderFactory(DaoDbType type)
        {
            this.type = type;            
        }

        public DaoContextProviderFactory(DaoDbType type, string connectionString)
            : this(type, connectionString, true)
        {
        }

        public DaoContextProviderFactory(DaoDbType type, string connectionString, bool initEmbedded): this(type)
        {
            Expect.IsNotNullOrEmpty(connectionString, "Connection string cannot be empty");
            this.ConnectionString = connectionString;
            if ((type == DaoDbType.Firebird || type == DaoDbType.SQLite) &&
                initEmbedded)
            {
                this.InitEmbeddedDb();
            }
        }

        public DaoDbType DbType
        {
            get
            {
                return this.type;
            }
        }

        private void InitEmbeddedDb()
        {
            if ((this.DbType == DaoDbType.Firebird && !string.IsNullOrEmpty(this.ConnectionString)) ||
                this.DbType == DaoDbType.SQLite && !string.IsNullOrEmpty(this.ConnectionString))
            {
                DbConnectionStringBuilder connectionStringBuilder = this.CreateConnectionStringBuilder();
                connectionStringBuilder.ConnectionString = this.ConnectionString;
                string dbName = "";
                if (this.DbType != DaoDbType.SQLite && connectionStringBuilder["Initial Catalog"] != null)
                {
                    dbName = connectionStringBuilder["Initial Catalog"].ToString();
                }

                if (string.IsNullOrEmpty(dbName) && connectionStringBuilder.ContainsKey("Database") &&
                    connectionStringBuilder["Database"] != null)
                {
                    dbName = connectionStringBuilder["Database"].ToString();
                }

                if (string.IsNullOrEmpty(dbName) && connectionStringBuilder.ContainsKey("Data Source") &&
                    connectionStringBuilder["Data Source"] != null)
                {
                    dbName = connectionStringBuilder["Data Source"].ToString();
                }

                if (string.IsNullOrEmpty(dbName))
                {
                    dbName = "default.db";
                }

                string path = "";
                if (HttpContext.Current == null)
                    path = FsUtil.GetCurrentUserAppDataFolder() + dbName;
                else
                    path = HttpContext.Current.Server.MapPath("~/App_Data/" + dbName);

                if (this.DbType == DaoDbType.Firebird)
                {
                    this.ConnectionString = "Database=" + path + ";ServerType=1;";
                    if (!File.Exists(path))
                        FbConnection.CreateDatabase(this.ConnectionString);
                }
                else if (this.DbType == DaoDbType.SQLite)
                {
                    FileInfo file = new FileInfo(path);
                    if (!Directory.Exists(file.DirectoryName))
                        Directory.CreateDirectory(file.DirectoryName);

                    connectionStringBuilder["Data Source"] = file.FullName;
                    this.ConnectionString = connectionStringBuilder.ConnectionString;
                }

            }
        }

        public override bool CanCreateDataSourceEnumerator { get { return false; } }

        public void CreateDaoSchema<T>(DatabaseAgent agent) where T : DaoObject, new()
        {
            Assembly assembly = typeof(T).Assembly;
            string contextName = new T().DataContextName;

            CreateDaoTables(agent, assembly, contextName);

            this.AddAllForeignKeys<T>(agent);
        }

        public void CreateDaoSchema(DatabaseAgent agent, Assembly assembly, string contextName)
        {
            CreateDaoTables(agent, assembly, contextName);
            AddAllForeignKeys(agent, assembly, contextName);
        }

        private void CreateDaoTables(DatabaseAgent agent, Assembly assembly, string contextName)
        {
            foreach (Type type in assembly.GetTypes())
            {
                object[] tableAttributes = type.GetCustomAttributes(typeof(DaoTable), true);
                if (tableAttributes.Length == 1)
                {
                    ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
                    DaoObject obj = (DaoObject)ctor.Invoke(null);
                    if (obj.DataContextName.Equals(contextName))
                        CreateDaoTable(obj, agent);
                }
            }
        }

        public AddForeignKeyResult AddAllForeignKeys<T>(DatabaseAgent agent) where T: DaoObject, new()
        {
            Assembly daoAssembly = typeof(T).Assembly;
            string contextName = new T().DataContextName;

            AddForeignKeyResult retVal = AddAllForeignKeys(agent, daoAssembly, contextName);

            return retVal;
        }

        private AddForeignKeyResult AddAllForeignKeys(DatabaseAgent agent, Assembly daoAssembly, string contextName)
        {
            List<DaoForeignKeyInfo> foreignKeys = new List<DaoForeignKeyInfo>();
            foreach (Type type in daoAssembly.GetTypes())
            {
                object[] tableAttributes = type.GetCustomAttributes(typeof(DaoTable), false);
                if (tableAttributes.Length == 1)
                {
                    DaoTable tableAttribute = (DaoTable)tableAttributes[0];
                    ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
                    DaoObject obj = (DaoObject)ctor.Invoke(null);
                    if (obj.DataContextName.Equals(contextName))
                        foreignKeys.AddRange(this.GetForeignKeys(obj, tableAttribute.TableName));
                }
            }

            AddForeignKeyResult retVal = AddForeignKeyResult.Success;
            foreach (DaoForeignKeyInfo foreignKey in foreignKeys)
            {
                if (this.AddForeignKey(foreignKey, agent) == AddForeignKeyResult.Error)
                    retVal = AddForeignKeyResult.Error;
            }
            return retVal;
        }

        public AddForeignKeyResult AddForeignKey(string referencingTableName, string referencingColumnName, string referencedTableName, string referencedKeyColumn, DatabaseAgent agent)
        {
            string foreignKeyName = StringExtensions.RandomString(31, false,false);
            DaoForeignKeyColumn foreignKeyColumn = new DaoForeignKeyColumn(referencingColumnName, -1, foreignKeyName, referencedKeyColumn, referencedTableName);
            DaoForeignKeyInfo foreignKey = new DaoForeignKeyInfo(foreignKeyColumn, referencingTableName);
            return this.AddForeignKey(foreignKey, agent);
        }

        public void AddColumn(string table, string columnName, string dataType, DatabaseAgent agent)
        {
            agent.ExecuteSql(string.Format(DatabaseAgent.ADDCOLUMNFORMAT, table, columnName + " " + dataType));
        }

        public DataTable GetFireBirdTableDetail(string tableName, DatabaseAgent agent)
        {
            string tableDetailQuery = @"SELECT r.RDB$FIELD_NAME AS field_name,
            r.RDB$DESCRIPTION AS field_description,
            r.RDB$DEFAULT_VALUE AS field_default_value,
            r.RDB$NULL_FLAG AS field_not_null_constraint,
            f.RDB$FIELD_LENGTH AS field_length,
            f.RDB$FIELD_PRECISION AS field_precision,
            f.RDB$FIELD_SCALE AS field_scale,
            CASE f.RDB$FIELD_TYPE
              WHEN 261 THEN 'BLOB'
              WHEN 14 THEN 'CHAR'
              WHEN 40 THEN 'CSTRING'
              WHEN 11 THEN 'D_FLOAT'
              WHEN 27 THEN 'DOUBLE'
              WHEN 10 THEN 'FLOAT'
              WHEN 16 THEN 'INT64'
              WHEN 8 THEN 'INTEGER'
              WHEN 9 THEN 'QUAD'
              WHEN 7 THEN 'SMALLINT'
              WHEN 12 THEN 'DATE'
              WHEN 13 THEN 'TIME'
              WHEN 35 THEN 'TIMESTAMP'
              WHEN 37 THEN 'VARCHAR'
              ELSE 'UNKNOWN'
            END AS field_type,
            f.RDB$FIELD_SUB_TYPE AS field_subtype,
            coll.RDB$COLLATION_NAME AS field_collation,
            cset.RDB$CHARACTER_SET_NAME AS field_charset
       FROM RDB$RELATION_FIELDS r
       LEFT JOIN RDB$FIELDS f ON r.RDB$FIELD_SOURCE = f.RDB$FIELD_NAME
       LEFT JOIN RDB$COLLATIONS coll ON f.RDB$COLLATION_ID = coll.RDB$COLLATION_ID
       LEFT JOIN RDB$CHARACTER_SETS cset ON f.RDB$CHARACTER_SET_ID = cset.RDB$CHARACTER_SET_ID
      WHERE r.RDB$RELATION_NAME='" + tableName + @"'
    ORDER BY r.RDB$FIELD_POSITION";

            return agent.GetDataTableFromSql(tableDetailQuery);
        }

        private AddForeignKeyResult AddForeignKey(DaoForeignKeyInfo foreignKey, DatabaseAgent agent)
        {
            if (agent.DbType == DaoDbType.SQLite)
                return AddForeignKeyResult.Success; // foreign key constraints aren't supported in SQLite

            string foreignKeyName = string.Empty;
            try
            {
                foreignKeyName = foreignKey.DaoForeignKeyColumn.ForeignKeyName;
                if (agent.DbType == DaoDbType.Firebird)
                    foreignKeyName = StringExtensions.RandomString(31, false, false);
                // table1Name, fkName, column1Name, table2Name, column2Name
                agent.ExecuteSql(string.Format(
                    DatabaseAgent.ADDFOREIGNKEYFORMAT,
                    foreignKey.TableName,
                    foreignKeyName,
                    foreignKey.DaoForeignKeyColumn.ColumnName,
                    foreignKey.DaoForeignKeyColumn.ReferencedTable,
                    foreignKey.DaoForeignKeyColumn.ReferencedKey));
            }
            catch (FbException ex)
            {
                if (ex.ErrorCode == -2147467259)// foreign key already exists
                {
                    return AddForeignKeyResult.Success;
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Message.ToLower().Contains(string.Format("already an object named '{0}'", foreignKeyName).ToLower()))
                {
                    return AddForeignKeyResult.Success;
                }
                else
                {
                    return AddForeignKeyResult.Error;
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(string.Format("An error occurred adding foreign key: {0}\r\n{1}", ex.Message, ex.StackTrace));
                return AddForeignKeyResult.Error;
            }

            return AddForeignKeyResult.Success;
        }

        public CreateTableResult CreateDaoTable(DaoObject daoObject, DatabaseAgent agent)
        {
            DaoTable tableAttribute = (DaoTable)daoObject.GetType().GetCustomAttributes(typeof(DaoTable), true)[0];
            Expect.IsNotNull(tableAttribute);
            string tableName = tableAttribute.TableName;
            string idColumnName = this.GetIdColumnName(daoObject);
            Dictionary<PropertyInfo, DaoColumn> columns = this.GetColumns(daoObject);

            StringBuilder sqlColumns = new StringBuilder();
            bool first = true;
            foreach (PropertyInfo key in columns.Keys)
            {
                string dataType = this.TranslateToDataType(key);
                string special = "";
                bool primaryKey = false;
                if (key.HasCustomAttributeOfType<DaoIdColumn>(true, true))// -- old -> key.Name.Equals(idColumnName))
                {
                    special += this.GetIdentitySpec();
                } // -- added 2/1/11
                else if (key.HasCustomAttributeOfType<DaoPrimaryKeyColumn>(true, true))
                {
                    special += this.GetPrimaryKeySpec();
                    primaryKey = true;
                }
                // -- end added

                if (!columns[key].AllowNulls && !primaryKey)
                {
                    special += " NOT NULL";
                }

                if (!first)
                    sqlColumns.Append(", ");
                sqlColumns.AppendFormat("\"{0}\" {1} {2}", columns[key].ColumnName, dataType, special);
                first = false;
            }

            try
            {
                agent.ExecuteSql(string.Format(DatabaseAgent.CREATETABLEFORMAT, tableName, sqlColumns.ToString()));
                if (agent.DbType == DaoDbType.Firebird)
                    this.CreateFirebirdIdTable(tableName, idColumnName, agent);
            }
            catch (Exception ex)
            {
                return CatchException(ex, tableName);//CreateDaoTableResult.UnknownError;
            }

            return CreateTableResult.Success;
        }

        internal CreateTableResult CatchException(Exception ex, string tableName)
        {
            if(ex is FbException)
            {
                string message = ex.Message.ToLowerInvariant();
                string check = string.Format("{0} already exists", tableName).ToLower();
                if (message.Contains(check))
                    return CreateTableResult.AlreadyExists;
                else
                    return CreateTableResult.UnknownError;
            }

            if(ex is SqlException)
            {
                if (ex.Message.ToLower().Contains(string.Format("already an object named '{0}'", tableName).ToLower()))
                    return CreateTableResult.AlreadyExists;
                else
                    return CreateTableResult.UnknownError;
            }

            if(ex is SQLiteException)
            {
                if (ex.Message.ToLower().Contains(string.Format("{0} already exists", tableName).ToLower()))
                    return CreateTableResult.AlreadyExists;
                else
                    return CreateTableResult.UnknownError;
            }

            return CreateTableResult.UnknownError;
        }
        
        internal void CreateFirebirdIdTable(string tableName, string columnName, DatabaseAgent agent)
        {
            string IdTableName = string.Format("{0}NID", tableName);
            // create a table to hold the next id for the specified table

            /*             
             This is specifically for Firebird since it doesn't have a way to 
             easily create an autoincrementing id field.  The closest thing they
             have is "Generators" used in "before insert" triggers.  Attempts
             to go in that direction is proving time consuming.  So, I'm 
              implementing my own ID Generator to be used internally to this component.
             */
            try
            {
                agent.ExecuteSqlFormat(DatabaseAgent.CREATETABLEFORMAT, IdTableName, string.Format("{0} BIGINT", columnName));
                agent.ExecuteSqlFormat(DatabaseAgent.INSERTFORMAT, IdTableName, columnName, "0");
            }
            catch (Exception ex)
            {
                CreateTableResult result = CatchException(ex, IdTableName);
                if(result != CreateTableResult.Success && result != CreateTableResult.AlreadyExists)
                    throw ex;
            }
           
        }

        internal string GetIdentitySpec()
        {
            return GetIdentitySpec(1, 1);
        }

        internal string GetIdentitySpec(int startId, int step)
        {
            switch (this.type)
            {
                case DaoDbType.Invalid:
                    break;
                case DaoDbType.Odbc:
                    break;
                case DaoDbType.OleDb:
                    break;
                case DaoDbType.MSSql:
                    return "IDENTITY (" + startId + "," + step + ") NOT NULL PRIMARY KEY";
                case DaoDbType.Firebird:
                    return "NOT NULL PRIMARY KEY";
                case DaoDbType.SQLite:
                    return "PRIMARY KEY AUTOINCREMENT";
                default:
                    break;
            }

            return "";
        }

        internal string GetPrimaryKeySpec()
        {
            switch (this.type)
            {
                case DaoDbType.Invalid:
                    break;
                case DaoDbType.Odbc:
                    break;
                case DaoDbType.OleDb:
                    break;
                case DaoDbType.MSSql:
                    return "NOT NULL PRIMARY KEY";
                case DaoDbType.Firebird:
                    return "NOT NULL PRIMARY KEY";   
                case DaoDbType.SQLite:
                    return GetIdentitySpec();
                default:
                    break;
            }

            return "";
        }

        private Dictionary<PropertyInfo, DaoColumn> GetColumns(DaoObject daoObject)
        {
            PropertyInfo[] columns = CustomAttributeExtension.GetPropertiesWithAttributeOfType<DaoColumn>(daoObject.GetType());
            Dictionary<PropertyInfo, DaoColumn> retVals = new Dictionary<PropertyInfo, DaoColumn>();
            foreach (PropertyInfo info in columns)
            {
                DaoColumn column = (DaoColumn)info.GetCustomAttributes(typeof(DaoColumn), true)[0];
                if (!(column is DaoForeignKeyColumn))
                    retVals.Add(info, column);
            }

            return retVals;
        }

        private DaoForeignKeyInfo[] GetForeignKeys(DaoObject daoObject, string tableName)
        {
            PropertyInfo[] columns = CustomAttributeExtension.GetPropertiesWithAttributeOfType<DaoForeignKeyColumn>(daoObject.GetType());
            List<DaoForeignKeyInfo> retVals = new List<DaoForeignKeyInfo>();
            foreach (PropertyInfo info in columns)
            {
                DaoForeignKeyColumn fk = (DaoForeignKeyColumn)info.GetCustomAttributes(typeof(DaoForeignKeyColumn), true)[0];
                retVals.Add(new DaoForeignKeyInfo(fk, tableName));
            }

            return retVals.ToArray();
        }

        private string GetIdColumnName(DaoObject daoObject)
        {
            Type type = daoObject.GetType();
            PropertyInfo[] daoIdColumnAttribute = CustomAttributeExtension.GetPropertiesWithAttributeOfType<DaoIdColumn>(type);
            if (daoIdColumnAttribute.Length > 1)
                ExceptionHelper.ThrowInvalidOperation("Multiple column primary keys not supported");
            if (daoIdColumnAttribute.Length == 1)
                return daoIdColumnAttribute[0].Name;

            return "";
        }

        public void AddIdRequest(StringBuilder insertStatement)
        {
            this.AddIdRequest(insertStatement, "");
        }

        public void AddIdRequest(StringBuilder insertStatement, string fbColumnNameOrEmpty)
        {
            switch (this.type)
            {
                case DaoDbType.Invalid:
                    break;
                case DaoDbType.Odbc:
                    break;
                case DaoDbType.OleDb:
                    break;
                case DaoDbType.MSSql:
                    insertStatement.Append(";SELECT @@IDENTITY AS ID");
                    break;
                case DaoDbType.Firebird:
                    insertStatement.Append(string.Format(" RETURNING {0};", fbColumnNameOrEmpty));
                    break;
                case DaoDbType.SQLite:
                    insertStatement.Append(";SELECT last_insert_rowid() AS ID");
                    break;
                default:
                    break;
            }
        }

        internal string GetDbIdType(DaoDbType type)
        {
            switch (type)
            {
                case DaoDbType.Invalid:
                    break;
                case DaoDbType.Odbc:
                    break;
                case DaoDbType.OleDb:
                    break;
                case DaoDbType.MSSql:
                    return "bigint";
                case DaoDbType.Firebird:
                    return "INTEGER";
                case DaoDbType.SQLite:
                    return "INTEGER";
                default:
                    break;
            }

            return "";
        }

        public string TranslateToDataType(Type type)
        {
            switch (this.type)
            {
                case DaoDbType.Invalid:
                    break;
                case DaoDbType.Odbc:
                    break;
                case DaoDbType.OleDb:
                    break;
                case DaoDbType.MSSql:
                    return TranslateToMSSql(type);
                case DaoDbType.Firebird:
                    return TranslateToFirebird(type);
                case DaoDbType.SQLite:
                    return TranslateToSQLite(type);
                default:
                    break;
            }

            return "";
        }

        public string TranslateToDataType(PropertyInfo property)
        {
            switch (this.type)
            {
                case DaoDbType.Invalid:
                    break;
                case DaoDbType.Odbc:
                    break;
                case DaoDbType.OleDb:
                    break;
                case DaoDbType.MSSql:
                    return TranslateToMSSql(property);
                case DaoDbType.Firebird:
                    return TranslateToFirebird(property);
                case DaoDbType.SQLite:
                    return TranslateToSQLite(property);
                default:
                    break;
            }

            return "";
        }

        private string TranslateToSQLite(PropertyInfo property)
        {
            return TranslateToSQLite(property.PropertyType);
        }

        private string TranslateToSQLite(Type type)
        {
            if (type == typeof(long))
                return "INTEGER";

            if (type == typeof(int))
                return "INTEGER";

            if (type == typeof(bool))
                return "INTEGER";

            if (type == typeof(Guid))
                return "VARCHAR(38)";

            if (type == typeof(double))
                return "REAL";

            if (type == typeof(DateTime))
                return "TEXT";

            if (type == typeof(byte[]))
                return "BLOB";

            return "TEXT";
        }

        private string TranslateToMSSql(PropertyInfo property)
        {
            return TranslateToMSSql(property.PropertyType);
        }

        private string TranslateToMSSql(Type type)
        {
            if (type == typeof(long))
                return "bigint";

            if (type == typeof(int))
                return "int";

            if (type == typeof(bool))
                return "bit";

            if (type == typeof(Guid))
                return "uniqueidentifier";

            if (type == typeof(double))
                return "float";

            if (type == typeof(DateTime))
                return "datetime";

            if (type == typeof(byte[]))
                return "varbinary(4000)";

            return "varchar(4000)";//return "varchar(max)";
        }

        private string TranslateToFirebird(PropertyInfo property)
        {
            return TranslateToFirebird(property.PropertyType);
        }

        private string TranslateToFirebird(Type type)
        {
            if (type == typeof(long))
                return "INTEGER";

            if (type == typeof(int))
                return "INTEGER";

            if (type == typeof(bool))
                return "CHAR(1)";

            if (type == typeof(Guid))
                return "CHAR(38)";

            if (type == typeof(double))
                return "FLOAT";

            if (type == typeof(DateTime))
                return "TIMESTAMP";

            return "VARCHAR(2000)";
        }

        public override DbCommand CreateCommand()
        {
            switch (this.type)
            {
                case DaoDbType.Invalid:
                    throw new InvalidOperationException("Invalid provider type specified");
                case DaoDbType.Odbc:
                    return new OdbcCommand();
                case DaoDbType.OleDb:
                    return new OleDbCommand();
                case DaoDbType.MSSql:
                    return new SqlCommand();
                case DaoDbType.Firebird:
                    return new FbCommand();
                case DaoDbType.SQLite:
                    return new SQLiteCommand();
            }

            return null;
        }
        public override DbCommandBuilder CreateCommandBuilder()
        {
            switch (this.type)
            {
                case DaoDbType.Invalid:
                    throw new InvalidOperationException("Invalid provider type specified");
                case DaoDbType.Odbc:
                    return new OdbcCommandBuilder();
                case DaoDbType.OleDb:
                    return new OleDbCommandBuilder();
                case DaoDbType.MSSql:
                    return new SqlCommandBuilder();
                case DaoDbType.Firebird:
                    return new FbCommandBuilder();
                case DaoDbType.SQLite:
                    return new SQLiteCommandBuilder();
            }
            return null;
        }
        
        public override DbConnection CreateConnection()
        {
            if (string.IsNullOrEmpty(this.ConnectionString))
                throw new InvalidOperationException("The ConnectionString property was not set");

            switch (this.type)
            {
                case DaoDbType.Invalid:
                    throw new InvalidOperationException("Invalid provider type specified");
                case DaoDbType.Odbc:
                    return new OdbcConnection(this.ConnectionString);
                case DaoDbType.OleDb:
                    return new OleDbConnection(this.ConnectionString);
                case DaoDbType.MSSql:
                    return new SqlConnection(this.ConnectionString);
                case DaoDbType.Firebird:
                    DbConnectionStringBuilder connStringBuilder = this.CreateConnectionStringBuilder();
                    if (!File.Exists(connStringBuilder["Database"].ToString()))
                        FbConnection.CreateDatabase(this.ConnectionString, false);
                    return new FbConnection(this.ConnectionString);
                case DaoDbType.SQLite:
                    return new SQLiteConnection(this.ConnectionString);
            }

            return null;
        }

        public string ConnectionString { get; set; }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            switch (this.type)
            {
                case DaoDbType.Invalid:
                    throw new InvalidOperationException("Invalid provider type specified");
                case DaoDbType.Odbc:
                    return new OdbcConnectionStringBuilder(this.ConnectionString);
                case DaoDbType.OleDb:
                    return new OleDbConnectionStringBuilder(this.ConnectionString);
                case DaoDbType.MSSql:
                    return new SqlConnectionStringBuilder(this.ConnectionString);
                case DaoDbType.Firebird:
                    return new FbConnectionStringBuilder(this.ConnectionString);
                case DaoDbType.SQLite:
                    return new SQLiteConnectionStringBuilder(this.ConnectionString);
            }
            return null;
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            switch (this.type)
            {
                case DaoDbType.Invalid:
                    throw new InvalidOperationException("Invalid provider type specified");
                case DaoDbType.Odbc:
                    return new OdbcDataAdapter();
                case DaoDbType.OleDb:
                    return new OleDbDataAdapter();
                case DaoDbType.MSSql:
                    return new SqlDataAdapter();
                case DaoDbType.Firebird:
                    return new FbDataAdapter();
                case DaoDbType.SQLite:
                    return new SQLiteDataAdapter();

            }
            return null;
        }

        public override DbParameter CreateParameter()
        {
            return CreateParameter(string.Empty, null);
        }

        public DbParameter CreateParameter(string name, object value)
        {
            if (value == null)
                value = DBNull.Value;

            switch (this.type)
            {
                case DaoDbType.Invalid:
                    throw new InvalidOperationException("Invalid provider type specified");
                case DaoDbType.Odbc:
                    return new OdbcParameter(name, value);
                case DaoDbType.OleDb:
                    return new OleDbParameter(name, value);
                case DaoDbType.MSSql:
                    return new SqlParameter(name, value);
                case DaoDbType.Firebird:
                    return new FbParameter(name, value);
                case DaoDbType.SQLite:
                    return new SQLiteParameter(name, value);
            }

            return null;
        }

        public DbParameter CopyParameter(DbParameter parameter)
        {
            return this.CreateParameter(parameter.ParameterName, parameter.Value);
        }
    }
}
