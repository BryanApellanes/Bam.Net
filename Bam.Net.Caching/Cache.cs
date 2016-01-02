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

namespace Bam.Net.Caching
{
	public class Cache: Loggable
	{
		object _evictionLock = new object();
		bool _keepGrooming;
		Thread _groomerThread;
		AutoResetEvent _groomerSignal;
		Queue<CacheItem> _evictionQueue;
		
		public Cache() : this(true) { }

		public Cache(bool groomInBackground)
			: this(System.Guid.NewGuid().ToString(), groomInBackground)
		{ }
		public Cache(string name, bool groomInBackground)
			: this(name, 524288, groomInBackground) // 512 kilobytes
		{ }

		public Cache(string name, int maxBytes, bool groomInBackground)
		{
			this.Items = new HashSet<CacheItem>();
			this.ItemsByHits = new List<CacheItem>();
			this.ItemsByMisses = new List<CacheItem>();
			this.ItemsById = new Dictionary<long, CacheItem>();
			
			this.MetaProvider = Bam.Net.Data.Repositories.MetaProvider.Default;
			
			this.Name = name;
			this.MaxBytes = maxBytes;

			this._keepGrooming = true;
			this._groomerSignal = new AutoResetEvent(false);
			this._evictionQueue = new Queue<CacheItem>();
			
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

		public CacheItem Retrieve(long id)
		{
			lock(_evictionLock)
			{
				CacheItem result = null;
				if (ItemsById.ContainsKey(id))
				{
					result = ItemsById[id];
					result.IncrementHits();
				}
				return result;
			}			
		}

		public CacheItem Retrieve(string uuid)
		{
			lock (_evictionLock)
			{
				CacheItem result = null;
				if (ItemsByUuid.ContainsKey(uuid))
				{
					result = ItemsByUuid[uuid];
					result.IncrementHits();
				}

				return result;
			}
		}

		public virtual CacheItem Add(object value)
		{
			lock (_evictionLock)
			{
				CacheItem item = new CacheItem(value, MetaProvider);				
				Items.Add(item);
				SetCollections();
				_groomerSignal.Set();
				return item;
			}
		}

		public virtual List<CacheItem> Add(IEnumerable<object> values)
		{
			return Add(values.ToArray());
		}

		public virtual List<CacheItem> Add(params object[] values)
		{
			lock(_evictionLock)
			{
				List<CacheItem> results = new List<CacheItem>();
				foreach (object value in values)
				{
					CacheItem item = new CacheItem(value, MetaProvider);
					Items.Add(item);
					results.Add(item);
				}
				SetCollections();
				_groomerSignal.Set();
				return results;
			}
		}

		public IEnumerable<object> Query(Predicate<object> predicate)
		{
			lock (_evictionLock)
			{
				HashSet<CacheItem> results = new HashSet<CacheItem>(Items.Where(ci => predicate(ci.Value)).ToList());
				Items.Each(new { Hits = results }, (ctx, ci) =>
				{
					if (!ctx.Hits.Contains(ci))
					{
						ci.IncrementMisses();
					}
					else
					{
						ci.IncrementHits();
					}
				});
				return results;
			}
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
				return Items.Select(c => c.MemorySize).Sum();
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
							CacheItem item = _evictionQueue.Dequeue();
							Evict(item);
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
			if (Items.Contains(item))
			{
				lock (_evictionLock)
				{
					Items.Remove(item);
					SetCollections();
				}
				
				FireEvent(Evicted, new CacheEventArgs { Cache = this, RemovedItems = new CacheItem[] { item } });
			}
		}
	
		/// <summary>
		/// Remove the specified number of entries from the end of 
		/// the Cache; all entries are sorted descending by hits
		/// </summary>
		/// <param name="count"></param>
		public void Evict(int count)
		{
			HashSet<CacheItem> removed = new HashSet<CacheItem>();
			lock(_evictionLock)
			{
				LastEvictionCount = count > Items.Count ? Items.Count : count;
				int firstCount = Items.Count - count;				
				if (firstCount < 0)
				{
					removed = Items;
					Items = new HashSet<CacheItem>();
				}
				else
				{
					HashSet<CacheItem> tempItems = new HashSet<CacheItem>();
					for (int i = 0; i < firstCount; i++)
					{
						tempItems.Add(ItemsByHits[i]);
					}
					for(int i = firstCount; i < Items.Count; i++)
					{
						removed.Add(ItemsByHits[i]);
					}
					Items = tempItems;
				}

				SetCollections();
			}

			FireEvent(Evicted, new CacheEventArgs { Cache = this, RemovedItems = removed.ToArray() });
		}

		public void Evict(Func<object, bool> predicate)
		{
			List<CacheItem> removed = new List<CacheItem>();
			lock(_evictionLock)
			{
				removed = Items.Where(ci => predicate(ci.Value)).ToList();
				LastEvictionCount = Items.RemoveWhere(ci => predicate(ci.Value));
				SetCollections();
			}

			FireEvent(Evicted, new CacheEventArgs { Cache = this, RemovedItems = removed.ToArray() });
		}

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

		private void SetCollections()
		{
			ItemsByHits = new List<CacheItem>(Items);
			ItemsByHits.Sort((x, y) => y.Hits.CompareTo(x.Hits));
			ItemsByMisses = new List<CacheItem>(Items);
			ItemsByMisses.Sort((x, y) => x.Misses.CompareTo(y.Misses));
			ItemsById = Items.ToDictionary(ci => ci.Id);
			ItemsByUuid = Items.ToDictionary(ci => ci.Uuid);
		}
	}
}
