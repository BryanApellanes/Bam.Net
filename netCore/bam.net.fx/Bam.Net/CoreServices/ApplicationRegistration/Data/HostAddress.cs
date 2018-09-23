using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    [Serializable]
    public class HostAddress: RepoData
    {
        public ulong MachineId { get; set; }
        public virtual Machine Machine { get; set; }
        public string IpAddress { get; set; }
        public string AddressFamily { get; set; }
        public string HostName { get; set; }
    }
}
