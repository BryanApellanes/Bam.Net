using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.JsonRpc
{
    public class JsonRpcResponse: JsonRpcMessage
    {
        public object Result { get; set; }
        public JsonRpcError Error { get; set; }
        public object Id { get; set; }

        /// <summary>
        /// Get the object/value intended to be rendered to the
        /// output stream as json.  This method exists to 
        /// enable the JsonRpcBatch to return an array and the JsonRpcRequest
        /// to return a single JsonRpcResponse
        /// </summary>
        /// <returns></returns>
        public virtual object GetOutput()
        {
            return this;
        }
    }
}
