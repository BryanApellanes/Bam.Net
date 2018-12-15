using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.Services.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Services
{
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
        }

        public static Action<ApplicationServiceRegistry> Configurer { get; set; }

        [ServiceRegistryLoader]
        public static ApplicationServiceRegistry Configure(Action<ApplicationServiceRegistry> config)
        {
            Configurer = config;
            ApplicationServiceRegistry result = new ApplicationServiceRegistry();
            result.CombineWith(CoreClientServiceRegistryContainer.Current);
            result.For<IApplicationNameProvider>().Use<DefaultConfigurationApplicationNameProvider>();
            config(result);
            result.CoreClient = result.Get<CoreClient>();
            return result;
        }
    }
}
