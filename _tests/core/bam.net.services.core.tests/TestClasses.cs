using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.Incubation;
using Bam.Net.Logging;

namespace Bam.Net.Services.Tests
{

    public class TestRegistryClass
    {
        public TestRegistryClass(string ctorParam, ILogger logger)
        {
            SetByCtor = ctorParam;
            Logger = logger;
        }

        public string SetByCtor { get; set; }
        public ILogger Logger { get; set; }
    }

    [ServiceRegistryContainer]
    public class TestRegistryContainer
    {
        public static string TestValue = "Some Test Value";
        [ServiceRegistryLoader("TestContainerWithAttribute")]
        public CoreServices.ServiceRegistry RegistryLoader()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            CoreServices.ServiceRegistry registry = new CoreServices.ServiceRegistry();
            registry
                .For<ILogger>().Use(logger)
                .ForCtor<TestRegistryClass>("ctorParam").Use(TestValue);
            return registry;
        }
    }
}
