/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Bam.Net.ServiceProxy;

namespace Bam.Net.ServiceProxy
{
    public class HttpContextWrapper: HttpContextBase, IHttpContext
    {
        public HttpContextWrapper() { }
        public HttpContextWrapper(HttpListenerContext context)
            : this(new RequestWrapper(context.Request), new ResponseWrapper(context.Response))
        { }
		public HttpContextWrapper(IHttpContext context)
			: this(new RequestWrapper(context.Request), new ResponseWrapper(context.Response))
		{
			this.User = context.User;
		}

        public HttpContextWrapper(HttpContextBase context)
            : this(new RequestWrapper(context.Request), new ResponseWrapper(context.Response))
        {
            this.User = context.User;
        }

        public HttpContextWrapper(HttpContextWrapper context)
            : this(new RequestWrapper(context.Request), new ResponseWrapper(context.Response))
        {
            this.User = context.User;
        }

        public HttpContextWrapper(ControllerContext context)
            : this(new HttpContextWrapper(context))
        {
        }

        public HttpContextWrapper(IRequest request, IResponse response)
        {
            this.Request = request;
            this.Response = response;
        }
		
        public IResponse Response { get; set; }
        public IRequest Request { get; set; }

        public IPrincipal User { get; set; }
    }
}
