/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Reflection;
using System.Threading;
using Naizari.Extensions;
using Naizari.Configuration;
using Naizari.Testing;
using Naizari.Helpers;
using Naizari.Logging;
using Naizari.Data.Common;

namespace Naizari.Data
{
    /// <summary>
    /// This class is intended to be a replacement for the DatabaseUtility as the DaoContext.DatabaseUtility property.
    /// The DatabaseUtility class has a bunch of stuff in it that is there due to old compatibility requirements and is heavily 
    /// dependent on Smo for some of its less used methods.  
    /// </summary>
    public abstract class DatabaseAgent 
    {
        // List of Types keyed by connection string
        protected static Dictionary<string, List<Type>> ensuredSchemas;
        private static Dictionary<string, DatabaseAgent> agents;

        public const string DataNamespace = "Naizari.Data.";

        public const string INSERTFORMAT = "INSERT INTO [{0}] ({1}) VALUES ({2})";
        public const string SELECTFORMAT = "SELECT {0}* FROM [{1}] {2}";
        public const string SELECTPROPERTYFORMAT = "SELECT {0} FROM [{1}] WHERE {2}";
        public const string SELECTWHEREFORMAT = "SELECT {0}* FROM [{1}] WHERE {2}";
        public const string SELECTTOPFORMAT = "SELECT TOP {0} * FROM [{1}] {2}";
        public const string SELECTDISTINCTFORMAT = "SELECT DISTINCT [{0}] FROM [{1}] WHERE {2}";
        public const string SQLITESELECTTOPFORMAT = "SELECT * FROM [{1}] {2} LIMIT {0}";
        public const string UPDATEFORMAT = "UPDATE [{0}] SET {1} WHERE {2}";
        public const string DELETEFORMAT = "DELETE FROM [{0}] WHERE {1}";
        public const string CREATETABLEFORMAT = "CREATE TABLE [{0}] ({1})";
        public const string ADDCOLUMNFORMAT = "ALTER TABLE [{0}] ADD {1}";
        public const string SELECTFROMPROPERTYLISTFORMAT = "SELECT * FROM [{0}] WHERE {1} IN ({2})";
        public const string SELECTPROPERTYFROMPROPERTYLISTFORMAT = "SELECT {0} FROM [{1}] WHERE {2} IN ({3})";
        public const string ADDFOREIGNKEYFORMAT = "ALTER TABLE [{0}] ADD CONSTRAINT {1} FOREIGN KEY ({2}) REFERENCES [{3}] ({4})";
        public const string SELECTCOUNTFORMAT = "SELECT COUNT(*) AS RESULT FROM [{0}] WHERE {1}";

        protected DaoContextProviderFactory providerFactory;
        protected List<DbConnection> connections;

        protected AutoResetEvent resetEvent;
        protected int maxConnections;
        protected ILogger logger;

        static DatabaseAgent()
        {
            ensuredSchemas = new Dictionary<string, List<Type>>();
            agents = new Dictionary<string, DatabaseAgent>();
        }

        protected DatabaseAgent()
        {
            this.RetryCount = 3;
            this.resetEvent = new AutoResetEvent(true);
            this.connections = new List<DbConnection>();
            this.maxConnections = 20;
            this.logger = new NullLogger();
        }

        protected DatabaseAgent(DaoDbType type, string connectionString): this()
        {
            this.providerFactory = new DaoContextProviderFactory(type, connectionString);
        }

        protected DatabaseAgent(DaoDbType type, string connectionString, int maxConnections)
            : this(type, connectionString)
        {
            this.maxConnections = maxConnections;
        }

        public abstract void Shrink();

        public virtual string SelectTopFormat
        {
            get
            {
                return DatabaseAgent.SELECTTOPFORMAT;
            }
        }
        public Exception ShrinkException { get; internal set; }

        static object agentLock = new object();            
        public static DatabaseAgent GetAgent(string contextName, DaoDbType type)
        {
            string connectionString = DefaultConfiguration.GetAppSetting("DaoContext." + LogEventData.ContextName);            

            if (string.IsNullOrEmpty(connectionString) && type == DaoDbType.SQLite)
            {
                return AppDb.Current;
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new UnableToDetermineConnectionStringException(LogEventData.ContextName, type);//InvalidOperationException("Unable to determine connection string from Dao.conf or default configuration for contextName '" + LogEventData.ContextName + "' DaoDbType '" + type.ToString() + "'");
            }

            DatabaseAgent retVal;
            lock (agentLock)
            {
                if (agents.ContainsKey(connectionString))
                    return agents[connectionString];

                retVal = DatabaseAgent.CreateAgent(type, connectionString);
                agents.Add(connectionString, retVal);
                return retVal;
            }            
        }
        
        public static DatabaseAgent CreateAgent(DaoDbType type, string connectionString)
        {
            if (type == DaoDbType.Invalid)
                throw new InvalidOperationException("Invalid DaoDbType value specified.");

            Type agentType = Type.GetType(DataNamespace + type.ToString() + "Agent");
            ConstructorInfo ctor = agentType.GetConstructor(new Type[] { typeof(string) });
            if (ctor == null)
                throw new InvalidOperationException("The specified DaoDbType type does not have a parameterless constructor");

            DatabaseAgent agent = (DatabaseAgent)ctor.Invoke(new object[] { connectionString });
            return agent;
        }

