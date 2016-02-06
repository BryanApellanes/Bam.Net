/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// The data model passed to the razor template.  Note
    /// that though there are properties that appear not to
    /// be referenced they are used by the razor template
    /// </summary>
    public class ServiceModel
    {
        public ServiceModel(Type serviceType, string protocol = "http", string host = "localhost", int port = 8080)
        {
            this.ServiceGenerationInfo = new ServiceGenerationInfo(serviceType);
            this.BaseType = serviceType;
            this.Protocol = protocol;
            this.Host = host;
            this.Port = port;
        }

        public Type BaseType { get; set; }
        public ServiceGenerationInfo ServiceGenerationInfo { get; private set; }
        public string Host { get; private set; }
        public string Protocol { get; set; }
        public int Port { get; set; }

        public string TypeName { get { return BaseType.Name; } }

        public ServiceMethodModel[] Methods
        {
            get
            {
                List<ServiceMethodModel> methods = new List<ServiceMethodModel>();
                ServiceProxySystem.GetProxiedMethods(BaseType).Each(mi =>
                {
                    methods.Add(new ServiceMethodModel(mi, ReferenceAssemblies));
                });
                return methods.ToArray();
            }
        }

        public string Namespace { get { return BaseType.Namespace; } }

        public string ProviderBaseUrl
        {
            get
            {
                return "{Protocol}://{Host}:{Port}/".NamedFormat(this);
            }
        }

        public Assembly[] ReferenceAssemblies
        {
            get
            {
                HashSet<Assembly> assemblies = new HashSet<Assembly>();
                assemblies.Add(typeof(ServiceModel).Assembly);
                ServiceGenerationInfo.ReferenceAssemblies.Each(a => assemblies.Add(a));
                return assemblies.ToArray();
            }
        }

        public string Render()
        {
            return RazorRenderer.RenderResource<ServiceModel>(this, "Service.tmpl", ReferenceAssemblies);
        }
    }
}
