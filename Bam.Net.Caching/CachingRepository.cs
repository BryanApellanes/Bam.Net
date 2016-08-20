/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Data.Repositories;
using System.Collections;
using Bam.Net.Data;

namespace Bam.Net.Caching
{
    public class CachingRepository<T>: CachingRepository where T : class, IRepository
    {
        public static implicit operator T(CachingRepository<T> repo)
        {
            return repo.TypedSourceRepository;
        }

        public CachingRepository(T sourceRepository) : base(sourceRepository)
        { }

        public T TypedSourceRepository { get { return SourceRepository as T; } }
    }

	public class CachingRepository: Repository
	{
		ConcurrentCacheManager _cacheManager;
		public CachingRepository(IRepository sourceRepository)
		{
			SourceRepository = sourceRepository;
			_cacheManager = new ConcurrentCacheManager();
		}

        public override IEnumerable<T> Query<T>(QueryFilter query)
        {
            return DelegateGenericOrThrow<IEnumerable<T>, T>("Query", query);
        }

        public override IEnumerable Query(Type type, QueryFilter query)
        {
            return DelegateOrThrow<IEnumerable>("Query", type, query);
        }

        public override T Create<T>(T toCreate)
		{
			Args.ThrowIfNull(toCreate, "toCreate");

			T result = this.SourceRepository.Create<T>(toCreate);
			_cacheManager.CacheFor<T>().Add(result);

			return result;
		}

		public override object Create(object toCreate)
		{
			Args.ThrowIfNull(toCreate, "toCreate");

			object result = this.SourceRepository.Create(toCreate);
			_cacheManager.CacheFor(result.GetType()).Add(result);

			return result;
		}

		public override T Retrieve<T>(int id)
		{
			return Retrieve<T>((long)id);
		}

		public override T Retrieve<T>(long id)
		{
            return Retrieve<T>((cache) => cache.Retrieve(id), () => SourceRepository.Retrieve<T>(id));
        }

        public override T Retrieve<T>(string uuid)
        {
            return Retrieve<T>((cache) => cache.Retrieve(uuid), () => SourceRepository.Retrieve<T>(uuid));
        }

        /// <summary>
        /// Delegates to the underlying SourceRepository
        /// without caching
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override IEnumerable<T> RetrieveAll<T>()
		{
			return SourceRepository.RetrieveAll<T>();
		}

		/// <summary>
		/// Delegates to the underlying SourceRepository
		/// without caching
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public override IEnumerable<object> RetrieveAll(Type type)
		{
			return SourceRepository.RetrieveAll(type);
		}

		public override object Retrieve(Type objectType, long id)
		{
			ConcurrentCache cache = _cacheManager.CacheFor(objectType);
			CacheItem cacheItem = cache.Retrieve(id);
			object result;
			if (cacheItem == null)
			{
				result = SourceRepository.Retrieve(objectType, id);
				cache.Add(result);
			}
			else
			{
				result = cacheItem.Value;
			}

			return result;
		}

		public override object Retrieve(Type objectType, string uuid)
		{
			ConcurrentCache cache = _cacheManager.CacheFor(objectType);
			CacheItem cacheItem = cache.Retrieve(uuid);
			object result;
			if (cacheItem == null)
			{
				result = SourceRepository.Retrieve(objectType, uuid);
				cache.Add(result);
			}
			else
			{
				result = cacheItem.Value;
			}

			return result;
		}

		[Verbosity(VerbosityLevel.Information, MessageFormat="Different types were found with the same property name and value: \r\n{DifferingTypes}")]
		public event EventHandler DifferringTypesFound;

		public string Types { get; private set; }

		public string PropertyName { get; set; }
		public string Value { get; set; }
        
		public override IEnumerable<object> Query(string propertyName, object value)
		{
			object[] results = SourceRepository.Query(propertyName, value).ToArray();
			if (results.Length > 0)
			{
                CheckForDifferringTypes(propertyName, value, results);
			}
			return results;
		}

		public override IEnumerable<object> Query(dynamic query)
		{
            return DelegateOrThrow<IEnumerable<object>>("Query", query);
		}

        /// <summary>
        /// Query the cache and the SourceRepository and return the results
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
		public override IEnumerable<T> Query<T>(Func<T, bool> query)
        {
            HashSet<T> cacheResults = QueryCache(query);
            HashSet<T> results = new HashSet<T>(DelegateGenericOrThrow<IEnumerable<T>, T>("Query", query));

            results.UnionWith(cacheResults);
            return results;
        }

