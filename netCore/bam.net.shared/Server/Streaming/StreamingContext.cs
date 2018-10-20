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
    public class StreamingContext<T>: StreamingContext
    {
        public new StreamingRequest<T> Request { get; set; }
    }

    public class StreamingContext
    {
        public StreamingContext() { }
        
        public StreamingRequest Request { get; set; }

        public NetworkStream ResponseStream { get; set; }

        public Encoding Encoding { get; set; }
    }
}
