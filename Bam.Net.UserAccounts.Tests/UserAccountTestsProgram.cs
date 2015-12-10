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
using Bam.Net.Messaging;
using System.Reflection;

namespace Bam.Net.UserAccounts.Tests
{
    [Serializable]
    public partial class UserAccountTestsProgram: CommandLineTestInterface
    {
        static void Main(string[] args)
        {
			IsolateMethodCalls = true;
            PreInit();
            Initialize(args);
        }
        
        public static void PreInit()
        {            
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
			AddValidArgument("t", true, "run all tests");
			DefaultMethod = typeof(UserAccountTestsProgram).GetMethod("Start");
		}

		public static void Start()
		{
			if (Arguments.Contains("t"))
			{
				RunAllTests(typeof(UserAccountTestsProgram).Assembly);
			}
			else
			{
				Interactive();
			}
		}

        [ConsoleAction("Output AssemblyQualifiedName of EasyMembershipProvider")]
        public void OutAssemblyQualifiedNameOfEasyMembershipProvider()
        {
            Out(typeof(DaoMembershipProvider).AssemblyQualifiedName);
            typeof(DaoMembershipProvider)
                .AssemblyQualifiedName
                .SafeWriteToFile(".\\EasyMembershipProviderAssemblyQualifiedName.txt");
        }
		
        static List<Dao> _toDelete = new List<Dao>();
        public void DeleteDaos()
        {
            
        }

        string userName = "monkey";
        string roleName = "TestRole";

        [UnitTest]
        public void ShouldBeAbleToCreateRole()
        {
            DaoRoleProvider provider = new DaoRoleProvider();
            Expect.IsFalse(provider.RoleExists(roleName));
            provider.CreateRole(roleName);
            Expect.IsTrue(provider.RoleExists(roleName));
            provider.DeleteRole(roleName, false);
            Expect.IsFalse(provider.RoleExists(roleName));
        }

        [UnitTest]
        public void ShouldBeAbleToAddUsersToRole()
        {
            User user = User.Ensure(userName);
            
            DaoRoleProvider provider = new DaoRoleProvider();
            provider.DeleteRole(roleName, false);
            Expect.IsFalse(provider.RoleExists(roleName));
            provider.CreateRole(roleName);

            Expect.IsFalse(provider.IsUserInRole(userName, roleName));
            provider.AddUsersToRoles(new string[] { userName }, new string[] { roleName });

            Expect.IsTrue(provider.IsUserInRole(userName, roleName), "user wasn't added to role");

            provider.DeleteRole(roleName, false);
        }

        [UnitTest]
        public void ShouldInitFromAppConfig()
        {
            Dictionary<string, string> settings = new Dictionary<string,string>();
            settings.Add("Roles", "Admin: Monkey, Gorilla; User: BabyShoes, RegularUser");
            DefaultConfiguration.SetAppSettings(settings);            
            
            DaoRoleProvider.InitializeFromConfig();

            ExpectExists("Gorilla");
            ExpectExists("Monkey");
            ExpectExists("BabyShoes");
            ExpectExists("RegularUser");

            DaoRoleProvider roleprovider = new DaoRoleProvider();
            Expect.IsTrue(roleprovider.IsUserInRole("Gorilla", "Admin"));
            Expect.IsTrue(roleprovider.IsUserInRole("Monkey", "Admin"));
            Expect.IsFalse(roleprovider.IsUserInRole("Gorilla", "User"));
            Expect.IsFalse(roleprovider.IsUserInRole("Monkey", "User"));
            Expect.IsTrue(roleprovider.IsUserInRole("BabyShoes", "User"));
            Expect.IsTrue(roleprovider.IsUserInRole("RegularUser", "User"));
            Expect.IsFalse(roleprovider.IsUserInRole("BabyShoes", "Admin"));
            Expect.IsFalse(roleprovider.IsUserInRole("RegularUser", "Admin"));
        }

        private void ExpectExists(string userName)
        {
            User user = User.OneWhere(c => c.UserName == userName);
            Expect.IsNotNull(user, "user {0} not found"._Format(userName));
            _toDelete.Add(user);
        }

        [UnitTest]
        public void DateComparisons()
        {
            DateTime yesterday = DateTime.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0));
            Expect.IsTrue(DateTime.UtcNow > yesterday);
        }
        
        [UnitTest]
        public void ShouldNotConfirmAccountIfTokenIsExpired()
        {
            DaoMembershipProvider provider;
            User user;
            Account confirmation;
            GetProviderAndPendingConfirmation(out provider, out user, out confirmation);

            confirmation.CreationDate = DateTime.UtcNow.Subtract(new TimeSpan(6, 0, 0, 0));
            confirmation.Save();

            bool result = provider.ConfirmAccount(confirmation.Token);

            Expect.IsFalse(result);

            confirmation = Account.OneWhere(c => c.Id == confirmation.Id);
            Expect.IsFalse(confirmation.IsConfirmed.Value);
            OutLine(confirmation.Token);
        }

        [UnitTest]
        public void ShouldConfirmAccount()
        {
            DaoMembershipProvider provider;
            User user;
            Account account;
            GetProviderAndPendingConfirmation(out provider, out user, out account);

            bool result = provider.ConfirmAccount(account.Token);

            Expect.IsTrue(result);

            account = Account.OneWhere(c => c.Id == account.Id);
            Expect.IsTrue(account.IsConfirmed.Value);
            OutLine(account.Token);
        }

        static string _testUserName = "TestUser";
        private static void GetProviderAndPendingConfirmation(out DaoMembershipProvider provider, out User user, out Account account)
        {
            SQLiteRegistrar.Register("Users");
            Db.TryEnsureSchema("Users");

            user = GetTestUser();

            provider = new DaoMembershipProvider();
            account = Account.Create(user, "test", "test");
            Expect.IsNotNull(account.Id);
            Expect.IsTrue(account.Id > 0);
            Expect.IsFalse(account.IsConfirmed.Value);
        }

        private static User GetTestUser()
        {
            User user;
            user = User.OneWhere(c => c.UserName == _testUserName);
            if (user == null)
            {
                user = new User();
                user.CreationDate = DateTime.UtcNow;
                user.UserName = _testUserName;
                user.Save();
            }
            return user;
        }

        
    }
}
