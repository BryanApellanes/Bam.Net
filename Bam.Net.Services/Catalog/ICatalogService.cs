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
        CatalogDefinition FindCatalog(string name);
        CatalogDefinition RenameCatalog(string catalogCuid, string name);
        CatalogDefinition GetCatalog(string catalogCuid);
        bool DeleteCatalog(string catalogCuid);

        ItemDefinition CreateItem(string name);
        ItemDefinition AddItem(string catalogCuid, string itemCuid);
        bool RemoveItem(string catalogCuid, string itemCuid);
        ItemDefinition RenameItem(string itemCuid, string name);
        ItemDefinition GetItem(string itemCuid);
        bool DeleteItem(string itemCuid);
        string[] FindItemCatalogs(string itemCuid);
    }
}
