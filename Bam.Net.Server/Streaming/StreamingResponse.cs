using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Streaming
{
    [Serializable]
    public class StreamingResponse<T>: StreamingResponse
    {
        public new T Body { get; set; }
    }

    [Serializable]
    public class StreamingResponse
    {
        public string Message { get; set; }
        public object Body { get; set; }
    }
}
