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

namespace Bam.Net.Testing
{
    public partial class Program
    {
        protected static string OpenCover
        {
            get
            {
                return "C:\\bam\\bot\\tools\\OpenCover\\OpenCover.Console.exe";
            }
        }

        protected static string OutputRoot
        {
            get
            {
                return "C:\\bam\\bot\\tests\\";
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
            if(!testType.Equals("Unit") && !testType.Equals("Integration"))
            {
                OutLineFormat("Invalid test type specified: {0}", testType);
                Exit(-1);
            }
            string testReportHost = GetArgument("testReportHost", "What server/hostname should tests report to?");
            string testReportPort = GetArgument("testReportPort", "What port is the test report service listening on?");
            string tag = string.Empty;
            string commitFile = Path.Combine(main.Directory.FullName, "commit");
            if (File.Exists(commitFile))
            {
                tag = File.ReadAllText(commitFile);
            }
            FileInfo[] testAssemblies = GetTestFiles(GetTestDirectory());
            DirectoryInfo outputDirectory = new DirectoryInfo(Path.Combine(OutputRoot, tag));
            if (!outputDirectory.Exists)
            {
                outputDirectory.Create();
            }
            int fileNum = 1;            
            Parallel.ForEach(testAssemblies, (file) =>
            {
                string xmlFile = Path.Combine(outputDirectory.FullName, "coverage", $"_{fileNum}.xml");
                string outputFile = Path.Combine(outputDirectory.FullName, "output", $"{Path.GetFileNameWithoutExtension(file.Name)}_output.txt");
                string errorFile = Path.Combine(outputDirectory.FullName, "output", $"{Path.GetFileNameWithoutExtension(file.Name)}_error.txt");
                string commandLine = $"{OpenCover} -target:\"{main.FullName}\" -targetargs:\"/{testType}Tests:{file.FullName} /testReportHost:{testReportHost} /testReportPort:{testReportPort} /tag:{tag}\" -register -filter:\"+[Bam.Net *]* -[*].Data.* -[*Test*].Tests.*\" -output:{xmlFile}";
                ProcessOutput output = commandLine.Run(7200000); // timeout after 2 hours
                output.StandardError.SafeWriteToFile(outputFile);
                output.StandardOutput.SafeWriteToFile(errorFile);
            });            
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
    }
}
