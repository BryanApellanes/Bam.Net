using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Web;
using Bam.Net.CoreServices.Data;

namespace Bam.Net.CoreServices.Distributed
{
    /// <summary>
    /// A subscription made by a process outside of
    /// the core set of services and processes
    /// </summary>
    public class ExternalEventSubscription: EventSubscription
    {
        public ExternalEventSubscription()
        {
            ExceptionHandler = ((ex) => { });
        }
        public Action<Exception> ExceptionHandler { get; set; }
        public Uri WebHookEndpoint { get; set; }
        public override object Invoke(params object[] args)
        {
            object inProcessResult = base.Invoke(args);
            Exec.TryAsync(() => Http.Post(WebHookEndpoint.ToString(), args.ToJson()), ExceptionHandler);
            return inProcessResult;
        }

        //public ExternalEventSubscriptionDescriptor ToRepoData()
        //{
        //    return ExternalEventSubscriptionDescriptor.FromExternalEventSubscription(this);
        //}
    }
}
