using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Services.ListService.Data;

namespace Bam.Net.Services.ListService
{
    [Proxy("listSvc")]
    public class ListService : ProxyableService, IListService
    {
        public ListDefinition AddItem(string listCuid, string itemCuid)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public ItemDefinition CreateItem(string name)
        {
            throw new NotImplementedException();
        }

        public ListDefinition CreateList(string name)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(string itemCuid)
        {
            throw new NotImplementedException();
        }

        public bool DeleteList(string listCuid)
        {
            throw new NotImplementedException();
        }

        public ListDefinition FindList(string name)
        {
            throw new NotImplementedException();
        }

        public ListDefinition GetList(string listCuid)
        {
            throw new NotImplementedException();
        }

        public bool RemoveItem(string listCuid, string itemCuid)
        {
            throw new NotImplementedException();
        }

        public ItemDefinition RenameItem(string itemCuid, string name)
        {
            throw new NotImplementedException();
        }

        public ListDefinition RenameList(string listCuid, string name)
        {
            throw new NotImplementedException();
        }
    }
}
