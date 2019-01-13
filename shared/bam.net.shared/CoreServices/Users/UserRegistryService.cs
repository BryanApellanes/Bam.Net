using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Data;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts;
using Bam.Net.UserAccounts.Data;
using Bam.Net.Messaging;
using Bam.Net.Encryption;

namespace Bam.Net.CoreServices
{
    [Proxy("userRegistrySvc")]
    [Encrypt]
    [ServiceSubdomain("userregistry")]
    public class UserRegistryService: ApplicationProxyableService, IUserManager, IUserResolver, IRoleResolver
    {
        protected UserRegistryService() { } // to enable auto proxy gen
        public UserRegistryService(IDatabaseProvider dbProvider, IUserManager wrapped, IApplicationNameProvider appNameProvider, IUserResolver userResolver, IRoleResolver roleResolver)
        {
            DatabaseProvider = dbProvider;
            UserManager = wrapped;
            ApplicationNameProvider = appNameProvider;
            UserResolver = userResolver;
            RoleResolver = roleResolver;
            dbProvider.SetDatabases(this);
            dbProvider.SetDatabases(UserManager);
            dbProvider.SetDatabases(ApplicationNameProvider);
            WireUserManagementEvents();
        }   

        [Exclude]
        public override object Clone()
        {
            UserRegistryService clone = new UserRegistryService(DatabaseProvider, UserManager, ApplicationNameProvider, UserResolver, RoleResolver);
            clone.CloneProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
        
        public Func<string, string> GetConfirmationUrlFunction
        {
            get
            {
                return UserManager.GetConfirmationUrlFunction;
            }

            set
            {
                UserManager.GetConfirmationUrlFunction = value;
            }
        }
        
        public Func<string, string> GetPasswordResetUrlFunction
        {
            get
            {
                return UserManager.GetPasswordResetUrlFunction;
            }

            set
            {
                UserManager.GetPasswordResetUrlFunction = value;
            }
        }

        public int PasswordResetTokensExpireInThisManyMinutes
        {
            get
            {
                return UserManager.PasswordResetTokensExpireInThisManyMinutes;
            }

            set
            {
                UserManager.PasswordResetTokensExpireInThisManyMinutes = value;
            }
        }

        public string SmtpSettingsVaultPath
        {
            get
            {
                return UserManager.SmtpSettingsVaultPath;
            }

            set
            {
                UserManager.SmtpSettingsVaultPath = value;
            }
        }

        public event EventHandler ConfirmAccountFailed;
        public event EventHandler ConfirmAccountSucceeded;
        public event EventHandler ForgotPasswordFailed;
        public event EventHandler ForgotPasswordSucceeded;
        public event EventHandler LoginFailed;
        public event EventHandler LoginSucceeded;
        public event EventHandler RequestConfirmationEmailFailed;
        public event EventHandler RequestConfirmationEmailSucceeded;
        public event EventHandler ResetPasswordFailed;
        public event EventHandler ResetPasswordSucceeded;
        public event EventHandler SignOutFailed;
        public event EventHandler SignOutStarted;
        public event EventHandler SignOutSucceeded;
        public event EventHandler SignUpFailed;
        public event EventHandler SignUpSucceeded;

        protected void WireUserManagementEvents()
        {
            UserManager.ConfirmAccountFailed += (o, a) => FireEvent(ConfirmAccountFailed, a);
            UserManager.ConfirmAccountSucceeded += (o, a) => FireEvent(ConfirmAccountSucceeded, a);
            UserManager.ForgotPasswordFailed += (o, a) => FireEvent(ForgotPasswordFailed, a);
            UserManager.ForgotPasswordSucceeded += (o, a) => FireEvent(ForgotPasswordSucceeded, a);
            UserManager.LoginFailed += (o, a) => FireEvent(LoginFailed, a);
            UserManager.LoginSucceeded += (o, a) => FireEvent(LoginSucceeded, a);
            UserManager.RequestConfirmationEmailFailed += (o, a) => FireEvent(RequestConfirmationEmailFailed, a);
            UserManager.RequestConfirmationEmailSucceeded += (o, a) => FireEvent(RequestConfirmationEmailSucceeded, a);
            UserManager.ResetPasswordFailed += (o, a) => FireEvent(ResetPasswordFailed, a);
            UserManager.ResetPasswordSucceeded += (o, a) => FireEvent(ResetPasswordSucceeded, a);
            UserManager.SignOutFailed += (o, a) => FireEvent(SignOutFailed, a);
            UserManager.SignOutStarted += (o, a) => FireEvent(SignOutStarted, a);
            UserManager.SignOutSucceeded += (o, a) => FireEvent(SignOutSucceeded, a);
            UserManager.SignUpFailed += (o, a) => FireEvent(SignUpFailed, a);
            UserManager.SignUpSucceeded += (o, a) => FireEvent(SignUpSucceeded, a);
        }

        public virtual ConfirmResponse ConfirmAccount(string token)
        {
            return UserManager.ConfirmAccount(token);
        }

        [Local]
        public virtual Email CreateEmail(string fromAddress = null, string fromDisplayName = null)
        {
            return UserManager.CreateEmail(fromAddress, fromDisplayName);
        }

        public virtual ForgotPasswordResponse ForgotPassword(string emailAddress)
        {
            return UserManager.ForgotPassword(emailAddress);
        }

        [Exclude]
        public User GetUser(IHttpContext context)
        {
            return UserManager.GetUser(context);
        }

        public virtual CheckEmailResponse IsEmailInUse(string emailAddress)
        {
            return UserManager.IsEmailInUse(emailAddress);
        }

        public virtual CheckUserNameResponse IsUserNameAvailable(string userName)
        {
            return UserManager.IsUserNameAvailable(userName);
        }

        public virtual SendEmailResponse RequestConfirmationEmail(string emailAddress, int accountIndex = 0)
        {
            return UserManager.RequestConfirmationEmail(emailAddress, accountIndex);
        }

        public virtual PasswordResetResponse ResetPassword(string passHash, string resetToken)
        {
            return UserManager.ResetPassword(passHash, resetToken);
        }

        public virtual SignOutResponse SignOut()
        {
            return UserManager.SignOut();
        }

        public virtual SignUpResponse SignUp(string emailAddress, string userName, string passHash, bool sendConfirmationEmail)
        {
            return UserManager.SignUp(emailAddress, userName, passHash, sendConfirmationEmail);
        }

        public virtual string GetCurrentUser()
        {
            return UserResolver.GetCurrentUser();
        }

        string IUserResolver.GetUser(IHttpContext context)
        {
            return UserResolver.GetUser(context);
        }
        
        public virtual bool IsInRole(string roleName)
        {
            return RoleResolver.IsInRole(UserResolver, roleName);
        }

        public virtual string[] GetRoles()
        {
            return GetRoles(UserResolver);
        }

        [Local]
        public bool IsInRole(IUserResolver userResolver, string roleName)
        {
            return RoleResolver.IsInRole(userResolver, roleName);
        }

        [Local]
        public string[] GetRoles(IUserResolver userResolver)
        {
            return RoleResolver.GetRoles(userResolver);
        }

        [Local]
        public Vault GetSmtpSettingsVault(string applicationName = null)
        {
            return UserManager.GetSmtpSettingsVault(applicationName);
        }
    }
}
