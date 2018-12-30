/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Caching
{
    /// <summary>
    /// Represents an item in a cache.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Bam.Net.Caching.CacheItem" />
    [Serializable]
    public class CacheItem<T>: CacheItem where T: IMemorySize, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItem{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="metaProvider">The meta provider.</param>
        public CacheItem(T value, IMetaProvider metaProvider) : base(value, metaProvider)
        {
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public new T Value
        {
            get
            {
                return (T)base.Value;
            }            
        }

        /// <summary>
        /// Gets the size of the value in memory.
        /// </summary>
        /// <value>
        /// The size of the memory.
        /// </value>
        public override int MemorySize => Value.MemorySize();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bam.Net.Caching.CacheItem" />
    [Serializable]
	public class CacheItem
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItem"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="metaProvider">The meta provider.</param>
        public CacheItem(object value, IMetaProvider metaProvider)
		{
			Value = value;			
			Meta = metaProvider.GetMeta(this);			
			Created = DateTime.UtcNow;
            Id = value.Property<ulong>("Id", false);
            Uuid = value.Property<string>("Uuid", false);
            Cuid = value.Property<string>("Cuid", false);
            Name = value.Property<string>("Name", false);
		}

        /// <summary>
        /// Gets or sets the meta.
        /// </summary>
        /// <value>
        /// The meta.
        /// </value>
        protected Meta Meta { get; set; }

		public ulong Id
		{
			get;
			set;
		}

		public string Uuid
		{
			get;
			set;
		}

        public string Cuid
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

		Type _type;
		public Type Type
		{
			get
			{
				if (_type == null)
				{
					if (Value != null)
					{
						_type = Value.GetType();
					}
				}
				return _type;
			}
		}

		public T ValueAs<T>()
		{
			return (T)Value;
		}

		public DateTime Created { get; private set; }
		public DateTime LastRead { get; private set; }
		public object Value { get; private set; }
		public int Hits { get; set; }
		public int Misses { get; set; }

		int _memorySize;
		public virtual int MemorySize 
		{
			get
			{
				if (_memorySize == 0)
				{
					_memorySize = Value.MemorySize();
				}

				return _memorySize;
			}
		}

		internal void IncrementHits()
		{
			++Hits;
			LastRead = DateTime.UtcNow;
		}

		internal void IncrementMisses()
		{
			++Misses;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Value.Equals(obj);
		}
	}
}
