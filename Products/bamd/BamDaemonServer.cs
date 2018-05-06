using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using System.IO;

namespace Bam.Net.Application
{
    public class BamDaemonServer : SimpleServer<BamDaemonResponder>
    {
        public BamDaemonServer(ILogger logger)
            : base(new BamDaemonResponder(), logger)
        {        
        }
    }
}
