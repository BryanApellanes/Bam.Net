using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    internal class ResponseCache
    {
        static ResponseCache()
        {
            Etag = new ConcurrentDictionary<string, string>();
            LastModified = new ConcurrentDictionary<string, DateTime>();
        }

        public static ConcurrentDictionary<string, string> Etag { get; set; }
        public static ConcurrentDictionary<string, DateTime> LastModified { get; set; }
    }
}
