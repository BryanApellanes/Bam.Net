using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    /// <summary>
    /// Used to denote a method that will not be 
    /// proxied and will execute locally.  Also
    /// allows a method to be network invoked if
    /// the service is exposed to the local loopback
    /// address 127.0.0.1
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class LocalAttribute: ExcludeAttribute
    {
    }
}
