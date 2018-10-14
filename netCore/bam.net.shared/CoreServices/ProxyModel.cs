/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;
using Bam.Net.Encryption;
using Bam.Net.Messaging;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// The data model passed to the razor template.  Note
    /// that though there are properties that appear not to
    /// be referenced they are used by the razor template
    /// </summary>
    public class ProxyModel
    {
        public ProxyModel(Type serviceType, string protocol = "http", string host = "localhost", int port = 8080, HashSet<Assembly> addedReferenceAssemblies = null)
        {
            AdditionalReferenceAssemblies = addedReferenceAssemblies ?? new HashSet<Assembly>();
            ServiceGenerationInfo = new ServiceGenerationInfo(serviceType);
            BaseType = serviceType;
            Protocol = protocol;
            Host = host;
            Port = port;
        }

        public HashSet<Assembly> AdditionalReferenceAssemblies { get; set; }
        public Type BaseType { get; set; }
        public ServiceGenerationInfo ServiceGenerationInfo { get; private set; }
        public string Host { get; private set; }
        public string Protocol { get; set; }
        public int Port { get; set; }
        public string[] Usings
        {
            get
            {
                HashSet<string> usings = new HashSet<string>();
                foreach (string u in Methods.Select(pmm => $"using {pmm.MethodGenerationInfo.Method.ReturnType.Namespace};"))
                {
                    usings.Add(u);
                }
                return usings.ToArray();
            }
        }
        public string TypeName { get { return BaseType.Name; } }
        public string Interfaces { get; set; }
        public ProxyMethodModel[] Methods
        {
            get
            {
                List<ProxyMethodModel> methods = new List<ProxyMethodModel>();
                ServiceProxySystem.GetProxiedMethods(BaseType).Each(mi =>
                {
                    methods.Add(new ProxyMethodModel(mi, ReferenceAssemblies));
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
                HashSet<Assembly> assemblies = new HashSet<Assembly>
                {
                    typeof(ProxyModel).Assembly
                };
                ServiceGenerationInfo.ReferenceAssemblies.Each(new { Assemblies = assemblies }, (ctx, a) => ctx.Assemblies.Add(a));
                TypeInheritanceDescriptor inheritance = new TypeInheritanceDescriptor(BaseType);
                inheritance.Chain.Each(new { Assemblies = assemblies }, (ctx, tt) => ctx.Assemblies.Add(tt.Type.Assembly));
                assemblies.Add(typeof(AuditRepoData).Assembly);
                assemblies.Add(typeof(Dao).Assembly);
                assemblies.Add(typeof(DataRow).Assembly);
                assemblies.Add(typeof(Vault).Assembly);
                assemblies.Add(typeof(SmtpSettingsProvider).Assembly);
                AdditionalReferenceAssemblies.Each(new { Assemblies = assemblies }, (ctx, a) => ctx.Assemblies.Add(a));
                return assemblies.ToArray();
            }
        }

        public string Render()
        {
            return RazorRenderer.RenderResource<ProxyModel>(this, "Proxy.tmpl", ReferenceAssemblies);
        }
    }
}
