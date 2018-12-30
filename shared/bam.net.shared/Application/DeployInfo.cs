using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace Bam.Net.Application
{
    public class DeployInfo
    {
        public DeployInfo()
        {
            Daemons = new DaemonInfo[] { };
            WindowsServices = new WindowsServiceInfo[] { };
        }

        public DaemonServiceInfo[] DaemonServices { get; set; }
        public DaemonInfo[] Daemons { get; set; }
        public WindowsServiceInfo[] WindowsServices { get; set; }
    }
}
