using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.JsonRpc
{
    [AttributeUsage(AttributeTargets.Method)]
    public class JsonRpcMethodAttribute: Attribute
    {
    }
}
