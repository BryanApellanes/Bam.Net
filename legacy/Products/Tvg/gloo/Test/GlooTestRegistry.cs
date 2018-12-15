using Bam.Net;
using Bam.Net.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;
using Bam.Net.Application;

namespace gloo.Test
{
    [Serializable]
    [ServiceRegistryContainer]
    public class GlooTestRegistry
    {
        [ServiceRegistryLoader("GlooTestRegistry", ProcessModes.Dev, ProcessModes.Test)]
        public static ServiceRegistry CreateTestRegistry()
        {
            return ServiceRegistry.Create()
                .For<GlooTestService>().Use<GlooTestService>()
                .For<GlooEncryptedTestService>().Use<GlooEncryptedTestService>()
                .For<GlooApiKeyRequiredTestService>().Use<GlooApiKeyRequiredTestService>()
                .Cast<ServiceRegistry>();
        }
    }
}
