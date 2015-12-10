/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Incubation;
using Castle.DynamicProxy;

namespace Bam.Net.Interceptors
{
    public class InterceptionIncubator: Incubator
    {
        ProxyGenerator proxyGenerator;
        public InterceptionIncubator()
        {
            proxyGenerator = new ProxyGenerator();
        }

        public IInterceptor DefaultInterceptor
        {
            get;
            set;
        }

        public new T Construct<T>() where T: class
        {
            return Construct<T>(DefaultInterceptor);
        }

        public T Construct<T>(params IInterceptor[] interceptors) where T: class
        {
            T instance = base.Construct<T>();
            T proxied = proxyGenerator.CreateClassProxyWithTarget<T>(instance, interceptors);
            this.Set<T>(proxied);
            return proxied;
        }

        public object Construct(Type type, IInterceptor[] interceptors = null, params object[] ctorParams)
        {
            if (interceptors == null)
            {
                interceptors = new IInterceptor[] { DefaultInterceptor };
            }

            object instance = base.Construct(type, ctorParams);
            object proxied = proxyGenerator.CreateClassProxyWithTarget(type, instance, interceptors);
            this.Set(type, proxied);
            return proxied;
        }

        public void SetInterceptors(Type type, IInterceptor[] interceptors)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            object instance = this[type];
            if (instance == null)
            {
                throw new InvalidOperationException(string.Format("The specified type ({0}) hasn't been instantiated in the current incubator.", type.Name));
            }
            object proxied = proxyGenerator.CreateClassProxyWithTarget(type, instance, interceptors);
            this.Set(type, proxied);
        }

        public void SetInterceptors<T>(IInterceptor[] interceptors)
        {
            SetInterceptors(typeof(T), interceptors);
        }
    }
}
