using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Streaming
{
    [Serializable]
    public class StreamingRequest<T>: StreamingRequest
    {
        public new T Message { get; set; }
    }

    [Serializable]
    public class StreamingRequest
    {
        public object Message { get; set; }
    }
}
