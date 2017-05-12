using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Distributed.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Services.Distributed;
using Bam.Net.Services.Distributed.Data;

namespace Bam.Net.Services.Distributed
{
    public class RepositoryTypeResolver : ITypeResolver
    {
        public RepositoryTypeResolver(Repository repo)
        {
            Repository = repo;
            Lookup = new Dictionary<string, Dictionary<string, Type>>();
            repo.StorableTypes.Each(type =>
            {
                Lookup.AddMissing(type.Namespace, new Dictionary<string, Type>());
                Lookup[type.Namespace].AddMissing(type.Name, type);
            });
        }
        
        public Repository Repository { get; }
        public Type ResolveType(Operation writeOperation)
        {
            return ResolveType(writeOperation.TypeNamespace, writeOperation.TypeName);
        }
        public Type ResolveType(string nameSpace, string typeName)
        {
            if (Lookup.ContainsKey(nameSpace) && Lookup[nameSpace].ContainsKey(typeName))
            {
                return Lookup[nameSpace][typeName];
            }
            return null;
        }

        protected Dictionary<string, Dictionary<string, Type>> Lookup { get; set; }
    }
}
