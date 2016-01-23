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
    public partial class Program: CommandLineTestInterface
    {
        static void Main(string[] args)
        {
			IsolateMethodCalls = true;
            PreInit();
            Initialize(args);
        }
        
        public static void PreInit()
        {
			DefaultMethod = typeof(Program).GetMethod("Start");
		}

		public static void Start()
		{
			if (Arguments.Contains("t"))
			{
				RunAllUnitTests(typeof(Program).Assembly);
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
        public void ShouldBeAbleToCreateAndDeleteRole()
        {
            DaoRoleProvider provider = new DaoRoleProvider();
            Expect.IsFalse(provider.RoleExists(roleName), "Role should not have existed: {0}"._Format(roleName));
            provider.CreateRole(roleName);
            Expect.IsTrue(provider.RoleExists(roleName), "Role didn't get created");
            provider.DeleteRole(roleName, false);
            Expect.IsFalse(provider.RoleExists(roleName), "Role didn't get deleted");
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
        
        private static void GetProviderAndPendingConfirmation(out DaoMembershipProvider provider, out User user, out Account account)
        {
            SQLiteRegistrar.Register("Users");
            Db.TryEnsureSchema("Users");

            user = UserTestTools.GetTestUser();

            provider = new DaoMembershipProvider();
            account = Account.Create(user, "test", "test");
            Expect.IsNotNull(account.Id);
            Expect.IsTrue(account.Id > 0);
            Expect.IsFalse(account.IsConfirmed.Value);
        }

        private void ExpectExists(string userName)
        {
            User user = User.OneWhere(c => c.UserName == userName);
            Expect.IsNotNull(user, "user {0} not found"._Format(userName));
            _toDelete.Add(user);
        }

    }
}
