using Bam.Net.CoreServices;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Administration
{
    [Encrypt]
    [Proxy("adminSvc")]
    public class AdministrationService : AsyncProxyableService
    {
        public AdministrationService(AsyncCallbackService callbackService, DaoRepository repository, AppConf appConf) : base(callbackService, repository, appConf)
        {
        }

        public override object Clone()
        {
            AdministrationService svc = new AdministrationService(CallbackService, DaoRepository, AppConf);
            svc.CopyProperties(this);
            svc.CopyEventHandlers(this);
            return svc;
        }

        [RoleRequired("/AccessDenied", "Admin")]
        public async virtual Task<CoreServiceResponse> GetAllUsers()
        {
            try
            {
                if (CurrentUser.Equals(UserAccounts.Data.User.Anonymous))
                {
                    return new CoreServiceResponse { Success = false, Message = "You must be logged in to do that" };
                }

                List<User> users = new List<User>();
                await ApplicationRegistrationRepository.BatchAllUsers(100, (u) => users.AddRange(u));

                return new CoreServiceResponse
                {
                    Success = true,
                    Data = users
                };
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception occurred in {0}", ex, nameof(AdministrationService.GetAllUsers));
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
        }


        [RoleRequired("/AccessDenied", "Admin")]
        public virtual CoreServiceResponse GetAllUserOrganizationApplications(string userIdentifier)
        {
            try
            {
                if (CurrentUser.Equals(UserAccounts.Data.User.Anonymous))
                {
                    return new CoreServiceResponse { Success = false, Message = "You must be logged in to do that" };
                }
                User user = ApplicationRegistrationRepository.OneUserWhere(c => c.UserName == userIdentifier || c.Email == userIdentifier);
                if (user == null)
                {
                    return new CoreServiceResponse { Success = false, Message = "Specified user not found" };
                }
                return new CoreServiceResponse
                {
                    Success = true,
                    Data = user.Organizations.Select(o => new
                    {
                        o.Name,
                        Applications = o.Applications.Select(a => a.Name)
                    })
                };
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception occurred in {0}", ex, nameof(AdministrationService.GetAllUserOrganizationApplications));
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
        }
    }
}
