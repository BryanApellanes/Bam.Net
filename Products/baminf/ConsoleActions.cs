using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Automation.Nuget;
using Bam.Net.Automation;
using System.Reflection;
using Bam.Net.Automation.AdvancedInstaller;

namespace baminf
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        #region ConsoleActions
        [ConsoleAction("baminfo.json", "specify the path to the baminfo.json file to use")]
        public static void SetBamInfo()
        {
            string nuspecRoot = GetNuspecRoot();
            string bamInfoPath = (Arguments["baminfo.json"] ?? Prompt("Enter the path to the baminfo.json file"));
            string versionString = GetVersion();
            BamInfo info = bamInfoPath.FromJsonFile<BamInfo>();
            Out("*** baminfo.json ***", ConsoleColor.Cyan);
            OutLine(info.PropertiesToString(), ConsoleColor.Cyan);
            OutLine("***", ConsoleColor.Cyan);
            OutLineFormat("Updating version from {0} to {1}", ConsoleColor.Yellow, info.VersionString, versionString);
            info.VersionString = versionString;
            info.ToJsonFile(bamInfoPath);
            DirectoryInfo nuspecRootDir = new DirectoryInfo(nuspecRoot);
            FileInfo[] nuspecFiles = nuspecRootDir.GetFiles("*.nuspec", SearchOption.AllDirectories);
            foreach(FileInfo file in nuspecFiles)
            {
                NuspecFile nuspecFile = new NuspecFile(file.FullName);
                nuspecFile.Authors = info.Authors;
                nuspecFile.Owners = info.Owners;
                nuspecFile.ReleaseNotes = info.ReleaseNotes;
                nuspecFile.Copyright = "Copyright © {0} {1}"._Format(info.Owners, DateTime.UtcNow.Year);
                nuspecFile.LicenseUrl = info.LicenseUrl;
                nuspecFile.ProjectUrl = info.ProjectUrl;
                string buildNumber = !string.IsNullOrEmpty(info.BuildNumber) ? "-{0}"._Format(info.BuildNumber) : "";
                string patch = string.Format("{0}{1}", info.PatchVersion.ToString(), buildNumber);
                nuspecFile.Version.Major = info.MajorVersion.ToString();
                nuspecFile.Version.Minor = info.MinorVersion.ToString();
                nuspecFile.Version.Patch = patch;                
                List<NugetPackageIdentifier> bamDependencies = new List<NugetPackageIdentifier>();
                if(nuspecFile.Dependencies != null)
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
            string srcRoot, version, nuspecRoot;
            GetParameters(out srcRoot, out version, out nuspecRoot);

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

        #endregion
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
            for(int i = 0; i < files.Length; i++)
            {
                OutLineFormat("{0}. {1}", i + 1, files[i].FullName);
            }
            int selection = NumberPrompt(string.Format("[1 - {0}]", files.Length));
            if(selection < 0 || selection > files.Length)
            {
                OutLineFormat("Invalid selection", ConsoleColor.Red);
                Environment.Exit(1);
            }
            return files[selection - 1].FullName;
        }
    }
}