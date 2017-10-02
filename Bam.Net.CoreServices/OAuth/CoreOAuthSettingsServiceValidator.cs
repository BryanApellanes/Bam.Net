using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.CoreServices.OAuth
{
    public class CoreOAuthSettingsServiceValidator : Validator
    {
        public CoreOAuthSettingsServiceValidator()
        {

        }
        public override bool Validate(ExecutionRequest request)
        {
            CoreOAuthSettingsService service = request.Instance as CoreOAuthSettingsService;
            if(service == null)
            {
                MethodInfo method = request.MethodInfo;
                request.Logger.Warning("Invalid Authorizer.Type specified for AuthorizeAttribute for {0}.{1}", method.DeclaringType.Name, method.Name);
                return false;
            }

            return service.UserIsLoggedIn() && service.RequestIsForCurrentApplication(true) && service.UserIsInApplicationOrganization();
        }
    }
}
