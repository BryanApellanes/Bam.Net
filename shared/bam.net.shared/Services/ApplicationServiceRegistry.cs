using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.Presentation;
using Bam.Net.Services.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Services
{
    /// <summary>
    /// A service registry (or dependency injection container) for the currently running application process.  The application name is
    /// determined by the default configuration file (app.config or web.config).
    /// </summary>
    public class ApplicationServiceRegistry: ServiceRegistry
    {
        protected CoreClient CoreClient { get; set; }

        static ApplicationServiceRegistry _appRegistry;
        static object _appRegistryLock = new object();
        public static ApplicationServiceRegistry Current
        {
            get
            {
                return _appRegistryLock.DoubleCheckLock(ref _appRegistry, () => Configure(Configurer ?? ((reg) => { })));
            }
            private set
            {
                _appRegistry = value;
            }
        }

        public static Action<ApplicationServiceRegistry> Configurer { get; set; }

        [ServiceRegistryLoader]
        public static ApplicationServiceRegistry Configure(Action<ApplicationServiceRegistry> configure)
        {
            Configurer = configure;
            ApplicationServiceRegistry appRegistry = new ApplicationServiceRegistry();
            appRegistry.CombineWith(CoreClientServiceRegistryContainer.Current);
            appRegistry
                .For<IApplicationNameProvider>().Use<DefaultConfigurationApplicationNameProvider>()
                .For<ProxyAssemblyGeneratorService>().Use<ProxyAssemblyGeneratorServiceProxy>()
                .For<ApplicationServiceRegistry>().Use(appRegistry)
                .For<ApplicationModel>().Use<ApplicationModel>();

            configure(appRegistry);
            appRegistry.CoreClient = appRegistry.Get<CoreClient>();
            Current = appRegistry;
            return appRegistry;
        }
    }
}
