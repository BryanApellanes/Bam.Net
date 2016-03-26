/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WebMatrix.WebData;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.UserAccounts.Data;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.ServiceProxy;
using Bam.Net.Messaging;
using Bam.Net.Incubation;
using Bam.Net.Encryption;
using System.IO;
using System.Net;
using System.Web;

namespace Bam.Net.UserAccounts
{
    /// <summary>
    /// Class used to manage users of an application
    /// </summary>
    [Encrypt]
    [Proxy("user", MethodCase = MethodCase.Both)]
    public class UserManager: Loggable, IRequiresHttpContext
    {
        static UserManager()
        {
            UserResolvers.Default.InsertResolver(0, new DaoUserResolver());
        }

        /// <summary>
        /// The name of the account confirmation email template
        /// </summary>
        public const string AccountConfirmationEmailName = "AccountConfirmationEmail";
        /// <summary>
        /// The name of the password reset email template
        /// </summary>
        public const string PasswordResetEmailName = "PasswordResetEmail";

        public UserManager()
        {
            SmtpSettingsVaultPath = ".\\SmtpSettings.vault.sqlite";
            PasswordResetTokensExpireInThisManyMinutes = 15;
            LastException = new NullException();
        }

        [Exclude]
        public UserManager Clone()
        {
            UserManager result = new UserManager();
            result.CopyProperties(this);
            result._serviceProvider = _serviceProvider.Clone();
            result.SmtpSettingsVault = SmtpSettingsVault;
            return result;
        }

        Incubator _serviceProvider;
        object _serviceProviderLock = new object();
        protected internal Incubator ServiceProvider
        {
            get
            {
                return _serviceProviderLock.DoubleCheckLock(ref _serviceProvider, () =>
                {
                    Incubator serviceProvider = new Incubator();
                    DaoUserResolver userResolver = new DaoUserResolver();
                    serviceProvider.Set<IUserResolver>(userResolver);
                    serviceProvider.Set<DaoUserResolver>(userResolver);
                    DaoRoleResolver roleResolver = new DaoRoleResolver();
                    serviceProvider.Set<IRoleResolver>(roleResolver);
                    serviceProvider.Set<DaoRoleResolver>(roleResolver);
                    serviceProvider.Set<EmailComposer>(new NamedFormatEmailComposer());
                    serviceProvider.Set<IApplicationNameProvider>(DefaultConfigurationApplicationNameProvider.Instance);

                    return serviceProvider;
                });
            }
        }

        protected internal DaoUserResolver DaoUserResolver
        {
            get
            {
                return ServiceProvider.Get<DaoUserResolver>();
            }
            set
            {
                ServiceProvider.Set<DaoUserResolver>(value);
            }
        }

        protected internal EmailComposer EmailComposer
        {
            get
            {
                return ServiceProvider.Get<EmailComposer>();
            }
            set
            {
                ServiceProvider.Set<EmailComposer>(value);
            }
        }

        protected internal Vault SmtpSettingsVault
        {
            get;
            set;
        }

        string _smtpSettingsVaultPath;
        [Exclude]
        public string SmtpSettingsVaultPath
        {
            get
            {
                return _smtpSettingsVaultPath;
            }
            set
            {
                _smtpSettingsVaultPath = value;
                FileInfo file = new FileInfo(_smtpSettingsVaultPath);
                SmtpSettingsVault = Vault.Load(file, "SmtpSettings");
            }
        }

        public int PasswordResetTokensExpireInThisManyMinutes
        {
            get;
            set;
        }

        [Exclude]
        public string ApplicationName // used by loggable implementation
        {
            get
            {
                return ApplicationNameProvider.GetApplicationName();
            }
        }

        [Exclude]
        public string LastExceptionMessage // used by loggable implementation
        {
            get
            {
                return LastException.Message;
            }
        }

        [Exclude]
        public Exception LastException { get; set; }

        protected internal IApplicationNameProvider ApplicationNameProvider
        {
            get
            {
                return ServiceProvider.Get<IApplicationNameProvider>();
            }
            set
            {
                ServiceProvider.Set<IApplicationNameProvider>(value);
            }
        }