        public string ConnectionString
        {
            get
            {
                return this.providerFactory.ConnectionString;
            }
        }
        public virtual DaoDbType DbType
        {
            get
            {
                return this.providerFactory.DbType;
            }
        }

        public DaoContextProviderFactory ProviderFactory
        {
            get { return this.providerFactory; }
        }

        public DatabaseAgent TryEnsureSchema<T>() where T : DaoObject, new()
        {
            try
            {
                EnsureSchema<T>();
            }
            catch (Exception ex)
            {
                Log.Default.AddEntry("An error occurred ensuring schema: {0}", ex, ex.Message);
            }
            return this;
        }
        
        public DatabaseAgent EnsureSchema<T>() where T : DaoObject, new()
        {
            if (ensuredSchemas.ContainsKey(this.ConnectionString) &&
                ensuredSchemas[this.ConnectionString].Contains(typeof(T)))
                return this;

            lock (ensuredSchemas)
            {
                this.providerFactory.CreateDaoSchema<T>(this);
                if (!ensuredSchemas.ContainsKey(this.ConnectionString))
                    ensuredSchemas.Add(this.ConnectionString, new List<Type>());
                ensuredSchemas[this.ConnectionString].Add(typeof(T));
            }
            return this;
        }

        public void EnsureSchema(Assembly assembly, string contextName)
        {
            try
            {
                this.providerFactory.CreateDaoSchema(this, assembly, contextName);
            }
            catch (Exception ex)
            {
                Log.Default.AddEntry("An error occurred ensuring schema: {0}", ex, ex.Message);
            }
        }

        public void CreateTable(string tableName, string idColumnName, string columnDefs)
        {
            if (this.DbType == DaoDbType.Firebird)
            {
                this.ProviderFactory.CreateFirebirdIdTable(tableName, idColumnName, this);
            }
            //if (addIdColumnDef)
            columnDefs = idColumnName + " " + this.ProviderFactory.GetDbIdType(this.DbType) + " " + this.ProviderFactory.GetIdentitySpec() + ", " + columnDefs;
            this.ExecuteSql(string.Format(DatabaseAgent.CREATETABLEFORMAT, tableName, columnDefs));
        }

        object fbIdLock = new object();

        internal void Delete<T>(DaoSearchFilter filter) where T: DaoObject, new()
        {
            if (this.DbType != DaoDbType.MSSql)
                filter.UseJulianDates = true;
            this.ExecuteSql(string.Format(DELETEFORMAT, new T().TableName, filter.ToString()));
        }

        internal int Insert(string tableName, string[] columnNames, string idColumnName, params DbParameter[] dbParameters)
        {
            int retVal = -1;
            List<string> atParameterNames = new List<string>();
            foreach (DbParameter parameter in dbParameters)
            {
                atParameterNames.Add(parameter.ParameterName);
            }

            StringBuilder insert = new StringBuilder();

            long newId = -1;
            if (this.DbType == DaoDbType.Firebird)
            {
                lock (fbIdLock)
                {
                    List<string> newColumnNames = new List<string>(columnNames);
                    newColumnNames.Add(idColumnName);
                    columnNames = newColumnNames.ToArray();

                    // get the next id
                    string idTableName = tableName + "NID";
                    DataTable fbIdDataTable = this.GetDataTableFromSql(string.Format("SELECT * FROM {0}", idTableName));
                    Expect.IsTrue(fbIdDataTable.Rows.Count > 0, "There were no rows returned when trying to retrieve the next Firebird ID.");
                    Expect.IsTrue(fbIdDataTable.Rows.Count < 2, "There was more than one row returned when trying to retrieve the next Firebird ID.");
                    object value = fbIdDataTable.Rows[0][0];
                    newId = (long)value;

                    // find the "ID" parameter and set its value since this is Firebird
                    bool set = false;
                    foreach (DbParameter parameter in dbParameters)
                    {
                        if (parameter.ParameterName.Equals("@" + idColumnName))
                        {
                            parameter.Value = newId;
                            set = true;
                            break;
                        }
                    }

                    // add the "ID" parameter if it wasn't specified.
                    if (!set)
                    {
                        atParameterNames.Add("@" + idColumnName);
                        List<DbParameter> newParameters = new List<DbParameter>(dbParameters);
                        newParameters.Add(this.CreateParameter(idColumnName, newId));
                        dbParameters = newParameters.ToArray();
                    }

                    // increment old id
                    DbParameter nextIdParam = this.CreateParameter("@NewId", newId + 1);
                    this.ExecuteSql(string.Format("UPDATE {0} SET {1} = @NewId", idTableName, idColumnName), nextIdParam);
                }
            }


            insert.AppendFormat(INSERTFORMAT, 
                tableName, 
                StringExtensions.ToCommaDelimited(columnNames), 
                StringExtensions.ToCommaDelimited(atParameterNames.ToArray()));

            providerFactory.AddIdRequest(insert, idColumnName);

            DataTable data = this.GetDataTableFromSql(insert.ToString(), dbParameters);//db.GetDataTableFromSql(insert.ToString(), parameters.ToArray());
            if (data.Rows.Count == 1 && data.Columns.Count == 1)
            {
                object obj = data.Rows[0][0];
                if (obj is DBNull)
                {
                    retVal = 0;
                }
                else
                {
                    retVal = System.Convert.ToInt32(data.Rows[0][0]);
                }
            }
            else if (newId != -1)
            {
                retVal = System.Convert.ToInt32(newId);
            }

            return retVal;
        }

