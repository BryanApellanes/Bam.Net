using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Tvg
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GlooContainerAttribute: Attribute // must have a static Get method
    {
    }
}
