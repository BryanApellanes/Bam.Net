using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    internal class Etags
    {
        static Etags()
        {
            Values = new ConcurrentDictionary<string, string>();
            LastModified = new ConcurrentDictionary<string, DateTime>();
        }

        internal static ConcurrentDictionary<string, string> Values { get; set; }
        internal static ConcurrentDictionary<string, DateTime> LastModified { get; set; }
    }
}
