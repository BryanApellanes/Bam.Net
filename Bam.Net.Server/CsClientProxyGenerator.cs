using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server
{
    public class CsClientProxyGenerator : ClientProxyGenerator
    {
        public override void BeforeWriteProxyCode(Incubator serviceProvider, IHttpContext context)
        {
            IRequest request = context.Request;
            IResponse response = context.Response;
            string nameSpace = request.QueryString["namespace"] ?? "ServiceProxyClients";
            response.Headers.Add("Content-Disposition", $"attachment;filename={nameSpace}.cs");
            response.Headers.Add("Content-Type", "text/plain");
        }
        public override string GetProxyCode(Incubator serviceProvider, IHttpContext context)
        {
            IRequest request = context.Request;
            IResponse response = context.Response;
            string defaultBaseAddress = ServiceProxySystem.GetBaseAddress(request.Url);
            string nameSpace = request.QueryString["namespace"] ?? "ServiceProxyClients";
            string contractNameSpace = "{0}.Contracts"._Format(nameSpace);
            string[] classNames = request.QueryString["classes"] == null ? serviceProvider.ClassNames : request.QueryString["classes"].DelimitSplit(",", ";");
            bool includeLocalMethods = request.UserHostAddress.StartsWith("127.0.0.1");

            return ServiceProxySystem.GenerateCSharpProxyCode(defaultBaseAddress, classNames, nameSpace, contractNameSpace, serviceProvider, Logger, includeLocalMethods).ToString();
        }
    }
}
