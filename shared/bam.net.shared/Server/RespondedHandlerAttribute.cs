/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RespondedHandlerAttribute: Attribute
    {
    }
}
