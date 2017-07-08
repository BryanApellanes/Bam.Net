using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;

namespace Bam.Net.CoreServices
{
    public class RegisterServiceRegistryContainerResult
    {
        public RegisterServiceRegistryContainerResult(string name, CoreServices.ServiceRegistry registry, Type type, MethodInfo method, ServiceRegistryLoaderAttribute attr)
        {
            Success = true;
            Type = type;
            MethodInfo = method;
            Attribute = attr;
            ServiceRegistry = registry;
            Name = name;
        }

        public RegisterServiceRegistryContainerResult(Exception ex)
        {
            Exception = ex;
            Success = false;
        }

        public string Name { get; set; }
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public ServiceRegistryLoaderAttribute Attribute { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public Type Type { get; set; }
        public CoreServices.ServiceRegistry ServiceRegistry { get; set; }
    }
}
