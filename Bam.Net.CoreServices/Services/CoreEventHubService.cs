using System;
using System.Linq;
using Bam.Net.CoreServices.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.CoreServices
{
    [ApiKeyRequired]
    [Proxy("eventHubSvc")]
    public class CoreEventHubService : ProxyableService
    {
        MetricsEventSourceService _metricsEvents;
        NotificationEventSourceService _notificationEvents;
        
        protected CoreEventHubService()
        {
        }

        public CoreEventHubService(
            IRepository genericRepo, 
            DaoRepository daoRepo, 
            AppConf appConf,
            MetricsEventSourceService metricsEvents,
            NotificationEventSourceService notificationEvents) : base(genericRepo, daoRepo, appConf)
        {
            _metricsEvents = metricsEvents;
            _notificationEvents = notificationEvents;

            daoRepo.AddType<ExternalEventSubscriptionInfo>();
        }

        protected IDatabaseProvider DatabaseProvider { get; set; }

        public virtual ExternalEventSubscriptionInfo SubscribeExternal(ExternalEventSubscriptionInfo subscriptionInfo)
        {
            if(string.IsNullOrWhiteSpace(subscriptionInfo.CreatedBy))
            {
                subscriptionInfo.CreatedBy = CurrentUser.UserName;
            }
            string clientNameProp = nameof(ExternalEventSubscriptionInfo.ClientName);
            string eventNameProp = nameof(ExternalEventSubscriptionInfo.EventName);
            Args.ThrowIfNullOrEmpty(subscriptionInfo.ClientName, clientNameProp);
            Args.ThrowIfNullOrEmpty(subscriptionInfo.EventName, eventNameProp);
            ExternalEventSubscriptionInfo lookedUp = DaoRepository.Query<ExternalEventSubscriptionInfo>
            (
                Query.Where(clientNameProp) == subscriptionInfo.ClientName &&
                Query.Where(eventNameProp) == subscriptionInfo.EventName &&
                Query.Where(nameof(ExternalEventSubscriptionInfo.CreatedBy)) == subscriptionInfo.CreatedBy
            )
            .FirstOrDefault();
            if(lookedUp != null)
            {
                return lookedUp;
            }
            return DaoRepository.Save(subscriptionInfo);
        }

        public virtual ServiceResponse RecieveExternal(ExternalEventSubscriptionInfo info)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            CoreEventHubService clone = new CoreEventHubService(Repository, DaoRepository, AppConf, _metricsEvents, _notificationEvents);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
