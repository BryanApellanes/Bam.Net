/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;

namespace Bam.Net.Caching
{
	public class ConcurrentCacheManager : Loggable, ICacheManager
    {
		ConcurrentDictionary<Type, ConcurrentCache> _cacheDictionary;
		public ConcurrentCacheManager()
		{
			_cacheDictionary = new ConcurrentDictionary<Type, ConcurrentCache>();
		}

        public void Clear()
        {
            _cacheDictionary.Clear();
        }

		public int AllCacheSize
		{
			get
			{
				return _cacheDictionary.Values.Sum(c => c.ItemsMemorySize);
			}
		}

		object _getLock = new object();
		public ConcurrentCache CacheFor<T>()
		{
			return CacheFor(typeof(T));
		}

		public void CacheFor<T>(ConcurrentCache cache)
		{
			CacheFor(typeof(T), cache);
		}

        [Verbosity(LogEventType.Warning, MessageFormat = "Failed to get Cache for type {TypeName}")]
        public event EventHandler GetCacheFailed;

        public ConcurrentCache CacheFor(Type type)
        {
            if (!_cacheDictionary.ContainsKey(type))
            {
                _cacheDictionary.TryAdd(type, new ConcurrentCache());
            }
            ConcurrentCache result;
            if(!_cacheDictionary.TryGetValue(type, out result))
            {
                FireEvent(GetCacheFailed, new CacheManagerEventArgs { Type = type });
            }
            return result;
        }

        [Verbosity(LogEventType.Information, MessageFormat = "Removed Cache for type {TypeName}")]
        public event EventHandler CacheRemoved;
        [Verbosity(LogEventType.Information, MessageFormat = "Set Cache for type {TypeName}")]
        public event EventHandler CacheSet;

        public void CacheFor(Type type, ConcurrentCache cache)
        {
            ConcurrentCache removed;
            if (_cacheDictionary.TryRemove(type, out removed))
            {
                FireEvent(CacheRemoved, new CacheManagerEventArgs { Type = type, Cache = removed });
            }

            _cacheDictionary.TryAdd(type, cache);
            FireEvent(CacheSet, new CacheManagerEventArgs { Type = type, Cache = cache });
        }

		static ConcurrentCacheManager _defaultCacheManager;
		static object _defaultCacheManagerLock = new object();
		public static ConcurrentCacheManager Default
		{
			get
			{
				return _defaultCacheManagerLock.DoubleCheckLock(ref _defaultCacheManager, () => new ConcurrentCacheManager());
			}
		}
	}
}
