using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Application
{
    public class VyooResponder : HttpHeaderResponder, IInitialize<VyooResponder>
    {
        public VyooResponder(BamConf conf, ILogger logger, bool verbose = false)
            : base(conf, logger)
        {
            ContentResponder = new ContentResponder(conf, logger);
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

        protected override bool Delete(IHttpContext context)
        {
            throw new NotImplementedException();
        }

        protected override bool Get(IHttpContext context)
        {
            throw new NotImplementedException();
        }

        protected override bool Post(IHttpContext context)
        {
            throw new NotImplementedException();
        }

        protected override bool Put(IHttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
