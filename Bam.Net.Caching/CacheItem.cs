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
    [Serializable]
    public class CacheItem<T>: CacheItem where T: IMemorySize, new()
    {
        public CacheItem(T value, IMetaProvider metaProvider) : base(value, metaProvider)
        {
        }

        public new T Value
        {
            get
            {
                return (T)base.Value;
            }            
        }

        public override int MemorySize => Value.MemorySize();
    }

    [Serializable]
	public class CacheItem
	{
		public CacheItem(object value, IMetaProvider metaProvider)
		{
			Value = value;			
			Meta = metaProvider.GetMeta(this);			
			Created = DateTime.UtcNow;
            Id = value.Property<long>("Id", false);
            Uuid = value.Property<string>("Uuid", false);
            Cuid = value.Property<string>("Cuid", false);
		}

		protected Meta Meta { get; set; }

		public long Id
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
            get;set;
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
