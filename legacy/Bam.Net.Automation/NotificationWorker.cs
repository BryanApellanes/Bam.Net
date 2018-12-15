/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    public abstract class NotificationWorker: Worker
    {
        public NotificationWorker() : base() { }
        public NotificationWorker(string name) : base(name) { }

        /// <summary>
        /// A comma/or semi-colon separated list of
        /// recipients (typically email addresses, but depends on 
        /// the implementation)
        /// </summary>
        public string Recipients { get; set; }
    }
}
