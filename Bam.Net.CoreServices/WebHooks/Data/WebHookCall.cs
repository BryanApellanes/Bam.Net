using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.WebHooks.Data
{
    public class WebHookCall: AuditRepoData
    {
        public WebHookCall() { }

        public virtual WebHookSubscriber WebHookSubscriber{ get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WebHookCall"/> succeeded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </value>
        public bool Succeeded { get; set; }

        public string Response { get; set; }

        public long WebHookDescriptorId { get; set; }
    }
}
