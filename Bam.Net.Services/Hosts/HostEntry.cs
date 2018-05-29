using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Hosts
{
    [Serializable]
    public class HostEntry
    {
        public HostEntry() { }
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public string IpAddressV6 { get; set; }
        public string MacAddress { get; set; }
        public string Cuid { get; set; }
    }
}
