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
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Analytics;
using System.Threading;
using Bam.Net.Data.Repositories.Tests.TestDtos;

namespace Bam.Net.Data.Repositories.Tests
{
	[Serializable]
	public class ObjectRepositoryUnitTests : CommandLineTestInterface
	{
		[UnitTest]
		public void ObjectRepositoryMetaProviderTest() 
		{
			ObjectRepository repo = GetTestObjectRepository();
			repo.AddType(typeof(Parent));

			Parent p = new Parent {Name = "Test Parent"};
			Parent saved = repo.Save(p);
			Meta meta = repo.MetaProvider.GetMeta(saved);

			Expect.IsFalse(string.IsNullOrEmpty(p.Uuid));
			Expect.AreEqual(meta.Uuid, p.Uuid);
			Expect.AreEqual(meta.Id, p.Id);
		}

		[UnitTest]
		public void ObjectRepositoryMetaPropertyTest()
		{
			ObjectRepository repo = GetTestObjectRepository();
			repo.AddType(typeof(Parent));

			Parent p = new Parent { Name = "Test Parent" };
			Parent saved = repo.Save(p);
			Meta meta = repo.MetaProvider.GetMeta(saved);

			meta.Property("Name", "Monkey");
			Parent check = repo.Query<Parent>(q => q.Uuid.Equals(saved.Uuid)).FirstOrDefault();
			Expect.IsNotNull(check);
			Expect.AreEqual("Monkey", check.Name);
		}

		[UnitTest]
		public void ObjectRepositoryMetaPropertyVersionTest()
		{
			ObjectRepository repo = GetTestObjectRepository(nameof(ObjectRepositoryMetaPropertyVersionTest));
			repo.AddType(typeof(Parent));

			Parent p = new Parent { Name = "Test Parent" };
			Parent saved = repo.Save(p);
			Meta meta = repo.MetaProvider.GetMeta(saved);

			meta.Property("Name", "Monkey");
			meta.Property("Name", "Orangutan");
			meta.Property("Name", "Gorilla");

			MetaProperty nameProp = meta.Property("Name");
			OutLineFormat("Version Count: {0}", nameProp.Versions.Length);
			foreach (MetaPropertyVersionInfo version in nameProp.GetVersionInfos()) 
			{
				OutLineFormat("Hash: {0}", ConsoleColor.Cyan, version.Hash);
				OutLineFormat("LastWrite: {0}, {1}", ConsoleColor.DarkCyan, version.LastWrite.ToShortDateString(), version.LastWrite.ToLongTimeString());
				OutLineFormat("Name: {0}", ConsoleColor.Cyan, version.Name);
				OutLineFormat("Version: {0}", ConsoleColor.Cyan, version.Version);
				OutLineFormat("Value: {0}", ConsoleColor.Cyan, version.Value);
			}
		}

		[UnitTest]
		public void ObjectRepositoryCreateShouldSetId()
		{
			ObjectRepository repo = GetTestObjectRepository();
			repo.AddType(typeof(TestContainer));
			TestContainer toCreate = new TestContainer();
			Expect.AreEqual(0, toCreate.Id);
			string testName = "Test Name".RandomLetters(5);
			toCreate.Name = testName;
			toCreate.BirthDay = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
			toCreate = repo.Create(toCreate);
			Expect.IsGreaterThan(toCreate.Id, 0);
			OutLineFormat("{0} passed", ConsoleColor.Green, repo.GetType().Name);
		}

		[UnitTest]
		public void ObjectRepositoryShouldRetrieveById()
		{
			ObjectRepository repo = GetTestObjectRepository();
			
			TestContainer toCreate = new TestContainer();
			Expect.AreEqual(0, toCreate.Id);
			string testName = "Test Name".RandomLetters(5);
			toCreate.Name = testName;
			toCreate.BirthDay = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
			toCreate = repo.Create(toCreate);
			TestContainer check = repo.Retrieve<TestContainer>(toCreate.Id);
			Expect.AreEqual(toCreate.Name, check.Name);
			OutLineFormat("{0} passed", ConsoleColor.Green, repo.GetType().Name);
		}

		[UnitTest]
		public void ObjectReaderWriterQueryTest()
		{
			ObjectReaderWriter rw = new ObjectReaderWriter(".\\TESTOBJECTREADERWRITER");
			string random = "RandomString_".RandomLetters(8);
			Parent one = new Parent { Name = "{0}:: Parent One"._Format(random) };
			Parent two = new Parent { Name = "{0}::Parent Two"._Format(random) };
			Parent three = new Parent { Name = "not this one" };

			rw.Write(typeof(Parent), one);
			rw.Write(typeof(Parent), two);
			rw.Write(typeof(Parent), three);

			Parent[] parents = rw.Query<Parent>(p => p.Name.StartsWith(random));
			Expect.AreEqual(2, parents.Length);
			parents.Each(p =>
			{
				OutLine(p.Name);
			});
			rw.Delete(one);
			rw.Delete(two);
			rw.Delete(three);		
		}

