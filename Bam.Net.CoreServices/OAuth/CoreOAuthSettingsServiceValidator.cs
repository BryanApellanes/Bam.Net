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

        public override bool RequestIsValid(ExecutionRequest request, out string failureMessage)
        {
            bool result = false;
            failureMessage = null;
            CoreOAuthSettingsService service = request.Instance as CoreOAuthSettingsService;
            if(service == null)
            {
                MethodInfo method = request.MethodInfo;
                string messageFormat = $"Invalid {nameof(Validator)}.Type specified for {nameof(ValidateAttribute)} for {0}.{1}";
                failureMessage = string.Format(messageFormat, method.DeclaringType.Name, method.Name);
                request.Logger.Warning(messageFormat, method.DeclaringType.Name, method.Name);
            }

            result = service.UserIsLoggedIn() && service.RequestIsForCurrentApplication(true) && service.UserIsInApplicationOrganization();
            return result;
        }
    }
}
