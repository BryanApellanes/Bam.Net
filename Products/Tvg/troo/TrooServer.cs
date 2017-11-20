using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Server;

namespace Bam.Net.Application
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
