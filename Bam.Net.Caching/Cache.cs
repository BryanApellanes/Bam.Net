/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Bam.Net.Logging;
using Bam.Net.Data.Repositories;
using System.Collections.Concurrent;

namespace Bam.Net.Caching
{
	public class Cache: Loggable
	{
		bool _keepGrooming;
		Thread _groomerThread;
		AutoResetEvent _groomerSignal;
		ConcurrentQueue<CacheItem> _evictionQueue;
		
		public Cache() : this(true) { }

		public Cache(bool groomInBackground)
			: this(System.Guid.NewGuid().ToString(), groomInBackground)
		{ }
		public Cache(string name, bool groomInBackground)
			: this(name, 524288, groomInBackground) // 512 kilobytes
		{ }

		public Cache(string name, int maxBytes, bool groomInBackground)
		{
			Items = new HashSet<CacheItem>();
			ItemsByHits = new List<CacheItem>();
			ItemsByMisses = new List<CacheItem>();
			ItemsById = new Dictionary<long, CacheItem>();
            ItemsByUuid = new Dictionary<string, CacheItem>();
			MetaProvider = Bam.Net.Data.Repositories.MetaProvider.Default;
			
			Name = name;
			MaxBytes = maxBytes;

			_keepGrooming = true;
			_groomerSignal = new AutoResetEvent(false);
			_evictionQueue = new ConcurrentQueue<CacheItem>();
			
			if (groomInBackground)
			{
				StartGroomer();
			}
		}

		public IMetaProvider MetaProvider { get; set; }
		protected HashSet<CacheItem> Items { get; set; }
		protected List<CacheItem> ItemsByHits { get; private set; }
		protected List<CacheItem> ItemsByMisses { get; private set; }
		protected Dictionary<long, CacheItem> ItemsById { get; set; }
		protected Dictionary<string, CacheItem> ItemsByUuid { get; set; }

		public int MaxBytes { get; set; }			

        public CacheItem Retrieve(object instance)
        {
            Meta meta = MetaProvider.GetMeta(instance);
            return Retrieve(meta.Uuid);
        }

        public CacheItem Retrieve(long id)
        {
            CacheItem result = null;
            if(ItemsById.TryGetValue(id, out result))
            {
                result.IncrementHits();
            }
            return result;
        }

        public CacheItem Retrieve(string uuid)
        {
            CacheItem result = null;
            if (ItemsByUuid.TryGetValue(uuid, out result))
            {
                result.IncrementHits();
            }

            return result;
        }

        public virtual CacheItem Add(object value)
        {
            HashSet<CacheItem> itemsCopy = new HashSet<CacheItem>(Items);
            CacheItem item = new CacheItem(value, MetaProvider);
            itemsCopy.Add(item);
            Items = itemsCopy;
            SetCollectionsAndLookup();
            _groomerSignal.Set();
            return item;
        }

		public virtual IEnumerable<CacheItem> Add<T>(IEnumerable<T> values)
		{
			return Add(values.ToArray());
		}

        /// <summary>
        /// Add each of the specified values to the cache yielding the
        /// resulting CacheItems
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IEnumerable<CacheItem> Add<T>(params T[] values)
        {
            List<CacheItem> results = new List<CacheItem>();
            HashSet<CacheItem> itemsCopy = new HashSet<CacheItem>(Items);
            foreach (object value in values)
            {
                CacheItem item = new CacheItem(value, MetaProvider);
                itemsCopy.Add(item);
                yield return item;
            }
            Items = itemsCopy;
            SetCollectionsAndLookup();
            _groomerSignal.Set();
        }

        public IEnumerable<CacheItem> Query(Predicate<object> predicate)
        {
            HashSet<CacheItem> itemsCopy = new HashSet<CacheItem>(Items);
            foreach(CacheItem ci in itemsCopy)
            {
                if (predicate(ci.Value))
                {
                    ci.IncrementHits();
                    yield return ci;
                }
                else
                {
                    ci.IncrementMisses();
                }
            }
        }