        public HashSet<T> QueryCache<T>(Func<T, bool> query) where T : class, new()
        {
            return new HashSet<T>(_cacheManager.CacheFor<T>().Query<T>(query));
        }

        public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
		{
            HashSet<object> cacheResults = new HashSet<object>(_cacheManager.CacheFor(type).Query(predicate));
            HashSet<object> results = new HashSet<object>(DelegateOrThrow<IEnumerable<object>>("Query", type, predicate));
            results.UnionWith(cacheResults);
            return results;
		}

		public override IEnumerable<T> Query<T>(dynamic query)
		{
            return DelegateGenericOrThrow<IEnumerable<T>, T>("Query", query);
		}

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            return DelegateGenericOrThrow<IEnumerable<T>, T>("Query", queryParameters);
        }

        public override IEnumerable Query(Type type, Dictionary<string, object> queryParameters)
        {
            return DelegateOrThrow<IEnumerable>("Query", type, queryParameters);
        }

		public override T Update<T>(T toUpdate)
		{
            Task.Run(() =>
            {
                ConcurrentCache cache = _cacheManager.CacheFor<T>();
                CacheItem fromCache = cache.Retrieve(toUpdate);
                if (fromCache != null)
                {
                    cache.Evict(fromCache);
                    cache.Add(toUpdate);
                }
            });
            return SourceRepository.Update<T>(toUpdate);
		}

		public override object Update(object toUpdate)
		{
            Task.Run(() =>
            {
                ConcurrentCache cache = _cacheManager.CacheFor(toUpdate.GetType());
                CacheItem fromCache = cache.Retrieve(toUpdate);
                if (fromCache != null)
                {
                    cache.Evict(fromCache);
                    cache.Add(toUpdate);
                }
            });
            return SourceRepository.Update(toUpdate);
        }
        
		public override bool Delete<T>(T toDelete)
		{
            throw new DeleteNotSupportedException(Meta.GetUuid(toDelete).Or(typeof(T).FullName));
		}

		public override bool Delete(object toDelete)
		{
            string id = toDelete == null ? "[null]" : Meta.GetUuid(toDelete);
            throw new DeleteNotSupportedException(id);
		}

        protected IRepository SourceRepository { get; private set; }

        private void CheckForDifferringTypes(string propertyName, object value, object[] results)
        {
            Type firstType = results[0].GetType();
            object differentType = results.FirstOrDefault(o => o.GetType() != firstType);
            if (differentType != null)
            {
                HashSet<Type> differingTypes = new HashSet<Type>();
                results.Each(differingTypes, (typeHash, o) =>
                {
                    typeHash.Add(o.GetType());
                });
                Types = differingTypes.ToArray().ToDelimited(t => t.Name, ", ");
                PropertyName = propertyName;
                Value = value == null ? "null" : value.ToString();
                FireEvent(DifferringTypesFound, EventArgs.Empty);
            }
        }

        private T DelegateOrThrow<T>(string methodName, params object[] parameters)
        {
            Args.ThrowIfNull(SourceRepository, "SourceRepository");
            DaoRepository daoRepo = SourceRepository as DaoRepository;
            if (daoRepo != null)
            {
                return daoRepo.Invoke<T>(methodName, parameters);
            }
            else
            {
                throw new UnsupportedRepositoryTypeException(SourceRepository.GetType());
            }
        }

        private T DelegateGenericOrThrow<T, TArg>(string methodName, params object[] parameters)
        {
            Args.ThrowIfNull(SourceRepository, "SourceRepository");
            DaoRepository daoRepo = SourceRepository as DaoRepository;
            if (daoRepo != null)
            {
                return daoRepo.InvokeGeneric<T, TArg>(methodName, parameters);
            }
            else
            {
                throw new UnsupportedRepositoryTypeException(SourceRepository.GetType());
            }
        }

        private T Retrieve<T>(Func<ConcurrentCache, CacheItem> cacheRetriever, Func<T> sourceRetriever)
        {
            ConcurrentCache cache = _cacheManager.CacheFor<T>();
            CacheItem cacheItem = cacheRetriever(cache);
            T result;
            if (cacheItem == null)
            {
                result = sourceRetriever();
                cache.Add(result);
            }
            else
            {
                result = cacheItem.ValueAs<T>();
            }
            if (result != null)
            {
                cache.Add(result);
            }

            return result;
        }
    }
}
