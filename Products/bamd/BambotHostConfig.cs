using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class BambotHostConfig
    {
        public BambotHostMode Mode { get; set; }
        public string[] DeployBranches { get; set; }
        public string[] Configs { get; set; }
        public BambotServiceConfig[] Services { get; set; }
    }
}
