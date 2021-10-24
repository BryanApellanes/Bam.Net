using System;
using System.Collections.Generic;
using System.Linq;
using Bam.Net.CommandLine;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.Services.Catalog.Data;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using NSubstitute;

namespace Bam.Net.Services.Tests
{
    public class CatalogTests : CommandLineTool
    {
        [UnitTest]
        [TestGroup("Catalog")]
        public void CanSaveCatalog()
        {
            DaoRepository repo = new DaoRepository();
            repo.AddType<CatalogDefinition>();
            Organization org = Organization.Public;
            string catalogName = "MyTestCatalog";
            CatalogDefinition catalogDefinition = new CatalogDefinition(){OrganizationKey = org.Key, Name = catalogName};
            CatalogDefinition saved = catalogDefinition.SaveByKey<CatalogDefinition>(repo);
            
            Expect.AreEqual(saved.Key, catalogDefinition.Key);
            Expect.AreEqual(saved, catalogDefinition);
        }

        [UnitTest]
        [TestGroup("Catalog")]
        public void CanCreateCatalog()
        {
            CatalogService svc = GetTestCatalogService(nameof(CanCreateCatalog), out DaoRepository ignore);

            string testCatalogName = "TestCatalog";
            CatalogDefinition catalogDefinition = svc.CreateCatalog(testCatalogName);
            Expect.IsNotNull(catalogDefinition);
            Expect.IsGreaterThan(catalogDefinition.Id, 0);
            Expect.IsGreaterThan(catalogDefinition.Key, 0);
        }

        [UnitTest]
        [TestGroup("Catalog")]
        public void KeysCalculateCorrectlyForValues()
        {
            OutLine(new CatalogDefinition().ToJson(true));
            
            CatalogService svc = GetTestCatalogService(nameof(CanFindCatalog), out DaoRepository daoRepository);
            string testCatalogName = $"{nameof(CanFindCatalog)}_TestCatalog";
            CatalogDefinition catalogDefinition = new CatalogDefinition()
            {
                Name = testCatalogName, OrganizationKey = svc.ServerOrganization.Key,
                ApplicationKey = svc.ServerApplication.Key
            };
            CatalogDefinition catalogDefinition2 = new CatalogDefinition()
            {
                Name = testCatalogName, OrganizationKey = svc.ServerOrganization.Key,
                ApplicationKey = svc.ServerApplication.Key
            }; 
            
            Expect.AreEqual(catalogDefinition.Key, catalogDefinition2.Key);
            Message.PrintLine(catalogDefinition2.Key.ToString(), ConsoleColor.Yellow);
        }

        [UnitTest]
        [TestGroup("Catalog")]
        public void CanSaveByKey()
        {
            CatalogService svc = GetTestCatalogService(nameof(CanSaveByKey), out DaoRepository daoRepository);
            string testCatalogName = $"{nameof(CanFindCatalog)}_TestCatalog";
            CatalogDefinition catalogDefinition = new CatalogDefinition()
            {
                Name = testCatalogName, OrganizationKey = svc.ServerOrganization.Key,
                ApplicationKey = svc.ServerApplication.Key
            };
            ulong key = catalogDefinition.Key;
            CatalogDefinition saved = catalogDefinition.SaveByKey<CatalogDefinition>(daoRepository);
            Expect.IsNotNull(saved);
            Expect.AreEqual(key, saved.Key);
        }
        
        [UnitTest]
        [TestGroup("Catalog")]
        public void CanLoadByKey()
        {
            CatalogService svc = GetTestCatalogService(nameof(CanLoadByKey), out DaoRepository daoRepository);
            string testCatalogName = $"{nameof(CanLoadByKey)}_TestCatalog";
            CatalogDefinition catalogDefinition = new CatalogDefinition()
            {
                Name = testCatalogName, OrganizationKey = svc.ServerOrganization.Key,
                ApplicationKey = svc.ServerApplication.Key
            };
            ulong key = catalogDefinition.Key;
            CatalogDefinition saved = catalogDefinition.SaveByKey<CatalogDefinition>(daoRepository);

            CatalogDefinition copied = catalogDefinition.CopyAs<CatalogDefinition>();

            CatalogDefinition loaded = copied.LoadByKey<CatalogDefinition>(daoRepository);
            Expect.AreEqual(copied, loaded);
            Expect.AreEqual(copied.Name, loaded.Name);
            Expect.AreEqual(loaded.Name, testCatalogName);
            Expect.AreEqual(copied.Key, loaded.Key);
            Expect.IsGreaterThan(loaded.Key, 0);
        }

