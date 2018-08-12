using System;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Server;
using Bam.Net.Testing;
using Bam.Net.Services.Clients;
using Bam.Net.Logging;
using Bam.Net.UserAccounts;
using Bam.Net.Automation;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Bam.Net.CoreServices;
using Bam.Net.UserAccounts.Data;
using Bam.Net.Data;
using System.Linq;
using Bam.Net.Messaging;
using Bam.Net.Testing.Integration;

namespace Bam.Net.Application
{
    /// <summary>
    /// User administrative actions
    /// </summary>
    /// <seealso cref="Bam.Net.Testing.CommandLineTestInterface" />
    [Serializable]
	public class UserAdministrationActions : CommandLineTestInterface
	{
        const string BamSysPath = "C:\\bam\\sys\\";

        /// <summary>
        /// Sets the local SMTP settings.
        /// </summary>
        [ConsoleAction("setLocalSmtpSettings", "set local smtp settings")]
        public void SetLocalSmtpSettings()
        {
            string smtpSettingsFile = GetArgument("setLocalSmtpSettings", "Please enter the path to the smtp settings json file");            
            if (!File.Exists(smtpSettingsFile))
            {
                OutLineFormat("Specified smtp settings file doesn't exist: {0}", ConsoleColor.Magenta, smtpSettingsFile);
                return;
            }
            SmtpSettings settings = smtpSettingsFile.FromJsonFile<SmtpSettings>();
            if (string.IsNullOrEmpty(settings.Password))
            {
                settings.Password = PasswordPrompt("Please enter the smtp password", ConsoleColor.Yellow);
            }

            NotificationService.SetDefaultSmtpSettings(settings);
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        [ConsoleAction("sendEmail", "send an email")]
        public void SendMessage()
        {
            string email = GetArgument("sendEmail", "Please enter the email address to send to");
            string subject = GetArgument("subject", "Please enter the message subject");
            string message = GetArgument("message", "Please enter the message body");
            string from = GetArgument("from", "Please enter the from display name");

            NotificationService svc = GetService<NotificationService>();
            svc.NotifyRecipientEmail(email, message, subject, null, from);
        }

        /// <summary>
        /// List all users from the local database.
        /// </summary>
        [ConsoleAction("listLocalUsers", "list local users")]
        public void ListLocalUsers()
        {
            Database userDatabase = GetUserDatabase();
            UserCollection users = User.LoadAll(userDatabase);
            int num = 1;
            foreach (User user in users)
            {
                OutLineFormat("{0}. ({1}) {2}", num, user.Email, user.UserName);
                OutLineFormat("\tRoles: {0}", string.Join(",", user.Roles.Select(r => r.Name).ToArray()));
            }
        }

        /// <summary>
        /// Create a user in the local user database.
        /// </summary>
        [ConsoleAction("createLocalUser", "create a local user account")]
        public void CreateLocalUser()
        {
            Database userDatabase = GetUserDatabase();
            string userName = Prompt("Please enter the name for the new user");
            string emailAddress = Prompt("Please enter the new user's email address");

            User user = User.Create(userName, emailAddress, ConfirmPasswordPrompt().Sha1());
            OutLineFormat("User created: \r\n{0}", ConsoleColor.Cyan, user.ToJsonSafe().ToJson(true));
        }

        /// <summary>
        /// Lists the local roles.
        /// </summary>
        [ConsoleAction("listLocalRoles", "list local roles")]
        public void ListLocalRoles()
        {
            Database userDatabase = GetUserDatabase();
            RoleCollection roles = Role.LoadAll(userDatabase);
            int num = 1;
            foreach(Role role in roles)
            {
                OutLineFormat("{0}. {1}", num, role.Name);
            }
        }

        /// <summary>
        /// Adds the user to role.
        /// </summary>
        [ConsoleAction("addUserToRole", "add user to role")]
        public void AddUserToRole()
        {
            Database userDatabase = GetUserDatabase();
            string email = Prompt("Please enter the user's email address");
            User user = User.FirstOneWhere(u => u.Email == email, userDatabase);
            if(user == null)
            {
                OutLine("Unable to find a user with the specified address", ConsoleColor.Yellow);
                return;
            }
            string role = Prompt("Please enter the role to add the user to");
            Role daoRole = Role.FirstOneWhere(r => r.Name == role, userDatabase);
            if(daoRole == null)
            {
                daoRole = new Role(userDatabase)
                {
                    Name = role
                };
                daoRole.Save(userDatabase);
            }
            Role existing = user.Roles.FirstOrDefault(r => r.Name.Equals(daoRole.Name));
            if(existing == null)
            {
                user.Roles.Add(daoRole);
                user.Save(userDatabase);
                OutLineFormat("User ({0}) added to role ({1})", user.UserName, daoRole.Name);
            }
            else
            {
                OutLine("User already in specified role");
            }
        }

        /// <summary>
        /// Deletes a local user.
        /// </summary>
        [ConsoleAction("deleteLocalUser", "delete a local user account")]
        public void DeleteLocalUser()
        {
            Database userDatabase = GetUserDatabase();
            if (!Confirm("Whoa, whoa, hold your horses cowboy!! Are you sure you know what you're doing?", ConsoleColor.DarkYellow))
            {
                return;
            }
            OutLineFormat("This might not work depending on the state of the user's activity and related data.  Full scrub of user's is not implemented to help ensure data integrity into the future.", ConsoleColor.DarkYellow);
            if (!Confirm("Continue?", ConsoleColor.DarkYellow))
            {
                return;
            }
            string email = Prompt("Please enter the user's email address");
            User toDelete = User.FirstOneWhere(u => u.Email == email, userDatabase);
            if (toDelete == null)
            {
                OutLineFormat("Unable to find the user with the email address {0}", ConsoleColor.Magenta, email);
                return;
            }

            try
            {
                if(!Confirm($"Last chance to turn back!! About to delete this user:\r\n{toDelete.ToJsonSafe().ToJson(true)}", ConsoleColor.Yellow))
                {
                    return;
                }
                toDelete.Delete(userDatabase);
                OutLineFormat("User deleted", ConsoleColor.DarkMagenta);
            }
            catch (Exception ex)
            {
                OutLineFormat("Delete user failed: {0}", ConsoleColor.Magenta, ex.Message);
            }
        }

        /// <summary>
        /// Signs up.
        /// </summary>
        [ConsoleAction("signUp", "Sign Up for an account on bamapps.net")]
		public void SignUp()
		{
            UserInfo info = GetUserInfo();
            Services.Clients.CoreClient client = new Services.Clients.CoreClient(GetLogger());
            SignUpResponse response = client.SignUp(info.Email, info.Password);
            if (!response.Success)
            {
                OutLine(response.Message, ConsoleColor.Magenta);
            }
            else
            {
                OutLineFormat("{0} signed up successfully", info.Email);
            }
		}

        /// <summary>
        /// Registers the application.
        /// </summary>
        [ConsoleAction("createClientApplication", "Create Client Application")]
		public void CreateClientApplication()
		{
			BamServer server = new BamServer(BamConf.Load(GetRoot()));
            ConsoleLogger logger = new ConsoleLogger()
            {
                AddDetails = false,
                UseColors = true
            };
            server.Subscribe(logger);
			AppContentResponder app = server.CreateApp(GetArgument("appName"));
			app.Subscribe(logger);
			app.Initialize();
		}
        
        private string ConfirmPasswordPrompt()
        {
            string password1 = PasswordPrompt("Please enter the new user's password");
            OutLine();
            string password2 = PasswordPrompt("Please confirm the new user's password");
            OutLine();
            if (!password1.Equals(password2))
            {
                OutLine("passwords did not match", ConsoleColor.Yellow);
                return ConfirmPasswordPrompt();
            }

            return password1;
        }

        private UserInfo GetUserInfo()
        {
            return new UserInfo
            {
                Email = GetArgument("email", true, "Please enter your email address"),
                Password = GetPasswordArgument("password", true, "Please enter your existing or new password"),
            };
        }

		private static string GetRoot()
		{
			string root;
			root = Arguments.Contains("root") ? Arguments["root"] : Prompt("Please enter the root directory path");
			return root;
		}
        
        static ILogger _logger;
        private static ILogger GetLogger()
        {
            if(_logger == null)
            {
                _logger = new ConsoleLogger()
                {
                    UseColors = true,
                    AddDetails = false
                };
                _logger.StartLoggingThread();                
            }
            return _logger;
        }

        private static T GetService<T>()
        {
            ServiceRegistry svcRegistry = CoreServiceRegistryContainer.GetServiceRegistry();
            return svcRegistry.Get<T>();
        }

        private static Database GetUserDatabase()
        {
            UserManager mgr = GetService<UserManager>();
            Database userDatabase = mgr.Database;
            ConsoleColor color = ConsoleColor.DarkYellow;
            switch (ProcessMode.Current.Mode)
            {
                case ProcessModes.Dev:
                    color = ConsoleColor.Green;
                    break;
                case ProcessModes.Test:
                    color = ConsoleColor.Yellow;
                    break;
                case ProcessModes.Prod:
                    color = ConsoleColor.Red;
                    break;
            }
            OutLineFormat(userDatabase.ConnectionString, color);
            return userDatabase;
        }
    }
}
