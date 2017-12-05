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
    /// A persistable descriptor for an interface
    /// class pair used as a service.
    /// </summary>
    [Serializable]
    public class ServiceDescriptor: AuditRepoData
    {
        public ServiceDescriptor()
        {
        }

        public ServiceDescriptor(Type forType, Type useType)
        {
            ForTypeIdentifier = ServiceTypeIdentifier.FromType(forType);
            UseTypeIdentifier = ServiceTypeIdentifier.FromType(useType);
            ForTypeDurableHash = ForTypeIdentifier.DurableHash;
            UseTypeDurableHash = UseTypeIdentifier.DurableHash;
            ForTypeDurableSecondaryHash = ForTypeIdentifier.DurableSecondaryHash;
            UseTypeDurableSecondaryHash = UseTypeIdentifier.DurableSecondaryHash;
        }
        
        public int SequenceNumber { get; set; }
        public int ForTypeDurableHash { get; set; }
        public int ForTypeDurableSecondaryHash { get; set; }

        public int UseTypeDurableHash { get; set; }
        public int UseTypeDurableSecondaryHash { get; set; }
        
        protected ServiceTypeIdentifier ForTypeIdentifier { get; set; }
        protected ServiceTypeIdentifier UseTypeIdentifier { get; set; }

        public virtual List<ServiceRegistryDescriptor> ServiceRegistry { get; set; }

        public new ServiceDescriptor Save(IRepository repo)
        {
            if(ForTypeIdentifier != null)
            {
                ForTypeIdentifier.Save(repo);
            }
            if(UseTypeIdentifier != null)
            {
                UseTypeIdentifier.Save(repo);
            }
            return repo.Save(this);
        }

        public ServiceTypeIdentifier GetForTypeIdentifier(IRepository repo)
        {
            return GetTypeIdentifier(repo, ForTypeDurableHash);
        }

        public ServiceTypeIdentifier GetUseTypeIdentifier(IRepository repo)
        {
            return GetTypeIdentifier(repo, UseTypeDurableHash);
        }

        public ServiceTypeIdentifier GetTypeIdentifier(IRepository repo, int durableHashCode)
        {
            var result = repo.Query(typeof(ServiceTypeIdentifier), new { DurableHashCode = durableHashCode }).FirstOrDefault();
            if(result != null)
            {
                return result.CopyAs<ServiceTypeIdentifier>();
            }
            return null;
        }

        public override bool Equals(object obj)
        {
            if(obj is ServiceDescriptor sd)
            {
                return sd.ForTypeDurableHash.Equals(ForTypeDurableHash) &&
                    sd.UseTypeDurableHash.Equals(UseTypeDurableHash);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return $"{SequenceNumber}{ForTypeDurableHash}{UseTypeDurableHash}".ToSha1Int();
        }
    }
}
