using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Bam.Net.CoreServices;
using System.Reflection;

namespace Bam.Net.Incubation.Mvc
{
    public class BamControllerFactory : DefaultControllerFactory
    {
        public BamControllerFactory(ServiceRegistry serviceRegistry)
        {
            ServiceRegistry = serviceRegistry;
        }

        protected ServiceRegistry ServiceRegistry { get; set; }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            List<object> parameters = ServiceRegistry.GetCtorParams(controllerType, out ConstructorInfo ctor);
            return ctor.Invoke(parameters.ToArray()) as IController;
        }
    }
}
