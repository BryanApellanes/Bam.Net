using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Rpc
{
    public class RpcFaultCode
    {
        public static implicit operator long(RpcFaultCode code)
        {
            return code.Code;
        }
        public static implicit operator string(RpcFaultCode faultCode)
        {
            return "{0}:: {1}"._Format(faultCode.Message, faultCode.Meaning);
        }

        public RpcFaultCode(long code, string message, string meaning)
        {
            this.Code = code;
            this.Message = message;
            this.Meaning = meaning;
        }

        public long Code { get; set; }
        public string Message { get; set; }
        public string Meaning { get; set; }

    }
}
