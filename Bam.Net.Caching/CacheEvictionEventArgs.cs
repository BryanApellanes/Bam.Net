/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching
{
	public class CacheEvictionEventArgs: EventArgs
	{
		public CacheEvictionEventArgs() { }

		public Cache Cache { get; set; }
		public CacheItem[] EvictedItems { get; set; }
	}
}
