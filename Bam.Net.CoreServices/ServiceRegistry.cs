using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;

namespace Bam.Net.CoreServices
{
    public class ServiceRegistry: Incubator
    {
        public string Name { get; set; }
        public ServiceRegistry Include(ServiceRegistry registry)
        {
            CombineWith(registry, true);
            return this;
        }

        public static ServiceRegistry Create()
        {
            return new ServiceRegistry();
        }
    }
}
