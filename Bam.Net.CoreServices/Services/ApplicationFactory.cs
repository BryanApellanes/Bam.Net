using System;
using System.Collections.Generic;
using System.Linq;
using Bam.Net.CoreServices.Data;
using Bam.Net.CoreServices.Data.Daos.Repository;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.CoreServices
{
    public class ApplicationFactory : MaximumLimitEnforcer<ServiceResponse<Data.Application>>
    {
        public ApplicationFactory(CoreRegistryRepository repo, Data.User user, ProxyableService service, string organizationName, string applicationName)
        {
            User = user;
            ApplicationName = organizationName;
            ApplicationRegistryRepository = repo;
            OrganizationName = organizationName;
            ApplicationName = applicationName;
            ClientIp = service.ClientIp;
            HostName = service.HostName;
        }
        public string ClientIp { get; set; }
        public string HostName { get; set; }
        public Data.User User { get; set; }
        public string ApplicationName { get; set; }
        public string OrganizationName { get; set; }
        public CoreRegistryRepository ApplicationRegistryRepository { get; set; }
        public override int GetMaximumLimit()
        {
            int max = 1;
            foreach (Subscription sub in User.Subscriptions.Where(s => s.ExpirationDate > DateTime.UtcNow).ToArray())
            {
                if (sub.MaxApplications > max)
                {
                    max = sub.MaxApplications;
                }
            }
            return max;
        }
        public override int GetThrottledValue()
        {
            Organization org = User.Organizations.Where(o => o.Name.Equals(OrganizationName)).FirstOrDefault();
            if (org?.Applications != null)
            {
                return org.Applications.Length;
            }
            return 0;
        }

        public override ServiceResponse<Data.Application> LimitNotReachedAction()
        {
            Organization org = User.Organizations.Where(o => o.Name.Equals(OrganizationName)).FirstOrDefault();
            Data.Application app = ApplicationRegistryRepository.OneApplicationWhere(c => c.Name == ApplicationName && c.OrganizationId == org.Id);
            if (app == null)
            {
                app = new Data.Application { Name = ApplicationName, OrganizationId = org.Id };
                app = ApplicationRegistryRepository.Save(app);
                ApplicationInstance instance = new ApplicationInstance { InstanceIdentifier = $"{ClientIp}-{app.Name}-{app.Cuid}" };
                app.Instances.Add(instance);
                app.HostNames.Add(new Data.HostName { Value = HostName });
                app = CoreApplicationRegistryService.AddApiKey(ApplicationRegistryRepository, app);
                return new ServiceResponse<Data.Application>(app) { Success = true, Message = $"Application {ApplicationName} created" };
            }
            else
            {
                return new ServiceResponse<Data.Application>(app) { Success = true, Message = $"Application {ApplicationName} already registered for the organization {OrganizationName}" };
            }
        }

        public override ServiceResponse<Data.Application> LimitReachedAction()
        {
            return new ServiceResponse<Data.Application>(null) { Success = false, Message = "Application NOT created; limit reached", Data = new ApplicationRegistrationResult { Status = ApplicationRegistrationStatus.LimitExceeded } };
        }

    }
}

