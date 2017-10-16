using Bam.Net.Data.Repositories;
using Bam.Net.Services.Distributed.Data;
using System;
using System.Collections.Generic;

namespace Bam.Net.Services.Distributed
{
    public class RepositoryTypeResolver : IRepositoryTypeResolver
    {
        public static string DefaultNamespace = "Bam.Net.Services.Distributed.Data";

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
        public Type ResolveType(string typeName)
        {
            return ResolveType(DefaultNamespace, typeName);
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
