using Bam.Net.Data;
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
    public class CacheItemComparer : IComparer<CacheItem>
    {
        bool _hits;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CacheItemComparer"/> compares by hits.
        /// </summary>
        /// <value>
        ///   <c>true</c> if hits; otherwise, <c>false</c>.
        /// </value>
        public bool Hits
        {
            get
            {
                return _hits;
            }
            set
            {
                _hits = value;
                _misses = !value;
            }
        }

        bool _misses;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CacheItemComparer"/> compares by misses.
        /// </summary>
        /// <value>
        ///   <c>true</c> if misses; otherwise, <c>false</c>.
        /// </value>
        public bool Misses
        {
            get
            {
                return _misses;
            }
            set
            {
                _misses = value;
                _hits = !value;
            }
        }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.
        /// </returns>
        public int Compare(CacheItem x, CacheItem y)
        {
            if (Hits)
            {
                if(SortOrder == SortOrder.Ascending)
                {
                    return x.Hits.CompareTo(y.Hits);
                }
                return y.Hits.CompareTo(x.Hits);
            }
            else
            {
                if(SortOrder == SortOrder.Ascending)
                {
                    return x.Misses.CompareTo(y.Misses);
                }
                return x.Misses.CompareTo(y.Misses);
            }
        }
    }
}
