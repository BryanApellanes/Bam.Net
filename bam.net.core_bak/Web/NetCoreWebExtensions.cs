using Bam.Net.Services;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net;
using System.Reflection;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NetCoreWebExtensions
    {
        public static AuthenticationBuilder AddBamAuthentication(this AuthenticationBuilder builder, Action<BamAuthOptions> options)
        {
            return builder.AddScheme<BamAuthOptions, BamAuthHandler>(BamAuthOptions.Scheme, options);
        }

        /// <summary>
        /// Adds the services from the registry to the services collection.
        /// </summary>
        /// <param name="registry">The registry.</param>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static ApplicationServiceRegistry AddServices(this ApplicationServiceRegistry registry, IServiceCollection services)
        {
            foreach (Type type in registry.MappedTypes)
            {
                if (type.HasCustomAttributeOfType<SingletonAttribute>())
                {
                    AddSingleton(registry, type, services);
                }
                else if (type.HasCustomAttributeOfType<ScopedAttribute>())
                {
                    AddScoped(registry, type, services);
                }
                else if (type.HasCustomAttributeOfType<TransientAttribute>())
                {
                    AddTransient(registry, type, services);
                }
            }

            return registry;
        }

        /// <summary>
        /// Registers the application modules into the registry.
        /// </summary>
        /// <param name="registry">The registry.</param>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static ApplicationServiceRegistry RegisterAppModules(this ApplicationServiceRegistry registry)
        {
            foreach (Type type in Assembly.GetCallingAssembly().GetTypes().Where(type => type.HasCustomAttributeOfType<AppModuleAttribute>()))
            {
                registry.Set(type, type);
            }

            return registry;
        }

        public static ApplicationServiceRegistry AddScoped(this ApplicationServiceRegistry registry, Type type, IServiceCollection services)
        {
            services.AddScoped(type, sp => registry.Get(type));
            return registry;
        }

        public static ApplicationServiceRegistry AddTransient(this ApplicationServiceRegistry registry, Type type, IServiceCollection services)
        {
            services.AddTransient(type, sp => registry.Get(type));
            return registry;
        }

        public static ApplicationServiceRegistry AddSingleton(this ApplicationServiceRegistry registry, Type type, IServiceCollection services)
        {
            AddSingleton(registry, type, services, registry.Get(type));
            return registry;
        }

        public static ApplicationServiceRegistry AddSingleton(this ApplicationServiceRegistry registry, Type type, IServiceCollection services, object instance)
        {
            services.AddSingleton(type, instance);
            return registry;
        }
    }
}
