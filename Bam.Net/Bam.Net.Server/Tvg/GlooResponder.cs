using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Server.Rest;
using Bam.Net.Server.Renderers;
using Bam.Net.ServiceProxy;
using Bam.Net.Server.Rpc;

namespace Bam.Net.Server.Tvg
{
    public class GlooResponder : ResponderBase, IInitialize<GlooResponder>
    {
        public GlooResponder(BamConf conf, ILogger logger)
            : base(conf, logger)
        {
            this.RendererFactory = new RendererFactory(logger);
            this.ServiceProxyResponder = new ServiceProxyResponder(conf, logger);
            this.RpcResponder = new RpcResponder(conf, logger);
        }
        public ServiceProxyResponder ServiceProxyResponder
        {
            get;
            private set;
        }
        public RpcResponder RpcResponder
        {
            get;
            private set;
        }
        public override bool Respond(IHttpContext context)
        {
            if (!TryRespond(context))
            {
                SendResponse(context, "Gloo Server");
            }
            context.Response.Close();
            return true;
        }
        public override bool TryRespond(IHttpContext context)
        {
            if (ServiceProxyResponder.MayRespond(context))
            {
                return ServiceProxyResponder.TryRespond(context);
            }
            else
            {
                return RpcResponder.TryRespond(context);
            }
        }

        public event Action<GlooResponder> Initializing;

        public event Action<GlooResponder> Initialized;
        public override void Initialize()
        {
            OnInitializing();
            ServiceProxyResponder.Initialize();
            RpcResponder.Initialize();
            base.Initialize();
            OnInitialized();
        }
        protected void OnInitializing()
        {
            if (Initializing != null)
            {
                Initializing(this);
            }
        }
        protected void OnInitialized()
        {
            if (Initialized != null)
            {
                Initialized(this);
            }
        }
    }
}
