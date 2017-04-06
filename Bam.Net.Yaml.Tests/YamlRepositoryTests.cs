using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing;
using Bam.Net.Yaml.Data;
using Bam.Net.Yaml.Tests.TestClasses;

namespace Bam.Net.Yaml.Tests
{
    [Serializable]
    public class YamlRepositoryTests : CommandLineTestInterface
    {
        [UnitTest]
        public void YamlRepositoryCreateRetrieveTest()
        {
            Parent parent = new Parent { Details = "Some details" };
            House house = new House { HouseDetails = "house details" };
            parent.Houses = new List<House>();
            parent.Houses.Add(house);
            YamlRepository repo = GetYamlRepo(nameof(YamlRepositoryCreateRetrieveTest));
            parent = repo.Save(parent);

            Parent retrieved = repo.Retrieve<Parent>(parent.Id);
            Expect.IsNotNull(retrieved);
            Expect.IsNotNull(retrieved.Houses);
            Expect.AreEqual(1, retrieved.Houses.Count);
        }

        [UnitTest]
        public void YamlSyncToDbTest()
        {
            string details = 10.RandomLetters();
            string updatedDetails = 5.RandomLetters();
            string testName = 8.RandomLetters();
            Parent parent = new Parent { Details = details, Name = testName };
            YamlRepository repo = GetYamlRepo(nameof(YamlSyncToDbTest));
            parent = repo.Save(parent);

            Parent loaded = repo.LoadYaml<Parent>(testName).ToList().FirstOrDefault();
            Expect.IsNotNull(loaded);
            loaded.Details = updatedDetails;
            repo.YamlDataDirectory.Save<Parent>(loaded);

            FileInfo file = repo.YamlDataDirectory.GetYamlFile(typeof(Parent), testName);
            YamlDataFile data = new YamlDataFile(typeof(Parent), file);
            Parent fromFile = data.As<Parent>();
            Expect.AreEqual(testName, fromFile.Name);
            Expect.AreEqual(updatedDetails, fromFile.Details);
            Parent fromDb = repo.DaoRepository.Retrieve<Parent>(fromFile.Id);
            Expect.AreEqual(details, fromDb.Details);
            repo.Sync();
            fromDb = repo.DaoRepository.Retrieve<Parent>(fromFile.Id);
            Expect.AreEqual(testName, fromDb.Name);
            Expect.AreEqual(updatedDetails, fromDb.Details);
        }

        private YamlRepository GetYamlRepo(string name)
        {
            YamlRepository repo =  new YamlRepository($".\\{name}", new SQLiteDatabase($"{name}_Db", name));
            repo.AddType<Parent>();
            repo.AddType<House>();
            repo.AddType<Child>();
            return repo;
        }
    }
}