        public IEnumerable<CacheItem> Query(Func<CacheItem, bool> predicate)
        {
            HashSet<CacheItem> itemsCopy = new HashSet<CacheItem>(Items);
            foreach (CacheItem ci in itemsCopy)
            {
                if (predicate(ci))
                {
                    ci.IncrementHits();
                    yield return ci;
                }
                else
                {
                    ci.IncrementMisses();
                }
            }
        }

        public IEnumerable<T> Query<T>(Func<T, bool> predicate)
        {
            HashSet<CacheItem> itemsCopy = new HashSet<CacheItem>(Items);
            foreach(CacheItem ci in itemsCopy)
            {
                if (predicate((T)ci.Value))
                {
                    ci.IncrementHits();
                    yield return (T)ci.Value;
                }
                else
                {
                    ci.IncrementMisses();
                }
            }
        }

        public IEnumerable<T> Query<T>(Func<T, bool> predicate, Func<IEnumerable<T>> sourceRetriever)
        {
            IEnumerable<T> results = Query<T>(predicate);
            if (results.Count() == 0)
            {
                results = sourceRetriever();
                Add(results);
            }
            else
            {
                Task.Run(() => Add(sourceRetriever()));
            }
            return results;
        }

        [Verbosity(VerbosityLevel.Information, MessageFormat="Evicted ({LastEvictionCount}) items from cache named {Name}")]
		public event EventHandler Evicted;

		public int LastEvictionCount
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			set;
		}

		public int ItemsMemorySize
		{
			get
			{
                HashSet<CacheItem> itemsCopy = new HashSet<CacheItem>(Items);
				return itemsCopy.Select(c => c.MemorySize).Sum();
			}
		}

		/// <summary>
		/// Start grooming in a background thread;
		/// </summary>
		public void StartGroomer()
		{
			Action<dynamic> groomer = (ctx) =>
			{
				ctx.Cache.Groomer();
			};
			
			if(_groomerThread != null && _groomerThread.ThreadState == ThreadState.Running)
			{
				StopGrooming();
			}

			_groomerThread = groomer.ExecuteInThread(new { Cache = this });

			AppDomain.CurrentDomain.DomainUnload += (o, e) =>
			{
				if (_groomerThread.ThreadState == ThreadState.Running)
				{
					StopGrooming();
				}
			};
		}

		/// <summary>
		/// Evict items from the cache that cause the cache to be larger
		/// than MaxBytes and evict anything in the eviction queue
		/// </summary>
		public void Groom()
		{
			if(_keepGrooming)
			{
				int evictableTailCount = GetEvictableTailCount();
				if (evictableTailCount > 0)
				{
					Evict(evictableTailCount);
				}

				if (_evictionQueue.Count > 0)
				{
					lock(_queueLock)
					{
						while (_evictionQueue.Count > 0)
						{
                            CacheItem item;
                            if(_evictionQueue.TryDequeue(out item))
                            {
                                Evict(item);
                            }
						}
					}
				}
			}
		}

		object _queueLock = new object();
		public void QueueEviction(long id)
		{
			lock(_queueLock)
			{
				_evictionQueue.Enqueue(Retrieve(id));
			}
			_groomerSignal.Set();
		}

		public void QueueEviction(string uuid)
		{
			lock (_queueLock)
			{
				_evictionQueue.Enqueue(Retrieve(uuid));
			}
			_groomerSignal.Set();
		}

		public void Evict(long id)
		{
			Evict(Retrieve(id));
		}

		public void Evict(string uuid)
		{
			Evict(Retrieve(uuid));
		}

        public void Evict(CacheItem item)
        {
            HashSet<CacheItem> itemsCopy = new HashSet<CacheItem>(Items.Where(ci => !ci.Equals(item)));
            Items = itemsCopy;
            SetCollectionsAndLookup();
            
            FireEvent(Evicted, new CacheEventArgs { Cache = this, RemovedItems = new CacheItem[] { item } });
        }

