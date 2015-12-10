/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.MsSql;
using Bam.Net.Configuration;

namespace Bam.Net.Javascript.Sql
{
	public class MsSqlJavscriptSqlProvider: JavaScriptSqlProvider
	{
		public MsSqlJavscriptSqlProvider()
		{
		}

		public string MsSqlUserId { get; set; }
		public string MsSqlPassword { get; set; }
		public string MsSqlServerName { get; set; }
		public string MsSqlDatabaseName { get; set; }

		protected override void Initialize()
		{
			MsSqlDatabase database = new MsSqlDatabase();
			MsSqlCredentials creds = new MsSqlCredentials { UserName = MsSqlUserId, Password = MsSqlPassword };
            MsSqlConnectionStringResolver conn = new MsSqlConnectionStringResolver(MsSqlServerName, MsSqlDatabaseName, creds);
			database.ConnectionStringResolver = conn;
			Database = database;
		}

		#region IHasRequiredProperties Members

		public override string[] RequiredProperties
		{
			get
			{
                return new string[] { "MsSqlServerName", "MsSqlDatabaseName" };
			}
		}

		#endregion
	}
}
