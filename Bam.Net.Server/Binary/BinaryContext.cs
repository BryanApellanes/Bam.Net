/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Bam.Net.Server.Binary
{
    public class BinaryContext<T>: BinaryContext
    {
        public new BinaryRequest<T> Request { get; set; }
    }

    public class BinaryContext
    {
        public BinaryContext() { }
        
        public BinaryRequest Request { get; set; }

        public NetworkStream ResponseStream { get; set; }

        public Encoding Encoding { get; set; }
    }
}
