using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.WebHooks.Data
{
    [Serializable]
    public class WebHookDescriptor: AuditRepoData
    {
        public WebHookDescriptor() { }
        public string WebHookName { get; set; }
        public string Description { get; set; }
        public string SharedSecret { get; set; }

        public virtual List<WebHookSubscriber> Subscribers { get; set; }
        public virtual List<WebHookCall> Calls { get; set; }
    }
}
