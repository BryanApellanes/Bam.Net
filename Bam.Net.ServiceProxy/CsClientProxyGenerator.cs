using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;

namespace Bam.Net.ServiceProxy
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
            string defaultBaseAddress = ServiceProxySystem.GetBaseAddress(request);
            string nameSpace = request.QueryString["namespace"] ?? "ServiceProxyClients";
            string contractNameSpace = "{0}.Contracts"._Format(nameSpace);
            string[] classNames = request.QueryString["classes"] == null ? serviceProvider.ClassNames : request.QueryString["classes"].DelimitSplit(",", ";");
            bool includeLocalMethods = request.UserHostAddress.StartsWith("127.0.0.1");

            return Generate(defaultBaseAddress, classNames, nameSpace, contractNameSpace, serviceProvider, Logger, includeLocalMethods).ToString();
        }

        public static StringBuilder Generate(string defaultBaseAddress, string[] classNames, string nameSpace, string contractNamespace, Incubator incubator, ILogger logger = null, bool includeLocalMethods = false)
        {
            logger = logger ?? Log.Default;
            List<Type> types = new List<Type>();
            classNames.Each(new { Logger = logger, Types = types }, (ctx, cn) =>
            {
                Type type = incubator[cn];
                if (type == null)
                {
                    ctx.Logger.AddEntry("Specified class name was not registered: {0}", LogEventType.Warning, cn);
                }
                else
                {
                    ctx.Types.Add(type);
                }
            });
            Args.ThrowIf(types.Count == 0, "None of the specified classes were found: {0}", string.Join(", ", classNames));
            return ServiceProxySystem.GenerateCSharpProxyCode(defaultBaseAddress, nameSpace, contractNamespace, types.ToArray(), includeLocalMethods);
        }
    }
}
