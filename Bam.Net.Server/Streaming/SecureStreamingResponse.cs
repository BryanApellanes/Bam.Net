using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Streaming
{
    [Serializable]
    public class SecureStreamingResponse<T> : SecureStreamingResponse
    {
        public new T Data { get; set; }
    }

    [Serializable]
    public class SecureStreamingResponse: StreamingResponse
    {
        public bool Success { get; set; }
        public string SessionId { get; set; }    
        public string PublicKey { get; set; }
    }
}
