using Bam.Net.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bam.Net.Logging;

namespace Bam.Net.Incubation.Mvc
{
    public class BamDependencyResolver : IDependencyResolver
    {
        public BamDependencyResolver(IDependencyResolver defaultResolver, ServiceRegistry serviceRegistry = null, ILogger logger = null)
        {
            DefaultResolver = defaultResolver;
            Logger = logger ?? Log.Default;
            ServiceRegistry = serviceRegistry ?? new ServiceRegistry();
            ServiceRegistry.For<IControllerFactory>().Use(new BamControllerFactory(ServiceRegistry));
        }

        protected IDependencyResolver DefaultResolver { get; set; }
        protected ServiceRegistry ServiceRegistry { get; set; }
        protected ILogger Logger { get; set; }
        public object GetService(Type serviceType)
        {
            try
            {
                return ServiceRegistry.Get(serviceType);
            }
            catch (Exception ex)
            {
                Logger.Warning("Failed to get service of type {0}, falling back to DefaultResolver: {1}", serviceType.Name, ex.Message);
                return DefaultResolver.GetService(serviceType);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return new List<object>() { ServiceRegistry.Get(serviceType) };
            }
            catch (Exception ex)
            {
                Logger.Warning("Failed to get serviceS of type {0}, falling back to DefaultResolver: {1}", serviceType.Name, ex.Message);
                return DefaultResolver.GetServices(serviceType);
            }
        }
    }
}
