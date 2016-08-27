/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web;
using Oracle.ManagedDataAccess;
using System.Data.Common;

namespace Bam.Net.Data
{
    /// <summary>
    /// Resolves connection string requests to a Oracle database
    /// </summary>
    public class OracleConnectionStringResolver: IConnectionStringResolver
    {
		public OracleConnectionStringResolver() { }
        public OracleConnectionStringResolver(string serverName, OracleCredentials creds = null)
        {
			this.ServerName = serverName;
			this.Credentials = creds ?? new OracleCredentials();
			this.InstanceName = "ORCL";
			this.Port = "1521";
        }

		public string ServerName { get; set; }
		public string Port { get; set; }
		public OracleCredentials Credentials { get; set; }
		public string InstanceName { get; set; }
		public string Password { get { return Credentials.Password; } }
		public string UserId { get { return Credentials.UserId; } }

        #region IConnectionStringResolver Members

        public ConnectionStringSettings Resolve(string connectionName)
        {
			ConnectionStringSettings connSettings = new ConnectionStringSettings();
			connSettings.Name = connectionName;
			connSettings.ProviderName = OracleRegistrar.OracleAssemblyQualifiedName();
			connSettings.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={ServerName})(PORT={Port}))(CONNECT_DATA=(SERVICE_NAME={InstanceName})));User Id={UserId};Password={Password};".NamedFormat(this);

			return connSettings;
        }

        public DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            
            return new DbConnectionStringBuilder { ConnectionString = Resolve("Default")?.ConnectionString };
        }
        #endregion
    }
}
