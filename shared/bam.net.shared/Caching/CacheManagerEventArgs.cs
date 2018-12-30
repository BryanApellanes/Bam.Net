using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching
{
    public class CacheManagerEventArgs: EventArgs
    {
        public string TypeName { get { return Type.Name; } }
        public Type Type { get; set; }
        public Cache Cache { get; set; }
    }
}
