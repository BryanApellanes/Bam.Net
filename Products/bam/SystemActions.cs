using Bam.Net.CommandLine;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    [Serializable]
    public class SystemActions: CommandLineTestInterface
    {
        [ConsoleAction("deployHeart", "Deploy and start the heart server to the specified target server")]
        public void DeployHeartService()
        {

        }
    }
}
