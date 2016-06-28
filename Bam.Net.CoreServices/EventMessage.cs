using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices
{
    public class EventMessage: RepoData
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
        /// dot net event of an object instance otherwise
        /// null
        /// </summary>
        public EventArgs EventArgs { get; set; }
    }
}
