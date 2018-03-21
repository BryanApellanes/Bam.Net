using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// Attribute used to denote a method 
    /// that will return a ServiceRegistry.  The 
    /// method must be static 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ServiceRegistryLoaderAttribute: Attribute
    {
        public ServiceRegistryLoaderAttribute() { }
        public ServiceRegistryLoaderAttribute(string registryName)
        {
            RegistryName = registryName;
            ProcessModes = new List<ProcessModes>(new ProcessModes[] { Net.ProcessModes.Dev, Net.ProcessModes.Test, Net.ProcessModes.Prod });
        }

        public ServiceRegistryLoaderAttribute(string registryName, params ProcessModes[] modes)
        {
            RegistryName = registryName;
            ProcessModes = new List<ProcessModes>(modes);
        }

        /// <summary>
        /// The modes during which this loader will be registered.
        /// </summary>
        public List<ProcessModes> ProcessModes { get; set; }

        public string RegistryName { get; set; }
        public string Description { get; set; }
    }
}
