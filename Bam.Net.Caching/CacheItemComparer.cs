using Bam.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching
{
    public class CacheItemComparer : IComparer<CacheItem>
    {
        bool _hits;
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
        public SortOrder SortOrder { get; set; }
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
