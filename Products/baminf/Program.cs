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
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;

namespace Bam.Net.Automation
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            PreInit();
            IsolateMethodCalls = false;
            AddValidArgument("t", true);
            DefaultMethod = typeof(Program).GetMethod("Start");

            Initialize(args);
        }

        public static void PreInit()
        {
            AddValidArgument("sai", true, description: "Set assembly information");
            AddValidArgument("baminfo.json", false, description: "Specify the path to the baminfo.json file (used to update nuspec files)");
            AddValidArgument("v", false, description: "Set version", valueExample: "1.0.0");
            AddValidArgument("root", "The root of the source tree");
            AddValidArgument("nuspecRoot", "The root directory to search for nuspec files");
            AddValidArgument("aip", false, addAcronym: false, description: "The path to the aip (Advanced Installer Project) file");
            AddValidArgument("smsiv", true, addAcronym: false, description: "Set msi version in aip (Advanced Installer Project) file");
            AddValidArgument("pause", true, addAcronym: false, description: "pause before exiting, only valid if command line switches are specified");

            AddBuildArguments();
            AddSwitches(typeof(BuildActions));
        }

        private static void AddBuildArguments()
        {
            AddValidArgument("mergeDllNamesFile", false, addAcronym: true, description: "generateBamDotExeScript: The path to the text file containing the names of the dlls to merge");
            AddValidArgument("dllNamesFile", false, addAcronym: true, description: "generateNugetScripts: The path to the text file containing the names of the dlls");
            AddValidArgument("exeNamesFile", false, addAcronym: true, description: "generateNugetScripts: The path to the text file containing the names of the exes");
            AddValidArgument("templateFile", false, addAcronym: true, description: "generateNugetScripts: The path to the template file to use");
            AddValidArgument("fileNameFormat", false, addAcronym: true, description: "generateNugetScripts: The file name format for generated files: default is copy_{0}.cmd, where 0 is the name of the library in dllNamesFile");
            AddValidArgument("outputDir", false, addAcronym: true, description: "generateNugetScripts: The directory to write files to");
            AddValidArgument("setReleaseNotes", true, addAcronym: true, description: "Set the release notes for each project by reading the git history");
        }
        #region do not modify
        public static void Start()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.StartLoggingThread();
            if (ExecuteSwitches(Arguments, typeof(BuildActions), false, logger))
            {
                logger.BlockUntilEventQueueIsEmpty();
                if (Arguments.Contains("pause"))
                {
                    Pause();
                }
            }
            else
            {
                Interactive();
            }
        }
        #endregion
    }
}
