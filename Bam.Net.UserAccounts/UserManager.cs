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
            this.SmtpSettingsVaultPath = ".\\SmtpSettings.vault.sqlite";
            this.PasswordResetTokensExpireInThisManyMinutes = 15;
        }

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
                string content = @"
Hi {UserName},<br /><br />

Thanks for signing up for {ApplicationName}.  Click the link below to confirm your account.<br /><br />

<a href=""{ConfirmationUrl}"">{ConfirmationUrl}</a><br /><br />

If you are unable to click on the link, copy and paste the above link into a browser address bar.<br /><br />

If you did not sign up for {ApplicationName} you may ignore this email.<br /><br />

Thanks,<br />
The {ApplicationName} Team<br />
";
                EmailComposer.SetEmailTemplate(AccountConfirmationEmailName, content);
            }
        }

        protected internal virtual void InitializePasswordResetEmail()
        {
            if (!EmailComposer.TemplateExists(PasswordResetEmailName))
            {
                string content = @"
Hi {UserName},<br /><br />

Someone recently requested a password reset for {ApplicationName}.  If this was you click the link below to reset your password.<br /><br />

<a href=""{PasswordResetUrl}"">{PasswordResetUrl}</a><br /><br />

If you are unable to click on the link, copy and paste the above link into a browser address bar.<br /><br />

If you did not request a password reset for {ApplicationName} you can disregard this email.<br /><br />

Thanks,<br />
The {ApplicationName} Team<br />
";
                EmailComposer.SetEmailTemplate(PasswordResetEmailName, content);
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
        
        public ForgotPasswordResponse ForgotPassword(string emailAddress)
        {
            try
            {
                User user = User.GetByEmail(emailAddress);
                PasswordReset reset = user.PasswordResetsByUserId.AddNew();
                reset.Token = Guid.NewGuid().ToString();
                reset.DateTime = new Instant();
                reset.ExpiresInMinutes = PasswordResetTokensExpireInThisManyMinutes;
                reset.WasReset = false;				

                user.Save();

                PasswordResetEmailData data = new PasswordResetEmailData
                {
                    UserName = user.UserName,
                    ApplicationName = ApplicationNameProvider.GetApplicationName(),
                    PasswordResetUrl = GetPasswordResetUrl(reset.Token)
                };
                string subject = "Password Reset for {0}"._Format(data.ApplicationName);
                string email = user.Email;
                ComposePasswordResetEmail(subject, data).To(email).Send();

                return GetSuccess<ForgotPasswordResponse>(reset.Token, "Password email was sent to {0}"._Format(email));
            }
            catch (Exception ex)
            {
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

        public LoginResponse Login(string userName, string passHash)
        {
            try
            {

                User user = null;
                if (userName.Contains("@"))
                {
                    user = User.GetByEmail(userName);
                }
                else
                {
                    user = User.GetByUserName(userName);
                }

                if (user != null)
                {
                    bool passwordIsValid = Password.Validate(user, passHash);

                    LoginResponse result = GetSuccess<LoginResponse>(passwordIsValid);
                    if (!passwordIsValid)
                    {
                        result.Message = "User name and password combination was invalid";
                        result.Success = false;
                    }
                    else
                    {
                        DaoUserResolver.SetUser(HttpContext, user, true);
                        user.AddLoginRecord();
                    }

                    return result;
                }
                else
                {
                    return GetFailure<LoginResponse>(new Exception("User name and password combination was invalid"));
                }                
            }
            catch (Exception ex)
            {
                return GetFailure<LoginResponse>(ex);
            }
        }

        public SignUpResponse SignUp(string emailAddress, string userName, string passHash, bool sendConfirmationEmail)
        {
            try
            {
                IApplicationNameProvider appNameResolver = ApplicationNameProvider;
                User user = User.Create(userName, emailAddress, passHash, appNameResolver, true, true, false);
                if (sendConfirmationEmail)
                {
                    RequestConfirmationEmail(emailAddress);
                }
                return GetSuccess<SignUpResponse>(user.ToJsonSafe());
            }
            catch (Exception ex)
            {
                return GetFailure<SignUpResponse>(ex);
            }
        }

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

                Session.End();

                return GetSuccess<SignOutResponse>("Sign out successful");
            }
            catch (Exception ex)
            {
                return GetFailure<SignOutResponse>(ex);
            }
        }

        public SendEmailResponse RequestConfirmationEmail(string emailAddress, int accountIndex = 0)
        {
            try
            {
                User user = User.GetByEmail(emailAddress);
                if (user == null)
                {
                    throw new UserNameNotFoundException(emailAddress);
                }

                Account account = null;
                if (user.AccountsByUserId.Count == 0)
                {
                    account = Account.Create(user, ApplicationNameProvider.GetApplicationName(), user.UserName, false);
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
                    UserName = user.UserName,
                    ApplicationName = ApplicationNameProvider.GetApplicationName(),
                    ConfirmationUrl = GetConfirmationUrl(account.Token)
                };

                string subject = "Account Registration Confirmation";
                ComposeConfirmationEmail(subject, data).To(user.Email).Send();

                return GetSuccess<SendEmailResponse>(true);
            }
            catch (Exception ex)
            {
                return GetFailure<SendEmailResponse>(ex);
            }
        }

        public ConfirmResponse ConfirmAccount(string token)
        {
            try
            {
                Account account = Account.OneWhere(c => c.Token == token);
                if (account == null)
                {
                    throw new ArgumentException("Invalid token");
                }
                else
                {
                    account.IsConfirmed = true;
                    account.Save();
                }

                return GetSuccess<ConfirmResponse>(account.Provider);
            }
            catch (Exception ex)
            {
                return GetFailure<ConfirmResponse>(ex);
            }
        }

        public CheckUserNameResponse IsUserNameAvailable(string userName)
        {
            try
            {
                bool? isAvailable = !User.Exists(userName);
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
                User user = User.GetByEmail(emailAddress);
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

        public PasswordResetResponse ResetPassword(string passHash, string resetToken)
        {
            try
            {
                PasswordReset reset = PasswordReset.OneWhere(c => c.Token == resetToken);
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

                Password.Set(reset.UserOfUserId, passHash);

                return GetSuccess<PasswordResetResponse>(true, "Password was successfully reset");
            }
            catch (Exception ex)
            {
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
        public User GetCurrentUser()
        {
            return Session.UserOfUserId;
        }

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
