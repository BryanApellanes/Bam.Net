using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching
{
    public class CacheRetrieveEventArgs<T>: EventArgs
    {
        public T Item { get; set; }
    }

    public class CacheRetrieveEventArgs: EventArgs
    {
        public Type Type { get; set; }
        public object Item { get; set; }
    }
}
