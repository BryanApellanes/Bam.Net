using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net.Javascript.Sql;
using Bam.Net.Server;
using Bam.Net.Testing;
using System;
using System.IO;

namespace Bam.Net.Application
{
    [Serializable]
	class Program : CommandLineTestInterface
	{
		static void Main(string[] args)
		{
			IsolateMethodCalls = false;
			Directory.SetCurrentDirectory(Server.ContentRoot);

			Type type = typeof(Program);
			AddSwitches(type);
			AddConfigurationSwitches();
			AddValidArgument("i", true, description: "interactive");

			DefaultMethod = type.GetMethod("Interactive");

			ParseArgs(args); 

			SetSqlProvider();

			if (Arguments.Length > 0 && !Arguments.Contains("i"))
			{
				ExecuteSwitches(Arguments, type, null, null);
			}
			else
			{
				Interactive();
			}
		}

		static BamServer _server;
		static object _serverLock = new object();
		public static BamServer Server
		{
			get
			{
				return _serverLock.DoubleCheckLock(ref _server, () => new BamServer(BamConf.Load()));
			}
		}

		[ConsoleAction("S", "Start jssql server")]
		public static void StartDefaultServer()
		{
			Server.Start();
			Pause("jssql server started");
		}

		[ConsoleAction("K", "Stop (Kill) jssql server")]
		public static void StopDefaultServer()
		{
			Server.Stop();
			_server = null;
			Pause("jssql server stopped");
		}

		[ConsoleAction("R", "Restart jssql server")]
		public static void RestartDefaultServer()
		{
			Server.Stop();
			_server = null; // force reinitialization
			Server.Start();
			Pause("jssql server re-started");
		}

		private static void SetSqlProvider()
		{
            JavaScriptSqlProvider provider = (JavaScriptSqlProvider)Type.GetType(DefaultConfiguration.GetAppSetting("JavaScriptSqlProvider")).Construct();
			CommandLineArgumentConfigurer configurer = new CommandLineArgumentConfigurer(Arguments);
			configurer.Configure(provider);
			Server.AddCommonService<JavaScriptSqlProvider>(provider);
		}
	}
}
