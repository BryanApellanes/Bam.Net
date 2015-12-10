/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Encryption;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.UserAccounts.Data;
using Bam.Net.UserAccounts;
using Bam.Net.Data;
using Bam.Net.Configuration;
using System.Reflection;
using FakeItEasy;
using Bam.Net.Web;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.Messaging;
using Bam.Net.ServiceProxy.Secure;
using System.Threading;

namespace Bam.Net.UserAccounts.Tests
{
    public partial class UserAccountTestsProgram
    {
        static UserAccountTestsProgram()
        {
            SQLiteRegistrar.Register<User>();
            //SqlClientRegistrar.Register<User>();
            Db.TryEnsureSchema<User>();

			ClearAllUserInfo();			
        }

		private static void ClearAllUserInfo()
		{
			DeleteAllAccounts();
			DeleteAllPasswords();
			DeleteAllLogins();
			DeleteAllSessions();
			DeleteAllUsers();
		}

        private static void DeleteAllUsers()
        {
            UserCollection users = User.LoadAll();
            users.Delete();
        }

        private static void DeleteAllSessions()
        {
            SessionCollection sessions = Session.LoadAll();
            sessions.Delete();
        }

        private static void DeleteAllLogins()
        {
            LoginCollection logins = Login.LoadAll();
            logins.Delete();
        }

        private static void DeleteAllPasswords()
        {
            PasswordCollection passwords = Password.LoadAll();
            passwords.Delete();
        }

        private static void DeleteAllAccounts()
        {
            AccountCollection accounts = Account.LoadAll();
            accounts.Delete();            
        }

        [UnitTest]
        public void UserNameIsAvailableShouldBeFalseIfUserAlreadyExists()
        {
            UserManager userProxy = new UserManager();
            string userName = "InUse";
            CheckUserNameResponse result = userProxy.IsUserNameAvailable(userName);
            Expect.IsTrue((bool)result.Data);

            User user = User.Create(userName);

            result = userProxy.IsUserNameAvailable(userName);
            Expect.IsFalse((bool)result.Data);
        }

        [UnitTest]
        public void EmailIsInUseShouldBeTrueIfAlreadyExists()
        {
            UserManager userProxy = new UserManager();
            string emailAddress = "email@domain.cxm";
            User user = User.Create("user", emailAddress, "blah");

            CheckEmailResponse result = userProxy.IsEmailInUse(emailAddress);

            Expect.IsTrue((bool)result.Data);
        }

        [UnitTest]
        public void SignUpShouldSucceed()
        {
            UserManager user = new UserManager();
			user.HttpContext = FakeItEasy.A.Fake<IHttpContext>();

            SignUpResponse result = user.SignUp("Test@stickerize.me", "Test", "Password".Sha1(), false);
            Expect.IsNotNull(result.Success, "No Success value specified");
            Expect.IsTrue(result.Success, "signup failed");
            OutLine(result.Message);
        }

        [UnitTest]
        public void SignUpShouldFailForExistingUserName()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            User user = User.Create(userName);

            UserManager userProxy = new UserManager();
            SignUpResponse result = userProxy.SignUp("Test@stickerize.me", userName, "password".Sha1(), false);

            Expect.IsFalse(result.Success, result.Message);
        }

        [UnitTest]
        public void SignUpShouldFailForExistingEmail()
        {
            UserManager user = new UserManager();
            string emailAddress = "email@{0}.cxm"._Format(MethodBase.GetCurrentMethod().Name);
            string userName = "User_".RandomLetters(6);
            User created = User.Create(userName, emailAddress, "password".Sha1());
            
            SignUpResponse result = user.SignUp(emailAddress, userName, "password".Sha1(), false);

            Expect.IsFalse(result.Success);
            OutLine(result.Message);
        }

        [UnitTest]
        public void SignUpShouldCreateUserEntry()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy = new UserManager();
            userProxy.SignUp("test_{0}@domain.cxm"._Format(8.RandomLetters()), userName, "password".Sha1(), false);

