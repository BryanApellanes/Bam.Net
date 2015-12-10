using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Rpc
{
    public static class RpcFaults
    {
        static Dictionary<long, RpcFaultCode> _codes;
        static RpcFaults()
        {
            _codes = new Dictionary<long, RpcFaultCode>();
            //-32700 	Parse error 	Invalid JSON was received by the server.
            _codes.Add(-32700, new RpcFaultCode(-32700, "Parse error", "Invalid JSON was received by the server.\r\nAn error occurred on the server while parsing the JSON text."));
            //
            //-32600 	Invalid Request 	The JSON sent is not a valid Request object.
            _codes.Add(-32600, new RpcFaultCode(-32600, "Invalid Request", "The JSON sent is not a valid Request object."));
            //-32601 	Method not found 	The method does not exist / is not available.
            _codes.Add(-32601, new RpcFaultCode(-32601, "Method not found", "The method does not exist / is not available."));
            //-32602 	Invalid params 	Invalid method parameter(s).
            _codes.Add(-32602, new RpcFaultCode(-32602, "Invalid params", "Invalid method parameter(s)."));
            //-32603 	Internal error 	Internal JSON-RPC error.
            _codes.Add(-32603, new RpcFaultCode(-32603, "Internal error", "Internal JSON-RPC error."));

            //-32000 to -32099 	Server error 	Reserved for implementation-defined server-errors.
            for (long i = -32099; i <= -3200; i++)
            {
                _codes.Add(i, new RpcFaultCode(i, "Reserved", "Reserved for implementation-defined server-errors."));
            }
        }

        public static Dictionary<long, RpcFaultCode> ByCode
        {
            get
            {
                return _codes;
            }
        }
    }
}
