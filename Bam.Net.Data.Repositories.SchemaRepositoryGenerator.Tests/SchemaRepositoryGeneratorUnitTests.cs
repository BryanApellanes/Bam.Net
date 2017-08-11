using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Repositories.Tests
{
    [Serializable]
    public class SchemaRepositoryGeneratorUnitTests: CommandLineTestInterface
    {
        [AfterUnitTests]
        public void Cleanup()
        {

        }

        [UnitTest]
        public void SaveAndQuerySchemaRepoTest()
        {
            TestDaoRepository testDaoRepo = new TestDaoRepository("TestDaoRepoSchema");

            string theName = "The Test Parent 1 ({0})"._Format(8.RandomLetters());
            ClrTypes.Parent parent = new ClrTypes.Parent { Name = theName };
            OutLine("Saving parent 1", ConsoleColor.Cyan);
            testDaoRepo.Save(parent);
            string theName2 = "The Test Parent 2 ({0})"._Format(8.RandomLetters());
            ClrTypes.Parent parent2 = new ClrTypes.Parent { Name = theName };
            OutLine("Saving parent 2", ConsoleColor.Cyan);
            testDaoRepo.Save(parent2);

            ClrTypes.Parent retrieved = testDaoRepo.ParentsWhere(c => c.Name == parent.Name).FirstOrDefault();
            Expect.IsNotNull(retrieved);
            Expect.AreEqual(parent.Name, retrieved.Name);
            Expect.AreEqual(theName, retrieved.Name);
        }

        [UnitTest]
        public void SchemaRepoTestGetOneWhere()
        {
            TestDaoRepository testDaoRepo = new TestDaoRepository("TestDaoRepoSchema");

            string theName = "The Test Parent 1 ({0})"._Format(8.RandomLetters());
            ClrTypes.Parent parent = new ClrTypes.Parent { Name = theName };
            OutLine("Saving parent 1", ConsoleColor.Cyan);
            testDaoRepo.Save(parent);

            ClrTypes.Parent retrieved = testDaoRepo.GetOneParentWhere(c => c.Name == parent.Name);
            Expect.IsNotNull(retrieved);
            Expect.AreEqual(theName, retrieved.Name);
        }

        [UnitTest]
        public void SchemaRepoTestOneWhere()
        {
            TestDaoRepository testDaoRepo = new TestDaoRepository("TestDaoRepoSchema");

            string theName = "The Test Parent 1 ({0})"._Format(8.RandomLetters());
            ClrTypes.Parent parent = new ClrTypes.Parent { Name = theName };
            OutLine("Saving parent 1", ConsoleColor.Cyan);
            testDaoRepo.Save(parent);

            ClrTypes.Parent retrieved = testDaoRepo.OneParentWhere(c => c.Name == parent.Name);
            Expect.IsNotNull(retrieved);
            Expect.AreEqual(theName, retrieved.Name);
        }

        [UnitTest]
        public void SchemaRepoTestTopWhere()
        {
            TestDaoRepository testDaoRepo = new TestDaoRepository("TestDaoRepoSchema");
            string startsWith = 4.RandomLetters();
            ClrTypes.Parent parent = GetClrParent();
            ClrTypes.Parent parent2 = GetClrParent();
            ClrTypes.Parent parent3 = GetClrParent();
            parent.Name = $"{startsWith}: {parent.Name}";
            parent2.Name = $"{startsWith}: {parent2.Name}";
            testDaoRepo.Save(parent);
            testDaoRepo.Save(parent2);
            testDaoRepo.Save(parent3);

            IEnumerable<ClrTypes.Parent> retrieved = testDaoRepo.TopParentsWhere(2, c => c.Name.StartsWith(startsWith));
            Expect.IsNotNull(retrieved);
            Expect.AreEqual(retrieved.Count(), 2);
            Expect.AreEqual(2, testDaoRepo.CountParentsWhere(c => c.Name.StartsWith(startsWith)));
        }

        [UnitTest]
        public async void SchemaRepoBatchQueryTest()
        {
            TestDaoRepository testDaoRepo = new TestDaoRepository("TestDaoRepoSchema");
            string startsWith = 4.RandomLetters();
            ClrTypes.Parent parent = GetClrParent();
            ClrTypes.Parent parent2 = GetClrParent();
            ClrTypes.Parent parent3 = GetClrParent();
            ClrTypes.Parent parent4 = GetClrParent();
            ClrTypes.Parent parent5 = GetClrParent();
            parent.Name = $"{startsWith}: {parent.Name}";
            parent2.Name = $"{startsWith}: {parent2.Name}";
            parent3.Name = $"{startsWith}: {parent3.Name}";
            parent4.Name = $"{startsWith}: {parent4.Name}";

            testDaoRepo.Save(parent);
            testDaoRepo.Save(parent2);
            testDaoRepo.Save(parent3);

            int? batchCount = 0;
            await testDaoRepo.BatchQueryParents(2, c => c.Name.StartsWith(startsWith), (batch) =>
            {
                batchCount++;
                batch.Each(p => OutLineFormat(p.PropertiesToString()));
            });
            int? batchAll = 0;
            await testDaoRepo.BatchAllParents(2, (batch) =>
            {
                batchAll++;
            });
            Expect.IsGreaterThan(batchAll.Value, 0);
            Expect.AreEqual(2, batchCount.Value);
            OutLineFormat("Parent count: {0}", ConsoleColor.Blue, testDaoRepo.CountParents());
        }

        [ConsoleAction("test schema repo generation to c:\\temp\\SchemaRepoTest")]
        public void GenerateSchemaRepo()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.StartLoggingThread();
            SchemaRepositoryGenerator generator = new SchemaRepositoryGenerator(Assembly.GetExecutingAssembly(), "Bam.Net.Data.Repositories.Tests.ClrTypes", logger);
            generator.GenerateRepositorySource("c:\\temp\\SchemaRepoTest", "TestDaoSchema");
        }

        [ConsoleAction("test schema repo assembly generation")]
        public void GenerateSchemaRepoAssembly()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.StartLoggingThread();
            SchemaRepositoryGenerator generator = new SchemaRepositoryGenerator(Assembly.GetExecutingAssembly(), "Bam.Net.Data.Repositories.Tests.ClrTypes", logger);
            generator.SchemaName = "TestDao";
            Assembly ass = generator.GetDaoAssembly(true);
            OutLineFormat("Assembly is at: {0}", ass.GetFilePath());
        }
        
        private static ClrTypes.Parent GetClrParent()
        {
            string theName = "The Test Parent 1 ({0})"._Format(8.RandomLetters());
            return new ClrTypes.Parent { Name = theName };
        }


    }
}
