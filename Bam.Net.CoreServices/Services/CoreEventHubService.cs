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

            daoRepo.AddType<ExternalEventSubscription>();
        }

        protected IDatabaseProvider DatabaseProvider { get; set; }

        public virtual ExternalEventSubscription SubscribeExternal(ExternalEventSubscription subscriptionInfo)
        {
            if(string.IsNullOrWhiteSpace(subscriptionInfo.CreatedBy))
            {
                subscriptionInfo.CreatedBy = CurrentUser.UserName;
            }
            string clientNameProp = nameof(ExternalEventSubscription.ClientName);
            string eventNameProp = nameof(ExternalEventSubscription.EventName);
            Args.ThrowIfNullOrEmpty(subscriptionInfo.ClientName, clientNameProp);
            Args.ThrowIfNullOrEmpty(subscriptionInfo.EventName, eventNameProp);
            ExternalEventSubscription lookedUp = DaoRepository.Query<ExternalEventSubscription>
            (
                Query.Where(clientNameProp) == subscriptionInfo.ClientName &&
                Query.Where(eventNameProp) == subscriptionInfo.EventName &&
                Query.Where(nameof(ExternalEventSubscription.CreatedBy)) == subscriptionInfo.CreatedBy
            )
            .FirstOrDefault();
            if(lookedUp != null)
            {
                return lookedUp;
            }
            return DaoRepository.Save(subscriptionInfo);
        }

        public virtual ServiceResponse RecieveExternal(ExternalEventSubscription info)
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
