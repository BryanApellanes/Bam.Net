/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Bam.Net.Incubation;
using Bam.Net.Server.Renderers;

namespace Bam.Net.Server.Rpc
{
	public class RpcResponder: HttpHeaderResponder
	{
		public RpcResponder(BamConf conf, ILogger logger = null)
			: base(conf, logger)
		{
            Executors = Incubator.Default;
        }

        public Incubator Executors
        {
            get;
            set;
        }
        
		protected override bool Post(IHttpContext context)
		{
            IRpcRequest request = RpcMessage.Parse(context);
            bool result = false;
            if (request != null)
            {
                RpcResponse response = request.Execute();
                IRenderer renderer = RendererFactory.CreateRenderer(context.Request);
                renderer.Render(response.GetOutput(), context.Response.OutputStream);
            }
            return result;
		}

		protected override bool Get(IHttpContext context)
		{
			throw new NotImplementedException();
		}

		protected override bool Put(IHttpContext context)
		{
            return false;
		}

		protected override bool Delete(IHttpContext context)
		{
            return false;
		}
	}
}