        public int Count<T>(DaoSearchFilter filter) where T: DaoObject, new()
        {
            T proxy = new T();
            DataTable result = this.GetDataTableFromSql(string.Format(SELECTCOUNTFORMAT, proxy.TableName, filter.ToString()), filter.DbParameters);
            Expect.IsTrue(result.Rows.Count == 1);

            return (int)result.Rows[0][0];
        }

        public T SelectById<T>(int id) where T: DaoObject, new()
        {
            return SelectById<T>((long)id);
        }

        public T SelectById<T>(long id) where T : DaoObject, new()//, string tableName, string columnName, string contextName) where T : DaoObject, new()
        {
            T proxy = new T();
            string topCount = "";
            string tableName = proxy.TableName;
            string idColumn = proxy.IdColumnName;
            if (string.IsNullOrEmpty(idColumn))
            {
                idColumn = DatabaseAgent.GetUniquenessColumn<T>().ColumnName;
            }

            string whereClause = string.Format("{0} = @ID", idColumn);
            T[] retVals = this.ExecuteSql<T>(string.Format(DatabaseAgent.SELECTWHEREFORMAT, topCount, tableName, whereClause), this.CreateParameter("@ID", id));
            if (retVals.Length > 1)
                throw new MultipleEntriesFoundException();

            if (retVals.Length == 0)
                return null;
            else
                return retVals[0];
        }
        
        public T[] SelectListWhere<T>(DaoSearchFilter filter) where T : DaoObject, new()
        {
            return Select<T>(filter);
        }

        public T[] SelectListWhere<T>(Enum column, object value) where T : DaoObject, new()
        {
            return SelectListWhere<T>(column.ToString(), value);
        }

        private T[] SelectListWhere<T>(string column, object value) where T : DaoObject, new()//, string tableName, string column, string contextName) where T : DaoObject, new()
        {
            Expect.IsNotNull(column, "parameter column cannot be null");
            Expect.IsNotNull(value, "parameter value cannot be null");
            T proxy = new T();

            DbParameter parameter = this.CreateParameter("@value", value);
            DaoSearchFilter filter = new DaoSearchFilter();
            filter.AddParameter(column, value);
            return Select<T>(filter);
        }

        public T[] Search<T>(DaoSearchFilter filter) where T: DaoObject, new()
        {
            return Select<T>(filter);
        }

        public T[] Search<T>(DaoSearchFilter filter, OrderBy order) where T : DaoObject, new()
        {
            return Search<T>(filter, order, -1);
        }

        public T[] Search<T>(DaoSearchFilter filter, OrderBy order, int count) where T : DaoObject, new()
        {
            if (this.DbType != DaoDbType.MSSql)
                filter.UseJulianDates = true;

            return Select<T>(filter.ToString(), order.ToString(), count, filter.DbParameters);
        }

        public T[] Select<T>(DaoSearchFilter filter) where T: DaoObject, new()
        {
            return Select<T>(filter, OrderBy.None, -1);
        }

        public T SelectOneWhere<T>(DaoSearchFilter filter) where T : DaoObject, new()
        {
            T[] results = Select<T>(filter);
            if (results.Length > 1)
                throw new MultipleEntriesFoundException();
            if (results.Length == 0)
                return null;
            else
                return results[0];
        }

        public T SelectTop<T>(OrderBy orderBy) where T : DaoObject, new()
        {
            T[] results = SelectTop<T>(orderBy, 1);
            if (results.Length == 1)
                return results[0];

            return null;
        }

        public T[] SelectTop<T>(OrderBy orderBy, int count) where T : DaoObject, new()
        {
            Expect.IsNotNull(orderBy, "orderBy can't be null.");


            T proxy = new T();
            string table = proxy.TableName;
            string format = this.SelectTopFormat;//this.DbType == DaoDbType.SQLite ? SQLITESELECTTOPFORMAT : SELECTTOPFORMAT;
            string sql = string.Format(format, count, table, orderBy.ToString());
            DataTable data = GetDataTableFromSql(sql);
            return this.ToObjectArray<T>(data);
        }

        public T[] SelectTop<T>(DaoSearchFilter filter, OrderBy orderBy, int count) where T : DaoObject, new()
        {
            return Select<T>(filter, orderBy, count);
        }

        public T[] Select<T>(DaoSearchFilter filter, OrderBy orderBy, int count) where T : DaoObject, new()
        {
            Expect.IsNotNull(orderBy, "orderBy by can't be null");
            if (this.DbType != DaoDbType.MSSql)
                filter.UseJulianDates = true;
            return Select<T>(filter.ToString(), orderBy.ToString(), count, filter.DbParameters);
        }

        /// <summary>
        /// Get an array of values representing the specified column using the specified filter.
        /// </summary>
        /// <typeparam name="T">The DaoType</typeparam>
        /// <typeparam name="R">The return type</typeparam>
        /// <param name="field"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public R[] SelectPropertyList<T, R>(Enum field, DaoSearchFilter filter) where T : DaoObject, new()
        {
            return SelectPropertyList<T, R>(EnumToColumnName<T>(field), filter);
        }

        internal R[] SelectPropertyList<T, R>(string field, DaoSearchFilter filter) where T : DaoObject, new()
        {
            return SelectPropertyList<T, R>(field, filter, OrderBy.None);
        }

