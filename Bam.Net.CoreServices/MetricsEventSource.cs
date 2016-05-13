using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Messaging;
using Bam.Net.Server;

namespace Bam.Net.CoreServices
{
    [Proxy("metricsEvents")]
    public class MetricsEventSource : EventSource
    {
        public MetricsEventSource(DaoRepository daoRepository, AppConf appConf, ILogger logger) : base(daoRepository, appConf, logger)
        {
        }

        public override object Clone()
        {
            return new MetricsEventSource(DaoRepository, AppConf, Logger);
        }
    }
}
