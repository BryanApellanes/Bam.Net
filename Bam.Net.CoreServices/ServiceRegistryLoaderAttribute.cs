using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// Attribute used to denote a method 
    /// that will return a CoreRegistry.  The 
    /// method must be static 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ServiceRegistryLoaderAttribute: Attribute
    {
        public ServiceRegistryLoaderAttribute() { }
        public ServiceRegistryLoaderAttribute(string registryName)
        {
            RegistryName = registryName;
        }

        public string RegistryName { get; set; }
        public string Description { get; set; }
    }
}
