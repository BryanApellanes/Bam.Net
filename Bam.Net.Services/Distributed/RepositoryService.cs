﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Services.Distributed.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.CoreServices.ApplicationRegistration;

namespace Bam.Net.Services.Distributed
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
            return Repository.StorableTypes.Select(t => $"{t.Namespace}.{t.Name}").ToArray();
        }

        public virtual IEnumerable<object> BatchAll(string type, int batchSize)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            RepositoryService clone = new RepositoryService(TypeResolver);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
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

        public virtual ReplicateOperation Replicate(ReplicateOperation operation)
        {
            ReplicateOperation result = DaoRepository.Save(operation);
            Task.Run(() => (ReplicateOperation)operation.Execute(this));
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
