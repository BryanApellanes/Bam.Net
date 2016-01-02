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

namespace Bam.Net.Caching
{
	public class CachingRepository: Repository
	{
		CacheManager _cacheManager;
		public CachingRepository(IRepository sourceRepository)
		{
			this.SourceRepository = sourceRepository;

			this._cacheManager = new CacheManager();
		}

		protected IRepository SourceRepository { get; private set; }

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
			Cache cache = _cacheManager.CacheFor<T>();
			CacheItem cacheItem = cache.Retrieve(id);
			T result;
			if (cacheItem == null)
			{
				result = SourceRepository.Retrieve<T>(id);
				cache.Add(result);
			}
			else
			{
				result = cacheItem.ValueAs<T>();
			}

			return result;
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
			Cache cache = _cacheManager.CacheFor(objectType);
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
			Cache cache = _cacheManager.CacheFor(objectType);
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

		object _differingTypesLock = new object();
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
            object[] results = SourceRepository.Query(query);
            throw new NotImplementedException();
		}

		public override IEnumerable<T> Query<T>(Func<T, bool> query)
		{
			throw new NotImplementedException();
		}

		public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public override IEnumerable<T> Query<T>(dynamic query)
		{
			throw new NotImplementedException();
		}

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable Query(Type type, Dictionary<string, object> queryParameters)
        {
            throw new NotImplementedException();
        }

		public override T Update<T>(T toUpdate)
		{
			throw new NotImplementedException();
		}

		public override object Update(object toUpdate)
		{
			throw new NotImplementedException();
		}

		public override bool Delete<T>(T toDelete)
		{
			throw new NotImplementedException();
		}

		public override bool Delete(object toDelete)
		{
			throw new NotImplementedException();
		}

        private void CheckForDifferringTypes(string propertyName, object value, object[] results)
        {
            Type firstType = results[0].GetType();
            object differentType = results.FirstOrDefault(o => o.GetType() != firstType);
            if (differentType != null)
            {
                lock (_differingTypesLock)
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
        }

	}
}
