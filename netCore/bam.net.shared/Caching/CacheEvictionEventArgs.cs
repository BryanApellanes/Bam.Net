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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class CacheEvictionEventArgs: EventArgs
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheEvictionEventArgs"/> class.
        /// </summary>
        public CacheEvictionEventArgs() { }

        /// <summary>
        /// Gets or sets the cache.
        /// </summary>
        /// <value>
        /// The cache.
        /// </value>
        public Cache Cache { get; set; }

        /// <summary>
        /// Gets or sets the evicted items.
        /// </summary>
        /// <value>
        /// The evicted items.
        /// </value>
        public CacheItem[] EvictedItems { get; set; }
	}
}
