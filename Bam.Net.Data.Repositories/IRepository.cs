/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using System.Reflection;
using System.Collections;

namespace Bam.Net.Data.Repositories
{
	public interface IRepository : ILoggable, IExtendedCrudProvider
    {
		IEnumerable<Type> StorableTypes { get; }
		void AddType(Type type);
        void AddNamespace(Type type);
        void AddNamespace(Assembly assembly, string ns);
		void AddTypes(IEnumerable<Type> types);
		void AddType<T>();
        /// <summary>
        /// When implemented in a derived class, calls
        /// Create or Update as appropriate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toSave"></param>
        /// <returns></returns>
        T Save<T>(T toSave) where T : class, new();

        /// <summary>
        /// When implemented in a derived class, calls
        /// Create or Update as appropriate
        /// </summary>
        /// <param name="toSave"></param>
        /// <returns></returns>
		object Save(object toSave);
        IEnumerable SaveCollection(IEnumerable values);
        IEnumerable<T> SaveCollection<T>(IEnumerable<T> values) where T : class, new();

        /// <summary>
        /// When implemented in a derived class retrieves
        /// the specified instance of type T with the
        /// specified id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Retrieve<T>(int id) where T : class, new();
        /// <summary>
        /// When implemented in a derived class retrieves
        /// the specified instance of type T with the
        /// specified id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Retrieve<T>(long id) where T : class, new();

        T Retrieve<T>(string uuid) where T : class, new();
        /// <summary>
        /// When implemented in a derived class retrieves
        /// all instances of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
		IEnumerable<T> RetrieveAll<T>() where T : class, new();
		IEnumerable<object> RetrieveAll(Type type);
		IEnumerable<object> Query(string propertyName, object propertyValue);
        IEnumerable<T> Query<T>(Dictionary<string, object> queryParams) where T: class, new();
        IEnumerable<object> Query(Type type, dynamic queryParams);
        IEnumerable<object> Query(Type type, Dictionary<string, object> queryParams);
		object Retrieve(Type objectType, long id);
		object Retrieve(Type objectType, string uuid);
		IEnumerable<T> Query<T>(dynamic query) where T : class, new();
        IEnumerable<T> Query<T>(Func<T, bool> query) where T : class, new();
		IEnumerable<object> Query(Type type, Func<object, bool> predicate);
        IEnumerable<T> Query<T>(QueryFilter query) where T : class, new();
        IEnumerable<object> Query(Type type, QueryFilter query);

        T Create<T>(T toCreate) where T : class, new();
        /// <summary>
        /// When implemented in a derived class, calls
        /// Create or Update as appropriate
        /// </summary>
        /// <param name="toCreate"></param>
        /// <returns></returns>
		object Create(object toCreate);
        object Create(Type type, object toCreate);
        T Update<T>(T toUpdate) where T : new();
		object Update(object toUpdate);
        object Update(Type type, object toUpdate);
		bool Delete<T>(T toDelete) where T : new();
		bool Delete(object toDelete);
        bool Delete(Type type, object toDelete);
        bool DeleteWhere<T>(dynamic filter);
        bool DeleteWhere(Type type, dynamic filter);
    }
}
