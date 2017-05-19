using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Services.CatalogService;
using Bam.Net.Services.CatalogService.Data;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;

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
        [IntegrationTest("ListService integration test (incomplete)")]
        public void ConsoleInvokableIntegrationTest()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            CoreClient client = new CoreClient("TestOr", "TestApp", "localhost", 9100, logger);
            CatalogService.CatalogService svc = client.GetProxy<CatalogService.CatalogService>();
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
    }
}
