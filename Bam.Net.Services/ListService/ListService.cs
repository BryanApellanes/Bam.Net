using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using Bam.Net.Services.ListService.Data;

namespace Bam.Net.Services.ListService
{
    [Proxy("listSvc")]
    public class ListService : ProxyableService, IListService
    {
        protected ListService() { }
        public ListService(DaoRepository repo, AppConf conf) : base(repo, conf)
        { }

        public virtual ListDefinition AddItem(string listCuid, string itemCuid)
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

        public virtual ListDefinition CreateList(string name)
        {
            throw new NotImplementedException();
        }

        public virtual bool DeleteItem(string itemCuid)
        {
            throw new NotImplementedException();
        }

        public virtual bool DeleteList(string listCuid)
        {
            throw new NotImplementedException();
        }

        public virtual ListDefinition FindList(string name)
        {
            throw new NotImplementedException();
        }

        public virtual ListDefinition GetList(string listCuid)
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

        public virtual ListDefinition RenameList(string listCuid, string name)
        {
            throw new NotImplementedException();
        }
    }
}
