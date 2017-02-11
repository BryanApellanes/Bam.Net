using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;

namespace Bam.Net.Services.Distributed
{
    [Proxy("eventAnnouncerSvc")]
    public class EventAnnouncerService : ProxyableService
    {
        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
