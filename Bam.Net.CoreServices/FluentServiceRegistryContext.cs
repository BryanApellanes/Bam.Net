using Bam.Net.Incubation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class FluentServiceRegistryContext<I>
    {
        public FluentServiceRegistryContext()
        {
        }

        public FluentServiceRegistryContext(ServiceRegistry registry)
        {
            ServiceRegistry = registry;
        }

        public ServiceRegistry Use(object instance)
        {
            return Returns(instance);
        }

        public ServiceRegistry Returns(object instance)
        {
            ServiceRegistry inc = ServiceRegistry ?? new ServiceRegistry();
            inc.Set(typeof(I), () => instance);
            return inc;
        }

        public ServiceRegistry Use<T>()
        {
            return Returns<T>();
        }

        /// <summary>
        /// Specify the return type T for the specified 
        /// type I ( same as To )
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ServiceRegistry Returns<T>()
        {
            ServiceRegistry inc = ServiceRegistry ?? new ServiceRegistry();
            inc.Set(typeof(I), () => inc.Construct(typeof(T)));
            return inc;
        }

        public ServiceRegistry Use<T>(Func<T> instanciator)
        {
            return Returns<T>(instanciator);
        }

        public ServiceRegistry Returns<T>(Func<T> instanciator)
        {
            ServiceRegistry inc = ServiceRegistry ?? new ServiceRegistry();
            inc.Set(typeof(I), instanciator, false);
            return inc;
        }

        public ServiceRegistry Use<T>(Func<ServiceRegistry, T> instanciator)
        {
            return Returns<T>(instanciator);
        }

        public ServiceRegistry Returns<T>(Func<ServiceRegistry, T> instanciator)
        {
            ServiceRegistry inc = ServiceRegistry ?? new ServiceRegistry();
            inc.Set(typeof(I), () => instanciator(inc));
            return inc;
        }

        /// <summary>
        /// Specify the return type T for the specified 
        /// type I ( same as Returns )
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ServiceRegistry To<T>()
        {
            return Returns<T>();
        }

        protected ServiceRegistry ServiceRegistry
        {
            get;
            set;
        }
    }
}
