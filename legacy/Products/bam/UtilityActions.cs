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
using System.IO.Compression;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Bam.Net.Configuration;
using System.Linq;
using Bam.Net.System;
using System.Diagnostics;
using System.Security;

namespace Bam.Net.Application
{
    [Serializable]
    public class UtilityActions: CommandLineTestInterface
    {
        [ConsoleAction("createBamProjectSrcPackage", "Create a BamProject package from a solution or project")]
        public static void CreateBamProjectPackage()
        {
            string projectOrSolutionPath = GetArgument("projectOrSolution", "Please specify the path to the solution or project file");
            FileInfo file = new FileInfo(projectOrSolutionPath);
            bool isSolution = file.Extension.Equals(".sln");
            bool isProject = file.Extension.Equals(".csproj");
            if(!isSolution && !isProject)
            {
                OutLineFormat("Specified file format not supported: {0}", file.Extension, ConsoleColor.Red);
                return;
            }

            List<string> projectFilePaths = new List<string>();
            if (isProject)
            {
                projectFilePaths.Add(file.FullName);
            }
            if (isSolution)
            {
                projectFilePaths.AddRange(file.GetProjectFilePaths());
            }

            BamProject bamProject = CreateBamProject(projectFilePaths);
            BamProject bamPackage = new BamProject();
            string bamPackageFilePath = ".\\BamPackage.zip";
            if (File.Exists(bamPackageFilePath))
            {
                File.Move(bamPackageFilePath, bamPackageFilePath.GetNextFileName());
            }
            string bamPackageDir = ".\\BamPackage\\";
            if (Directory.Exists(bamPackageDir))
            {
                Directory.Move(bamPackageDir, bamPackageDir.GetNextDirectoryName());
            }
            Directory.CreateDirectory(bamPackageDir);
            DirectoryInfo dir = new DirectoryInfo(bamPackageDir);
            List<CsFile> targetCsFiles = new List<CsFile>();
            foreach(CsFile csFile in bamProject.CsFiles)
            {
                CsFile targetCsFile = csFile.CopyTo(dir.FullName);
                DirectoryInfo rootDir = new DirectoryInfo(csFile.Root);
                targetCsFile.Root = Path.Combine(bamPackageDir, rootDir.Name);
                targetCsFiles.Add(targetCsFile);
            }
            bamPackage.CsFiles = targetCsFiles.ToArray();
            bamPackage.AssemblyReferences = bamProject.AssemblyReferences;
            bamPackage.ToJsonFile(Path.Combine(dir.FullName, "BamProject.json"));
            string outputPath = new FileInfo(bamPackageFilePath).FullName;
            ZipFile.CreateFromDirectory(dir.FullName, outputPath);
            OutLineFormat("Package file written: {0}", outputPath);
        }

        [ConsoleAction("createBamProjectFromSolution", "Create a BamProject.json file from a solution file")]
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
                List<string> projectFilePaths = solutionFile.GetProjectFilePaths().ToList();
                if (projectFilePaths.Count > 0)
                {
                    string outputPath = GetArgument("outputPath", "Please specify the path to write the list to");
                    BamProject bamProject = CreateBamProject(projectFilePaths);
                    bamProject.ToJsonFile(outputPath);
                }
                else
                {
                    OutLineFormat("No project files were found in the specified solution: {0}", solutionFilePath);
                }
            }
        }

        private static BamProject CreateBamProject(List<string> projectFilePaths)
        {
            BamProject bamProject = new BamProject();
            List<CsFile> csFiles = new List<CsFile>();
            List<AssemblyReference> assemblyReferences = new List<AssemblyReference>();
            foreach (string projectFilePath in projectFilePaths)
            {
                FileInfo projectFile = new FileInfo(projectFilePath);
                FillFileAndReferenceLists(projectFile, csFiles, assemblyReferences);
            }
            bamProject.CsFiles = csFiles.ToArray();
            bamProject.AssemblyReferences = assemblyReferences.ToArray();
            return bamProject;
        }

        /// <summary>
        /// Fills the file and reference lists.
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        /// <param name="csFiles">The cs files.</param>
        /// <param name="assemblyReferences">The assembly references.</param>
        protected static void FillFileAndReferenceLists(FileInfo projectFile, List<CsFile> csFiles, List<AssemblyReference> assemblyReferences)
        {
            Project project = projectFile.FromXmlFile<Project>();
            project.ItemGroup.Each(itemGroup =>
            {
                itemGroup.Compile.Each(compileItem =>
                {
                    if (!compileItem.Include.Equals("Properties\\AssemblyInfo.cs"))
                    {
                        csFiles.Add(new CsFile { Root = projectFile.Directory.FullName, Path = compileItem.Include });
                    }
                });
                itemGroup.Reference.Each(reference =>
                {
                    assemblyReferences.Add(new AssemblyReference { AssemblyName = reference.Include });
                });
            });
        }
    }
}
