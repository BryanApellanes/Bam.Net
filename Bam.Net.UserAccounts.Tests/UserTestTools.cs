using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.UserAccounts;
using Bam.Net.UserAccounts.Data;
using Bam.Net.ServiceProxy;
using Bam.Net.Data.SQLite;
using Bam.Net.Data;
using FakeItEasy;

namespace Bam.Net.UserAccounts.Tests
{
    public class UserTestTools
    {
        public static string TestUser = "TestUser";
        static UserTestTools()
        {
            SQLiteRegistrar.Register<User>();
            Db.TryEnsureSchema<User>();

            ClearAllUserInfo();
        }

        public static User GetTestUser()
        {
            User user;
            user = User.OneWhere(c => c.UserName == TestUser);
            if (user == null)
            {
                user = new User();
                user.CreationDate = DateTime.UtcNow;
                user.UserName = TestUser;
                user.Save();
            }
            return user;
        }

        public static void SignUp(string userName, string email, out UserManager userProxy, out IHttpContext context, out string passHash)
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

        public static void EnsureUserNameAndEmailAreAvailable(string userName, string email)
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

        public static void SignUp(string userName, out UserManager userProxy, out IHttpContext context, out string passHash)
        {
            UserTestTools.SignUp(userName, "test@domain.cxm", out userProxy, out context, out passHash);
        }

        public static void SignUp(string userName, string email)
        {
            UserManager mgr;
            IHttpContext context;
            string passHash;
            UserTestTools.SignUp(userName, email, out mgr, out context, out passHash);
        }

        public static UserManager CreateTestUserManager()
        {
            UserManagerConfig config = new UserManagerConfig();
            config.SmtpSettingsVaultPath = "C:\\TestData\\Vaults\\StickerizeSmtpSettings.vault.sqlite";
            config.ApplicationNameResolverType = typeof(TestAppNameProvider).AssemblyQualifiedName;
            config.EmailTemplateDirectoryPath = "C:\\TestData\\NamedFormatEmailTemplates";

            UserManager mgr = config.Create();
            return mgr;
        }
        public static void ClearAllUserInfo()
        {
            DeleteAllAccounts();
            DeleteAllPasswords();
            DeleteAllLogins();
            DeleteAllSessions();
            DeleteAllUsers();
        }

        public static void DeleteAllUsers()
        {
            UserCollection users = User.LoadAll();
            users.Delete();
        }

        public static void DeleteAllSessions()
        {
            SessionCollection sessions = Session.LoadAll();
            sessions.Delete();
        }

        public static void DeleteAllLogins()
        {
            LoginCollection logins = Login.LoadAll();
            logins.Delete();
        }

        public static void DeleteAllPasswords()
        {
            PasswordCollection passwords = Password.LoadAll();
            passwords.Delete();
        }

        public static void DeleteAllAccounts()
        {
            AccountCollection accounts = Account.LoadAll();
            accounts.Delete();
        }
    }
}
