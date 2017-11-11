using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.CoreServices.AssemblyManagement.Data.Dao.Repository;
using Bam.Net.CoreServices.ServiceRegistration.Data.Dao.Repository;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bam.Net.Application
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        static string contentRootConfigKey = "ContentRoot";
        static string defaultContentRoot = "C:\\bam\\content";
        static string defaultGlooScriptsSrcPath = "C:\\bam\\gloo\\scripts";
        static string csGlooBin = "C:\\bam\\gloo\\bin";

        static GlooServer glooServer;
        
        [ConsoleAction("startGlooServer", "Start the gloo server")]
        public void StartGlooServer()
        {
            ConsoleLogger logger = GetLogger();
            StartGlooServer(logger);
            Pause("Gloo is running");
        }

        [ConsoleAction("killGlooServer", "Kill the gloo server")]
        public void StopGlooServer()
        {
            if (glooServer != null)
            {
                glooServer.Stop();
                Pause("Gloo stopped");
            }
            else
            {
                OutLine("Gloo server not running");
            }
        }

        [ConsoleAction("serve", "Start the gloo server serving a specific service class")]
        public void Serve()
        {
            string serviceClassName = GetArgument("serve", "Enter the name of the class to serve ");
            string contentRoot = GetArgument("ContentRoot", $"Enter the path to the content root (default: {defaultContentRoot} ");
            Type serviceType = GetServiceType(serviceClassName);
            HostPrefix prefix = GetConfiguredHostPrefix();
            if (serviceType.HasCustomAttributeOfType(out ServiceSubdomainAttribute attr))
            {
                prefix.HostName = $"{attr.Subdomain}.{prefix.HostName}";
            }

            ServeServiceTypes(contentRoot, prefix, null, serviceType);
            Pause($"Gloo server is serving service {serviceClassName}");
        }

        [ConsoleAction("csgloo", "Start the gloo server serving the compiled results of the specified csgloo files")]
        public void ServeCsGloo()
        {
            string csglooDirectoryPath = GetArgument("CsGlooSrc", $"Enter the path to the CsGloo source directory (default: {defaultGlooScriptsSrcPath})");
            DirectoryInfo csglooSrcDirectory = new DirectoryInfo(csglooDirectoryPath.Or(defaultGlooScriptsSrcPath)).EnsureExists();
            DirectoryInfo csglooBinDirectory = new DirectoryInfo(csGlooBin).EnsureExists();
            FileInfo[] files = csglooSrcDirectory.GetFiles("*.csgloo", SearchOption.AllDirectories);
            StringBuilder src = new StringBuilder();
            foreach (FileInfo file in files)
            {
                src.AppendLine(file.ReadAllText());
            }
            string hash = src.ToString().Sha1();
            string assemblyName = $"{hash}.dll";
            Assembly csglooAssembly = null;
            string csglooAssemblyBinPath = Path.Combine(csglooBinDirectory.FullName, assemblyName);
            if (!File.Exists(csglooAssemblyBinPath))
            {
                csglooAssembly = files.ToAssembly(assemblyName);
                FileInfo assemblyFile = csglooAssembly.GetFileInfo();
                FileInfo targetPath = new FileInfo(csglooAssemblyBinPath);
                if (!targetPath.Directory.Exists)
                {
                    targetPath.Directory.Create();
                }
                File.Copy(assemblyFile.FullName, csglooAssemblyBinPath);
            }
            else
            {
                csglooAssembly = Assembly.LoadFile(csglooAssemblyBinPath);
            }
            
            string contentRoot = GetArgument("ContentRoot", $"Enter the path to the content root (default: {defaultContentRoot} ");
            
            HostPrefix prefix = GetConfiguredHostPrefix();
            Type[] glooTypes = csglooAssembly.GetTypes().Where(t => t.HasCustomAttributeOfType<ProxyAttribute>()).ToArray();
            ServeServiceTypes(contentRoot, prefix, null, glooTypes);
            Pause($"Gloo server is serving cs gloo types: {string.Join(", ", glooTypes.Select(t=> t.Name).ToArray())}");
        }

        [ConsoleAction("registries", "Start the gloo server serving the registries of the specified names")]
        public void ServeRegistries()
        {
            ConsoleLogger logger = GetLogger();
            string registries = GetArgument("registries", "Enter the registry names to serve in a comma separated list ");
            ServeRegistries(logger, registries);
        }

        [ConsoleAction("app", "Start the gloo server serving the registry for the current application (determined by the default configuration file ApplicationName value)")]
        public void ServeApplicationRegistry()
        {
            ConsoleLogger logger = GetLogger();
            ServeRegistries(logger, DefaultConfigurationApplicationNameProvider.Instance.GetApplicationName());
        }

        private static void ServeRegistries(ILogger logger, string registries)
        {
            string contentRoot = GetArgument("ContentRoot", $"Enter the path to the content root (default: {defaultContentRoot} ");
            DataSettings dataSettings = DataSettings.Default;
            IApplicationNameProvider appNameProvider = DefaultConfigurationApplicationNameProvider.Instance;

            CoreServiceRegistrationService serviceRegistryService = GetCoreServiceRegistrationService(logger, dataSettings, appNameProvider);

            string[] requestedRegistries = registries.DelimitSplit(",");
            HashSet<Type> serviceTypes = new HashSet<Type>();
            ServiceRegistry allTypes = new ServiceRegistry();
            foreach (string registryName in requestedRegistries)
            {
                ServiceRegistry registry = serviceRegistryService.GetServiceRegistry(registryName);
                foreach (string className in registry.ClassNames)
                {
                    registry.Get(className, out Type type);
                    if (type.HasCustomAttributeOfType<ProxyAttribute>())
                    {
                        serviceTypes.Add(type);
                    }
                }
                allTypes.CombineWith(registry);
            }
            Type[] services = serviceTypes.ToArray();
            ServeServiceTypes(contentRoot, GetConfiguredHostPrefix(), allTypes, services);
            Pause($"Gloo server is serving services\r\n\t{services.ToArray().ToDelimited(s => s.FullName, "\r\n\t")}");
        }

        private static CoreServiceRegistrationService GetCoreServiceRegistrationService(ILogger logger, DataSettings dataSettings, IApplicationNameProvider appNameProvider)
        {
            CoreFileService fileService = new CoreFileService(new DaoRepository(dataSettings.GetDatabaseFor(typeof(CoreFileService), $"{nameof(CoreServiceRegistrationService)}_{nameof(CoreFileService)}")));
            AssemblyServiceRepository assRepo = new AssemblyServiceRepository();
            assRepo.Database = dataSettings.GetDatabaseFor(assRepo);
            CoreAssemblyService assemblyService = new CoreAssemblyService(fileService, assRepo, appNameProvider);
            ServiceRegistrationRepository serviceRegistryRepo = new ServiceRegistrationRepository();
            serviceRegistryRepo.Database = dataSettings.GetDatabaseFor(serviceRegistryRepo);
            serviceRegistryRepo.EnsureDaoAssemblyAndSchema();
            CoreServiceRegistrationService serviceRegistryService = new CoreServiceRegistrationService(
                assemblyService,
                serviceRegistryRepo,
                DataSettings.Default.GetGenericDaoRepository(logger),
                new AppConf { Name = appNameProvider.GetApplicationName() }
            );
            return serviceRegistryService;
        }

        public static void ServeServiceTypes(string contentRoot, HostPrefix prefix, ServiceRegistry registry = null, params Type[] serviceTypes)
        {
            BamConf conf = BamConf.Load(contentRoot.Or(defaultContentRoot));
            if(registry != null && GlooServer.ServiceRegistry == null)
            {
                GlooServer.ServiceRegistry = registry;
            }
            glooServer = new GlooServer(conf, GetLogger(), GetArgument("verbose", "Log responses?").IsAffirmative())
            {
                HostPrefixes = new HashSet<HostPrefix> { prefix },
                MonitorDirectories = new string[] { }                
            };
            serviceTypes.Each(t => glooServer.ServiceTypes.Add(t));

            glooServer.Start();
        }
        
        public static void StartGlooServer(ConsoleLogger logger)
        {
            BamConf conf = BamConf.Load(DefaultConfiguration.GetAppSetting(contentRootConfigKey).Or(defaultContentRoot));
            glooServer = new GlooServer(conf, logger, GetArgument("verbose", "Log responses?").IsAffirmative())
            {
                HostPrefixes = new HashSet<HostPrefix> { GetConfiguredHostPrefix() },
                MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";")
            };
            glooServer.Start();
        }

        public static HostPrefix GetConfiguredHostPrefix()
        {
            HostPrefix hostPrefix = new HostPrefix()
            {
                HostName = DefaultConfiguration.GetAppSetting("HostName").Or("localhost"),
                Port = int.Parse(DefaultConfiguration.GetAppSetting("Port", "91000")),
                Ssl = bool.Parse(DefaultConfiguration.GetAppSetting("Ssl"))
            };
            return hostPrefix;
        }

        private static ConsoleLogger GetLogger()
        {
            ConsoleLogger logger = new ConsoleLogger()
            {
                AddDetails = false,
                UseColors = true
            };
            logger.StartLoggingThread();
            return logger;
        }

        private Type GetServiceType(string className)
        {
            Assembly assembly = GetAssembly(className, out Type result);
            return result;
        }

        private Assembly GetAssembly(string className, out Type type)
        {
            type = null;
            Assembly result = null;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                type = GetType(assembly, className);
                if(type != null)
                {
                    result = assembly;
                    break;
                }
            }
            if(result == null)
            {
                string assemblyPath = GetArgument("assemblyPath", true);
                result = Assembly.LoadFrom(assemblyPath);
                type = GetType(result, className);
                if(type == null)
                {
                    type = result.GetType(className);
                }
            }

            return result;
        }

        private Type GetType(Assembly assembly, string className)
        {
            return assembly.GetTypes().Where(t => t.Name.Equals(className) || $"{t.Namespace}.{t.Name}".Equals(className)).FirstOrDefault();
        }
    }
}