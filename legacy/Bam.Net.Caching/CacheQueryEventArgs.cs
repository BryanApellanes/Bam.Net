using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;

namespace Bam.Net.Caching
{
    public class CacheQueryEventArgs<T>: EventArgs
    {
        public Type Type { get; set; }

        /// <summary>
        /// The filter used for the query; may be null
        /// in cases where a different query overload
        /// was used not requiring a QueryFilter
        /// </summary>
        public QueryFilter QueryFilter { get; set; }

        public IEnumerable<T> Results{ get; set; }
    }
}
