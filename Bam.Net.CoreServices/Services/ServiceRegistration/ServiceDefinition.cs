using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.ServiceRegistration
{
    public class ServiceDefinition
    {
        public Type ForType { get; set; }
        public Assembly ForAssembly { get; set; }
        public Type UseType { get; set; }
        public Assembly UseAssembly { get; set; }
    }
}
