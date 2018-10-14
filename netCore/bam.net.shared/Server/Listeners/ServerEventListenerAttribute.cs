/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Listeners
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ServerEventListenerAttribute: Attribute
    {
        public string EventName { get; set; }
    }
}
