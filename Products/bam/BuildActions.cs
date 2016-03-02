using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net;
using System.IO;

namespace bam
{
    [Serializable]
    public class BuildActions: CommandLineTestInterface
    {
        const string NugetReleaseDirectory = @"Z:\Workspace\NugetPackages\Push\";
        internal class AssemblyNameContainer
        {
            public string LibraryName { get; set; }
            public string Ext { get; set; }
        }

        [ConsoleAction("generateNugetScripts", "Generate copy commands for build process")]
        public static void GenerateNugetScripts()
        {
            string dllListPath = GetDllNameTextFile();
            string exeListPath = GetExeNameTextFile();
            string template = GetTemplateFilePath().SafeReadFile();
            string fileNameFormat = GetFileNameFormat();
            StringBuilder copyAllScript = new StringBuilder();
            copyAllScript.AppendLine("@echo on");
            copyAllScript.AppendLine("call copy_Bam.Net.Data.cmd %1");
            StringBuilder cleanScript = new StringBuilder();
            cleanScript.Append(@"@echo on
SET LIB=net45
SET VER=v4.5
SET NEXT=END

RMDIR /S /Q .\BuildOutput
RMDIR /S /Q ..\..\Products\BUILD");
            string packData = "nuget pack Bam.Net.Data\\Bam.Net.Data.nuspec";
            StringBuilder packScript = GetPackScriptStart();
            packScript.AppendLine(packData);
            StringBuilder packScriptDebug = GetPackScriptStart("Debug");
            packScriptDebug.AppendLine(packData);

            StringBuilder pushScript = new StringBuilder();
            pushScript.AppendLine("@echo on");
            pushScript.AppendLine($@"nuget push {NugetReleaseDirectory}\Push\BamToolkit.%1.nupkg");

            DirectoryInfo outputDir = new DirectoryInfo(GetOutputDirectory());
            using (StreamReader sr = new StreamReader(dllListPath))
            {
                while (!sr.EndOfStream)
                {
                    Append(template, fileNameFormat, copyAllScript, cleanScript, packScript, packScriptDebug, pushScript, outputDir, sr, "dll");
                }
            }
            using (StreamReader sr = new StreamReader(exeListPath))
            {
                while (!sr.EndOfStream)
                {
                    Append(template, fileNameFormat, copyAllScript, cleanScript, packScript, packScriptDebug, pushScript, outputDir, sr, "exe");
                }
            }

            packScript.AppendLine("call build_toolkit.cmd");
            packScriptDebug.AppendLine("call build_toolkit_debug.cmd");
            pushScript.Append(@"call git_commit_all.cmd %1
call git_tag_version.cmd %1");
            copyAllScript.ToString().SafeWriteToFile(Path.Combine(outputDir.FullName, "copy_all.cmd"), true);
            packScript.ToString().SafeWriteToFile(Path.Combine(outputDir.FullName, "pack.cmd"), true);
            packScriptDebug.ToString().SafeWriteToFile(Path.Combine(outputDir.FullName, "pack_debug.cmd"), true);
            pushScript.ToString().SafeWriteToFile(Path.Combine(outputDir.FullName, "push.cmd"), true);
            cleanScript.ToString().SafeWriteToFile(Path.Combine(outputDir.FullName, "clean.cmd"), true);
        }

        private static void Append(string template, string fileNameFormat, StringBuilder copyAllScript, StringBuilder cleanScript, StringBuilder packScript, StringBuilder packScriptDebug, StringBuilder pushScript, DirectoryInfo outputDir, StreamReader sr, string ext)
        {
            string libraryName = sr.ReadLine().Trim();
            string fileName = string.Format(fileNameFormat, libraryName);
            string filePath = Path.Combine(outputDir.FullName, fileName);
            string fileContent = template.NamedFormat(new AssemblyNameContainer { LibraryName = libraryName, Ext = ext });
            fileContent.SafeWriteToFile(filePath, true);
            copyAllScript.AppendLine($"call {fileName} %1");
            string packLine = $"nuget pack {libraryName}\\{libraryName}.nuspec";
            packScript.AppendLine(packLine);
            packScriptDebug.AppendLine(packLine);
            pushScript.AppendLine($"nuget push {NugetReleaseDirectory}{libraryName}.%1.nupkg");

            cleanScript.AppendLine($@"RMDIR /S /Q ..\..\{libraryName}\obj\");
            cleanScript.AppendLine($@"del /F /Q .\{libraryName}\lib\%LIB%\*");
        }

        private static StringBuilder GetPackScriptStart(string config = "Release")
        {
            StringBuilder packScript = new StringBuilder();
            packScript.AppendLine("@echo on");
            packScript.AppendLine($"call copy_all.cmd {config}");
            packScript.AppendLine();
            return packScript;
        }

        private static string GetDllNameTextFile()
        {
            string value = Arguments["dnf"].Or(Arguments["dllNamesFile"]);
            return PromptIfNullOrEmpty(value, "Please enter the path to the dll names file");
        }

        private static string GetExeNameTextFile()
        {
            string value = Arguments["enf"].Or(Arguments["exeNamesFile"]);
            return PromptIfNullOrEmpty(value, "Please enter the path to the dll names file");
        }

        private static string GetTemplateFilePath()
        {
            string value = Arguments["tf"].Or(Arguments["templateFile"]);
            return PromptIfNullOrEmpty(value, "Please enter the path to the template file");
        }

        private static string GetFileNameFormat()
        {
            return Arguments["fnf"].Or(Arguments["fileNameFormat"]).Or("copy_{0}.cmd");
        }

        private static string GetOutputDirectory()
        {
            return Arguments["od"].Or(Arguments["outputDir"]).Or(".");
        }
        
        private static string PromptIfNullOrEmpty(string value, string prompt)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Prompt(prompt);
            }
            return value;
        }
    }
}
