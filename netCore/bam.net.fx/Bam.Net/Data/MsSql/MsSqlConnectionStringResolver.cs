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

namespace Bam.Net.Data.MsSql
{
	public class MsSqlConnectionStringResolver: IConnectionStringResolver
	{
		public MsSqlConnectionStringResolver(string serverName, string databaseName, MsSqlCredentials credentials = null)
		{
			this.ServerName = serverName;
			this.DatabaseName = databaseName;
			this.Credentials = credentials;
			this.TrustedConnection = credentials == null;
		}

		public string ServerName { get; set; }
		public string DatabaseName { get; set; }

		public bool TrustedConnection { get; set; }

		public MsSqlCredentials Credentials { get; set; }

		#region IConnectionStringResolver Members

		public ConnectionStringSettings Resolve(string connectionName)
        {
            ConnectionStringSettings s = new ConnectionStringSettings();
            s.Name = connectionName;
            s.ProviderName = typeof(SqlClientFactory).AssemblyQualifiedName;

            DbConnectionStringBuilder connectionStringBuilder = GetConnectionStringBuilder();

            s.ConnectionString = connectionStringBuilder.ConnectionString;

            return s;
        }

        public DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            DbConnectionStringBuilder connectionStringBuilder = SqlClientFactory.Instance.CreateConnectionStringBuilder();
            connectionStringBuilder.Add("Data Source", ServerName);
            connectionStringBuilder.Add("Initial Catalog", DatabaseName);
            if (TrustedConnection)
            {
                connectionStringBuilder.Add("Integrated Security", "SSPI");
            }
            else
            {
                connectionStringBuilder.Add("User ID", Credentials.UserName);
                connectionStringBuilder.Add("Password", Credentials.Password);
            }

            return connectionStringBuilder;
        }

        #endregion
    }
}
