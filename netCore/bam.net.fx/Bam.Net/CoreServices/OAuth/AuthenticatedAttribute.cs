using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.CoreServices.OAuth
{
    /// <summary>
    /// Request filter that ensures that the user is logged in,
    /// the request is for the current application and the user is
    /// a part of the organization of the current application.
    /// </summary>
    public class AuthenticatedAttribute : RequestFilterAttribute
    {
        public AuthenticatedAttribute()
        {
        }

        public override bool RequestIsAllowed(ExecutionRequest request, out string failureMessage)
        {
            bool result = false;
            failureMessage = null;
            ApplicationProxyableService service = request.Instance as ApplicationProxyableService;
            if(service == null)
            {
                MethodInfo method = request.MethodInfo;
                string messageFormat = $"Invalid {nameof(AuthenticatedAttribute)} addorned invalid type, must be placed on ApplicationProxyableService or derivative";
                failureMessage = string.Format(messageFormat, method.DeclaringType.Name, method.Name);
                request.Logger.Warning(messageFormat, method.DeclaringType.Name, method.Name);
            }

            result = service.UserIsLoggedIn() && service.RequestIsForCurrentApplication(true) && service.UserIsInApplicationOrganization();
            return result;
        }
    }
}
