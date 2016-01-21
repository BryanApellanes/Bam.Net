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
using nuver.Nuget;

namespace assinf
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        // See the below for examples of ConsoleActions and UnitTests
        static List<AssemblyAttributeInfo> GetAssemblyAttributeInfos(string fileName, string version, string nuspecRoot)
        {
            List<AssemblyAttributeInfo> _assemblyAttributeInfos = new List<AssemblyAttributeInfo>();
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

            _assemblyAttributeInfos = new List<AssemblyAttributeInfo>();
            _assemblyAttributeInfos.Add(new AssemblyAttributeInfo { AttributeName = "AssemblyVersion", Value = version });
            _assemblyAttributeInfos.Add(new AssemblyAttributeInfo { AttributeName = "AssemblyFileVersion", Value = version });
            _assemblyAttributeInfos.Add(new AssemblyAttributeInfo { AttributeName = "AssemblyCompany", NuspecFile = nuspecFile, NuspecProperty = "Owners" });
            _assemblyAttributeInfos.Add(new AssemblyAttributeInfo { AttributeName = "AssemblyDescription", NuspecFile = nuspecFile, NuspecProperty = "Description" });
        
            return _assemblyAttributeInfos;
        }
        #region ConsoleAction examples
        [ConsoleAction("si", "Set assembly info")]
        public static void SetInfo()
        {
            string srcRoot = Arguments["root"] ?? Prompt("Please enter the root of the source tree");
            string version = Arguments["v"] ?? Prompt("Please enter the version number");
            string nuspecRoot = Arguments["nuspecRoot"] ?? Prompt("Please enter the root to search for nuspec files");

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
                        foreach(AssemblyAttributeInfo attributeInfo in attributeInfos)
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
                foreach(AssemblyAttributeInfo attributeInfo in attributeInfos)
                {
                    if (!attributeInfo.WroteInfo)
                    {
                        newContent.AppendLine(attributeInfo.AssemblyAttribute);
                    }
                }
                newContent.ToString().SafeWriteToFile(infoFile.FullName, true);
            });
        }

        //[ConsoleAction("sv", "Set Version")]
        //public static void SetVersion()
        //{
        //    string srcRoot = Arguments["root"] ?? Prompt("Please enter the root of the source tree");
        //    string version = Arguments["sv"] ?? Prompt("Please enter the version number");
        //    string assemblyVersionFormat = "[assembly: AssemblyVersion(\"{0}\")]";
        //    string fileVersionFormat = "[assembly: AssemblyFileVersion(\"{0}\")]";

        //    DirectoryInfo srcRootDir = new DirectoryInfo(srcRoot);
        //    srcRootDir.GetFiles("AssemblyInfo.cs", SearchOption.AllDirectories).Each(infoFile =>
        //    {
        //        OutLineFormat("Writing assembly info into: {0}", ConsoleColor.Blue, infoFile.FullName);
        //        StringBuilder newContent = new StringBuilder();
        //        bool wroteAssemblyVersion = false;
        //        bool wroteFileVersion = false;
        //        using (StreamReader reader = new StreamReader(infoFile.FullName))
        //        {
        //            while (!reader.EndOfStream)
        //            {
        //                string currentLine = reader.ReadLine();
        //                if (currentLine.StartsWith("[assembly: AssemblyVersion"))
        //                {
        //                    currentLine = assemblyVersionFormat._Format(version);
        //                    wroteAssemblyVersion = true;
        //                }
        //                else if (currentLine.StartsWith("[assembly: AssemblyFileVersion"))
        //                {
        //                    currentLine = fileVersionFormat._Format(version);
        //                    wroteFileVersion = true;
        //                }
        //                newContent.AppendLine(currentLine);
        //            }
        //        }
        //        if (!wroteAssemblyVersion)
        //        {
        //            newContent.AppendLine(assemblyVersionFormat._Format(version));
        //        }
        //        if (!wroteFileVersion)
        //        {
        //            newContent.AppendLine(fileVersionFormat._Format(version));
        //        }
        //        newContent.ToString().SafeWriteToFile(infoFile.FullName, true);
        //    });
        //}
        #endregion
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