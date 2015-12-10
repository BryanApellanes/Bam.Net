/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Js;

namespace Bam.Net.Syndication
{
    public static class ServiceProxySystemRss
    {
        /// <summary>
        /// Registers the rss file extension and the delegate used
        /// to handle it.
        /// </summary>
        public static void  Register()
        {
            ServiceProxySystem.RegisterServiceProxyRequestDelegate("rss", Rss);
        }

        public static RssResult Rss(ExecutionRequest request)
        {
            return new RssResult(request.Result);
        }

        public static void RegisterRss(this ServiceProxySystem sys)
        {
            Register();
        }
    }
}
