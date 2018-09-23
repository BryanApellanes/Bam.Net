using System;
using System.Collections;
using System.Collections.Generic;

namespace Bam.Net.Data.Repositories
{
    public interface IExtendedCrudProvider: IReflectionCrudProvider
    {
        T Create<T>(T toCreate) where T : class, new();
        bool Delete<T>(T toDelete) where T : new();
        IEnumerable<object> Query(Type type, QueryFilter query);
        IEnumerable<T> Query<T>(QueryFilter query) where T : class, new();
        T Retrieve<T>(string uuid) where T : class, new();
        object Retrieve(Type objectType, long id);
        object Retrieve(Type objectType, ulong id);
        T Retrieve<T>(long id) where T : class, new();
        T Retrieve<T>(ulong id) where T : class, new();
        T Retrieve<T>(int id) where T : class, new();
        IEnumerable<object> RetrieveAll(Type type);
        IEnumerable<T> RetrieveAll<T>() where T : class, new();
        IEnumerable<T> SaveCollection<T>(IEnumerable<T> values) where T : class, new();
        T Save<T>(T toSave) where T : class, new();
        T Update<T>(T toUpdate) where T : new();
    }
}