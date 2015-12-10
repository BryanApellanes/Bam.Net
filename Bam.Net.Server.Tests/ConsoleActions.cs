/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Ionic.Zip;
using Bam.Net.Logging;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Server;

namespace Bam.Net.Server.Tests
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        [ConsoleAction("Elevate test")]
        public void ElevateTest()
        {
            if (!WeHaveAdminRights())
            {
                Elevate();
            }
        }
        [ConsoleAction]
        public void ListServerEventsAndTypes()
        {
            string methodFormat = "public virtual void {MethodName}({Arguments}){{}}\r\n\r\n";
            typeof(BamServer).GetEvents().Each(ei =>
            {
                string arguments = ei.EventHandlerType.GetGenericArguments().Select(t => string.Format("{0} {1}", t.Name, t.Name.CamelCase())).ToArray().ToDelimited(s => s);
                var args = new { MethodName = ei.Name, Arguments = arguments };
                methodFormat.NamedFormat(args).SafeAppendToFile(".\\tmp.txt");
            });
            "notepad .\\tmp.txt".Run();
        }

        [ConsoleAction("Test TcpServer")]
        public void TestTcpServer()
        {
            TcpServer tcpServer = new TcpServer(5, new ConsoleLogger());

            tcpServer.Start();
            tcpServer.ProcessRequest = (context) =>
            {
                List<byte> data = new List<byte>();
                context.RequestData.Each(b =>
                {
                    data.Add(b);
                });
                byte[] echo = data.ToArray();
                context.ResponseStream.Write(echo, 0, echo.Length);
            };
            Pause("Tcp server started, press any key to quit");
            tcpServer.Stop();
        }

        [ConsoleAction("Test TcpClient")]
        public void TestTcpClient()
        {
            TcpClient client = new TcpClient(Dns.GetHostName(), 8888);
            client.Client.ReceiveTimeout = 10000;
            NetworkStream stream = client.GetStream();  
            string input = string.Empty;
            
            while (!input.Equals("q"))
            {
                input = Prompt("Type something (q to quit)");
                byte[] inputData = Encoding.UTF8.GetBytes(input);
                byte[] readBuffer = new byte[64];
              
                MemoryStream memStream = new MemoryStream();
                int bytesRead = 0;
                OutLineFormat("Sending: {0}", input);
                stream.Write(inputData, 0, inputData.Length);

                do
                {
                    try
                    {
                        bytesRead = stream.Read(readBuffer, 0, readBuffer.Length);
                        memStream.Write(readBuffer, 0, bytesRead);
                    }
                    catch (Exception ex)
                    {
                        OutLine(ex.Message);
                    }

                } while (bytesRead > 0);

                OutLine(Encoding.UTF8.GetString(memStream.ToArray()));

            }

            stream.Close();
            client.Close();
        }

        static Dictionary<string, BamServer> _servers;        
        private static Dictionary<string, BamServer> Servers
        {
            get
            {
                if (_servers == null)
                {
                    _servers = new Dictionary<string, BamServer>();
                }

                return _servers;
            }
        }

        internal static BamServer CreateServer(int port, string rootDir = "", string name = "")
        {
            BamServer server = new BamServer(BamConf.Load());
            if (string.IsNullOrEmpty(rootDir))
            {
                rootDir = ".\\".RandomLetters(5);
            }
            server.ContentRoot = rootDir;
            //server.Port = port;

            if (string.IsNullOrEmpty(name) || Servers.ContainsKey(name))
            {
                int num = 1;
                if (string.IsNullOrEmpty(name))
                {
                    name = "Server";
                }
                string format = "{0}_{1}";
                name = format._Format(name, num);
                while(Servers.ContainsKey(name))
                {
                    num++;
                    name = format._Format(name, num);
                }
            }

            Servers[name] = server;
            return server;
        }

		[ConsoleAction("List server events")]
		public void ListServerEvents()
		{
			string fileName = ".\\temp.txt";
			if (File.Exists(fileName))
			{
				File.Delete(fileName);
			}
			EventInfo[] events = typeof(BamServer).GetEvents();
			events.Each(ev =>
			{
				OutLineFormat("{0}", ConsoleColor.Cyan, ev.Name);
				ev.Name.SafeAppendToFile(fileName);
				"\r\n".SafeAppendToFile(fileName);
			});

			"notepad {0}"._Format(fileName).Run();
		}

        [ConsoleAction("Start server")]
        public void StartServer()
        {
            string rootDir = BamHappyPrompt("Enter the name of the root directory to create\r\n");
            if (Directory.Exists(rootDir))
            {
                bool delete = ConfirmFormat("Directory {0} already exists, reinitialize?\r\n [y][N]", ConsoleColor.Cyan, false, rootDir);
                if (delete)
                {
                    OutFormat("Attempting to delete directory {0}\r\n", ConsoleColor.Cyan, rootDir);
                    Directory.Delete(rootDir, true);
                    OutFormat("The directory {0} was deleted and will be recreated when the server is started\r\n", ConsoleColor.Yellow, rootDir);
                }
            }

            string portString = BamHappyPrompt("Enter the port number to listen on\r\n");
            int port = 8080;
            int.TryParse(portString, out port);

            BamServer server = CreateServer(port, rootDir, rootDir);
            BamConf conf = server.GetCurrentConf();
            conf.GenerateDao = true;            
            server.SetConf(conf);
            server.SaveConf();
            
            _startTrap = new AutoResetEvent(false);
            _stopTrap = new AutoResetEvent(false);
            OutLine("Starting Server ", ConsoleColor.Cyan);
            server.Started += (s) =>
            {
                _startTrap.Set();
            };
            server.Stopped += (s) =>
            {
                _stopTrap.Set();
            };

            server.Start();            
            CommandLoop();
            server.Stop();

            _stopTrap.WaitOne();
            Thread.Sleep(2000);
        }

        AutoResetEvent _startTrap;
        AutoResetEvent _stopTrap;

        string[] _quits = new string[] { "q", "quit", "bye", "exit" };
        private void CommandLoop()
        {
            _startTrap.WaitOne();

            Thread.Sleep(3000);
            OutLine("Now listening for commands, logs will continue to output to this window", ConsoleColor.Cyan);
            OutLine("Type ? or help for command list", ConsoleColor.Yellow);
            string command = BamHappyPrompt();
            while (!_quits.Contains(command))
            {
                try
                {
                    ParseCommand(command);

                    command = BamHappyPrompt();
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    string stack = ex.StackTrace ?? "None";
                    OutFormat("Exception:\r\n{0}\r\nStack:\r\n{1}", ConsoleColor.Red, msg, stack);
                }
            }
        }

        private static void ParseCommand(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string[] chunks = input.DelimitSplit(" ");
                string command = chunks[0];
                StringBuilder argBuilder = new StringBuilder();
                if (chunks.Length > 1)
                {
                    chunks.Rest(1, s =>
                    {
                        argBuilder.AppendFormat("{0} ", s);
                    });
                }

                if (CommandActions.ContainsKey(command))
                {
                    TryAction(CommandActions[command], argBuilder.ToString().Trim());
                }
                else
                {
                    OutLine("Command not supported", ConsoleColor.Red);
                }
            }
        }

        private string BamHappyPrompt(string message = "")
        {
            return Prompt(message, " B) ", ConsoleColor.Cyan);
        }


        #region COMMAND ACTIONs handled here
        static List<ILogger> _addedLoggers;
        static Dictionary<string, Action<string>> _commandActions;
        static object _commandActionsLock = new object();
        private static Dictionary<string, Action<string>> CommandActions
        {
            get
            {
                return _commandActionsLock.DoubleCheckLock(ref _commandActions, () =>
                {
                    _addedLoggers = new List<ILogger>();
                    Dictionary<string, Action<string>> commands = new Dictionary<string, Action<string>>();
                    commands.Add("run", (args) =>
                    {
                        ProcessOutput output = args.Run();
                        OutFormat("Exit Code {0}\r\n", ConsoleColor.Yellow, output.ExitCode);
                        Out("Output (if any):\r\n", ConsoleColor.Cyan);
                        Out(output.StandardOutput, ConsoleColor.White);
                        Out("Error Output (if any):\r\n", ConsoleColor.Yellow);
                        Out(output.StandardError, ConsoleColor.Red);
                        OutLine("\r\nNo .Net exception was thrown", ConsoleColor.Cyan);
                    });

                    commands.Add("show", (args) =>
                    {
                        OutLine("Servers:", ConsoleColor.Cyan);
                        Servers.Keys.Each(server =>
                        {
                            OutLine(server, ConsoleColor.Green);
                        });
                    });

                    commands.Add("restart", (serverName) =>
                    {
                        if (IsValidServerName(serverName))
                        {
                            OutLineFormat("Restarting server ({0})", ConsoleColor.Yellow, serverName);
                            BamServer server = Servers[serverName];
                            server.Restart();
                            Thread.Sleep(1500);
                            OutLineFormat("Restarted server ({0})", ConsoleColor.Green, serverName);
                        }
                    });

                    commands.Add("show_config", (serverName) =>
                    {
                        if (IsValidServerName(serverName))
                        {
                            OutLineFormat("Config for {0}:", ConsoleColor.Cyan, serverName);
                            BamConf conf = Servers[serverName].GetCurrentConf();
                            OutLineFormat("{0}", ConsoleColor.Green, conf.PropertiesToString());
                        }
                    });
                    
                    commands.Add("pack_app", (args) =>
                    {
                        string[] split = args.DelimitSplit(" ");
                        if (split.Length < 2)
                        {
                            OutLine("Server and app name must be specified");                         
                        }
                        else 
                        {
                            string serverName = split[0];
                            string appName = split[1];
                            if (IsValidServerName(serverName))
                            {
                                string fullFileName = Path.Combine(Servers[serverName].ContentRoot, "bkg", "apps", "{0}.zip"._Format(appName));
                                PackApp(serverName, appName, fullFileName);
                            }
                        }                        
                    });

                    commands.Add("pack_all", (serverName) =>
                    {
                        if (IsValidServerName(serverName))
                        {
                            PackAll(serverName);
                        }
                    });

                    commands.Add("list_loggers", (serverName) =>
                    {
                        if (IsValidServerName(serverName))
                        {
                            BamServer server = Servers[serverName];
                            BamConf conf = server.GetCurrentConf(false);
                            OutLine("Available logger types:", ConsoleColor.Cyan);
                            conf.AvailableLoggers.Each(type =>
                            {
                                OutLine(type.Name, ConsoleColor.Green);
                            });
                        }
                    });

                    commands.Add("add_logger", (input) =>
                    {
                        string[] args = input.DelimitSplit(" ");
                        if (args.Length < 2)
                        {
                            OutLine("Server name and logger type must be specified in that order", ConsoleColor.Red);
                        }
                        else
                        {
                            string serverName = args[0];
                            string loggerTypeName = args[1];
                            if (IsValidServerName(serverName))
                            {
                                BamServer server = Servers[serverName];
                                BamConf conf = server.GetCurrentConf(false);
                                Type loggerType = conf.AvailableLoggers.Where(type => type.Name.Equals(loggerTypeName)).FirstOrDefault();
                                if (loggerType == null)
                                {
                                    OutLine("The specified logger was not found", ConsoleColor.Red);
                                }
                                else
                                {
                                    server.AddLogger((ILogger)loggerType.Construct());
                                }
                            }
                        }
                    });


                    Action<string> help = (args) =>
                    {
                        OutLine("Available commands:", ConsoleColor.Cyan);
                        commands.Keys.Each(key =>
                        {
                            OutLine(key, ConsoleColor.Green);
                        });
                    };
                    commands.Add("?", help);
                    commands.Add("help", help);

                    return commands;
                });
            }
        }

        private static void PackAll(string serverName)
        {
            BamServer server = Servers[serverName];
            OutLineFormat("Packing {0}", ConsoleColor.Cyan, server.ContentRoot);
            PackDir(server.ContentRoot, Path.Combine(server.ContentRoot, "bkg", "content.root"));
            OutLine("Pack done", ConsoleColor.Green);
        }

        private static void PackApp(string serverName, string appName, string saveTo)
        {
            BamServer server = Servers[serverName];
            BamConf conf = server.GetCurrentConf(false);
            AppConf appConf = conf[appName];
            string root = appConf.AppRoot.Root;
            OutLineFormat("Packing {0}", ConsoleColor.Cyan, root);
            PackDir(appConf.AppRoot.Root, saveTo);
            OutLine("Pack done", ConsoleColor.Green);
        }
        #endregion

        private static bool IsValidServerName(string serverName)
        {
            bool result = true;
            if (string.IsNullOrEmpty(serverName))
            {
                OutLineFormat("Server name was not specified", ConsoleColor.Red);
                result = false;
            }
            else if (!Servers.ContainsKey(serverName))
            {
                OutLineFormat("Specified server name ({0}) was not found", ConsoleColor.Red, serverName);
                result = false;
            }

            return result;
        }

        private static void TryAction(Action<string> action, string args = "")
        {
            try
            {
                action(args);
            }
            catch (Exception ex)
            {
                OutLineFormat(".Net exception was thrown:\r\n{0}\r\n", ConsoleColor.Red, ex.Message);
                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    OutLineFormat("{0}", ConsoleColor.Magenta, ex.StackTrace);
                }
            }
        }

        [ConsoleAction("Repack app and content")]
        public static void RepackAppAndContent()
        {
            RepackApp();
            RepackContent();
        }

        [ConsoleAction("Re-pack content folder")]
        public static void RepackContent()
        {
            string contentPath = File.ReadAllText(".\\contentpath").Trim();
            ZipFile file = new ZipFile();
            DirectoryInfo contentDir = new DirectoryInfo(contentPath);
            DirectoryInfo[] content = contentDir.GetDirectories();
            content.Each(dir =>
            {
                if (!dir.Name.StartsWith("."))
                {
                    file.AddDirectory(dir.FullName, dir.Name);
                }
            });
            DirectoryInfo contentParent = contentDir.Parent;
            string fileName = Path.Combine(contentParent.FullName, "content.root");
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            file.Save(fileName);
        }

        [ConsoleAction("Re-pack app folder")]
        public static void RepackApp()
        {
            string appPath = File.ReadAllText(".\\apppath").Trim();
            DirectoryInfo contentParent = new DirectoryInfo(appPath).Parent;
            PackDir(appPath, Path.Combine(contentParent.FullName, "app.base"));
        }
        
        private static void PackDir(string appPath, string saveTo)
        {
            ZipFile zipFile = new ZipFile();
            DirectoryInfo appDir = new DirectoryInfo(appPath);
            DirectoryInfo[] dirs = appDir.GetDirectories();
            dirs.Each(dir =>
            {
                if (!dir.Name.StartsWith("."))
                {
                    zipFile.AddDirectory(dir.FullName, dir.Name);
                }
            });

            FileInfo[] files = appDir.GetFiles();
            files.Each(f =>
            {
                if (!f.Name.StartsWith("."))
                {
                    zipFile.AddFile(f.FullName, "");
                }
            });

            if (File.Exists(saveTo))
            {
                File.Delete(saveTo);
            }
            zipFile.Save(saveTo);
        }


    }
}
