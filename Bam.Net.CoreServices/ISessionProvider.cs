using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.CoreServices
{
    public interface ISessionProvider
    {
        Session GetSession(IHttpContext context);
    }
}
