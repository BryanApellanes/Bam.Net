using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.Caching;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Schema;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// A repository that is made up of a variety of different
    /// types of repositories used for different purposes such
    /// as reading, writing, caching and backup
    /// </summary>
    public partial class CompositeRepository : AsyncRepository, IHasTypeSchemaTempPathProvider
    {      
        public IRepository BackupRepository { get; private set; }

        public void WireBackup()
        {
            UnwireBackup();
            Dao.AfterCommitAny += Backup;
        }

        public void UnwireBackup()
        {
            Dao.AfterCommitAny -= Backup;
        }

        public Task BackupTask { get; protected set; }

        public IDataDirectoryProvider DataSettings { get; set; }
        public DaoRepository SourceRepository { get; }
        public Database SourceDatabase { get { return SourceRepository?.Database; } }
        public CachingRepository ReadRepository { get;  }
        public HashSet<IRepository> WriteRepositories { get; }
        
        public Func<SchemaDefinition, TypeSchema, string> TypeSchemaTempPathProvider { get; set; }

        string _workspacePath;
        public string WorkspacePath
        {
            get
            {
                return _workspacePath;
            }
            set
            {
                _workspacePath = value;
            }
        }

        public override void AddType(Type type)
        {
            SourceRepository.AddType(type);
            BackupRepository.AddType(type);
            ForEachWriteRepo(repo => repo.AddType(type));
            base.AddType(type);
        }

        public override object Create(Type type, object toCreate)
        {
            return Create(toCreate);
        }

        public override object Create(object toCreate)
        {
            object result = SourceRepository.Create(toCreate);
            CreateInWriteRepos(toCreate);
            return result;
        }

        public override T Create<T>(T toCreate)
        {
            T result = SourceRepository.Create<T>(toCreate);
            CreateInWriteRepos(toCreate);
            return result;
        }
        public override bool Delete(Type type, object toDelete)
        {
            return Delete(toDelete);
        }
        public override bool Delete(object toDelete)
        {
            bool result = SourceRepository.Delete(toDelete);
            DeleteFromWriteRepos(toDelete);
            return result;
        }

        public override bool Delete<T>(T toDelete)
        {
            bool result = SourceRepository.Delete(toDelete);
            DeleteFromWriteRepos<T>(toDelete);
            return result;
        }

        public override IEnumerable<object> Query(dynamic query)
        {
            return ReadRepository.Query(query);
        }

        public override IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters)
        {
            return ReadRepository.Query(type, queryParameters);
        }

        public override IEnumerable<object> Query(Type type, QueryFilter query)
        {
            return ReadRepository.Query(type, query);
        }

        public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
        {
            return ReadRepository.Query(type, predicate);
        }

        public override IEnumerable<object> Query(string propertyName, object propertyValue)
        {
            return ReadRepository.Query(propertyName, propertyValue);
        }

        public override IEnumerable<T> Query<T>(dynamic query)
        {
            return ReadRepository.Query<T>(query);
        }

        public override IEnumerable<T> Query<T>(QueryFilter query)
        {
            return ReadRepository.Query<T>(query);
        }

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            return ReadRepository.Query<T>(queryParameters);
        }

        public override IEnumerable<T> Query<T>(Func<T, bool> query)
        {
            return ReadRepository.Query<T>(query);
        }

        public override object Retrieve(Type objectType, string uuid)
        {
            return ReadRepository.Retrieve(objectType, uuid);
        }

        public override object Retrieve(Type objectType, long id)
        {
            return ReadRepository.Retrieve(objectType, id);
        }
        
        public override object Retrieve(Type objectType, ulong id)
        {
            return ReadRepository.Retrieve(objectType, id);
        }

        public override T Retrieve<T>(long id)
        {
            return ReadRepository.Retrieve<T>(id);
        }

        public override T Retrieve<T>(ulong id)
        {
            return ReadRepository.Retrieve<T>(id);
        }

        public override T Retrieve<T>(int id)
        {
            return ReadRepository.Retrieve<T>(id);
        }
        public override void BatchRetrieveAll(Type type, int batchSize, Action<IEnumerable<object>> processor)
        {
            ReadRepository.BatchRetrieveAll(type, batchSize, processor);
        }
        public override IEnumerable<object> RetrieveAll(Type type)
        {
            return ReadRepository.RetrieveAll(type);
        }

        public override IEnumerable<T> RetrieveAll<T>()
        {
            return ReadRepository.RetrieveAll<T>();
        }
        public override object Update(Type type, object toUpdate)
        {
            return Update(toUpdate);
        }
        public override object Update(object toUpdate)
        {
            object result = SourceRepository.Update(toUpdate);
            UpdateInWriteRepos(toUpdate);
            return result;
        }

        public override T Update<T>(T toUpdate)
        {
            T result = SourceRepository.Update<T>(toUpdate);
            UpdateInWriteRepos<T>(toUpdate);
            return result;
        }

        public override T Retrieve<T>(string uuid)
        {
            return ReadRepository.Retrieve<T>(uuid);
        }
        
        private void CreateInWriteRepos(object toCreate)
        {
            ForEachWriteRepo(repo => repo.Create(toCreate));
        }

        private void CreateInWriteRepos<T>(T toCreate) where T : class, new()
        {
            ForEachWriteRepo(repo => repo.Create<T>(toCreate));
        }

        private void DeleteFromWriteRepos(object toDelete)
        {
            ForEachWriteRepo(repo => repo.Delete(toDelete));
        }

        private void DeleteFromWriteRepos<T>(T toDelete) where T : new()
        {
            ForEachWriteRepo(repo => repo.Delete(toDelete));
        }

        private void UpdateInWriteRepos(object toUpdate)
        {
            ForEachWriteRepo(repo => repo.Update(toUpdate));
        }

        private void UpdateInWriteRepos<T>(T toUpdate) where T : new()
        {
            ForEachWriteRepo(repo => repo.Update<T>(toUpdate));
        }

        private void ForEachWriteRepo(Action<IRepository> eacher)
        {
            Task.Run(() => WriteRepositories.Each(eacher)).ConfigureAwait(false);
        }

        private void Backup(Database database, Dao dao)
        {
            if (database == SourceDatabase)
            {
                BackupTask = Task.Run(() =>
                {
                    object dtoInstance = Dto.Copy(dao);
                    object existing = BackupRepository.Retrieve(dtoInstance.GetType(), dtoInstance.Property<string>("Uuid"));
                    if (existing != null)
                    {
                        BackupRepository.Save(dtoInstance);
                    }
                    else
                    {
                        BackupRepository.Create(dtoInstance);
                    }
                });
                BackupTask.ConfigureAwait(false);
            }
        }

    }
}
