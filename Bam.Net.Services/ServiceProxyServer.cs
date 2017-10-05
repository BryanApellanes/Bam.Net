using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.CoreServices;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.Services
{
    public class ServiceProxyServer : SimpleServer<ServiceProxyResponder>
    {
        public ServiceProxyServer(ServiceRegistry serviceRegistry, ServiceProxyResponder responder, ILogger logger) : base(responder, logger)
        {
            ServiceSubdomains = new HashSet<ServiceSubdomainAttribute>();
            RegisterServices(serviceRegistry);            
        }

        public void RegisterServices(ServiceRegistry serviceRegistry)
        {
            ServiceRegistry = serviceRegistry;
            Responder.ClearCommonServices();
            Responder.ClearAppServices();
            ServiceRegistry.MappedTypes.Where(t => t.HasCustomAttributeOfType<ProxyAttribute>()).Each(t => AddService(t));
            IApiKeyResolver apiKeyResolver = ServiceRegistry.Get<IApiKeyResolver>();
            Responder.CommonSecureChannel.ApiKeyResolver = apiKeyResolver;
            Responder.AppSecureChannels.Values.Each(sc => sc.ApiKeyResolver = apiKeyResolver);
        }

        public override void Start()
        {
            HostPrefix[] copy = new HostPrefix[HostPrefixes.Count];
            HostPrefixes.CopyTo(copy);
            ServiceSubdomains.Each(sub =>
            {
                copy.Each(hp =>
                {
                    HostPrefixes.Add(hp.FromServiceSubdomain(sub));
                });
            });
            base.Start();
        }

        public void AddService(Type serviceType)
        {
            Responder.AddCommoneService(serviceType, () => ServiceRegistry.Get(serviceType));
            if(serviceType.HasCustomAttributeOfType(out ServiceSubdomainAttribute attr))
            {
                ServiceSubdomains.Add(attr);
            }
        }

        public HashSet<ServiceSubdomainAttribute> ServiceSubdomains { get; set; }
        protected ServiceRegistry ServiceRegistry { get; set; }
    }
}
