using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Hosts.Data
{
    [Serializable]
    public class HostEntryData: AuditRepoData
    {
        public HostEntryData() { }
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
    }
}
