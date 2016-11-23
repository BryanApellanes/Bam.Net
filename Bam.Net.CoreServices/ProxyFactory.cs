/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Linq;
using System.Reflection;
using Bam.Net.Logging;
using Bam.Net.Incubation;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// A class used for generating service proxies for use in code
    /// </summary>
    public class ProxyFactory: AssemblyGenerationEventSource
    {
        public ProxyFactory(ILogger logger = null)
            : this(".", logger)
        { }

        public ProxyFactory(Incubator incubator) 
            : this(".", null, incubator)
        { }

        public ProxyFactory(string workspaceDirectory, ILogger logger = null, Incubator serviceProvider = null)
        {
            WorkspaceDirectory = workspaceDirectory;
            DefaultSettings = new ProxySettings { Protocol = Protocols.Http, Host = "localhost", Port = 8080 };
            Logger = logger ?? Log.Default;
            ServiceProvider = serviceProvider ?? Incubator.Default;
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
        public T GetProxy<T>()
        {
            Assembly assembly = GetAssembly<T>();
            return ConstructProxy<T>(assembly);
        }

        public T GetProxy<T>(string hostName, int port = 8080)
        {
            Assembly assembly = GetAssembly<T>(hostName, port);
            return ConstructProxy<T>(assembly, ServiceProvider);
        }

        /// <summary>
        /// Gets a generated proxy Assembly using local code.  No code will be network
        /// downloaded
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected internal Assembly GetAssembly<T>()
        {
            ProxySettings settings = DefaultSettings.Clone();
            settings.ServiceType = typeof(T);
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
        protected internal Assembly GetAssembly<T>(string hostName, int port = 8080)
        {
            ProxySettings settings = DefaultSettings.Clone();
            settings.ServiceType = typeof(T);
            settings.DownloadClient = true;
            settings.Host = hostName;
            settings.Port = port;
            return GetAssembly(settings);
        }

        /// <summary>
        /// Gets a generated assembly using the specified settings.  The specified settings
        /// must refer to a running ServiceProxySystem instance if DownloadClient is true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings"></param>
        /// <returns></returns>
        protected internal Assembly GetAssembly(ProxySettings settings)
        {
            Args.ThrowIfNull(settings.ServiceType, "ProxySettings.ServiceType");
            settings.ValidateOrThrow();

            settings = settings ?? DefaultSettings;
            ProxyAssemblyGenerator generator = new ProxyAssemblyGenerator(settings, WorkspaceDirectory, Logger);
            generator.AssemblyGenerating += (o, args) =>
            {
                OnAssemblyGenerating(args);
            };
            generator.AssemblyGenerated += (o, args) =>
            {
                OnAssemblyGenerated(args);
            };
            generator.MethodWarning += (o, args) =>
            {
                OnMethodWarning(args);
            };
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
    }
}
