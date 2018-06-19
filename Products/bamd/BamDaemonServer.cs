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
        public BamDaemonServer(BamConf conf, BamDaemonProcessMonitorService monitorService, ILogger logger, bool verbose = false)
            : base(new BamDaemonResponder(conf, monitorService, logger, verbose), logger)
        {
            MonitorService = monitorService;
        }        

        public BamDaemonProcessMonitorService MonitorService { get; }
    }
}
