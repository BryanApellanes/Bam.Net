/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace Bam.Net.Data
{
    public partial class Database: Loggable
    {
        List<DbConnection> _connections;
        public Database()
        {
			_resetEvent = new AutoResetEvent(false);
			_connections = new List<DbConnection>();
			_schemaNames = new HashSet<string>();
            ServiceProvider = Incubator.Default;           
            MaxConnections = 20;
            ConnectionManager = new DefaultDbConnectionManager(this);
        }

        public Database(Incubator serviceProvider, string connectionString, string connectionName = null)
            : this()
        {
            ServiceProvider = serviceProvider;
            ConnectionString = connectionString;
            ConnectionName = connectionName;
			ParameterPrefix = "@";
            if (!string.IsNullOrEmpty(ConnectionName))
            {
                Db.For(ConnectionName, this);
            }
        }

        public Database(string connectionString, string connectionName = null)
            : this(new Incubator(), connectionString, connectionName)
        {
        }

        static HashSet<DatabaseInfo> _infos;
        static object _infosLock = new object();
        public static HashSet<DatabaseInfo> Infos
        {
            get
            {
                return _infosLock.DoubleCheckLock(ref _infos, () => new HashSet<DatabaseInfo>());
            }
        }

        public static ColumnNameListProvider Star
        {
            get
            {
                return (type, db) => "*";
            }
        }

        public static ColumnNameListProvider ColumnNames
        {
            get
            {
                return (type, db) => ColumnAttribute.GetColumns(type).ToDelimited(c => db.ColumnNameProvider(c));
            }
        }

        public ColumnNameListProvider ColumnNameListProvider
        {
            get
            {
                return SelectStar ? Star : ColumnNames;
            }
        }
        /// <summary>
        /// When true will use Star instead of ColumnNames when executing Query instances
        /// </summary>
        public bool SelectStar
        {
            get; set;
        }

        public DaoTransaction BeginTransaction()
        {
            return Db.BeginTransaction(this);
        }

        public IDbConnectionManager ConnectionManager
        {
            get;
            set;
        }

        public int MaxConnections { get; set; }

        public Incubator ServiceProvider { get; set; }

		public string ParameterPrefix { get; set; }
		/// <summary>
		/// Used to locate the connection string in the 
		/// configuration file as well as uniquely identify
		/// types that are associated with a specific 
		/// schema.  
		/// </summary>
        public string ConnectionName { get; set; }

		protected HashSet<string> _schemaNames;
		public string[] SchemaNames
		{
			get
			{
				return _schemaNames.ToArray();
			}
		}

        public virtual string Name
        {
            get
            {
                DbConnectionStringBuilder cb = CreateConnectionStringBuilder();                
                cb.ConnectionString = this.ConnectionString;

                string databaseName = cb["Initial Catalog"] as string;

                if (string.IsNullOrEmpty(databaseName))
                {
                    databaseName = cb["Database"] as string;
                }

                if (string.IsNullOrEmpty(databaseName))
                {
                    databaseName = cb["Data Source"] as string;
                }

                if (string.IsNullOrEmpty(databaseName))
                {
                    throw new InvalidOperationException("Unable to determine database name from the connection string");
                }
                return databaseName;
            }
        }

        public virtual void Hydrate(Dao dao)
        {
            GetHydrator()?.Hydrate(dao, this);
        }

        public virtual IHydrator GetHydrator()
        {
            return ServiceProvider?.Get<IHydrator>() ?? Hydrator.DefaultHydrator;
        }

        public Dictionary<EnumType, DaoType> FillEnumDictionary<EnumType, DaoType>(Dictionary<EnumType, DaoType> dictionary, string nameColumn) where DaoType : Dao, new()
        {
            QuerySet query = ExecuteQuery<DaoType>();

            QueryResult result = ((QueryResult)query.Results[0]);
            if (result.DataTable.Rows.Count == 0)
            {
                InitEnumValues<EnumType, DaoType>("Value", nameColumn);
                query = ExecuteQuery<DaoType>();
                result = ((QueryResult)query.Results[0]);
            }

            foreach (DataRow row in result.DataTable.Rows)
            {
                EnumType enumVal = (EnumType)Enum.Parse(typeof(EnumType), (string)row[nameColumn]);
                DaoType inst = new DaoType
                {
                    DataRow = row
                };
                dictionary.AddMissing(enumVal, inst);
            }

            return dictionary;
        }

		public virtual Query<C, T> GetQuery<C, T>() 
			where C : IQueryFilter, IFilterToken, new()
			where T: Dao, new()
		{
			return new Query<C,T>();
		}

		public virtual Query<C, T> GetQuery<C, T>(WhereDelegate<C> where, OrderBy<C> orderBy = null)
			where C : IQueryFilter, IFilterToken, new()
			where T : Dao, new()
		{
			return new Query<C, T>(where, orderBy, this);
		}

		public virtual Query<C, T> GetQuery<C, T>(Func<C, QueryFilter<C>> where, OrderBy<C> orderBy = null)
			where C : IQueryFilter, IFilterToken, new()
			where T : Dao, new()
		{
			return new Query<C, T>(where, orderBy, this);
		}

		public virtual Query<C, T> GetQuery<C, T>(Delegate where)
			where C : IQueryFilter, IFilterToken, new()
			where T : Dao, new()
		{
			return new Query<C, T>(where, this);
		}

        public SchemaWriter GetSchemaWriter()
        {
            return ServiceProvider.Get<SchemaWriter>();
        }

        public SqlStringBuilder Sql()
        {
            return GetSqlStringBuilder();
        }

        public virtual SqlStringBuilder GetSqlStringBuilder()
        {
            SqlStringBuilder sql = ServiceProvider.Get<SqlStringBuilder>();
            sql.SelectStar = SelectStar;
            return sql;
        }

        public virtual QuerySet GetQuerySet()
        {
            QuerySet sql = ServiceProvider.Get<QuerySet>();
            sql.Database = this;
            sql.SelectStar = SelectStar;
            return sql;
        }

        public virtual IDataTypeTranslator GetDataTypeTranslator()
        {
            if (!ServiceProvider.TryGet<IDataTypeTranslator>(out IDataTypeTranslator dataTypeTranslator))
            {
                return new DataTypeTranslator();
            }
            return dataTypeTranslator;
        }

        public DbParameter[] GetParameters(SqlStringBuilder sqlStrinbuilder)
        {
            IParameterBuilder paramBuilder = GetService<IParameterBuilder>();
            Args.ThrowIfNull(paramBuilder, "IParameterBuilder");
            return paramBuilder.GetParameters(sqlStrinbuilder);
        }

        /// <summary>
        /// Execute the specified SqlStringBuilder using the 
        /// specified generic type to determine which database
        /// to use.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        public virtual void ExecuteSql<T>(SqlStringBuilder builder) where T : Dao 
        {
            ExecuteSql(builder, ServiceProvider.Get<IParameterBuilder>());
        }

        public virtual void ExecuteSql(SqlStringBuilder builder)
        {
            ExecuteSql(builder, ServiceProvider.Get<IParameterBuilder>());
        }

        public virtual void ExecuteSql(SqlStringBuilder builder, IParameterBuilder parameterBuilder)
        {
            ExecuteSql(builder, CommandType.Text, parameterBuilder.GetParameters(builder));
        }

        public virtual void ExecuteStoredProcedure(string sprocName, params DbParameter[] dbParameters)
        {
            ExecuteSql(sprocName, CommandType.StoredProcedure, dbParameters);
        }

        public virtual void ExecutSql(string sqlStatement, object dbParameters)
        {
            ExecuteSql(sqlStatement, dbParameters.ToDbParameters(this).ToArray());
        }

        public virtual void ExecuteSql(string sqlStatement, params DbParameter[] dbParameters)
        {
            ExecuteSql(sqlStatement, CommandType.Text, dbParameters);
        }

        public virtual void ExecuteSql(string sqlStatement, CommandType commandType, params DbParameter[] dbParameters)
        {
            DbConnection conn = GetOpenDbConnection();
            ExecuteSql(sqlStatement, commandType, dbParameters, conn);
        }

        public virtual void ExecuteSql(string sqlStatement, DbParameter[] dbParameters, DbConnection conn = null, bool releaseConnection = true)
        {
            ExecuteSql(sqlStatement, CommandType.Text, dbParameters, conn ?? GetOpenDbConnection(), releaseConnection);
        }

        public virtual void ExecuteSql(string sqlStatement, CommandType commandType, DbParameter[] dbParameters, DbConnection conn, bool releaseConnection = true)
        {
            ExecuteSql(sqlStatement, commandType, dbParameters, conn, (ex) => { }, releaseConnection);
        }

        public event EventHandler CommandExecuted;
        public event EventHandler CommandException;
        public virtual void ExecuteSql(string sqlStatement, CommandType commandType, DbParameter[] dbParameters, DbConnection conn, Action<Exception> exceptionHandler, bool releaseConnection = true)
        {
            try
            {
                DbCommand cmd = PrepareCommand(sqlStatement, commandType, dbParameters, conn);
                cmd.ExecuteNonQuery();
                FireEvent(CommandExecuted, new DatabaseExecutionEventArgs { Database = this, Command = cmd });
            }
            catch (Exception ex)
            {
                exceptionHandler(ex);
                FireEvent(CommandException, new DatabaseExecutionEventArgs { Database = this, Exception = ex, Message = ex.Message });
            }
            finally
            {
                if (releaseConnection)
                {
                    ReleaseConnection(conn);
                }
            }
        }

        public virtual IEnumerable<T> ExecuteReader<T>(SqlStringBuilder sqlStatement, Action<DbDataReader> onReaderExecuted = null) where T : class, new()
        {
            return ExecuteReader<T>(sqlStatement.ToString(), GetParameters(sqlStatement), null, true, onReaderExecuted);
        }

        public virtual IEnumerable<T> ExecuteReader<T>(string sqlStatement, object dbParameters, Action<DbDataReader> onReaderExecuted = null) where T : class, new()
        {
            return ExecuteReader<T>(sqlStatement, dbParameters.ToDbParameters(this).ToArray(), null, true, onReaderExecuted);
        }

        public virtual IEnumerable<T> ExecuteReader<T>(string sqlStatement, DbParameter[] dbParameters, out DbConnection conn, Action<DbDataReader> onReaderExecuted = null) where T : class, new()
        {
            conn = GetOpenDbConnection();
            return ExecuteReader<T>(sqlStatement, dbParameters, conn, false, onReaderExecuted);
        }

        public virtual IEnumerable<T> ExecuteReader<T>(string sqlStatement, DbParameter[] dbParameters, DbConnection conn = null, bool closeConnection = true, Action<DbDataReader> onReaderExecuted = null) where T : class, new()
        {
            return ExecuteReader<T>(sqlStatement, CommandType.Text, dbParameters, conn ?? GetOpenDbConnection(), closeConnection, onReaderExecuted);
        }
        
        public virtual IEnumerable<T> ExecuteReader<T>(string sqlStatement, params DbParameter[] dbParameters) where T : class, new()
        {
            return ExecuteReader<T>(sqlStatement, CommandType.Text, dbParameters, GetOpenDbConnection());
        }

        public virtual IEnumerable<T> ExecuteReader<T>(string sqlStatement, CommandType commandType, DbParameter[] dbParameters, DbConnection conn, bool closeConnection = true, Action<DbDataReader> onReaderExecuted = null) where T: class, new()
        {
            DbDataReader reader = ExecuteReader(sqlStatement, commandType, dbParameters, conn);
            onReaderExecuted = onReaderExecuted ?? ((dr) => { });
            if (reader.HasRows)
            {
                List<string> columnNames = GetColumnNames(reader);
                while (reader.Read())
                {
                    T next = new T();
                    foreach(string columnName in columnNames)
                    {
                        ReaderPropertySetter(next, columnName, reader[columnName]);
                    }
                    yield return next;
                }
            }
            if (closeConnection)
            {
                ReleaseConnection(conn);
            }
            onReaderExecuted(reader);
            yield break;
        }

        protected virtual void ReaderPropertySetter(object instance, string propertyName, object propertyValue)
        {
            instance.Property(propertyName, propertyValue);
        }


        public virtual DbDataReader ExecuteReader(SqlStringBuilder sqlStatement)
        {
            return ExecuteReader(sqlStatement.ToString(), GetParameters(sqlStatement));
        }

        public virtual DbDataReader ExecuteReader(string sqlStatement, object dbParameters)
        {
            return ExecuteReader(sqlStatement, dbParameters, out DbConnection ignore);
        }

        public virtual DbDataReader ExecuteReader(string sqlStatement, object dbParameters, out DbConnection conn)
        {
            return ExecuteReader(sqlStatement, dbParameters.ToDbParameters(this).ToArray(), out conn);
        }

        public virtual DbDataReader ExecuteReader(string sqlStatement, DbParameter[] dbParameters, out DbConnection conn)
        {
            conn = GetOpenDbConnection();
            return ExecuteReader(sqlStatement, CommandType.Text, dbParameters, conn);
        }

        public virtual DbDataReader ExecuteReader(string sqlStatement, DbParameter[] dbParameters, DbConnection conn = null)
        {
            return ExecuteReader(sqlStatement, CommandType.Text, dbParameters, conn ?? GetOpenDbConnection());
        }

        public event EventHandler ReaderExecuted;
        public event EventHandler ReaderException;
        public virtual DbDataReader ExecuteReader(string sqlStatement, CommandType commandType, DbParameter[] dbParameters, DbConnection conn)
        {
            DbDataReader reader = null;
            try
            {
                DbCommand cmd = PrepareCommand(sqlStatement, commandType, dbParameters, conn);
                reader = cmd.ExecuteReader();
                FireEvent(ReaderExecuted, new DatabaseExecutionEventArgs { Database = this, DataReader = reader });
            }
            catch (Exception ex)
            {
                FireEvent(ReaderException, new DatabaseExecutionEventArgs { Database = this, Exception = ex, Message = ex.Message });
            }
            return reader;
        }

        // -- start datatable readers
        public virtual DataTable GetDataTableFromReader(SqlStringBuilder sqlStatement)
        {
            return GetDataTableFromReader(sqlStatement.ToString(), GetParameters(sqlStatement));
        }
        public virtual DataTable GetDataTableFromReader(string sqlStatement, object dbParameters)
        {
            DbConnection ignore;
            return GetDataTableFromReader(sqlStatement, dbParameters, out ignore);
        }
        public virtual DataTable GetDataTableFromReader(string sqlStatement, object dbParameters, out DbConnection conn)
        {
            return GetDataTableFromReader(sqlStatement, dbParameters.ToDbParameters(this).ToArray(), out conn);
        }

        public virtual DataTable GetDataTableFromReader(string sqlStatement, DbParameter[] dbParameters, out DbConnection conn)
        {
            conn = GetOpenDbConnection();
            return GetDataTableFromReader(sqlStatement, CommandType.Text, dbParameters, conn, false);
        }

        public virtual DataTable GetDataTableFromReader(string sqlStatement, DbParameter[] dbParameters, DbConnection conn = null)
        {
            return GetDataTableFromReader(sqlStatement, CommandType.Text, dbParameters, conn ?? GetOpenDbConnection(), false);
        }

        public virtual DataTable GetDataTableFromReader(string sqlStatement, CommandType commandType, DbParameter[] dbParameters, DbConnection conn, bool closeConnection = true)
        {
            DbDataReader reader = ExecuteReader(sqlStatement, commandType, dbParameters, conn);
            DataTable table = new DataTable(8.RandomLetters());
            if (reader.HasRows)
            {
                table = table ?? new DataTable(8.RandomLetters());
                List<string> columnNames = GetColumnNames(reader);
                columnNames.Each(new { Table = table }, (ctx, cn) =>
                {
                    ctx.Table.Columns.Add(new DataColumn(cn));
                });
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    columnNames.Each(new { Row = row, Reader = reader }, (ctx, cn) =>
                    {
                        ctx.Row[cn] = ctx.Reader[cn];
                    });
                    table.Rows.Add(row);
                }
            }
            if (closeConnection)
            {
                ReleaseConnection(conn);
            }
            return table;
        }

        public virtual IEnumerable<DataRow> GetDataRowsFromReader(SqlStringBuilder sqlStatement)
        {
            DbConnection ignore;
            return GetDataRowsFromReader(sqlStatement.ToString(), GetParameters(sqlStatement), out ignore);
        }

        public virtual IEnumerable<DataRow> GetDataRowsFromReader(SqlStringBuilder sqlStatement, out DbConnection conn)
        {
            return GetDataRowsFromReader(sqlStatement.ToString(), GetParameters(sqlStatement), out conn);
        }

        public virtual IEnumerable<DataRow> GetDataRowsFromReader(string sqlStatement, DbParameter[] dbParameters, out DbConnection conn)
        {
            conn = GetOpenDbConnection();
            return GetDataRowsFromReader(sqlStatement, dbParameters, conn);
        }

        public virtual IEnumerable<DataRow> GetDataRowsFromReader(string sqlStatement, DbParameter[] dbParameters, DbConnection conn)
        {
            return GetDataRowsFromReader(sqlStatement, CommandType.Text, dbParameters, conn);
        }

        public virtual IEnumerable<DataRow> GetDataRowsFromReader(string sqlStatement, CommandType commandType, DbParameter[] dbParameters, DbConnection conn)
        {
            DbDataReader reader = ExecuteReader(sqlStatement, commandType, dbParameters, conn);
            return GetDataRowsFromReader(reader);
        }

        protected virtual IEnumerable<DataRow> GetDataRowsFromReader(DbDataReader reader, DataTable table = null)
        {
            if (reader.HasRows)
            {
                table = table ?? new DataTable(8.RandomLetters());
                List<string> columnNames = GetColumnNames(reader);
                columnNames.Each(new { Table = table }, (ctx, cn) =>
                {
                    ctx.Table.Columns.Add(new DataColumn(cn));
                });
                while (reader.Read())
                {
                    DataRow row = table.NewRow();                    
                    columnNames.Each(new { Row = row, Reader = reader }, (ctx, cn) =>
                    {
                        ctx.Row[cn] = ctx.Reader[cn];
                    });
                    table.Rows.Add(row);
                    yield return row;
                }
            }
            yield break;
        }
        // -- end datatable readers

        public virtual T QuerySingle<T>(SqlStringBuilder sql)
        {
            return QuerySingle<T>(sql, GetService<IParameterBuilder>().GetParameters(sql));
        }

        public virtual T QuerySingle<T>(string singleValueQuery, object dynamicParamters)
        {
            return QuerySingle<T>(singleValueQuery, dynamicParamters.ToDbParameters(this).ToArray());
        }

        public virtual T QuerySingle<T>(string singleValueQuery, params DbParameter[] dbParameters)
        {
            DataRow row = GetFirstRow(singleValueQuery, dbParameters);
            if(row.Table.Columns.Count > 0 && row[0] != DBNull.Value)
            {                
                return (T)row[0];
            }
            return default(T);
        }

        public virtual IEnumerable<T> QuerySingleColumn<T>(string singleColumnQuery, object dynamicParameters)
        {
            return QuerySingleColumn<T>(singleColumnQuery, dynamicParameters.ToDbParameters(this).ToArray());
        }
        /// <summary>
        /// Execute a query that returns a single column of results casting
        /// each to the specified generic type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="singleColumnQuery"></param>
        /// <param name="dbParameters"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> QuerySingleColumn<T>(string singleColumnQuery, params DbParameter[] dbParameters)
        {
            return Query<T>(singleColumnQuery, (row) => (T)row[0], dbParameters);
        }

        /// <summary>
        /// Execute the specified sqlQuery and return results as an Enumerable of
        /// dynamic object instances.  Property access can be done using column names 
        /// directly.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="dynamicDbParameters"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> Query(string sqlQuery, object dynamicDbParameters, string typeName = null)
        {
            DbParameter[] dbParameters = dynamicDbParameters.ToDbParameters(this).ToArray();            
            return Query(sqlQuery, dbParameters, typeName);
        }

        public IEnumerable<dynamic> Query(string sqlQuery, Dictionary<string, object> dictDbParameters, string typeName = null)
        {
            DbParameter[] dbParameters = dictDbParameters.ToDbParameters(this).ToArray();
            return Query(sqlQuery, dbParameters, typeName);
        }

        public IEnumerable<T> Query<T>(string sqlQuery, object dynamicDbParameters)
        {
            return Query<T>(sqlQuery, dynamicDbParameters.ToDbParameters(this).ToArray());
        }
        public IEnumerable<T> Query<T>(string sqlQuery, Dictionary<string, object> dbParameters)
        {
            return Query<T>(sqlQuery, dbParameters.ToDbParameters(this).ToArray());
        }
        public IEnumerable<T> Query<T>(string sqlQuery, params DbParameter[] dbParameters)
        {
            return Query<T>(sqlQuery, (row) => row.ToInstanceOf<T>(), dbParameters);
        }

        public IEnumerable<T> Query<T>(string sqlQuery, Func<DataRow, T> rowProcessor, params DbParameter[] dbParameters)
        {
            DataTable table = GetDataTable(sqlQuery, dbParameters);
            foreach(DataRow row in table.Rows)
            {
                yield return rowProcessor(row);
            }
        }

		public virtual DataRow GetFirstRow(string sqlStatement, CommandType commandType, params DbParameter[] dbParameters)
		{
			return GetDataTable(sqlStatement, commandType, dbParameters).Rows[0];
		}

        public virtual DataRow GetFirstRow(string sqlStatement, Dictionary<string, object> dbParameters)
        {
            return GetFirstRow(sqlStatement, dbParameters.ToDbParameters(this).ToArray());
        }

        public virtual DataRow GetFirstRow(string sqlStatement, params DbParameter[] dbParameters)
        {
            DataTable table = GetDataTable(sqlStatement, CommandType.Text, dbParameters);
            if(table.Rows.Count > 0)
            {
                return table.Rows[0];
            }
            return table.NewRow();
        }

        public virtual DataTable GetDataTable(string sqlStatement, object dynamicParameters)
        {
            return GetDataTable(sqlStatement, dynamicParameters.ToDbParameters(this).ToArray());
        }

        public virtual DataTable GetDataTable(string sqlStatement, Dictionary<string, object> parameters)
        {
            return GetDataTable(sqlStatement, parameters.ToDbParameters(this).ToArray());
        }

        public virtual DataTable GetDataTable(string sqlStatement, params DbParameter[] dbParameters)
        {
            return GetDataTable(sqlStatement, CommandType.Text, dbParameters);
        }

        public virtual DataTable GetDataTable(string sqlStatement, CommandType commandType, params DbParameter[] dbParameters)
        {
            DbProviderFactory providerFactory = ServiceProvider.Get<DbProviderFactory>();
            DbConnection conn = GetDbConnection();
            DataTable table = new DataTable();
            try
            {
				DbCommand command = BuildCommand(sqlStatement, commandType, dbParameters, providerFactory, conn);
                FillTable(table, command);
            }
            finally
            {
                ReleaseConnection(conn);
            }

            return table;
        }

		public T New<T>() where T : Dao, new()
		{
			return typeof(T).Construct<T>(this);
		}

		public T Save<T>(T dao) where T: Dao, new()
		{
			dao.Save(this);
			return dao;
		}

		public T GetService<T>()
		{
			return ServiceProvider.Get<T>();
		}

		public virtual long? GetIdValue(Dao dao)
		{
			string keyColumnName = dao.KeyColumnName;
			DataRow row = dao.DataRow;
			return GetLongValue(keyColumnName, row);
		}

		public virtual long? GetLongValue(string columnName, DataRow row)
		{
			object value = row[columnName];
			if (value != null && value != DBNull.Value)
			{
                return new long?(Convert.ToInt64(value));
			}

            return new long?();
		}

		public virtual long? GetIdValue<T>(DataRow row) where T: Dao, new()
		{
			return this.GetLongValue(Dao.GetKeyColumnName(typeof(T)), row);
		}

        public virtual DbConnection CreateConnection()
        {
            DbConnection conn = ServiceProvider.Get<DbProviderFactory>().CreateConnection();
            conn.ConnectionString = ConnectionString;
            return conn;
        }

        public virtual DbCommand CreateCommand()
        {
            return ServiceProvider.Get<DbProviderFactory>().CreateCommand();
        }

        public virtual T CreateConnectionStringBuilder<T>() where T : DbConnectionStringBuilder, new()
        {
            return (T)CreateConnectionStringBuilder();
        }

        public virtual DbParameter CreateParameter(string name, object value)
        {
            return ServiceProvider.Get<IParameterBuilder>().BuildParameter(name, value);
        }

        public virtual DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return ServiceProvider.Get<DbProviderFactory>().CreateConnectionStringBuilder();
        }

        public virtual DataSet GetDataSetFromSql(string sqlStatement, CommandType commandType, params DbParameter[] dbParamaters)
        {
            return GetDataSetFromSql(sqlStatement, commandType, true, dbParamaters);
        }

        public virtual DataSet GetDataSetFromSql(string sqlStatement, CommandType commandType, bool releaseConnection, params DbParameter[] dbParamaters)
        {
            DbConnection conn = GetDbConnection();
            return GetDataSetFromSql(sqlStatement, commandType, releaseConnection,  conn, dbParamaters);
        }

        public virtual DataSet GetDataSetFromSql(string sqlStatement, CommandType commandType, bool releaseConnection, DbConnection conn, params DbParameter[] dbParamaters)
        {
            return GetDataSetFromSql(sqlStatement, commandType, releaseConnection, conn, null, dbParamaters);
        }

		public virtual DataSet GetDataSetFromSql(string sqlStatement, CommandType commandType, bool releaseConnection, DbConnection conn, DbTransaction tx, params DbParameter[] dbParamaters)
		{
			return GetDataSetFromSql<object>(sqlStatement, commandType, releaseConnection, conn, tx, dbParamaters);
		}

        public virtual DataSet GetDataSetFromSql<T>(string sqlStatement, CommandType commandType, bool releaseConnection, DbConnection conn, DbTransaction tx, params DbParameter[] dbParamaters)
        {
            DbProviderFactory providerFactory = ServiceProvider.Get<DbProviderFactory>();

			DataSet set = new DataSet(Dao.ConnectionName<T>().Or(8.RandomLetters()));
            try
            {
                DbCommand command = BuildCommand(sqlStatement, commandType, dbParamaters, providerFactory, conn, tx);
                FillDataSet(set, command);
            }
            finally
            {
                if (releaseConnection)
                {
                    ReleaseConnection(conn);
                }
            }

            return set;
        }

		protected internal virtual AssignValue GetAssignment(string keyColumn, object value, Func<string, string> columnNameformatter = null)
		{
			return new AssignValue(keyColumn, value, columnNameformatter);
		}

        protected internal virtual DbCommand PrepareCommand(string sqlStatement, CommandType commandType, DbParameter[] dbParameters, DbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            DbProviderFactory providerFactory = ServiceProvider.Get<DbProviderFactory>();
            DbCommand cmd = BuildCommand(sqlStatement, commandType, dbParameters, providerFactory, conn);
            return cmd;
        }

        protected internal virtual DbCommand BuildCommand(string sqlStatement, CommandType commandType, DbParameter[] dbParameters, DbProviderFactory providerFactory, DbConnection conn, DbTransaction tx  =null)
        {
            DbCommand command = providerFactory.CreateCommand();
            command.Connection = conn;
            if (tx != null)
            {
                command.Transaction = tx;
            }
            command.CommandText = sqlStatement;
            command.CommandType = commandType;
            command.CommandTimeout = 10000;            
            command.Parameters.AddRange(dbParameters);
            return command;
        }

        protected void FillTable(DataTable table, DbCommand command)
        {
            DbDataAdapter adapter = ServiceProvider.Get<DbProviderFactory>().CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
        }

        protected void FillDataSet(DataSet dataSet, DbCommand command)
        {
            DbProviderFactory factory = ServiceProvider.Get<DbProviderFactory>();
            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(dataSet);
        }

        public virtual string ConnectionString
        {
            get;
            set;
        }

		public override bool Equals(object obj)
		{
			if (obj.GetType() == this.GetType() && 
				!string.IsNullOrEmpty(ConnectionString))
			{
				Database db = obj as Database;
				return db.ConnectionString.Equals(this.ConnectionString);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		public override int GetHashCode()
		{
			if(!string.IsNullOrEmpty(ConnectionString))
			{
				return ConnectionString.GetHashCode();
			}
			else
			{
				return base.GetHashCode();
			}
		}

        public DbConnection GetOpenDbConnection()
        {
            DbConnection conn = GetDbConnection();
            conn.Open();
            return conn;
        }

        public DbConnection GetDbConnection()
        {
            return ConnectionManager.GetDbConnection();
        }
        
        AutoResetEvent _resetEvent;
        protected readonly object connectionLock = new object();
        public virtual void ReleaseConnection(DbConnection conn)
        {
            ConnectionManager.ReleaseConnection(conn);
        }

        public EnsureSchemaStatus TryEnsureSchema(Assembly assembly, ILogger logger = null)
        {
            Type daoType = assembly.GetTypes().FirstOrDefault(d => d.IsSubclassOf(typeof(Dao)));
            if (daoType == null)
            {
                return EnsureSchemaStatus.Invalid;
            }
            else
            {
                return TryEnsureSchema(daoType, logger);
            }
        }

        public EnsureSchemaStatus TryEnsureSchema<T>(ILogger logger = null)
        {
            return TryEnsureSchema(typeof(T), logger);
        }

		public EnsureSchemaStatus TryEnsureSchema(Type type, ILogger logger = null)
		{
            return TryEnsureSchema(type, out Exception e, logger);
        }

		public EnsureSchemaStatus TryEnsureSchema(Type type, out Exception ex, ILogger logger = null)
		{
			return TryEnsureSchema(type, false, out ex, logger);
		}

		public virtual EnsureSchemaStatus TryEnsureSchema(Type type, bool force, out Exception ex, ILogger logger = null)
		{
			EnsureSchemaStatus result = EnsureSchemaStatus.Invalid;
			ex = null;
			try
			{
				string schemaName = Dao.RealConnectionName(type);
				if (!SchemaNames.Contains(schemaName) || force)
				{
					_schemaNames.Add(schemaName);
					SchemaWriter schema = ServiceProvider.Get<SchemaWriter>();
					schema.WriteSchemaScript(type);
					ExecuteSql(schema, ServiceProvider.Get<IParameterBuilder>());					
					result = EnsureSchemaStatus.Success;
				}
				else
				{
					result = EnsureSchemaStatus.AlreadyDone;
				}
			}
			catch (Exception e)
			{
				ex = e;
				result = EnsureSchemaStatus.Error;
				logger = logger ?? Log.Default;
				logger.AddEntry("Non fatal error occurred trying to write schema for type {0}: {1}", LogEventType.Warning, ex, type.Name, ex.Message);
			}
			return result;
		}

		Func<ColumnAttribute, string> _columnNameProvider;
		protected internal virtual Func<ColumnAttribute, string> ColumnNameProvider
		{
			get
			{
				if (_columnNameProvider == null)
				{
					_columnNameProvider = (c) =>
					{
						return string.Format("[{0}]", c.Name);
					};
				}

				return _columnNameProvider;
			}
			set
			{
				_columnNameProvider = value;
			}
		}

        protected List<DbConnection> Connections
        {
            get
            {
                return _connections.ToList();
            }
        }
		
		private QuerySet ExecuteQuery<T>() where T : Dao, new()
		{
			QuerySet query = new QuerySet();
			query.Select<T>();
			query.Execute(this);
			return query;
		}

		static readonly object initEnumLock = new object();
		private void InitEnumValues<EnumType, T>(string valueColumn, string nameColumn) where T : Dao, new()
		{
			FieldInfo[] fields = typeof(EnumType).GetFields(BindingFlags.Public | BindingFlags.Static);
			foreach (FieldInfo field in fields)
			{
				T entry = new T();
				entry.SetValue(valueColumn, field.GetRawConstantValue());
				entry.SetValue(nameColumn, field.Name);
				entry.Save();
			}
		}

        private static List<string> GetColumnNames(DbDataReader reader)
        {
            List<string> columnNames = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columnNames.Add(reader.GetName(i));
            }

            return columnNames;
        }
    }
}
