using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Server;

namespace Bam.Net.Services
{
    public class RequestRouter
    {
        public const string Route = "{Protocol}://{Domain}/$PathName/{PathAndQuery}";
        public RequestRouter() : this("api") { }
        public RequestRouter(string pathName)
        {
            PathName = pathName;
        }

        /// <summary>
        /// The prefix of the path
        /// </summary>
        public string PathName { get; set; }


        public RequestRoute ParseUrl(string url)
        {
            return ParseUrl(new Uri(url));
        }

        public RequestRoute ParseUrl(Uri uri)
        {
            RouteParser parser = new RouteParser(Route.Replace("$PathName", PathName));
            RequestRoute result = new RequestRoute { PathName = PathName, OriginalUrl = uri };
            Dictionary<string, string> values = parser.ParseRouteInstance(uri.ToString());
            result.Protocol = values["Protocol"];
            result.Domain = values["Domain"];
            result.PathAndQuery = values["PathAndQuery"];
            result.ParsedValues = values;
            return result;
        }
    }
}
