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

namespace Bam.Net.Application
{
    [Serializable]
	public class ManagementActions : CommandLineTestInterface
	{
        const string BamSysPath = "C:\\bam\\sys\\";

        [ConsoleAction("listLocalUsers", "list local users")]
        public void UserAdmin()
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

        [ConsoleAction("createLocalUser", "create a local user account")]
        public void CreateLocalUser()
        {
            Database userDatabase = GetUserDatabase();
            string userName = Prompt("Please enter the name for the new user");
            string emailAddress = Prompt("Please enter the new user's email address");

            User user = User.Create(userName, emailAddress, ConfirmPasswordPrompt().Sha1());
            OutLineFormat("User created: \r\n{0}", ConsoleColor.Cyan, user.ToJsonSafe().ToJson(true));
        }

        [ConsoleAction("listRoles", "list local roles")]
        public void ListRoles()
        {
            Database userDatabase = GetUserDatabase();
            RoleCollection roles = Role.LoadAll(userDatabase);
            int num = 1;
            foreach(Role role in roles)
            {
                OutLineFormat("{0}. {1}", num, role.Name);
            }
        }

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

        [ConsoleAction("signUp", "Sign Up for an account on bamapps.net")]
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

        private static Database GetUserDatabase()
        {
            ServiceRegistry svcRegistry = ApplicationServiceRegistryContainer.GetServiceRegistry();
            UserManager mgr = svcRegistry.Get<UserManager>();
            Database userDatabase = mgr.Database;
            return userDatabase;
        }
    }
}
