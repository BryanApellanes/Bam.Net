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

namespace Bam.Net.Application
{
    [Serializable]
	public class ManagementActions : CommandLineTestInterface
	{
        const string BamSysPath = "C:\\bam\\sys\\";

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

		[ConsoleAction("createClientApplication", "Create Client Application")]
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
        
        [ConsoleAction("createManifest", "Create BamAppManifest from a specified directory")]
        public void CreateManifest()
        {
            string directoryPath = GetArgument("appDirectory", true, "Please enter the path to the directory");
            string appName = GetArgument("appName", true, "Please enter the name of the application to create the manifest for");
            BamAppManifest manifest = new BamAppManifest() { AppName = appName };
            DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
            List<string> fileNames = new List<string>();
            foreach(FileInfo file in dirInfo.GetFiles())
            {
                fileNames.Add(file.Name);
            }
            manifest.FileNames = fileNames.ToArray();
            List<string> dirNames = new List<string>();
            foreach(DirectoryInfo dir in dirInfo.GetDirectories())
            {
                dirNames.Add(dir.Name);
            }
            manifest.DirectoryNames = dirNames.ToArray();
            manifest.ToJsonFile(Path.Combine(BamSysPath, $"{manifest.AppName}.bamapp.json"));
        }

        [ConsoleAction("updateFromManifest", "Update the specified system app in c:\\bam\\sys\\{appName}")]
        public void UpdateSysApp()
        {
            string appName = GetArgument("appName", true, "Please enter the name of the application to update");
            string manifestPath = Path.Combine(BamSysPath, $"{appName}.bamapp.json");
            if (!File.Exists(manifestPath))
            {
                Warn("Manifest for the specified app was not found");
                return;
            }            
            Log.AddLogger(new ConsoleLogger { UseColors = true, AddDetails = false, ApplicationName = Assembly.GetEntryAssembly().GetFileInfo().Name });
            string appPath = Path.Combine(BamSysPath, appName);
            BamAppManifest manifest = manifestPath.FromJsonFile<BamAppManifest>();
            DirectoryInfo source = new DirectoryInfo(".");
            DirectoryInfo dest = new DirectoryInfo(appPath);
            if (dest.Exists)
            {
                string moveTo = $"{appPath}_unkown-commit-".RandomLetters(4);
                string commitFile = Path.Combine(dest.FullName, "commit");
                if (File.Exists(commitFile))
                {
                    moveTo = $"{appPath}_{File.ReadAllText(commitFile)}";
                }
                else
                {
                    Log.Warn("Commit file {0} not found", commitFile);
                }
                Log.Info("Moving old instance from {0} to {1}", dest.Name, moveTo);
                dest.MoveTo(moveTo);
            }
            dest = new DirectoryInfo(appPath);
            foreach(string dirName in manifest.DirectoryNames)
            {
                DirectoryInfo srcSubdir = new DirectoryInfo(Path.Combine(source.FullName, dirName));
                if (srcSubdir.Exists)
                {
                    DirectoryInfo destSubDir = new DirectoryInfo(Path.Combine(dest.FullName, dirName));
                    if (!destSubDir.Parent.Exists)
                    {
                        destSubDir.Parent.Create();
                    }
                    Log.Info("Copying {0} to {1}", srcSubdir.FullName, destSubDir.FullName);
                    srcSubdir.Copy(destSubDir.FullName);
                }
                else
                {
                    Log.Warn("Directory {0} doesn't exist", srcSubdir.FullName);
                }
            }
            foreach(string fileName in manifest.FileNames)
            {
                FileInfo srcFile = new FileInfo(Path.Combine(source.FullName, fileName));
                if (srcFile.Exists)
                {
                    FileInfo destFile = new FileInfo(Path.Combine(dest.FullName, fileName));
                    if (!destFile.Directory.Exists)
                    {
                        destFile.Directory.Create();
                    }
                    Log.Info("Copying {0} to {1}", srcFile.FullName, destFile.FullName);
                    srcFile.CopyTo(destFile.FullName);
                }
                else
                {
                    Log.Warn("File {0} doesn't exist", srcFile.FullName);
                }
            }
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
