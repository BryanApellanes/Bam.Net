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
    public partial class HttpContextWrapper : HttpContextBase, IHttpContext
    {
        public HttpContextWrapper(HttpContext context)
            : this(new RequestWrapper(context.Request), new ResponseWrapper(context.Response))
        { }

        public HttpContextWrapper(HttpContextBase context)
            : this(new RequestWrapper(context.Request), new ResponseWrapper(context.Response))
        {
            this.User = context.User;
        }

        public HttpContextWrapper(ControllerContext context)
            : this(new HttpContextWrapper(context))
        {
        }        
    }
}
