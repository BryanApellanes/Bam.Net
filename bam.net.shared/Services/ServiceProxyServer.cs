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

        public void RegisterServices(ServiceRegistry serviceRegistry, bool requireApiKeyResover = false)
        {
            ServiceRegistry = new WebServiceRegistry(serviceRegistry);
            Responder.ClearCommonServices();
            Responder.ClearAppServices();
            Responder.SetCommonWebServices(ServiceRegistry);
            foreach(Type type in ServiceRegistry.MappedTypes)
            {
                if(type.HasCustomAttributeOfType(out ServiceSubdomainAttribute attr))
                {
                    ServiceSubdomains.Add(attr);
                }
            }
            SetApiKeyResolver(serviceRegistry, requireApiKeyResover ? ApiKeyResolver.Default : null);
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

        public HashSet<ServiceSubdomainAttribute> ServiceSubdomains { get; set; }
        protected WebServiceRegistry ServiceRegistry { get; set; }

        protected void SetApiKeyResolver(ServiceRegistry registry, IApiKeyResolver ifNull)
        {
            IApiKeyResolver apiKeyResolver = registry.Get(ifNull);
            Responder.CommonSecureChannel.ApiKeyResolver = apiKeyResolver;
            Responder.AppSecureChannels.Values.Each(sc => sc.ApiKeyResolver = apiKeyResolver);
        }
    }
}
