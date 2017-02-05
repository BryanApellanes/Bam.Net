/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

namespace Bam.Net.Data.OleDb
{
	public class OleDbDatabase: Database, IHasConnectionStringResolver
	{
        public const string DefaultProvider = "Microsoft.ACE.OLEDB.12.0";
        public OleDbDatabase()
        {
            ConnectionStringResolver = DefaultConnectionStringResolver.Instance;
            Register();
        }
        public OleDbDatabase(string dataSource) 
            : this(DefaultProvider, dataSource)
        { }

		public OleDbDatabase(string provider, string dataSource)
			: this(provider, dataSource, dataSource)
		{ }

		public OleDbDatabase(string provider, string dataSource, string connectionName)
			: base()
		{
            if (!Path.GetExtension(dataSource).Equals(".accdb"))
            {
                dataSource = $"{dataSource}.accdb";
            }
            FileInfo db = new FileInfo(dataSource);
            if (!db.Exists)
            {                
                CreateAccessFile(db.FullName);
            }
            ConnectionStringResolver = new OleDbConnectionStringResolver(provider, dataSource);
            ConnectionName = connectionName;
            Register();
		}

        public OleDbDatabase(OleDbConnectionStringResolver connectionStringResolver)
        {
            ConnectionName = connectionStringResolver.ConnectionName;
            ConnectionStringResolver = connectionStringResolver;
            Register();
        }

        private void Register()
        {
            ServiceProvider = new Incubator();
            ServiceProvider.Set<DbProviderFactory>(OleDbFactory.Instance);
            OleDbRegistrar.Register(this);
            Infos.Add(new DatabaseInfo(this));
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
					_connectionString = ConnectionStringResolver?.Resolve(ConnectionName)?.ConnectionString;
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
            List<DbParameter> parameterList = new List<DbParameter>(dbParameters);
            foreach (string sql in commands)
            {
                List<DbParameter> parameters = TakeParameters(parameterList, sql);
                base.ExecuteSql(sql, commandType, parameters.ToArray());
            }
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
                    List<DbParameter> parameters = TakeParameters(parameterList, sql);
                    if (sql.StartsWith("INSERT", StringComparison.InvariantCultureIgnoreCase) ||
                        sql.StartsWith("UPDATE", StringComparison.InvariantCultureIgnoreCase))
                    {
                        ExecuteSql(sql, parameters.ToArray(), conn, false);
                    }
                    else
                    {
                        DataTable next = GetDataTableFromReader(sql, commandType, parameters.ToArray(), conn, false);
                        result.Merge(next);
                    }
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

        private static List<DbParameter> TakeParameters(List<DbParameter> parameterList, string sql)
        {
            int paramCount = sql.Count(c => c.Equals('@'));
            int atAtCount = Regex.Matches(sql, "@@").Count;
            paramCount = paramCount - (atAtCount * 2);
            List<DbParameter> parameters = parameterList.GetRange(0, paramCount);
            parameterList.RemoveRange(0, paramCount);
            return parameters;
        }

        private void CreateAccessFile(string fileName)
        {
            typeof(OleDbDatabase).Assembly.WriteResource(typeof(OleDbDatabase), "empty.accdb", fileName);
        }
	}
}