        protected internal Email ComposeConfirmationEmail(string subject, object data)
        {
            Email email = EmailComposer.Compose(subject, AccountConfirmationEmailName, data);
            return EmailComposer.SetEmailSettings(SmtpSettingsVault, email);
        }

        protected internal Email ComposePasswordResetEmail(string subject, object data)
        {
            Email email = EmailComposer.Compose(subject, PasswordResetEmailName, data);
            return EmailComposer.SetEmailSettings(SmtpSettingsVault, email);
        }

        protected internal virtual void InitializeConfirmationEmail()
        {
            if (!EmailComposer.TemplateExists(AccountConfirmationEmailName))
            {
                string content = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns = ""http://www.w3.org/1999/xhtml"">  
   <head>
    <meta http - equiv = ""Content-Type"" content = ""text/html; charset=UTF-8"" />       
    <title>{Title}</title>
    <meta name = ""viewport"" content = ""width=device-width, initial-scale=1.0"" />
    </head>
    <body style=""margin: 0; padding: 0;"">
     <table border=""0"" cellpadding=""15"" cellspacing=""0"" width=""100%"">
      <tr>
       <td>
        Hi {UserName},
       </td>
      </tr>
      <tr>
       <td>
        Thanks for signing up for {ApplicationName}.  Please click the link below to confirm your account.
       </td>
      </tr>
      <tr>
       <td>
        <a href=""{ConfirmationUrl}"">{ConfirmationUrl}</a>
       </td>
      </tr>
      <tr>
       <td>
        If you are unable to click on the link, copy and paste the above link into a browser address bar.
       </td>
      </tr>
      <tr>
       <td>
        If you did not sign up for {ApplicationName} you may ignore this email.
       </td>
      </tr>
      <tr>
       <td>
        Thanks,<br>
        The {ApplicationName} team
       </td>
      </tr>
     </table>
    </body>
</html>";
                EmailComposer.SetEmailTemplate(AccountConfirmationEmailName, content, true);
            }
        }

        protected internal virtual void InitializePasswordResetEmail()
        {
            if (!EmailComposer.TemplateExists(PasswordResetEmailName))
            {
                string content = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns = ""http://www.w3.org/1999/xhtml"">  
   <head>
    <meta http - equiv = ""Content-Type"" content = ""text/html; charset=UTF-8"" />       
    <title>{Title}</title>
    <meta name = ""viewport"" content = ""width=device-width, initial-scale=1.0"" />
    </head>
    <body style=""margin: 0; padding: 0;"">
     <table border=""0"" cellpadding=""15"" cellspacing=""0"" width=""100%"">
      <tr>
       <td>
        Hi {UserName},
       </td>
      </tr>
      <tr>
       <td>
        Someone recently requested a password reset for {ApplicationName}.  If this was you click the link below to reset your password.
       </td>
      </tr>
      <tr>
       <td>
        <a href=""{PasswordResetUrl}"">{PasswordResetUrl}</a>
       </td>
      </tr>
      <tr>
       <td>
        If you are unable to click on the link, copy and paste the above link into a browser address bar.
       </td>
      </tr>
      <tr>
       <td>
        If you did not request a password reset for {ApplicationName} you can disregard this email.
       </td>
      </tr>
      <tr>
       <td>
        Thanks,<br>
        The {ApplicationName} team
       </td>
      </tr>
     </table>
    </body>
</html>";
                EmailComposer.SetEmailTemplate(PasswordResetEmailName, content, true);
            }
        }

        Func<string, string> _getConfirmationUrlFunction;
        object _getConfirmationUrlFunctionLock = new object();
        public Func<string,string> GetConfirmationUrlFunction
        {
            get
            {
                IHttpContext context = HttpContext;
                Func<string, string> func = (token) =>
                {
                    string baseAddress = ServiceProxySystem.GetBaseAddress(context.Request.Url);
                    return string.Format("{0}auth/confirmAccount?token={1}&layout=basic", baseAddress, token);
                };

                return func;
            }
            set
            {
                _getConfirmationUrlFunction = value;
            }
        }

        protected internal string GetConfirmationUrl(string token)
        {
            return GetConfirmationUrlFunction(token);
        }

