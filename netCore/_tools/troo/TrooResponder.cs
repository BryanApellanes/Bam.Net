using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.Server.Renderers;
using Bam.Net.Server.Rest;
using Bam.Net.ServiceProxy;
using System;

namespace Bam.Net.Application
{
    public class TrooResponder : HttpHeaderResponder, IInitialize<TrooResponder>
    {
        public TrooResponder(BamConf conf, ILogger logger, IRepository repository, bool verbose = false)
            : base(conf, logger)
        {
            this.DaoResponder = new DaoResponder(conf, logger);
            this.RestResponder = new RestResponder(conf, repository, logger);
        }

        public DaoResponder DaoResponder
        {
            get;
            private set;
        }

        public RestResponder RestResponder
        {
            get;
            private set;
        }

        public override bool Respond(IHttpContext context)
        {
            if (!TryRespond(context))
            {
                SendResponse(context, 404, new { BamServer = "Troo Server" });
            }
            context.Response.Close();
            return true;
        }

        public override bool TryRespond(IHttpContext context)
        {
            if (DaoResponder.MayRespond(context))
            {
                return DaoResponder.TryRespond(context);
            }
            else
            {
                return RestResponder.TryRespond(context);
            }
        }

        public event Action<TrooResponder> Initializing;

        public event Action<TrooResponder> Initialized;

        public override void Initialize()
        {
            OnInitializing();
            DaoResponder.Initialize();
            RestResponder.Initialize();
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
