using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.Incubation;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.CoreServices.ServiceRegistration;
using Bam.Net.CoreServices.ServiceRegistration.Data;
using Bam.Net.CoreServices.ServiceRegistration.Data.Dao.Repository;
using Bam.Net.ServiceProxy;
using System.IO;
using Bam.Net.Yaml;
using Bam.Net.CoreServices.Files;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.CoreServices.AssemblyManagement.Data.Dao.Repository;
using Bam.Net.Data;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// Provides a central point of management for
    /// registering and retrieving services and their
    /// implementations.
    /// </summary>
    [ApiKeyRequired]
    [Proxy("serviceRegistrySvc")]
    [ServiceSubdomain("svcregistry")]
    public class ServiceRegistryService : ApplicationProxyableService
    {
        Dictionary<Type, List<ServiceRegistryContainerRegistrationResult>> _scanResults;
        protected ServiceRegistryService() { }
        public ServiceRegistryService(
            IFileService fileservice, 
            IAssemblyService assemblyService, 
            ServiceRegistryRepository repo, 
            DaoRepository daoRepo, 
            AppConf appConf,
            DefaultDataDirectoryProvider dataSettings = null) : base(daoRepo, appConf)
        {
            FileService = fileservice;
            ServiceRegistryRepository = repo;
            AssemblyService = assemblyService;
            DataSettings = dataSettings ?? DefaultDataDirectoryProvider.Current;
            AssemblySearchPattern = DefaultConfiguration.GetAppSetting("AssemblySearchPattern", "*.dll");
            _scanResults = new Dictionary<Type, List<ServiceRegistryContainerRegistrationResult>>();            
        }

        [Local]
        public static ServiceRegistryService GetLocalServiceRegistryService(string assemblySearchPattern = "*Services.dll", ILogger logger = null)
        {
            return GetLocalServiceRegistryService(DefaultDataDirectoryProvider.Current, DefaultConfigurationApplicationNameProvider.Instance, assemblySearchPattern, logger);
        }

        [Local]
        public static ServiceRegistryService GetLocalServiceRegistryService(DefaultDataDirectoryProvider dataSettings, IApplicationNameProvider appNameProvider, string assemblySearchPattern, ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            DaoRepository repo = dataSettings.GetSysDaoRepository(logger, nameof(FileService));
            FileService fileService = new FileService(repo);
            AssemblyServiceRepository assRepo = new AssemblyServiceRepository();
            assRepo.Database = dataSettings.GetSysDatabaseFor(assRepo);
            assRepo.EnsureDaoAssemblyAndSchema();
            AssemblyService assemblyService = new AssemblyService(DefaultDataDirectoryProvider.Current, fileService, assRepo, appNameProvider);
            ServiceRegistryRepository serviceRegistryRepo = new ServiceRegistryRepository();
            serviceRegistryRepo.Database = dataSettings.GetSysDatabaseFor(serviceRegistryRepo);
            serviceRegistryRepo.EnsureDaoAssemblyAndSchema();
            ServiceRegistryService serviceRegistryService = new ServiceRegistryService(
                fileService,
                assemblyService,
                serviceRegistryRepo,
                dataSettings.GetSysDaoRepository(logger),
                new AppConf { Name = appNameProvider.GetApplicationName() }
            )
            {
                AssemblySearchPattern = assemblySearchPattern
            };
            return serviceRegistryService;
        }

        protected Task ScanForServiceRegistryContainers()
        {
            return Task.Run(() =>
            {
                try
                {
                    DirectoryInfo entryDir = Assembly.GetEntryAssembly().GetFileInfo().Directory;
                    DirectoryInfo sysAssemblies = DataSettings.GetSysAssemblyDirectory();
                    DirectoryInfo[] dirs = new DirectoryInfo[] { entryDir, sysAssemblies };
                    string[] searchPatterns = AssemblySearchPattern.DelimitSplit(",", true);
                    List<FileInfo> files = searchPatterns.SelectMany(searchPattern =>
                    {
                        List<FileInfo> tmp = new List<FileInfo>();
                        foreach (DirectoryInfo dir in dirs)
                        {
                            if (dir.Exists)
                            {
                                tmp.AddRange(dir.GetFiles(searchPattern));
                            }
                        }
                        
                        return tmp;
                    }).ToList();                    
                    
                    _scanResults.Clear();
                    Parallel.ForEach(files, file =>
                    {
                        try
                        {
                            Assembly assembly = Assembly.LoadFile(file.FullName);
                            foreach (Type type in assembly.GetTypes().Where(t => t.HasCustomAttributeOfType<ServiceRegistryContainerAttribute>()))
                            {
                                _scanResults.Add(type, RegisterServiceRegistryContainer(type));
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.AddEntry("Exception scanning for ServiceRegistries in file ({0})", ex, file.FullName);
                        }
                    });
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Exception occurred scanning for service registry containers: {0}", ex, ex.Message);
                }
            });
        }

        static object _scanLock = new object();
        static Task _scanningTask;
        protected Task ScanningTask
        {
            get
            {
                return _scanLock.DoubleCheckLock(ref _scanningTask, ScanForServiceRegistryContainers);
            }
        }

        /// <summary>
        /// The search pattern to use when scanning for service assemblies.
        /// </summary>
        public string AssemblySearchPattern { get; set; }
        public IFileService FileService { get; set; }
        public IDataDirectoryProvider DataSettings { get; set; }
        public ServiceRegistryRepository ServiceRegistryRepository { get; set; }

        public IAssemblyService AssemblyService { get; set; }

        /// <summary>
        /// Get the service registry using the underlying ServcieRegistryLoaderDescriptor
        /// or ServiceRegistryDescriptor with the specified name
        /// </summary>
        /// <param name="registryName"></param>
        /// <returns></returns>
        [Local]
        public ServiceRegistry GetServiceRegistry(string registryName)
        {
            ScanningTask.Wait();
            ServiceRegistryLoaderDescriptor loader = GetServiceRegistryLoaderDescriptor(registryName);            
            if(loader == null)
            {
                ServiceRegistryDescriptor descriptor = GetServiceRegistryDescriptor(registryName);
                return GetServiceRegistry(descriptor);
            }
            return GetServiceRegistry(loader);
        }

        /// <summary>
        /// Local: register the specified Incubator as the registry with the specified name
        /// </summary>
        /// <param name="registryName"></param>
        /// <param name="inc"></param>
        /// <returns></returns>
        [Local]
        public ServiceRegistry RegisterServiceRegistry(string registryName, Incubator inc, bool overwrite)
        {
            ServiceRegistryDescriptor descriptor = ServiceRegistryDescriptor.FromIncubator(registryName, inc);
            RegisterServiceRegistryDescriptor(descriptor, overwrite);
            return GetServiceRegistry(descriptor);
        }

        /// <summary>
        /// Register the containers in the specified assembly and 
        /// return result descriptors for each one found.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        [Local]
        public List<ServiceRegistryContainerRegistrationResult> RegisterServiceRegistryContainers(Assembly assembly)
        {
            List<ServiceRegistryContainerRegistrationResult> results = new List<ServiceRegistryContainerRegistrationResult>();
            foreach (Type type in assembly.GetTypes().Where(t => t.HasCustomAttributeOfType<ServiceRegistryContainerAttribute>()))
            {
                results.AddRange(RegisterServiceRegistryContainer(type));
            }
            return results;
        }

        [Local]
        public List<ServiceRegistryContainerRegistrationResult> RegisterServiceRegistryContainer(Type type)
        {
            List<ServiceRegistryContainerRegistrationResult> results = new List<ServiceRegistryContainerRegistrationResult>();
            foreach (MethodInfo method in type.GetMethods().Where(m => m.HasCustomAttributeOfType<ServiceRegistryLoaderAttribute>()))
            {
                try
                {
                    ServiceRegistryLoaderAttribute loaderAttr = method.GetCustomAttributeOfType<ServiceRegistryLoaderAttribute>();
                    if(loaderAttr.ProcessModes.Contains(Bam.Net.ProcessMode.Current.Mode))
                    {
                        string registryName = loaderAttr.RegistryName ?? $"{type.Namespace}.{type.Name}.{method.Name}";
                        string description = loaderAttr.Description ?? registryName;
                        ServiceRegistry registry = RegisterServiceRegistryLoader(registryName, method, true, description);
                        results.Add(new ServiceRegistryContainerRegistrationResult(registryName, registry, type, method, loaderAttr));
                    }
                }
                catch (Exception ex)
                {
                    results.Add(new ServiceRegistryContainerRegistrationResult(ex) { MethodInfo = method });
                }
            }
            return results;
        }

        [Local]
        public ServiceRegistry RegisterServiceRegistryLoader(MethodInfo method, bool overwrite, string description = null)
        {
            Type type = method.DeclaringType;
            string registryName = $"{type.Namespace}.{type.Name}.{method.Name}";
            if (method.HasCustomAttributeOfType(out ServiceRegistryLoaderAttribute loaderAttr))
            {
                registryName = loaderAttr.RegistryName ?? registryName;
            }
            return RegisterServiceRegistryLoader(registryName, method, overwrite, description);
        }

        /// <summary>
        /// Register the specified method as the ServiceRegistryLoader for the specified registryName
        /// </summary>
        /// <param name="registryName"></param>
        /// <param name="method"></param>
        /// <param name="overwrite"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [Local]
        public ServiceRegistry RegisterServiceRegistryLoader(string registryName, MethodInfo method, bool overwrite, string description = null)
        {
            ServiceRegistryLoaderDescriptor loader = new ServiceRegistryLoaderDescriptor
            {
                Name = registryName,
                Description = description,
                LoaderType = method.DeclaringType.FullName,
                LoaderMethod = method.Name,
                LoaderAssembly = method.DeclaringType.Assembly.FullName
            };
            loader = RegisterServiceRegistryLoaderDescriptor(loader, overwrite);
            return GetServiceRegistry(loader);
        }

        /// <summary>
        /// Gets the service registry for the specified descriptor.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="setProperties">if set to <c>true</c> [set properties].</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [Local]
        public ServiceRegistry GetServiceRegistry(ServiceRegistryLoaderDescriptor descriptor, bool setProperties = false)
        {
            Type loaderType = ResolveType(descriptor.LoaderType, descriptor.LoaderAssembly);
            MethodInfo loader = loaderType.GetMethod(descriptor.LoaderMethod);
            object instance = null;
            if (!loader.IsStatic)
            {
                instance = loaderType.Construct();
            }
            if(loader.ReturnType != typeof(CoreServices.ServiceRegistry))
            {
                throw new InvalidOperationException($"The specified method {descriptor.LoaderMethod} must have a ReturnType of {nameof(CoreServices.ServiceRegistry)}");
            }

            return (CoreServices.ServiceRegistry)loader.Invoke(instance, new object[] { });
        }

        [Local]
        public ServiceRegistry GetServiceRegistry(ServiceRegistryDescriptor descriptor)
        {
            Args.ThrowIfNull(descriptor, "descriptor");
            ServiceRegistryBuilder builder = new ServiceRegistryBuilder();
            foreach (ServiceDescriptor service in descriptor.Services)
            {
                ServiceDefinition definition = ResolveDefinition(service);
                builder.For(definition.ForType);
                builder.Use(definition.UseType);
            }
            return builder.Build();
        }

        /// <summary>
        /// Get the ServiceRegistryDescriptor with the specified name by loading it from the first file found of
        /// {name}.yml, {name}.json in DataSettings.GetSysDataDirectory().  If the 
        /// file is not found and a ServiceRegistryDescriptor with the specified name is found in the 
        /// ServiceRegistryRepository then the file {name}.yml will be written from the ServiceRegistryDescriptor
        /// found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [RoleRequired("/", "Admin")]
        public virtual ServiceRegistryDescriptor GetServiceRegistryDescriptor(string name)
        {            
            Dictionary<string, Func<FileInfo, ServiceRegistryDescriptor>> deserializers = new Dictionary<string, Func<FileInfo, ServiceRegistryDescriptor>>
            {
                {".json", (fi)=> fi.FromJsonFile<ServiceRegistryDescriptor>() },
                {".yml", (fi)=> fi.FromYamlFile<ServiceRegistryDescriptor>() }
            };
            DirectoryInfo systemServiceRegistryDir = DataSettings.GetSysDataDirectory(nameof(ServiceRegistry).Pluralize());
            ServiceRegistryDescriptor fromFile = new ServiceRegistryDescriptor { Name = name };
            ServiceDescriptor[] descriptors = new ServiceDescriptor[] { };
            FileInfo file = new FileInfo(Path.Combine(systemServiceRegistryDir.FullName, $"{name}.json"));
            foreach(string extension in new[] { ".yml", ".json" })
            {
                string path = Path.Combine(systemServiceRegistryDir.FullName, $"{name}{extension}");
                if (File.Exists(path))
                {
                    file = new FileInfo(path);
                    fromFile = deserializers[extension](file);
                    break;
                }
            }

            Dictionary<string, Action<ServiceRegistryDescriptor, FileInfo>> serializers = new Dictionary<string, Action<ServiceRegistryDescriptor, FileInfo>>
            {
                {".json", (sr, fi)=> sr.ToJsonFile(fi) },
                {".yml", (sr, fi)=> sr.ToYamlFile(fi) }
            };
            ServiceRegistryDescriptor fromRepo = ServiceRegistryRepository.ServiceRegistryDescriptorsWhere(c => c.Name == name).FirstOrDefault();
            if (fromRepo != null)
            {                
                HashSet<ServiceDescriptor> svcs = new HashSet<ServiceDescriptor>();
                if (fromFile != null)
                {
                    fromFile.Services?.Each(svc => svcs.Add(svc));
                }
                fromRepo.Services?.Each(svc => svcs.Add(svc));
                fromRepo.Services = svcs.ToList();
                serializers[Path.GetExtension(file.FullName)](fromRepo, file);
            }

            ServiceRegistryDescriptor toSave = fromRepo ?? fromFile;
            if (toSave != null)
            {
                ServiceRegistryRepository.Save(toSave);
            }
            return fromRepo ?? fromFile;
        }

        [RoleRequired("/", "Admin")]
        public virtual ServiceRegistryLoaderDescriptor GetServiceRegistryLoaderDescriptor(string name)
        {
            return ServiceRegistryRepository.ServiceRegistryLoaderDescriptorsWhere(c => c.Name == name).FirstOrDefault();
        }

        [RoleRequired("/", "Admin")]
        public virtual ServiceRegistryDescriptor RegisterServiceRegistryDescriptor(ServiceRegistryDescriptor registry, bool overwrite)
        {
            Args.ThrowIfNull(registry, "registry");

            ServiceRegistryDescriptor existing = ServiceRegistryRepository.ServiceRegistryDescriptorsWhere(c => c.Name == registry.Name).FirstOrDefault();
            Args.ThrowIf(existing != null && !overwrite, "Registry by that name ({0}) already exists", registry.Name);

            if((existing != null && overwrite) || existing == null)
            {
                existing = ServiceRegistryRepository.Save(registry);
            }

            return existing;
        }

        static object _registerLoaderLock = new object();
        /// <summary>
        /// Registers the service registry loader descriptor.
        /// </summary>
        /// <param name="loader">The loader.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <returns></returns>
        [RoleRequired("/", "Admin")]
        public virtual ServiceRegistryLoaderDescriptor RegisterServiceRegistryLoaderDescriptor(ServiceRegistryLoaderDescriptor loader, bool overwrite)
        {
            Args.ThrowIfNull(loader, "loader");
            lock (_registerLoaderLock)
            {
                ServiceRegistryLoaderDescriptor existing = ServiceRegistryRepository.ServiceRegistryLoaderDescriptorsWhere(c => c.Name == loader.Name).FirstOrDefault();
                if (existing != null && overwrite && IsLocked(loader.Name))
                {
                    Args.Throw<InvalidOperationException>("Registry by that name ({0}) is locked", loader.Name);
                }
                Args.ThrowIf(existing != null && !overwrite, "RegistryLoader by that name ({0}) already exists", loader.Name);

                if ((existing != null && overwrite) || existing == null)
                {
                    if(existing != null)
                    {
                        ServiceRegistryRepository.Delete(existing);
                    }
                    existing = ServiceRegistryRepository.Save(loader);
                }

                return existing;
            }            
        }

        /// <summary>
        /// Lock the ServiceRegistry with the specified name.  This effectively
        /// dissables updates
        /// </summary>
        /// <param name="name"></param>
        [RoleRequired("/", "Admin")]
        public virtual void LockServiceRegistry(string name)
        {
            ServiceRegistryLock regLock = ServiceRegistryRepository.OneServiceRegistryLockWhere(c => c.Name == name);
            if(regLock != null)
            {
                regLock.Deleted = null;
            }

            if (regLock == null)
            {
                regLock = new ServiceRegistryLock { Name = name, CreatedBy = UserName };
            }

            regLock.Modified = DateTime.UtcNow;
            regLock.ModifiedBy = UserName;
            ServiceRegistryRepository.Save(regLock);
        }

        /// <summary>
        /// Unlock the specified ServiceRegistry or ServiceRegistryLoader
        /// enabling updates
        /// </summary>
        /// <param name="name"></param>
        [RoleRequired("/", "Admin")]
        public virtual void UnlockServiceRegistry(string name)
        {
            ServiceRegistryLock regLock = ServiceRegistryRepository.OneServiceRegistryLockWhere(c => c.Name == name);
            if(regLock != null)
            {
                regLock.Deleted = DateTime.UtcNow;
                regLock.Modified = regLock.Deleted;
                regLock.ModifiedBy = UserName;
                ServiceRegistryRepository.Save(regLock);
            }
        }

        [RoleRequired("/", "Admin")]
        public virtual bool IsLocked(string name)
        {
            return IsLocked(name, out ServiceRegistryLock theLock);
        }

        [Local]
        public bool IsLocked(string name, out ServiceRegistryLock theLock)
        {
            theLock = ServiceRegistryRepository.OneServiceRegistryLockWhere(c => c.Name == name && c.Deleted == null);
            return theLock != null;
        }

        [Local]
        public override object Clone()
        {
            ServiceRegistryService clone = new ServiceRegistryService(FileService, AssemblyService, ServiceRegistryRepository, DaoRepository, AppConf);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        [Local]
        public ServiceDefinition ResolveDefinition(ServiceDescriptor serviceDescriptor)
        {
            Type forType = ResolveType(serviceDescriptor.GetForTypeIdentifier(ServiceRegistryRepository));
            Type useType = ResolveType(serviceDescriptor.GetUseTypeIdentifier(ServiceRegistryRepository));

            Args.ThrowIfNull(forType, "forType");
            Args.ThrowIfNull(useType, "useType");

            return new ServiceDefinition
            {
                ForAssembly = forType.Assembly,
                UseAssembly = useType.Assembly,
                ForType = forType,
                UseType = useType
            };
        }
                
        [Local]
        public Type ResolveType(ServiceTypeIdentifier typeIdentifier)
        {
            string localAssemblyPath = Path.Combine(DataSettings.GetSysAssemblyDirectory().FullName, typeIdentifier.AssemblyName);
            FileInfo assemblyFile = FileService.RestoreFile(typeIdentifier.AssemblyFileHash, localAssemblyPath);
            Assembly assembly = Assembly.LoadFile(localAssemblyPath);
            Type result = assembly.GetTypes().Where(t => t.Name.Equals(typeIdentifier.TypeName) && t.Namespace.Equals(typeIdentifier.Namespace)).FirstOrDefault();
            return result;
        }

        [Local]
        public Type ResolveType(string typeName, string assemblyName)
        {
            Type forType = Type.GetType(typeName);
            if (forType == null)
            {
                Assembly forAssembly = AssemblyService.ResolveAssembly(assemblyName);
                if (forAssembly == null)
                {
                    throw new InvalidOperationException($"Failed to resolve assembly: {assemblyName}");
                }
                forType = forAssembly.GetType(typeName);
            }

            return forType;
        }
    }
}
