using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Tvg
{
    /// <summary>
    /// Attribute used to denote a method 
    /// that will return a GlooRegistry
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GlooRegistryProviderAttribute: Attribute
    {
    }
}
