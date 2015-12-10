using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Rpc
{
    public class RpcBatchResonse: RpcResponse
    {
        public RpcBatchResonse()
        {
            this._responses = new List<RpcResponse>();
        }
        List<RpcResponse> _responses;
        public RpcResponse[] Responses
        {
            get
            {
                return _responses.ToArray();
            }
            set
            {
                _responses = new List<RpcResponse>(value);
            }
        }
        public override object GetOutput()
        {
            return Responses;
        }

        public void AddResponse(RpcResponse response)
        {
            _responses.Add(response);
        }
    }
}
