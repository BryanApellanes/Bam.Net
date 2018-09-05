/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Bam.Net.Server.Streaming
{
    public class SecureStreamingContext<T> : SecureStreamingContext
    {
        public new SecureStreamingRequest<T> Request { get; set; }
    }

    public class SecureStreamingContext: StreamingContext
    {
        public SecureStreamingContext() { }

        public new SecureStreamingRequest Request { get; set; }        
    }
}
