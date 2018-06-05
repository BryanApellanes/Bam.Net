using Bam.Net.Application;
using Bam.Net.Automation.MSBuild;
using Bam.Net.Automation.Nuget;
using Bam.Net.Automation.SourceControl;
using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;

namespace Bam.Net.Automation
{
    [Serializable]
    public class BuildActions : CommandLineTestInterface
    {
        public const string BamToolkitDirectory = "C:\\bam\\tools\\BamToolkit";

        static string _nugetPath;
        /// <summary>
        /// Gets the path to nuget.exe.
        /// </summary>
        /// <value>
        /// The nuget path.
        /// </value>
        public static string NugetPath
        {
            get
            {
                if (string.IsNullOrEmpty(_nugetPath))
                {
                    _nugetPath = GetArgument("NugetPath", "Please enter the path to nuget.exe");
                }
                return _nugetPath;
            }
        }

        static string _outputDirectory;
        public static string OutputDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_outputDirectory))
                {
                    _outputDirectory = GetArgument("OutputDirectory", "Please enter the path to output files or nuget packages to.");
                }
                return _outputDirectory;
            }
        }

        static string _nugetInternalSource;
        /// <summary>
        /// Gets the nuget release directory.
        /// </summary>
        /// <value>
        /// The nuget release directory.
        /// </value>
        public static string NugetInternalSource
        {
            get
            {
                if (string.IsNullOrEmpty(_nugetInternalSource))
                {
                    _nugetInternalSource = DefaultConfiguration.GetAppSetting("NugetInternalSource", @"C:\bam\nuget\repository");
                }
                return _nugetInternalSource;
            }
        }

        static string _nugetPublicSource;
        public static string NugetPublicSource
        {
            get
            {
                if (string.IsNullOrEmpty(_nugetPublicSource))
                {
                    _nugetPublicSource = DefaultConfiguration.GetAppSetting("NugetPublicSource", @"nuget.org");
                }
                return _nugetPublicSource;
            }
        }

        static string _defaultLib;
        /// <summary>
        /// Gets the library.
        /// </summary>
        /// <value>
        /// The library.
        /// </value>
        public static string NugetLib
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultLib))
                {
                    _defaultLib = DefaultConfiguration.GetAppSetting("NugetLib", "net462");
                }
                return _defaultLib;
            }
        }

        static string _defaultVer;
        public static string FrameworkVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultVer))
                {
                    _defaultVer = DefaultConfiguration.GetAppSetting("FrameworkVersion", "v4.6.2");
                }
                return _defaultVer;
            }
        }

        static string _defaultStage;
        public static string NugetStage
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultStage))
                {
                    _defaultStage = DefaultConfiguration.GetAppSetting("NugetStage", "C:\\bam\\nuget\\stage");
                }
                return _defaultStage;
            }
        }

        static string _gitPath;
        public static string GitPath
        {
            get
            {
                if (string.IsNullOrEmpty(_gitPath))
                {
                    _gitPath = DefaultConfiguration.GetAppSetting("GitPath", "C:\\Program Files\\Git\\cmd\\git.exe");
                }
                return _gitPath;
            }
        }

        [ConsoleAction("dev", "Create dev nuget packages from the specified ReleaseFolder.")]
        public static void BuildDevPackages()
        {
            _nugetArg = Arguments["dev"];
            string commit = _nugetArg.CommitHash();
            string commitFile = Path.Combine(_nugetArg, "commit");
            if (string.IsNullOrEmpty(commit) && File.Exists(commitFile))
            {
                commit = commitFile.SafeReadFile().Trim();
            }
            _suffix = string.IsNullOrEmpty(commit) ? "Dev" : $"Dev-{commit.First(8)}";
            CreateNugetPackages();
        }

        [ConsoleAction("release", "Create the release from a specified source directory.  The release includes nuget packages and an msi file.")]
        public static void BuildRelease()
        {
            // update version
            string targetPath = GetTargetPath();            
            DirectoryInfo srcRoot = GetSourceRoot(targetPath);
            srcRoot.GetCommitHash().SafeWriteToFile(Path.Combine(srcRoot.FullName, typeof(Args).Namespace, "commit"), true);
            BamInfo info = GetBamInfo(srcRoot);
            info.VersionString = GetNextVersion(info.VersionString);
            UpdateAssemblyVersions(srcRoot, info);
            Task.WaitAll(SetSolutionNuspecInfos(srcRoot, info.VersionString).ToArray());

            BakeSettings settings = GetBuildTargetSettings(GetArgument("Solution", "Please enter path to the solution relative to the git repo root."));
            if (NugetRestore(settings))
            {
                settings.OutputPath = GetArgument("ReleaseFolder", "Please enter the path to the release folder.");
                if (Build(settings))
                {
                    // 
                    DirectoryInfo wixMergeModuleDirectory = new DirectoryInfo(Path.Combine(targetPath, GetArgument("WixMergeModule", "Please enter the source root relative path to the directory containing the wix file to update")));
                    string wixPath = Path.Combine(wixMergeModuleDirectory.FullName, $"{wixMergeModuleDirectory.Name}.wxs");
                    if (!MsiVersionUpdater(wixPath, info.VersionString))
                    {
                        OutLineFormat("Failed to update msi version", ConsoleColor.Magenta);
                    }
                    else if(BuildMsi())
                    {
                        _nugetArg = settings.OutputPath;
                        CreateNugetPackages();
                    }
                    else
                    {
                        OutLineFormat("Failed to build msi, see output for more information");
                    }
                }
            }
        }

        [ConsoleAction("publish", "Publish nuget packages to the internal or public nuget source.")]
        public static void Publish()
        {
            string searchPattern = GetSearchPattern(out string version);
            string publishFormat = "{NugetPath} {NugetAction} {Package} -Source {NugetSource}";

            string publishTarget = GetArgument("publish", "Please enter the target nuget source to publish to.").PascalCase();
            NugetSourceKind sourceKind = publishTarget.ToEnum<NugetSourceKind>();
            string nugetAction = string.Empty;
            string nugetSource = string.Empty;
            DirectoryInfo sourceRoot = null;
            Action finish = () => { };
            switch (sourceKind)
            {
                case NugetSourceKind.Internal:
                    nugetAction = "add";
                    nugetSource = NugetInternalSource;
                    break;
                case NugetSourceKind.Public:
                    nugetAction = "push";
                    nugetSource = NugetPublicSource;
                    sourceRoot = GetSourceRoot(GetTargetPath());
                    finish = () =>
                    {
                        Commit(version, sourceRoot);
                        Tag(version, sourceRoot);
                    };
                    break;
                case NugetSourceKind.Invalid:
                default:
                    OutLineFormat(string.Format("Unrecognized publish argument, should be one of (Internal | Public): ({0})", ConsoleColor.Magenta, publishTarget.Or("[null]")));
                    Exit(1);
                    break;
            }
            
            DirectoryInfo nugetPackageDirectory = new DirectoryInfo(OutputDirectory);
            List<Task> publishTasks = new List<Task>();
            foreach (FileInfo nugetPackage in nugetPackageDirectory.GetFiles(searchPattern))
            {
                OutLineFormat("Publishing {0} to {1}", ConsoleColor.DarkCyan, nugetPackage.FullName, nugetSource);
                publishTasks.Add(Task.Run(() => publishFormat.NamedFormat(new { NugetPath, NugetAction = nugetAction, Package = nugetPackage.FullName, NugetSource = nugetSource }).Run(line => Console.WriteLine(line), err => OutLineFormat(err, ConsoleColor.Magenta), 600000)));
            }

            Task.WaitAll(publishTasks.ToArray());
            finish();
        }

        [ConsoleAction("tag", "Create and commit version tag")]
        public static void CommitTag()
        {
            string version = GetVersionTag();
            DirectoryInfo sourceRoot = GetSourceRoot(GetTargetPath());
            Commit(version, sourceRoot);
            Tag(version, sourceRoot);
        }

        private static string GetVersionTag()
        {
            string tag = Arguments["tag"];
            if (string.IsNullOrEmpty(tag) && Arguments.Contains("v"))
            {
                tag = Arguments["v"];
            }
            return tag;
        }

        [ConsoleAction("msi", "Build the bam toolkit msi from a set of related wix project files.  The contents of the msi are set to the contents of the specified ReleaseFolder folder.")]
        public static bool BuildMsi()
        {
            string srcRoot = GetTargetPath();
            string releaseFolder = GetArgument("ReleaseFolder", "Please enter the path to the release folder.");

            DirectoryInfo wixMergeModuleDirectory = new DirectoryInfo(Path.Combine(srcRoot, GetArgument("WixMergeModule", "Please enter the source root relative path to the directory containing the wix file to update.")));
            DirectoryInfo wixMsiDirectory = new DirectoryInfo(Path.Combine(srcRoot, GetArgument("WixMsi", "Please enter the path to the directory where the wix msi project file (.wixproj) is found.")));

            string wixMergeModuleXml = Path.Combine(wixMergeModuleDirectory.FullName, $"{wixMergeModuleDirectory.Name}.wxs");
            string wixMergeModuleProject = Path.Combine(wixMergeModuleDirectory.FullName, $"{wixMergeModuleDirectory.Name}.wixproj");
            string wixMsiProject = Path.Combine(wixMsiDirectory.FullName, $"{wixMsiDirectory.Name}.wixproj");
            string wixOutput = GetArgument("WixOutput", "Please enter the path wix will use for output.");

            WixDocument wixDoc = new WixDocument(wixMergeModuleXml);            
            wixDoc.SetTargetContents(releaseFolder);

            // build the merge module
            BakeSettings settings = GetSettings();            
            ProcessOutput mergeModuleBuildOutput = $"{settings.MsBuild} {wixMergeModuleProject} /p:OutputPath={wixOutput}".Run(line => Console.WriteLine(line), err => OutLineFormat(err, ConsoleColor.Magenta), 600000);
            if(mergeModuleBuildOutput.ExitCode == 0)
            {
                // build the msi
                ProcessOutput msiBuildOutput = $"{settings.MsBuild} {wixMsiProject} /p:OutputPath={wixOutput}".Run(line => Console.WriteLine(line), err=> OutLineFormat(err, ConsoleColor.Magenta), 600000);
                return msiBuildOutput.ExitCode == 0;
            }
            return false;
        }

        [ConsoleAction("nuget", "Pack one or more binaries as nuget packages provided they have associated nuspec files.")]
        public static void CreateNugetPackages()
        {
            string targetPath = GetNugetArgument();
            if (targetPath.Equals("init"))
            {
                SetNuspecFiles();
            }
            else if (System.IO.File.Exists(targetPath))
            {
                FileInfo assemblyFile = new FileInfo(targetPath);
                PackAssembly(assemblyFile);
            }
            else if (System.IO.Directory.Exists(targetPath))
            {
                // look for nuspec files in the targetPath
                // assume that assemblies are in the targetPath
                List<Task> tasks = new List<Task>();
                DirectoryInfo dir = new DirectoryInfo(targetPath);
                foreach (FileInfo nuspecFile in dir.GetFiles("*.nuspec"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(nuspecFile.Name);
                    string dllName = Path.Combine(nuspecFile.Directory.FullName, $"{fileName}.dll");
                    string exeName = Path.Combine(nuspecFile.Directory.FullName, $"{fileName}.exe");
                    if (System.IO.File.Exists(dllName))
                    {
                        tasks.Add(Task.Run(() => PackAssembly(new FileInfo(dllName))));
                    }
                    else if (System.IO.File.Exists(exeName))
                    {
                        tasks.Add(Task.Run(() => PackAssembly(new FileInfo(exeName))));
                    }
                    else
                    {
                        OutLineFormat("WARN:Found nuspec without assembly: ({0})", ConsoleColor.Yellow, nuspecFile.FullName);
                    }
                }
                Task.WaitAll(tasks.ToArray());
            }
        }

        [ConsoleAction("bam", "Create a single bam.exe by using ILMerge.exe to combine all Bam.Net binaries into one.")]
        public static void CreateBamDotExe()
        {
            string ilMergePath = GetArgument("ILMergePath", "Please specify the path to ILMerge.exe");
            string root = GetArgument("root", "Please specify the path to the Bam.Net binaries and nuspec files");

            DirectoryInfo rootDir = new DirectoryInfo(root);
            StringBuilder arguments = new StringBuilder();
            arguments.Append("bam.exe");
            foreach (FileInfo nuspecFile in rootDir.GetFiles("*.nuspec"))
            {
                string fileName = Path.GetFileNameWithoutExtension(nuspecFile.Name);
                string argToAdd = Path.Combine(root, $"{fileName}.dll");
                if (!File.Exists(argToAdd))
                {
                    argToAdd = Path.Combine(root, $"{fileName}.exe");
                }
                if (File.Exists(argToAdd))
                {
                    arguments.Append($" {argToAdd}");
                }
            }

            string lib = $@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\{FrameworkVersion}";
            arguments.Append($" /closed /targetplatform:v4 /lib:\"{lib}\" /out:{OutputDirectory}\\bam.exe");
            string command = $"{ilMergePath} {arguments.ToString()}";
            OutLine(command);
            ProcessOutput output = command.ToStartInfo(root).Run();
            OutLine(output.StandardOutput);
            OutLine(output.StandardError, ConsoleColor.Red);
            OutLine("done");
        }

        [ConsoleAction("nuspec", "Initialize or update project nuspec files.")]
        public static void SetNuspecFiles()
        {
            string argValue = Arguments["nuspec"];
            List<Task> tasks = new List<Task>();
            string owners = GetArgument("Owners", "Please enter the owners for new nuspec files.");
            string authors = GetArgument("Authors", "Please enter the authors for new nuspec files.");
            Predicate<string> predicate = GetPredicate();

            if (string.IsNullOrEmpty(argValue))
            {
                DirectoryInfo sourceRoot = new DirectoryInfo(GetArgument("root", "Please enter the path to the root of the source tree."));
                string version = GetVersion(sourceRoot);
                tasks.AddRange(SetSolutionNuspecInfos(sourceRoot, version, owners, authors, predicate));
            }
            else if (System.IO.Directory.Exists(argValue))
            {
                DirectoryInfo target = new DirectoryInfo(argValue);
                string version = GetVersion(target);
                tasks.AddRange(SetSolutionNuspecInfos(target, version, owners, authors, predicate));
            }
            else if (System.IO.File.Exists(argValue))
            {
                FileInfo fileArg = new FileInfo(argValue);
                string version = GetVersion(fileArg.Directory);
                if (fileArg.Extension.Equals(".sln", StringComparison.InvariantCultureIgnoreCase))
                {
                    tasks.AddRange(SetSolutionNuspecInfos(fileArg, version, owners, authors, predicate));
                }
                else if (fileArg.Extension.Equals(".csproj"))
                {
                    tasks.Add(SetProjectNuspecInfo(fileArg, version, owners, authors, predicate));
                }
            }

            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// /The path to look for binaries when packing nuget packages
        /// </summary>
        static string _nugetArg; 
        private static string GetNugetArgument()
        {
            if (!string.IsNullOrEmpty(_nugetArg))
            {
                return _nugetArg;
            }
            return GetArgument("nuget", "Please specify the path to pack nuget packages from.");
        }

        private static bool Build(BakeSettings bakeSettings)
        {
            string command = $"{bakeSettings.MsBuild} /t:Build /filelogger /p:AutoGenerateBindingRedirects={bakeSettings.AutoGenerateBindingRedirects};GenerateDocumentation={bakeSettings.GenerateDocumentation};OutputPath={bakeSettings.OutputPath};Configuration={bakeSettings.Config};Platform=\"{bakeSettings.Platform}\";TargetFrameworkVersion={bakeSettings.TargetFrameworkVersion};CompilerVersion={bakeSettings.TargetFrameworkVersion} {bakeSettings.ProjectFile} /m:{bakeSettings.MaxCpuCount}";

            ProcessOutput output = command.Run((line) => Console.WriteLine(line), (err) => OutLine(err, ConsoleColor.Magenta), 600000);
            return output.ExitCode == 0;
        }

        protected static List<Task> SetSolutionNuspecInfos(DirectoryInfo sourceRoot, string version)
        {
            string owners = GetArgument("Owners", "Please enter the owners for new nuspec files.");
            string authors = GetArgument("Authors", "Please enter the authors for new nuspec files.");
            Predicate<string> predicate = GetPredicate();
            return SetSolutionNuspecInfos(sourceRoot, version, owners, authors, predicate);
        }

        protected static List<Task> SetSolutionNuspecInfos(DirectoryInfo sourceRoot, string version, string defaultOwners, string defaultAuthors, Predicate<string> predicate)
        {
            List<Task> tasks = new List<Task>();
            OutLineFormat("Searching for all solution files (*.sln) in ({0})", ConsoleColor.DarkCyan, sourceRoot.FullName);
            foreach (FileInfo solutionFile in sourceRoot.GetFiles("*.sln"))
            {
                tasks.AddRange(SetSolutionNuspecInfos(solutionFile, version, defaultOwners, defaultAuthors, predicate));
            }
            return tasks;
        }

        private static List<Task> SetSolutionNuspecInfos(FileInfo solutionFile, string version, string defaultOwners, string defaultAuthors, Predicate<string> predicate)
        {
            return ForEachProjectFileInSolution(solutionFile, (projectFile) => SetProjectNuspecInfo(projectFile, version, defaultOwners, defaultAuthors, predicate));
        }

        private static Task SetProjectNuspecInfo(FileInfo projectFile, string version, string defaultOwners, string defaultAuthors, Predicate<string> predicate)
        {
            if (!projectFile.IsInGitRepo())
            {
                OutLineFormat("Project not in a git repository: ({0})", ConsoleColor.Magenta, projectFile.FullName);
            }
            DirectoryInfo sourceRoot = projectFile.Directory.UpToGitRoot();
            string fileName = Path.GetFileNameWithoutExtension(projectFile.Name);

            if ((Arguments.Contains("prefix") && projectFile.Name.StartsWith(Arguments["prefix"])) ||
                !Arguments.Contains("prefix"))
            {
                return Task.Run(() =>
                {
                    OutLineFormat("Updating nuspec for {0}", ConsoleColor.Cyan, projectFile.FullName);
                    DirectoryInfo currentProjectDirectory = projectFile.Directory;
                    NuspecFile nuspecFile = new NuspecFile(Path.Combine(currentProjectDirectory.FullName, $"{fileName}.nuspec"));
                    nuspecFile.Version = new PackageVersion(version);
                    nuspecFile.Save();
                    if (!System.IO.File.Exists(nuspecFile.Path))
                    {
                        nuspecFile = new NuspecFile(nuspecFile.Path)
                        {
                            Id = fileName,
                            Title = fileName,
                            Description = fileName,
                            Owners = defaultOwners,
                            Authors = defaultAuthors
                        };
                        nuspecFile.AddProjectDependencies(version, predicate);
                    }
                    FileInfo packageConfig = new FileInfo(Path.Combine(projectFile.Directory.FullName, "packages.config"));
                    if (packageConfig.Exists)
                    {
                        OutLineFormat("Updating dependencies for {0}", ConsoleColor.DarkCyan, projectFile.FullName);
                        nuspecFile.AddPackageDependencies(packageConfig);
                    }
                    nuspecFile.UpdateProjectDependencyVersions(version, predicate);
                    nuspecFile.Save();
                    nuspecFile.UpdateReleaseNotes(sourceRoot.FullName);
                    nuspecFile.Save();
                    OutLineFormat("{0}: Release Notes: {1}", ConsoleColor.DarkBlue, nuspecFile.Id, nuspecFile.ReleaseNotes);
                });
            }

            return Task.CompletedTask;
        }

        static Func<string, string, bool> _msiVersionUpdater;
        static object _msiVersionUpdaterLock = new object();
        public static Func<string, string, bool> MsiVersionUpdater
        {
            get
            {
                return _msiVersionUpdaterLock.DoubleCheckLock(ref _msiVersionUpdater, () => (wixPath, version) =>
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(wixPath);
                    foreach (XmlNode node in xmlDoc) // XPath would have been easier and prettier but I couldn't get it to work.
                    {
                        if (node.Name.Equals("Wix"))
                        {
                            foreach (XmlNode o in node)
                            {
                                if (o.Name.Equals("Module"))
                                {
                                    foreach (XmlAttribute attribute in o.Attributes)
                                    {
                                        if (attribute.Name.Equals("Version"))
                                        {
                                            attribute.Value = version;
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    return false;
                });
            }
            set
            {
                _msiVersionUpdater = value;
            }
        }

        protected static BamInfo UpdateVersion(DirectoryInfo srcRootDir)
        {
            BamInfo info = GetBamInfo(srcRootDir);
            info.VersionString = GetNextVersion(info.VersionString);
            UpdateAssemblyVersions(srcRootDir, info);
            return info;
        }

        protected static void UpdateAssemblyVersions(DirectoryInfo srcRootDir, BamInfo info)
        {
            GitReleaseNotes miscReleaseNotes = GitReleaseNotes.MiscSinceLatestRelease(srcRootDir.FullName);
            miscReleaseNotes.Summary = $"Version {info.VersionString}";
            OutLineFormat("Updating release notes:\r\n{0}", ConsoleColor.DarkYellow, info.ReleaseNotes);
            info.ReleaseNotes = miscReleaseNotes.Value;
            info.ToJsonFile(Path.Combine(srcRootDir.FullName, "bam.json"));

            srcRootDir.GetFiles("*.csproj", SearchOption.AllDirectories).Each(projectFile =>
            {
                try
                {
                    SetAssemblyInfo(projectFile, info.VersionString);
                    NuspecFile.ForFile(projectFile).UpdateProjectDependencyVersions(info.VersionString, GetPredicate());
                }
                catch (Exception ex)
                {
                    OutLineFormat("Error updating version for ({0})", ex, projectFile.FullName);
                }
            });
        }

        protected static BamInfo GetBamInfo(DirectoryInfo srcRootDir)
        {
            string bamInfoPath = Path.Combine(srcRootDir.FullName, "bam.json");
            if (!System.IO.File.Exists(bamInfoPath))
            {
                OutLineFormat("Unable to find bam.json, expected it at ({0})", ConsoleColor.Magenta, bamInfoPath);
                Exit(1);
            }
            BamInfo info = bamInfoPath.FromJsonFile<BamInfo>();
            Out("*** bam.json ***", ConsoleColor.Cyan);
            OutLine(info.PropertiesToString(), ConsoleColor.Cyan);
            OutLine("***", ConsoleColor.Cyan);
            return info;
        }

        private static string GetVersion(DirectoryInfo referenceDirectory)
        {
            string lastRelease = Git.LatestRelease(referenceDirectory.UpToGitRoot().FullName);
            string version = lastRelease.TruncateFront(1);

            if (Arguments.Contains("v"))
            {
                string argVersion = Arguments["v"];
                if (!argVersion.Equals("latest"))
                {
                    version = argVersion;
                }
            }

            return version;
        }

        private static string GetNextVersion(string currentVersion)
        {
            PackageVersion packageVersion = new PackageVersion(currentVersion);
            if (!Arguments.Contains("patch") && !Arguments.Contains("minor") && !Arguments.Contains("major"))
            {
                packageVersion.IncrementPatch();
            }
            else
            {
                if (Arguments.Contains("patch"))
                    packageVersion.IncrementPatch();

                if (Arguments.Contains("minor"))
                    packageVersion.IncrementMinor();

                if (Arguments.Contains("major"))
                    packageVersion.IncrementMajor();
            }

            return packageVersion.Value;
        }

        private static void SetAssemblyInfo(FileInfo projectFile, string version)
        {
            DirectoryInfo projectDirectory = projectFile.Directory;
            FileInfo assemblyInfo = new FileInfo(Path.Combine(projectDirectory.FullName, "Properties", "AssemblyInfo.cs"));
            if (assemblyInfo.Exists)
            {
                OutLineFormat("Setting assembly info for {0}", ConsoleColor.Cyan, projectFile.FullName);
                string projectName = Path.GetFileNameWithoutExtension(projectFile.Name);
                FileInfo nuspecFileInfo = new FileInfo(Path.Combine(projectDirectory.FullName, $"{projectName}.nuspec"));
                NuspecFile nuspecFile = null;
                if (nuspecFileInfo.Exists)
                {
                    nuspecFile = new NuspecFile(nuspecFileInfo.FullName);
                }

                StringBuilder newContent = new StringBuilder();
                List<AssemblyAttributeInfo> attributeInfos = GetAssemblyAttributeInfos(nuspecFile, version);
                using (StreamReader reader = new StreamReader(assemblyInfo.FullName))
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
                foreach(AssemblyAttributeInfo attributeInfo in attributeInfos.Where(ai=> !ai.WroteInfo))
                {
                    newContent.AppendLine(attributeInfo.AssemblyAttribute);
                }
                newContent.ToString().SafeWriteToFile(assemblyInfo.FullName, true);
            }
        }

        private static List<AssemblyAttributeInfo> GetAssemblyAttributeInfos(NuspecFile nuspecFile, string version)
        {            
            List<AssemblyAttributeInfo> _assemblyAttributeInfos = new List<AssemblyAttributeInfo>();

            _assemblyAttributeInfos = new List<AssemblyAttributeInfo>
            {
                new AssemblyAttributeInfo { AttributeName = "AssemblyVersion", Value = version },
                new AssemblyAttributeInfo { AttributeName = "AssemblyFileVersion", Value = version },
                new AssemblyAttributeInfo { AttributeName = "AssemblyCompany", NuspecFile = nuspecFile, NuspecProperty = "Owners" },
                new AssemblyAttributeInfo { AttributeName = "AssemblyDescription", NuspecFile = nuspecFile, NuspecProperty = "Description" }
            };

            return _assemblyAttributeInfos;
        }

        /// <summary>
        /// Gets the target path, which is either the path specified by the /release argument or the path to the /root argument.  If neither is specifed,
        /// then you're prompted for the value.
        /// </summary>
        /// <returns></returns>
        protected static string GetTargetPath()
        {
            // get root where src is
            string targetPath = Arguments["release"];
            if (string.IsNullOrEmpty(targetPath))
            {
                targetPath = GetArgument("root", "Please enter the path to the root of the source tree.");
            }

            return targetPath;
        }

        protected static string PrepareNugetStage(FileInfo assemblyFile, string lib = null)
        {
            string fileName = Path.GetFileNameWithoutExtension(assemblyFile.Name);
            string xmlFileName = $"{fileName}.xml"; // the documentation file
            FileInfo xmlFile = new FileInfo(Path.Combine(assemblyFile.Directory.FullName, xmlFileName));
            DirectoryInfo stage = new DirectoryInfo(Path.Combine(NugetStage, fileName));
            if (!stage.Exists)
            {
                stage.Create();
            }
            string libPath = Path.Combine(stage.FullName, "lib", lib ?? NugetLib);
            if (!System.IO.Directory.Exists(libPath))
            {
                System.IO.Directory.CreateDirectory(libPath);
            }
            assemblyFile.CopyTo(Path.Combine(libPath, assemblyFile.Name), true);
            if (xmlFile.Exists)
            {
                xmlFile.CopyTo(Path.Combine(libPath, xmlFile.Name), true);
            }
            return stage.FullName;
        }

        // bake.exe /nuget:assemblyPath
        private static ProcessOutput PackAssembly(FileInfo assemblyOrNuspec)
        {
            if (!assemblyOrNuspec.Exists)
            {
                OutLineFormat(string.Format("File not found: {0}", ConsoleColor.Magenta, assemblyOrNuspec.FullName));
                Exit(1);
            }
            string stagePath = PrepareNugetStage(assemblyOrNuspec);
            DirectoryInfo stage = new DirectoryInfo(stagePath);
            string fileName = Path.GetFileNameWithoutExtension(assemblyOrNuspec.Name);
            string existingNuspec = Path.Combine(assemblyOrNuspec.Directory.FullName, $"{fileName}.nuspec");
            string stageNuspec = Path.Combine(stage.FullName, $"{fileName}.nuspec");
            if (System.IO.File.Exists(existingNuspec))
            {
                System.IO.File.Copy(existingNuspec, stageNuspec, true);
            }
            NuspecFile nuspecFile = new NuspecFile(stageNuspec);
            if (!System.IO.File.Exists(nuspecFile.Path))
            {
                nuspecFile.Id = fileName;
                nuspecFile.Title = fileName;
                nuspecFile.Description = fileName;
                nuspecFile.Owners = GetArgument("Owners", $"Please enter the owners for the {fileName} nuget package.");
                nuspecFile.Authors = GetArgument("Authors", $"Please enter the authors for the {fileName} nuget package."); ;
                nuspecFile.Save();
            }
            string suffix = GetSuffix();
            ProcessOutput output = $"{NugetPath} pack {nuspecFile.Path} -OutputDirectory \"{OutputDirectory}\"{suffix}".Run((o) => OutLineFormat(o, ConsoleColor.Cyan), (err) => OutLineFormat(err, ConsoleColor.Magenta), 600000);
            OutLineFormat("{0} {1}: package written to {2}", nuspecFile.Id, $"{nuspecFile.Id}.{nuspecFile.Version.ToString()}{suffix}", OutputDirectory);
            return output;
        }

        static string _suffix;
        private static string GetSuffix()
        {
            if (string.IsNullOrEmpty(_suffix))
            {
                _suffix = Arguments.Contains("suffix") && !string.IsNullOrEmpty(Arguments["suffix"]) ? Arguments["suffix"] : "";
            }
            return string.IsNullOrEmpty(_suffix) ? _suffix : $" -Suffix {_suffix}";
        }

        private static List<Task> ForEachProjectFileInSolution(FileInfo solutionFile, Action<FileInfo> forEach)
        {
            HashSet<string> projectFilePaths = new HashSet<string>();
            List<Task> results = new List<Task>();
            foreach (string projectFilePath in solutionFile.GetProjectFilePaths())
            {
                if (!projectFilePaths.Contains(projectFilePath))
                {
                    projectFilePaths.Add(projectFilePath);
                    FileInfo projectFile = new FileInfo(projectFilePath);
                    results.Add(Task.Run(() => forEach(projectFile)));
                }
            }
            return results;
        }

        private static bool NugetRestore(BakeSettings bakeSettings)
        {
            string command = $"{bakeSettings.Nuget} restore {bakeSettings.ProjectFile} -PackagesDirectory {bakeSettings.PackagesDirectory}";
            ProcessOutput output = command.Run((line) => Console.WriteLine(line), (err) => OutLineFormat(err, ConsoleColor.Magenta), 600000);
            return output.ExitCode == 0;
        }

        private static BakeSettings GetBuildTargetSettings(string projectOrSolution, string config = "Release", string platform = "x64", string framework = "v4.6.2")
        {
            BakeSettings settings = GetSettings(config, platform, framework);
            settings.ProjectFile = projectOrSolution;
            return settings;
        }

        private static BakeSettings GetSettings(string config = "Release", string platform = "x64", string framework = "v4.6.2")
        {
            return new BakeSettings
            {
                Config = config,
                Platform = platform,
                TargetFrameworkVersion = framework,
                MsBuild = GetArgument("MsBuildPath", "Please enter the path to msbuild.exe."),
                Nuget = NugetPath,
                PackagesDirectory = GetArgument("PackagesDirectory", "Please enter the path to restore nuget packages to."),
            };
        }

        private static DirectoryInfo GetSourceRoot(string targetPath)
        {
            DirectoryInfo srcRoot = new DirectoryInfo(targetPath);
            if (!srcRoot.IsInGitRepo())
            {
                OutLineFormat("{0} is not a git repository", targetPath);
                Exit(1);
            }

            return srcRoot;
        }

        private static Predicate<string> GetPredicate()
        {
            return (projectName) => !Arguments.Contains("prefix") || projectName.StartsWith(Arguments["prefix"]);
        }

        private static void Commit(string message, DirectoryInfo sourceRootDir = null)
        {
            if (message.Contains("'"))
            {
                OutLineFormat("Specified message contains one or more (') characters all of which will be replaced with (`) to avoid git command line errors.", ConsoleColor.Yellow);
                message = message.Replace("'", "`");
            }
            sourceRootDir = sourceRootDir ?? GetSourceRoot(GetTargetPath());
            string sourceRoot = sourceRootDir.FullName;
            GitPath.ToStartInfo("add --all", sourceRoot).RunAndWait(o => OutLine(o, ConsoleColor.Cyan), (e) => OutLine(e, ConsoleColor.Magenta));
            GitPath.ToStartInfo($"commit -m '{message}'", sourceRoot).RunAndWait(o => OutLine(o, ConsoleColor.Cyan), (e) => OutLine(e, ConsoleColor.Magenta));
            GitPath.ToStartInfo("push origin", sourceRoot).RunAndWait(o => OutLine(o, ConsoleColor.Cyan), (e) => OutLine(e, ConsoleColor.Magenta));
        }

        private static void Tag(string version, DirectoryInfo sourceRootDir = null)
        {
            sourceRootDir = sourceRootDir ?? GetSourceRoot(GetTargetPath());
            string sourceRoot = sourceRootDir.FullName;
            GitPath.ToStartInfo($"tag -a v{version} -m 'v{version}'", sourceRoot).RunAndWait(o => OutLine(o, ConsoleColor.Cyan), (e) => OutLine(e, ConsoleColor.Magenta));
            GitPath.ToStartInfo($"push origin v{version}", sourceRoot).RunAndWait(o => OutLine(o, ConsoleColor.Cyan), (e) => OutLine(e, ConsoleColor.Magenta));
        }

        private static string GetSearchPattern(out string version)
        {
            string searchPatternFormat = "*{Version}.nupkg";
            version = GetArgument("v", "Please enter the version to publish");
            if (Arguments.Contains("prefix"))
            {
                searchPatternFormat = string.Format("{0}*{{Version}}.nupkg", Arguments["prefix"]);
            }
            string searchPattern = searchPatternFormat.NamedFormat(new { Version = version });
            return searchPattern;
        }
    }
}
