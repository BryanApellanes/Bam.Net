/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.Automation.Testing;
using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.Testing.Integration;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Bam.Net.Testing
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        private const string _exitOnFailure = "exitOnFailure";
        private const string _programName = "bamtestrunner";
        
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
            #endregion
            AddValidArgument("search", false, description: "The search pattern to use to locate test assemblies");
            AddValidArgument("dir", false, description: "The directory to look for test assemblies in");
            AddValidArgument("debug", true, description: "If specified, the runner will pause to allow for a debugger to be attached to the process");
            AddValidArgument("data", false, description: "The path to save the results to, default is the current directory if not specified");
            AddValidArgument("dataPrefix", true, description: "The file prefix for the sqlite data file or 'BamTests' if not specified");
            AddValidArgument("type", false, description: "The type of tests to run [Unit | Integration], default is unit.");
            AddValidArgument("testReportHost", false, description: "The hostname of the test report service");

            AddValidArgument(_exitOnFailure, true);
            AddSwitches(typeof(Program));

            TestAction = RunUnitTests;

            DefaultMethod = typeof(Program).GetMethod("Start");
        }
        
        public static void Start()
        {
            if (Arguments.Contains("debug"))
            {
                Pause("Attach the debugger now");
            }

            Enum.TryParse<TestType>(Arguments["type"].Or("Unit"), out TestType testType);

            Setup(out string startDirectory, out FileInfo[] files);

            switch (testType)
            {
                case TestType.Unit:
                    TestAction = RunUnitTests;
                    break;
                case TestType.Integration:
                    TestAction = RunIntegrationTests;
                    break;
            }
            TestAction(startDirectory, files);
        }

        protected static Action<string, FileInfo[]> TestAction { get; set; }

        protected static void RunIntegrationTests(string startDirectory, FileInfo[] files)
        {
            bool exceptionOccurred = false;
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fi = files[i];
                RunIntegrationTestsInFile(fi.FullName, startDirectory);
            }
            
            if (_failedCount > 0 || exceptionOccurred)
            {
                Exit(1);
            }
            else
            {
                Exit(0);
            }
        }
        static int? _failedCount;
        static int? _passedCount;
        protected static void RunUnitTests(string startDirectory, FileInfo[] files)
        {
            bool exceptionOccurred = false;
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fi = files[i];
                RunUnitTestsInFile(fi.FullName, startDirectory);                
            }

            OutLineFormat("Passed: {0}", ConsoleColor.Green, _passedCount);
            OutLineFormat("Failed: {0}", ConsoleColor.Red, _failedCount);

            if (_failedCount > 0 || exceptionOccurred)
            {
                Exit(1);
            }
            else
            {
                Exit(0);
            }
        }

        [ConsoleAction("UnitTests", "[path_to_test_assembly]", "Run unit tests in the specified assembly")]
        public static void RunUnitTestsInFile(string assemblyPath = null, string endDirectory = null)
        {
            assemblyPath = assemblyPath ?? Arguments["UnitTests"];
            endDirectory = endDirectory ?? Environment.CurrentDirectory;
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                RunAllUnitTests(assembly, Log.Default, (o, a) => _passedCount++, (o, a) => _failedCount++);
                Environment.CurrentDirectory = endDirectory;
            }
            catch (Exception ex)
            {
                Environment.CurrentDirectory = endDirectory;
                HandleException(ex);
            }
        }

        [ConsoleAction("IntegrationTests", "[path_to_test_assembly]", "Run integration tests in the specified assemlby")]
        public static void RunIntegrationTestsInFile(string assemblyPath = null, string endDirectory = null)
        {
            assemblyPath = assemblyPath ?? Arguments["IntegrationTests"];
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                IntegrationTestRunner.RunIntegrationTests(assembly);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static void HandleException(Exception ex)
        {
            OutLineFormat("{0}: {1}", ConsoleColor.DarkRed, _programName, ex.Message);
            if (Arguments.Contains(_exitOnFailure))
            {
                Exit(1);
            }
        }
        
        private static void Setup(out string startDirectory, out FileInfo[] files)
        {
            string resultDirectory = Arguments.Contains("data") ? Arguments["data"] : ".";
            string filePrefix = Arguments["dataPrefix"].Or("BamTests");
            List<ITestRunListener<UnitTestMethod>> testRunListeners = new List<ITestRunListener<UnitTestMethod>> { new UnitTestRunListener(resultDirectory, $"{filePrefix}_{DateTime.Now.Date.ToString("MM_dd_yyyy")}") };

            string reportHost = Arguments["testReportHost"];
            if (string.IsNullOrEmpty(reportHost))
            {
                reportHost = DefaultConfiguration.GetAppSetting("TestReportHost", string.Empty);
            }
            if (!string.IsNullOrEmpty(reportHost))
            {
                testRunListeners.Add(new UnitTestRunReportingListener(reportHost));
            }

            GetUnitTestRunListeners = () => testRunListeners;
            startDirectory = Environment.CurrentDirectory;
            DirectoryInfo testDir = GetTestDirectory();
            Environment.CurrentDirectory = testDir.FullName;

            files = GetTestFiles(testDir);          
        }
        
        private static FileInfo[] GetTestFiles(DirectoryInfo testDir)
        {
            FileInfo[] files = null;
            if (Arguments.Contains("search"))
            {
                files = testDir.GetFiles(Arguments["search"]);
            }
            else
            {
                files = testDir.GetFiles();
            }
            return files;
        }
       
        private static DirectoryInfo GetTestDirectory()
        {
            DirectoryInfo testDir = new DirectoryInfo(".");
            if (Arguments.Contains("dir"))
            {
                string dir = Arguments["dir"];
                testDir = new DirectoryInfo(dir);
                if (!testDir.Exists)
                {
                    OutLineFormat("The specified directory ({0}) was not found", ConsoleColor.Red, dir);
                    Exit(1);
                }
            }
            return testDir;
        }
    }

}
