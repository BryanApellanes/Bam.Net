using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy.Secure;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Bam.Net.CoreServices
{
    [Encrypt]
    [Proxy("repo")]
    [ApiKeyRequired]
    public class RepositoryService: Repository
    {
        IRepository _localRepo;
        List<IRepository> _remoteRepos;

        public RepositoryService(IRepository repo)
        {
            _localRepo = repo;
            _remoteRepos = new List<IRepository>();
        }

        public override object Create(object toCreate)
        {
            throw new NotImplementedException();
        }

        public override T Create<T>(T toCreate)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(object toDelete)
        {
            throw new NotImplementedException();
        }

        public override bool Delete<T>(T toDelete)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Query(dynamic query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable Query(Type type, Dictionary<string, object> queryParameters)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Query(string propertyName, object propertyValue)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> Query<T>(dynamic query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> Query<T>(Func<T, bool> query)
        {
            throw new NotImplementedException();
        }

        public override object Retrieve(Type objectType, string uuid)
        {
            throw new NotImplementedException();
        }

        public override object Retrieve(Type objectType, long id)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(long id)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> RetrieveAll(Type type)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override object Update(object toUpdate)
        {
            throw new NotImplementedException();
        }

        public override T Update<T>(T toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
