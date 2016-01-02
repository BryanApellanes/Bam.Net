/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.ServiceProxy
{
    public class ServiceGenerationInfo
    {
        public ServiceGenerationInfo(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; private set; }

        public MethodGenerationInfo[] MethodGenerationInfos
        {
            get
            {
                return ServiceProxySystem.GetProxiedMethods(Type).Select(m => new MethodGenerationInfo(m)).ToArray();
            }
        }

        public Assembly[] ReferenceAssemblies
        {
            get
            {
                HashSet<Assembly> results = new HashSet<Assembly>();
                results.Add(typeof(Uri).Assembly);
                results.Add(Type.Assembly);
                MethodGenerationInfos.Each(mgi =>
                {
                    mgi.ReferenceAssemblies.Each(a => results.Add(a));
                });
                AddDefaultAssemblies(results);
                return results.ToArray();
            }
        }

        private void AddDefaultAssemblies(HashSet<Assembly> assemblies)
        {
            assemblies.Add(typeof(Args).Assembly);
        }
    }
}
