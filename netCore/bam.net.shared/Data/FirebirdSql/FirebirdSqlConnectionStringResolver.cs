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
using FirebirdSql;
using FirebirdSql.Data.FirebirdClient;

namespace Bam.Net.Data.FirebirdSql
{
    public class FirebirdSqlConnectionStringResolver : IConnectionStringResolver
    {
        public FirebirdSqlConnectionStringResolver(string databaseName, FirebirdSqlCredentials credentials = null) : this("localhost", databaseName, credentials)
        {
        }

        public FirebirdSqlConnectionStringResolver(string serverName, string databaseName, FirebirdSqlCredentials credentials = null)
        {
            ServerName = serverName;
            DatabaseName = databaseName;
            Credentials = credentials ?? new FirebirdSqlCredentials();
            Role = string.Empty;
            Pooling = true;
            Port = 3050;
            ConnectionLifetime = 15;
            PacketSize = 8192;
        }

        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public int Port { get; set; }

        public bool Pooling { get; set; }
        public int MinPoolSize { get; set; }
        public int MaxPoolSize { get; set; }
        public string Role { get; set; }
        public int ConnectionLifetime { get; set; }
        public bool EmbeddedMode { get; set; }
        public int PacketSize { get; set; }
        public FirebirdSqlCredentials Credentials { get; set; }

        #region IConnectionStringResolver Members

        public ConnectionStringSettings Resolve(string connectionName)
        {
            ConnectionStringSettings s = new ConnectionStringSettings
            {
                Name = connectionName,
                ProviderName = typeof(FirebirdClientFactory).AssemblyQualifiedName
            };
            DbConnectionStringBuilder connectionStringBuilder = GetConnectionStringBuilder();
            s.ConnectionString = connectionStringBuilder.ConnectionString;

            return s;
        }

        public DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            DbConnectionStringBuilder connectionStringBuilder = FirebirdClientFactory.Instance.CreateConnectionStringBuilder();
            connectionStringBuilder.Add("DataSource", ServerName);
            connectionStringBuilder.Add("Database", DatabaseName);
            connectionStringBuilder.Add("User", Credentials.UserId);
            connectionStringBuilder.Add("Password", Credentials.Password);
            connectionStringBuilder.Add("Dialect", 3);
            connectionStringBuilder.Add("Charset", "NONE");
            connectionStringBuilder.Add("Role", Role);
            connectionStringBuilder.Add("Connection lifetime", ConnectionLifetime);
            if (Port > 0)
            {
                connectionStringBuilder.Add("Port", Port.ToString());
            }
            if (Pooling)
            {
                connectionStringBuilder.Add("Pooling", "true");
            }
            connectionStringBuilder.Add("MinPoolSize", MinPoolSize);
            connectionStringBuilder.Add("MaxPoolSize", MaxPoolSize);
            connectionStringBuilder.Add("Packet Size", PacketSize);
            connectionStringBuilder.Add("ServerType", EmbeddedMode ? 1: 0);
            return connectionStringBuilder;
        }

        #endregion
    }
}
