using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public class SecureChannelInvokeException: Exception
    {
        public SecureChannelInvokeException(string className, string methodName, string jsonParams) : base($"Unexpected execution result for {className}.{methodName}({jsonParams})")
        { }
    }
}
