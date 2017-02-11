using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Messaging;
using Bam.Net.Server;

namespace Bam.Net.CoreServices.Distributed
{
    [Proxy("metricsEvents")]
    public class MetricsEventSourceService : EventSourceService
    {
        public MetricsEventSourceService(DaoRepository daoRepository, AppConf appConf, ILogger logger) : base(daoRepository, appConf, logger)
        {
        }

        public override object Clone()
        {
            return new MetricsEventSourceService(DaoRepository, AppConf, Logger);
        }
    }
}
