using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;

namespace Bam.Net.CoreServices.ServiceRegistration.Data
{
    /// <summary>
    /// A descriptor providing meta data about a method used to load a service registry.
    /// </summary>
    /// <seealso cref="Bam.Net.Data.Repositories.AuditRepoData" />
    [Serializable]
    public class ServiceRegistryLoaderDescriptor: AuditRepoData
    {
        public ServiceRegistryLoaderDescriptor() { }
        public ServiceRegistryLoaderDescriptor(Assembly ass)
        {
            Type[] types = ass.GetTypes().Where(t => t.HasCustomAttributeOfType<ServiceRegistryContainerAttribute>()).ToArray();
            Type toUse = types.FirstOrDefault();
            if(types.Length > 1)
            {
                Log.Warn("The specified assembly {0} contains more than one type addorned with a {1} attribute, registering {2}", ass.GetFilePath(), nameof(ServiceRegistryContainerAttribute), toUse.FullName);
            }
            Initialize(toUse);
        }
        public ServiceRegistryLoaderDescriptor(Type type, string name = null, string description = null)
        {
            Initialize(type, name, description);
        }

        private void Initialize(Type type, string name = null, string description = null)
        {
            LoaderType = type.FullName;
            LoaderAssembly = type.Assembly.FullName;
            MethodInfo loaderMethodInfo = type.GetMethods().Where(mi => mi.HasCustomAttributeOfType(out ServiceRegistryLoaderAttribute a) && a.RegistryName.Equals(name)).FirstOrDefault();
            if (loaderMethodInfo == null)
            {
                loaderMethodInfo = type.GetFirstMethodWithAttributeOfType<ServiceRegistryLoaderAttribute>();
                if(loaderMethodInfo == null)
                {
                    throw new InvalidOperationException($"The specified type doesn't have a method addorned with an attribute of {nameof(ServiceRegistryLoaderAttribute)}");
                }
            }
            ServiceRegistryLoaderAttribute attr = loaderMethodInfo.GetCustomAttributeOfType<ServiceRegistryLoaderAttribute>();
            Name = string.IsNullOrEmpty(name) ? (string.IsNullOrEmpty(attr.RegistryName) ?  $"{type.Assembly.GetFileInfo().Name}_{type.Name}" : attr.RegistryName): name;
            Description = string.IsNullOrEmpty(description) ? attr.Description: description;
        }

        /// <summary>
        /// Gets or sets the name of the registry.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets an optional description.  Corresponds to the description of
        /// the ServiceRegistryLoaderAttribute if specified.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the loader.
        /// </summary>
        /// <value>
        /// The type of the loader.
        /// </value>
        public string LoaderType { get; set; }

        /// <summary>
        /// Gets or sets the loader assembly.
        /// </summary>
        /// <value>
        /// The loader assembly.
        /// </value>
        public string LoaderAssembly { get; set; }

        /// <summary>
        /// Gets or sets the loader method.
        /// </summary>
        /// <value>
        /// The loader method.
        /// </value>
        public string LoaderMethod { get; set; }
    }
}
