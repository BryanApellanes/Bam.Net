using Bam.Net.Automation.Testing;
using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.Razor;
using Bam.Net.Testing.Integration;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Bam.Net.Testing
{
    [Serializable]
    public partial class Program : CommandLineTestInterface
    {
        private const string _exitOnFailure = "exitOnFailure";
        private const string _programName = "bamtestrunner";
        
        static void Main(string[] args)
        {
            IsolateMethodCalls = false;
            RazorBaseTemplate.DefaultInspector = (s) => { OutLineFormat("Parsed razor: {0} ...", s.ReadUntil('\n')); };
            PreInit();
            Initialize(args);
            ConsoleLogger logger = new ConsoleLogger() { AddDetails = false, ShowTime = true, ApplicationName = "bamtestrunnter", UseColors = true };
            logger.StartLoggingThread();
            if(ExecuteSwitches(Arguments, typeof(Program), false, logger))
            {
                return;
            }
            else
            {
                Start();
            }
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
            AddValidArgument("search", false, description: "The search pattern to use to locate test assemblies, the default is *Tests.* if not specified.");
            AddValidArgument("testFile", false, description: "The path to the assembly containing tests to run");
            AddValidArgument("dir", false, description: "The directory to look for test assemblies in");
            AddValidArgument("debug", true, description: "If specified, the runner will pause to allow for a debugger to be attached to the process");
            AddValidArgument("data", false, description: "The path to save the results to, default is the current directory if not specified");
            AddValidArgument("dataPrefix", true, description: "The file prefix for the sqlite data file or 'BamTests' if not specified");
            AddValidArgument("type", false, description: "The type of tests to run [Unit | Integration], default is Unit.");
            AddValidArgument("testReportHost", false, description: "The hostname of the test report service");
            AddValidArgument("testReportPort", false, description: "The port that the test report service is listening on");

            AddValidArgument(_exitOnFailure, true);
            AddSwitches(typeof(Program));

            TestAction = RunUnitTests;
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
        /// <summary>
        /// Runs the unit tests found in the specified files.
        /// </summary>
        /// <param name="startDirectory">The start directory.</param>
        /// <param name="files">The files.</param>
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

        private static void HandleException(Exception ex)
        {
            OutLineFormat("{0}: {1}", ConsoleColor.DarkRed, _programName, ex.Message);
            OutLineFormat("Stack: {0}", ConsoleColor.DarkRed, ex.StackTrace);
            if (Arguments.Contains(_exitOnFailure))
            {
                Exit(1);
            }
        }
        
        private static void Setup()
        {
            Setup(out string ignoreDirectory, out FileInfo[] ignoreFiles);
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
                if(!int.TryParse(Arguments["testReportPort"].Or(DefaultConfiguration.GetAppSetting("TestReportPort", string.Empty)).Or("80"), out int port))
                {
                    port = 80;
                }
                testRunListeners.Add(new UnitTestRunReportingListener(reportHost, port));
            }

            GetUnitTestRunListeners = () => testRunListeners;
            startDirectory = Environment.CurrentDirectory;
            DirectoryInfo testDir = GetTestDirectory();
            Environment.CurrentDirectory = testDir.FullName;

            files = GetTestFiles(testDir);          
        }

        private static FileInfo[] GetTestFiles(DirectoryInfo testDir)
        {
            OutLineFormat("Getting test files from: {0}", ConsoleColor.DarkCyan, testDir.FullName);
            FileInfo[] files = new FileInfo[] { };
            if (Arguments.Contains("search"))
            {
                OutLine("search switch specified", ConsoleColor.DarkCyan);
                string search = Arguments["search"];
                OutLineFormat("/search switch specified: {0}", ConsoleColor.DarkCyan, search);
                files = testDir.GetFiles(search);
            }
            else if (Arguments.Contains("testFile"))
            {
                OutLine("testFile switch specified", ConsoleColor.DarkCyan);
                string testFile = Arguments["testFile"];
                OutLineFormat("/testFile switch specified: {0}", ConsoleColor.DarkCyan, testFile);
                FileInfo file = new FileInfo(testFile);
                if (!file.Exists)
                {
                    throw new InvalidOperationException(string.Format("The specified test file was not found: {0}", file.FullName));
                }
                files = new FileInfo[] { file };
            }
            else
            {
                OutLineFormat("Getting default tests");
                List<FileInfo> tmp = new List<FileInfo>();
                tmp.AddRange(testDir.GetFiles("*Tests.exe"));
                tmp.AddRange(testDir.GetFiles("*Tests.dll"));
                files = tmp.ToArray();
            }
            OutLineFormat("retrieved ({0}) files", files.Length);
            return files;
        }
       
        private static DirectoryInfo GetTestDirectory()
        {
            OutLine("Getting test directory");
            DirectoryInfo testDir = new DirectoryInfo(".");
            if (Arguments.Contains("dir"))
            {
                string dir = Arguments["dir"];
                testDir = new DirectoryInfo(dir);
                if (!testDir.Exists)
                {
                    OutLineFormat("The specified directory ({0}) was not found", ConsoleColor.Magenta, dir);
                    Exit(1);
                }
            }
            OutLineFormat("Got test directory: {0}", testDir.FullName);
            return testDir;
        }
    }

}
