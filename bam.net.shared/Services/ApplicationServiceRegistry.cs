using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.Services.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Services
{
    /// <summary>
    /// A service registry for the currently running application process.  The application name is
    /// determined by the default configuration file (app.config or web.config) where the 
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
            ApplicationServiceRegistry result = new ApplicationServiceRegistry();
            result.CombineWith(CoreClientServiceRegistryContainer.Current);
            result.For<IApplicationNameProvider>().Use<DefaultConfigurationApplicationNameProvider>();
            configure(result);
            result.CoreClient = result.Get<CoreClient>();
            Current = result;
            return result;
        }
    }
}
