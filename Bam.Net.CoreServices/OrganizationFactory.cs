using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;
//using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;

namespace Bam.Net.CoreServices
{
    public class OrganizationFactory : MaximumLimitEnforcer<CoreServiceResponse<Organization>>
    {
        public OrganizationFactory(ApplicationRegistrationRepository repo, User user, string organizationName)
        {
            User = user;
            OrganizationName = organizationName;
            ApplicationRegistryRepository = repo;
        }
        public User User { get; set; }
        public string OrganizationName { get; set; }
        public ApplicationRegistrationRepository ApplicationRegistryRepository { get; set; }
        public override int GetMaximumLimit()
        {
            int max = 1;
            foreach(Subscription sub in User.Subscriptions.Where(s=> s.ExpirationDate > DateTime.UtcNow).ToArray())
            {
                if(sub.MaxOrganizations > max)
                {
                    max = sub.MaxOrganizations;
                }
            }
            return max;
        }
        public override int GetThrottledValue()
        {
            return User.Organizations.Count;
        }

        public override CoreServiceResponse<Organization> LimitNotReachedAction()
        {
            Organization org = User.Organizations.Where(c => c.Name == OrganizationName).FirstOrDefault();
            if (org == null)
            {
                org = ApplicationRegistryRepository.OneOrganizationWhere(c => c.Name == OrganizationName);
                if(org == null)
                {
                    org = new Organization { Name = OrganizationName };
                    org = ApplicationRegistryRepository.Save(org);
                }
                User.Organizations.Add(org);
                User = ApplicationRegistryRepository.Save(User);                
            }
            return new CoreServiceResponse<Organization>(org) { Success = true, Message = $"Organization {OrganizationName} created" };
        }

        public override CoreServiceResponse<Organization> LimitReachedAction()
        {
            return new CoreServiceResponse<Organization>(null) { Success = false, Message = "Organization NOT created; limit reached" };
        }
    }
}
