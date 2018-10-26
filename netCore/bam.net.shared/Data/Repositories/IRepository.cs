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
        object Save(Type type, object toSave);        
        void BatchRetrieveAll(Type type, int batchSize, Action<IEnumerable<object>> processor);        
		IEnumerable<object> Query(string propertyName, object propertyValue);
        IEnumerable<T> Query<T>(Dictionary<string, object> queryParams) where T: class, new();
        IEnumerable<object> Query(Type type, dynamic queryParams);
        new IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters);
        IEnumerable<T> Query<T>(dynamic query) where T : class, new();
        IEnumerable<T> Query<T>(Func<T, bool> query) where T : class, new();
        IEnumerable<object> Query(Type type, Func<object, bool> predicate);        
        object Create(Type type, object toCreate);        
        object Update(Type type, object toUpdate);		
        bool Delete(Type type, object toDelete);
        bool DeleteWhere<T>(dynamic filter);
        bool DeleteWhere(Type type, dynamic filter);

        bool TryHydrate(RepoData data);
    }
}
