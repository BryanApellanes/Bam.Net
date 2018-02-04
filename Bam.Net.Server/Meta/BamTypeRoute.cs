using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Meta
{
    public class BamTypeRoute : TypeRoute
    {
        public BamTypeRoute()
        {
            PathPrefix = "bam";
        }

        public static BamTypeRoute Parse(string uri)
        {
            BamTypeRoute route = new BamTypeRoute();
            return (BamTypeRoute)route.ParseRoute(uri);
        }
    }
}
