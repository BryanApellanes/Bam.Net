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
        public new T Data { get; set; }
    }

    [Serializable]
    public class StreamingResponse
    {
        public object Data { get; set; }
    }
}
