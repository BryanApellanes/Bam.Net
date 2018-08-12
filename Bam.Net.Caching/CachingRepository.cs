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
using System.Reflection;

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

	public class CachingRepository: Repository, IQueryFilterable
	{
        const string ExceptionText = "The specified type is not marked as serializable, add the [Serializable] attribute to the class definition to ensure proper caching behavior";

        CacheManager _cacheManager;
        public event EventHandler RetrievedFromSource;
        public event EventHandler RetrievedFromCache;
        public event EventHandler QueriedSource;
        public event EventHandler QueriedCache;
        public event EventHandler Evicted;

        protected CachingRepository() { }

        public CachingRepository(IRepository sourceRepository, ILogger logger = null)
        {
            SetSourceRepository(sourceRepository);
            SetCacheManager();

            Logger = logger ?? Log.Default;
        }

        protected void SetCacheManager()
        {
            _cacheManager = new CacheManager();
            _cacheManager.SubscribeOnce(nameof(CacheManager.Evicted), OnEvicted);
        }

        protected void SetSourceRepository(IRepository sourceRepository)
        {
            SourceRepository = sourceRepository;
            SourceRepository.StorableTypes.Each(new { Repo = this }, (ctx, t) => ctx.Repo.AddType(t));
            ValidateTypes();
        }

        public CachingRepository(Database database, ILogger logger = null) : this(new DaoRepository(database, logger), logger)
	    {
	    }
        
        public void ValidateTypes()
        {
            foreach(Type type in SourceRepository.StorableTypes)
            {
                Args.ThrowIf(!type.HasCustomAttributeOfType<SerializableAttribute>(), ExceptionText);
            }
        }

        public override void AddType(Type type)
        {
            Args.ThrowIf(!type.HasCustomAttributeOfType<SerializableAttribute>(), ExceptionText);
            SourceRepository.AddType(type);
        }
        /// <summary>
        /// Queries the source repository and adds the results to the internal cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public override IEnumerable<T> Query<T>(QueryFilter query)
        {
            IEnumerable<T> results = DelegateGenericOrThrow<IEnumerable<T>, T>("Query", query).CopyAs<T>();
            QueriedSource?.Invoke(this, new CacheQueryEventArgs<T> { Type = typeof(T), QueryFilter = query, Results = results });
            foreach(CacheItem item in _cacheManager.CacheFor<T>().Add(results))
            {
                yield return item.ValueAs<T>();
            }
        }
        
        /// <summary>
        /// Queries the source repository and adds the results to the internal
        /// cache
        /// </summary>
        /// <param name="type"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public override IEnumerable<object> Query(Type type, QueryFilter query)
        {
            IEnumerable<object> results = DelegateOrThrow<IEnumerable<object>>("Query", type, query).CopyAs(type);
            QueriedSource?.Invoke(this, new CacheQueryEventArgs<object> { Type = type, QueryFilter = query, Results = results });
            foreach (CacheItem item in _cacheManager.CacheFor(type).Add(results))
            {
                yield return item.Value;
            }
        }

        public override T Create<T>(T toCreate)
		{
			Args.ThrowIfNull(toCreate, "toCreate");

			T result = this.SourceRepository.Create<T>(toCreate);
			_cacheManager.CacheFor<T>().Add(result);

			return result;
		}
        public override object Create(Type type, object toCreate)
        {
            return Create(toCreate);
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

		public override T Retrieve<T>(ulong id)
		{
            return Retrieve<T>((cache) => cache.Retrieve(id), () => SourceRepository.Retrieve<T>(id));
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

        public override void BatchRetrieveAll(Type type, int batchSize, Action<IEnumerable<object>> processor)
        {
            SourceRepository.BatchRetrieveAll(type, batchSize, processor);
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
			Cache cache = _cacheManager.CacheFor(objectType);
			CacheItem cacheItem = cache.Retrieve(id);
			object result;
			if (cacheItem == null)
			{
				result = SourceRepository.Retrieve(objectType, id);
				cache.Add(result);
                RetrievedFromSource?.Invoke(this, new CacheRetrieveEventArgs { Type = objectType, Item = result });
			}
			else
			{
				result = cacheItem.Value;
                RetrievedFromCache?.Invoke(this, new CacheRetrieveEventArgs { Type = objectType, Item = result });
            }

			return result;
		}

        public override object Retrieve(Type objectType, ulong id)
        {
            Cache cache = _cacheManager.CacheFor(objectType);
            CacheItem cacheItem = cache.Retrieve(id);
            object result;
            if (cacheItem == null)
            {
                result = SourceRepository.Retrieve(objectType, id);
                cache.Add(result);
                RetrievedFromSource?.Invoke(this, new CacheRetrieveEventArgs { Type = objectType, Item = result });
            }
            else
            {
                result = cacheItem.Value;
                RetrievedFromCache?.Invoke(this, new CacheRetrieveEventArgs { Type = objectType, Item = result });
            }

            return result;
        }

        public override object Retrieve(Type objectType, string uuid)
		{
			Cache cache = _cacheManager.CacheFor(objectType);
			CacheItem cacheItem = cache.Retrieve(uuid);
			object result;
			if (cacheItem == null)
			{
				result = SourceRepository.Retrieve(objectType, uuid);
				cache.Add(result);
                RetrievedFromSource?.Invoke(this, new CacheRetrieveEventArgs { Type = objectType, Item = result });
            }
			else
			{
				result = cacheItem.Value;
                RetrievedFromCache?.Invoke(this, new CacheRetrieveEventArgs { Type = objectType, Item = result });
            }

			return result;
		}

		[Verbosity(VerbosityLevel.Information, MessageFormat="Different types were found with the same property name and value: \r\n{DifferingTypes}")]
		public event EventHandler DifferringTypesFound;

		public string Types { get; private set; }

		public string PropertyName { get; set; }
		public string Value { get; set; }
        /// <summary>
        /// Event that fires when a non typed query is executed.  Used as a 
        /// warning that the type cannot be determined and will have to be
        /// resolved by the caller
        /// </summary>
        public event EventHandler<CachingRepositoryEventArgs> TypelessQuery;

        protected void OnQueriedSource<T>(IEnumerable<T> results)
        {
            QueriedSource?.Invoke(this, new CacheQueryEventArgs<T> { Type = typeof(T), Results = results });
        }
        /// <summary>
        /// Query the cache and the SourceRepository and return the results
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public override IEnumerable<T> Query<T>(Func<T, bool> predicate)
        {
            Cache cache = _cacheManager.CacheFor<T>();
            Task<HashSet<T>> cacheResults = Task.Run(() => QueryCache<T>(predicate));
            Task<HashSet<T>> queryResults = Task.Run(() =>
            {
                HashSet<T> qr = new HashSet<T>(DelegateOrThrow<IEnumerable<object>>("RetrieveAll", typeof(T)).CopyAs<T>().Where(predicate));
                OnQueriedSource<T>(qr);
                return qr;
            });
            Task<HashSet<T>[]> resultsHashes = Task.WhenAll(cacheResults, queryResults);
            resultsHashes.Wait();
            HashSet<T> results = HandleResults(cache, resultsHashes.Result);
            return results;
        }

        public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
        {
            Cache cache = _cacheManager.CacheFor(type);
            Task<HashSet<object>> cacheResults = Task.Run(() => QueryCache(type, predicate));
            Task<HashSet<object>> queryResults = Task.Run(() =>
            {
                HashSet<object> qr = new HashSet<object>(DelegateOrThrow<IEnumerable<object>>("Query", type, predicate).CopyAs(type));
                OnQueriedSource<object>(qr);
                return qr;
            });
            Task<HashSet<object>[]> resultsHashes = Task.WhenAll(cacheResults, queryResults);
            resultsHashes.Wait();
            return HandleResults(cache, resultsHashes.Result);
        }

        public override IEnumerable<T> Query<T>(dynamic query)
        {
            Cache cache = _cacheManager.CacheFor<T>();
            Task<HashSet<T>> cacheResults = Task.Run((Func<HashSet<T>>)(() => QueryCache<T>((dynamic)query)));
            Task<HashSet<T>> queryResults = Task.Run(() => 
            {
                IEnumerable<object> results = DelegateOrThrow<IEnumerable<object>>("Query", typeof(T), Bam.Net.Extensions.ToDictionary(query));
                OnQueriedSource<object>(results);
                return new HashSet<T>(results.CopyAs<T>());
            });
            Task<HashSet<T>[]> resultsHashes = Task.WhenAll(cacheResults, queryResults);
            resultsHashes.Wait();
            return HandleResults(cache, resultsHashes.Result);
		}

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            Cache cache = _cacheManager.CacheFor<T>();
            Task<HashSet<T>> cacheResults = Task.Run(() => QueryCache<T>(queryParameters));
            Task<HashSet<T>> queryResults = Task.Run(() =>
            {
                HashSet<T> results = new HashSet<T>(DelegateGenericOrThrow<IEnumerable<T>, T>("Query", queryParameters).CopyAs<T>());
                OnQueriedSource(results);
                return results;
            });
            Task<HashSet<T>[]> resultsHashes = Task.WhenAll(cacheResults, queryResults);
            resultsHashes.Wait();
            return HandleResults(cache, resultsHashes.Result);
        }

        public override IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters)
        {
            Cache cache = _cacheManager.CacheFor(type);
            Task<HashSet<object>> cacheResults = Task.Run(() => QueryCache(type, queryParameters));
            Task<HashSet<object>> queryResults = Task.Run(() =>
            {
                HashSet<object> results = new HashSet<object>(DelegateOrThrow<IEnumerable<object>>("Query", type, queryParameters).CopyAs(type));
                OnQueriedSource(results);
                return results;
            });
            Task<HashSet<object>[]> resultsHashes = Task.WhenAll(cacheResults, queryResults);
            resultsHashes.Wait();
            return HandleResults(cache, resultsHashes.Result);
        }

        public override IEnumerable<object> Query(Type type, dynamic query)
        {
            Cache cache = _cacheManager.CacheFor(type);
            Task<HashSet<object>> cacheResults = Task.Run((Func<HashSet<object>>)(() => QueryCache(type, query)));
            Task<HashSet<object>> queryResults = Task.Run(() =>
            {
                IEnumerable<object> results = DelegateOrThrow<IEnumerable<object>>("Query", type, query);
                OnQueriedSource(results);
                return new HashSet<object>(results.CopyAs(type));
            });
            Task<HashSet<object>[]> resultsHashes = Task.WhenAll(cacheResults, queryResults);
            resultsHashes.Wait();
            return HandleResults(cache, resultsHashes.Result);
        }

        /// <summary>
        /// Execute a non typed query against the underlying SourceRepository.
        /// Does not use the cache
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override IEnumerable<object> Query(string propertyName, object value)
        {
            TypelessQuery?.Invoke(this, new CachingRepositoryEventArgs { PropertyName = PropertyName, ParameterValue = value });
            object[] results = SourceRepository.Query(propertyName, value).ToArray();
            if (results.Length > 0)
            {
                CheckForDifferringTypes(propertyName, value, results);
            }
            return results;
        }

        public Task<IEnumerable<T>> CacheAsync<T>(Func<T, bool> predicate)
        {
            return Task.Run(() => Cache<T>(predicate));
        }

        /// <summary>
        /// Prime the cache with the results of the specified query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> Cache<T>(Func<T, bool> predicate)
        {
            Cache cache = _cacheManager.CacheFor(typeof(T));
            return HandleResults(cache, new HashSet<T>(DelegateGenericOrThrow<IEnumerable<T>, T>("Query", predicate)));
        }

        public Task<IEnumerable<object>> CacheAysync(Type type, Func<object, bool> predicate)
        {
            return Task.Run(() => Cache(type, predicate));
        }

        public IEnumerable<object> Cache(Type type, Func<object, bool> predicate)
        {
            Cache cache = _cacheManager.CacheFor(type);
            return HandleResults(cache, new HashSet<object>(DelegateOrThrow<IEnumerable<object>>("Query", type, predicate)));
        }

        public Task<IEnumerable<T>> CacheAsync<T>(dynamic query)
        {
            return Task.Run((Func<IEnumerable<T>>)(() => (Cache<T>(query))));
        }

        public IEnumerable<T> Cache<T>(dynamic query)
        {
            Cache cache = _cacheManager.CacheFor<T>();
            return HandleResults(cache, new HashSet<T>(DelegateGenericOrThrow<IEnumerable<T>, T>("Query", query)));
        }

        public Task<IEnumerable<T>> CacheAsync<T>(Dictionary<string, object> queryParameters)
        {
            return Task.Run(() => Cache<T>(queryParameters));
        }

        public IEnumerable<T> Cache<T>(Dictionary<string, object> queryParameters)
        {
            Cache cache = _cacheManager.CacheFor<T>();
            return HandleResults(cache, new HashSet<T>(DelegateGenericOrThrow<IEnumerable<T>, T>("Query", queryParameters)));
        }

        public Task<IEnumerable<object>> CacheAsync(Type type, Dictionary<string, object> queryParameters)
        {
            return Task.Run(() => Cache(type, queryParameters));
        }

        public IEnumerable<object> Cache(Type type, Dictionary<string, object> queryParameters)
        {
            Cache cache = _cacheManager.CacheFor(type);
            return HandleResults(cache, new HashSet<object>(DelegateOrThrow<IEnumerable<object>>("Query", type, queryParameters)));
        }

        public HashSet<object> QueryCache(Type type, dynamic query)
        {
            Cache cache = _cacheManager.CacheFor(type);
            Type queryType = query.GetType();
            PropertyInfo[] queryProps = queryType.GetProperties();
            return new HashSet<object>(cache.Query<object>(o =>
            {
                foreach (PropertyInfo prop in queryProps)
                {
                    PropertyInfo currentProp = type.GetProperty(prop.Name);
                    if (!ReflectionExtensions.Property(o, prop.Name).Equals(prop.GetValue(query)))
                    {
                        return false;
                    }
                }
                return true;
            }));
        }

        public HashSet<T> QueryCache<T>(dynamic query)
        {
            Cache cache = _cacheManager.CacheFor<T>();
            Type queryType = query.GetType();
            PropertyInfo[] queryProps = queryType.GetProperties();
            return new HashSet<T>(cache.Query<T>(o =>
            {
                foreach (PropertyInfo prop in queryProps)
                {
                    PropertyInfo currentProp = typeof(T).GetProperty(prop.Name);
                    if (!ReflectionExtensions.Property(o, prop.Name).Equals(prop.GetValue(query)))
                    {
                        return false;
                    }
                }

                return true;
            }));
        }

        public HashSet<T> QueryCache<T>(Func<T, bool> query) where T : class, new()
        {
            Cache cache = _cacheManager.CacheFor<T>();
            IEnumerable<T> results = cache.Query<T>(query);
            QueriedCache?.Invoke(this, new CacheQueryEventArgs<T> { Type = typeof(T), Results = results });
            return new HashSet<T>(results);
        }

        public HashSet<object> QueryCache(Type type, Func<object, bool> predicate)
        {
            Cache cache = _cacheManager.CacheFor(type);
            IEnumerable<object> results = cache.Query(predicate);
            QueriedCache?.Invoke(this, new CacheQueryEventArgs<object> { Type = type, Results = results });
            return new HashSet<object>(results);
        }

        public HashSet<object> QueryCache(Type type, Dictionary<string, object> parameters)
        {
            Cache cache = _cacheManager.CacheFor(type);
            IEnumerable<object> results = cache.Query(o =>
            {
                foreach (string propName in parameters.Keys)
                {
                    if (!ReflectionExtensions.Property(o.Value, propName).Equals(parameters[propName]))
                    {
                        return false;
                    }
                }
                return true;
            }).Select(ci => ci.Value);
            QueriedCache?.Invoke(this, new CacheQueryEventArgs<object> { Type = type, Results = results });
            return new HashSet<object>(results);
        }

        public HashSet<T> QueryCache<T>(Dictionary<string, object> parameters)
        {
            Cache cache = _cacheManager.CacheFor<T>();
            return new HashSet<T>(cache.Query<T>(o =>
            {
                foreach(string propName in parameters.Keys)
                {
                    if(!ReflectionExtensions.Property(o, propName).Equals(parameters[propName]))
                    {
                        return false;
                    }
                }
                return true;
            }));
        }

        public override T Update<T>(T toUpdate)
		{
            Task.Run(() =>
            {
                Cache cache = _cacheManager.CacheFor<T>();
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
            return Update(toUpdate.GetType(), toUpdate);
        }
        public override object Update(Type type, object toUpdate)
		{
            Task.Run(() =>
            {
                Cache cache = _cacheManager.CacheFor(type);
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

        public override bool Delete(Type type, object toDelete)
        {
            return Delete(toDelete);
        }

        public override bool Delete(object toDelete)
		{
            string id = toDelete == null ? "[null]" : Meta.GetUuid(toDelete);
            throw new DeleteNotSupportedException(id);
		}

        public IRepository SourceRepository { get; private set; }

        private static HashSet<T> HandleResults<T>(Cache cache, params HashSet<T>[] arrayOfHashes)
        {
            HashSet<T> results = new HashSet<T>();
            foreach (HashSet<T> hs in arrayOfHashes)
            {
                results.UnionWith(hs);
            }
            Task.Run(() => cache.Add(results.ToArray()));
            return results;
        }

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
            if (SourceRepository is DaoRepository daoRepo)
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
            if (SourceRepository is DaoRepository daoRepo)
            {
                return daoRepo.InvokeGeneric<T, TArg>(methodName, parameters);
            }
            else
            {
                throw new UnsupportedRepositoryTypeException(SourceRepository.GetType());
            }
        }

        private T Retrieve<T>(Func<Cache, CacheItem> cacheRetriever, Func<T> sourceRetriever)
        {
            Cache cache = _cacheManager.CacheFor<T>();
            CacheItem cacheItem = cacheRetriever(cache);
            T result;
            if (cacheItem == null)
            {
                result = sourceRetriever();
                cache.Add(result);
                RetrievedFromSource?.Invoke(this, new CacheRetrieveEventArgs<T> { Item = result });
            }
            else
            {
                result = cacheItem.ValueAs<T>();
                RetrievedFromCache?.Invoke(this, new CacheRetrieveEventArgs<T> { Item = result });
            }
            if (result != null)
            {
                cache.Add(result);
            }

            return result;
        }
        private void OnEvicted(object sender, EventArgs e)
        {
            Evicted?.Invoke(sender, e);
        }
    }
}
