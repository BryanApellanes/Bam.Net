/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Caching
{
	public class CacheManager
	{
		Dictionary<Type, Cache> _cacheDictionary;
		public CacheManager()
		{
			_cacheDictionary = new Dictionary<Type, Cache>();
		}

		public void Clear()
		{
			lock (_getLock)
			{
				_cacheDictionary.Clear();
			}
		}

		public int AllCacheSize
		{
			get
			{
				return _cacheDictionary.Values.Sum(c => c.ItemsMemorySize);
			}
		}

		object _getLock = new object();
		public Cache CacheFor<T>()
		{
			return CacheFor(typeof(T));
		}

		public void CacheFor<T>(Cache cache)
		{
			CacheFor(typeof(T), cache);
		}

		public Cache CacheFor(Type type)
		{
			lock(_getLock)
			{
				if (!_cacheDictionary.ContainsKey(type))
				{
					_cacheDictionary.Add(type, new Cache());
				}

				return _cacheDictionary[type];
			}
		}

		public void CacheFor(Type type, Cache cache)
		{
			lock (_getLock)
			{
				_cacheDictionary.Set(type, cache);
			}
		}

		static CacheManager _defaultCacheManager;
		static object _defaultCacheManagerLock = new object();
		public static CacheManager Default
		{
			get
			{
				return _defaultCacheManagerLock.DoubleCheckLock(ref _defaultCacheManager, () => new CacheManager());
			}
		}
	}
}
