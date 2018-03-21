using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Meta
{
    public abstract class TypeRoute
    {
        public string Route
        {
            get { return string.Format("{Protocol}://{Domain}/{0}/{PathAndQuery}", PathPrefix); }
        }
        
        public string PathPrefix { get; set; }
        public string Protocol { get; set; }
        public string Domain { get; set; }
        public string PathAndQuery { get; set; }      
        public MethodRoute MethodRoute { get; set; }
        

        public virtual TypeRoute ParseRoute(string uri)
        {
            RouteParser parser = new RouteParser(Route);
            TypeRoute route = (TypeRoute)parser.ParseRouteInstance(uri).ToInstance(this.GetType());
            route.ParseMethod();
            return route;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Protocol) && !string.IsNullOrEmpty(Domain) && !string.IsNullOrEmpty(PathAndQuery);
        }

        public bool ParseMethod()
        {
            if (!IsValid())
            {
                return false;
            }
            MethodRoute = new MethodRoute();
            return MethodRoute.Parse(PathAndQuery);
        }
    }
}
