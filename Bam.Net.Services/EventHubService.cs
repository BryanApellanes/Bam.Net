using System;
using System.Linq;
using Bam.Net.CoreServices.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.CoreServices.Distributed;

namespace Bam.Net.CoreServices
{
    [ApiKeyRequired]
    [Proxy("eventHubSvc")]
    public class EventHubService : ProxyableService
    {
        MetricsEventSourceService _metricsEvents;
        NotificationEventSourceService _notificationEvents;
        
        protected EventHubService()
        {
        }

        public EventHubService(
            IRepository genericRepo, 
            DaoRepository daoRepo, 
            AppConf appConf,
            MetricsEventSourceService metricsEvents,
            NotificationEventSourceService notificationEvents) : base(genericRepo, daoRepo, appConf)
        {
            _metricsEvents = metricsEvents;
            _notificationEvents = notificationEvents;

            daoRepo.AddType<ExternalEventSubscriptionDescriptor>();
        }

        protected IDatabaseProvider DatabaseProvider { get; set; }

        public virtual ExternalEventSubscriptionDescriptor SubscribeExternal(ExternalEventSubscriptionDescriptor subscriptionInfo)
        {
            if(string.IsNullOrWhiteSpace(subscriptionInfo.CreatedBy))
            {
                subscriptionInfo.CreatedBy = CurrentUser.UserName;
            }
            string clientNameProp = nameof(ExternalEventSubscriptionDescriptor.ClientName);
            string eventNameProp = nameof(ExternalEventSubscriptionDescriptor.EventName);
            Args.ThrowIfNullOrEmpty(subscriptionInfo.ClientName, clientNameProp);
            Args.ThrowIfNullOrEmpty(subscriptionInfo.EventName, eventNameProp);
            ExternalEventSubscriptionDescriptor lookedUp = DaoRepository.Query<ExternalEventSubscriptionDescriptor>
            (
                Query.Where(clientNameProp) == subscriptionInfo.ClientName &&
                Query.Where(eventNameProp) == subscriptionInfo.EventName &&
                Query.Where(nameof(ExternalEventSubscriptionDescriptor.CreatedBy)) == subscriptionInfo.CreatedBy
            )
            .FirstOrDefault();
            if(lookedUp != null)
            {
                return lookedUp;
            }
            return DaoRepository.Save(subscriptionInfo);
        }

        public virtual ServiceResponse RecieveExternal(ExternalEventSubscriptionDescriptor info)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            EventHubService clone = new EventHubService(Repository, DaoRepository, AppConf, _metricsEvents, _notificationEvents);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
