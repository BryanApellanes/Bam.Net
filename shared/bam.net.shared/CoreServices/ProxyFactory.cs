/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Linq;
using System.Reflection;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using System.Collections.Generic;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// A class used for generating service proxies for use in code
    /// </summary>
    public class ProxyFactory: ProxyAssemblyGenerationEventSource
    {
        public ProxyFactory(ILogger logger = null)
            : this(".", logger)
        { }

        public ProxyFactory(Incubator incubator) 
            : this(".", null, incubator)
        { }

        /// <summary>
        /// Instanciate a new ProxyFactory placing temporary code files
        /// in the specified workspaceDirectory 
        /// </summary>
        /// <param name="workspaceDirectory">The directory to write temp files to</param>
        /// <param name="logger">The logger used to log activity</param>
        /// <param name="serviceProvider"></param>
        public ProxyFactory(string workspaceDirectory, ILogger logger = null, Incubator serviceProvider = null)
        {
            WorkspaceDirectory = workspaceDirectory;
            DefaultSettings = new ProxySettings { Protocol = Protocols.Http, Host = "localhost", Port = 9100 };
            Logger = logger ?? Log.Default;
            ServiceProvider = serviceProvider ?? Incubator.Default;
            HostNameMunger = NoMungeMunger;
        }

        protected Func<Type, string, string> SubdomainPrefixMunger
        {
            get
            {
                return (type, hostName) =>
                {
                    if (type.HasCustomAttributeOfType(out ServiceSubdomainAttribute attr))
                    {
                        return $"{attr.Subdomain}.{hostName}";
                    }
                    return hostName;
                };
            }
        }
        protected Func<Type, string, string> NoMungeMunger
        {
            get
            {
                return (type, hostName) => hostName;
            }
        }
        bool _mungeHostName;
        /// <summary>
        /// Gets or sets a value indicating whether the client hostname used will have 
        /// the service name as a subdomain.
        /// </summary>
        public bool MungeHostNames
        {
            get
            {
                return _mungeHostName;
            }
            set
            {
                _mungeHostName = value;
                if (_mungeHostName)
                {
                    HostNameMunger = SubdomainPrefixMunger;
                }
                else
                {
                    HostNameMunger = NoMungeMunger;
                }
            }
        }

        public Incubator ServiceProvider { get; set; }

        /// <summary>
        /// The logger used to log events in the current ProxyFactory
        /// </summary>
        public ILogger Logger { get; set; }
        /// <summary>
        /// The default settings to use if none are specified
        /// </summary>
        public ProxySettings DefaultSettings { get; set; }

        /// <summary>
        /// The directory to save temp and generated files in
        /// </summary>
        public string WorkspaceDirectory { get; private set; }

        public T GetProxy<T>(EventHandler<ServiceProxyInvokeEventArgs> invocationExceptionHandler)
        {
            T result = GetProxy<T>();
            SubscribeToInvocationExceptions(result, invocationExceptionHandler);
            return result;
        } 

        /// <summary>
        /// Get a proxy of the specified generic type T which, when invoked, will delegate
        /// to the specified hostName and port subscribing the specified invocationExceptionHandler
        /// to any exceptions related to the ServiceProxyClient delegated invocations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hostName"></param>
        /// <param name="port"></param>
        /// <param name="invocationExceptionHandler"></param>
        /// <returns></returns>
        public T GetProxy<T>(string hostName, int port, EventHandler<ServiceProxyInvokeEventArgs> invocationExceptionHandler)
        {
            T result = GetProxy<T>(hostName, port, new HashSet<Assembly>());
            SubscribeToInvocationExceptions(result, invocationExceptionHandler);
            return result;
        }

        public T GetProxy<T>(string hostName, int port, ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            T proxy = GetProxy<T>(hostName, port, new HashSet<Assembly>());
            SubscribeToInvocationExceptions(proxy, (o, a) => logger.AddEntry("Proxy Invocation Failed:(Cuid={0}):Remote={1}:{2}.{3}: {4}", a.Exception, a.Cuid, a.BaseAddress, a.ClassName, a.MethodName, a.Message));
            return proxy;
        }

        public object GetProxy(Type type)
        {
            Assembly assembly = GetAssembly(type);
            return ConstructProxy(type, assembly, ServiceProvider);
        }

        public static T DownloadProxy<T>(string hostName, int port = 9100, ILogger logger = null, HashSet<Assembly> addedReferenceAssemblies = null)
        {
            logger = logger ?? Log.Default;
            ProxyFactory factory = new ProxyFactory(DefaultDataDirectoryProvider.Current.GetWorkspaceDirectory(typeof(ProxyFactory)).FullName, logger);
            return factory.GetProxy<T>(hostName, port, logger);
        }

        /// <summary>
        /// Get a proxy instance using locally available
        /// assemblies
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetProxy<T>()
        {
            Assembly assembly = GetAssembly<T>();
            return ConstructProxy<T>(assembly, ServiceProvider);
        }
        
        public string GetProxySource(Type type)
        {
            ProxySettings settings = DefaultSettings.CopyAs<ProxySettings>();
            settings.ServiceType = type;
            return GetProxySource(settings);
        }

        public string GetProxySource<T>(string hostName, int port)
        {
            return GetProxySource(typeof(T), hostName, port);
        }

        public string GetProxySource(Type type, string hostName, int port)
        {
            ProxySettings settings = DefaultSettings.Clone();
            settings.ServiceType = type;
            settings.DownloadClient = true;
            settings.Host = hostName;
            settings.Port = port;
            return GetProxySource(settings);
        }

        public string GetProxySource(ProxySettings proxySettings)
        {
            ProxyAssemblyGenerator generator = new ProxyAssemblyGenerator(proxySettings, WorkspaceDirectory, Logger);
            generator.AssemblyGenerating += (o, args) => OnAssemblyGenerating(args);
            generator.AssemblyGenerated += (o, args) => OnAssemblyGenerated(args);
            generator.MethodWarning += (o, args) => OnMethodWarning(args);
            return generator.GetSource();
        }

        /// <summary>
        /// Get a proxy instance downloading source from the
        /// specified hostName and port
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hostName"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public T GetProxy<T>(string hostName, int port = 9100, HashSet<Assembly> addedReferenceAssemblies = null)
        {
            Assembly assembly = GetAssembly<T>(MungeHostName(typeof(T), hostName), port, addedReferenceAssemblies);
            return ConstructProxy<T>(assembly, ServiceProvider);
        }

        public object GetProxy(Type type, string hostName, int port = 9100)
        {
            Assembly assembly = GetAssembly(type, MungeHostName(type, hostName), port);
            return ConstructProxy(type, assembly, ServiceProvider);
        }

        protected string MungeHostName(Type type, string hostName)
        {
            return HostNameMunger?.Invoke(type, hostName) ?? hostName;
        }

        /// <summary>
        /// A function that takes the service type and hostname and optionally
        /// returns a modified hostname.  Useful if services will be
        /// hosted on subdomains, for example: bamapps.net -> appregistration.bammapps.net.
        /// </summary>
        public Func<Type, string, string> HostNameMunger { get; set; }

        /// <summary>
        /// Gets a generated proxy Assembly using local code.  No code will be network
        /// downloaded
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected internal Assembly GetAssembly<T>()
        {
            return GetAssembly(typeof(T));
        }

        protected internal Assembly GetAssembly(Type type)
        {
            ProxySettings settings = DefaultSettings.Clone();
            settings.ServiceType = type;
            settings.DownloadClient = false;
            return GetAssembly(settings);
        }

        /// <summary>
        /// Gets a generated proxy Assembly for the specified type downloading it from the specified
        /// host and port
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hostName"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        protected internal Assembly GetAssembly<T>(string hostName, int port = 9100, HashSet<Assembly> addedReferenceAssemblies = null)
        {
            return GetAssembly(typeof(T), hostName, port, addedReferenceAssemblies);
        }

        protected internal Assembly GetAssembly(Type type, string hostName, int port= 9100, HashSet<Assembly> addedReferenceAssemblies = null)
        {
            ProxySettings settings = DefaultSettings.Clone();
            settings.ServiceType = type;
            settings.DownloadClient = true;
            settings.Host = hostName;
            settings.Port = port;
            return GetAssembly(settings, addedReferenceAssemblies);
        }

        /// <summary>
        /// Gets a generated assembly using the specified settings.  The specified settings
        /// must refer to a running ServiceProxySystem instance if DownloadClient is true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings"></param>
        /// <returns></returns>
        protected internal Assembly GetAssembly(ProxySettings settings, HashSet<Assembly> addedReferenceAssemblies = null)
        {
            Args.ThrowIfNull(settings.ServiceType, "ProxySettings.ServiceType");
            settings.ValidateTypeMethodsOrThrow();

            settings = settings ?? DefaultSettings;
            ProxyAssemblyGenerator generator = new ProxyAssemblyGenerator(settings, WorkspaceDirectory, Logger, addedReferenceAssemblies);
            generator.AssemblyGenerating += (o, args) => OnAssemblyGenerating(args);
            generator.AssemblyGenerated += (o, args) => OnAssemblyGenerated(args);
            generator.MethodWarning += (o, args) => OnMethodWarning(args);
            return generator.GetAssembly();
        }

        private static T ConstructProxy<T>(Assembly assembly, Incubator serviceProvider = null)
        {
            string proxyTypeName = "{0}Proxy"._Format(typeof(T).Name);
            Type proxyType = assembly.GetTypes().FirstOrDefault(t => t.Name.Equals(proxyTypeName));
            if (proxyType == null)
            {
                Args.Throw<ArgumentException>("The proxy {0} for type {1} was not found in the specified assembly: {2}", proxyTypeName, typeof(T).Name, assembly.FullName);
            }
            T result = proxyType.Construct<T>();
            if(serviceProvider != null)
            {
                serviceProvider.SetProperties(result);
            }
            return result;
        }

        private static object ConstructProxy(Type type, Assembly assembly, Incubator serviceProvider = null)
        {
            string proxyTypeName = "{0}Proxy"._Format(type.Name);
            Type proxyType = assembly.GetTypes().FirstOrDefault(t => t.Name.Equals(proxyTypeName));
            if (proxyType == null)
            {
                Args.Throw<ArgumentException>("The proxy {0} for type {1} was not found in the specified assembly: {2}", proxyTypeName, type.Name, assembly.FullName);
            }            
            object result = proxyType.Construct(type);
            if (serviceProvider != null)
            {
                serviceProvider.SetProperties(result);
            }
            return result;
        }
        
        private static void SubscribeToInvocationExceptions<T>(T result, EventHandler<ServiceProxyInvokeEventArgs> invocationExceptionHandler)
        {
            ServiceProxyClient client = result.Property<ServiceProxyClient>("Client"); // Client is defined on the generated proxy class, and this is using reflection to access it; magical knowledge 
            client.Subscribe(nameof(ServiceProxyClient.InvocationException), invocationExceptionHandler);
        }
    }
}
