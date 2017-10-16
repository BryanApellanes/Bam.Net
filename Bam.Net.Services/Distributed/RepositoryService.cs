using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Services.Distributed.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Services.Distributed.Data;

namespace Bam.Net.Services.Distributed
{
    [Encrypt]
    [Proxy("repoSvc")]
    public class RepositoryService : ApplicationProxyableService, IDistributedRepository
    {
        protected internal RepositoryService() { }
        public RepositoryService(IRepository repository, IRepositoryTypeResolver typeResolver)
        {
            Repository = repository;
            TypeResolver = typeResolver;
        }
        
        protected internal IRepositoryTypeResolver TypeResolver { get; set; }
        public override object Clone()
        {
            RepositoryService clone = new RepositoryService(Repository, TypeResolver);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public object Create(CreateOperation value)
        {
            Type type;
            object instance;
            ResolveTypeAndInstance(value, out type, out instance);
            return Repository.Create(type, instance);
        }

        public bool Delete(DeleteOperation value)
        {
            Type type;
            object instance;
            ResolveTypeAndInstance(value, out type, out instance);
            return Repository.Delete(type, instance);
        }

        public IEnumerable<object> Query(QueryOperation query)
        {
            Type type = TypeResolver.ResolveType(query);
            return Repository.Query(type, query.Properties.ToDictionary(dp => dp.Name, dp => dp.Value));
        }

        public Task<ReplicationResult> RecieveReplica(ReplicateOperation operation)
        {
            throw new NotImplementedException();
        }

        public object Retrieve(RetrieveOperation value)
        {
            return Repository.Query(value.UniversalIdentifier.ToString(), value.Identifier).FirstOrDefault();
        }

        public object Update(UpdateOperation value)
        {
            Type type;
            object instance;
            ResolveTypeAndInstance(value, out type, out instance);            
            return Repository.Update(type, instance);
        }

        private void ResolveTypeAndInstance(WriteOperation value, out Type type, out object instance)
        {
            type = TypeResolver.ResolveType(value);
            instance = type.Construct();
            value.Properties.Each(new { Instance = instance }, (ctx, dp) =>
            {
                ReflectionExtensions.Property(ctx.Instance, dp.Name, dp.Value);
            });            
        }
    }
}
