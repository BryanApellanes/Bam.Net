/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Oracle;
using Bam.Net.Configuration;

namespace Bam.Net.Javascript.Sql
{
	public class OracleJavaScriptSqlProvider: JavaScriptSqlProvider
	{
		public OracleJavaScriptSqlProvider()
		{
		}

		public string OracleUserId { get; set; }
		public string OraclePassword { get; set; }
		public string OracleServerName { get; set; }
		public string OraclePort { get; set; }
		public string OracleInstanceName { get; set; }

		protected override void Initialize()
		{
			OracleDatabase database = new OracleDatabase();
			OracleCredentials creds = new OracleCredentials { UserId = OracleUserId, Password = OraclePassword };
			OracleConnectionStringResolver conn = new OracleConnectionStringResolver { ServerName = OracleServerName, InstanceName = OracleInstanceName, Port = OraclePort, Credentials = creds };
			database.ConnectionStringResolver = conn;
			Database = database;
		}

		#region IHasRequiredProperties Members

		public override string[] RequiredProperties
		{
			get
			{
				return new string[] { "OracleUserId", "OraclePassword", "OracleServerName", "OraclePort", "OracleInstanceName" };
			}
		}

		#endregion
	}
}
