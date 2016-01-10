/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Testing;
using Bam.Net.Incubation;
using Bam.Net.DaoRef;
using Bam.Net.CommandLine;
using Bam.Net.Data.MsSql;
using Bam.Net.Data.SQLite;
using Bam.Net.Data.Oracle;
using Bam.Net.Data.MySql;
using Bam.Net.Data.Npgsql;
using Bam.Net.Testing.Integration;
using Bam.Net.Data.Schema;
using System.Configuration;

namespace Bam.Net.Data.Integration.Tests
{
	public static class DataTools
	{
		/// <summary>
		/// Create a set of test databases
		/// </summary>
		/// <returns></returns>
		[ConsoleAction]
		public static HashSet<Database> Setup()
		{
			HashSet<Database> testDatabases = new HashSet<Database>();

            //MsSqlDatabase msDatabase = new MsSqlDatabase("chumsql2", "DaoRef", new MsSqlCredentials { UserName = "mssqluser", Password = "mssqlP455w0rd" });
            //Db.TryEnsureSchema<TestTable>(msDatabase);
            //testDatabases.Add(msDatabase);

            //SQLiteDatabase sqliteDatabase = new SQLiteDatabase(".\\DaoCrudTests", "DaoRef");
            //Db.TryEnsureSchema<TestTable>(sqliteDatabase);
            //testDatabases.Add(sqliteDatabase);

            //OracleDatabase oracleDatabase = new OracleDatabase("chumsql2", "DaoRef", new OracleCredentials { UserId = "C##ORACLEUSER", Password = "oracleP455w0rd" });
            //Db.TryEnsureSchema<TestTable>(oracleDatabase);
            //testDatabases.Add(oracleDatabase);

            MySqlDatabase mySqlDatabase = new MySqlDatabase("chumsql2", "DaoRef", new MySqlCredentials { UserId = "mysql", Password = "mysqlP455w0rd" });
            Db.TryEnsureSchema<TestTable>(mySqlDatabase);
            testDatabases.Add(mySqlDatabase);

            NpgsqlDatabase npgsqlDatabase = new NpgsqlDatabase("chumsql2", "DaoRef", new NpgsqlCredentials { UserId = "postgres", Password = "postgresP455w0rd" });
            Db.TryEnsureSchema<TestTable>(npgsqlDatabase);
            testDatabases.Add(npgsqlDatabase);

            return testDatabases;
		}

		[ConsoleAction]
		public static void Cleanup(HashSet<Database> testDatabases)
		{
			testDatabases.Each(db =>
			{
				SchemaWriter dropper = db.ServiceProvider.Get<SchemaWriter>();
				dropper.EnableDrop = true;
				dropper.DropAllTables<TestTable>();
				db.ExecuteSql(dropper);
			});
		}

        public static SchemaExtractor GetMsSqlSmoSchemaExtractor(string connectionName)
        {
            string connString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            Expect.IsNotNullOrEmpty(connString, "The connection string named {0} wasn't found in the config file"._Format(connectionName));

            return new MsSqlSmoSchemaExtractor(connectionName);
        }

        public static SchemaExtractor GetMsSqlSmoSchemaExtractor(MsSqlDatabase database)
        {
            return new MsSqlSmoSchemaExtractor(database);
        }
		public static TestTable CreateTestTable(string name, Database db)
		{
			return CreateTestTable(name, string.Empty, db);
		}

		public static TestTable CreateTestTable(string name, string description, Database db, bool save = true)
		{
			TestTable table = new TestTable(db);
			table.Name = name;
			table.Description = description;
			if (save)
			{
				table.Save(db);
			}
			return table;
		}

		public static Left CreateLeft(string name, Database db)
		{
			Left left = new Left(db);
			left.Name = name;
			left.Save(db);

			return left;
		}

		public static Right CreateRight(string name, Database db)
		{
			Right right = new Right(db);
			right.Name = name;
			right.Save(db);

			return right;
		}
	}
}
