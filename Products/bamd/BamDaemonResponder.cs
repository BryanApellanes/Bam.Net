using Bam.Net.Configuration;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Application
{
    public class BamDaemonResponder : HttpHeaderResponder
    {
        public BamDaemonResponder() : base(BamConf.Load())
        { }

        public override bool Respond(IHttpContext context)
        {
            Logger.AddEntry("BambotResponder Responding: {0}", context.Request.RawUrl);
            SendResponse(context.Response, "OK");
            return true;
        }
    }
}
