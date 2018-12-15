/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Web;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server
{
	public abstract class HttpMethodResponder : Responder
	{
		public HttpMethodResponder(BamConf conf)
			: base(conf)
		{
			BamConf = conf;
		}

		public HttpMethodResponder(BamConf conf, ILogger logger)
			: this(conf)
		{
			Logger = logger;
		}

		public override bool TryRespond(IHttpContext context)
		{
			bool responded = false;
			string httpMethod = context.Request.HttpMethod.ToUpperInvariant();
			if (HttpMethodHandlers.ContainsKey(httpMethod))
			{
				responded = HttpMethodHandlers[httpMethod](context);
			}

			return responded;
		}

        protected virtual bool Post(IHttpContext context)
        {
            return false;
        }

		protected virtual bool Get(IHttpContext context)
        {
            return false;
        }

        protected virtual bool Put(IHttpContext context)
        {
            return false;
        }

        protected virtual bool Delete(IHttpContext context)
        {
            return false;
        }

        protected virtual bool Connect(IHttpContext context)
		{
			return false;
		}

		protected virtual bool Head(IHttpContext context)
		{
			return false;
		}

		protected virtual bool Options(IHttpContext context)
		{
			return false;
		}

		protected virtual bool Trace(IHttpContext context)
		{
			return false;
		}

		Dictionary<string, Func<IHttpContext, bool>> _httpMethodHandlers;
		object _httpMethodHandlersLock = new object();
		protected Dictionary<string, Func<IHttpContext, bool>> HttpMethodHandlers
		{
			get
			{
				return _httpMethodHandlersLock.DoubleCheckLock(ref _httpMethodHandlers, () =>
				{
					return new Dictionary<string, Func<IHttpContext, bool>>
					{
						{"POST", Post},
						{"GET", Get},
						{"PUT", Put},
						{"DELETE", Delete},
						{"CONNECT", Connect},
						{"HEAD", Head},
						{"OPTIONS", Options},
						{"TRACE", Trace}
					};
				});
			}
		}

	}
}