        /// <summary>
        /// Get an array of values representing the specified column using the specified filter.
        /// </summary>
        /// <typeparam name="T">The DaoType</typeparam>
        /// <typeparam name="R">The return type</typeparam>
        /// <param name="field"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        internal R[] SelectPropertyList<T, R>(string field, DaoSearchFilter filter, OrderBy orderBy) where T : DaoObject, new()
        {
            T proxy = new T();
            if (this.DbType != DaoDbType.MSSql)
                filter.UseJulianDates = true;
            string sql = string.Format(SELECTPROPERTYFORMAT, field, proxy.TableName, filter.ToString());
            sql = string.Format("{0}{1}", sql, orderBy.ToString());
            DataTable results = this.GetDataTableFromSql(sql, filter.DbParameters);
            List<R> returnValues = new List<R>();
            foreach (DataRow row in results.Rows)
            {
                returnValues.Add((R)System.Convert.ChangeType(row[field], typeof(R)));//(R)row[field]);
            }

            return returnValues.ToArray();
        }

        public T[] SelectByIdList<T>(int[] ids) where T : DaoObject, new()
        {
            long[] longs = new long[ids.Length];
            ids.CopyTo(longs, 0);
            return SelectByIdList<T>(longs);
        }

        public T[] SelectByIdList<T>(long[] ids) where T : DaoObject, new()
        {
            if (ids.Length == 0)
                return new T[] { };

            object[] objIds = new object[ids.Length];
            ids.CopyTo(objIds, 0);
            return SelectByIdList<T>(objIds);
        }

        public T[] SelectByIdList<T>(object[] ids) where T : DaoObject, new()
        {
            return SelectByIdList<T>(ids, OrderBy.None);
        }

        public T[] SelectByIdList<T>(long[] ids, OrderBy orderBy) where T : DaoObject, new()
        {
            object[] objIds = new object[ids.Length];
            ids.CopyTo(objIds, 0);
            return SelectByIdList<T>(objIds, orderBy);
        }
        
        public T[] SelectByIdList<T>(object[] ids, OrderBy orderBy) where T : DaoObject, new()
        {
            T proxy = new T();
            string idColumn = proxy.IdColumnName;
            if (string.IsNullOrEmpty(idColumn))
            {
                idColumn = DatabaseAgent.GetUniquenessColumn<T>().ColumnName;
            }

            return SelectByPropertyList<T>(idColumn, ids, orderBy);
        }

        public T[] SelectByPropertyList<T>(Enum field, object[] propertyValues) where T : DaoObject, new()
        {
            return SelectByPropertyList<T>(EnumToColumnName<T>(field), propertyValues);
        }

        internal protected T[] SelectByPropertyList<T>(string field, object[] propertyValues) where T : DaoObject, new()
        {
            return SelectByPropertyList<T>(field, propertyValues, OrderBy.None);
        }

        internal protected T[] SelectByPropertyList<T>(string field, long[] propertyValues, OrderBy orderBy) where T : DaoObject, new()
        {
            object[] objIds = new object[propertyValues.Length];
            propertyValues.CopyTo(objIds, 0);
            return SelectByPropertyList<T>(field, objIds, orderBy);
        }

        internal protected T[] SelectByPropertyList<T>(string field, object[] propertyValues, OrderBy orderBy) where T : DaoObject, new()
        {
            if (string.IsNullOrEmpty(field))
            {
                throw new ArgumentNullException("field");
            }

            T proxy = new T();
            StringBuilder commaSeparatedValues;
            List<DbParameter> dbParamaters;
            CreateParameterList(propertyValues, out commaSeparatedValues, out dbParamaters);

            string sql = string.Format(SELECTFROMPROPERTYLISTFORMAT, proxy.TableName, field, commaSeparatedValues.ToString());
            sql = string.Format("{0}{1}", sql, orderBy.ToString());
            return ExecuteSql<T>(sql, dbParamaters.ToArray());
        }

        internal protected object[] SelectByPropertyList(Type type, string field, object[] propertyValues, SortOrder sortOrder)
        {
            return SelectByPropertyList(type, field, propertyValues, new OrderBy(field, sortOrder));
        }

        internal protected object[] SelectByPropertyList(Type type, string field, object[] propertyValues, OrderBy orderBy)
        {
            if (string.IsNullOrEmpty(field))
            {
                throw new ArgumentNullException("field");
            }

            DaoTable daoTable = type.GetCustomAttributeOfType<DaoTable>();
            StringBuilder commaSeparatedValues;
            List<DbParameter> dbParamaters;
            CreateParameterList(propertyValues, out commaSeparatedValues, out dbParamaters);

            string sql = string.Format(SELECTFROMPROPERTYLISTFORMAT, daoTable.TableName, field, commaSeparatedValues.ToString());
            sql = string.Format("{0}{1}", sql, orderBy.ToString());
            
            return ExecuteSql(type, sql, dbParamaters.ToArray());
        }

