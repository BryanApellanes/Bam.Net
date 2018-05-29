using System;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts.Data;
using Bam.Net.Messaging;

namespace Bam.Net.UserAccounts
{
    public interface IUserManager: ICloneable, IRequiresHttpContext, ISmtpSettingsProvider
    {
        IApplicationNameProvider ApplicationNameProvider { get; set; }
        Func<string, string> GetConfirmationUrlFunction { get; set; }
        Func<string, string> GetPasswordResetUrlFunction { get; set; }
        int PasswordResetTokensExpireInThisManyMinutes { get; set; }

        event EventHandler ConfirmAccountFailed;
        event EventHandler ConfirmAccountSucceeded;
        event EventHandler ForgotPasswordFailed;
        event EventHandler ForgotPasswordSucceeded;
        event EventHandler LoginFailed;
        event EventHandler LoginSucceeded;
        event EventHandler RequestConfirmationEmailFailed;
        event EventHandler RequestConfirmationEmailSucceeded;
        event EventHandler ResetPasswordFailed;
        event EventHandler ResetPasswordSucceeded;
        event EventHandler SignOutFailed;
        event EventHandler SignOutStarted;
        event EventHandler SignOutSucceeded;
        event EventHandler SignUpFailed;
        event EventHandler SignUpSucceeded;

        ConfirmResponse ConfirmAccount(string token);
        ForgotPasswordResponse ForgotPassword(string emailAddress);
        User GetUser(IHttpContext context);
        CheckEmailResponse IsEmailInUse(string emailAddress);
        CheckUserNameResponse IsUserNameAvailable(string userName);
        LoginResponse Login(string userName, string passHash);
        SendEmailResponse RequestConfirmationEmail(string emailAddress, int accountIndex = 0);
        PasswordResetResponse ResetPassword(string passHash, string resetToken);
        SignOutResponse SignOut();
        SignUpResponse SignUp(string emailAddress, string userName, string passwordHash, bool sendConfirmationEmail);
    }
}