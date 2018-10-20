/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using System.IO;
using System.Data;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Reflection;

namespace Bam.Net.Data.Oracle
{
    public class OracleDatabase : Database, IHasConnectionStringResolver
    {
        public OracleDatabase()
            : base()
        {
            ConnectionStringResolver = new NullConnectionStringResolver(this);
            ConnectionName = "Oracle";
            Register();
        }
        /// <summary>
        /// Instantiate a new OracleDatabase instance using the specified serverName
        /// connectionName and credentials
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="connectionName"></param>
        public OracleDatabase(string serverName, string connectionName, OracleCredentials creds = null)
            : base()
        {
            ConnectionStringResolver = new OracleConnectionStringResolver(serverName, creds);
            ConnectionName = connectionName;
            Register();
        }

        /// <summary>
        /// Instantiate a new OracleDatabase instance using the specified serverName and
        /// credentials
        /// </summary>
        public OracleDatabase(string serverName, OracleCredentials creds = null)
            : this(serverName, "Oracle", creds)
        { }

        /// <summary>
        /// Instantiate a new OracleDatabase instance using the specified serverName and
        /// credentials
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public OracleDatabase(string serverName, string userId, string password)
            : this(serverName, new OracleCredentials { UserId = userId, Password = password })
        { }

        public OracleDatabase(string connectionString, string connectionName = null)
            : base(connectionString, connectionName)
        {
        }

        public IConnectionStringResolver ConnectionStringResolver
        {
            get;
            set;
        }

        string _connectionString;
        public override string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = ConnectionStringResolver.Resolve(ConnectionName).ConnectionString;
                }

                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public override void ExecuteSql(string sqlStatement, System.Data.CommandType commandType, params DbParameter[] dbParameters)
        {
            string[] commands = sqlStatement.DelimitSplit("\r", "\n");
            foreach (string command in commands)
            {
                base.ExecuteSql(command, commandType, dbParameters);
            }
        }

        public override long? GetIdValue(Dao dao)
        {
            long? result = base.GetIdValue(dao);
            if (result == null)
            {
                string keyName = dao.KeyColumnName;
                decimal oracleId = (decimal)dao.DataRow[keyName];
                result = Convert.ToInt64(oracleId);
            }
            return result;
        }

        public override long? GetLongValue(string columnName, DataRow row)
        {
            object value = row[columnName];
            if (value is long || value is long?)
            {
                return (long?)value;
            }
            else if (value is decimal || value is decimal?)
            {
                decimal d = (decimal)value;
                return Convert.ToInt64(d);
            }
            return null;
        }

        protected internal override DbCommand BuildCommand(string sqlStatement, CommandType commandType, DbParameter[] dbParameters, DbProviderFactory providerFactory, DbConnection conn, DbTransaction tx = null)
        {
            OracleCommand command = (OracleCommand)base.BuildCommand(sqlStatement, commandType, dbParameters, providerFactory, conn, tx);
            command.BindByName = true;
            return command;
        }

        public override DataTable GetDataTable(string sqlStatement, CommandType commandType, params DbParameter[] dbParamaters)
        {
            DbProviderFactory providerFactory = ServiceProvider.Get<DbProviderFactory>();
            DbConnection conn = GetDbConnection();
            DataTable table = new DataTable();
            try
            {
                DbCommand command = BuildCommand(sqlStatement, commandType, dbParamaters, providerFactory, conn);
                FillTable(table, command);
            }
            finally
            {
                ReleaseConnection(conn);
            }

            return table;
        }

        public override DataSet GetDataSetFromSql<T>(string sqlStatement, CommandType commandType, bool releaseConnection, DbConnection conn, DbTransaction tx, params DbParameter[] dbParamaters)
        {
            DbProviderFactory providerFactory = ServiceProvider.Get<DbProviderFactory>();

            DataSet result = new DataSet(Dao.ConnectionName<T>().Or(8.RandomLetters()));
            List<DbParameter> parameterList = new List<DbParameter>(dbParamaters);
            try
            {
                foreach (string sql in sqlStatement.DelimitSplit("\r", "\n"))
                {
                    DataSet next = new DataSet(result.DataSetName);
                    int colonCount = sql.Count(c => c.Equals(':'));
                    List<DbParameter> parameters = parameterList.GetRange(0, colonCount);
                    parameterList.RemoveRange(0, colonCount);
                    DbCommand command = BuildCommand(sql, commandType, parameters.ToArray(), providerFactory, conn, tx);
                    FillDataSet(next, command);
                    result.Merge(next);
                }
            }
            finally
            {
                if (releaseConnection)
                {
                    ReleaseConnection(conn);
                }
            }

            return result;
        }
        public override Query<C, T> GetQuery<C, T>()
        {
            Query<C, T> q = base.GetQuery<C, T>();
            q.ColumnNameProvider = ColumnNameProvider;
            return q;
        }

        public override Query<C, T> GetQuery<C, T>(Delegate where)
        {
            Query<C, T> q = base.GetQuery<C, T>(where);
            q.ColumnNameProvider = ColumnNameProvider;
            return q;
        }

        public override Query<C, T> GetQuery<C, T>(Func<C, QueryFilter<C>> where, OrderBy<C> orderBy = null)
        {
            Query<C, T> q = base.GetQuery<C, T>(where, orderBy);
            q.ColumnNameProvider = ColumnNameProvider;
            return q;
        }

        public override Query<C, T> GetQuery<C, T>(WhereDelegate<C> where, OrderBy<C> orderBy = null)
        {
            Query<C, T> q = base.GetQuery<C, T>(where, orderBy);
            q.ColumnNameProvider = ColumnNameProvider;
            return q;
        }

        protected internal override Func<ColumnAttribute, string> ColumnNameProvider
        {
            get
            {
                return (c) => c.Name;
            }
        }

        protected internal override AssignValue GetAssignment(string keyColumn, object value, Func<string, string> columnNameformatter = null)
        {
            AssignValue result = base.GetAssignment(keyColumn, value, columnNameformatter);
            result.ParameterPrefix = ":";
            return result;
        }

        protected override void ReaderPropertySetter(object instance, string propertyName, object propertyValue)
        {
            PropertyInfo prop = instance.GetType().GetProperties().Where(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            ReflectionExtensions.SetProperty(instance, prop, propertyValue);
        }

        private void Register()
        {
            ServiceProvider = new Incubator();
            ServiceProvider.Set<DbProviderFactory>(OracleClientFactory.Instance);
            OracleRegistrar.Register(this);
            Infos.Add(new DatabaseInfo(this));
        }
    }
}
