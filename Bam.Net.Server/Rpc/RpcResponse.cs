using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Rpc
{
    public class RpcResponse: RpcMessage
    {
        public object Result { get; set; }
        public RpcError Error { get; set; }
        public object Id { get; set; }

        /// <summary>
        /// Get the object/value intended to be rendered to the
        /// output stream as json.  This method exists to 
        /// enable the RpcBatch to return an array and the RpcRequest
        /// to return a single RpcResponse
        /// </summary>
        /// <returns></returns>
        public virtual object GetOutput()
        {
            return this;
        }
    }
}
