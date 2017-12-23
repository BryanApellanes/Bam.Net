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
        public CatalogService(AsyncCallbackService callbackService, DaoRepository repo, AppConf conf) : base(callbackService, repo, conf)
        { }

        public virtual CatalogDefinition AddItem(string listCuid, string itemCuid)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public virtual ItemDefinition CreateItem(string name)
        {
            throw new NotImplementedException();
        }

        public virtual CatalogDefinition CreateCatalog(string name)
        {
            throw new NotImplementedException();
        }

        public virtual bool DeleteItem(string itemCuid)
        {
            throw new NotImplementedException();
        }

        public virtual bool DeleteCatalog(string listCuid)
        {
            throw new NotImplementedException();
        }

        public virtual CatalogDefinition FindCatalog(string name)
        {
            throw new NotImplementedException();
        }

        public virtual CatalogDefinition GetCatalog(string listCuid)
        {
            throw new NotImplementedException();
        }

        public virtual bool RemoveItem(string listCuid, string itemCuid)
        {
            throw new NotImplementedException();
        }

        public virtual ItemDefinition RenameItem(string itemCuid, string name)
        {
            throw new NotImplementedException();
        }

        public virtual CatalogDefinition RenameCatalog(string listCuid, string name)
        {
            throw new NotImplementedException();
        }
    }
}
