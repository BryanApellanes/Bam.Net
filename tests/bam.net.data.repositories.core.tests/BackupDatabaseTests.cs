/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Schema;
using Bam.Net.Data;
using Bam.Net.Data.MsSql;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using Bam.Net.Data.SQLite;
using System.Data;
using System.Collections;
using Bam.Net.Logging;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Repositories.Tests
{
	[Serializable]
	public class BackupDatabaseTests : CommandLineTool
	{
		static ILogger _logger;
		static object _loggerLock = new object();
		public new static ILogger Logger
		{
			get
			{
				return _loggerLock.DoubleCheckLock(ref _logger, () =>
				{
					var logger = new ConsoleLogger();
					logger.StartLoggingThread();
					return logger;
				});
			}
		}

		[UnitTest]
		public void ShouldBeAbleToGetOne()
		{
			SQLiteDatabase db = GetTestDatabase(false);
			string name = 8.RandomLetters();
			MainObject o = MainObject.OneWhere(c => c.Name == name, db);
			Expect.IsNull(o);
			o = MainObject.GetOneWhere(c => c.Name == name, db);
			Expect.IsNotNull(o);
			Expect.AreEqual(name, o.Name);
			MainObject check = MainObject.GetOneWhere(c => c.Name == name, db);
			Expect.AreEqual(o.Id, check.Id);
			Expect.AreEqual(o.Uuid, check.Uuid);
		}

		[UnitTest]
		public void ShouldBeAbleToShowFksFromReflection()
		{
			SQLiteDatabase testDatabase = GetTestDatabase();
			typeof(MainObject).Assembly.GetTypes().Where(t => t.HasCustomAttributeOfType<TableAttribute>()).Each(t =>
			{
				TableAttribute table = t.GetCustomAttribute<TableAttribute>();
				t.GetProperties().Where(p => p.HasCustomAttributeOfType<ForeignKeyAttribute>()).Each(p =>
				{
					ForeignKeyAttribute fk = p.GetCustomAttribute<ForeignKeyAttribute>();
					Message.PrintLine("ReferencingTable: {0}, ReferencedKey: {1}, ReferencedTable: {2}", table.TableName, fk.ReferencedKey, fk.ReferencedTable);
				});
			});
		}

		[UnitTest]
		public void ShouldBeAbleToSetValuesFromQueryFilter()
		{
			SQLiteDatabase testDatabase = GetTestDatabase();
			string testName = 8.RandomLetters();
			QueryFilter filter = Query.Where("Name") == testName;
			MainObject one = MainObject.OneWhere(filter, testDatabase);
			Expect.IsNull(one);
			if (one == null)
			{
				one = new MainObject();
				filter.Parameters.Each(p =>
				{
					one.Property(p.ColumnName, p.Value);
				});
				one.Save(testDatabase);
			}

			MainObject check = MainObject.OneWhere(filter, testDatabase);
			Expect.IsNotNull(check);
			OutLine(check.TryPropertiesToString());
		}

		// Record the OldToNewIdMapping for each

		// -- restore
        private static Func<Type, bool> TestDaoPredicate
        {
            get
            {
                return t => t.HasCustomAttributeOfType<TableAttribute>() && t.Namespace.Equals(typeof(MainObject).Namespace);
            }
        }

		private static void CreateTestDatabases(string sourceName, string destName, out SQLiteDatabase source, out SQLiteDatabase dest)
		{
			source = new SQLiteDatabase(".\\{0}"._Format(sourceName), "{0}Db"._Format(sourceName));
			source.TryEnsureSchema(typeof(MainObject), new ConsoleLogger());
			dest = new SQLiteDatabase(".\\{0}"._Format(destName), "{0}Db"._Format(destName));
			dest.TryEnsureSchema(typeof(MainObject), new ConsoleLogger());
		}

		private static void FillDatabaseWithTestData(SQLiteDatabase toBackup)
		{
			3.Times(i =>
			{
				Message.PrintLine("Creating main...", ConsoleColor.DarkCyan);
				MainObject m = new MainObject();
				m.Created = DateTime.UtcNow;
				m.Name = "MainObject: {0}"._Format(i);
				m.Save(toBackup);
				OutLine(m.PropertiesToString(), ConsoleColor.Blue);

				Message.PrintLine("\tAdding secondary...", ConsoleColor.Yellow);				
				RandomNumber.Between(1, 3).Times(n =>
				{
					SecondaryObject s = m.SecondaryObjectsByMainId.AddChild();
					s.Created = DateTime.UtcNow;
					s.Name = "\tSecondaryObject: {0}"._Format(n + 1);
				});
				m.Save(toBackup);				
				m.SecondaryObjectsByMainId.Each(s =>
				{
					Message.PrintLine("\t\tAdding ternary...", ConsoleColor.Blue);
					Message.PrintLine(s.PropertiesToString(), ConsoleColor.Cyan);
					RandomNumber.Between(1, 3).Times(n =>
					{
						TernaryObject t = s.TernaryObjects.AddNew();
						t.Created = DateTime.UtcNow;
						t.Name = "\t\tTernaryObject: {0}"._Format(n + 1);
					});
					s.Save(toBackup);
					s.TernaryObjects.Each(t =>
					{
						Message.PrintLine(t.PropertiesToString(), ConsoleColor.DarkGray);
					});
				});
			});
		}
		// restore from 

		private static SQLiteDatabase GetTestDatabase(string namePrefix)
		{
			return GetTestDatabase(true, namePrefix);
		}
		private static SQLiteDatabase GetTestDatabase(bool setupTestData = true, string namePrefix = null)
		{
			namePrefix = namePrefix ?? 8.RandomLetters();
			SQLiteDatabase testDatabase = new SQLiteDatabase(namePrefix + "_SQLite", Dao.ConnectionName(typeof(MainObject)));
			if(testDatabase.DatabaseFile.Exists)
			{
				try
				{
					testDatabase.DatabaseFile.Delete();
				}
				catch (Exception ex)
				{
					Message.PrintLine("Unable to delete existing db file: {0}", ConsoleColor.Yellow, ex.Message);
				}
			}

			Exception e;
			EnsureSchemaStatus result = testDatabase.TryEnsureSchema(typeof(MainObject), out e, Logger);
			if (result == EnsureSchemaStatus.Error)
			{
				throw e;
			}

			if (setupTestData)
			{
				SetupTestData(testDatabase);
			}

			return testDatabase;
		}

		private static void SetupTestData(SQLiteDatabase testDatabase)
		{
			10.Times(i =>
			{
				MainObject o = new MainObject();
				o.Name = 8.RandomLetters().Plus("::Main_Object {0}", i);
				o.Created = DateTime.UtcNow;
				o.Save(testDatabase);
			});
			10.Times(i =>
			{
				SecondaryObject o = new SecondaryObject();
				o.Name = 4.RandomLetters().Plus("::Secondary_object {0}", i);
				o.Created = DateTime.UtcNow;
				o.Save(testDatabase);
			});
			10.Times(i =>
			{
				TernaryObject o = new TernaryObject();
				o.Name = 3.RandomLetters().Plus("::Ternary_object ", i);
				o.Created = DateTime.UtcNow;
				o.Save(testDatabase);
			});
		}

		private static void OutputWasIs(HashSet<OldToNewIdMapping> wasIs)
		{
			wasIs.Each(otnim =>
			{
				Message.PrintLine("Type: {0}, Uuid: {1}, OldId: {2}, NewId: {3}", otnim.DaoType.Name, otnim.Uuid, otnim.OldId, otnim.NewId);
			});
		}

		private static IRepository GetBackupDaoRepo(string methodName)
		{
			IRepository repo = new DaoRepository(new SQLiteDatabase("{0}_BackupRepo"._Format(methodName), "BackupRepoDb"));
			return repo;
		}

	}
}
