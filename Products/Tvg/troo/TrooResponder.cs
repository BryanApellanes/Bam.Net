using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.Server.Renderers;
using Bam.Net.Server.Rest;
using System;

namespace Bam.Net.Application
{
    public class TrooResponder : RestResponder, IInitialize<TrooResponder>
    {
        public TrooResponder(BamConf conf, IRepository repository, ILogger logger)
            : base(conf, repository, logger)
        {
            this.RendererFactory = new RendererFactory(logger);
            this.Repository = repository;
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
 
        public override bool TryRespond(ServiceProxy.IHttpContext context)
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
