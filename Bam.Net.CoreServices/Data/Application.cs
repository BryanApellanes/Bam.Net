using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.Data
{
    public class Application: AuditRepoData
    {
        public long OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public string Name { get; set; }
        public virtual List<Machine> Machines { get; set; }
        public virtual List<ApiKey> ApiKeys { get; set; }
        public virtual List<ProcessDescriptor> Instances { get; set; }
        public virtual List<Configuration> Configuration { get; set; }
    }
}
