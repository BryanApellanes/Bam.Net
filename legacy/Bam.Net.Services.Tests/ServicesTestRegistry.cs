using Bam.Net.CoreServices;
using Bam.Net.Incubation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Tests
{
    [ServiceRegistryContainer]
    public class ServicesTestRegistry
    {
        public void deleteme()
        {
            //ServiceRegistry registry = CoreServiceRegistryContainer.Create();
            //registry.For<object>().Use<object>()
            //    .For<string>().Use<string>()
            //    .For<int>().Use(1);

            //ServiceRegistry registry = ClientServiceRegistryContainer.Create();
            //registry.For<object>().Use<object>()
            //    .For<string>().Use<string>()
            //    .For<int>().Use(1);
        }
    }
}
