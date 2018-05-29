using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Meta
{
    /// <summary>
    /// Used to specify the path prefix for a proxyable service
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RoutePrefixAttribute: Attribute
    {
        public string Value { get; set; }
    }
}
