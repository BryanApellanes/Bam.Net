using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;

namespace Bam.Net.CoreServices
{
    public class ServiceRegistry: Incubator
    {
        public string Name { get; set; }

        public FluentServiceRegistryContext<I> For<I>()
        {
            return new FluentServiceRegistryContext<I>(this);
        }

        public ServiceRegistry Include(Incubator incubator)
        {
            CombineWith(incubator, true);
            return this;
        }

        public ServiceRegistry Include(ServiceRegistry registry)
        {
            CombineWith(registry, true);
            return this;
        }

        public static ServiceRegistry Create()
        {
            return new ServiceRegistry();
        }

        public new static ServiceRegistry Default { get; set; }
        public static Func<object> GetServiceLoader(Type type, object orDefault = null)
        {
            if (Default == null)
            {
                Type coreRegistryContainer = type.Assembly.GetTypes().Where(t => t.HasCustomAttributeOfType<ServiceRegistryContainerAttribute>()).FirstOrDefault();
                if (coreRegistryContainer != null)
                {
                    MethodInfo provider = coreRegistryContainer.GetMethods().Where(mi => mi.HasCustomAttributeOfType<ServiceRegistryLoaderAttribute>() || mi.Name.Equals("Get")).FirstOrDefault();
                    if (provider != null)
                    {
                        object instance = provider.IsStatic ? null : provider.DeclaringType.Construct();
                        Default = (ServiceRegistry)provider.Invoke(instance, null);
                    }
                }
            }
            return Default == null ? (() => type.Construct()) : (Func<object>)(() =>
            {
                if (!Default.TryGet(type, out object result))
                {
                    result = orDefault;
                }
                return result;
            });
        }
    }
}
