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
using NSubstitute;
using Bam.Net.Web;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.Messaging;
using Bam.Net.ServiceProxy.Secure;
using System.Threading;
using Bam.Net.Testing.Integration;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing.Unit;

namespace Bam.Net.UserAccounts.Tests
{
    public partial class Program
    {
        [BeforeUnitTests]
        public void Setup()
        {
            UserTestTools.ClearAllUserInfo();
        }

        [AfterUnitTests]
        public void Teardown()
        {
            UserTestTools.ClearAllUserInfo();
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
            UserManager user = new UserManager
            {
                HttpContext = Substitute.For<IHttpContext>()
            };

            SignUpResponse result = user.SignUp("Test@stickerize.me", "Test", "Password".Sha1(), false);
            Expect.IsNotNull(result.Success, "No Success value specified");
            Expect.IsTrue(result.Success, "signup failed");
            OutLine(result.Message);
            OutLine(result.Data.ToString());
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
            IHttpContext context = Substitute.For<IHttpContext>();
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
            IHttpContext context = Substitute.For<IHttpContext>();
            context.Request = new TestRequest();

            Session session = Session.Init(context);

            Expect.IsNotNull(session.CreationDate, "CreationDate was null");
            Instant created = new Instant(session.CreationDate.Value);

            Expect.IsNotNull(session.LastActivity, "LastActivity was null");
            Instant lastActivity = new Instant(session.LastActivity.Value);
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
            UserTestTools.SignUpAndLogin(userName, out context, out result);

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
            UserTestTools.SignUpAndLogin(userName, out context, out result, out userMgr);

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
            UserTestTools.SignUpAndLogin(userName, out context, out result, out userMgr);

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
            UserTestTools.SignUpAndLogin(userName, out context, out result, out userMgr);

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
            UserTestTools.SignUpAndLogin(userName, out context, out result, out userMgr);

            Session session = Session.Get(context);
            session.End();

            Expect.IsFalse(session.IsActive.Value);

            Session doubleCheck = Session.OneWhere(c => c.Id == session.Id.Value);

            Expect.IsFalse(doubleCheck.IsActive.Value, "Doublecheck was still active");
        }

        [UnitTest]
        public void LoginShouldFailIfBadPassword()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy;
            IHttpContext context;
            string passHash;
            UserTestTools.SignUp(userName, out userProxy, out context, out passHash);

            LoginResponse result = userProxy.Login(userName, "badPassword");
            Expect.IsFalse(result.Success, "login should have failed");
        }

        [UnitTest]
        public void LoginShouldSetUserInCurrentHttpSession()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserManager userProxy = new UserManager();
            IHttpContext context = Substitute.For<IHttpContext>();
            context.Request = new TestRequest();
            userProxy.HttpContext = context;

            Session session = Session.Get(context);

            string passHash = "password".Sha1();
            string email = "test@domain.cxm";
            UserTestTools.EnsureUserNameAndEmailAreAvailable(userName, email);
            userProxy.SignUp(email, userName, passHash, false);
            LoginResponse result = userProxy.Login(userName, passHash);

            Expect.IsTrue(result.Success);

            string check = context.User.Identity.Name;

            Expect.AreEqual(userName, check);
        }

        [IntegrationTest]
        public void SessionInitShouldSetHttpContextUser()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            UserTestTools.SignUpAndLogin(userName, out IHttpContext context, out LoginResponse result);

            IHttpContext context2 = Substitute.For<IHttpContext>();
            context2.Request = new TestRequest();

            Session session = Session.Init(context2);

            Expect.IsNotNull(context2.User, "context2.User was null");
            Expect.IsNotNull(context2.User.Identity, $"context2.User.Identity was null:\r\n   {session.Database.ConnectionString}");
            Expect.AreEqual(userName, context2.User.Identity.Name);
        }

        [UnitTest]
        public void LoginShouldCreateLoginEntry()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            Login.LoadAll().Delete();
            Expect.AreEqual(0, Login.LoadAll().Count);

            UserTestTools.SignUpAndLogin(userName, out IHttpContext context, out LoginResponse result);

            User user = User.OneWhere(c => c.UserName == userName);
            Login login = Login.OneWhere(c => c.UserId == user.Id);
            Expect.IsNotNull(login);
        }

        [UnitTest]
        public void UserShouldBeAuthenticatedAfterLogin()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            User.UserDatabase = new SQLiteDatabase(userName);
            User.UserDatabase.TryEnsureSchema<User>();
            UserTestTools.SignUpAndLogin(userName, out IHttpContext context, out LoginResponse result);

            IHttpContext context2 = Substitute.For<IHttpContext>();
            context2.Request = new TestRequest();

            Session session = Session.Init(context2);
            Expect.IsTrue(session.UserOfUserId.IsAuthenticated);
            Expect.IsTrue(context2.User.Identity.IsAuthenticated);
        }

        [UnitTest]
        public void LoginDateTimeShouldBeNowInstant()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            User.UserDatabase = new SQLiteDatabase(userName);
            User.UserDatabase.TryEnsureSchema<User>();
            UserTestTools.SignUpAndLogin(userName, out IHttpContext context, out LoginResponse result);

            User user = User.GetByUserNameOrDie(userName);
            Instant nowInstant = new Instant();
            Login login = new Login
            {
                UserId = user.Id,
                DateTime = nowInstant.ToDateTime()
            };
            login.Save();

            Login check = Login.OneWhere(c => c.Id == login.Id);

            Instant loginAt = new Instant(check.DateTime.Value);

            Expect.AreEqual(nowInstant, loginAt);
        }

        [UnitTest]
        public void SignUpShouldSetAccountToken()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            User.UserDatabase = new SQLiteDatabase(userName);
            User.UserDatabase.TryEnsureSchema<User>();
            UserTestTools.SignUp(userName, out UserManager userProxy, out IHttpContext context, out string passHash);

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
            UserTestTools.SignUpAndLogin(userName, out IHttpContext context, out LoginResponse result);

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

        [UnitTest]
        public void ShouldInitializeDaoUserManagerAppNameResolver()
        {
            UserManagerConfig config = new UserManagerConfig();
            config.ApplicationNameResolverType = typeof(TestAppNameProvider).AssemblyQualifiedName;
            UserManager mgr = config.Create();

            Expect.IsNotNull(mgr);
            Expect.IsNotNull(mgr.ApplicationNameProvider);
            Expect.IsObjectOfType<TestAppNameProvider>(mgr.ApplicationNameProvider, "Resolver was a {0}"._Format(mgr.ApplicationNameProvider.GetType().Name));
        }

        [UnitTest]
        public void DaoUserManagerConfigTemplateDirectoryMustNotBeNull()
        {
            UserManagerConfig config = new UserManagerConfig();
            Expect.IsNotNull(config.EmailTemplateDirectoryPath);
        }

        static DirectoryInfo _testEmailComposerTemplateDir = new DirectoryInfo(".\\TestEmailComposerTemplates");
        static string _ext = ".TestEmailComposerExt";
        public class TestEmailComposer : EmailComposer
        {
            public TestEmailComposer()
            {
                this.TemplateDirectory = _testEmailComposerTemplateDir;
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
            UserManager mgr = config.Create();

            Expect.IsNotNull(mgr);
            Expect.IsNotNull(mgr.EmailComposer);
            Expect.IsObjectOfType<TestEmailComposer>(mgr.EmailComposer, "Composer was a {0}"._Format(mgr.EmailComposer.GetType().Name));
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
    }
}
