using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.ApplicationRegistration.Dao.Repository;
using Bam.Net.Server;
using Bam.Net.Data.Repositories;
using Bam.Net.CoreServices.OAuth;
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.ServiceProxy;

namespace Bam.Net.CoreServices
{
    public abstract class ApplicationProxyableService: ProxyableService
    {
        public ApplicationProxyableService() { }
        public ApplicationProxyableService(DaoRepository repository, AppConf appConf) 
            : base(repository, appConf)
        { }

        public ApplicationProxyableService(IRepository genericRepo, DaoRepository daoRepo, AppConf appConf) 
            : base(genericRepo, daoRepo, appConf)
        { }

        public ApplicationRegistrationRepository ApplicationRegistrationRepository { get; set; }

        public IApplicationNameProvider ApplicationNameProvider { get; set; }
        public string ServerApplicationName
        {
            get
            {
                if(ApplicationNameProvider != null && ApplicationNameProvider != this)
                {
                    return ApplicationNameProvider.GetApplicationName();
                }
                return ApplicationRegistration.Application.Unknown.Name;
            }
        }

        public string ClientApplicationName
        {
            get
            {
                return base.ApplicationName;
            }
        }

        /// <summary>
        /// The application representing what the server thinks it is
        /// </summary>
        public ApplicationRegistration.Application ServerApplication
        {
            get
            {
                return ApplicationRegistrationRepository.GetOneApplicationWhere(c => c.Name == ServerApplicationName);
            }
        }

        /// <summary>
        /// The application representing what the client requested
        /// </summary>
        public ApplicationRegistration.Application ClientApplication
        {
            get
            {
                return ApplicationRegistrationRepository.GetOneApplicationWhere(c => c.Name == ClientApplicationName);
            }
        }

        /// <summary>
        /// Returns true if the ApplicationNameProvider returns the same 
        /// ApplicationName as is specified by the request headers
        /// </summary>
        /// <returns></returns>
        [Local]
        public bool RequestIsForCurrentApplication(bool dieOnWarning = false)
        {
            if(ApplicationNameProvider == null)
            {
                Logger.Warning("{0} was null, '{1}' will always return true in this case", nameof(ApplicationNameProvider), nameof(RequestIsForCurrentApplication));
                if (dieOnWarning)
                {
                    throw new InvalidOperationException(string.Format("{0} was null, '{1}' will always return true in this case", nameof(ApplicationNameProvider), nameof(RequestIsForCurrentApplication)));
                }
            }
            return ClientApplicationName.Equals(ServerApplicationName);
        }

        [Local]
        public bool UserIsInApplicationOrganization()
        {
            return ServerApplication.Organization.Users.Contains(GetApplicationUser());
        }

        [Local]
        public bool UserIsInOrganization(string organizationName)
        {
            if (organizationName.Equals(Organization.Public.Name))
            {
                return true;
            }
            User user = GetApplicationUserOrDie();
            Organization org = ApplicationRegistrationRepository.OneOrganizationWhere(c => c.Name == organizationName);
            if(org != null)
            {
                return org.Users.Contains(user);
            }
            return false;
        }
        
        /// <summary>
        /// Returns true if the ServerApplication is authorized
        /// for the current domain requested as specified by a
        /// HostDomain entry for the current application
        /// </summary>
        /// <returns></returns>
        [Local]
        public bool HostDomainIsAuthorized()
        {
            IRequest request = HttpContext?.Request;
            string host = request.Url?.Host;
            int port = (request?.Url?.Port).Value;
            Application app = GetServerApplicationOrDie();
            if(app.HostDomains.Count > 0)
            {
                HostDomain hd = app.HostDomains.Where(h => h.DomainName.Equals(host) && h.Port.Equals(port)).FirstOrDefault();
                return hd == null ? false : hd.Authorized;
            }
            else
            {                
                HostDomain hd = new HostDomain
                {
                    Authorized = true,
                    DomainName = host,
                    DefaultApplicationName = app.Name,
                    Port = port
                };
                app.HostDomains.Add(hd);
                Task.Run(() => ApplicationRegistrationRepository.Save(app));
                return true;
            }
        }

        protected internal ApplicationRegistration.User GetApplicationUserOrDie()
        {
            ApplicationRegistration.User user = GetApplicationUser();
            if(user == null)
            {
                throw new ApplicationUserNotFoundException(ApplicationName, UserName);
            }
            return user;
        }

        /// <summary>
        /// Get the User entry from the ApplicationRegistrationRepository for the current user
        /// </summary>
        /// <returns></returns>
        protected internal User GetApplicationUser()
        {
            UserIsLoggedInOrDie();
            return ApplicationRegistrationRepository.GetOneUserWhere(c => c.UserName == UserName);
        }

        protected internal bool UserIsLoggedIn()
        {
            return !CurrentUser.Equals(UserAccounts.Data.User.Anonymous);
        }

        protected internal void UserIsLoggedInOrDie()
        {
            if (CurrentUser.Equals(UserAccounts.Data.User.Anonymous))
            {
                throw new InvalidOperationException("User not logged in");
            }
        }
        
        protected internal Application GetServerApplicationOrDie()
        {
            Application app = ApplicationRegistrationRepository.GetOneApplicationWhere(c => c.Name == ServerApplicationName);
            if (app.Equals(ApplicationRegistration.Application.Unknown))
            {
                throw new InvalidOperationException("Application is Uknown");
            }

            return app;
        }
    }
}
