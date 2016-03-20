using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;

namespace Bam.Net.CoreServices
{
    public class ProxyableServiceBase: Loggable, IRequiresHttpContext
    {
        [Exclude]
        public IHttpContext HttpContext
        {
            get;
            set;
        }
    }
}
