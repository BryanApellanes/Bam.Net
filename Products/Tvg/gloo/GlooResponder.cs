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
        Dictionary<string, IResponder> _responders;
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
                SendResponse(context, "Gloo Server");
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
            string requestedResponder = GetRequestedResponderName(context).Or(ServiceProxyResponder.Name);
            responder = _responders[requestedResponder];
            return responder.TryRespond(context);
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
            if (Initialized != null)
            {
                Initialized(this);
            }
        }

        protected override bool Post(IHttpContext context)
        {
            throw new NotImplementedException();
        }

        protected override bool Get(IHttpContext context)
        {
            throw new NotImplementedException();
        }

        protected override bool Put(IHttpContext context)
        {
            throw new NotImplementedException();
        }

        protected override bool Delete(IHttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
