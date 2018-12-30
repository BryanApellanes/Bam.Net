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

namespace Bam.Net.Data.MsSql
{
	public class MsSqlDatabase: Database, IHasConnectionStringResolver
	{
        public MsSqlDatabase()
        {
            ConnectionStringResolver = DefaultConnectionStringResolver.Instance;
            Register();
        }
		public MsSqlDatabase(string serverName, string databaseName, MsSqlCredentials credentials = null)
			: this(serverName, databaseName, databaseName, credentials)
		{ }

		public MsSqlDatabase(string serverName, string databaseName, string connectionName, MsSqlCredentials credentials = null)
			: base()
		{
			ConnectionStringResolver = new MsSqlConnectionStringResolver(serverName, databaseName, credentials);
			ConnectionName = connectionName;
            Register();
		}

        public MsSqlDatabase(MsSqlConnectionStringResolver connectionStringResolver)
        {
            ConnectionName = connectionStringResolver.DatabaseName;
            ConnectionStringResolver = connectionStringResolver;
            Register();
        }

        public MsSqlDatabase(string connectionString, string connectionName = null) 
            : base(connectionString, connectionName)
        {
            Register();
        }

        private void Register()
        {
            ServiceProvider = new Incubator();
            ServiceProvider.Set<DbProviderFactory>(SqlClientFactory.Instance);
            MsSqlRegistrar.Register(this);
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
	}
}
