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
using Bam.Net.Testing.Unit;

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
            repo.Synchronize();
            fromDb = repo.DaoRepository.Retrieve<Parent>(fromFile.Id);
            Expect.AreEqual(testName, fromDb.Name);
            Expect.AreEqual(updatedDetails, fromDb.Details);
        }

        [UnitTest]
        public void CanAddNameToLoad()
        {
            YamlRepository repo = GetYamlRepo(nameof(CanAddNameToLoad));
            FileInfo loadNamesFile = repo.GetLoadNamesFile(typeof(Parent));
            if (File.Exists(loadNamesFile.FullName))
            {
                File.Delete(loadNamesFile.FullName);
            }
            Expect.IsFalse(File.Exists(loadNamesFile.FullName));
            HashSet<string> names = repo.AddNameToLoad<Parent>("monkey");
            Expect.IsTrue(File.Exists(loadNamesFile.FullName));
            names = repo.AddNameToLoad<Parent>("monkey");
            names = repo.AddNameToLoad<Parent>("banana");
            Expect.AreEqual(2, names.Count);
            List<string> fromFile = File.ReadAllLines(loadNamesFile.FullName).ToList();
            Expect.AreEqual(2, fromFile.Count);
            Expect.IsTrue(fromFile.Contains("monkey"));
            Expect.IsTrue(fromFile.Contains("banana"));
        }

        [UnitTest]
        public void LoadYamlTest()
        {
            YamlRepository repo = GetYamlRepo(nameof(LoadYamlTest));
            // save some stuff to daorepo
            string details = 10.RandomLetters();
            string testName = 8.RandomLetters();
            Parent parent = new Parent { Details = details, Name = testName };
            parent = repo.DaoRepository.Save(parent);
            FileInfo yamlFile = repo.GetYamlFile(typeof(Parent), parent.Name);
            string yamlFilePath = repo.YamlDataDirectory.GetYamlFilePath(typeof(Parent), parent.Name);
            Expect.IsFalse(File.Exists(yamlFilePath), $"file existed but shouldn't have: {yamlFilePath}");
            Expect.IsNull(yamlFile);
            FileInfo loadNamesFile = repo.GetLoadNamesFile(typeof(Parent));
            // write names to sync file
            repo.AddNameToLoad<Parent>(parent.Name);
            // Call LoadYaml
            repo.WriteYaml();
            // expect yaml files to exist
            yamlFile = repo.GetYamlFile(typeof(Parent), parent.Name);
            Expect.IsNotNull(yamlFile);
            Parent fromYaml = repo.ReadYaml<Parent>(parent.Name);
            Expect.AreEqual(fromYaml.Cuid, parent.Cuid);
            Expect.AreEqual(fromYaml.Name, parent.Name);
            Expect.AreEqual(fromYaml.Uuid, parent.Uuid);
        }
        [UnitTest]
        public void YamlSyncFromDbTest()
        {
            YamlRepository repo = GetYamlRepo(nameof(LoadYamlTest));
            string details = 10.RandomLetters();
            string updatedDetails = 15.RandomLetters();
            string testName = 8.RandomLetters();
            Parent parent = new Parent { Details = details, Name = testName };
            parent = repo.DaoRepository.Save(parent);
            repo.AddNameToLoad<Parent>(parent.Name);
            repo.WriteYaml();
            Parent fromFile = repo.ReadYaml<Parent>(parent.Name);
            Expect.AreEqual(fromFile.Details, parent.Details);
            parent.Details = updatedDetails;
            parent = repo.DaoRepository.Save(parent);
            repo.Synchronize();
            fromFile = repo.ReadYaml<Parent>(parent.Name);
            Expect.AreEqual(fromFile.Details, updatedDetails);
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
