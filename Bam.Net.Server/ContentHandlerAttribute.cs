using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ContentHandlerAttribute: Attribute
    {
    }
}
