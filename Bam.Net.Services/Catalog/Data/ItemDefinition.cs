using System.Collections.Generic;
using Bam.Net.Data.Repositories;
using Bam.Net.Services.DataReplication.Data;

namespace Bam.Net.Services.Catalog.Data
{
    public class ItemDefinition: AuditRepoData
    {
        public string Name { get; set; }
        public List<DataProperty> Properties { get; set; }
    }
}
