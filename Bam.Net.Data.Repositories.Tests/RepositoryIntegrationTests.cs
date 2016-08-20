using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing.Integration;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Data.Repositories.Tests.TestDtos;
using System.IO;
using Bam.Net.Data.Tests.Integration;
//using Bam.Net.Schema.Org;

namespace Bam.Net.Data.Repositories.Tests
{
    [Serializable]
    [IntegrationTestContainer]
    public class RepositoryIntegrationTests: CommandLineTestInterface
    {
        HashSet<IRepository> _repos;
        const string RootDir = ".\\TestRepositories";
        [IntegrationTestSetup]
        public void Setup()
        {
            _repos = new HashSet<IRepository>();

            DaoRepository daoRepo = new DaoRepository(new SQLiteDatabase(RootDir, "DaoRepoTest"));
            AddTypes(daoRepo);
            _repos.Add(daoRepo);

            ObjectRepository objRepo = new ObjectRepository(Path.Combine(RootDir, "ObjectRepo"));
            AddTypes(objRepo);
            _repos.Add(objRepo);

            DatabaseRepository dbRepo = new DatabaseRepository(new SQLiteDatabase(RootDir, "DbRepoTest"));
            AddTypes(dbRepo);
            _repos.Add(dbRepo);
        }

        [IntegrationTest]
        public void RepositoryCRUDTest()
        {
            string testName = nameof(RepositoryCRUDTest);
            _repos.Each(testName, (tn, repo) =>
            {
                StartMessage(tn, repo);
                string name = 5.RandomLetters();
                Parent p = new Parent();
                p.Name = name;

                p = repo.Save(p);
                Expect.IsGreaterThan(p.Id, 0, "Id wasn't set");

                Parent retrieved = repo.Retrieve<Parent>(p.Id);
                Expect.AreEqual(name, p.Name);
                Expect.AreEqual(name, retrieved.Name);
                Expect.AreEqual(p.Id, retrieved.Id);
                Expect.AreEqual(p.Uuid, retrieved.Uuid);

                string newName = 8.RandomLetters();
                retrieved.Name = newName;
                repo.Save(retrieved);

                Parent again = repo.Retrieve<Parent>(p.Uuid);
                Expect.AreEqual(newName, again.Name);

                repo.Delete(again);

                Parent shouldBeNull = repo.Retrieve<Parent>(p.Id);
                Expect.IsNull(shouldBeNull);

                EndMessage(tn);
            });
        }

        [IntegrationTest]
        public void RepositoryChildListTest()
        {
            string testName = nameof(RepositoryChildListTest);
            _repos.Each(testName, (tn, repo) =>
            {
                StartMessage(tn, repo);

                string s1 = 4.RandomLetters();
                string s2 = 4.RandomLetters();                
                string d1 = 5.RandomLetters();
                string d2 = 5.RandomLetters();
                string d3 = 5.RandomLetters();

                Parent parent = new Parent { Name = 6.RandomLetters(), Sons = new List<Son>() };
                parent.Sons.Add(new Son { Name = s1 });
                parent.Sons.Add(new Son { Name = s2 });
                List<Daughter> daughters = new List<Daughter>();
                daughters.Add(new Daughter { Name = d1 });
                daughters.Add(new Daughter { Name = d2 });
                daughters.Add(new Daughter { Name = d3 });
                parent.Daughters = daughters.ToArray();

                repo.Save(parent);

                Parent retrieved = repo.Retrieve<Parent>(parent.Id);
                Expect.AreEqual(3, retrieved.Daughters.Length);
                Expect.AreEqual(2, retrieved.Sons.Count);
                Expect.IsTrue(retrieved.Sons.Any(s => s.Name.Equals(s1)));
                Expect.IsTrue(retrieved.Sons.Any(s => s.Name.Equals(s2)));
                Expect.IsTrue(retrieved.Daughters.Any(d => d.Name.Equals(d1)));
                Expect.IsTrue(retrieved.Daughters.Any(d => d.Name.Equals(d2)));
                Expect.IsTrue(retrieved.Daughters.Any(d => d.Name.Equals(d3)));

                EndMessage(tn);
            });
        }

        [IntegrationTest]
        public void RepositoryXrefListTest()
        {
            string testName = nameof(RepositoryXrefListTest);
            _repos.Each(testName, (tn, repo) =>
            {
                StartMessage(tn, repo);

                House[] houses = 10.Times(i => new House { Name = "House " + i, Parents = new List<Parent>() });
                List<House> savedHouses = new List<House>();
                10.Times(i =>
                {
                    savedHouses.Add(repo.Save(new House { Name = $"House {i} ".RandomLetters(4) }));
                });
                List<Parent> savedParents = new List<Parent>();
                3.Times(i =>
                {
                    savedParents.Add(repo.Save(new Parent { Name = $"Parent {i} ".RandomLetters(4) }));
                });
                savedParents.Each(parent =>
                {
                    parent.Houses = savedHouses.ToArray();
                    repo.Save(parent);                    
                });
                savedParents.Each(parent =>
                {
                    House[] retrievedHouses = repo.Retrieve<Parent>(parent.Id).Houses;
                    Expect.AreEqual(10, retrievedHouses.Length);
                    retrievedHouses.Each(h =>
                    {
                        House house = repo.Retrieve<House>(h.Id);
                        Expect.AreEqual(3, house.Parents.Count);
                    });
                });
                EndMessage(tn);
            });
        }

        //[ConsoleAction]
        //public void Testing()
        //{
        //    SaveOfInheritingTypeTest();
        //}



        private void AddTypes(IRepository repo)
        {
            repo.AddType<Parent>();
            repo.AddType<House>();
            repo.AddType<Son>();
            repo.AddType<Daughter>();
        }

        private void StartMessage(dynamic testName, IRepository repo, ConsoleColor color = ConsoleColor.Cyan)
        {
            OutLineFormat("Starting {0} with repo of type {1}", color, testName, repo.GetType().Name);
        }

        private void EndMessage(dynamic testName, ConsoleColor color = ConsoleColor.DarkCyan)
        {
            OutLineFormat("{0} complete", color, testName);
        }

    }
}
