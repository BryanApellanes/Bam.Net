using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.Server.JsonRpc;
using Bam.Net.Server.Renderers;
using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;

namespace Bam.Net.Application
{
    public class GlooResponder : HttpHeaderResponder, IInitialize<GlooResponder>
    {
        readonly Dictionary<string, IResponder> _responders;
        public GlooResponder(BamConf conf, ILogger logger, bool verbose = false)
            : base(conf, logger)
        {
            RendererFactory = new RendererFactory(logger);
            ServiceProxyResponder = new ServiceProxyResponder(conf, logger);
            RpcResponder = new JsonRpcResponder(conf, logger);
            _responders = new Dictionary<string, IResponder>
            {
                { ServiceProxyResponder.Name, ServiceProxyResponder },
                { RpcResponder.Name, RpcResponder }
            };
            if (verbose)
            {
                WireResponseLogging();
            }
        }

        public void WireResponseLogging()
        {
            WireResponseLogging(ServiceProxyResponder, Logger);
            WireResponseLogging(RpcResponder, Logger);
        }

        public ServiceProxyResponder ServiceProxyResponder
        {
            get;
            private set;
        }

        public JsonRpcResponder RpcResponder
        {
            get;
            private set;
        }

        public override bool Respond(IHttpContext context)
        {
            if (!TryRespond(context))
            {
                SendResponse(context, 404, new { BamServer = "Gloo Server" } );
            }
            context.Response.Close();
            return true;
        }

        public override bool TryRespond(IHttpContext context)
        {
            return TryRespond(context, out IResponder responder);
        }

        public bool TryRespond(IHttpContext context, out IResponder responder)
        {
            try
            {
                string requestedResponder = GetRequestedResponderName(context).Or(ServiceProxyResponder.Name);
                responder = _responders[requestedResponder];
                return responder.TryRespond(context);
            }
            catch (Exception ex)
            {
                responder = null;
                Logger.AddEntry("Gloo: exception occurred trying to respond, {0}", ex, ex.Message);
                return false;
            }
        }

        public event Action<GlooResponder> Initializing;
        public event Action<GlooResponder> Initialized;

        public override void Initialize()
        {
            OnInitializing();
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