            User check = User.OneWhere(c => c.UserName == userName);
            Expect.IsNotNull(check);
        }

        [UnitTest]
        public void SignUpShouldCreateAccountEntry()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy = new UserManager();
            userProxy.SignUp("test_{0}@domain.cxm"._Format(8.RandomLetters()), userName, "password".Sha1(), false);

            User user = User.OneWhere(c => c.UserName == userName);
            Expect.IsNotNull(user);
            Account account = user.AccountsByUserId.FirstOrDefault();

            Expect.IsNotNull(account);
        }

        [UnitTest]
        public void SignUpShouldSetAccountProviderValue()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy = new UserManager();
            userProxy.SignUp("test_{0}@domain.cxm"._Format(8.RandomLetters()), userName, "password".Sha1(), false);

            User user = User.OneWhere(c => c.UserName == userName);
            Expect.IsNotNull(user);
            Account account = user.AccountsByUserId.FirstOrDefault();

            Expect.IsFalse(string.IsNullOrEmpty(account.Provider), "Provider was null or empty");
            OutLineFormat("Provider: {0}", account.Provider);
        }

        [UnitTest]
        public void SignUpAccountShouldNotBeConfirmed()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy = new UserManager();
            userProxy.SignUp("test_{0}@domain.cxm"._Format(8.RandomLetters()), userName, "password".Sha1(), false);

            User user = User.OneWhere(c => c.UserName == userName);
            Expect.IsNotNull(user);
            Account account = user.AccountsByUserId.FirstOrDefault();

            Expect.IsFalse(account.IsConfirmed.Value, "Was confirmed but shouldn't have been");
        }

        [UnitTest]
        public void SignUpShouldCreatePasswordEntry()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy = new UserManager();
            userProxy.SignUp("test_{0}@domain.cxm"._Format(8.RandomLetters()), userName, "password".Sha1(), false);

            User user = User.OneWhere(c => c.UserName == userName);
            Password password = user.PasswordsByUserId.FirstOrDefault();

            Expect.IsNotNull(password);
        }

        [UnitTest]
        public void SignUpShouldSavePasswordHashOfHash()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy = new UserManager();
            string password = "password";
            string passwordHash = password.Sha1();
            string passwordHashHash = passwordHash.Sha1();
            userProxy.SignUp("test_{0}@domain.cxm"._Format(8.RandomLetters()), userName, passwordHash, false);

            User user = User.OneWhere(c => c.UserName == userName);
            Password entry = user.PasswordsByUserId.FirstOrDefault();

            Expect.AreEqual(passwordHashHash, entry.Value);
        }

        [UnitTest]
        public void LoginShouldNotReturnNull()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy = new UserManager();
            string passHash = "password".Sha1();
            userProxy.SignUp("test_{0}@domain.cxm"._Format(8.RandomLetters()), userName, passHash, false);
            LoginResponse result = userProxy.Login(userName, passHash);
            Expect.IsNotNull(result);
        }
        
        [UnitTest]
        public void SessionShouldInitAndGetSameSession()
        {
            IHttpContext context = A.Fake<IHttpContext>();
            context.Request = new TestRequest();

            Session session = Session.Init(context);
            Session session2 = Session.Get(context);
            Session session3 = Session.Get(context);

            Expect.AreEqual(session.Id, session2.Id);
            Expect.AreEqual(session2.Id, session3.Id);
        }

        [UnitTest]
        public void SessionShouldHaveCreatedAndLastActivityOnInit()
        {
            IHttpContext context = A.Fake<IHttpContext>();
            context.Request = new TestRequest();

            Session session = Session.Init(context);
            
            Expect.IsNotNull(session.CreationDate, "CreationDate was null");
            Instant created = new Instant(session.CreationDate.Value);

            Expect.IsNotNull(session.LastActivity, "LastActivity was null");
            Instant lastActivity = new Instant(session.LastActivity.Value);
        }

        [UnitTest]
        public void SessionGetShouldUpdateLastActivity()
        {
            IHttpContext context = A.Fake<IHttpContext>();
            context.Request = new TestRequest();

            Session session = Session.Init(context);

            Instant created = new Instant(session.CreationDate.Value);
            Instant lastActivity = new Instant(session.LastActivity.Value);
            Expect.AreEqual(lastActivity, created, "LastActivity should have been the same as CreationDate");
            Thread.Sleep(30);
            session = Session.Get(context);
            lastActivity = new Instant(session.LastActivity.Value);
            int diff = lastActivity.DiffInMilliseconds(created);
            Expect.IsGreaterThan(diff, 30);
        }

        
        [UnitTest]
        public void ShouldBeEqual()
        {
            Instant first = new Instant();
            Instant second = new Instant(first.ToDateTime());
            Expect.IsTrue(first.Equals(second));
        }

        [UnitTest]
        public void ShouldBeSetDiffInMilliseconds()
        {
            Instant first = new Instant();
            Instant second = new Instant(first.ToDateTime().AddMilliseconds(30));

            int diff = first.DiffInMilliseconds(second);
            Expect.AreEqual(30, diff);

            diff = second.DiffInMilliseconds(first);
            Expect.AreEqual(30, diff);
        }

        [UnitTest]
        public void ShouldBeSetDiffInSeconds()
        {
            Instant first = new Instant();
            Instant second = new Instant(first.ToDateTime().AddSeconds(30));

            int diff = first.DiffInSeconds(second);
            Expect.AreEqual(30, diff);

            diff = second.DiffInSeconds(first);
            Expect.AreEqual(30, diff);
        }

        [UnitTest]
        public void ShouldBeSetDiffInMinutes()
        {
            Instant first = new Instant();
            Instant second = new Instant(first.ToDateTime().AddMinutes(30));

            int diff = first.DiffInMinutes(second);
            Expect.AreEqual(30, diff);

            diff = second.DiffInMinutes(first);
            Expect.AreEqual(30, diff);
        }

        [UnitTest]
        public void LoginShouldSetUserInUserResolvers()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            IHttpContext context;
            LoginResponse result;
            SignUpAndLogin(userName, out context, out result);

            Expect.IsTrue(result.Success);

            string check = UserResolvers.Default.GetUser(context);
            string check2 = context.User.Identity.Name;

            Expect.AreEqual(userName, check);
            Expect.AreEqual(userName, check2);
        }

        [UnitTest]
        public void SessionShouldBeActiveAfterLogin()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            IHttpContext context;
            LoginResponse result;
            UserManager userMgr;
            SignUpAndLogin(userName, out context, out result, out userMgr);

            Session session = Session.Get(context);
            Expect.IsTrue(session.IsActive.Value, "session wasn't active");
        }

        [UnitTest]
        public void UserManagerShouldGetCorrectSession()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            IHttpContext context;
            LoginResponse result;
            UserManager userMgr;
            SignUpAndLogin(userName, out context, out result, out userMgr);

            Session session = Session.Get(context);

            Expect.AreEqual(session.Id.Value, userMgr.Session.Id.Value, "Session ids didn't match");
        }

        [UnitTest]
        public void SignOutShouldSucceed()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            IHttpContext context;
            LoginResponse result;
            UserManager userMgr;
            SignUpAndLogin(userName, out context, out result, out userMgr);

            SignOutResponse response = userMgr.SignOut();
            Expect.AreEqual(true, response.Success);
        }

        [UnitTest]
        public void SessionEndShouldEndSession()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            IHttpContext context;
            LoginResponse result;
            UserManager userMgr;
            SignUpAndLogin(userName, out context, out result, out userMgr);

            Session session = Session.Get(context);
            session.End();

            Expect.IsFalse(session.IsActive.Value);

            Session doubleCheck = Session.OneWhere(c => c.Id == session.Id.Value);

            Expect.IsFalse(doubleCheck.IsActive.Value, "Doublecheck was still active");
        }

        public static void SignUpAndLogin(string userName, out IHttpContext context, out LoginResponse result)
        {
            UserManager userProxy;
            SignUpAndLogin(userName, out context, out result, out userProxy);
        }

        public static void SignUpAndLogin(string userName, out IHttpContext context, out LoginResponse result, out UserManager userProxy)
        {
            string passHash;
            SignUp(userName, out userProxy, out context, out passHash);
            result = userProxy.Login(userName, passHash);
        }

        protected internal static void SignUp(string userName, out UserManager userProxy, out IHttpContext context, out string passHash)
        {
            SignUp(userName, "test@domain.cxm", out userProxy, out context, out passHash);
        }

        protected internal static void SignUp(string userName, string email)
        {
            UserManager mgr;
            IHttpContext context;
            string passHash;
            SignUp(userName, email, out mgr, out context, out passHash);
        }

        private static void SignUp(string userName, string email, out UserManager userProxy, out IHttpContext context, out string passHash)
        {
            userProxy = new UserManager();
            context = A.Fake<IHttpContext>();
            context.Request = new TestRequest();
            userProxy.HttpContext = context;

            Session session = Session.Get(context);

            passHash = "password".Sha1();
            EnsureUserNameAndEmailAreAvailable(userName, email);
            userProxy.SignUp(email, userName, passHash, false);
        }

        private static void EnsureUserNameAndEmailAreAvailable(string userName, string email)
        {
            User byEmail = User.OneWhere(c => c.Email == email);
            if (byEmail != null)
            {
                byEmail.Delete();
            }
            User byUserName = User.OneWhere(c => c.UserName == userName);
            if (byUserName != null)
            {
                byUserName.Delete();
            }
        }

        [UnitTest]
        public void LoginShouldFailIfBadPassword()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy;
            IHttpContext context;
            string passHash;
            SignUp(userName, out userProxy, out context, out passHash);

            LoginResponse result = userProxy.Login(userName, "badPassword");
            Expect.IsFalse(result.Success, "login should have failed");            
        }
        
        [UnitTest]
        public void LoginShouldSetUserInCurrentHttpSession()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy = new UserManager();
            IHttpContext context = A.Fake<IHttpContext>();
            context.Request = new TestRequest();
            userProxy.HttpContext = context;

            Session session = Session.Get(context);

            string passHash = "password".Sha1();
            string email = "test@domain.cxm";
            EnsureUserNameAndEmailAreAvailable(userName, email);
            userProxy.SignUp(email, userName, passHash, false);
            LoginResponse result = userProxy.Login(userName, passHash);

            Expect.IsTrue(result.Success);

            string check = context.User.Identity.Name;

            Expect.AreEqual(userName, check);
        }

        [UnitTest]
        public void SessionInitShouldSetHttpContextUser()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            IHttpContext context;
            LoginResponse result;
            SignUpAndLogin(userName, out context, out result);

            IHttpContext context2 = A.Fake<IHttpContext>();
            context2.Request = new TestRequest();

            Session session = Session.Init(context2);

            Expect.IsNotNull(context2.User);
            Expect.IsNotNull(context2.User.Identity);
            Expect.AreEqual(userName, context2.User.Identity.Name);
        }

        [UnitTest]
        public void LoginShouldCreateLoginEntry()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            Login.LoadAll().Delete();
            Expect.AreEqual(0, Login.LoadAll().Count);

            IHttpContext context;
            LoginResponse result;
            SignUpAndLogin(userName, out context, out result);

            User user = User.OneWhere(c=>c.UserName == userName);
            Login login = Login.OneWhere(c => c.UserId == user.Id);
            Expect.IsNotNull(login);
        }

        [UnitTest]
        public void UserShouldBeAuthenticatedAfterLogin()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            IHttpContext context;
            LoginResponse result;
            SignUpAndLogin(userName, out context, out result);

            IHttpContext context2 = A.Fake<IHttpContext>();
            context2.Request = new TestRequest();

            Session session = Session.Init(context2);
            Expect.IsTrue(session.UserOfUserId.IsAuthenticated);
            Expect.IsTrue(context2.User.Identity.IsAuthenticated);
        }

        [UnitTest]
        public void LoginDateTimeShouldBeNowInstant()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            IHttpContext context;
            LoginResponse result;
            SignUpAndLogin(userName, out context, out result);

            User user = User.GetByUserNameOrDie(userName);
            Instant nowInstant = new Instant();
            Login login = new Login();
            login.UserId = user.Id;
            login.DateTime = nowInstant.ToDateTime();
            login.Save();

            Login check = Login.OneWhere(c => c.Id == login.Id);

            Instant loginAt = new Instant(check.DateTime.Value);

            Expect.AreEqual(nowInstant, loginAt);
        }

        [UnitTest]
        public void SignUpShouldSetAccountToken()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy;
            IHttpContext context;
            string passHash;
            SignUp(userName, out userProxy, out context, out passHash);

            User user = User.GetByUserNameOrDie(userName);
            Account account = user.AccountsByUserId.FirstOrDefault();
            Expect.IsNotNull(user.AccountsByUserId.FirstOrDefault());
            Expect.IsNotNullOrEmpty(account.Token);

            OutLineFormat("Token: {0}", account.Token);
        }

        [UnitTest]
        public void ConfirmShouldConfirmAccount()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            IHttpContext context;
            LoginResponse result;
            SignUpAndLogin(userName, out context, out result);

            UserManager userMgr = new UserManager();
            User user = User.GetByUserNameOrDie(userName);
            Account account = user.AccountsByUserId.FirstOrDefault();

            userMgr.ConfirmAccount(account.Token);

            user.AccountsByUserId.Reload();
            account = user.AccountsByUserId.FirstOrDefault();

            Expect.IsTrue(account.IsConfirmed.Value, "was not confirmed");
        }

        [UnitTest]
        public void ShouldInitializeDaoUserManagerFromConfig()
        {
            UserManagerConfig config = new UserManagerConfig();
            UserManager mgr = config.Create();

            Expect.IsNotNull(mgr);
            Expect.IsNotNull(mgr.EmailComposer);
            Expect.IsNotNull(mgr.SmtpSettingsVault);
        }

        public class TestAppNameResolver: IApplicationNameProvider
        {
            public string GetApplicationName()
            {
                return "test";
            }
        }

        [UnitTest]
        public void ShouldInitializeDaoUserManagerAppNameResolver()
        {
            UserManagerConfig config = new UserManagerConfig();
            config.ApplicationNameResolverType = typeof(TestAppNameResolver).AssemblyQualifiedName;
            UserManager mgr = config.Create();

            Expect.IsNotNull(mgr);
            Expect.IsNotNull(mgr.ApplicationNameProvider);
            Expect.IsObjectOfType<TestAppNameResolver>(mgr.ApplicationNameProvider, "Resolver was a {0}"._Format(mgr.ApplicationNameProvider.GetType().Name));
        }

        [UnitTest]
        public void DaoUserManagerConfigTemplateDirectoryMustNotBeNull()
        {
            UserManagerConfig config = new UserManagerConfig();
            Expect.IsNotNull(config.EmailTemplateDirectoryPath);
        }

        static DirectoryInfo _testEmailComposerTemplateDir = new DirectoryInfo(".\\TestEmailComposerTempaltes");
        static string _ext = ".TestEmailComposerExt";
        public class TestEmailComposer : EmailComposer
        {
            public TestEmailComposer()
            {
                this.TemplateDirectory = _testEmailComposerTemplateDir;
                this.FileExtension = _ext;
            }
            public override string GetEmailBody(string emailName, params object[] data)
            {
                throw new NotImplementedException();
            }
        }
        [UnitTest]
        public void ShouldInitializeUserManagerEmailComposer()
        {
            UserManagerConfig config = new UserManagerConfig();
            config.EmailComposerType = typeof(TestEmailComposer).AssemblyQualifiedName;
            config.EmailTemplateDirectoryPath = _testEmailComposerTemplateDir.FullName;
            config.EmailTemplateExtension = _ext;
            UserManager mgr = config.Create();

            Expect.IsNotNull(mgr);
            Expect.IsNotNull(mgr.EmailComposer);
            Expect.IsObjectOfType<TestEmailComposer>(mgr.EmailComposer, "Composer was a {0}"._Format(mgr.EmailComposer.GetType().Name));
            Expect.AreEqual(_ext, mgr.EmailComposer.FileExtension);
            Expect.AreEqual(_testEmailComposerTemplateDir.FullName, mgr.EmailComposer.TemplateDirectory.FullName);
        }

        [UnitTest]
        public void ShouldInitializeUserManagerVault()
        {
            UserManagerConfig config = new UserManagerConfig();
            config.SmtpSettingsVaultPath = ".\\Test.vault.sqlite";
            UserManager mgr = config.Create();

            Expect.IsNotNull(mgr);
            Expect.IsNotNull(mgr.SmtpSettingsVault);
            Expect.IsNotNull(mgr.SmtpSettingsVault.ConnectionString);
            Expect.IsTrue(mgr.SmtpSettingsVault.ConnectionString.ToLowerInvariant().Contains("test.vault.sqlite"), "Unexpected connection string value");
        }
               
        [UnitTest]
        public void ShouldHaveConfirmationEmail()
        {
            UserManagerConfig config = new UserManagerConfig();
            UserManager mgr = config.Create();
            
            Expect.IsTrue(mgr.EmailComposer.TemplateExists(UserManager.AccountConfirmationEmailName), "Account Confirmation email template was not there");
        }

        [UnitTest]
        public void ShouldHavePasswordResetEmail()
        {
            UserManagerConfig config = new UserManagerConfig();
            UserManager mgr = config.Create();

            Expect.IsTrue(mgr.EmailComposer.TemplateExists(UserManager.PasswordResetEmailName), "Password Reset email template was not there");
        }

        [UnitTest]
        public void ShouldBeAbleToRequestConfirmationEmail()
        {
            UserManager mgr = CreateTestUserManager();
            mgr.HttpContext = A.Fake<IHttpContext>();
            mgr.HttpContext.Request = new TestRequest();
            SignUp("monkey", "bryan.apellanes@gmail.com");
            SendEmailResponse result = mgr.RequestConfirmationEmail("bryan.apellanes@gmail.com");

            Expect.IsTrue(result.Success, result.Message);
        }

        [UnitTest]
        public void ForgotPasswordShouldCreatePasswordResetEntry()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            string email = "bryan.apellanes@gmail.com";
            SignUp(userName, email);
            User user = User.GetByEmail(email);
            Expect.AreEqual(0, user.PasswordResetsByUserId.Count);

            UserManager userMgr = CreateTestUserManager();
            userMgr.HttpContext = A.Fake<IHttpContext>();
            userMgr.HttpContext.Request = new TestRequest();

            userMgr.ForgotPassword(email);

            user.PasswordResetsByUserId.Reload();
            Expect.AreEqual(1, user.PasswordResetsByUserId.Count);            
        }

        [UnitTest]
        public void ForgotPasswordShouldSucceed()
        {
			ClearAllUserInfo();
            string userName = MethodBase.GetCurrentMethod().Name;
            string email = "bryan.apellanes@gmail.com";
            SignUp(userName, email);
            User user = User.GetByEmail(email);
            Expect.AreEqual(0, user.PasswordResetsByUserId.Count);

            UserManager userMgr = CreateTestUserManager();
            userMgr.HttpContext = A.Fake<IHttpContext>();
            userMgr.HttpContext.Request = new TestRequest();

            ForgotPasswordResponse response = userMgr.ForgotPassword(email);

            Expect.IsTrue(response.Success);
        }

        [UnitTest]
        public void ResetPasswordShouldSucceed()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            string email = "bryan.apellanes@gmail.com";
            SignUp(userName, email);
            User user = User.GetByEmail(email);
            Expect.AreEqual(0, user.PasswordResetsByUserId.Count);

            UserManager userMgr = CreateTestUserManager();
            userMgr.HttpContext = A.Fake<IHttpContext>();
            userMgr.HttpContext.Request = new TestRequest();

            string password = ServiceProxySystem.GenerateId();
            ForgotPasswordResponse forgot = userMgr.ForgotPassword(email);
            PasswordResetResponse reset = userMgr.ResetPassword(password.Sha1(), (string)forgot.Data);
            Expect.IsTrue(reset.Success, "Reset failed");
        }

        [UnitTest]
        public void ResetPasswordShouldBeLoginnable()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            string email = "bryan.apellanes@gmail.com";
            SignUp(userName, email);
            User user = User.GetByEmail(email);
            Expect.AreEqual(0, user.PasswordResetsByUserId.Count);

            UserManager userMgr = CreateTestUserManager();
            userMgr.HttpContext = A.Fake<IHttpContext>();
            userMgr.HttpContext.Request = new TestRequest();

            string password = ServiceProxySystem.GenerateId();
            ForgotPasswordResponse forgot = userMgr.ForgotPassword(email);
            PasswordResetResponse reset = userMgr.ResetPassword(password.Sha1(), (string)forgot.Data);
            LoginResponse login = userMgr.Login(user.UserName, password.Sha1());
            Expect.IsTrue(login.Success, "Login failed");
        }

        private static UserManager CreateTestUserManager()
        {
            UserManagerConfig config = new UserManagerConfig();
            config.SmtpSettingsVaultPath = "C:\\src\\TestData\\Vaults\\StickerizeSmtpSettings.vault.sqlite";
            config.ApplicationNameResolverType = typeof(TestAppNameResolver).AssemblyQualifiedName;
            config.EmailTemplateDirectoryPath = "C:\\src\\TestData\\NamedFormatEmailTemplates";
            config.EmailTemplateExtension = ".nft";

            UserManager mgr = config.Create();
            return mgr;
        }
    }
}
