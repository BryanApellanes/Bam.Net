using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.Data
{
    public class User: RepoData
    {
        public virtual List<Organization> Organizations { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public virtual Subscription[] Subscriptions { get; set; }
    }
}
