/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using System.IO;

namespace masuco
{
    class Program: CommandLineTestInterface
    {
        static void Main(string[] args)
        {
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

            AddValidArgument("run", true, "Run the copy process");
            AddValidArgument("conf", false, "Path to the config file to use", "<.\\masuco.json>");

            DefaultMethod = typeof(Program).GetMethod("Start");
            Expect.IsNotNull(DefaultMethod);
            #endregion
        }

        public static void Start()
        {
            if (Arguments.Contains("run"))
            {
                if (Arguments.Contains("conf"))
                {
                    _configFile = Arguments["conf"];
                }

                CopyMatchingSubfolders();
            }
            else
            {
                MainMenu("(Ma)tching (Su)bfolder (Co)py");
            }
        }

        static string _configFile = ".\\masuco.json";
        [ConsoleAction("Copy matching subfolders; \r\n\tWhere <source> has subfolders \r\n\tmatching in name to subfolders in <destination>")]
        public static void CopyMatchingSubfolders()
        {
            if (!File.Exists(_configFile))
            {
                OutLineFormat("Config file {0} not found.", ConsoleColor.Yellow, _configFile);
                OutLine("Starting configuration...", ConsoleColor.Cyan);
                Configure();
            }
            else
            {
                MasucoConfig config = _configFile.SafeReadFile().FromJson<MasucoConfig>();
                DirectoryInfo sourceDir = new DirectoryInfo(config.Source);
                DirectoryInfo destinationDir = new DirectoryInfo(config.Destination);                

                if (Exists(sourceDir) && Exists(destinationDir))
                {
                    DirectoryInfo[] dirsToMatch = destinationDir.GetDirectories();
                    for (int i = 0; i < dirsToMatch.Length; i++)
                    {
                        DirectoryInfo currentDirToMatch = dirsToMatch[i];
                        string sourcePath = Path.Combine(sourceDir.FullName, currentDirToMatch.Name);
                        DirectoryInfo currentSource = new DirectoryInfo(sourcePath);
                        if (currentSource.Exists)
                        {
                            currentSource.Copy(currentDirToMatch.FullName, true,
                                (srcFile, destFile) =>
                                {
                                    FileInfo d = new FileInfo(destFile);
                                    if (d.Exists)
                                    {
                                        if (d.IsReadOnly)
                                        {
                                            d.IsReadOnly = false;
                                        }
                                        d.Delete();
                                        d.Refresh();
                                    }

                                    OutLineFormat("Copying {0} -> {1}", ConsoleColor.Yellow, srcFile, destFile);
                                });
                        }
                    }
                }
            }
        }

        [ConsoleAction("Configure")]
        public static void Configure()
        {
            string source = Prompt("Enter the path to the source folder");
            string destination = Prompt("Enter the path to the destination folder");
            MasucoConfig config = new MasucoConfig();
            config.Source = source;
            config.Destination = destination;
            config.ToJsonFile(_configFile);
            if (Confirm("Start copying?"))
            {
                CopyMatchingSubfolders();
            }
        }

        [ConsoleAction("Show configuration")]
        public static void ShowConfiguration()
        {
            if (!File.Exists(_configFile))
            {
                OutLineFormat("Config file {0} not found.", ConsoleColor.Yellow, _configFile);
                OutLine("Starting configuration...", ConsoleColor.Cyan);
                Configure();
            }
            else
            {
                MasucoConfig config = _configFile.SafeReadFile().FromJson<MasucoConfig>();
                OutLineFormat("Source: {0}", ConsoleColor.Cyan, config.Source);
                OutLineFormat("Destination: {0}", ConsoleColor.Cyan, config.Destination);
            }
        }

        private static bool Exists(DirectoryInfo dir)
        {
            bool result = dir.Exists;
            if (!result)
            {
                OutLineFormat("{0} was not found", ConsoleColor.Red, dir.FullName);
            }

            return result;
        }
    }
}