        Func<string, string> _getPasswordResetUrlFunction;
        object _getPasswordResetUrlFunctionLock = new object();
        public Func<string, string> GetPasswordResetUrlFunction
        {
            get
            {
                IHttpContext context = HttpContext;
                Func<string, string> func = (token) =>
                {
                    string baseAddress = ServiceProxySystem.GetBaseAddress(context.Request.Url);
                    return string.Format("{0}auth/resetPassword?token={1}&layout=basic", baseAddress, token);
                };

                return func;
            }
            set
            {
                _getPasswordResetUrlFunction = value;
            }
        }

        protected internal string GetPasswordResetUrl(string token)
        {
            return GetPasswordResetUrlFunction(token);
        }

        Database _database;
        public Database Database
        {
            get
            {
                if(_database == null)
                {
                    _database = Db.For<User>();
                }
                return _database;
            }
            set
            {
                _database = value;
                DaoUserResolver.UserDatabase = _database;
                User.UserDatabase = _database;
            }
        }

        /// <summary>
        /// The vent that is fired when someone logs in
        /// </summary>
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: ForgotPasswordSucceeded")]
        public event EventHandler ForgotPasswordSucceeded;
        /// <summary>
        /// The event that is fired when a login fails
        /// </summary>
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: ForgotPasswordSucceeded: {LastExceptionMessage}")]
        public event EventHandler ForgotPasswordFailed;
        public ForgotPasswordResponse ForgotPassword(string emailAddress)
        {
            try
            {
                User user = User.GetByEmail(emailAddress, Database);
                PasswordReset reset = user.PasswordResetsByUserId.AddNew();
                reset.Token = Guid.NewGuid().ToString();
                reset.DateTime = new Instant();
                reset.ExpiresInMinutes = PasswordResetTokensExpireInThisManyMinutes;
                reset.WasReset = false;				

                user.Save(Database);

                PasswordResetEmailData data = new PasswordResetEmailData
                {
                    Title = "Password Reset",
                    UserName = user.UserName,
                    ApplicationName = ApplicationNameProvider.GetApplicationName(),
                    PasswordResetUrl = GetPasswordResetUrl(reset.Token)
                };
                string subject = "Password Reset for {0}"._Format(data.ApplicationName);
                string email = user.Email;
                ComposePasswordResetEmail(subject, data).To(email).Send();
                FireEvent(ForgotPasswordSucceeded);
                return GetSuccess<ForgotPasswordResponse>(reset.Token, "Password email was sent to {0}"._Format(email));
            }
            catch (Exception ex)
            {
                LastException = ex;
                FireEvent(ForgotPasswordFailed);
                return GetFailure<ForgotPasswordResponse>(ex);
            }
        }
        public const string FacebookProvider = "facebook";

        public dynamic LoginFbUser(string fbId, string userName)
        {
            //User user = User.Ensure(userName);


            //return new { userName = userName, isAuthenticated = true };
            throw new NotImplementedException();
        }
        /// <summary>
        /// The vent that is fired when someone logs in
        /// </summary>
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: LoginSucceeded")]
        public event EventHandler LoginSucceeded;
        /// <summary>
        /// The event that is fired when a login fails
        /// </summary>
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: LoginFailed: {LastExceptionMessage}")]
        public event EventHandler LoginFailed;
        public LoginResponse Login(string userName, string passHash)
        {
            string failureMessage = "User name and password combination was invalid";
            EventHandler eventToFire = LoginSucceeded;
            LoginResponse result = GetFailure<LoginResponse>(new Exception("Unknown exception occurred"));
            try
            {
                User user = null;
                if (userName.Contains("@"))
                {
                    user = User.GetByEmail(userName, Database);
                }
                else
                {
                    user = User.GetByUserName(userName, Database);
                }

                if (user != null)
                {
                    bool passwordIsValid = Password.Validate(user, passHash, Database);

                    result = GetSuccess<LoginResponse>(passwordIsValid);
                    if (!passwordIsValid)
                    {
                        result.Message = failureMessage;
                        result.Success = false;
                        eventToFire = LoginFailed;
                    }
                    else
                    {
                        DaoUserResolver.SetUser(HttpContext, user, true, Database);
                        user.AddLoginRecord(Database);
                    }
                }
                else
                {
                    eventToFire = LoginFailed;
                    result = GetFailure<LoginResponse>(new Exception(failureMessage));
                }                
            }
            catch (Exception ex)
            {
                eventToFire = LoginFailed;
                result = GetFailure<LoginResponse>(ex);
            }

            FireEvent(eventToFire, EventArgs.Empty);
            return result;
        }

