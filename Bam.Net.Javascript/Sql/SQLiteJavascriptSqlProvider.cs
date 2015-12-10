/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Configuration;

namespace Bam.Net.Javascript.Sql
{
	public class SQLiteJavaScriptSqlProvider: JavaScriptSqlProvider
	{
		public SQLiteJavaScriptSqlProvider() { }

		public string SQLiteDirectoryPath { get; set; }
		public string SQLiteFileName { get; set; }

		protected override void Initialize()
		{
			this.Database = new SQLiteDatabase(SQLiteDirectoryPath, SQLiteFileName);
		}

		public override string[] RequiredProperties
		{
			get { return new string[] { "SQLiteDirectoryPath", "SQLiteFileName" }; }
		}
	}
}
