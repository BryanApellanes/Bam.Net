using Bam.Net.CoreServices;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Administration
{
    [ServiceRegistryContainer]
    public class AdministrationServiceRegistryContainer
    {
        public const string Registryname = "AdministrationServiceRegistry";

        static ServiceRegistry _administrationServiceRegistry;
        static object _administrationRegistryLock = new object();

        [ServiceRegistryLoader(Registryname)]
        public static ServiceRegistry GetServiceRegistry()
        {
            return _administrationRegistryLock.DoubleCheckLock(ref _administrationServiceRegistry, Create);
        }

        public static ServiceRegistry Create()
        {
            ServiceRegistry registry = CoreServiceRegistryContainer.Create();

            ServiceRegistry reg = (ServiceRegistry)(new ServiceRegistry())
                .For<ILogger>().Use(Log.Default)
                .For<AdministrationService>().Use<AdministrationService>();

            reg.CombineWith(registry);
            return reg;
        }
    }
}
