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
using System.Data.SqlClient;
using Bam.Net.Data;
using MySql.Data.MySqlClient;

namespace Bam.Net.Data.MySql
{
    public class MySqlDatabase : Database, IHasConnectionStringResolver
    {
        public MySqlDatabase()
        {
            ConnectionStringResolver = DefaultConnectionStringResolver.Instance;
            Register();
        }
        public MySqlDatabase(string serverName, string databaseName, MySqlCredentials credentials = null, bool ssl = true)
            : this(serverName, databaseName, databaseName, credentials, ssl)
        {
        }

        public MySqlDatabase(string serverName, string databaseName, string connectionName, MySqlCredentials credentials = null, bool ssl = true)
        {
            ColumnNameProvider = (c) => c.Name;
            ConnectionStringResolver = new MySqlConnectionStringResolver(serverName, databaseName, credentials) { Ssl = ssl };            
            ConnectionName = connectionName;
            Register();
        }

        public MySqlDatabase(string connectionString, string connectionName = null)
            : base(connectionString, connectionName)
        {
            Register();
        }

        private void Register()
        {
            ServiceProvider = new Incubator();
            ServiceProvider.Set<DbProviderFactory>(MySqlClientFactory.Instance);
            MySqlRegistrar.Register(this);
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
                    _connectionString = ConnectionStringResolver.Resolve(ConnectionName).ConnectionString;
                }

                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public override long? GetLongValue(string columnName, System.Data.DataRow row)
        {
            object value = row[columnName];
            if (value is long || value is long?)
            {
                return (long?)value;
            }
            else if (value is int || value is int?)
            {
                int d = (int)value;
                return Convert.ToInt64(d);
            }
            return null;
        }
    }
}
