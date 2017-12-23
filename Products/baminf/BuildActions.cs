using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net;
using System.IO;
using Bam.Net.Automation.AdvancedInstaller;
using Bam.Net.Automation.Nuget;
using Bam.Net.Automation;
using Bam.Net.Automation.SourceControl;

namespace Bam.Net.Automation
{
    [Serializable]
    public class BuildActions: CommandLineTestInterface
    {
        static string _nugetReleaseDirectory;
        public static string NugetReleaseDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_nugetReleaseDirectory))
                {
                    _nugetReleaseDirectory = ".\\nugetreleasedirectory.txt".SafeReadFile().Or(@"Z:\Workspace\NugetPackages\Push\");
                }
                return _nugetReleaseDirectory;
            }
        }

        static string _lib;
        public static string Lib
        {
            get
            {
                if (string.IsNullOrEmpty(_lib))
                {
                    _lib = ".\\lib.txt".SafeReadFile();
                }
                return _lib;
            }
        }

        static string _ver;
        public static string Ver
        {
            get
            {
                if(string.IsNullOrEmpty(_ver))
                {
                    _ver = ".\\ver.txt".SafeReadFile();
                }
                return _ver;
            }
        }
        internal class AssemblyNameContainer
        {
            public string NetLib { get; set; }
            public string NetVer { get; set; }
            /// <summary>
            /// The name of the assembly
            /// </summary>
            public string LibraryName { get; set; }
            /// <summary>
            /// The assembly extension, either exe or dll
            /// </summary>
            public string Ext { get; set; }
        }

        [ConsoleAction("generateBamDotExeScript", "Generate ILMerge script")]
        public static void GenerateBamDotExeScript()
        {
            string[] dllList = new FileInfo(GetMergeDllNameTextFile()).ReadAllText().DelimitSplit("\r", "\n");
            StringBuilder script = new StringBuilder($@"
@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB={Lib}
SET VER={Ver}
cd .\BuildOutput\%CONFIG%\{Ver}
md ..\..\..\BamDotExe\lib\%LIB%\
..\..\..\ilmerge.exe bam.exe");
            
            dllList.Each(dll =>
            {
                script.Append($" {dll}.dll");
            });
            string lib = $@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\{Ver}";
            script.AppendLine($" /closed /targetplatform:v4 /lib:\"{lib}\" /out:..\\..\\..\\BamDotExe\\lib\\%LIB%\\bam.exe");
            script.Append("cd ..\\..\\..\\");
            script.ToString().SafeWriteToFile("generate_bam_dot_exe.cmd", true);
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
            cleanScript.Append($@"@echo on
SET LIB={Lib}
SET VER={Ver}
SET NEXT=END

RMDIR /S /Q .\BuildOutput
RMDIR /S /Q ..\..\Products\BUILD
DEL /F /Q .\BamDotExe\lib\%LIB%\*");
            string packData = "nuget pack Bam.Net.Data\\Bam.Net.Data.nuspec";
            string packDotExe = "nuget pack BamDotExe\\BamDotExe.nuspec";
            StringBuilder packScript = GetPackScriptStart();
            packScript.AppendLine(packData);
            packScript.AppendLine(packDotExe);
            StringBuilder packScriptDebug = GetPackScriptStart("Debug");
            packScriptDebug.AppendLine(packData);
            packScriptDebug.AppendLine(packDotExe);

            StringBuilder pushScript = new StringBuilder();
            pushScript.AppendLine("@echo on");
            pushScript.AppendLine($@"nuget push -source nuget.org {NugetReleaseDirectory}BamToolkit.%1.nupkg");
            pushScript.AppendLine($@"nuget push -source nuget.org {NugetReleaseDirectory}BamDotExe.%1.nupkg");
            pushScript.AppendLine($@"nuget push -source nuget.org {NugetReleaseDirectory}Bam.Net.Data.%1.nupkg");

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
            string copyAllPath = Path.Combine(outputDir.FullName, "copy_all.cmd");
            string packPath = Path.Combine(outputDir.FullName, "pack.cmd");
            string packDebug = Path.Combine(outputDir.FullName, "pack_debug.cmd");
            string pushPath = Path.Combine(outputDir.FullName, "push.cmd");
            string cleanPath = Path.Combine(outputDir.FullName, "clean.cmd");

            copyAllScript.ToString().SafeWriteToFile(copyAllPath, true);
            OutLineFormat("Wrote file {0}", copyAllPath);

            packScript.ToString().SafeWriteToFile(packPath, true);
            OutLineFormat("Wrote file {0}", packPath);

            packScriptDebug.ToString().SafeWriteToFile(packDebug, true);
            OutLineFormat("Wrote file {0}", packDebug);

            pushScript.ToString().SafeWriteToFile(pushPath, true);
            OutLineFormat("Wrote file {0}", pushPath);

            cleanScript.ToString().SafeWriteToFile(cleanPath, true);
            OutLineFormat("Wrote file {0}", cleanPath);
        }

        [ConsoleAction("baminfo.json", "specify the path to the baminfo.json file to use")]
        public static void SetBamInfo()
        {
            string nuspecRoot = GetNuspecRoot();
            string bamInfoPath = (Arguments["baminfo.json"] ?? Prompt("Enter the path to the baminfo.json file"));
            string versionString = GetVersion();
            string srcRoot = GetSourceRoot();
            
            BamInfo info = bamInfoPath.FromJsonFile<BamInfo>();
            Out("*** baminfo.json ***", ConsoleColor.Cyan);
            OutLine(info.PropertiesToString(), ConsoleColor.Cyan);
            OutLine("***", ConsoleColor.Cyan);
            OutLineFormat("Updating version from {0} to {1}", ConsoleColor.Yellow, info.VersionString, versionString);
            info.VersionString = versionString;
            info.ToJsonFile(bamInfoPath);

            GitReleaseNotes miscReleaseNotes = GitReleaseNotes.MiscSinceLatestRelease(srcRoot);
            miscReleaseNotes.Summary = $"Version {versionString}";
            OutLineFormat("Updating release notes:\r\n{0}", ConsoleColor.DarkYellow, info.ReleaseNotes);
            info.ReleaseNotes = miscReleaseNotes.Value;
            info.ToJsonFile(bamInfoPath);
            string rootReleaseNotes = Path.Combine(srcRoot, "RELEASENOTES");
            miscReleaseNotes.Value.SafeWriteToFile(rootReleaseNotes, true);

            DirectoryInfo nuspecRootDir = new DirectoryInfo(nuspecRoot);
            FileInfo[] nuspecFiles = nuspecRootDir.GetFiles("*.nuspec", SearchOption.AllDirectories);
            foreach (FileInfo file in nuspecFiles)
            {
                NuspecFile nuspecFile = new NuspecFile(file.FullName)
                {
                    Authors = info.Authors,
                    Owners = info.Owners
                };
                GitReleaseNotes releaseNotes = GitReleaseNotes.SinceLatestRelease(nuspecFile.Id, srcRoot);
                if (!WriteReleaseNotes(srcRoot, releaseNotes, out string projectRoot))
                {
                    Warn("Unable to find project directory ({0}) to write release notes", projectRoot);
                }
                releaseNotes.Summary = $"Version {versionString}";
                nuspecFile.ReleaseNotes = releaseNotes.Value;
                nuspecFile.Copyright = "Copyright © {0} {1}"._Format(info.Owners, DateTime.UtcNow.Year);
                nuspecFile.LicenseUrl = info.LicenseUrl;
                nuspecFile.ProjectUrl = info.ProjectUrl;
                string buildNumber = !string.IsNullOrEmpty(info.BuildNumber) ? "-{0}"._Format(info.BuildNumber) : "";
                string patch = string.Format("{0}{1}", info.PatchVersion.ToString(), buildNumber);
                nuspecFile.Version.Major = info.MajorVersion.ToString();
                nuspecFile.Version.Minor = info.MinorVersion.ToString();
                nuspecFile.Version.Patch = patch;
                List<NugetPackageIdentifier> bamDependencies = new List<NugetPackageIdentifier>();
                if (nuspecFile.Dependencies != null)
                {
                    nuspecFile.Dependencies.Where(npi => npi.Id.StartsWith(typeof(Args).Namespace)).Each(npi =>
                    {
                        bamDependencies.Add(new NugetPackageIdentifier(npi.Id, info.VersionString));
                    });
                    nuspecFile.Dependencies = bamDependencies.ToArray();
                }
                nuspecFile.Save();
            }
        }

        [ConsoleAction("sai", "Set assembly info")]
        public static void SetAssemblyInfo()
        {
            GetParameters(out string srcRoot, out string version, out string nuspecRoot);

            DirectoryInfo srcRootDir = new DirectoryInfo(srcRoot);
            srcRootDir.GetFiles("AssemblyInfo.cs", SearchOption.AllDirectories).Each(infoFile =>
            {
                OutLineFormat("Writing assembly info into: {0}", ConsoleColor.Blue, infoFile.FullName);
                StringBuilder newContent = new StringBuilder();
                DirectoryInfo dotDot = infoFile.Directory.Parent;
                List<AssemblyAttributeInfo> attributeInfos = GetAssemblyAttributeInfos(dotDot.Name, version, nuspecRoot);
                using (StreamReader reader = new StreamReader(infoFile.FullName))
                {
                    while (!reader.EndOfStream)
                    {
                        string currentLine = reader.ReadLine();
                        foreach (AssemblyAttributeInfo attributeInfo in attributeInfos)
                        {
                            if (currentLine.StartsWith(attributeInfo.StartsWith))
                            {
                                currentLine = attributeInfo.AssemblyAttribute;
                                attributeInfo.WroteInfo = true;
                            }
                        }
                        newContent.AppendLine(currentLine);
                    }
                }
                foreach (AssemblyAttributeInfo attributeInfo in attributeInfos)
                {
                    if (!attributeInfo.WroteInfo)
                    {
                        newContent.AppendLine(attributeInfo.AssemblyAttribute);
                    }
                }
                newContent.ToString().SafeWriteToFile(infoFile.FullName, true);
            });
        }

        [ConsoleAction("smsiv", "Set msi version")]
        public static void SetMsiVersion()
        {
            string aipPath = GetAdvancedInstallerProjectFilePath();
            DOCUMENT aip = aipPath.FromXmlFile<DOCUMENT>();
            List<DOCUMENTCOMPONENTROW> rows = new List<DOCUMENTCOMPONENTROW>(aip.COMPONENT[0].ROW);
            int versionRow = rows.FindIndex(r => r.Property.Equals("ProductVersion"));
            DOCUMENTCOMPONENTROW row = rows[versionRow];
            row.Value = GetVersion();
            rows[versionRow] = row;
            aip.COMPONENT[0].ROW = rows.ToArray();
            aip.XmlSerialize(aipPath);
        }

        private static void GetParameters(out string srcRoot, out string version, out string nuspecRoot)
        {
            srcRoot = GetSourceRoot();
            version = GetVersion();
            nuspecRoot = GetNuspecRoot();
        }

        private static string GetSourceRoot()
        {
            return Arguments["root"] ?? Prompt("Please enter the root of the source tree");
        }

        private static string GetVersion()
        {
            return Arguments["v"] ?? Prompt("Please enter the version number");
        }

        private static string GetNuspecRoot()
        {
            return Arguments["nuspecRoot"] ?? Prompt("Please enter the root to search for nuspec files");
        }

        private static string GetAdvancedInstallerProjectFilePath()
        {
            return Arguments["aip"] ?? Prompt("Please enter the path to the aip (Advanced Installer Project) file");
        }
                
        private static List<AssemblyAttributeInfo> GetAssemblyAttributeInfos(string fileName, string version, string nuspecRoot)
        {
            List<AssemblyAttributeInfo> _assemblyAttributeInfos = new List<AssemblyAttributeInfo>();
            NuspecFile nuspecFile = GetNuspecFile(fileName, nuspecRoot);

            _assemblyAttributeInfos = new List<AssemblyAttributeInfo>();
            _assemblyAttributeInfos.Add(new AssemblyAttributeInfo { AttributeName = "AssemblyVersion", Value = version });
            _assemblyAttributeInfos.Add(new AssemblyAttributeInfo { AttributeName = "AssemblyFileVersion", Value = version });
            _assemblyAttributeInfos.Add(new AssemblyAttributeInfo { AttributeName = "AssemblyCompany", NuspecFile = nuspecFile, NuspecProperty = "Owners" });
            _assemblyAttributeInfos.Add(new AssemblyAttributeInfo { AttributeName = "AssemblyDescription", NuspecFile = nuspecFile, NuspecProperty = "Description" });

            return _assemblyAttributeInfos;
        }

        private static NuspecFile GetNuspecFile(string fileName, string nuspecRoot)
        {
            DirectoryInfo nuspecRootDir = new DirectoryInfo(nuspecRoot);
            FileInfo[] nuspecFiles = nuspecRootDir.GetFiles("{0}.nuspec"._Format(fileName), SearchOption.AllDirectories);
            string nuspecFilePath = string.Empty;
            NuspecFile nuspecFile = null;
            if (nuspecFiles.Length == 1)
            {
                nuspecFilePath = nuspecFiles[0].FullName;
            }
            else if (nuspecFiles.Length > 1)
            {
                nuspecFilePath = SelectNuspecFile(nuspecFiles);
            }

            if (!string.IsNullOrEmpty(nuspecFilePath))
            {
                nuspecFile = new NuspecFile(nuspecFilePath);
            }

            return nuspecFile;
        }

        private static string SelectNuspecFile(FileInfo[] files)
        {
            OutLineFormat("Multiple nuspec files found named {0}\r\n{1}Select from the list:\r\n", Path.GetFileNameWithoutExtension(files[0].FullName));
            for (int i = 0; i < files.Length; i++)
            {
                OutLineFormat("{0}. {1}", i + 1, files[i].FullName);
            }
            int selection = IntPrompt(string.Format("[1 - {0}]", files.Length));
            if (selection < 0 || selection > files.Length)
            {
                OutLineFormat("Invalid selection", ConsoleColor.Red);
                Environment.Exit(1);
            }
            return files[selection - 1].FullName;
        }

        private static void Append(string template, string fileNameFormat, StringBuilder copyAllScript, StringBuilder cleanScript, StringBuilder packScript, StringBuilder packScriptDebug, StringBuilder pushScript, DirectoryInfo outputDir, StreamReader sr, string ext)
        {
            string libraryName = sr.ReadLine().Trim();
            string fileName = string.Format(fileNameFormat, libraryName);
            string filePath = Path.Combine(outputDir.FullName, fileName);
            string fileContent = template.NamedFormat(new AssemblyNameContainer { LibraryName = libraryName, Ext = ext, NetLib = Lib, NetVer = Ver });
            fileContent.SafeWriteToFile(filePath, true);
            copyAllScript.AppendLine($"call {fileName} %1");
            string packLine = $"nuget pack {libraryName}\\{libraryName}.nuspec";
            packScript.AppendLine(packLine);
            packScriptDebug.AppendLine(packLine);
            pushScript.AppendLine($"nuget push -source nuget.org {NugetReleaseDirectory}{libraryName}.%1.nupkg");

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

        private static string GetMergeDllNameTextFile()
        {
            string value = Arguments["mdnf"].Or(Arguments["mergeDllNamesFile"]);
            return PromptIfNullOrEmpty(value, "Please enter the path to the merge dll names file");
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

        private static bool WriteReleaseNotes(string srcRoot, GitReleaseNotes notes, out string projectRoot)
        {
            projectRoot = Path.Combine(srcRoot, notes.PackageId);
            if (Directory.Exists(projectRoot))
            {
                string releaseNotesFile = Path.Combine(projectRoot, "RELEASENOTES");
                notes.Value.SafeWriteToFile(releaseNotesFile, true);
                return true;
            }
            return false;
        }
    }
}
