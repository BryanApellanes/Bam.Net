using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Rpc
{
    public class RpcBatch: RpcMessage, IRpcRequest
    {
        public static implicit operator IRpcRequest[](RpcBatch batch)
        {
            return batch.Requests;
        }

        public IHttpContext HttpContext { get; set; }

        public IRpcRequest[] Requests { get; set; }

        public RpcResponse Execute()
        {
            RpcBatchResonse response = new RpcBatchResonse();
            Parallel.ForEach(Requests, (request) =>
            {
                RpcResponse rpcResponse = request.Execute();
                if (request.Is<RpcRequest>()) // exclude Notifications from the response
                {
                    response.AddResponse(rpcResponse);
                }
            });
            return response;
        }
    }
}
