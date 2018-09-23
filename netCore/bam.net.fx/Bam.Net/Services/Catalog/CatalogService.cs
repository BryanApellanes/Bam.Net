using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using Bam.Net.Services.Catalog.Data;

namespace Bam.Net.Services
{
    [Proxy("catalogSvc")]
    public class CatalogService : AsyncProxyableService, ICatalogService
    {
        protected CatalogService() { }
        public CatalogService(IRepository catalogRepo, AsyncCallbackService callbackService, DaoRepository repo, AppConf conf) : base(callbackService, repo, conf)
        {
            Repository = catalogRepo;
            DaoRepository.WarningsAsErrors = false;
            DaoRepository.AddReferenceAssemblies(typeof(CoreExtensions).Assembly);
        }

        public override object Clone()
        {
            CatalogService svc = new CatalogService(Repository, CallbackService, DaoRepository, AppConf);
            svc.CopyProperties(this);
            svc.CopyEventHandlers(this);
            return svc;
        }

        public CatalogDefinition CreateCatalog(string name)
        {
            CatalogDefinition catalog = new CatalogDefinition { Name = name };
            return Repository.Save(catalog);
        }

        public CatalogDefinition FindCatalog(string name)
        {
            CatalogDefinition catalog = Repository.Query<CatalogDefinition>(new { Name = name }).FirstOrDefault();
            return Repository.Retrieve<CatalogDefinition>(catalog.Uuid);
        }

        public CatalogDefinition RenameCatalog(string catalogCuid, string name)
        {
            CatalogDefinition catalog = Repository.Query<CatalogDefinition>(new { Cuid = catalogCuid }).FirstOrDefault();
            Args.ThrowIf(catalog == null, "Catalog ({0}) was not found", catalogCuid);
            catalog.Name = name;
            return Repository.Save(catalog);
        }

        public CatalogDefinition GetCatalog(string catalogCuid)
        {
            CatalogDefinition catalog = Repository.Query<CatalogDefinition>(new { Cuid = catalogCuid }).FirstOrDefault();
            if(catalog != null)
            {
                catalog = Repository.Retrieve<CatalogDefinition>(catalog.Uuid);
                catalog.Items = GetCatalogItems(catalog.Cuid).ToList();
                return catalog;
            }
            return null;
        }

        protected IEnumerable<ItemDefinition> GetCatalogItems(string catalogCuid)
        {
            foreach(CatalogItem ci in Repository.Query<CatalogItem>(new { CatalogCuid = catalogCuid }))
            {
                ItemDefinition item = Repository.Query<ItemDefinition>(new { Cuid = ci.ItemCuid }).FirstOrDefault();
                if(item != null)
                {
                    ItemDefinition result = Repository.Retrieve<ItemDefinition>(item.Uuid);
                    result.Properties = GetItemProperties(result.Cuid).ToList();
                    yield return result;
                }
            }
        }

        protected IEnumerable<ItemProperty> GetItemProperties(string itemCuid)
        {
            return Repository.Query<ItemProperty>(new { ItemDefinitionCuid = itemCuid });
        }

        public bool DeleteCatalog(string catalogCuid)
        {
            return Repository.DeleteWhere(typeof(CatalogDefinition), new { Cuid = catalogCuid });
        }

        public ItemDefinition CreateItem(string name)
        {
            return Repository.Create(new ItemDefinition { Name = name });
        }

        public ItemDefinition AddItem(string catalogCuid, string itemCuid)
        {
            CatalogDefinition catalog = GetCatalog(catalogCuid);
            Args.ThrowIf(catalog == null, "Catalog not found ({0})", catalogCuid);
            ItemDefinition item = GetItem(itemCuid);
            Args.ThrowIf(item == null, "Item not found ({0})", itemCuid);
            CatalogItem xref = new CatalogItem { CatalogCuid = catalogCuid, ItemCuid = itemCuid };
            Repository.Save(xref);
            return item;
        }

        public bool RemoveItem(string catalogCuid, string itemCuid)
        {
            try
            {
                CatalogItem xref = Repository.Query<CatalogItem>(new { CatalogCuid = catalogCuid, ItemCuid = itemCuid }).FirstOrDefault();
                if(xref != null)
                {
                    Repository.Delete(xref);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error removing item ({0}) from catalog ({1})", ex, itemCuid, catalogCuid);
                return false;
            }
        }

        public ItemDefinition RenameItem(string itemCuid, string name)
        {
            ItemDefinition item = GetItem(itemCuid);
            item.Name = name;
            return Repository.Save(item);
        }

        public ItemDefinition GetItem(string itemCuid)
        {
            ItemDefinition item = Repository.Query<ItemDefinition>(new { Cuid = itemCuid }).FirstOrDefault();
            if(item != null)
            {
                return Repository.Retrieve<ItemDefinition>(item.Uuid);
            }
            return null;
        }

        public bool DeleteItem(string itemCuid)
        {
            return Repository.DeleteWhere<ItemDefinition>(new { Cuid = itemCuid });
        }

        public string[] FindItemCatalogs(string itemCuid)
        {
            return Repository.Query<CatalogItem>(new { ItemCuid = itemCuid }).Select(x=> x.CatalogCuid).ToArray();
        }
    }
}
