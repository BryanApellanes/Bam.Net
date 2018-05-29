using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Catalog.Data
{
    [Serializable]
    public class ItemProperty: AuditRepoData
    {
        public string ItemDefinitionCuid { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
