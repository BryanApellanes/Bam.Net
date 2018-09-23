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
                HashSet<Assembly> results = new HashSet<Assembly>
                {
                    typeof(Uri).Assembly,
                    Type.Assembly
                };
                MethodGenerationInfos.Each(mgi =>
                {
                    mgi.ReferenceAssemblies.Each(a => results.Add(a));
                });
                CustomAttributeTypeDescriptor customAttributes = new CustomAttributeTypeDescriptor(Type);
                customAttributes.AttributeTypes.Each(attrType => results.Add(attrType.Assembly));
                AddDefaultAssemblies(results);
                return results.ToArray();
            }
        }

        private void AddDefaultAssemblies(HashSet<Assembly> assemblies)
        {
            assemblies.Add(typeof(Args).Assembly);
            assemblies.Add(typeof(ServiceProxySystem).Assembly);
        }
    }
}
