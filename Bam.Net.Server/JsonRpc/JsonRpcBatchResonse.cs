using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.JsonRpc
{
    public class JsonRpcBatchResonse: JsonRpcResponse
    {
        public JsonRpcBatchResonse()
        {
            this._responses = new List<JsonRpcResponse>();
        }
        List<JsonRpcResponse> _responses;
        public JsonRpcResponse[] Responses
        {
            get
            {
                return _responses.ToArray();
            }
            set
            {
                _responses = new List<JsonRpcResponse>(value);
            }
        }
        public override object GetOutput()
        {
            return Responses;
        }

        public void AddResponse(JsonRpcResponse response)
        {
            _responses.Add(response);
        }
    }
}
