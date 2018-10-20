using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.Incubation;

namespace Bam.Net.ServiceProxy
{
    public class JsClientProxyGenerator : ClientProxyGenerator
    {
        public override void BeforeWriteProxyCode(Incubator serviceProvider, IHttpContext context)
        {
            IResponse response = context.Response;
            response.ContentType = "application/javascript";
        }
        public override string GetProxyCode(Incubator serviceProvider, IHttpContext context)
        {
            IRequest request = context.Request;
            bool includeLocalMethods = request.UserHostAddress.StartsWith("127.0.0.1");

            return ServiceProxySystem.GenerateJsProxyScript(serviceProvider, serviceProvider.ClassNames, includeLocalMethods, context.Request).ToString();
        }
    }
}
