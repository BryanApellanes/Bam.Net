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
	public class CacheManager : Loggable, ICacheManager
    {
		ConcurrentDictionary<Type, Cache> _cacheDictionary;
		public CacheManager(uint maxCacheSizeBytes = 524288) // 512 kilobytes
        {
			_cacheDictionary = new ConcurrentDictionary<Type, Cache>();
            MaxCacheSizeBytes = maxCacheSizeBytes;
		}

        public void Clear()
        {
            _cacheDictionary.Clear();
        }

        public uint MaxCacheSizeBytes { get; set; }

		public uint AllCacheSize
		{
			get
			{
				return (uint)_cacheDictionary.Values.Sum(c => c.ItemsMemorySize);
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

        public event EventHandler Evicted;

        [Verbosity(LogEventType.Warning, MessageFormat = "Failed to get Cache for type {TypeName}")]
        public event EventHandler GetCacheFailed;

        /// <summary>
        /// Checks for a cache for the specified type setting it to the
        /// return value of cacheProvider if its not present.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cacheProvider"></param>
        protected void EnsureCache(Type type, Func<Cache> cacheProvider)
        {
            if (!_cacheDictionary.ContainsKey(type))
            {
                Cache cache = cacheProvider();
                _cacheDictionary.TryAdd(type, cache);
                FireEvent(CacheSet, new CacheManagerEventArgs { Type = type, Cache = cache });
            }
        }

        private void OnEvicted(object sender, EventArgs args)
        {
            Evicted?.Invoke(sender, args);
        }

        [Verbosity(LogEventType.Information, MessageFormat = "Removed Cache for type {TypeName}")]
        public event EventHandler CacheRemoved;
        [Verbosity(LogEventType.Information, MessageFormat = "Set Cache for type {TypeName}")]
        public event EventHandler CacheSet;

        public Cache<CacheType> CacheFor<CacheType>(Func<Cache<CacheType>> cacheProvider) where CacheType : IMemorySize, new()
        {
            Cache<CacheType> cache = cacheProvider();
            CacheFor(typeof(CacheType), cache);
            return cache;
        }

        public Cache CacheFor(Type type)
        {
            EnsureCache(type, () => new Cache(type.Name, MaxCacheSizeBytes, true, OnEvicted));
            if (!_cacheDictionary.TryGetValue(type, out Cache result))
            {
                FireEvent(GetCacheFailed, new CacheManagerEventArgs { Type = type });
            }
            return result;
        }

        public void CacheFor(Type type, Cache cache)
        {
            if (_cacheDictionary.TryRemove(type, out Cache removed))
            {
                FireEvent(CacheRemoved, new CacheManagerEventArgs { Type = type, Cache = removed });
            }
            cache.Name = type.Name;
            cache.MaxBytes = MaxCacheSizeBytes;
            EnsureCache(type, () => cache);            
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
