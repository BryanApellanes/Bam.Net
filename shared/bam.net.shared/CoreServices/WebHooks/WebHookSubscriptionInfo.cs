using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.WebHooks
{
    [Serializable]
    public class WebHookSubscriptionInfo
    {
        public WebHookSubscriptionInfo() { }
        public string WebHookName { get; set; }
        public string Url { get; set; }
    }
}
