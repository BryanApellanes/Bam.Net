using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// Attribute used to decorate a class that contains a 
    /// method used to retrieve a ServiceRegistry
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceRegistryContainerAttribute: Attribute 
    {
    }
}
