using System;
using System.Collections.Generic;
using System.Linq;
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.CoreServices.ApplicationRegistration.Dao.Repository;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.CoreServices
{
    public class ClientApplicationFactory : MaximumLimitEnforcer<CoreServiceResponse<ApplicationRegistration.Application>>
    {
        public ClientApplicationFactory(CoreApplicationRegistrationService service, ApplicationRegistration.User user, string organizationName, ProcessDescriptor processDescriptor)
        {
            CoreApplicationRegistryService = service;
            User = user;
            CoreRegistryRepository = service.CoreRegistryRepository;
            OrganizationName = organizationName;
            ProcessDescriptor = processDescriptor;
            ClientIpAddress = service.ClientIpAddress;
            HostName = service.HostName;
        }
        public string ClientIpAddress { get; set; }
        public string HostName { get; set; }
        public ApplicationRegistration.User User { get; set; }
        public ProcessDescriptor ProcessDescriptor { get; set; }
        public string OrganizationName { get; set; }
        public ApplicationRegistrationRepository CoreRegistryRepository { get; set; }
        public CoreApplicationRegistrationService CoreApplicationRegistryService { get; set; }
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

        public override CoreServiceResponse<ApplicationRegistration.Application> LimitNotReachedAction()
        {
            string ApplicationName = ProcessDescriptor.Application.Name;
            Organization org = User.Organizations.Where(o => o.Name.Equals(OrganizationName)).FirstOrDefault();
            ApplicationRegistration.Application app = CoreRegistryRepository.OneApplicationWhere(c => c.Name == ApplicationName && c.OrganizationId == org.Id);
            if (app == null)
            {
                app = new ApplicationRegistration.Application { Name = ApplicationName, OrganizationId = org.Id };
                app = CoreRegistryRepository.Save(app);
                ProcessDescriptor instance = new ProcessDescriptor { InstanceIdentifier = $"{ClientIpAddress}-{app.Name}-{app.Cuid}" };
                app.Instances.Add(instance);
                app.Machines.Add(new Machine { Name = HostName });
                app = CoreApplicationRegistryService.AddApiKey(CoreRegistryRepository, app);
                return new CoreServiceResponse<ApplicationRegistration.Application>(app) { Success = true, Message = $"Application {ApplicationName} created" };
            }
            else
            {
                return new CoreServiceResponse<ApplicationRegistration.Application>(app) { Success = true, Message = $"Application {ApplicationName} already registered for the organization {OrganizationName}" };
            }
        }

        public override CoreServiceResponse<ApplicationRegistration.Application> LimitReachedAction()
        {
            return new CoreServiceResponse<ApplicationRegistration.Application>(null) { Success = false, Message = "Application NOT created; limit reached", Data = new ApplicationRegistrationResult { Status = ApplicationRegistrationStatus.LimitExceeded } };
        }

    }
}

