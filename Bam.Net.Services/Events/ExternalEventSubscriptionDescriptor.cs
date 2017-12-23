using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Services.DataReplication;

namespace Bam.Net.Services.Events
{
    public class ExternalEventSubscriptionDescriptor: AuditRepoData
    {
        public string ClientName { get; set; }
        public string EventName { get; set; }
        public string WebHookEndpoint { get; set; }
        public static ExternalEventSubscriptionDescriptor FromExternalEventSubscription(ExternalEventSubscription subscription)
        {
            return new ExternalEventSubscriptionDescriptor
            {
                EventName = subscription.EventName,
                WebHookEndpoint = subscription.WebHookEndpoint.ToString()
            };
        }
    }
}
