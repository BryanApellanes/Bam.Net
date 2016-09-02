using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Server.Tvg
{
    public class TrooServer: SimpleServer<TrooResponder>
    {
        public TrooServer(BamConf conf, ILogger logger, IRepository repository)
            : base(new TrooResponder(conf, repository, logger), logger)
        {
            this.Responder.Initialize();
        }
    }
}
