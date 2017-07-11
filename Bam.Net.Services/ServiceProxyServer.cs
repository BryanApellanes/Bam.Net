using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.CoreServices;

namespace Bam.Net.Services
{
    public class ServiceProxyServer : SimpleServer<ServiceProxyResponder>
    {
        public ServiceProxyServer(CoreServices.ServiceRegistry serviceRegistry, ServiceProxyResponder responder, ILogger logger) : base(responder, logger)
        {
            RegisterServices(serviceRegistry);
        }

        protected CoreServices.ServiceRegistry ServiceRegistry { get; set; }
        public void RegisterServices(CoreServices.ServiceRegistry serviceRegistry)
        {
            ServiceRegistry = serviceRegistry;
            Responder.ClearCommonServices();
            Responder.ClearAppServices();
            ServiceRegistry.MappedTypes.Where(t => t.HasCustomAttributeOfType<ProxyAttribute>()).Each(t => AddService(t));
        }

        public void AddService(Type serviceType)
        {
            Responder.AddCommoneService(serviceType, () => ServiceRegistry.Get(serviceType));
        }
    }
}
