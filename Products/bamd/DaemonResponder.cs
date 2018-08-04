using Bam.Net.Configuration;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Logging;
using Bam.Net.Server.Renderers;
using Bam.Net.Services.Automation;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Application
{
    public class DaemonResponder : HttpHeaderResponder
    {
        public DaemonResponder(BamConf conf, DaemonProcessMonitorService monitorService, ILogger logger, bool verbose = false) 
            : base(conf, logger)
        {
            RendererFactory = new RendererFactory(logger);
            ServiceProxyResponder = new ServiceProxyResponder(conf, logger);
            ServiceProxyResponder.AddCommonService(new CommandService());
            ServiceProxyResponder.AddCommonService(monitorService);
            DefaultDataSettingsProvider.Current.SetRuntimeAppDataDirectory();
            if (verbose)
            {
                WireResponseLogging(ServiceProxyResponder, logger);
            }
        }

        public ServiceProxyResponder ServiceProxyResponder
        {
            get;
            private set;
        }

        public override bool Respond(IHttpContext context)
        {
            if (!ServiceProxyResponder.TryRespond(context))
            {
                SendResponse(context, 404, new { BamServer = "BamDaemon" });
            }
            context.Response.Close();
            return true;
        }
    }
}
