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
using Bam.Net.Web;

namespace Bam.Net.Server
{
    /// <summary>
    /// A responder that checks the value of the X-Bam-Responder HttpHeader
    /// to determine if the current request is intended for this responder
    /// </summary>
    public abstract class HttpHeaderResponder: HttpMethodResponder
    {
        public HttpHeaderResponder(BamConf conf)
            : base(conf)
        {
            RespondToHeaderValue = ResponderSignificantName;
        }

        public HttpHeaderResponder(BamConf conf, ILogger logger) : base(conf, logger)
        {
            RespondToHeaderValue = ResponderSignificantName;
        }

        /// <summary>
        /// Returns true if the request Header named "X-Bam-Responder" exists 
        /// and the value is equal to the value of RespondToHeaderValue
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool MayRespond(IHttpContext context)
        {
            string requestedResponder = GetRequestedResponderName(context);
            return requestedResponder == null ? true: requestedResponder.Equals(RespondToHeaderValue);
        }

        /// <summary>
        /// The name of the HttpHeader to check the value for, to determine
        /// if the request is intended for the current responder
        /// </summary>
        public static string RespondToHeaderName { get { return CustomHeaders.Responder; } }

        /// <summary>
        /// The value of the HttpHeader if the request is intended for the
        /// currenct responder
        /// </summary>
        public string RespondToHeaderValue { get; protected set; }

        public virtual string GetRequestedResponderName(IHttpContext context)
        {
            return context.Request.Headers[RespondToHeaderName];
        }
    }
}
