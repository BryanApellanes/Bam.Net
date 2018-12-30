using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Catalog.Data
{
    [Serializable]
    public class CatalogDefinition: AuditRepoData
    {
        public string Name { get; set; }
        public List<ItemDefinition> Items { get; set; }
        public string KindOfCatalog { get; set; } // should implicitly map to KindsOfCatalogs
    }
}
