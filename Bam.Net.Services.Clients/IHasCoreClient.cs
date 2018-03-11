using Bam.Net.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Clients
{
    public interface IHasCoreClient
    {
        IUserManager UserManager { get; }
        CoreClient CoreClient { get; set; }
    }
}
