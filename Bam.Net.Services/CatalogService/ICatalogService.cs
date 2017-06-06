using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.Catalog.Data;

namespace Bam.Net.Services
{
    public interface ICatalogService
    {
        CatalogDefinition CreateCatalog(string name);
        ItemDefinition CreateItem(string name);
        CatalogDefinition AddItem(string catalogCuid, string itemCuid);
        bool RemoveItem(string listCuid, string itemCuid);
        CatalogDefinition GetCatalog(string catalogCuid);
        CatalogDefinition FindCatalog(string name);
        CatalogDefinition RenameCatalog(string catalogCuid, string name);
        ItemDefinition RenameItem(string itemCuid, string name);
        bool DeleteCatalog(string catalogCuid);
        bool DeleteItem(string itemCuid);
    }
}
