using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class DeployInfo
    {
        public DaemonInfo[] Daemons { get; set; }
        public WindowsServiceInfo[] WindowsServices { get; set; }
    }
}
