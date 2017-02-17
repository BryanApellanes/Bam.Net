﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.CoreServices.Distributed;

namespace Bam.Net.CoreServices.Data
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