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

namespace Bam.Net.Data.Intersystems
{
    public class InterSystemsDatabase : Database, IHasConnectionStringResolver
    {
        public InterSystemsDatabase()
        {
            ConnectionStringResolver = DefaultConnectionStringResolver.Instance;
            Register();
        }
        public InterSystemsDatabase(string serverName, string databaseName, InterSystemsCredentials credentials = null)
            : this(serverName, databaseName, databaseName, credentials)
        { }

        public InterSystemsDatabase(string serverName, string databaseName, string connectionName, InterSystemsCredentials credentials = null)
            : base()
        {
            ConnectionStringResolver = new InterSystemsConnectionStringResolver(serverName, databaseName, credentials);
            ConnectionName = connectionName;
            Register();
        }

        public InterSystemsDatabase(InterSystemsConnectionStringResolver connectionStringResolver)
        {
            ConnectionName = connectionStringResolver.DatabaseName;
            ConnectionStringResolver = connectionStringResolver;
            Register();
        }

        public InterSystemsDatabase(string connectionString, string connectionName = null)
            : base(connectionString, connectionName)
        {
            Register();
        }

        private void Register()
        {
            SelectStar = true;
            ServiceProvider = new Incubator();
            ServiceProvider.Set<DbProviderFactory>(CacheFactory.Instance);
            InterSystemsRegistrar.Register(this);
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

        /// <summary>
        /// The prefix to prepend to table names when a SqlStringBuilder is retrieved.
        /// </summary>
        public string TableNamePrefix { get; set; }

        public override SqlStringBuilder GetSqlStringBuilder()
        {
            InterSystemsSqlStringBuilder sqlStringBuilder = (InterSystemsSqlStringBuilder)base.GetSqlStringBuilder();
            sqlStringBuilder.TableNamePrefix = TableNamePrefix;
            sqlStringBuilder.SelectStar = SelectStar;
            return sqlStringBuilder;
        }

        public override QuerySet GetQuerySet()
        {
            InterSystemsQuerySet qs = (InterSystemsQuerySet)base.GetQuerySet();
            qs.TableNamePrefix = TableNamePrefix;
            return qs;
        }
    }
}
