using System;
using System.Collections.Generic;
using System.Linq;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;

namespace Bam.Net.CoreServices
{
    public class ClientApplicationFactory : MaximumLimitEnforcer<CoreServiceResponse<CoreServices.ApplicationRegistration.Data.Application>>
    {
        public ClientApplicationFactory(ApplicationRegistrationService service, User user, string organizationName, ProcessDescriptor processDescriptor)
        {
            CoreApplicationRegistryService = service;
            User = user;
            ApplicationRegistrationRepository = service.ApplicationRegistrationRepository;
            OrganizationName = organizationName;
            ProcessDescriptor = processDescriptor;
            ClientIpAddress = service.ClientIpAddress;
            HostName = service.HostName;
        }

        public ClientApplicationFactory(ApplicationRegistrationService service, User user)
        {
            CoreApplicationRegistryService = service;
            User = user;
            ApplicationRegistrationRepository = service.ApplicationRegistrationRepository;
            OrganizationName = Organization.Public.Name;
        }

        public string ClientIpAddress { get; set; }

        public string HostName { get; set; }

        public User User { get; set; }

        public ProcessDescriptor ProcessDescriptor { get; set; }

        public string OrganizationName { get; set; }

        public ApplicationRegistrationRepository ApplicationRegistrationRepository { get; set; }

        public ApplicationRegistrationService CoreApplicationRegistryService { get; set; } // TODO: rename this to CoreApplicationRegistrationService and test

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

        public override CoreServiceResponse<CoreServices.ApplicationRegistration.Data.Application> LimitNotReachedAction()
        {
            string ApplicationName = ProcessDescriptor.Application.Name;
            return CreateApplication(ApplicationName);
        }

        public CoreServiceResponse<CoreServices.ApplicationRegistration.Data.Application> CreateApplication(string applicationName)
        {
            Organization org = User.Organizations.Where(o => o.Name.Equals(OrganizationName)).FirstOrDefault();
            CoreServices.ApplicationRegistration.Data.Application app = ApplicationRegistrationRepository.OneApplicationWhere(c => c.Name == applicationName && c.OrganizationId == org.Id);
            if (app == null)
            {
                app = new CoreServices.ApplicationRegistration.Data.Application { Name = applicationName, OrganizationId = org.Id };
                app = ApplicationRegistrationRepository.Save(app);
                ProcessDescriptor instance = new ProcessDescriptor { InstanceIdentifier = $"{ClientIpAddress}-{app.Name}-{app.Cuid}" };
                app.Instances.Add(instance);
                app.Machines.Add(new Machine { Name = HostName });
                app = CoreApplicationRegistryService.AddApiKey(ApplicationRegistrationRepository, app);
                return new CoreServiceResponse<ApplicationRegistration.Data.Application>(app) { Success = true, Message = $"Application {applicationName} created" };
            }
            else
            {
                return new CoreServiceResponse<ApplicationRegistration.Data.Application>(app) { Success = true, Message = $"Application {applicationName} already registered for the organization {OrganizationName}" };
            }
        }

        public override CoreServiceResponse<CoreServices.ApplicationRegistration.Data.Application> LimitReachedAction()
        {
            return new CoreServiceResponse<ApplicationRegistration.Data.Application>(null) { Success = false, Message = "Application NOT created; limit reached", Data = new ApplicationRegistrationResult { Status = ApplicationRegistrationStatus.LimitExceeded } };
        }

    }
}

