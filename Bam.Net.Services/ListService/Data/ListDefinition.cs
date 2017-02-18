using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.ListService.Data
{
    public class ListDefinition: AuditRepoData
    {
        public string Name { get; set; }
        public List<ItemDefinition> Items { get; set; }
    }
}
