using Bam.Net.Server.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class KeyValueRequest: SecureStreamingRequest
    {
        public KeyValueRequestTypes Type { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
