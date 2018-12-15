using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    [Serializable]
    public class Application: AuditRepoData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ulong OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }        
        public virtual List<HostDomain> HostDomains { get; set; }
        public virtual List<Machine> Machines { get; set; }
        public virtual List<ApiKey> ApiKeys { get; set; }
        public virtual List<ProcessDescriptor> Instances { get; set; }
        public virtual List<Configuration> Configurations { get; set; }
        public virtual List<Client> Clients { get; set; }

        public static object ConfigurationLock { get; set; } = new object();

        static object _defaultLock = new object();
        static Application _defaultApplication;
        public static Application Unknown
        {
            get { return _defaultLock.DoubleCheckLock(ref _defaultApplication, () => new Application { Name = ApplicationDiagnosticInfo.UnknownApplication }); }
        }
    }
}