        /// <summary>
        /// The vent that is fired when someone signs up
        /// </summary>
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: SignUpSucceeded")]
        public event EventHandler SignUpSucceeded;
        /// <summary>
        /// The event that is fired when a sign up fails
        /// </summary>
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: SignUpFailed: {LastExceptionMessage}")]
        public event EventHandler SignUpFailed;
        public SignUpResponse SignUp(string emailAddress, string userName, string passHash, bool sendConfirmationEmail)
        {
            try
            {
                IApplicationNameProvider appNameResolver = ApplicationNameProvider;
                User user = User.Create(userName, emailAddress, passHash, appNameResolver, true, true, false, Database);
                if (sendConfirmationEmail)
                {
                    RequestConfirmationEmail(emailAddress);
                }
                FireEvent(SignUpSucceeded, EventArgs.Empty);
                return GetSuccess<SignUpResponse>(user.ToJsonSafe());
            }
            catch (Exception ex)
            {
                LastException = ex;
                FireEvent(SignUpFailed, EventArgs.Empty);
                return GetFailure<SignUpResponse>(ex);
            }
        }

        /// <summary>
        /// The vent that is fired when someone signs up
        /// </summary>
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: SignOutSucceeded")]
        public event EventHandler SignOutSucceeded;
        /// <summary>
        /// The event that is fired when a sign up fails
        /// </summary>
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: SignOutFailed: {LastExceptionMessage}")]
        public event EventHandler SignOutFailed;
        public SignOutResponse SignOut()
        {
            try
            {
                IRequest request = HttpContext.Request;
                
                if (HttpContext.Request.Cookies[Session.CookieName] != null)
                {
                    IResponse response = HttpContext.Response;
                    Cookie cookie = request.Cookies[Session.CookieName];                    
                    cookie.Expires = DateTime.Now.AddDays(-1d);

                    response.Cookies.Add(cookie);
                }

                Session.End(Database);
                FireEvent(SignOutSucceeded);
                return GetSuccess<SignOutResponse>("Sign out successful");
            }
            catch (Exception ex)
            {
                LastException = ex;
                FireEvent(SignOutFailed);
                return GetFailure<SignOutResponse>(ex);
            }
        }
        
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: RequestConfirmationEmailSucceeded")]
        public event EventHandler RequestConfirmationEmailSucceeded;
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: RequestConfirmationEmailFailed: {LastExceptionMessage}")]
        public event EventHandler RequestConfirmationEmailFailed;

        public SendEmailResponse RequestConfirmationEmail(string emailAddress, int accountIndex = 0)
        {
            try
            {
                User user = User.GetByEmail(emailAddress, Database);
                if (user == null)
                {
                    throw new UserNameNotFoundException(emailAddress);
                }

                Account account = null;
                if (user.AccountsByUserId.Count == 0)
                {
                    account = Account.Create(user, ApplicationNameProvider.GetApplicationName(), user.UserName, false, Database);
                }
                else if(user.AccountsByUserId.Count <= accountIndex)
                {
                    account = user.AccountsByUserId[accountIndex];
                }
                else
                {
                    account = user.AccountsByUserId[0];
                }

                AccountConfirmationEmailData data = new AccountConfirmationEmailData
                {
                    Title = "Account Confirmation",
                    UserName = user.UserName,
                    ApplicationName = ApplicationNameProvider.GetApplicationName(),
                    ConfirmationUrl = GetConfirmationUrl(account.Token)
                };

                string subject = "Account Registration Confirmation";
                ComposeConfirmationEmail(subject, data).To(user.Email).Send();

                FireEvent(RequestConfirmationEmailSucceeded);
                return GetSuccess<SendEmailResponse>(true);
            }
            catch (Exception ex)
            {
                LastException = ex;
                FireEvent(RequestConfirmationEmailFailed);
                return GetFailure<SendEmailResponse>(ex);
            }
        }

        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: ConfirmAccountSucceeded")]
        public event EventHandler ConfirmAccountSucceeded;
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: ConfirmAccountFailed: {LastExceptionMessage}")]
        public event EventHandler ConfirmAccountFailed;

