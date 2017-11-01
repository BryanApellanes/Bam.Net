/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Server;
using Bam.Net.Testing;
using Bam.Net.Services.Clients;
using Bam.Net.Logging;
using Bam.Net.UserAccounts;

namespace bam
{
    [Serializable]
	public class ManagementActions : CommandLineTestInterface
	{
        public const string HeartServer = "http://bamapps.net";
        
        [ConsoleAction("signUp", "Sign Up")]
		public void SignUp()
		{
            UserInfo info = GetUserInfo();
            CoreClient client = new CoreClient(GetLogger());
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

        [ConsoleAction("login", "Log in user to begin secure session")]
        public void Login()
        {
            throw new NotImplementedException();
        }

        [ConsoleAction("registerService", "Register Service")]
		public void RegisterApplication()
		{
			throw new NotImplementedException();
		}

		[ConsoleAction("registerClientApplication", "Register Client Application")]
		public void RegisterApp()
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
        
        private UserInfo GetUserInfo()
        {
            return new UserInfo
            {
                Email = GetArgument("email", true, "Please enter your email address"),
                Password = GetPasswordArgument("password", true, "Please enter your existing or new password"),
            };
        }

        private static string GetArgument(string name)
		{
			string value = Arguments.Contains(name) ? Arguments[name] : Prompt("Please enter a value for {0}"._Format(name));
			return value;
		}

		private static string GetRoot()
		{
			string root;
			root = Arguments.Contains("root") ? Arguments["root"] : Prompt("Please enter the root directory path");
			return root;
		}

		private static void GetRootAndSaveTarget(out string root, out string saveTo)
		{
			root = GetRoot();
			saveTo = Arguments.Contains("saveTo") ? Arguments["saveTo"] : Prompt("Please enter the file name to save to");
			if (!saveTo.EndsWith(".zip"))
			{
				saveTo += ".zip";
			}
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
	}
}
