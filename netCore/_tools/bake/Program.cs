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
            AddValidArgument("pause", true, addAcronym: false, description: "pause before exiting, only valid if command line switches are specified");
            AddBuildArguments();
            AddSwitches(typeof(BuildActions));
        }

        private static void AddBuildArguments()
        {
            AddValidArgument("v", false, description: "Set version", valueExample: "1.0.0");
            AddValidArgument("root", "The root of the source tree or the location of binaries when baking dev nuget packages");
            AddValidArgument("ILMergePath", false, addAcronym: true, description: "/bam: The path to the ILMerge.exe file.");
            AddValidArgument("MsBuildPath", false, addAcronym: false, description: "/release: The path to msbuild.exe");
            AddValidArgument("Solution", false, addAcronym: false, description: "/release: The repo root relative path to the solution file to build.");
            AddValidArgument("ReleaseFolder", false, addAcronym: false, description: "/release: The path to build to or the path where assemblies have already been built.");
            AddValidArgument("NugetPath", false, addAcronym: true, description: "/nuget: The path to the nuget.exe file");
            AddValidArgument("OutputDirectory", false, addAcronym: true, description: "/nuget, bam: The directory to write files or packages to.");
            AddValidArgument("prefix", true, addAcronym: false, description: "/nuget:  When initializing nuspec files, the prefix that a project name must start with for it to recieve a nuspec file.  When publishing, the prefix that .nupkg files must start with for them to be published.");
            AddValidArgument("releaseNotesSince", false, addAcronym: false, description: "/nuget: Optional, if specified update release notes since the specified version instead of since the latest release.  The version is specified in the format [major].[minor].[patch] with no leading or trailing characters.");
            AddValidArgument("major", true, addAcronym: false, description: "/release: Increment the major version number.");
            AddValidArgument("minor", true, addAcronym: false, description: "/release: Increment the minor version number.");
            AddValidArgument("patch", true, addAcronym: false, description: "/release: Increment the patch version number.");
            AddValidArgument("suffix", false, addAcronym: false, description: "/dev: The build version value.");
            AddValidArgument("WixMergeModule", false, addAcronym: false, description: "/msi, release: The source root relative path to the directory containing the wix file to update.");
            AddValidArgument("WixMsi", false, addAcronym: false, description: "/msi, release: The path to the directory where the wix msi project file (.wixproj) is found.");
            AddValidArgument("WixOutput", false, addAcronym: false, description: "/msi, release: The path wix will use for output.");
            AddValidArgument("PackagesDirectory", false, addAcronym: false, description: "/release: The directory to restore nuget packages to.");
            AddValidArgument("host", false, addAcronym: false, description: "/deploy: specify the host to process, if ommitted all hosts are processed.");
            AddValidArgument("tag", false, addAcronym: false, description: "/test: specify the tag to associate with the test run.");
            AddValidArgument("branch", false, addAcronym: false, description: "/build: specify a branch to build overriding what is defined in the specified build config.");
        }
        #region do not modify
        public static void Start()
        {
            ConsoleLogger logger = new ConsoleLogger
            {
                AddDetails = false
            };
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
