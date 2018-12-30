using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Data;
using Bam.Net.Logging;
using Bam.Net.Testing;
using Bam.Net.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    internal class ServiceTools : CommandLineTestInterface
    {
        public static T GetService<T>()
        {
            ServiceRegistry svcRegistry = CoreServiceRegistryContainer.GetServiceRegistry();
            return svcRegistry.Get<T>();
        }

        public static Database GetUserDatabase()
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
            OutLine(userDatabase.ConnectionString, color);
            return userDatabase;
        }

        public static string ConfirmPasswordPrompt()
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

        public static UserInfo GetUserInfo()
        {
            return new UserInfo
            {
                Email = GetArgument("email", true, "Please enter your email address"),
                Password = GetPasswordArgument("password", true, "Please enter your existing or new password"),
            };
        }
        
        static ILogger _logger;
        public static ILogger GetLogger()
        {
            if (_logger == null)
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
    }
}
