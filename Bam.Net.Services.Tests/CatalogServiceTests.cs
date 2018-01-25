using Bam.Net.CoreServices;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Incubation;
using Bam.Net.Services.Catalog.Data;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class CatalogServiceTests: CommandLineTestInterface
    {
        static Incubator _serviceRegistry;
        [BeforeUnitTests]
        public void Setup()
        {
            DaoRepository repo = new DaoRepository();
            repo.AddNamespace(typeof(CatalogDefinition));
            _serviceRegistry = new ServiceRegistry()
                .For<DaoRepository>().Use(repo)
                .For<ICatalogService>().Use<CatalogService>();
        }

        [UnitTest("Catalog: Can Create")]
        public void CanCreateCatalog()
        {
            string catalogName = "Test Catalog Name ".RandomLetters(8);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            CatalogDefinition catalog = svc.CreateCatalog(catalogName);
            Expect.IsNotNull(catalog, "catalog was null");
            Expect.AreEqual(catalogName, catalog.Name);
            Expect.IsNotNullOrEmpty(catalog.Cuid, "cuid was not set");
        }

        [UnitTest("Catalog: find by name")]
        public void CanFindCatalogByName()
        {
            string catalogName = "Test Catalog To Find ".RandomLetters(10);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            svc.CreateCatalog(catalogName);
            CatalogDefinition catalog = svc.FindCatalog(catalogName);
            Expect.IsNotNull(catalog, "catalog was null");
            Expect.AreEqual(catalogName, catalog.Name);
        }

        [UnitTest("Catalog: Get by cuid")]
        public void CanGetCatalogByCuid()
        {
            string catalogName = "Test Catalog To Find ".RandomLetters(10);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            CatalogDefinition catalog = svc.CreateCatalog(catalogName);
            CatalogDefinition gotCatalog = svc.GetCatalog(catalog.Cuid);
            Expect.IsNotNull(gotCatalog, "catalog was null");
            Expect.AreEqual(catalogName, gotCatalog.Name);
        }

        [UnitTest("Catalog: rename")]
        public void CanRenameCatalog()
        {
            string catalogName = "Test Catalog To Find ".RandomLetters(10);
            string renamedName = "Renamed Test Catalog ".RandomLetters(10);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            CatalogDefinition catalog = svc.CreateCatalog(catalogName);
            Expect.AreEqual(catalogName, catalog.Name);
            svc.RenameCatalog(catalog.Cuid, renamedName);
            catalog = svc.GetCatalog(catalog.Cuid);
            Expect.AreEqual(renamedName, catalog.Name);
        }

        [UnitTest("Catalog: delete")]
        public void CanDeleteCatalog()
        {
            string catalogName = "Test Catalog To Find ".RandomLetters(10);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            CatalogDefinition catalog = svc.CreateCatalog(catalogName);
            CatalogDefinition gotCatalog = svc.GetCatalog(catalog.Cuid);
            Expect.IsNotNull(gotCatalog, "gotCatalog was null");
            Expect.AreEqual(catalog.Cuid, gotCatalog.Cuid);
            svc.DeleteCatalog(gotCatalog.Cuid);
            gotCatalog = svc.GetCatalog(catalog.Cuid);
            Expect.IsNull(gotCatalog);
        }

        [UnitTest("Catalog: create item")]
        public void CanCreateItem()
        {
            string itemName = "Test Item Name ".RandomLetters(4);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            ItemDefinition item = svc.CreateItem(itemName);
            Expect.IsNotNull(item);
            Expect.AreEqual(itemName, item.Name);
        }
        //ItemDefinition AddItem(string catalogCuid, string itemCuid);
        [UnitTest("Catalog: add item to catalog")]
        public void CanAddItemToCatalog()
        {
            string itemName = "Test AddItem ".RandomLetters(6);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            CatalogDefinition catalog = svc.CreateCatalog(5.RandomLetters());
            ItemDefinition item = svc.CreateItem(6.RandomLetters());
            svc.AddItem(catalog.Cuid, item.Cuid);
            catalog = svc.GetCatalog(catalog.Cuid);
            Expect.IsTrue(catalog.Items.Select(i=> i.Cuid.Equals(item.Cuid)).Count() == 1);
        }

        //bool RemoveItem(string catalogCuid, string itemCuid);
        [UnitTest("Catalog: remove item from catalog")]
        public void CanRemoveItemFromCatalog()
        {
            string itemName = "Test AddItem ".RandomLetters(6);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            CatalogDefinition catalog = svc.CreateCatalog(5.RandomLetters());
            ItemDefinition item = svc.CreateItem(6.RandomLetters());
            svc.AddItem(catalog.Cuid, item.Cuid);
            catalog = svc.GetCatalog(catalog.Cuid);
            List<ItemDefinition> items = catalog.Items.Where(i => i.Cuid.Equals(item.Cuid)).ToList();
            Expect.IsTrue(items.Count == 1, $"Expected 1 catalog item but there were {items.Count}");
            svc.RemoveItem(catalog.Cuid, item.Cuid);
            catalog = svc.GetCatalog(catalog.Cuid);
            items = catalog.Items.Where(i => i.Cuid.Equals(item.Cuid)).ToList();
            Expect.IsTrue(items.Count == 0, $"Expected 0 catalog items but there were {items.Count}");
        }
        //ItemDefinition RenameItem(string itemCuid, string name);
        [UnitTest("Catalog: rename item")]
        public void CanRenameItem()
        {
            string itemName = "Test RenameItem ".RandomLetters(8);
            string newName = "New Name ".RandomLetters(8);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            CatalogDefinition catalog = svc.CreateCatalog(5.RandomLetters());
            ItemDefinition item = svc.CreateItem(itemName);
            svc.AddItem(catalog.Cuid, item.Cuid);
            catalog = svc.GetCatalog(catalog.Cuid);
            List<ItemDefinition> namedItems = catalog.Items.Where(i => i.Name.Equals(itemName)).ToList();
            Expect.IsTrue(namedItems.Count == 1, $"expected 1 item in catalog but there were {namedItems.Count}");
            svc.RenameItem(item.Cuid, newName);
            catalog = svc.GetCatalog(catalog.Cuid);
            List<ItemDefinition> oldNamedItems = catalog.Items.Where(i => i.Name.Equals(itemName)).ToList();
            Expect.IsTrue(oldNamedItems.Count == 0);
            List<ItemDefinition> newNamedItems = catalog.Items.Where(i => i.Name.Equals(newName)).ToList();
            Expect.IsTrue(newNamedItems.Count == 1, $"Expected 1 new item but there were {newNamedItems.Count}");
        }
        //ItemDefinition GetItem(string itemCuid);
        [UnitTest("Catalog: get item")]
        public void CanGetItem()
        {
            string itemName = "Test RenameItem ".RandomLetters(8);
            string newName = "New Name ".RandomLetters(8);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            CatalogDefinition catalog = svc.CreateCatalog(5.RandomLetters());
            ItemDefinition item = svc.CreateItem(itemName);
            ItemDefinition gotItem = svc.GetItem(item.Cuid);
            Expect.IsNotNull(gotItem);
            Expect.AreEqual(item.Cuid, gotItem.Cuid);
            Expect.AreEqual(item.Name, gotItem.Name);
        }
        //bool DeleteItem(string itemCuid);
        [UnitTest("Catalog: delete item")]
        public void CanDeleteItem()
        {
            string itemName = "Test RenameItem ".RandomLetters(8);
            string newName = "New Name ".RandomLetters(8);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            CatalogDefinition catalog = svc.CreateCatalog(5.RandomLetters());
            ItemDefinition item = svc.CreateItem(itemName);
            svc.DeleteItem(item.Cuid);
            ItemDefinition shouldBeNull = svc.GetItem(item.Cuid);
            Expect.IsNull(shouldBeNull);
        }
        //string[] FindItemCatalogs(string itemCuid);
        [UnitTest("Catalog: find item catalogs")]
        public void ShouldFindItemCatalogs()
        {
            string itemName = "Test RenameItem ".RandomLetters(8);
            string newName = "New Name ".RandomLetters(8);
            CatalogService svc = _serviceRegistry.Get<CatalogService>();
            CatalogDefinition catalog = svc.CreateCatalog(5.RandomLetters());
            CatalogDefinition catalog2 = svc.CreateCatalog(6.RandomLetters());
            ItemDefinition item = svc.CreateItem(itemName);
            svc.AddItem(catalog.Cuid, item.Cuid);
            svc.AddItem(catalog2.Cuid, item.Cuid);
            string[] catalogCuids = svc.FindItemCatalogs(item.Cuid);
            Expect.AreEqual(2, catalogCuids.Length);
        }
        
    }
}