        [UnitTest]
        [TestGroup("Catalog")]
        public void CanRenameCatalog()
        {
            CatalogService svc = GetTestCatalogService(nameof(CanRenameCatalog), out DaoRepository daoRepository);
            string testCatalogName = $"{nameof(CanRenameCatalog)}_TestCatalog";
            string renamedName = $"{testCatalogName}_Renamed";
            CatalogDefinition catalogDefinition = svc.CreateCatalog(testCatalogName);
            Expect.AreEqual(testCatalogName, catalogDefinition.Name);
            CatalogDefinition renamed = svc.RenameCatalog(catalogDefinition, renamedName);
            Expect.AreEqual(renamedName, renamed.Name);
            CatalogDefinition retrieved = svc.FindCatalog(renamedName);
            Expect.AreEqual(retrieved, renamed);
        }
        
        [UnitTest]
        [TestGroup("Catalog")]
        public void CanFindCatalog()
        {
            string testCatalogName = $"{nameof(CanFindCatalog)}_TestCatalog";
            
            CatalogService svc = GetTestCatalogService(testCatalogName, nameof(CanFindCatalog));
            CatalogDefinition retrieved = svc.FindCatalog(testCatalogName);
            Expect.IsNotNull(retrieved);
            Expect.AreEqual(testCatalogName, retrieved.Name);
            Expect.AreEqual(svc.ServerOrganization.Key, retrieved.OrganizationKey);
            Expect.AreEqual(svc.ServerApplication.Key, retrieved.ApplicationKey);
        }

        [UnitTest]
        [TestGroup("Catalog")]
        public void CanAddAndGetItem()
        {
            string catalogName = $"{nameof(CanAddAndGetItem)}_TestCatalog";
            string testItemName = $"{nameof(CanAddAndGetItem)}_TestItem";
            CatalogService svc = GetTestCatalogService(catalogName, nameof(CanAddAndGetItem));
            ItemDefinition item = svc.AddItem(svc.FindCatalog(catalogName), testItemName);

            ItemDefinition retrieved = svc.GetItem(item);
            
            Expect.AreEqual(retrieved, item);
            Expect.AreEqual(retrieved.Name, item.Name);
            Expect.AreEqual(testItemName, retrieved.Name);
        }

        [UnitTest]
        [TestGroup("Catalog")]
        public void CanGetCatalogItems()
        {
            string catalogName = $"{nameof(CanGetCatalogItems)}_TestCatalog";
            string testItemName = $"{nameof(CanGetCatalogItems)}_TestItem";
            string testItemName2 = $"{nameof(CanGetCatalogItems)}_TestItem2";
            CatalogService svc = GetTestCatalogService(catalogName, nameof(CanGetCatalogItems));
            CatalogDefinition catalogDefinition = svc.FindCatalog(catalogName);
            ItemDefinition itemOne = svc.AddItem(catalogDefinition, testItemName, out CatalogItem xref);
            Expect.IsNotNull(xref);
            ItemDefinition itemTwo = svc.AddItem(catalogDefinition, testItemName2, out CatalogItem xref2);
            Expect.IsNotNull(xref2);

            CatalogDefinition retrievedCatalog = svc.GetCatalog(catalogDefinition);
            Expect.AreEqual(2, retrievedCatalog.Items.Count);
            retrievedCatalog.Items.Contains(itemOne).IsTrue();
            retrievedCatalog.Items.Contains(itemTwo).IsTrue();
        }

        [UnitTest]
        [TestGroup("Catalog")]
        public void CanDeleteCatalog()
        {
            string testCatalogName = nameof(CanDeleteCatalog);
            CatalogService svc = GetTestCatalogService(testCatalogName);
            CatalogDefinition catalogDefinition = svc.FindCatalog(testCatalogName);
            Expect.IsNotNull(catalogDefinition);

            svc.DeleteCatalog(catalogDefinition);

            catalogDefinition = svc.FindCatalog(testCatalogName);
            
            Expect.IsNull(catalogDefinition);
        }

        [UnitTest]
        [TestGroup("Catalog")]
        public void CanCreateAndGetItem()
        {
            string testCatalogName = nameof(CanCreateAndGetItem);
            string testItemName = "testItem_".RandomLetters(6);
            CatalogService svc = GetTestCatalogService(testCatalogName);
            ItemDefinition itemDefinition = svc.CreateItem(testItemName);
            Expect.AreEqual(testItemName, itemDefinition.Name);
            Expect.IsNotNullOrEmpty(itemDefinition.Uuid);
            Expect.IsNotNullOrEmpty(itemDefinition.Cuid);
            ItemDefinition retrieved = svc.GetItem(itemDefinition);
            
            Expect.AreEqual(itemDefinition, retrieved);
        }

