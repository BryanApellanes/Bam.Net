using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.DistributedHashTable.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.CoreServices.DistributedHashTable;
using Bam.Net.CoreServices.DistributedHashTable.Data;

namespace Bam.Net.CoreServices.DistributedHashTable
{
    public class RepositoryTypeResolver : ITypeResolver
    {
        public RepositoryTypeResolver(Repository repo)
        {
            Repository = repo;
            Lookup = new Dictionary<string, Dictionary<string, Type>>();
            SetLookup();
        }

        object _lookupLock = new object();
        protected internal void SetLookup()
        {
            lock (_lookupLock)
            {
                Repository.StorableTypes.Each(type =>
                {
                    Lookup.AddMissing(type.Namespace, new Dictionary<string, Type>());
                    Lookup[type.Namespace].AddMissing(type.Name, type);
                });
            }
        }

        public Repository Repository { get; }
        public Type ResolveType(Operation writeOperation)
        {
            return ResolveType(writeOperation.TypeNamespace, writeOperation.TypeName);
        }
        public Type ResolveType(string nameSpace, string typeName)
        {
            lock (_lookupLock)
            {
                if (Lookup.ContainsKey(nameSpace) && Lookup[nameSpace].ContainsKey(typeName))
                {
                    return Lookup[nameSpace][typeName];
                }
                return null;
            }
        }

        protected Dictionary<string, Dictionary<string, Type>> Lookup { get; set; }
    }
}
