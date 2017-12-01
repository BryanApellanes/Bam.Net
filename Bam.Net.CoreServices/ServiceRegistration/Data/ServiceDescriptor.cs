using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.CoreServices.Files;

namespace Bam.Net.CoreServices.ServiceRegistration.Data
{
    /// <summary>
    /// A persistable descriptor for a .Net Type
    /// </summary>
    [Serializable]
    public class ServiceDescriptor: AuditRepoData
    {
        public ServiceDescriptor()
        {
        }

        public ServiceDescriptor(Type forType, Type useType)
        {
            ForType = forType.FullName;
            ForAssembly = forType.Assembly.FullName;
            UseType = useType.FullName;
            UseAssembly = useType.Assembly.FullName;
        }

        string _description;
        public string Description
        {
            get
            {
                return _description ?? $"For<{ForType}>().Use<{UseType}>()";
            }
            set
            {
                _description = value;
            }
        }
        public int SequenceNumber { get; set; }
        public string ForType { get; set; }
        public string ForAssembly { get; set; }
        public string UseType { get; set; }
        public string UseAssembly { get; set; }

        public virtual List<ServiceRegistryDescriptor> ServiceRegistry { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is ServiceDescriptor sd)
            {
                return sd.ForType.Equals(ForType) &&
                    sd.ForAssembly.Equals(ForAssembly) &&
                    sd.UseType.Equals(UseType) &&
                    sd.UseAssembly.Equals(UseAssembly);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return $"{SequenceNumber}{ForType}{ForAssembly}{UseType}{UseAssembly}".ToSha1Int();
        }
    }
}