        /// <summary>
        /// Return an array of the specified return property R for the specified table T
        /// </summary>
        /// <typeparam name="T">The type representing the table to query</typeparam>
        /// <typeparam name="R">The type representing the return type</typeparam>
        /// <param name="returnField">The enum representing the field/column to return </param>
        /// <param name="compareField">The enum representing the field/column to compare to the provided values</param>
        /// <param name="propertyValues">The values to compare</param>
        /// <returns></returns>
        public R[] SelectPropertyByPropertyList<T, R>(Enum returnField, Enum compareField, params object[] propertyValues) where T : DaoObject, new()
        {
            T proxy = new T();
            string returnProperty = EnumToColumnName<T>(returnField);
            string tableName = proxy.TableName;
            string compareProperty = EnumToColumnName<T>(compareField);
           
            StringBuilder commaSeparatedValues;
            List<DbParameter> dbParamaters;
            CreateParameterList((object[])propertyValues, out commaSeparatedValues, out dbParamaters);

            DataTable results = GetDataTableFromSql(string.Format(
                SELECTPROPERTYFROMPROPERTYLISTFORMAT, returnProperty, proxy.TableName, compareProperty, commaSeparatedValues)
                , dbParamaters.ToArray());

            List<R> values = new List<R>();
            foreach (DataRow row in results.Rows)
            {
                values.Add((R)row[returnProperty]);
            }

            return values.ToArray();
        }

        private void CreateParameterList(object[] propertyValues, out StringBuilder commaSeparatedValues, out List<DbParameter> dbParamaters)
        {
            commaSeparatedValues = new StringBuilder();
            dbParamaters = new List<DbParameter>();
            bool first = true;
            for (int i = 0; i < propertyValues.Length; i++)
            {
                if (!first)
                {
                    commaSeparatedValues.Append(",");
                }
                commaSeparatedValues.AppendFormat("@p{0}", i);
                first = false;

                dbParamaters.Add(this.CreateParameter(string.Format("@p{0}", i), propertyValues[i]));
            }
        }

        public R[] SelectDistinct<T, R>(Enum field, DaoSearchFilter filter) where T : DaoObject, new()
        {
            T proxy = new T();
            if (this.DbType != DaoDbType.MSSql)
                filter.UseJulianDates = true;

            string sql = string.Format(SELECTDISTINCTFORMAT, EnumToColumnName<T>(field), proxy.TableName, filter.ToString());
            DataTable results = this.GetDataTableFromSql(sql, filter.DbParameters);
            List<R> returnValues = new List<R>();
            foreach (DataRow row in results.Rows)
            {
                returnValues.Add((R)row[field.ToString()]);
            }

            return returnValues.ToArray();
        }

        internal T[] Select<T>(string whereClause, string orderBy, int count, params DbParameter[] parameters) where T : DaoObject, new()
        {
            string topCount = ParseTopAndWhere(ref whereClause, count);

            T proxy = new T();
            string sql = GetSql(whereClause, ref orderBy, topCount, proxy.TableName);          

            DataTable data = this.GetDataTableFromSql(sql, parameters);
            return this.ToObjectArray<T>(data);
        }

        protected object[] Select(string tableName, DaoSearchFilter filter, OrderBy orderBy, int count, params DbParameter[] parameters)//string tableName, string whereClause, string orderBy, int count, params DbParameter[] parameters)
        {
            string whereClause = filter.ToString();
            string orderByString = orderBy.ToString();
            string topCount = ParseTopAndWhere(ref whereClause, count);
            string sql = GetSql(whereClause, ref orderByString, topCount, tableName);
            DataTable data = this.GetDataTableFromSql(sql, parameters);
            return null;
        }

        protected virtual string GetSql(string whereClause, ref string orderBy, string topCount, string tableName)
        {
            string sql = string.Format(SELECTFORMAT, topCount, tableName, whereClause);

            if (!string.IsNullOrEmpty(orderBy) && !orderBy.Trim().ToUpperInvariant().StartsWith("ORDER BY"))
            {
                orderBy = new OrderBy(orderBy, SortOrder.Ascending).ToString();
            }

            if (!string.IsNullOrEmpty(orderBy))
                sql += orderBy;
            return sql;
        }

        protected virtual string ParseTopAndWhere(ref string whereClause, int count)
        {
            if (!string.IsNullOrEmpty(whereClause) && !whereClause.Trim().ToUpperInvariant().StartsWith("WHERE"))
                whereClause = " WHERE " + whereClause;

            string topCount = "";
            if (count > 0)
                topCount = string.Format("TOP {0} ", count);
            return topCount;
        }

        internal object[] ToObjectArray(DataTable data, Type type)
        {
            List<object> ret = new List<object>();

            foreach (DataRow row in data.Rows)
            {
                object newVal = Convert(row, type);
                ret.Add(newVal);
            }

            return ret.ToArray();
        }

        internal T[] ToObjectArray<T>(DataTable data) where T : DaoObject, new()
        {
            List<T> ret = new List<T>();

            foreach (DataRow row in data.Rows)
            {
                T newVal = Convert<T>(row);
                ret.Add(newVal);
            }
            return ret.ToArray();
        }

        public T Convert<T>(DataRow row) where T : DaoObject, new()
        {
            return (T)Convert(row, typeof(T));
        }

        public DaoObject Convert(DataRow row, Type type)
        {
            ConstructorInfo ctor = type.GetConstructor(new Type[] { });
            DaoObject newVal = (DaoObject)ctor.Invoke(null);
            newVal.DatabaseAgent = this;
            Dictionary<string, string> propToColumnMap = newVal.GetPropertyToColumnMap();
            DatabaseAgent.FromDataRow(newVal, row, propToColumnMap);
            foreach (string key in propToColumnMap.Values)
            {
                if (!newVal.initialValues.ContainsKey(key))
                    newVal.initialValues.Add(key, null);
            }
            return newVal;
        }

        public void ExecuteSqlFormat(string sqlFormat, params object[] args)
        {
            this.ExecuteSql(string.Format(sqlFormat, args));
        }
        
