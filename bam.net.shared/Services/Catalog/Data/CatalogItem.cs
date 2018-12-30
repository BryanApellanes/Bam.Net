using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Catalog.Data
{
    [Serializable]
    public class CatalogItem: RepoData
    {
        public string CatalogCuid { get; set; }
        public string ItemCuid { get; set; }
    }
}
