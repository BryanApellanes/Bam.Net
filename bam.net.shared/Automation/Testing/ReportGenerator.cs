using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Automation.Testing
{
    public class ReportGenerator
    {
        static ReportGenerator()
        {
            Path = "C:\\bam\\tools\\ReportGenerator\\ReportGenerator.exe";
        }

        public static string Path { get; set; }

        public static ProcessOutput Run(string outputDirectoryPath)
        {
            DirectoryInfo coverageFolder = new DirectoryInfo(System.IO.Path.Combine(Paths.Tests, TestConstants.CoverageXmlFolder));
            if (!coverageFolder.Exists)
            {
                coverageFolder.Create();
            }
            string[] reportXmlFilePaths = coverageFolder.GetFiles("*_coverage.xml").Select(fi => fi.FullName).ToArray(); //bamtestrunner.exe will output xml files to this folder 
            return Run(outputDirectoryPath, reportXmlFilePaths, System.IO.Path.Combine(Paths.Tests, TestConstants.HistoryFolder));
        }

        public static ProcessOutput Run(string outputDirectoryPath, string[] reportXmlFilePaths, string historyDirectoryPath = null)
        {
            return Run((s) => Console.WriteLine("INFO: {0}", s), (e) => Console.WriteLine("ERR: {0}", e), reportXmlFilePaths, outputDirectoryPath, historyDirectoryPath);
        }

        public static ProcessOutput Run(Action<string> output, Action<string> error, string[] reportXmlFilePaths, string outputDirectoryPath, string historyDirectoryPath = null)
        {
            return GetCommand(outputDirectoryPath, historyDirectoryPath).Run(output, error, 120000);
        }

        public static ProcessOutput Run(string computerName, string outputDirectoryPath, string historyDirectoryPath = null)
        {
            return PsExec.Run(
                   computerName,
                   GetCommand(outputDirectoryPath, historyDirectoryPath),
                   (s) => Console.WriteLine(s),
                   (e) => Console.WriteLine(e),
                   600000
               );
        }

        private static string GetCommand(string outputDirectoryPath, string historyDirectoryPath = null)
        {
            DirectoryInfo coverageFolder = new DirectoryInfo(System.IO.Path.Combine(Paths.Tests, TestConstants.CoverageXmlFolder));
            string[] reportXmlFilePaths = coverageFolder.GetFiles("*_coverage.xml").Select(fi => fi.FullName).ToArray(); //bamtestrunner.exe will output xml files to this folder 
            historyDirectoryPath = historyDirectoryPath ?? System.IO.Path.Combine(Paths.Tests, "CoverageHistory");
            return $"{Path} -reports:{string.Join(";", reportXmlFilePaths)} -targetdir:{outputDirectoryPath} -historydir:{historyDirectoryPath}";
        }
    }
}