		[UnitTest]
		public void ObjectReaderWriterQueryPropertyTest()
		{
			ObjectReaderWriter rw = new ObjectReaderWriter(".\\{0}"._Format(MethodBase.GetCurrentMethod().Name));
			string random = "RandomString_".RandomLetters(5);
			Parent one = new Parent { Name = "{0}:: Parent Name one"._Format(random) };
			Parent two = new Parent { Name = "{0}:: Parent Name two"._Format(random) };
			Parent three = new Parent { Name = "not this one" };
			rw.Write(typeof(Parent), one);
			rw.Write(typeof(Parent), two);
			Thread.Sleep(1500); // does a non blocking write of properties
			Parent[] parents = rw.QueryProperty<Parent>("Name", val => 
			{
				return ((string)val).StartsWith(random);
			});

			Expect.AreEqual(2, parents.Length);
			parents.Each(p =>
			{
				OutLine(p.Name);
			});

			rw.Delete(one);
			rw.Delete(two);
			rw.Delete(three);
		}

		[UnitTest]
		public void ObjectReaderWriterUpdateTest()
		{
			ObjectReaderWriter rw = new ObjectReaderWriter(".\\{0}"._Format(MethodBase.GetCurrentMethod().Name));
			string random = "RandomString_".RandomLetters(5);
			Parent one = new Parent { Name = "{0}:: Parent Name one"._Format(random) };			
			rw.Write(typeof(Parent), one);			

			Thread.Sleep(1500); // does a non blocking write of properties
			
			Parent[] parents = rw.QueryProperty<Parent>("Name", val =>
			{
				return ((string)val).StartsWith(random);
			});

			Expect.AreEqual(1, parents.Length);

			Parent check = parents.First();
			string uuid = check.Uuid;
			Parent parent = rw.Query<Parent>(p => p.Uuid.Equals(uuid)).FirstOrDefault();
			
			Expect.IsNotNull(parent);
			parent.Name = "Updated";
			rw.Write(typeof(Parent), parent);

			check = rw.Query<Parent>(p => p.Uuid.Equals(uuid)).FirstOrDefault();
			Expect.AreEqual("Updated", check.Name);

			rw.Delete(one);
		}

		[UnitTest]
		public void ObjectRepoSavingParentShouldSaveChildLists()
		{
			ObjectRepository repo = GetTestObjectRepository();
			House house = new House { Name = "TestHouse", Parents = new List<Parent> { new Parent { Name = "TestParent" } } };
			repo.Save(house);
			Thread.Sleep(1500); // ensure the background write thread has time to write the xrefs
			House retrieved = repo.Retrieve<House>(house.Id);
			Expect.AreEqual(1, retrieved.Parents.Count);
		}

		[UnitTest]
		public void ObjectRepoSavingParentShouldMakeChildListsQueryable()
		{
			ObjectRepository repo = GetTestObjectRepository(MethodBase.GetCurrentMethod().Name);
			repo.BlockOnChildWrites = true;
			string parentName = "TestParent";
			House house = new House { Name = "TestHouse", Parents = new List<Parent> { new Parent { Name = parentName } } };
			repo.Save(house);

			Parent check = repo.Query<Parent>(p => p.Name.Equals(parentName)).FirstOrDefault();
			Expect.IsNotNull(check);
			Expect.AreEqual(parentName, check.Name);
			Expect.IsGreaterThan(check.Id, 0);
		}

		public class NotSerializable
		{
			public long Id { get; set; }
			public string Name { get; set; }
			public bool BooleanProperty { get; set; }

			public DateTime BirthDate { get; set; }
		}
		[UnitTest]
		public void ShouldBeAbleToSaveAndQueryNonSerializable()
		{
			ObjectRepository repo = GetTestObjectRepository(MethodBase.GetCurrentMethod().Name);
			string name = "This is a the name";
			DateTime date = DateTime.Now.Subtract(TimeSpan.FromDays(365 * 5));
			NotSerializable value = new NotSerializable { Name = name, BooleanProperty = true, BirthDate = date };
			repo.Save(value);

			NotSerializable check = repo.Query<NotSerializable>((ns) => ns.BirthDate == date).FirstOrDefault();
			Expect.IsNotNull(check);
			Expect.AreEqual(name, check.Name);
			Expect.IsTrue(check.BooleanProperty);
			Expect.AreEqual(date, check.BirthDate);
		}
		
		[UnitTest]
		public void ObjectRepoSavingParentXrefShouldSetChildXref()
		{
			ObjectRepository repo = GetTestObjectRepository();

			House house = new House { Name = "TestHouse", Parents = new List<Parent> { new Parent { Name = "TestParent" } } };
			repo.Save(house);
			Thread.Sleep(1500); // ensure the background write thread has time to write the xrefs
			House retrieved = repo.Retrieve<House>(house.Id);
			Parent parent = repo.Retrieve<Parent>(retrieved.Parents[0].Id);
			Expect.AreEqual(1, parent.Houses.Length);
		}

		internal static ObjectRepository GetTestObjectRepository(string root = null)
		{
            ConsoleLogger logger = new ConsoleLogger();
            logger.UseColors = true;
            logger.AddDetails = false;
            logger.StartLoggingThread();

            ObjectRepository repo = string.IsNullOrEmpty(root) ? new ObjectRepository() : new ObjectRepository(root);
			repo.AddType(typeof(TestContainer));
			repo.Subscribe(logger);
			return repo;
		}

		private static ObjectRepository GetTestMongoObjectRepository(string root = null)
		{
			ObjectRepository repo = string.IsNullOrEmpty(root) ? new ObjectRepository() : new ObjectRepository(root);
			repo.AddType(typeof(TestContainer));
			repo.Subscribe(new ConsoleLogger());
			repo.ObjectReaderWriter = new MongoObjectReaderWriter();
			return repo;
		}
	}
}
