using System;

namespace Bam.Net.Caching
{
    public interface ICacheManager
    {
        uint AllCacheSize { get; }

        event EventHandler CacheRemoved;
        event EventHandler CacheSet;
        event EventHandler GetCacheFailed;

        Cache CacheFor(Type type);
        void CacheFor(Type type, Cache cache);
        Cache CacheFor<T>();
        void CacheFor<T>(Cache cache);
        void Clear();
    }
}