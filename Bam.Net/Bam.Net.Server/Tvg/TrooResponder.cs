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

namespace Bam.Net.Server.Tvg
{
    public class TrooResponder : ResponderBase, IInitialize<TrooResponder>
    {
        public TrooResponder(BamConf conf, ILogger logger, IRepository repository)
            : base(conf, logger)
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
        public IRepository Repository
        {
            get;
            set;
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
