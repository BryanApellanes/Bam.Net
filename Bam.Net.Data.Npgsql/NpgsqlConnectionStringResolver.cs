/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using Bam.Net.Data;
using Npgsql;

namespace Bam.Net.Data.Npgsql
{
	public class NpgsqlConnectionStringResolver: IConnectionStringResolver
	{
		public NpgsqlConnectionStringResolver(string serverName, string databaseName, NpgsqlCredentials credentials = null)
		{
			this.ServerName = serverName;
			this.DatabaseName = databaseName;
			this.Credentials = credentials ?? new NpgsqlCredentials();
            this.Pooling = true;
            this.Port = 5432;
		}

		public string ServerName { get; set; }
		public string DatabaseName { get; set; }
        public int Port { get; set; }

        public bool Pooling { get; set; }
        public int MinPoolSize { get; set; }
        public int MaxPoolSize { get; set; }
		public NpgsqlCredentials Credentials { get; set; }

		#region IConnectionStringResolver Members

		public ConnectionStringSettings Resolve(string connectionName)
        {
            ConnectionStringSettings s = new ConnectionStringSettings();
            s.Name = connectionName;
            s.ProviderName = typeof(NpgsqlFactory).AssemblyQualifiedName;
            DbConnectionStringBuilder connectionStringBuilder = GetConnectionStringBuilder();
            s.ConnectionString = connectionStringBuilder.ConnectionString;

            return s;
        }

        public DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            DbConnectionStringBuilder connectionStringBuilder = NpgsqlFactory.Instance.CreateConnectionStringBuilder();
            connectionStringBuilder.Add("Host", ServerName);
            connectionStringBuilder.Add("Database", DatabaseName);
            connectionStringBuilder.Add("User ID", Credentials.UserId);
            connectionStringBuilder.Add("Password", Credentials.Password);
            if (Port > 0)
            {
                connectionStringBuilder.Add("Port", Port.ToString());
            }
            if (Pooling)
            {
                connectionStringBuilder.Add("Pooling", "true");
            }

            return connectionStringBuilder;
        }

        #endregion
    }
}
