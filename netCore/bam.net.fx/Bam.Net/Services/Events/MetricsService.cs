using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Messaging;
using Bam.Net.Server;

namespace Bam.Net.Services.Events
{
    [Proxy("metricsSvc")]
    public class MetricsService : EventSourceService
    {
        public MetricsService(DaoRepository daoRepository, AppConf appConf, ILogger logger) : base(daoRepository, appConf, logger)
        {
        }

        public override object Clone()
        {
            return new MetricsService(DaoRepository, AppConf, Logger);
        }
    }
}
