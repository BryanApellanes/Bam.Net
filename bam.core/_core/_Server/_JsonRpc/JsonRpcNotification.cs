using Bam.Net.Incubation;
using Bam.Net.Server.ServiceProxy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.JsonRpc
{
    public partial class JsonRpcNotification
    {
        public virtual JsonRpcResponse Execute()
        {
            JsonRpcResponse response = new JsonRpcResponse();
            // get the method from RpcMethods
            MethodInfo mi = RpcMethods.FirstOrDefault(m => m.Name.Equals(Method, StringComparison.InvariantCultureIgnoreCase));
            // if its not there get it from all methods
            if (mi == null)
            {
                mi = AllMethods.FirstOrDefault(m => m.Name.Equals(Method, StringComparison.InvariantCultureIgnoreCase));
            }
            // if its not there set error in the response
            if (mi == null)
            {
                response = GetErrorResponse(JsonRpcFaultCodes.MethodNotFound);
            }
            else
            {
                ServiceProxyInvocation execRequest = ServiceProxyInvocation.Create(ServiceRegistry, mi, null);//GetInputParameters(mi));
                if (execRequest.Execute())
                {
                    response.Result = execRequest.Result;
                }
                else
                {
                    response = GetErrorResponse(JsonRpcFaultCodes.InternalError);
                }
            }

            return response;
        }
    }
}
