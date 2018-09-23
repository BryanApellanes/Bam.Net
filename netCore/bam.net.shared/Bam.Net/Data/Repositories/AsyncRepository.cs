using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public abstract class AsyncRepository : Repository, IAsyncRepository
    {
        public Task<T> CreateAsync<T>(T instance) where T : class, new()
        {
            return Task.Run(() => Create(instance));
        }

        public Task<bool> DeleteAsync(object toDelete)
        {
            return Task.Run(() => Delete(toDelete));
        }

        public Task<bool> DeleteAsync<T>(T toDelete) where T : new()
        {
            return Task.Run(() => Delete(toDelete));
        }

        public Task<IEnumerable<object>> QueryAsync(dynamic query)
        {
            return Task.Run((Func<IEnumerable<object>>)(() => Query(query)));
        }

        public Task<IEnumerable<object>> QueryAsync(Type type, Dictionary<string, object> queryParams)
        {
            return Task.Run(() => Query(type, queryParams));
        }

        public Task<IEnumerable<object>> QueryAsync(Type type, Func<object, bool> predicate)
        {
            return Task.Run(() => Query(type, predicate));
        }

        public Task<IEnumerable<object>> QueryAsync(string propertyName, object propertyValue)
        {
            return Task.Run(() => Query(propertyName, propertyValue));
        }

        public Task<IEnumerable<T>> QueryAsync<T>(Func<T, bool> query) where T : class, new()
        {
            return Task.Run(() => Query(query));
        }

        public Task<IEnumerable<T>> QueryAsync<T>(Dictionary<string, object> queryParams) where T : class, new()
        {
            return Task.Run(() => Query<T>(queryParams));
        }

        public Task<IEnumerable<object>> RetrieveAllAsync(Type type)
        {
            return Task.Run(() => RetrieveAll(type));
        }

        public Task<IEnumerable<T>> RetrieveAllAsync<T>() where T : class, new()
        {
            return Task.Run(() => RetrieveAll<T>());
        }

        public Task<object> RetrieveAsync(Type objectType, string uuid)
        {
            return Task.Run(() => Retrieve(objectType, uuid));
        }

        public Task<object> RetrieveAsync(Type objectType, long id)
        {
            return Task.Run(() => Retrieve(objectType, id));
        }

        public Task<T> RetrieveAsync<T>(long id) where T : class, new()
        {
            return Task.Run(() => Retrieve<T>(id));
        }

        public Task<T> RetrieveAsync<T>(int id) where T : class, new()
        {
            return Task.Run(() => Retrieve<T>(id));
        }

        public Task<object> SaveAsync(object instance)
        {
            return Task.Run(() => Save(instance));
        }

        public Task<T> SaveAsync<T>(T instance) where T : class, new()
        {
            return Task.Run(() => Save<T>(instance));
        }

        public Task<object> UpdateAsync(object toUpdate)
        {
            return Task.Run(() => Update(toUpdate));
        }

        public Task<T> UpdateAsync<T>(T toUpdate) where T : new()
        {
            return Task.Run(() => Update<T>(toUpdate));
        }
    }
}
