using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.ServiceRegistry.Data
{
    [Serializable]
    public class ServiceRegistryLoaderDescriptor: AuditRepoData
    {
        public ServiceRegistryLoaderDescriptor() { }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LoaderType { get; set; }
        public string LoaderAssembly { get; set; }
        public string LoaderMethod { get; set; }
    }
}
