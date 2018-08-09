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
        Dictionary<string, IResponder> _responders;
        public VyooResponder(BamConf conf, ILogger logger, bool verbose = false)
            : base(conf, logger)
        {
            ServiceProxyResponder = new ServiceProxyResponder(conf, logger);
            ContentResponder = new ContentResponder(conf, logger);
            Init(logger, verbose);
        }

        public VyooResponder(AppConf[] appConfigs, ILogger logger, bool verbose = false) : base(BamConf.Load(), logger)
        {
            ServiceProxyResponder = new ServiceProxyResponder(BamConf, logger);
            ContentResponder = new ContentResponder(logger) { AppConfigs = appConfigs };
            Init(logger, verbose);
        }

        protected void Init(ILogger logger, bool verbose)
        {
            ServiceProxyResponder.AddCommonService(new AppMetaManager(BamConf));
            _responders = new Dictionary<string, IResponder>
            {
                { ServiceProxyResponder.Name, ServiceProxyResponder },
                { ContentResponder.Name, ContentResponder }
            };
            if (verbose)
            {
                WireResponseLogging(ServiceProxyResponder, logger);
                WireResponseLogging(ContentResponder, logger);

                ServiceProxyResponder.Subscribe(logger);
                ContentResponder.Subscribe(logger);                
            }
        }

        public ServiceProxyResponder ServiceProxyResponder { get; private set; }
        public ContentResponder ContentResponder { get; private set; }
        
        public event Action<VyooResponder> Initializing;
        public event Action<VyooResponder> Initialized;

        public override bool Respond(IHttpContext context)
        {
            if (!ContentResponder.TryRespond(context))
            {
                SendResponse(context, 404, new { BamServer = "Vyoo Server" });
            }
            context.Response.Close();
            return true;
        }

        public override bool TryRespond(IHttpContext context)
        {
            try
            {
                string requestedResponder = GetRequestedResponderName(context).Or(ContentResponder.Name);
                return _responders[requestedResponder].TryRespond(context);
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Vyoo: exception occurred trying to respond, {0}", ex, ex.Message);
                return false;
            }
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
