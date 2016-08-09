using System;

namespace Bam.Net.Caching
{
    public interface ICacheManager
    {
        int AllCacheSize { get; }

        event EventHandler CacheRemoved;
        event EventHandler CacheSet;
        event EventHandler GetCacheFailed;

        ConcurrentCache CacheFor(Type type);
        void CacheFor(Type type, ConcurrentCache cache);
        ConcurrentCache CacheFor<T>();
        void CacheFor<T>(ConcurrentCache cache);
        void Clear();
    }
}