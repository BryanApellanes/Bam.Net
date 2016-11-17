using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Data;
using Bam.Net.CoreServices.Data.Daos.Repository;

namespace Bam.Net.CoreServices
{
    public class OrganizationFactory : MaximumLimitEnforcer<ServiceResponse<Organization>>
    {
        public OrganizationFactory(CoreRegistryRepository repo, User user, string organizationName)
        {
            User = user;
            OrganizationName = organizationName;
            ApplicationRegistryRepository = repo;
        }
        public User User { get; set; }
        public string OrganizationName { get; set; }
        public CoreRegistryRepository ApplicationRegistryRepository { get; set; }
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

        public override ServiceResponse<Organization> LimitNotReachedAction()
        {
            Organization org = User.Organizations.Where(c => c.Name == OrganizationName).FirstOrDefault();
            if (org == null)
            {
                org = new Organization { Name = OrganizationName };
                User.Organizations.Add(org);
                User = ApplicationRegistryRepository.Save(User);
                org = ApplicationRegistryRepository.OneOrganizationWhere(c => c.Name == OrganizationName);
            }
            return new ServiceResponse<Organization>(org) { Success = true, Message = $"Organization {OrganizationName} created" };
        }

        public override ServiceResponse<Organization> LimitReachedAction()
        {
            return new ServiceResponse<Organization>(null) { Success = false, Message = "Organization NOT created; limit reached" };
        }
    }
}
