using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Rpc
{
    public class RpcError
    {
        public long Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
