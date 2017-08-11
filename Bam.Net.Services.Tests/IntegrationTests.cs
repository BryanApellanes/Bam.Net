using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Caching;
using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.Services.Catalog;
using Bam.Net.Services.Catalog.Data;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;
using Bam.Net.Services.Clients;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    [IntegrationTestContainer]
    public class IntegrationTests : CommandLineTestInterface
    {
        [IntegrationTestSetup]
        public void Setup()
        {
            
        }

        [IntegrationTestCleanup]
        public void CleanUp()
        {

        }

        [ConsoleAction]
        [IntegrationTest("CatalogService integration test (incomplete)")]
        public void ConsoleInvokableIntegrationTest()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            CoreClient client = new CoreClient("TestOr", "TestApp", "localhost", 9100, logger);
            CatalogService svc = client.GetProxy<CatalogService>();
            CatalogDefinition list = svc.CreateCatalog("test list");
            //ListDefinition CreateList(string name);
            //ItemDefinition CreateItem(string name);
            //ListDefinition AddItem(string listCuid, string itemCuid);
            //bool RemoveItem(string listCuid, string itemCuid);
            //ListDefinition GetList(string listCuid);
            //ListDefinition FindList(string name);
            //ListDefinition RenameList(string listCuid, string name);
            //ItemDefinition RenameItem(string itemCuid, string name);
            //bool DeleteList(string listCuid);
            //bool DeleteItem(string itemCuid);

        }

        [UnitTest]        
        public void GetHashThrowsIfNoKeyProperties()
        {
            Expect.Throws(() => new KeyHashRepoThrowsData().GetHashCode(), (ex) => OutLineFormat("Exception thrown as expected: {0}", ConsoleColor.Green, ex.Message));
            Expect.Throws(() => new KeyHashRepoThrowsData().GetLongKeyHash(), (ex) => OutLineFormat("Exception thrown as expected: {0}", ConsoleColor.Green, ex.Message));
        }

        [UnitTest]
        public void GetHashReturnsSameValueForDifferentInstances()
        {
            string name = 16.RandomLetters();
            string otherProp = 32.RandomLetters();
            KeyHashRepoTestData one = new KeyHashRepoTestData { Name = name, SomeOtherUniqueProperty = otherProp };
            KeyHashRepoTestData two = new KeyHashRepoTestData { Name = name, SomeOtherUniqueProperty = otherProp };
            KeyHashRepoTestData three = new KeyHashRepoTestData { Name = name, SomeOtherUniqueProperty = "different" };
            Expect.IsFalse(one == two);
            Expect.AreEqual(one.GetHashCode(), two.GetHashCode());
            Expect.AreEqual(one.GetLongKeyHash(), two.GetLongKeyHash());

            Expect.IsFalse(one.GetHashCode().Equals(three.GetHashCode()));
            Expect.IsFalse(two.GetHashCode().Equals(three.GetHashCode()));

            OutLine(one.GetLongKeyHash().ToString());
        }


    }
}