        /// <summary>
        /// Remove the specified number of entries from the end of 
        /// the Cache; all entries are sorted descending by hits
        /// </summary>
        /// <param name="count"></param>
        public void Evict(int count)
        {
            HashSet<CacheItem> removed = new HashSet<CacheItem>();
            HashSet<CacheItem> itemsCopy = new HashSet<CacheItem>(Items);

            LastEvictionCount = count > itemsCopy.Count ? Items.Count : count;
            int firstCount = itemsCopy.Count - count;
            if (firstCount < 0)
            {
                removed = new HashSet<CacheItem>(itemsCopy);
                itemsCopy = new HashSet<CacheItem>();
            }
            else
            {
                for (int i = 0; i < firstCount; i++)
                {
                    itemsCopy.Add(ItemsByHits[i]);
                }
                for (int i = firstCount; i < itemsCopy.Count; i++)
                {
                    removed.Add(ItemsByHits[i]);
                }
            }

            Items = itemsCopy;
            SetCollectionsAndLookup();
            FireEvent(Evicted, new CacheEventArgs { Cache = this, RemovedItems = removed.ToArray() });
        }

		public void Evict(Func<object, bool> predicate)
		{
            HashSet<CacheItem> itemsCopy = new HashSet<CacheItem>(Items);
            HashSet<CacheItem> kept = new HashSet<CacheItem>();
            List<CacheItem> removed = new List<CacheItem>();
            itemsCopy.Each(new { Removed = removed, Kept = kept }, (ctx, ci) =>
            {
                if (predicate(ci.Value))
                {
                    ctx.Removed.Add(ci);
                }
                else
                {
                    ctx.Kept.Add(ci);
                }
            });
            LastEvictionCount = removed.Count();
            Items = kept;
            SetCollectionsAndLookup();

			FireEvent(Evicted, new CacheEventArgs { Cache = this, RemovedItems = removed.ToArray() });
		}

        /// <summary>
        /// Get the count of items at the tail of the 
        /// cache that can be evicted if any.  This value is
        /// determined by comparing the ammount of memory used
        /// by the cache vs the MaxBytes property value
        /// </summary>
        /// <returns></returns>
		protected int GetEvictableTailCount()
		{
			int maxBytes = MaxBytes;
			int bytesOver = ItemsMemorySize - MaxBytes;
			int result = 0;
			if(bytesOver > 0)
			{
				int currentTailSize = 0;
				int currentItemCount = 0;
				for (int i = Items.Count - 1; i >= 0; i--)
				{
					currentItemCount++;
					CacheItem item = ItemsByHits[i];
					currentTailSize += item.MemorySize;
					if(currentTailSize > bytesOver)
					{
						result = currentItemCount;
						break;
					}
				}
			}

			return result;
		}
		
		private void Groomer() // referenced by dynamic setup of _groomerThread; see StartGroomer
		{
			while(_keepGrooming)
			{
				_groomerSignal.WaitOne();
				Groom();
			}
		}
		
		private void StopGrooming()
		{
			_keepGrooming = false;
			_groomerThread.Abort();
			_groomerThread.Join(3000);
		}

        private void SetCollectionsAndLookup()
        {
            List<CacheItem> itemsByHits = new List<CacheItem>(Items);
            itemsByHits.Sort((x, y) => y.Hits.CompareTo(x.Hits));

            List<CacheItem> itemsByMisses = new List<CacheItem>(Items);
            itemsByMisses = new List<CacheItem>(Items);
            itemsByMisses.Sort((x, y) => x.Misses.CompareTo(y.Misses));

            HashSet<CacheItem> itemsCopy = new HashSet<CacheItem>(Items);
            Dictionary<long, CacheItem> itemsById = itemsCopy.ToDictionary(ci => ci.Id);
            Dictionary<string, CacheItem> itemsByUuid = itemsCopy.ToDictionary(ci => ci.Uuid);

            ItemsByHits = itemsByHits;
            ItemsByMisses = itemsByMisses;
            ItemsById = itemsById;
            ItemsByUuid = itemsByUuid;
        }
    }
}
