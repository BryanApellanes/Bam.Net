using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Services.DistributedService.Data;

namespace Bam.Net.Services.CatalogService.Data
{
    public class ItemDefinition: AuditRepoData
    {
        public string Name { get; set; }
        public List<DataProperty> Properties { get; set; }
    }
}
