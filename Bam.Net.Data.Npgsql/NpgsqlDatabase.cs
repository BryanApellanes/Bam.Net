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
using Npgsql;

namespace Bam.Net.Data.Npgsql
{
    public class NpgsqlDatabase : Database, IHasConnectionStringResolver
    {
        public NpgsqlDatabase()
        {
            ConnectionStringResolver = DefaultConnectionStringResolver.Instance;
            Register();
        }
        public NpgsqlDatabase(string serverName, string databaseName, NpgsqlCredentials credentials = null)
            : this(serverName, databaseName, databaseName, credentials)
        { }

        public NpgsqlDatabase(string serverName, string databaseName, string connectionName, NpgsqlCredentials credentials = null)
        {
            ColumnNameProvider = (c) => "\"{0}\""._Format(c.Name);
            ConnectionStringResolver = new NpgsqlConnectionStringResolver(serverName, databaseName, credentials);
            ConnectionName = connectionName;
            Register();
        }

        public NpgsqlDatabase(string connectionString, string connectionName = null)
            : base(connectionString, connectionName)
        {
        }

        private void Register()
        {
            ServiceProvider = new Incubator();
            ServiceProvider.Set<DbProviderFactory>(NpgsqlFactory.Instance);
            NpgsqlRegistrar.Register(this);
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
