using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.MSBuild
{
    public static class MSBuildExtensions
    {
        public static IEnumerable<CsFile> GetSolutionCsFiles(this string solutionPath)
        {
            return GetSolutionCsFiles(new FileInfo(solutionPath));
        }
        
        public static IEnumerable<CsFile> GetSolutionCsFiles(this FileInfo solutionFile)
        {
            return GetProjectFilePaths(solutionFile).SelectMany(projectPath => GetProjectCsFiles(projectPath));
        }

        public static IEnumerable<CsFile> GetProjectCsFiles(this string projectFilePath)
        {
            return GetProjectCsFiles(new FileInfo(projectFilePath));
        }

        public static IEnumerable<CsFile> GetProjectCsFiles(this FileInfo projectFile)
        {
            Project project = projectFile.FromXmlFile<Project>();
            List<CsFile> result = new List<CsFile>();
            foreach (ProjectItemGroup itemGroup in project.ItemGroup)
            {
                foreach(ProjectItemGroupCompile compileItem in itemGroup.Compile)
                {
                    yield return new CsFile { Root = projectFile.Directory.FullName, Path = compileItem.Include };
                }
            }
        }
        
        public static IEnumerable<AssemblyReference> GetSolutionAssemblyReferences(this string solutionPath)
        {
            return GetSolutionAssemblyReferences(new FileInfo(solutionPath));
        }

        public static IEnumerable<AssemblyReference> GetSolutionAssemblyReferences(this FileInfo solutionFile)
        {
            return GetProjectFilePaths(solutionFile).SelectMany(projectPath => GetAssemblyReferences(projectPath));
        }

        public static IEnumerable<AssemblyReference> GetAssemblyReferences(this string projectFilePath)
        {
            return GetAssemblyReferences(new FileInfo(projectFilePath));
        }

        public static IEnumerable<AssemblyReference> GetAssemblyReferences(this FileInfo projectFile)
        {
            Project project = projectFile.FromXmlFile<Project>();
            List<AssemblyReference> result = new List<AssemblyReference>();
            if(project.ItemGroup != null)
            {
                foreach (ProjectItemGroup itemGroup in project.ItemGroup)
                {
                    if(itemGroup.Reference != null)
                    {
                        foreach (ProjectItemGroupReference reference in itemGroup.Reference)
                        {
                            yield return new AssemblyReference { AssemblyName = reference.Include,  };
                        }
                    }
                }
            }
        }

        public static IEnumerable<ProjectReference> GetProjectReferences(this string projectFilePath)
        {
            return GetProjectReferences(new FileInfo(projectFilePath));
        }

        public static IEnumerable<ProjectReference> GetProjectReferences(this FileInfo projectFile)
        {
            Project project = projectFile.FromXmlFile<Project>();
            List<ProjectReference> result = new List<ProjectReference>();
            if (project.ItemGroup != null)
            {
                foreach (ProjectItemGroup itemGroup in project.ItemGroup)
                {
                    if (itemGroup.ProjectReference != null)
                    {
                        foreach (ProjectItemGroupProjectReference reference in itemGroup.ProjectReference)
                        {
                            yield return new ProjectReference { Guid = reference.Project, Include = reference.Include, Name = reference.Name };
                        }
                    }
                }
            }
        }

        public static IEnumerable<string> GetProjectFilePaths(this string solutionFilePath)
        {
            return GetProjectFilePaths(new FileInfo(solutionFilePath));
        }

        public static IEnumerable<string> GetProjectFilePaths(this FileInfo solutionFile)
        {
            List<string> projectFilePaths = new List<string>();
            foreach(string line in solutionFile.FullName.SafeReadFile().DelimitSplit("\r", "\n"))
            {
                if (line.StartsWith("Project"))
                {
                    string[] options = line.DelimitSplit(",");
                    if (options.Length >= 2 && options[1].EndsWith(".csproj\""))
                    {             
                        yield return Path.Combine(solutionFile.Directory.FullName, options[1].TrimNonLetters());
                    }
                }
            }
        }
    }
}
