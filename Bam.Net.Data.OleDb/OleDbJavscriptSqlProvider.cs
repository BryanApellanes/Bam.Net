/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.OleDb;
using Bam.Net.Configuration;

namespace Bam.Net.Javascript.Sql
{
	public class OleDbJavscriptSqlProvider: JavaScriptSqlProvider
	{
		public OleDbJavscriptSqlProvider()
		{
		}

		public string Provider { get; set; }
        public string DataSource { get; set; }
		protected override void Initialize()
		{
			OleDbDatabase database = new OleDbDatabase();
            OleDbConnectionStringResolver conn = new OleDbConnectionStringResolver(Provider, DataSource);
			database.ConnectionStringResolver = conn;
			Database = database;
		}

		#region IHasRequiredProperties Members

		public override string[] RequiredProperties
		{
			get
			{
                return new string[] { "Provider", "DataSource" };
			}
		}

		#endregion
	}
}
