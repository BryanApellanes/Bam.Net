using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Meta
{
    public class MethodRoute
    {
        public MethodRoute()
        {
            VersionRoute = "{Version}/{TypeName}/{MethodName}";
            Route = "{TypeName}/{MethodName}";
        }

        public string VersionRoute { get; set; }
        public string Route { get; set; }
        public string Version { get; set; }
        public string TypeName { get; set; }
        public string MethodName { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(TypeName) && !string.IsNullOrEmpty(MethodName);
        }
        public bool IsVersioned()
        {
            return IsValid() && !string.IsNullOrEmpty(Version);
        }

        public bool Parse(string pathAndQuery)
        {
            RouteParser versionedParser = new RouteParser(VersionRoute);
            MethodRoute versionedMethod = versionedParser.ParseRouteInstance(pathAndQuery).ToInstance<MethodRoute>();
            if (versionedMethod.IsVersioned())
            {
                this.CopyProperties(versionedMethod);
                return true;
            }
            RouteParser parser = new RouteParser(Route);
            MethodRoute methodRoute = parser.ParseRouteInstance(pathAndQuery).ToInstance<MethodRoute>();
            if (methodRoute.IsValid())
            {
                this.CopyProperties(methodRoute);
                return true;
            }
            return false;
        }
    }
}
