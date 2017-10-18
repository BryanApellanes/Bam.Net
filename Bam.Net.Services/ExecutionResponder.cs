using Bam.Net.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Services
{
    public class ExecutionResponder : Responder
    {
        public ExecutionResponder(BamConf conf) : base(conf)
        {
        }

        public ExecutionResponder(BamConf conf, ILogger logger) : base(conf, logger)
        {
        }

        public override bool TryRespond(IHttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
