using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.JsonRpc
{
    public class JsonRpcBatch: JsonRpcMessage, IJsonRpcRequest
    {
        [Exclude]
        public object Clone()
        {
            JsonRpcBatch clone = new JsonRpcBatch();
            clone.CopyProperties(this);
            return clone;
        }
        public static implicit operator IJsonRpcRequest[](JsonRpcBatch batch)
        {
            return batch.Requests;
        }

        public IHttpContext HttpContext { get; set; }

        public IJsonRpcRequest[] Requests { get; set; }

        public JsonRpcResponse Execute()
        {
            JsonRpcBatchResonse response = new JsonRpcBatchResonse();
            Parallel.ForEach(Requests, (request) =>
            {
                JsonRpcResponse rpcResponse = request.Execute();
                if (request.Is<JsonRpcRequest>()) // exclude Notifications from the response
                {
                    response.AddResponse(rpcResponse);
                }
            });
            return response;
        }
    }
}
