/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Server;
using Bam.Net.Testing;

namespace bam
{
    [Serializable]
	public class ManagementActions : CommandLineTestInterface
	{
        [ConsoleAction("signUp", "Sign Up")]
		public void SignUp()
		{
			throw new NotImplementedException();
		}

		[ConsoleAction("registerApplication", "Register Application")]
		public void PackApp()
		{
			throw new NotImplementedException();
		}
		
        [ConsoleAction("login", "Log in user to begin secure session")]
        public void Login()
        {
            throw new NotImplementedException();
        }

		[ConsoleAction("registerClientApplication", "Register Client Application")]
		public void RegisterApp()
		{
			BamServer server = new BamServer(BamConf.Load(GetRoot()));
			ConsoleLogger logger = new ConsoleLogger();
			logger.AddDetails = false;
			logger.UseColors = true;
			server.Subscribe(logger);
			AppContentResponder app = server.CreateApp(GetArgument("appName"));
			app.Subscribe(logger);
			app.Initialize();
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
	}
}
