using System;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Server;
using Bam.Net.Testing;
using Bam.Net.Services.Clients;
using Bam.Net.Logging;
using Bam.Net.UserAccounts;
using System.IO;
using System.Collections.Generic;
using Bam.Net.Automation;
using Bam.Net.Automation.MSBuild;

namespace bam
{
    [Serializable]
    public class UtilityActions: CommandLineTestInterface
    {
        [ConsoleAction("createSourceDirectory", "Copy all the referenced cs files for the specified csproj files into a specified directory")]
        public void CreateSourceDirectory()
        {
            
        }

        [ConsoleAction("createBamProjectFromSolution", "Create a list of all the cs files in a specified solution file by reading all the csproj files therein")]
        public void CreateBamProjectFromSolution()
        {
            string solutionFilePath = GetArgument("solution", "Please specify the path to the solution file");
            FileInfo solutionFile = new FileInfo(solutionFilePath);
            if (!solutionFile.Exists)
            {
                OutLineFormat("Specified solution file was not found: {0}", ConsoleColor.Red, solutionFilePath);
            }
            else
            {
                List<string> projectFilePaths = new List<string>();
                solutionFile.FullName.SafeReadFile().DelimitSplit("\r", "\n").Each(line =>
                {
                    if (line.StartsWith("Project"))
                    {
                        string[] options = line.DelimitSplit(",");
                        if (options[1].EndsWith(".csproj\""))
                        {
                            projectFilePaths.Add(options[1].TrimNonLetters());
                        }
                    }
                });
                if(projectFilePaths.Count > 0)
                {
                    string outputPath = GetArgument("outputPath", "Please specify the path to write the list to");
                    BamProject bamProject = new BamProject();
                    List<string> csFiles = new List<string>();
                    List<AssemblyReference> assemblyReferences = new List<AssemblyReference>();
                    foreach(string projectFilePath in projectFilePaths)
                    {
                        FileInfo projectFile = new FileInfo(Path.Combine(solutionFile.Directory.FullName, projectFilePath));
                        FillFileAndReferenceLists(projectFile, csFiles, assemblyReferences);
                    }
                    bamProject.CsFiles = csFiles.ToArray();
                    bamProject.AssemblyReferences = assemblyReferences.ToArray();
                    bamProject.ToJsonFile(outputPath);
                }
                else
                {
                    OutLineFormat("No project files were found in the specified solution: {0}", solutionFilePath);
                }
            }
        }

        protected static void FillFileAndReferenceLists(FileInfo projectFile, List<string> csFiles, List<AssemblyReference> assemblyReferences)
        {
            Project project = projectFile.FromXmlFile<Project>();
            project.ItemGroup.Each(itemGroup =>
            {
                itemGroup.Compile.Each(compileItem =>
                {
                    csFiles.Add(Path.Combine(projectFile.Directory.FullName, compileItem.Include));
                });
                itemGroup.Reference.Each(reference =>
                {
                    assemblyReferences.Add(new AssemblyReference { AssemblyName = reference.Include, HintPath = reference.HintPath });
                });
            });
        }
    }
}
