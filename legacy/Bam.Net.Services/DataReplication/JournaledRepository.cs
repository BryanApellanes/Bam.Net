using Bam.Net.Caching;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Services.DataReplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class JournaledRepository : Repository
    {
        protected JournaledRepository(JournalManager journalManager, DaoRepository sourceRepo, ILogger logger = null)
        {
            JournalManager = journalManager;
            Logger = logger;            
            Repository = new CachingRepository(sourceRepo, logger);
        }

        public JournaledRepository(JournalManager journalManager, ILogger logger = null) : this(journalManager, new DaoRepository(), logger)
        { }

        public CachingRepository Repository { get; set; }
        public JournalManager JournalManager { get; private set; }

        public override void BatchRetrieveAll(Type dtoOrPocoType, int batchSize, Action<IEnumerable<object>> processor)
        {
            Repository.BatchRetrieveAll(dtoOrPocoType, batchSize, processor);
        }

        public override T Create<T>(T toCreate)
        {
            KeyHashAuditRepoData data = CastOrDie(toCreate);
            AutoResetEvent blocker = new AutoResetEvent(false);
            T result = toCreate;
            JournalManager.Enqueue(data, (je) =>
            {
                result = Repository.Save<T>(toCreate);
                blocker.Set();
            });
            blocker.WaitOne();
            return result;
        }

        public override object Create(object toCreate)
        {
            KeyHashAuditRepoData data = CastOrDie(toCreate);
            AutoResetEvent blocker = new AutoResetEvent(false);
            object result = toCreate;
            JournalManager.Enqueue(data, (je) =>
            {
                result = Repository.Save(toCreate);
                blocker.Set();
            });
            blocker.WaitOne();
            return result;
        }                

        public override object Create(Type type, object toCreate)
        {
            KeyHashAuditRepoData data = CastOrDie(toCreate);
            AutoResetEvent blocker = new AutoResetEvent(false);
            object result = toCreate;
            JournalManager.Enqueue(data, (je) =>
            {
                result = Repository.Save(type, toCreate);
                blocker.Set();
            });
            blocker.WaitOne();
            return result;
        }
        
        public override T Update<T>(T toUpdate)
        {
            KeyHashAuditRepoData data = CastOrDie(toUpdate);
            AutoResetEvent blocker = new AutoResetEvent(false);
            T result = toUpdate;
            JournalManager.Enqueue(data, (je) =>
            {
                result = Repository.Update<T>(toUpdate);
                blocker.Set();
            });
            blocker.WaitOne();
            return result;
        }

        public override object Update(object toUpdate)
        {
            KeyHashAuditRepoData data = CastOrDie(toUpdate);
            AutoResetEvent blocker = new AutoResetEvent(false);
            object result = toUpdate;
            JournalManager.Enqueue(data, (je) =>
            {
                result = Repository.Update(toUpdate);
                blocker.Set();
            });
            blocker.WaitOne();
            return result;
        }

        public override object Update(Type type, object toUpdate)
        {
            KeyHashAuditRepoData data = CastOrDie(toUpdate);
            AutoResetEvent blocker = new AutoResetEvent(false);
            object result = toUpdate;
            JournalManager.Enqueue(data, (je) =>
            {
                result = Repository.Update(type, toUpdate);
                blocker.Set();
            });
            blocker.WaitOne();
            return result;
        }

        public override bool Delete<T>(T toDelete)
        {
            KeyHashAuditRepoData data = CastOrDie(toDelete);
            AutoResetEvent blocker = new AutoResetEvent(false);
            bool? result = false;
            JournalManager.Enqueue(data, (je) =>
            {
                result = Repository.Delete<T>(toDelete);
                blocker.Set();
            });
            blocker.WaitOne();
            return result.Value;
        }

        public override bool Delete(object toDelete)
        {
            KeyHashAuditRepoData data = CastOrDie(toDelete);
            AutoResetEvent blocker = new AutoResetEvent(false);
            bool? result = false;
            JournalManager.Enqueue(data, (je) =>
            {
                result = Repository.Delete(toDelete);
                blocker.Set();
            });
            blocker.WaitOne();
            return result.Value;
        }

        public override bool Delete(Type type, object toDelete)
        {
            KeyHashAuditRepoData data = CastOrDie(toDelete);
            AutoResetEvent blocker = new AutoResetEvent(false);
            bool? result = false;
            JournalManager.Enqueue(data, (je) =>
            {
                result = Repository.Delete(type, toDelete);
                blocker.Set();
            });
            blocker.WaitOne();
            return result.Value;
        }

        public override IEnumerable<object> Query(string propertyName, object propertyValue)
        {
            return Repository.Query(propertyName, propertyValue);
        }

        public override IEnumerable<T> Query<T>(Func<T, bool> query)
        {
            return Repository.Query<T>(query);
        }

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            return Repository.Query<T>(queryParameters);
        }

        public override IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters)
        {
            return Repository.Query(type, queryParameters);
        }

        public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
        {
            return Repository.Query(type, predicate);
        }

        public override IEnumerable<T> Query<T>(QueryFilter query)
        {
            return Repository.Query<T>(query);
        }

        public override IEnumerable<object> Query(Type type, QueryFilter query)
        {
            return Repository.Query(type, query);
        }

        public override T Retrieve<T>(int id)
        {
            return Repository.Retrieve<T>(id);
        }

        public override T Retrieve<T>(long id)
        {
            return Repository.Retrieve<T>(id);
        }

        public override T Retrieve<T>(ulong id)
        {
            return Repository.Retrieve<T>(id);
        }

        public override T Retrieve<T>(string uuid)
        {
            return Repository.Retrieve<T>(uuid);
        }

        public override object Retrieve(Type objectType, long id)
        {
            return Repository.Retrieve(objectType, id);
        }

        public override object Retrieve(Type objectType, ulong id)
        {
            return Repository.Retrieve(objectType, id);
        }

        public override object Retrieve(Type objectType, string uuid)
        {
            return Repository.Retrieve(objectType, uuid);
        }

        public override IEnumerable<T> RetrieveAll<T>()
        {
            return Repository.RetrieveAll<T>();
        }

        public override IEnumerable<object> RetrieveAll(Type type)
        {
            return RetrieveAll(type);
        }

        private static KeyHashAuditRepoData CastOrDie<T>(T toCreate)
        {
            if (!toCreate.TryCast(out KeyHashAuditRepoData data))
            {
                string name = toCreate == null ? "[null]" : toCreate.GetType().Name;
                throw new InvalidOperationException($"Could not cast specified instance to {nameof(KeyHashAuditRepoData)}, was {name}");
            }

            return data;
        }
    }
}