        public void ExecuteSql(string sqlStatement)
        {
            this.ExecuteSql(sqlStatement, new DbParameter[] { });
        }

        public void ExecuteSql(string sqlStatement, params DbParameter[] sqlParameters)
        {
            this.ExecuteSql(sqlStatement, CommandType.Text, sqlParameters);
        }

        public void ExecuteStoredProcedure(string procedureName, params DbParameter[] sqlParameters)
        {
            this.ExecuteSql(procedureName, CommandType.StoredProcedure, sqlParameters);
        }

        public void ExecuteSql(string sqlStatement, CommandType commandType, params DbParameter[] dbParameters)
        {
            DbConnection conn = GetDbConnection();
            try
            {
                using (conn)
                {
                    conn.Open();
                    DbCommand cmd = providerFactory.CreateCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sqlStatement;
                    cmd.CommandType = commandType;//CommandType.Text;
                    cmd.CommandTimeout = 1000;
                    foreach (DbParameter param in dbParameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                ReleaseConnection(conn);
            }
        }

        public T[] ExecuteSql<T>(string sqlStatement) where T : DaoObject, new()
        {
            return ExecuteSql<T>(sqlStatement, new DbParameter[] { });
        }

        public T[] ExecuteSql<T>(string sqlStatement, params DbParameter[] parameters) where T : DaoObject, new()
        {
            T proxy = new T();
            if (!sqlStatement.ToLowerInvariant().Contains("from"))
            {
                sqlStatement += string.Format(" FROM {0}", proxy.TableName);
            }

            DataTable data = this.GetDataTableFromSql(sqlStatement, parameters);
            return this.ToObjectArray<T>(data);
        }

        public object[] ExecuteSql(Type type, string sqlStatement, params DbParameter[] parameters)
        {
            DaoTable daoTable;
            type.HasCustomAttributeOfType<DaoTable>(out daoTable);
            if (daoTable == null)
            {
                throw new InvalidOperationException("Ivalid type specified");
            }

            if (!sqlStatement.ToLowerInvariant().Contains("from"))
            {
                sqlStatement += string.Format(" FROM {0}", daoTable.TableName);
            }

            DataTable data = this.GetDataTableFromSql(sqlStatement, parameters);
            return this.ToObjectArray(data, type);
        }

        public T[] ExecuteStoredProcedure<T>(string procedureName) where T : DaoObject, new()
        {
            return ExecuteStoredProcedure<T>(procedureName, new DbParameter[] { });
        }

        public T[] ExecuteStoredProcedure<T>(string procedureExecStatement, params DbParameter[] parameters) where T : DaoObject, new()
        {            
            DataTable data = this.GetDataTableFromStoredProcedure(procedureExecStatement, parameters);
            return this.ToObjectArray<T>(data);
        }

        public string GetEnumCSharpCode<T>(string enumName, string valuePropertyName, string namePropertyName) where T: DaoObject, new()
        {
            T instance = new T();
            T[] values = ExecuteSql<T>(string.Format(SELECTFORMAT, "", instance.TableName, ""));
            StringBuilder code = new StringBuilder("public enum " + enumName + "\r\n{");
            for(int i = 0 ; i < values.Length; i++)
            {
                T value = values[i];
                PropertyInfo valueProperty = typeof(T).GetProperty(valuePropertyName);
                if (valueProperty == null)
                    throw new InvalidOperationException("Invalid valuePropertyName specified");
                
                PropertyInfo nameProperty = typeof(T).GetProperty(namePropertyName);
                if (nameProperty == null)
                    throw new InvalidOperationException("Invalid namePropertyName specified");

                object nameProperyValue = nameProperty.GetValue(value, null);
                object valuePropertyValue = valueProperty.GetValue(value, null);

                if(nameProperyValue == null || valuePropertyValue == null)
                {
                    throw new InvalidOperationException("Null database values are not allowed when calling " + MethodBase.GetCurrentMethod().Name);                
                }

                Type valueType = valuePropertyValue.GetType();
                if (valueType != typeof(int) && valueType != typeof(long))
                {
                    throw new InvalidOperationException("valueProperty must be either int or long not " + valueType.Name + " when calling " + MethodBase.GetCurrentMethod().Name);
                }

                code.AppendFormat("\t{0} = {1}", nameProperyValue.ToString().ToCSharpVariableNameWithoutNumbers(), valuePropertyValue.ToString());

                if (i != values.Length - 1)
                {
                    code.AppendLine(",");
                }
            }

            code.Append("}");

            return code.ToString();
        }

        /// <summary>
        /// Will insert value and names into the specified table T for the specified StatusEnum.
        /// Once done for one table T that one will not be done again during the applications life.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="StatusEnum"></typeparam>
        public void SetEnumTable<T, StatusEnum>() where T : DaoObject, new()
        {
            FieldInfo[] fields = typeof(StatusEnum).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                object enumValue = field.GetRawConstantValue();
                string enumString = field.Name;
                DaoSearchFilter filter = new DaoSearchFilter();
                filter.AddParameter("Value", enumValue);
                filter.AddParameter("Status", enumString); 

                T temp = new T();
                DataTable existing = this.GetDataTableFromSql(string.Format(SELECTFORMAT, "", temp.TableName, "WHERE " + filter.ToString()), filter.DbParameters);
                if (existing.Rows.Count == 1)
                {
                    return;
                }
                else if (existing.Rows.Count > 1)
                {
                    Log.Default.AddEntry("More than 1 entry was found for enum value {0}.{1} in table {2}", LogEventType.Warning, typeof(StatusEnum).Name, enumString, temp.TableName);
                    return;
                }

                MethodInfo[] methods = typeof(T).GetMethods();
                IEnumerable<MethodInfo> methodResults = from method in methods
                                                        where method.Name.Equals("New")
                                                            && method.GetParameters().Length == 1
                                                        select method;
                object insertInstance = methodResults.First().Invoke(null, new object[] { this });

                PropertyInfo value = typeof(T).GetProperty("Value");
                string errorFormat = "Invalid Enum table specified, no {0} column was found: {1}";
                if (value == null)
                {
                    ExceptionHelper.Throw<InvalidOperationException>(errorFormat, "Value", typeof(T).Name);
                }
                PropertyInfo status = typeof(T).GetProperty("Status");
                if (status == null)
                {
                    ExceptionHelper.Throw<InvalidOperationException>(errorFormat, "Status", typeof(T).Name);
                }

                value.SetValue(insertInstance, enumValue, null);
                status.SetValue(insertInstance, enumString, null);

                object result = typeof(T).GetMethod("Insert").Invoke(insertInstance, null);
                if (result.Equals(-1))
                {
                    throw ((DaoObject)insertInstance).LastException;
                }
            }
        }

        public void FillStatusDictionary<T, StatusEnum>(Dictionary<StatusEnum, T> dictionary) where T : DaoObject, new()
        {
            FieldInfo[] fields = typeof(StatusEnum).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                object enumValue = field.GetRawConstantValue();
                string enumString = field.Name;
                DaoSearchFilter filter = new DaoSearchFilter();
                filter.AddParameter("Value", enumValue);
                filter.AddParameter("Status", enumString);

                T daoInstance = SelectOneWhere<T>(filter);
                dictionary.Add((StatusEnum)enumValue, daoInstance);
            }
        }

