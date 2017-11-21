using Bam.Net.CoreServices;
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Services.Events;
using System;
using System.Linq;

namespace Bam.Net.Services.DataReplication
{
    [ApiKeyRequired]
    [Proxy("writeEventSvc")]
    public class WriteEventService : ApplicationProxyableService
    {
        MetricsService _metricsEvents;
        SystemEventService _notificationEvents;

        protected WriteEventService()
        {
        }

        public WriteEventService(
            IRepository genericRepo,
            DaoRepository daoRepo,
            AppConf appConf,
            MetricsService metricsEvents,
            SystemEventService notificationEvents) : base(genericRepo, daoRepo, appConf)
        {
            _metricsEvents = metricsEvents;
            _notificationEvents = notificationEvents;

            daoRepo.AddType<ExternalEventSubscriptionDescriptor>();
        }
        
        public virtual ExternalEventSubscriptionDescriptor SubscribeExternal(ExternalEventSubscriptionDescriptor subscriptionInfo)
        {
            if (string.IsNullOrWhiteSpace(subscriptionInfo.CreatedBy))
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
            if (lookedUp != null)
            {
                return lookedUp;
            }
            return DaoRepository.Save(subscriptionInfo);
        }

        public virtual CoreServiceResponse RecieveEvent(ExternalEventSubscriptionDescriptor info)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            WriteEventService clone = new WriteEventService(Repository, DaoRepository, AppConf, _metricsEvents, _notificationEvents);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
