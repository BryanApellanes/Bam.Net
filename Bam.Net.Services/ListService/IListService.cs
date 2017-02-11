using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.ListService.Data;

namespace Bam.Net.Services.ListService
{
    public interface IListService
    {
        ListDefinition CreateList(string name);
        ItemDefinition CreateItem(string name);
        ListDefinition AddItem(string listCuid, string itemCuid);
        bool RemoveItem(string listCuid, string itemCuid);
        ListDefinition GetList(string listCuid);
        ListDefinition FindList(string name);
        ListDefinition RenameList(string listCuid, string name);
        ItemDefinition RenameItem(string itemCuid, string name);
        bool DeleteList(string listCuid);
        bool DeleteItem(string itemCuid);
    }
}
