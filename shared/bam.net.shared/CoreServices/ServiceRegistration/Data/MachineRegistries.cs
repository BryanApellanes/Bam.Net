using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.ServiceRegistration.Data
{
    public class MachineRegistries: AuditRepoData
    {
        public MachineRegistries()
        {
            Name = Environment.MachineName;
            DnsName = Dns.GetHostName();
        }
        public MachineRegistries(string name, string dnsName)
        {
            Name = name;
            DnsName = dnsName;
        }
        public string Name { get; set; }
        public string DnsName { get; set; }
        
        /// <summary>
        /// A delimited list of ServiceRegistry names
        /// </summary>
        public string RegistryNames { get; set; }
    }
}
