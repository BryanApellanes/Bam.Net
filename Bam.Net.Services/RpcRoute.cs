using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Server;

namespace Bam.Net.Services
{
    public class RpcRoute
    {
        public const string MethodRoute = "{ClassName}/{MethodName}.{Ext}?{NamedParameters}";
        public RpcRoute(string pathAndQuery)
        {
            RouteParser parser = new RouteParser(MethodRoute);
            Dictionary<string, string> values = parser.ParseRouteInstance(pathAndQuery);
            ClassName = values["ClassName"];
            MethodName = values["MethodName"];
            NamedParameters = values["NamedParameters"];
            PathAndQuery = pathAndQuery;
        }
        public string PathAndQuery { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string NamedParameters { get; set; }
        public Dictionary<string, string> QueryStringParameters
        {
            get
            {
                return NamedParameters.QueryStringToDictionary();
            }
        }
    }
}
