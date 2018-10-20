using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Services.DataReplication.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication
{
    [Encrypt]
    [Proxy("repoSvc")]
    public class RepositoryService : AsyncProxyableService, IDistributedRepository
    {
        protected internal RepositoryService() { }
        public RepositoryService(IRepositoryTypeResolver typeResolver)
        {
            Repository = DaoRepository;
            TypeResolver = typeResolver;
        }
        
        protected internal IRepositoryTypeResolver TypeResolver { get; set; }
        
        public virtual string[] GetTypes()
        {
            return DaoRepository.StorableTypes.Select(t => $"{t.Namespace}.{t.Name}").ToArray();
        }

        public virtual IEnumerable<object> NextSet(ReplicationOperation operation)
        {
            Type type = TypeResolver.ResolveType(operation);
            yield return DaoRepository.Top(operation.BatchSize, type, QueryFilter.Where("Cuid") > operation.FromCuid);
        }

        public override object Clone()
        {
            RepositoryService clone = new RepositoryService(TypeResolver);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public virtual object Save(SaveOperation value)
        {
            ResolveTypeAndInstance(value, out Type type, out object instance);
            return Repository.Save(instance);
        }

        public virtual object Create(CreateOperation value)
        {
            ResolveTypeAndInstance(value, out Type type, out object instance);
            return Repository.Create(type, instance);
        }

        public virtual bool Delete(DeleteOperation value)
        {
            ResolveTypeAndInstance(value, out Type type, out object instance);
            return Repository.Delete(type, instance);
        }

        public virtual IEnumerable<object> Query(QueryOperation query)
        {
            Type type = TypeResolver.ResolveType(query);
            return Repository.Query(type, query.Properties.ToDictionary(dp => dp.Name, dp => dp.Value));
        }

        public virtual ReplicationOperation Replicate(ReplicationOperation operation)
        {
            ReplicationOperation result = DaoRepository.Save(operation);
            Task.Run(() => (ReplicationOperation)operation.Execute(this));
            return result;
        }

        public virtual object Retrieve(RetrieveOperation value)
        {
            return Repository.Query(value.UniversalIdentifier.ToString(), value.Identifier).FirstOrDefault();
        }

        public virtual object Update(UpdateOperation value)
        {
            ResolveTypeAndInstance(value, out Type type, out object instance);
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
