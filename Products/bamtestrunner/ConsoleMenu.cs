using Bam.Net.CommandLine;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net.Logging;
using Bam.Net.Testing.Integration;
using System.IO;
using System.Diagnostics;
using Bam.Net.Data;

namespace Bam.Net.Testing
{
    public partial class Program
    {
        protected static string OpenCover
        {
            get
            {
                return "/bam/bot/tools/OpenCover/OpenCover.Console.exe";
            }
        }

        protected static string OutputRoot
        {
            get
            {
                return "/bam/bot/tests/";
            }
        }

        [ConsoleAction("TestsWithCoverage", "Run tests with coverage using opencover.console")]
        public static void RunTestsWithCoverage()
        {
            Process process = Process.GetCurrentProcess();
            FileInfo main = new FileInfo(process.MainModule.FileName);
            string testType = "Unit";
            if (Arguments.Contains("type"))
            {
                testType = Arguments["type"];
            }
            if (!testType.Equals("Unit") && !testType.Equals("Integration"))
            {
                OutLineFormat("Invalid test type specified: {0}", testType);
                Exit(-1);
            }
            string testReportHost = GetArgument("testReportHost", "What server/hostname should tests report to?");
            string testReportPort = GetArgument("testReportPort", "What port is the test report service listening on?");
            string tag = string.Empty;
            OutLine("Checking for commit file");
            string commitFile = Path.Combine(main.Directory.FullName, "commit");
            if (File.Exists(commitFile))
            {
                OutLine("commit file found; reading commit hash to use as tag");
                tag = File.ReadAllText(commitFile);
            }
            if (string.IsNullOrEmpty(tag))
            {
                tag = GetArgument("tag", "Enter a tag to use to identify test results");
            }
            DirectoryInfo outputDirectory = EnsureOutputDirectories(tag);
            FileInfo[] testAssemblies = GetTestFiles(GetTestDirectory());
            int pageSize = Environment.ProcessorCount < 4 ? 4 : Environment.ProcessorCount;
            List<List<FileInfo>> allPages = new List<List<FileInfo>>();
            int currentItem = 0;
            allPages.Add(new List<FileInfo>());
            foreach(FileInfo testFile in testAssemblies)
            {
                if(currentItem == pageSize)
                {
                    currentItem = 0;
                    allPages.Add(new List<FileInfo>());
                }
                allPages[allPages.Count - 1].Add(testFile);
                currentItem++;
            }
            int pageNum = 1;
            string allPidsFile = Path.Combine(outputDirectory.FullName, "output", "pids.txt");
            foreach(List<FileInfo> page in allPages)
            {
                int fileNum = 1;
                Parallel.ForEach(page, (file) =>
                {
                    string xmlFile = Path.Combine(outputDirectory.FullName, "coverage", $"_{pageNum}_{fileNum}.xml");
                    string outputFile = Path.Combine(outputDirectory.FullName, "output", $"{Path.GetFileNameWithoutExtension(file.Name)}_output.txt");
                    string errorFile = Path.Combine(outputDirectory.FullName, "output", $"{Path.GetFileNameWithoutExtension(file.Name)}_error.txt");
                    string pidFile = Path.Combine(outputDirectory.FullName, "output", $"{Path.GetFileNameWithoutExtension(file.Name)}.pid");
                    string commandLine = $"{OpenCover} -target:\"{main.FullName}\" -targetargs:\"/{testType}Tests:{file.FullName} /testReportHost:{testReportHost} /testReportPort:{testReportPort} /tag:{tag}\" -register:user -filter:\"+[Bam.Net*]* -[*].Data.* -[*Test*].Tests.*\" -output:{xmlFile}";
                    ProcessOutput output = commandLine.Run(7200000); // timeout after 2 hours
                    output.StandardError.SafeWriteToFile(errorFile, true);
                    output.StandardOutput.SafeWriteToFile(outputFile, true);
                    try
                    {
                        OutLineFormat("Writing pid files for tracking: \r\n\t{0}\r\n\t{1}", ConsoleColor.Cyan, pidFile, allPidsFile);
                        string pid = output?.Process?.Id.ToString();
                        pid.SafeAppendToFile(pidFile);
                        pid.SafeAppendToFile(allPidsFile);
                    }
                    catch (Exception ex)
                    {
                        OutLineFormat("Failed to write pid files: {0}\r\n\t{1}\r\n\t{2}", ConsoleColor.Magenta, ex.Message, pidFile, allPidsFile);
                    }
                    ++fileNum;
                });
                ++pageNum;
            }
        }

        [ConsoleAction("UnitTests", "[path_to_test_assembly]", "Run unit tests in the specified assembly")]
        public static void RunUnitTestsInFile(string assemblyPath = null, string endDirectory = null)
        {
            assemblyPath = assemblyPath ?? Arguments["UnitTests"];
            endDirectory = endDirectory ?? Environment.CurrentDirectory;
            try
            {
                Setup();
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
                Setup();
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                IntegrationTestRunner.RunIntegrationTests(assembly);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static DirectoryInfo EnsureOutputDirectories(string tag)
        {
            OutLineFormat("Creating output directories as necessary: OutputRoot={0}, tag={1}", ConsoleColor.Cyan, OutputRoot, tag);
            DirectoryInfo outputDirectory = new DirectoryInfo(Path.Combine(OutputRoot, tag));
            OutLineFormat("Checking for output directory: {0}", ConsoleColor.Cyan, outputDirectory);
            if (!outputDirectory.Exists)
            {
                OutLineFormat("Directory doesn't exist, creating it: {0}", outputDirectory.FullName);
                outputDirectory.Create();
            }
            string coverageDir = Path.Combine(outputDirectory.FullName, "coverage");
            OutLineFormat("Checking for coverage directory: {0}", ConsoleColor.Cyan, coverageDir);
            if (!Directory.Exists(coverageDir))
            {
                OutLineFormat("Coverage directory doesn't exist, creating it: {0}", coverageDir);
                Directory.CreateDirectory(coverageDir);
            }

            return outputDirectory;
        }
    }
}
