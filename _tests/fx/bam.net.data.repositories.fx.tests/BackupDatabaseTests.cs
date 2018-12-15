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
	public class BackupDatabaseTests : CommandLineTestInterface
	{
		static ILogger _logger;
		static object _loggerLock = new object();
		public static ILogger Logger
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
		// *** Backup process and restore
		// Get Dto representation of existing Daos
		[UnitTest]
		public void ShouldBeAbleToGetGeneratedDtoAssembly()
		{
			string curDir = Environment.CurrentDirectory;
			string newDir = Path.Combine(curDir, string.Format("{0}_{1}"._Format(MethodBase.GetCurrentMethod().Name, 4.RandomLetters())));
			if(!Directory.Exists(newDir))
			{
				Directory.CreateDirectory(newDir);
			}
			Environment.CurrentDirectory = newDir;
			Assembly assembly = typeof(MainObject).Assembly;
			string pocoAssembly = Dto.GetDefaultFileName(assembly);
			if (File.Exists(pocoAssembly))
			{
				File.Delete(pocoAssembly);
			}

			// Do it thrice to ensure testing of multiple execution paths within implementation
			GeneratedAssemblyInfo assemblyInfo = Dto.GetGeneratedDtoAssemblyInfo(typeof(MainObject).Assembly);			
			assemblyInfo = Dto.GetGeneratedDtoAssemblyInfo(typeof(MainObject).Assembly);
			assemblyInfo = Dto.GetGeneratedDtoAssemblyInfo(typeof(MainObject).Assembly);
			
			Environment.CurrentDirectory = curDir;
		}
		// Create Repository and add generated Dto types to it
		[UnitTest]
		public void ShouldBeAbleToCreateRepositoryAndAddGeneratedDtoTypesToIt()
		{
			ObjectRepository repo = new ObjectRepository(MethodBase.GetCurrentMethod().Name);
			Type[] pocoTypes = Dto.GetTypesFromDaos(typeof(MainObject).Assembly);
			repo.AddTypes(pocoTypes);
			OutLine("All poco types", ConsoleColor.Blue);
			pocoTypes.Each(t =>
			{
				OutLine(t.Name, ConsoleColor.Cyan);
			});
			OutLine("All storeable types", ConsoleColor.Blue);
			repo.StorableTypes.Each(t =>
			{
				OutLine(t.Name, ConsoleColor.Cyan);
			});
		}

		[UnitTest]
		public void ShouldBeAbleToCopyDaoAsGeneratedDtoType()
		{
			SQLiteDatabase saveTo = GetTestDatabase(false);
			MainObject toAdd = new MainObject();
			toAdd.Name = 4.RandomLetters().Plus(":: The Name");
			toAdd.Save(saveTo);

			object dtoInstance = Dto.Copy(toAdd);
			OutLine("Dto info", ConsoleColor.Cyan);
			OutLineFormat("Type: {0}.{1}", dtoInstance.GetType().Namespace, dtoInstance.GetType().Name);
			OutLine(dtoInstance.TryPropertiesToString());

			MainObject daoInstance = MainObject.FirstOneWhere(c => c.Name == dtoInstance.Property("Name"), saveTo);
			Expect.IsNotNull(daoInstance);
			Expect.AreSame(saveTo, daoInstance.Database);
			OutLine("Dao info", ConsoleColor.Cyan);
			OutLineFormat("Type: {0}.{1}", daoInstance.GetType().Namespace, daoInstance.GetType().Name);
			OutLine(daoInstance.TryPropertiesToString());
		}

		// Get all the Dao instances of every type and save into the Repository
		[UnitTest]
		public void ShouldBeAbleToBackupWithDatabaseBackup()
		{
			SQLiteDatabase toBackup = GetTestDatabase("backup_");
			DaoBackup backup = new DaoBackup(typeof(MainObject).Assembly, toBackup);
			backup.Backup();
			MainObject one = MainObject.FirstOneWhere(c => c.Id > 0, toBackup);
			IRepository repo = backup.BackupRepository;
			object fromRepo = repo.Retrieve(Dto.TypeFor(one), one.Uuid);
			Expect.AreEqual(one.Uuid, fromRepo.Property("Uuid"));
			Expect.AreEqual(one.Name, fromRepo.Property("Name"));
			OutLine(fromRepo.TryPropertiesToString(), ConsoleColor.DarkBlue);
			OutLine("Dto types defined in repo are: ", ConsoleColor.DarkCyan);
			repo.StorableTypes.Each(t =>
			{
				OutLineFormat("Namespace: {0}, Name: {1}", t.Namespace, t.Name);
			});
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
					OutLineFormat("ReferencingTable: {0}, ReferencedKey: {1}, ReferencedTable: {2}", table.TableName, fk.ReferencedKey, fk.ReferencedTable);
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
		[UnitTest]
		public void ShouldBeAbleToRestore()
		{
			SQLiteDatabase toBackup = GetTestDatabase(MethodBase.GetCurrentMethod().Name);
			toBackup.MaxConnections = 20;
			FillDatabaseWithTestData(toBackup);

			MainObjectCollection main = MainObject.LoadAll(toBackup);
			SecondaryObjectCollection secondary = SecondaryObject.LoadAll(toBackup);
			TernaryObjectCollection ternary = TernaryObject.LoadAll(toBackup);

            List<IRepository> repos = new List<IRepository>
            {
                new DaoRepository(new SQLiteDatabase("BackupRepo_{0}"._Format(MethodBase.GetCurrentMethod().Name), "BackupRepoDb")),
                new ObjectRepository("ObjectRepo_{0}"._Format(MethodBase.GetCurrentMethod().Name))
            };

            foreach (IRepository repo in repos)
			{
				DaoBackup backup = new DaoBackup(typeof(MainObject).Assembly, toBackup, repo);
				backup.Backup();
				SQLiteDatabase restored = GetTestDatabase("{0}_Restored_For_{1}"._Format(MethodBase.GetCurrentMethod().Name, repo.GetType().Name));

				HashSet<OldToNewIdMapping> restoreInfo = backup.Restore(restored);
				List<string> keys = restoreInfo.Select(i => i.Uuid).ToList();
				main.Each(new { Uuids = keys }, (ctx, m) =>
				{
					Expect.IsTrue(ctx.Uuids.Contains(m.Uuid));
				});
				secondary.Each(new { Uuids = keys }, (ctx, s) =>
				{
					Expect.IsTrue(ctx.Uuids.Contains(s.Uuid));
				});
				ternary.Each(new { Uuids = keys}, (ctx, t) =>
				{
					Expect.IsTrue(ctx.Uuids.Contains(t.Uuid));
				});
				restoreInfo.Each(ri =>
				{
					OutLineFormat(ri.PropertiesToString(), ConsoleColor.DarkYellow);
				});
				OutLineFormat("Repository of type {0} passed restore test", ConsoleColor.Green, repo.GetType().Name);
			}
		}

		[UnitTest]
		public void ShouldNotHaveSameId()
		{
			string sourceName = "{0}_{1}"._Format(MethodBase.GetCurrentMethod().Name, "Source");
			string destName = "{0}_{1}"._Format(MethodBase.GetCurrentMethod().Name, "Destintation");

			SQLiteDatabase source;
			SQLiteDatabase dest;
			CreateTestDatabases(sourceName, destName, out source, out dest);

			MainObject one = new MainObject();
			one.Name = "One: ".RandomLetters(8);
			one.Created = DateTime.UtcNow;
			one.Save(source);
			Expect.IsTrue(one.Id > 0);
			3.Times(i =>
			{
				MainObject existing = new MainObject();
				existing.Name = "Existing in destintation: ".RandomLetters(6);
				existing.Created = DateTime.UtcNow;
				existing.Save(dest);
			});
			MainObject check = MainObject.OneWhere(c => c.Name == one.Name, source);
			Expect.IsNotNull(check);
			check = MainObject.OneWhere(c => c.Name == one.Name, dest);
			Expect.IsNull(check, "Should have been null");

			string methodName = MethodBase.GetCurrentMethod().Name;

			List<IRepository> repos = new List<IRepository>();
			repos.Add(new DaoRepository(new SQLiteDatabase("BackupRepo_{0}"._Format(methodName), "BackupRepoDb")));
			repos.Add(new ObjectRepository("ObjectRepo_{0}"._Format(methodName)));

			foreach (IRepository repo in repos)
			{
                DaoBackup backup = new DaoBackup(typeof(MainObject).Assembly, source, repo);
				backup.Backup();
				HashSet<OldToNewIdMapping> wasIs = backup.Restore(dest);
				OutputWasIs(wasIs);
				check = MainObject.OneWhere(c => c.Uuid == one.Uuid, dest);
				Expect.IsNotNull(check);
				Expect.IsFalse(one.Id == check.Id);
				Expect.IsFalse(one.Id.Equals(check.Id));
				OutLineFormat(check.PropertiesToString());
			}
		}

		[UnitTest]
		public void ChildCollectionsShouldRestoreProperly()
		{
			string sourceName = "{0}_{1}"._Format(MethodBase.GetCurrentMethod().Name, "Source");
			string destName = "{0}_{1}"._Format(MethodBase.GetCurrentMethod().Name, "Destintation");
			Dao.GlobalInitializer = (dao) =>
			{
				dao.Property("Created", DateTime.UtcNow, false);
			};
			Dao.BeforeCommitAny += (db, dao) =>
			{
				dao.Property("Modified", DateTime.UtcNow, false);
			};

			SQLiteDatabase source;
			SQLiteDatabase dest;
			CreateTestDatabases(sourceName, destName, out source, out dest);

            MainObject main = new MainObject
            {
                Name = "The Main Parent"
            };
            main.Save(source);
			SecondaryObject secondary = main.SecondaryObjectsByMainId.AddNew();
			string secondaryOneName = 8.RandomLetters();
			secondary.Name = secondaryOneName;
			SecondaryObject secondary2 = main.SecondaryObjectsByMainId.AddNew();
			string secondaryTwoName = 6.RandomLetters();
			secondary2.Name = secondaryTwoName;
			main.Save(source);
			Expect.IsNotNullOrEmpty(main.Uuid);
			TernaryObject ternary = secondary2.TernaryObjects.AddNew();
			ternary.Name = 4.RandomLetters();
			ternary.Save(source);
			secondary2.Save(source);
			ternary = TernaryObject.OneWhere(c => c.Uuid == ternary.Uuid, source);
			Expect.AreEqual(1, ternary.SecondaryObjects.Count);
			string uuid = main.Uuid;
			MainObject check = MainObject.OneWhere(c => c.Id == main.Id, source);
			Expect.IsTrue(check.SecondaryObjectsByMainId.Count == 2);

			string methodName = MethodBase.GetCurrentMethod().Name;
            List<IRepository> repos = new List<IRepository>
            {
                new ObjectRepository("ObjectRepo_{0}"._Format(methodName))
            };

            foreach (IRepository repo in repos)
			{
				DaoBackup backup = new DaoBackup(typeof(MainObject).Assembly, source, repo);
				backup.Backup();
				HashSet<OldToNewIdMapping> wasIs = backup.Restore(dest);
				OutputWasIs(wasIs);
				MainObject toValidate = MainObject.OneWhere(c => c.Uuid == uuid, dest);
				Expect.IsTrue(toValidate.SecondaryObjectsByMainId.Count == 2);
				List<string> names = toValidate.SecondaryObjectsByMainId.Select(s => s.Name).ToList();
				Expect.IsTrue(names.Contains(secondaryOneName));
				Expect.IsTrue(names.Contains(secondaryTwoName));
				SecondaryObject secondary2Check = SecondaryObject.OneWhere(c => c.Uuid == secondary2.Uuid, dest);
				Expect.IsNotNull(secondary2Check);
				Expect.AreEqual(1, secondary2Check.TernaryObjects.Count);
				Expect.IsTrue(secondary2Check.TernaryObjects[0].Name.Equals(ternary.Name));
				TernaryObject ternaryCheck = TernaryObject.OneWhere(c=> c.Uuid == secondary2.TernaryObjects[0].Uuid, dest);
				Expect.IsTrue(ternaryCheck.SecondaryObjects.Count == 1);
			}
		}

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
				OutLineFormat("Creating main...", ConsoleColor.DarkCyan);
				MainObject m = new MainObject();
				m.Created = DateTime.UtcNow;
				m.Name = "MainObject: {0}"._Format(i);
				m.Save(toBackup);
				OutLine(m.PropertiesToString(), ConsoleColor.Blue);

				OutLineFormat("\tAdding secondary...", ConsoleColor.Yellow);				
				RandomNumber.Between(1, 3).Times(n =>
				{
					SecondaryObject s = m.SecondaryObjectsByMainId.AddNew();
					s.Created = DateTime.UtcNow;
					s.Name = "\tSecondaryObject: {0}"._Format(n + 1);
				});
				m.Save(toBackup);				
				m.SecondaryObjectsByMainId.Each(s =>
				{
					OutLineFormat("\t\tAdding ternary...", ConsoleColor.Blue);
					OutLine(s.PropertiesToString(), ConsoleColor.Cyan);
					RandomNumber.Between(1, 3).Times(n =>
					{
						TernaryObject t = s.TernaryObjects.AddNew();
						t.Created = DateTime.UtcNow;
						t.Name = "\t\tTernaryObject: {0}"._Format(n + 1);
					});
					s.Save(toBackup);
					s.TernaryObjects.Each(t =>
					{
						OutLineFormat(t.PropertiesToString(), ConsoleColor.DarkGray);
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
					OutLineFormat("Unable to delete existing db file: {0}", ConsoleColor.Yellow, ex.Message);
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
				OutLineFormat("Type: {0}, Uuid: {1}, OldId: {2}, NewId: {3}", otnim.DaoType.Name, otnim.Uuid, otnim.OldId, otnim.NewId);
			});
		}

		private static IRepository GetBackupDaoRepo(string methodName)
		{
			IRepository repo = new DaoRepository(new SQLiteDatabase("{0}_BackupRepo"._Format(methodName), "BackupRepoDb"));
			return repo;
		}

	}
}
