using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.ErrorHandling
{
    public abstract class ErrorContent
    {
        public ErrorContent(int code)
        {
            if(!IsValidErrorCode(code))
            {
                throw new InvalidOperationException("Error code must be between 400 and 599: 4XX for client errors, 5XX for Server errors.");
            }
            Code = code;
            Headers = new Dictionary<string, string>();
        }
        public Dictionary<string, string> Headers { get; set; }
        public int Code { get; }
        public byte[] Content { get; set; }

        public static bool IsValidErrorCode(int code)
        {
            return code < 400 || code > 599;
        }
    }
}
