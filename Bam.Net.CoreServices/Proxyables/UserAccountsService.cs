using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.UserAccounts;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.CoreServices.Proxyables
{
    [Encrypt]
    [Proxy("userAccounts")]
    [ApiKeyRequired]
    public class UserAccountsService : IUserManager
    {
        public IApplicationNameProvider ApplicationNameProvider
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Func<string, string> GetConfirmationUrlFunction
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Func<string, string> GetPasswordResetUrlFunction
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int PasswordResetTokensExpireInThisManyMinutes
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SmtpSettingsVaultPath
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
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

        public ConfirmResponse ConfirmAccount(string token)
        {
            throw new NotImplementedException();
        }

        public Email CreateEmail(string fromAddress = null, string fromDisplayName = null)
        {
            throw new NotImplementedException();
        }

        public ForgotPasswordResponse ForgotPassword(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public User GetUser(IHttpContext context)
        {
            throw new NotImplementedException();
        }

        public CheckEmailResponse IsEmailInUse(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public CheckUserNameResponse IsUserNameAvailable(string userName)
        {
            throw new NotImplementedException();
        }

        public LoginResponse Login(string userName, string passHash)
        {
            throw new NotImplementedException();
        }

        public SendEmailResponse RequestConfirmationEmail(string emailAddress, int accountIndex = 0)
        {
            throw new NotImplementedException();
        }

        public PasswordResetResponse ResetPassword(string passHash, string resetToken)
        {
            throw new NotImplementedException();
        }

        public SignOutResponse SignOut()
        {
            throw new NotImplementedException();
        }

        public SignUpResponse SignUp(string emailAddress, string userName, string passHash, bool sendConfirmationEmail)
        {
            throw new NotImplementedException();
        }
    }
}
