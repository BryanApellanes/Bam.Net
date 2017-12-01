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

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// Provides a central point of management for
    /// registering and retrieving services and their
    /// implementations
    /// </summary>
    [ApiKeyRequired]
    [Proxy("serviceRegistrySvc")]
    [ServiceSubdomain("svcregistry")]
    public class CoreServiceRegistrationService : ProxyableService
    {
        public CoreServiceRegistrationService(IAssemblyService assemblyService, ServiceRegistrationRepository repo, DaoRepository daoRepo, AppConf appConf, DataSettings dataSettings = null) : base(daoRepo, appConf)
        {
            ServiceRegistryRepository = repo;
            AssemblyService = assemblyService;
            DataSettings = dataSettings ?? DataSettings.Default;
        }

        public DataSettings DataSettings { get; set; }
        public ServiceRegistrationRepository ServiceRegistryRepository { get; set; }

        public IAssemblyService AssemblyService { get; set; }

        /// <summary>
        /// Get the service registry using the underlying ServcieRegistryLoaderDescriptor
        /// or ServiceRegistryDescriptor with the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Local]
        public ServiceRegistry GetServiceRegistry(string name)
        {
            ServiceRegistryLoaderDescriptor loader = GetServiceRegistryLoaderDescriptor(name);            
            if(loader == null)
            {
                ServiceRegistryDescriptor descriptor = GetServiceRegistryDescriptor(name);
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
        /// return result descriptors for each one found
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        [Local]
        public List<RegisterServiceRegistryContainerResult> RegisterServiceRegistryContainers(Assembly assembly)
        {
            List<RegisterServiceRegistryContainerResult> results = new List<RegisterServiceRegistryContainerResult>();
            foreach (Type type in assembly.GetTypes().Where(t => t.HasCustomAttributeOfType<ServiceRegistryContainerAttribute>()))
            {
                results.AddRange(RegisterServiceRegistryContainer(type));
            }
            return results;
        }

        [Local]
        public List<RegisterServiceRegistryContainerResult> RegisterServiceRegistryContainer(Type type)
        {
            List<RegisterServiceRegistryContainerResult> results = new List<RegisterServiceRegistryContainerResult>();
            foreach (MethodInfo method in type.GetMethods().Where(m => m.HasCustomAttributeOfType<ServiceRegistryLoaderAttribute>()))
            {
                try
                {
                    ServiceRegistryLoaderAttribute loaderAttr = method.GetCustomAttributeOfType<ServiceRegistryLoaderAttribute>();
                    string registryName = loaderAttr.RegistryName ?? $"{type.Namespace}.{type.Name}.{method.Name}";
                    string description = loaderAttr.Description ?? registryName;
                    ServiceRegistry registry = RegisterServiceRegistryLoader(registryName, method, true, description);
                    results.Add(new RegisterServiceRegistryContainerResult(registryName, registry, type, method, loaderAttr));
                }
                catch (Exception ex)
                {
                    results.Add(new RegisterServiceRegistryContainerResult(ex));
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
            ServiceRegistrationBuilder builder = new ServiceRegistrationBuilder();
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
            ServiceRegistryDescriptor fromFile = null;
            FileInfo file = null;
            foreach(string extension in new[] { ".yml", ".json" })
            {
                string path = Path.Combine(systemServiceRegistryDir.FullName, $"{name}{extension}");
                if (File.Exists(path))
                {
                    file = new FileInfo(path);
                    fromFile = deserializers[extension](file);
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
                    fromFile.Services.Each(svc => svcs.Add(svc));
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

        [RoleRequired("/", "Admin")]
        public virtual ServiceRegistryLoaderDescriptor RegisterServiceRegistryLoaderDescriptor(ServiceRegistryLoaderDescriptor loader, bool overwrite)
        {
            Args.ThrowIfNull(loader, "loader");
            ServiceRegistryLoaderDescriptor existing = ServiceRegistryRepository.ServiceRegistryLoaderDescriptorsWhere(c => c.Name == loader.Name).FirstOrDefault();
            if(existing != null && overwrite && IsLocked(loader.Name))
            {
                Args.Throw<InvalidOperationException>("Registry by that name ({0}) is locked", loader.Name);
            }
            Args.ThrowIf(existing != null && !overwrite, "RegistryLoader by that name ({0}) already exists", loader.Name);

            if ((existing != null && overwrite) || existing == null)
            {
                existing = ServiceRegistryRepository.Save(loader);
            }

            return existing;
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
            CoreServiceRegistrationService clone = new CoreServiceRegistrationService(AssemblyService, ServiceRegistryRepository, DaoRepository, AppConf);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        [Local]
        public ServiceDefinition ResolveDefinition(ServiceDescriptor serviceDescriptor)
        {
            Type forType = ResolveType(serviceDescriptor.ForType, serviceDescriptor.ForAssembly);
            Type useType = ResolveType(serviceDescriptor.UseType, serviceDescriptor.UseAssembly);

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
