using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    /// <summary>
    /// A domain associated with an application
    /// </summary>
    [Serializable]
    public class HostDomain: AuditRepoData
    {
        public HostDomain()
        {
            Port = 80;
        }
        public string DefaultApplicationName { get; set; }
        public string DomainName { get; set; }        
        public int Port { get; set; }
        public bool Authorized { get; set; }
        public virtual List<Application> Applications { get; set; }
    }
}