        [UnitTest]
        [TestGroup("Catalog")]
        public void CanAddItemProperties()
        {
            string testCatalogName = nameof(CanAddItemProperties);
            string testItemName = "testItem_".RandomLetters(6);
            CatalogService svc = GetTestCatalogService(testCatalogName);

            CatalogDefinition catalogDefinition = svc.FindCatalog(testCatalogName);
            ItemDefinition item = svc.AddItem(catalogDefinition, testItemName);
            List<ItemProperty> properties = svc.AddItemProperties(item, new {TailCount = 5, FallopianTubes = true}).ToList();
            Expect.AreEqual(2, properties.Count);
            Expect.AreEqual("5", properties.First(p=> p.Name.Equals("TailCount")).Value);
            Expect.AreEqual("True", properties.First(p=> p.Name.Equals("FallopianTubes")).Value);
        }

        [UnitTest]
        [TestGroup("Catalog")]
        public void CanAddItemToCatalog()
        {
            string testCatalogName = nameof(CanAddItemToCatalog);
            string testItemName = "testItem_".RandomLetters(6);
            CatalogService svc = GetTestCatalogService(testCatalogName);

            CatalogDefinition catalogDefinition = svc.FindCatalog(testCatalogName);
            ItemDefinition item = svc.AddItem(catalogDefinition, testItemName);
            catalogDefinition = svc.GetCatalog(catalogDefinition.Key);
            Expect.AreEqual(1, catalogDefinition.Items.Count);
            Expect.AreEqual(testItemName, catalogDefinition.Items[0].Name);
        }

        [UnitTest]
        [TestGroup("Catalog")]
        [TestGroup("AdHoc")]
        public void CanRemoveItemFromCatalogWithoutDeletingItem()
        {
            string testCatalogName = nameof(CanRemoveItemFromCatalogWithoutDeletingItem);
            string testItemName = "testItem_".RandomLetters(4);
            CatalogService svc = GetTestCatalogService(testCatalogName);

            CatalogDefinition catalogDefinition = svc.FindCatalog(testCatalogName);
            ItemDefinition item = svc.AddItem(catalogDefinition, testItemName);
            catalogDefinition = svc.GetCatalog(catalogDefinition);
            Expect.AreEqual(1, catalogDefinition.Items.Count);
            svc.RemoveItem(catalogDefinition, item);
            catalogDefinition = svc.GetCatalog(catalogDefinition);
            Expect.AreEqual(0, catalogDefinition.Items.Count);

            item = svc.GetItem(item);
            Expect.IsNotNull(item);
        }
        
        [UnitTest]
        [TestGroup("Catalog")]
        public void SaveByKeySavesChanges()
        {
            CatalogService svc = GetTestCatalogService(nameof(SaveByKeySavesChanges), out DaoRepository daoRepository);
            string name = "part of the composite key so it must not change";
            string originalValue = "original value";
            string updatedValue = "updated value";
            ItemDefinition itemDefinition = new ItemDefinition() {Name = name, Description = originalValue};
            itemDefinition = itemDefinition.SaveByKey<ItemDefinition>(daoRepository);
            Expect.AreEqual(originalValue, itemDefinition.Description);
            Expect.AreEqual(name, itemDefinition.Name);

            itemDefinition.Description = updatedValue;
            itemDefinition = itemDefinition.SaveByKey<ItemDefinition>(daoRepository);
            
            Expect.AreEqual(updatedValue, itemDefinition.Description);
            Expect.AreEqual(name, itemDefinition.Name);
        }
        
        private CatalogService GetTestCatalogService(string databaseName, out DaoRepository daoRepository)
        {
            Database testDatabase = DataProvider.Current.GetAppDatabase(databaseName);
            Log.Default = new ConsoleLogger();
            daoRepository = new DaoRepository(testDatabase, Log.Default);
            IRepository catalogRepo = daoRepository;
            ApplicationRegistrationRepository applicationRegistrationRepository = new ApplicationRegistrationRepository();
            AsyncCallbackService asyncCallbackService = Substitute.For<AsyncCallbackService>();
            CatalogService svc = new CatalogService(catalogRepo, asyncCallbackService, daoRepository, new AppConf())
            {
                ApplicationRegistrationRepository = applicationRegistrationRepository
            };
            
            daoRepository.DeleteAll<CatalogDefinition>();
            daoRepository.DeleteAll<CatalogItem>();
            daoRepository.DeleteAll<ItemDefinition>();
            daoRepository.DeleteAll<ItemOption>();
            daoRepository.DeleteAll<ItemProperty>();
            return svc;
        }
        
        private CatalogService GetTestCatalogService(string testCatalogToCreate, string databaseName = "testDatabaseName")
        {
            CatalogService svc = GetTestCatalogService(databaseName, out DaoRepository daoRepository);
            CatalogDefinition catalogDefinition = new CatalogDefinition()
            {
                Name = testCatalogToCreate, OrganizationKey = svc.ServerOrganization.Key,
                ApplicationKey = svc.ServerApplication.Key
            };
            catalogDefinition.SaveByKey<CatalogDefinition>(daoRepository);
            return svc;
        }
    }
}