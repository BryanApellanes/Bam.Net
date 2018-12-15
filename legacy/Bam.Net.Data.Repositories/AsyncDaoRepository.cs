using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// An asynchronous wrapper for a DaoRepository
    /// </summary>
    public class AsyncDaoRepository : AsyncRepository
    {
        public AsyncDaoRepository(DaoRepository daoRepository)
        {
            DaoRepository = daoRepository;
        }

        public AsyncDaoRepository(Database database, ILogger logger = null) 
            : this(new DaoRepository(database, logger))
        {
        }

        public override void AddType(Type type)
        {
            DaoRepository.AddType(type);
            base.AddType(type);
        }
        public DaoRepository DaoRepository { get; set; }

        public override object Create(Type type, object toCreate)
        {
            return DaoRepository.Create(type, toCreate);
        }
        public override object Create(object toCreate)
        {
            return DaoRepository.Create(toCreate);
        }
        
        public override IEnumerable<T> Query<T>(QueryFilter query)
        {
            return DaoRepository.Query<T>(query);
        }

        public override IEnumerable<object> Query(Type type, QueryFilter query)
        {
            return DaoRepository.Query(type, query);
        }


        public override T Create<T>(T toCreate)
        {
            return DaoRepository.Create(toCreate);
        }
        public override bool Delete(Type type, object toDelete)
        {
            return DaoRepository.Delete(type, toDelete);
        }
        public override bool Delete(object toDelete)
        {
            return DaoRepository.Delete(toDelete);
        }

        public override bool Delete<T>(T toDelete)
        {
            return DaoRepository.Delete<T>(toDelete);
        }

        public override IEnumerable<object> Query(dynamic query)
        {
            return DaoRepository.Query(query);
        }

        public override IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters)
        {
            return DaoRepository.Query(type, queryParameters);
        }

        public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
        {
            return DaoRepository.Query(type, predicate);
        }

        public override IEnumerable<object> Query(string propertyName, object propertyValue)
        {
            return DaoRepository.Query(propertyName, propertyValue);
        }

        public override IEnumerable<T> Query<T>(dynamic query)
        {
            return DaoRepository.Query<T>(query);
        }

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            return DaoRepository.Query<T>(queryParameters);
        }

        public override IEnumerable<T> Query<T>(Func<T, bool> query)
        {
            return DaoRepository.Query<T>(query);
        }

        public override object Retrieve(Type objectType, string uuid)
        {
            return DaoRepository.Retrieve(objectType, uuid);
        }

        public override object Retrieve(Type objectType, long id)
        {
            return DaoRepository.Retrieve(objectType, id);
        }

        public override object Retrieve(Type objectType, ulong id)
        {
            return DaoRepository.Retrieve(objectType, id);
        }

        public override T Retrieve<T>(long id)
        {
            return DaoRepository.Retrieve<T>(id);
        }

        public override T Retrieve<T>(ulong id)
        {
            return DaoRepository.Retrieve<T>(id);
        }

        public override T Retrieve<T>(int id)
        {
            return DaoRepository.Retrieve<T>(id);
        }
        public override T Retrieve<T>(string uuid)
        {
            return DaoRepository.Retrieve<T>(uuid);
        }
        public override IEnumerable<object> RetrieveAll(Type type)
        {
            return DaoRepository.RetrieveAll(type);
        }

        public override void BatchRetrieveAll(Type type, int batchSize, Action<IEnumerable<object>> processor)
        {
            DaoRepository.BatchRetrieveAll(type, batchSize, processor);
        }

        public override IEnumerable<T> RetrieveAll<T>()
        {
            return DaoRepository.RetrieveAll<T>();
        }

        public override object Update(object toUpdate)
        {
            return DaoRepository.Update(toUpdate);
        }
        public override object Update(Type type, object toUpdate)
        {
            return DaoRepository.Update(type, toUpdate);
        }
        public override T Update<T>(T toUpdate)
        {
            return DaoRepository.Update<T>(toUpdate);
        }
    }
}
