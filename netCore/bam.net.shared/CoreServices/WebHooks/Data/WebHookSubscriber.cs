using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.WebHooks.Data
{
    [Serializable]
    public class WebHookSubscriber: AuditRepoData
    {
        public WebHookSubscriber()
        {
            Descriptors = new List<WebHookDescriptor>();
        }

        public virtual List<WebHookDescriptor> Descriptors { get; set; }
        public string Url { get; set; }
        public string SharedSecret { get; set; }
    }
}
