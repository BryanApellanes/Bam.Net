using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public interface IAsyncRepository: IRepository
    {
        Task<T> SaveAsync<T>(T instance) where T : class, new();
        Task<object> SaveAsync(object instance);
        Task<T> CreateAsync<T>(T instance) where T : class, new();
        Task<T> RetrieveAsync<T>(int id) where T : class, new();
        Task<T> RetrieveAsync<T>(long id) where T : class, new();
        Task<IEnumerable<T>> RetrieveAllAsync<T>() where T : class, new();
        Task<IEnumerable<object>> RetrieveAllAsync(Type type);
        Task<IEnumerable<object>> QueryAsync(string propertyName, object propertyValue);
        Task<IEnumerable<T>> QueryAsync<T>(Dictionary<string, object> queryParams) where T : class, new();
        Task<IEnumerable<object>> QueryAsync(Type type, Dictionary<string, object> queryParams);
        Task<object> RetrieveAsync(Type objectType, long id);
        Task<object> RetrieveAsync(Type objectType, string uuid);
        Task<IEnumerable<object>> QueryAsync(dynamic query);
        Task<IEnumerable<T>> QueryAsync<T>(Func<T, bool> query) where T : class, new();
        Task<IEnumerable<object>> QueryAsync(Type type, Func<object, bool> predicate);
        Task<T> UpdateAsync<T>(T toUpdate) where T : new();
        Task<object> UpdateAsync(object toUpdate);
        Task<bool> DeleteAsync<T>(T toDelete) where T : new();
        Task<bool> DeleteAsync(object toDelete);
    }
}
