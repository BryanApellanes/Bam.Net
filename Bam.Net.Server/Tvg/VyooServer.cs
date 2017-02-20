using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Server.Tvg
{
    public class VyooServer : SimpleServer<ContentResponder>
    {
        public VyooServer(BamConf conf, ILogger logger)
            : base(new ContentResponder(conf, logger), logger)
        {
            Responder.Initialize();
        }
    }
}
