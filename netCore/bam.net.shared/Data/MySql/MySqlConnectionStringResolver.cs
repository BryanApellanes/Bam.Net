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
using MySql.Data.MySqlClient;

namespace Bam.Net.Data.MySql
{
	public class MySqlConnectionStringResolver: IConnectionStringResolver
	{
		public MySqlConnectionStringResolver(string serverName, string databaseName, MySqlCredentials credentials = null)
		{
			ServerName = serverName;
			DatabaseName = databaseName;
			Credentials = credentials ?? new MySqlCredentials();
			TrustedConnection = credentials == null;
            Ssl = true;
		}

		public string ServerName { get; set; }
		public string DatabaseName { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
		public bool TrustedConnection { get; set; }

		public MySqlCredentials Credentials { get; set; }

		#region IConnectionStringResolver Members

		public ConnectionStringSettings Resolve(string connectionName)
        {
            ConnectionStringSettings s = new ConnectionStringSettings();
            s.Name = connectionName;
            s.ProviderName = typeof(MySqlClientFactory).AssemblyQualifiedName;
            DbConnectionStringBuilder connectionStringBuilder = GetConnectionStringBuilder();

            s.ConnectionString = connectionStringBuilder.ConnectionString;

            return s;
        }

        public DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            //Data Source=chumsql2;User ID=mysql;Password=******;Allow Zero Datetime=Yes;Allow User Variables=true;Persist Security Info=true;Default Command Timeout=3600
            DbConnectionStringBuilder connectionStringBuilder = MySqlClientFactory.Instance.CreateConnectionStringBuilder();
            connectionStringBuilder.Add("Data Source", ServerName);
            connectionStringBuilder.Add("Database", DatabaseName);
            connectionStringBuilder.Add("User ID", Credentials.UserId);
            connectionStringBuilder.Add("Password", Credentials.Password);
            if (Port > 0)
            {
                connectionStringBuilder.Add("Port", Port.ToString());
            }
            if(!Ssl)
            {
                connectionStringBuilder.Add("SslMode", "none");
            }
            return connectionStringBuilder;
        }

        #endregion
    }
}
