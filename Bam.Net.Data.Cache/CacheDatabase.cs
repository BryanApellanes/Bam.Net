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
using InterSystems.Data.CacheClient;

namespace Bam.Net.Data.Cache
{
    public class CacheDatabase : Database, IHasConnectionStringResolver
    {
        public CacheDatabase()
        {
            ConnectionStringResolver = DefaultConnectionStringResolver.Instance;
            Register();
        }
        public CacheDatabase(string serverName, string databaseName, CacheCredentials credentials = null)
            : this(serverName, databaseName, databaseName, credentials)
        { }

        public CacheDatabase(string serverName, string databaseName, string connectionName, CacheCredentials credentials = null)
            : base()
        {
            ConnectionStringResolver = new CacheConnectionStringResolver(serverName, databaseName, credentials);
            ConnectionName = connectionName;
            Register();
        }

        public CacheDatabase(CacheConnectionStringResolver connectionStringResolver)
        {
            ConnectionName = connectionStringResolver.DatabaseName;
            ConnectionStringResolver = connectionStringResolver;
            Register();
        }

        public CacheDatabase(string connectionString, string connectionName = null)
            : base(connectionString, connectionName)
        {
            Register();
        }

        private void Register()
        {
            ServiceProvider = new Incubator();
            ServiceProvider.Set<DbProviderFactory>(CacheFactory.Instance);
            CacheRegistrar.Register(this);
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
