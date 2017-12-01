using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Incubation;
using Bam.Net.Yaml;

namespace Bam.Net.CoreServices.ServiceRegistration.Data
{
    /// <summary>
    /// A serializable descriptor for a ServiceRegistry
    /// </summary>
    [Serializable]
    public class ServiceRegistryDescriptor: AuditRepoData
    {
        public ServiceRegistryDescriptor() { }
        public ServiceRegistryDescriptor(string name, string description) { }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<ServiceDescriptor> Services { get; set; }

        public ServiceDescriptor AddService(Type forType, Type useType)
        {
            if(Services == null)
            {
                Services = new List<ServiceDescriptor>();
            }
            ServiceDescriptor add = new ServiceDescriptor(forType, useType);
            Services.Add(add);
            SetSequenceValues();
            return add;
        }
        public static ServiceRegistryDescriptor FromIncubator(string name, Incubator incubator, string desciption = null)
        {
            ServiceRegistryDescriptor result = new ServiceRegistryDescriptor(name, desciption);
            foreach(Type type in incubator.MappedTypes)
            {
                result.AddService(type, incubator[type].GetType());
            }
            return result;
        }

        public void ToYamlFile(string filePath)
        {
            List<dynamic> types = new List<dynamic>();
            foreach(ServiceDescriptor sd in Services)
            {
                types.Add(new { ForAssembly = sd.ForAssembly, ForType = sd.ForType, UseAssembly = sd.UseAssembly, UseType = sd.UseType});
            }
            types.ToYamlFile(filePath);
        }

        private void SetSequenceValues()
        {
            if(Services != null)
            {
                for(int i = 0; i < Services.Count; i++)
                {
                    Services[i].SequenceNumber = i + 1;
                }
            }
        }
    }
}
