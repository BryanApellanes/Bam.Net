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
    public class DaemonServer : SimpleServer<DaemonResponder>
    {
        public DaemonServer(BamConf conf, DaemonProcessMonitorService monitorService, ILogger logger, bool verbose = false)
            : base(new DaemonResponder(conf, monitorService, logger, verbose), logger)
        {
            MonitorService = monitorService;
        }        

        public DaemonProcessMonitorService MonitorService { get; }
    }
}
