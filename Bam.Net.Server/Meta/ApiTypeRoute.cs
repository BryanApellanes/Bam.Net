using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Meta
{
    public class ApiTypeRoute : TypeRoute
    {
        public ApiTypeRoute()
        {
            PathPrefix = "api";
        }

        public static ApiTypeRoute Parse(string uri)
        {
            ApiTypeRoute route = new ApiTypeRoute();
            return (ApiTypeRoute)route.ParseRoute(uri);
        }
    }
}