        public ConfirmResponse ConfirmAccount(string token)
        {
            try
            {
                Account account = Account.OneWhere(c => c.Token == token, Database);
                if (account == null)
                {
                    throw new ArgumentException("Invalid token");
                }
                else
                {
                    account.IsConfirmed = true;
                    account.Save(Database);
                }

                FireEvent(ConfirmAccountSucceeded);
                return GetSuccess<ConfirmResponse>(account.Provider);
            }
            catch (Exception ex)
            {
                LastException = ex;
                FireEvent(ConfirmAccountFailed);
                return GetFailure<ConfirmResponse>(ex);
            }
        }

        public CheckUserNameResponse IsUserNameAvailable(string userName)
        {
            try
            {
                bool? isAvailable = !User.Exists(userName, Database);
                return GetSuccess<CheckUserNameResponse>(isAvailable);
            }
            catch (Exception ex)
            {
                return GetFailure<CheckUserNameResponse>(ex);
            }
        }

        public CheckEmailResponse IsEmailInUse(string emailAddress)
        {
            try
            {
                User user = User.GetByEmail(emailAddress, Database);
                bool? emailIsInUse = user != null;
                return GetSuccess<CheckEmailResponse>(emailIsInUse);
            }
            catch (Exception ex)
            {
                return GetFailure<CheckEmailResponse>(ex);
            }
        }

        private T GetSuccess<T>(object data, string message = null) where T: RequestResponse, new()
        {
            T result = new T();
            result.Success = true;
            result.Message = message;
            result.Data = data;
            return result;
        }

        private T GetFailure<T>(Exception ex) where T: RequestResponse, new()
        {
            T result = new T();
            result.Success = false;
            result.Message = ex.Message;
            result.Data = null;
            return result;
        }

        public PasswordResetPageResponse PasswordResetPage(string token, string layout)
        {
            PasswordResetPageResponse response = GetSuccess<PasswordResetPageResponse>(token);
            response.Token = token;
            response.Layout = layout;
            return response;
        }

        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: RequestConfirmationEmailSucceeded")]
        public event EventHandler ResetPasswordSucceeded;
        [Verbosity(VerbosityLevel.Information, MessageFormat = "{ApplicationName}::{UserName}:: RequestConfirmationEmailFailed: {LastExceptionMessage}")]
        public event EventHandler ResetPasswordFailed;

        public PasswordResetResponse ResetPassword(string passHash, string resetToken)
        {
            try
            {
                PasswordReset reset = PasswordReset.OneWhere(c => c.Token == resetToken, Database);
                if (reset == null)
                {
                    throw new InvalidTokenException();
                }

                Instant timeOfRequest = new Instant(reset.DateTime.Value);
                Instant now = new Instant();
                if (now.DiffInMinutes(timeOfRequest) > reset.ExpiresInMinutes.Value)
                {
                    throw new InvalidTokenException();
                }

                Password.Set(reset.UserOfUserId, passHash, Database);
                FireEvent(ResetPasswordSucceeded);
                return GetSuccess<PasswordResetResponse>(true, "Password was successfully reset");
            }
            catch (Exception ex)
            {
                LastException = ex;
                FireEvent(ResetPasswordFailed);
                return GetFailure<PasswordResetResponse>(ex);
            }
        }

        public dynamic GetCurrent()
        {
            bool isAuthenticated = false;
            User user = GetCurrentUser();

            if (user.Id.Value != User.Anonymous.Id.Value)
            {
                isAuthenticated = true;
            }

            int loginCount = isAuthenticated ? user.LoginsByUserId.Count : 0;

            dynamic result = new
            {
                userName = user.UserName,
                id = user.Id,
                isAuthenticated = isAuthenticated,
                loginCount = loginCount
            };

            return result;
        }

        [Exclude]
        public string UserName
        {
            get
            {
                return GetCurrentUser().UserName;
            }
        }

        [Exclude]
        public User GetCurrentUser()
        {
            if(HttpContext == null)
            {
                return User.Anonymous;
            }
            return Session.UserOfUserId;
        }

        [Exclude]
        public Session Session
        {
            get
            {
                return Session.Get(HttpContext);
            }
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }
    }
}
