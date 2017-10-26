/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Data;
using Bam.Net.Data.Oracle;
using Bam.Net.Javascript;
using Bam.Net.Javascript.Sql;
using Bam.Net.Configuration;
using System.Reflection;
using System.Security.Cryptography;

namespace Bam.Net.Data.Tests
{
	[Serializable]
	public class ConsoleActions: CommandLineTestInterface
	{
        [ConsoleAction]
        public void DynamicReflectionTest()
        {
            dynamic o = new { Name = "Monkey", Age = 85 };
            OutLine(Bam.Net.Extensions.PropertiesToString(o));
        }

		[ConsoleAction]
		public void TestOracleSqlProvider()
		{
            string serverName = "chumsql2";
			OracleJavaScriptSqlProvider db = new OracleJavaScriptSqlProvider();
            OracleCredentials creds = new OracleCredentials { UserId = "C##ORACLEUSER", Password = "oracleP455w0rd" };
			OracleDatabase oracle = new OracleDatabase();
            oracle.ConnectionStringResolver = new OracleConnectionStringResolver { ServerName = serverName, Port = "1521", InstanceName = "orcl", Credentials = creds };
			db.Database = oracle;

			RunTestQuery(db);
		}

		[ConsoleAction]
		public void TestConfiguredOracleSqlProvider()
		{
			OracleJavaScriptSqlProvider db = new OracleJavaScriptSqlProvider();
			DefaultConfigurer configurer = new DefaultConfigurer();
			configurer.Configure(db);

			RunTestQuery(db);
		}

		[ConsoleAction]
		public void OutputOracleSqlProviderFullName()
		{
			Out(typeof(OracleJavaScriptSqlProvider).AssemblyQualifiedName);
		}

		private static void RunTestQuery(OracleJavaScriptSqlProvider db)
		{
			SqlResponse response = db.Execute("SELECT * FROM diamond WHERE ROWNUM <= 100");
			Expect.IsTrue(response.Count == 100);

			foreach (object result in response.Results)
			{
				OutLineFormat("{0}\r\n", result.PropertiesToString());
			}
		}
	}
}
