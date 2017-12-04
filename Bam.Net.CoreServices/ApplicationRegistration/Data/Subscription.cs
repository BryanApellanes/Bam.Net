using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    [Serializable]
    public class Subscription: AuditRepoData
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public string SubscriptionLevel { get; set; }
        public int MaxOrganizations { get; set; }
        public int MaxApplications { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