        public int RetryCount { get; set; }

        public DataTable GetDataTableFromSql(string sqlStatement)
        {
            return GetDataTableFromSql(sqlStatement, new DbParameter[] { });
        }

        public DataTable GetDataTableFromStoredProcedure(string sqlStatement, params DbParameter[] dbParameters)
        {
            return GetDataTableFromSql(sqlStatement, CommandType.StoredProcedure, dbParameters);
        }

        public DataTable GetDataTableFromSql(string sqlStatement, params DbParameter[] dbParameters)
        {
            return GetDataTableFromSql(sqlStatement, CommandType.Text, dbParameters);
        }

        public virtual DataTable GetDataTableFromSql(string sqlStatement, CommandType commandType, params DbParameter[] dbParamaters)
        {
            return this.GetDataTableFromSql(sqlStatement, commandType, RetryCount, dbParamaters);
        }

        public virtual DataTable GetDataTableFromSql(string sqlStatement, CommandType commandType, int retryCount, params DbParameter[] dbParamaters)
        {
            DataTable returnTable = new DataTable();
            DbCommand command = null;
            DbConnection conn = GetDbConnection();
            try
            {

                using (conn)
                {
                    DbDataAdapter dataAdapter = providerFactory.CreateDataAdapter();//SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    command = providerFactory.CreateCommand();//new SqlCommand(sqlStatement, conn);
                    command.Connection = conn;
                    command.CommandText = sqlStatement;
                    command.CommandType = commandType;
                    command.CommandTimeout = 10000;

                    foreach (DbParameter param in dbParamaters)
                    {
                        command.Parameters.Add(providerFactory.CopyParameter(param));
                    }

                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(returnTable);
                }
            }
            catch (Exception ex)
            {
                // This catch statement is intended specifically to address a 
                // bug with SqlServer 2000 where a dirty connection in the pool
                // will randomly cause the "Fill" method of the DataAdapter class
                // to throw an InvalidOperationException with a message of 
                // "internal connection fatal error".  Numerous Google searches
                // on the text of the message turned up no solution.  Though the post here,
                // http://forums.microsoft.com/msdn/ShowPost.aspx?PostID=2833401&SiteID=1, is
                // specifically about the WorkFlow engine it stated that setting "EnableRetries"
                // to true fixed the problem.  Hence, I implemented a "Retry" mechanism which
                // seems to have solved the problem.
                // 
                ReleaseConnection(conn);
                if (retryCount > 0)
                {
                    int newRetryCount = retryCount - 1;
                    return this.GetDataTableFromSql(sqlStatement, commandType, newRetryCount, dbParamaters);
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                ReleaseConnection(conn);
            }
            return returnTable;
        }

        public override bool Equals(object obj)
        {
            if(obj is DatabaseAgent)
            {
                return ((DatabaseAgent)obj).ConnectionString.Equals(this.ConnectionString);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.ConnectionString.GetHashCode();
        }

        #region // -- utility methods

        public bool UseUnicode
        {
            get;
            set;
        }

        internal DbParameter[] ConvertParameters(DbParameter[] input)
        {
            List<DbParameter> values = new List<DbParameter>();
            foreach (DbParameter parameter in input)
            {
                values.Add(this.CreateParameter(parameter.ParameterName, parameter.Value, parameter.Size));
            }
            return values.ToArray();
        }

        public DbParameter CreateParameter(string name, object value)
        {
            return this.CreateParameter(name, value, 2000);
        }

        public DbParameter CreateParameter(string name, object value, int size)
        {
            if (!name.StartsWith("@"))
                name = "@" + name;

            DbParameter param = providerFactory.CreateParameter();//new SqlParameter();
            param.ParameterName = name;
            param.Value = value;
            if (size > 0)
            {
                if (this.UseUnicode && size == 8000  && value is string)
                {
                    size = 4000;
                }
                else if (size == 8000 && value is string)
                {
                    param.DbType = System.Data.DbType.AnsiString;
                    param.Size = size;
                }
                else
                {
                    param.Size = size;
                }
            }

            return param;
        }
        
        private DbConnection GetDbConnection()
        {
            return this.GetDbConnection(this.maxConnections);
        }

        private DbConnection GetDbConnection(int max)
        {
            if (connections.Count >= max)
            {
                resetEvent.WaitOne();
            }

            DbConnection conn = providerFactory.CreateConnection();
            connections.Add(conn);
            return conn;
        }

        object connectionLock = new object();
        private void ReleaseConnection(DbConnection conn)
        {
            try
            {
                lock (connectionLock)
                {
                    if (connections.Contains(conn))
                    {
                        connections.Remove(conn);
                    }
                }
            
                //SqlConnection.ClearPool(conn);
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            catch //(Exception ex)
            {
                // do nothing
            }

            resetEvent.Set();
        }

        public static void FromDataRow(object target, DataRow row)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            foreach (DataColumn column in row.Table.Columns)
            {
                map.Add(column.ColumnName, column.ColumnName);
            }

            FromDataRow(target, row, map);
        }

        public static void FromDataRow(object target, DataRow row, Dictionary<string, string> propertyToColumnMap)
        {
            Type t = target.GetType();

            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                string colName = prop.Name;
                if (propertyToColumnMap != null)
                {
                    if (!propertyToColumnMap.ContainsKey(prop.Name) && !propertyToColumnMap.ContainsKey(prop.Name.ToUpperInvariant()))
                        continue;

                    colName = propertyToColumnMap.ContainsKey(prop.Name) ? propertyToColumnMap[prop.Name] : propertyToColumnMap[prop.Name.ToUpperInvariant()];
                }

                // verify column is in row
                bool columnNameExists = false;
                foreach (DataColumn column in row.Table.Columns)
                {
                    if (column.ColumnName.ToUpperInvariant().Equals(colName.ToUpperInvariant()))
                    {
                        columnNameExists = true;
                        break;
                    }
                }

                if (!columnNameExists)
                    continue;

                object rowVal = row[colName];
                if (rowVal is long && prop.PropertyType == typeof(int))
                    rowVal = System.Convert.ToInt32(rowVal);

                if (rowVal is long && prop.PropertyType == typeof(Boolean))
                    rowVal = System.Convert.ToBoolean(rowVal);

                if (rowVal is string && prop.PropertyType == typeof(Boolean))
                    rowVal = rowVal.ToString().Equals("1");

                if (rowVal is string && prop.PropertyType == typeof(Guid))
                {
                    if (string.IsNullOrEmpty(rowVal.ToString()))
                        rowVal = Guid.Empty;
                    else
                        rowVal = new Guid(rowVal.ToString());
                }

                if (prop.PropertyType == typeof(string))
                    rowVal = rowVal.ToString();

                if (prop.PropertyType == typeof(DateTime) && (rowVal is string))
                    rowVal = JulianDate.ToDateTime(double.Parse(rowVal.ToString()));

                if (prop.PropertyType.IsEnum)
                    rowVal = Enum.Parse(prop.PropertyType, (string)rowVal);

                if (prop.CanWrite &&
                    rowVal != null &&
                    rowVal != DBNull.Value)
                {
                    prop.SetValue(target, rowVal, null);//Convert.ToString(row[prop.Name]), null);
                }

            }
        }

        public static DaoSearchFilter GetSearchFilter<T>() where T : DaoObject, new()
        {
            Type searchFilterType = typeof(T).GetNestedType("SearchFilter");
            return (DaoSearchFilter)searchFilterType.GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        public static DaoColumn GetUniquenessColumn<T>() where T : DaoObject, new()
        {
            return GetUniquenessColumn(typeof(T));
        }

        /// <summary>
        /// Gets the DaoColumn attribute (either DaoPrimaryKeyColumn or DaoIdColumn) addorning the
        /// property that represents the unique identifier column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected internal static DaoColumn GetUniquenessColumn(Type type)
        {
            DaoColumn attribute;
            DaoIdColumn idColumn;
            PropertyInfo idProp = type.GetFirstProperyWithAttributeOfType<DaoIdColumn>(out idColumn);
            if (idProp == null)
            {
                DaoPrimaryKeyColumn primKey;
                idProp = type.GetFirstProperyWithAttributeOfType<DaoPrimaryKeyColumn>(out primKey);
                if (idProp == null)
                {
                    throw new IdColumnNotDefinedException(string.Format("The specified DaoType {0} has no id column or primary key defined.", type.Name));
                }
                else
                {
                    attribute = primKey;
                }
            }
            else
            {
                attribute = idColumn;
            }
            return attribute;
        }

        public static string EnumToColumnName<T>(Enum fieldEnum) where T : DaoObject, new()
        {
            T proxy = new T();
            return proxy.GetPropertyToColumnMap()[fieldEnum.ToString()];
        }
        #endregion // -- end utility methods
    }
}
