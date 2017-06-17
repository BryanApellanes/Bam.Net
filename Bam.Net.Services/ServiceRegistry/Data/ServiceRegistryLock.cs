using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.ServiceRegistry.Data
{
    [Serializable]
    public class ServiceRegistryLock: AuditRepoData
    {
        public string Name { get; set; }
    }
}
