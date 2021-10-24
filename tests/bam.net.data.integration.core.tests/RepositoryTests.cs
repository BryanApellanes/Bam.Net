/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing.Integration;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Data.SQLite;
using Bam.Net.Data.Repositories;
using System.IO;
using System.Reflection;
using Bam.Net.DaoRef;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Integration.Core.Tests
{
	[Serializable]
	public class TestDto
	{
		public long Id { get; set; }
		public string Uuid { get; set; }
		public string Name { get; set; }
	}

	[IntegrationTestContainer]
	public class RepositoryTests: CommandLineTool
	{
		HashSet<IRepository> _repos;
		DirectoryInfo _sqliteDir;
		DirectoryInfo _objectDir;
		public RepositoryTests()
		{		
		}
		
		[IntegrationTestSetup]
		public void Setup()
		{
			_sqliteDir = new DirectoryInfo("./RepositoryTests_SQLite");
			_objectDir = new DirectoryInfo("./RepositoryTests_ObjectRepo");
			_repos = new HashSet<IRepository>();
			_repos.Add(new DaoRepository(new SQLiteDatabase(_sqliteDir.FullName, "RepositoryTests")));
			//_repos.Add(new ObjectRepository(_objectDir.FullName));	
		}

		[IntegrationTestCleanup]
		public void Cleanup()
		{
			_sqliteDir.Delete(true);
			_objectDir.Delete(true);
		}

        [IntegrationTest]
		public void UpdateShouldntResetId()
		{
			string testName = MethodBase.GetCurrentMethod().Name;
			_repos.Each(testName, (tn, repo) =>
			{
				repo.AddType(typeof(TestDto));
				string name1 = 8.RandomLetters();
				string name2 = 8.RandomLetters();
				TestDto value = new TestDto { Name = name1 };
				Expect.IsTrue(value.Id == 0, "Id should have been 0");
				repo.Save(value);
				Expect.IsNotNullOrEmpty(value.Uuid, "Uuid didn't get set");
				long id = value.Id;
				string uuid = value.Uuid;
				Message.PrintLine("The id is {0}", id);
				Message.PrintLine("The Uuid is {0}", uuid);
				Expect.IsTrue(value.Id > 0, "Id should have been greater than 0");
				TestDto retrieved = repo.Retrieve<TestDto>(id);
				Expect.AreEqual(name1, retrieved.Name);
				retrieved.Name = name2;
				repo.Save(retrieved);
				Expect.AreEqual(id, retrieved.Id);
				Expect.AreEqual(uuid, retrieved.Uuid);

				retrieved = repo.Retrieve<TestDto>(id);
				Expect.AreEqual(id, retrieved.Id, "The id changed");
				Expect.AreEqual(uuid, retrieved.Uuid);
				Expect.AreEqual(name2, retrieved.Name);

				Pass("{0}:{1}"._Format((string)tn, repo.GetType().Name));
			});
		}
	}
}
