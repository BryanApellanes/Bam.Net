using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Events
{
    public class EventMessage: AuditRepoData, IJsonable
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Json { get; set; }

        /// <summary>
        /// The sender of the event if the event was a standard
        /// dot net event otherwise null
        /// </summary>
        public object Sender { get; set; }
        /// <summary>
        /// The EventArgs if the event was a standard
        /// dot net event of an in process object instance otherwise
        /// null
        /// </summary>
        public EventArgs EventArgs { get; set; }

        public string ToJson()
        {
            if (EventArgs is IJsonable args)
            {
                return args.ToJson();
            }
            else
            {
                return new
                {
                    Name,
                    UserName,
                    Json
                }.ToJson();
            }
        }
    }
}
