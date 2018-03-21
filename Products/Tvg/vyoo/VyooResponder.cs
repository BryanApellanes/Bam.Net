using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.Server.Meta;

namespace Bam.Net.Application
{
    public class VyooResponder : HttpHeaderResponder, IInitialize<VyooResponder>
    {
        public VyooResponder(BamConf conf, ILogger logger, bool verbose = false)
            : base(conf, logger)
        {
            ContentResponder = new ContentResponder(conf, logger);
            if (verbose)
            {
                WireResponseLogging(ContentResponder, logger);
                ContentResponder.Subscribe(logger);
            }
        }

        public VyooResponder(AppConf[] appConfigs, ILogger logger, bool verbose = false) : base(null, logger)
        {
            ContentResponder = new ContentResponder(logger) { AppConfigs = appConfigs };
            if (verbose)
            {
                WireResponseLogging(ContentResponder, logger);
                ContentResponder.Subscribe(logger);
            }
        }

        public ContentResponder ContentResponder { get; private set; }

        public event Action<VyooResponder> Initializing;
        public event Action<VyooResponder> Initialized;

        public override bool Respond(IHttpContext context)
        {
            if (!ContentResponder.TryRespond(context))
            {
                SendResponse(context, "Vyoo Server");
            }
            context.Response.Close();
            return true;
        }
        
        public override void Initialize()
        {
            OnInitializing();
            ContentResponder.Initialize();
            base.Initialize();
            OnInitialized();
        }

        protected void OnInitializing()
        {
            Initializing?.Invoke(this);
        }

        protected void OnInitialized()
        {
            Initialized?.Invoke(this);
        }
    }
}
