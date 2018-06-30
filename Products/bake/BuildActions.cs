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
using System.Threading;
using Bam.Net.System;
using System.Management;

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
        /// <summary>
        /// Gets the output directory.  Either the binary output or the nuget package output results
        /// from packing.
        /// </summary>
        /// <value>
        /// The output directory.
        /// </value>
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

        static string _platform;
        public static string Platform
        {
            get
            {
                if (string.IsNullOrEmpty(_platform))
                {
                    _platform = DefaultConfiguration.GetAppSetting("Platform", "x64");
                }
                return _platform;
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

        static string _builds;
        public static string Builds
        {
            get
            {
                if (string.IsNullOrEmpty(_builds))
                {
                    _builds = DefaultConfiguration.GetAppSetting("Builds", "C:\\bam\\build");
                }
                return _builds;
            }
        }

        [ConsoleAction("init", "Initialize nuget related paths to values expected by Bam.Net.")]
        public static void Init()
        {
            BakeSettings settings = GetSettings();
            string setRepoPath = $"config -Set repositoryPath=\"{settings.PackagesDirectory}\"";
            string setGlobalPath = $"config -Set globalPackagesFolder=\"{settings.GlobalPackagesDirectory}\"";
            OutLineFormat("Setting nuget config properties:\r\n{0}\r\n{1}", setRepoPath, setGlobalPath);
            
            $"{settings.Nuget}".RunAndWait(setRepoPath);
            $"{settings.Nuget}".RunAndWait(setGlobalPath);
        }

        [ConsoleAction("clean", "Clear all local nuget caches by issuing the command: nuget locals all -clear")]
        public static void Clean()
        {
            OutLine("Clearing local nuget caches", ConsoleColor.Cyan);
            $"{NugetPath}".RunAndWait("locals all -clear");
            DeleteDevLatestPackages();
            OutLine("Done clearing local nuget caches", ConsoleColor.Green);
        }

        [ConsoleAction("build", "Build the specified branch.")]
        public static void Build()
        {
            string buildConfigPath = GetArgument("build", "Please enter the path to the build config file.");
            if (!File.Exists(buildConfigPath))
            {
                OutLineFormat("Build config not found: {0}", ConsoleColor.Magenta, buildConfigPath);
                Thread.Sleep(1000);
                Exit(1);
            }
            BuildConfig buildConfig = buildConfigPath.FromJsonFile<BuildConfig>();
            if (string.IsNullOrEmpty(buildConfig.BamBotRoot))
            {
                OutLineFormat("BamBotRoot not defined in config file: {0}", ConsoleColor.Magenta, buildConfigPath);
                Exit(1);
            }

            // get latest
            string cloneIn = $"{buildConfig.BamBotRoot}\\src";
            if (!Directory.Exists(cloneIn))
            {
                Directory.CreateDirectory(cloneIn);
            }
            string clone = Path.Combine(cloneIn, buildConfig.RepoName);

            CloneRepository(buildConfig, cloneIn, clone);

            CheckoutBranch(buildConfig, clone);

            GetLatest(clone);

            string projectFilePath = Path.Combine(clone, buildConfig.ProjectFile);
            string arguments = $"restore {buildConfig.RestoreReference} -PackagesDirectory {GetArgument("PackagesDirectory", "Please enter the path to restore packages to")}";
            NugetPath.ToStartInfo(arguments, clone).RunAndWait(o => OutLine(o, ConsoleColor.Cyan), e => OutLine(e, ConsoleColor.Magenta));

            FileInfo msbuild = new FileInfo(buildConfig.MsBuildPath);
            if (!msbuild.Exists)
            {
                msbuild = new FileInfo(GetArgument("MsBuildPath", "Please enter the path to msbuild.exe."));
            }
            string msBuildPath = msbuild.FullName;
            //The root path where binaries are built.  Binaries will be in {Builds}{Platform}{FrameworkVersion}\Debug\_{commitHash}
            DirectoryInfo clonedDir = new DirectoryInfo(clone);
            string commitHash = clonedDir.GetCommitHash();
            string outputPath = buildConfig.GetOutputPath(Builds, commitHash);
            string command = $"/t:Build /filelogger /p:OutDir={outputPath};GenerateDocumentation=true;Configuration={buildConfig.Config};Platform=\"{buildConfig.Platform}\";TargetFrameworkVersion={buildConfig.FrameworkVersion};CompilerVersion={buildConfig.FrameworkVersion} {buildConfig.ProjectFile} /m:1";
            OutLineFormat("Using msbuild: {0}", ConsoleColor.Yellow, msBuildPath);
            ProcessOutput output = msBuildPath.ToStartInfo(command, clone)
                .RunAndWait(o => Console.WriteLine(o), e => Console.WriteLine(e), 600000);
            if (output.ExitCode != 0)
            {
                OutLineFormat("Build Failed: {0}", ConsoleColor.Magenta, output.ExitCode);
                Thread.Sleep(1000);
                Exit(output.ExitCode);
            }
            else
            {
                string latestFile = Path.Combine(Builds, "latest");
                BuildInfo buildInfo = new BuildInfo { LastBuild = DateTime.Now.ToLongDateString(), BuildConfig = buildConfig, Commit = commitHash };
                buildInfo.ToJsonFile(latestFile);
                OutLine("Build Succeeded", ConsoleColor.Green);
                OutLineFormat("Files output to\r\n   {0}", ConsoleColor.Cyan, outputPath);
                Thread.Sleep(1000);
                Exit(0);
            }
        }

        [ConsoleAction("deploy", @"Deploy Windows Services from the latest build to the folder C:\bam\sys\")]
        public static void Deploy()
        {
            // read deploy.json
            string deployConfigPath = GetArgument("deploy", "Please enter the path to the deploy config file.");
            if (!File.Exists(deployConfigPath))
            {
                OutLineFormat("Deploy config not found: {0}", ConsoleColor.Magenta, deployConfigPath);
                Thread.Sleep(1000);
                Exit(1);
            }
            DirectoryInfo latestBinaries = GetLatestBuildBinaryDirectory();
            DeployInfo deployInfo = deployConfigPath.FromJsonFile<DeployInfo>();
            // for each windows service
            foreach (WindowsServiceInfo svcInfo in deployInfo.WindowsServices)
            {
                DeployWindowsService(latestBinaries, svcInfo);
            }

            StopAndDeleteBamDaemonOnHosts(deployInfo);

            Dictionary<string, List<DaemonProcess>> daemonsByHost = new Dictionary<string, List<DaemonProcess>>();
            // for each daemon entry 
            foreach (DaemonInfo daemonInfo in deployInfo.Daemons)
            {
                PrepareDaemonFiles(latestBinaries, daemonsByHost, daemonInfo);
            }

            foreach (string host in daemonsByHost.Keys)
            {
                List<DaemonProcess> daemonProcesses = daemonsByHost[host];
                ConfigureAndStartBamDaemon(latestBinaries, host, daemonProcesses);
            }
        }

        [ConsoleAction("test", "Run unit and integration tests for the specified build")]
        public static void Test()
        {
            throw new NotImplementedException();
        }

        [ConsoleAction("latest", "Create dev nuget packages from the latest build, clear nuget caches, delete all existing dev-latest packages from the internal source and republish.")]
        public static void Latest()
        {
            BuildInfo info = GetLatestBuildInfo();
            string latestArg = Arguments["latest"];
            OutLineFormat("{0}", ConsoleColor.Cyan, info.Commit);
            if (!string.IsNullOrEmpty(latestArg) && latestArg.Equals("show"))
            {
                Exit(0);
            }
            Clean();
            DirectoryInfo _binRoot = GetCommitBinaryDirectory(info.Commit.First(8));
            _nugetArg = _binRoot.FullName;
            _suffix = $"Dev-latest";
            DirectoryInfo bamLatest = new DirectoryInfo("C:\\bam\\latest");
            OutLineFormat("Copying {0} to {1}", _binRoot.FullName, bamLatest.FullName);
            bamLatest.Delete(true);
            _binRoot.Copy(bamLatest.FullName, true);
            Nuget();
        }

        [ConsoleAction("commit", "Create dev nuget packages from the specified commit assuming it has been built to the path specified by {Builds} in the config file.")]
        public static void Commit()
        {
            string commitArg = Arguments["commit"];

            DirectoryInfo _binRoot = GetCommitBinaryDirectory(commitArg);
            _nugetArg = _binRoot.FullName;
            _suffix = $"Dev-{GetBuildNum()}-{_binRoot.Name.TruncateFront(1).First(5)}";
            Nuget();
        }

        [ConsoleAction("dev", "Create dev nuget packages from binaries in the specified folder.")]
        public static void Dev()
        {
            _nugetArg = Arguments["dev"];
            int buildNum = GetBuildNum();

            string commit = GetCommitHash();
            _suffix = string.IsNullOrEmpty(commit) ? $"Dev-{buildNum}" : $"Dev-{buildNum}-{commit.First(8)}";
            Nuget();
        }
        
        [ConsoleAction("release", "Create the release from a specified source directory.  The release includes nuget packages and an msi file.")]
        public static void Release()
        {
            // update version
            string targetPath = GetTargetPath();            
            DirectoryInfo srcRoot = GetSourceRoot(targetPath);
            srcRoot.GetCommitHash().SafeWriteToFile(Path.Combine(srcRoot.FullName, typeof(Args).Namespace, "commit"), true);
            GitPath.ToStartInfo("reset --hard", srcRoot.FullName).RunAndWait();
            BamInfo info = GetBamInfo(srcRoot);
            info.VersionString = GetNextVersion(info.VersionString);
            OutLineFormat("Setting version to {0}", ConsoleColor.DarkYellow, info.VersionString);
            Thread.Sleep(3000); // in case someone is curious they'll have a short time to see the version
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
                    else if(Msi())
                    {
                        _nugetArg = settings.OutputPath;
                        Nuget();
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
        public static void Tag()
        {
            DirectoryInfo sourceRoot = GetSourceRoot(GetTargetPath());
            string version = GetVersionTag();
            
            Commit(version, sourceRoot);
            Tag(version, sourceRoot);
        }

        [ConsoleAction("msi", "Build the bam toolkit msi from a set of related wix project files.  The contents of the msi are set to the contents of the specified ReleaseFolder folder.")]
        public static bool Msi()
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
        public static void Nuget()
        {
            string targetPath = GetNugetArgument();
            if (targetPath.Equals("init"))
            {
                Nuspec();
            }
            else if (File.Exists(targetPath))
            {
                FileInfo assemblyFile = new FileInfo(targetPath);
                PackAssembly(assemblyFile);
            }
            else if (Directory.Exists(targetPath))
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
                    if (File.Exists(dllName))
                    {
                        tasks.Add(Task.Run(() => PackAssembly(new FileInfo(dllName))));
                    }
                    else if (File.Exists(exeName))
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
        public static void Bam()
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
        public static void Nuspec()
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
            else if (Directory.Exists(argValue))
            {
                DirectoryInfo target = new DirectoryInfo(argValue);
                string version = GetVersion(target);
                tasks.AddRange(SetSolutionNuspecInfos(target, version, owners, authors, predicate));
            }
            else if (File.Exists(argValue))
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
                    if (!File.Exists(nuspecFile.Path))
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
            if (!File.Exists(bamInfoPath))
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

        private static string GetVersionTag()
        {
            string tag = Arguments["tag"];
            if (string.IsNullOrEmpty(tag) && Arguments.Contains("v"))
            {
                tag = Arguments["v"];
            }
            return tag;
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
                SetPatch(packageVersion);
                SetMinor(packageVersion);
                SetMajor(packageVersion);
            }

            return packageVersion.Value;
        }

        private static void SetMajor(PackageVersion packageVersion)
        {
            if (Arguments.Contains("major"))
            {
                string major = Arguments["major"];
                if (string.IsNullOrEmpty(major))
                {
                    packageVersion.IncrementMajor();
                }
                else
                {
                    packageVersion.Major = major;
                }
            }
        }

        private static void SetMinor(PackageVersion packageVersion)
        {
            if (Arguments.Contains("minor"))
            {
                string minor = Arguments["minor"];
                if (string.IsNullOrEmpty(minor))
                {
                    packageVersion.IncrementMinor();
                }
                else
                {
                    packageVersion.Minor = minor;
                }
            }
        }

        private static void SetPatch(PackageVersion packageVersion)
        {
            if (Arguments.Contains("patch"))
            {
                string patch = Arguments["patch"];
                if (string.IsNullOrEmpty(patch))
                {
                    packageVersion.IncrementPatch();
                }
                else
                {
                    packageVersion.Patch = patch;
                }
            }
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
            if (!Directory.Exists(libPath))
            {
                Directory.CreateDirectory(libPath);
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
            if (File.Exists(existingNuspec))
            {
                File.Copy(existingNuspec, stageNuspec, true);
            }
            NuspecFile nuspecFile = new NuspecFile(stageNuspec);
            if (!File.Exists(nuspecFile.Path))
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
                GlobalPackagesDirectory = GetArgument("GlobalPackagesDirectory", "Please enter the path to the global packages directory.")
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

        private static int GetBuildNum()
        {
            int buildNum = 1;
            string buildFile = Path.Combine(_nugetArg, "buildnum");
            if (File.Exists(buildFile))
            {
                buildNum += buildFile.SafeReadFile().ToInt();
            }
            buildNum.ToString().SafeWriteToFile(buildFile, true);
            return buildNum;
        }

        private static string GetCommitHash()
        {
            string commit = _nugetArg.CommitHash();
            string commitFile = Path.Combine(_nugetArg, "commit");
            if (string.IsNullOrEmpty(commit) && File.Exists(commitFile))
            {
                commit = commitFile.SafeReadFile().Trim();
            }

            return commit;
        }

        private static void GetLatest(string clone)
        {
            OutLineFormat("Getting latest: {0}", ConsoleColor.DarkGreen, clone);
            ProcessOutput pullProcess = GitPath.ToStartInfo("pull", clone).RunAndWait(o => OutLine(o, ConsoleColor.Cyan), e => OutLine(e, ConsoleColor.Blue));
            if (pullProcess.ExitCode != 0)
            {
                OutLineFormat("Failed to checkout branch: \r\n\t{0}\r\n\t{1}", ConsoleColor.Magenta, pullProcess.StandardOutput, pullProcess.StandardError);
                Thread.Sleep(1000);
                Exit(pullProcess.ExitCode);
            }
        }

        private static void CheckoutBranch(BuildConfig buildConfig, string clone)
        {
            OutLineFormat("Checking out branch: {0}", ConsoleColor.DarkGray, buildConfig.Branch);
            ProcessOutput checkoutProcess = GitPath.ToStartInfo($"checkout {buildConfig.Branch}", clone).RunAndWait(o => OutLine(o, ConsoleColor.Cyan), e => OutLine(e, ConsoleColor.Blue));
            if (checkoutProcess.ExitCode != 0)
            {
                OutLineFormat("Failed to checkout branch: \r\n\t{0}\r\n\t{1}", ConsoleColor.Magenta, checkoutProcess.StandardOutput, checkoutProcess.StandardError);
                Thread.Sleep(1000);
                Exit(checkoutProcess.ExitCode);
            }
        }

        private static void CloneRepository(BuildConfig buildConfig, string cloneIn, string clone)
        {
            if (Directory.Exists(clone) && buildConfig.Clean)
            {
                try
                {
                    Directory.Delete(clone, true);
                }
                catch (Exception ex)
                {
                    OutLineFormat("Yikes! Error on clean: {0}", ConsoleColor.DarkYellow, ex.Message);
                }
            }

            if (!Directory.Exists(clone))
            {
                OutLineFormat("Cloning repository to: {0}", ConsoleColor.DarkGreen, clone);
                ProcessOutput cloneProcess = GitPath.ToStartInfo($"clone {buildConfig.RepoRoot}/{buildConfig.RepoName}", cloneIn).RunAndWait(o => OutLine(o, ConsoleColor.Cyan), e => OutLine(e, ConsoleColor.Blue));
                if (cloneProcess.ExitCode != 0)
                {
                    OutLineFormat("Failed to get latest: \r\n\t{0}\r\n\t{1}", ConsoleColor.Magenta, cloneProcess.StandardOutput, cloneProcess.StandardError);
                    Thread.Sleep(1000);
                    Exit(cloneProcess.ExitCode);
                }
            }
        }

        private static DirectoryInfo GetLatestBuildBinaryDirectory()
        {
            return GetCommitBinaryDirectory(GetLatestBuildCommitHash());
        }

        private static DirectoryInfo GetCommitBinaryDirectory(string commitPrefix)
        {
            //{Builds}{Platform}{FrameworkVersion}\Debug\_{commitHash}
            DirectoryInfo debugRoot = new DirectoryInfo(Path.Combine(Builds, Platform, FrameworkVersion, "Debug"));
            DirectoryInfo[] subDirectories = debugRoot.GetDirectories($"_{commitPrefix}*");
            if (subDirectories.Length == 0)
            {
                OutLineFormat("Specified commit was not found in the builds directory ({0}): {1}", ConsoleColor.Magenta, debugRoot.FullName, commitPrefix);
                Exit(1);
            }
            if (subDirectories.Length > 1)
            {
                OutLineFormat("Multiple directories found for specific commit hash prefix ({0}), specifiy more characters of the hash to isolate a single commit", ConsoleColor.Magenta, commitPrefix);
                Exit(1);
            }
            DirectoryInfo _binRoot = subDirectories[0];
            return _binRoot;
        }

        private static string GetLatestBuildCommitHash()
        {
            return GetLatestBuildInfo().Commit;
        }

        private static BuildInfo GetLatestBuildInfo()
        {
            string latestFile = Path.Combine(Builds, "latest");
            if (!File.Exists(latestFile))
            {
                OutLineFormat("The file {0} was not found", ConsoleColor.Magenta, latestFile);
                Exit(1);
            }
            BuildInfo info = latestFile.FromJsonFile<BuildInfo>();
            return info;
        }

        private static void DeleteDevLatestPackages()
        {
            OutLineFormat("Deleting *-Dev-latest from {0}", ConsoleColor.Yellow, NugetInternalSource);
            DirectoryInfo internalSource = new DirectoryInfo(NugetInternalSource);
            foreach (DirectoryInfo packageDirectory in internalSource.GetDirectories())
            {
                foreach (DirectoryInfo devLatestDirectory in packageDirectory.GetDirectories("*-Dev-latest"))
                {
                    OutLineFormat("Deleting {0}", ConsoleColor.Yellow, devLatestDirectory.FullName);
                    devLatestDirectory.Delete(true);
                }
            }
        }

        private static void CallServiceExecutable(WindowsServiceInfo svcInfo, string action, string pathOnRemote, string commandSwitch)
        {
            string host = svcInfo.Host;
            string name = svcInfo.Name;

            CallServiceExecutable(host, name, action, pathOnRemote, commandSwitch);
        }

        private static void CallServiceExecutable(string host, string serviceName, string action, string pathOnRemote, string commandSwitch)
        {
            OutLineFormat("Trying to {0} service {1} on {2} > {3}", ConsoleColor.Yellow, action, serviceName, host, pathOnRemote);
            ProcessOutput uninstallOutput = PsExec.Run(host, $"{pathOnRemote} {commandSwitch}");
            OutLineFormat("{0} output:\r\n{1}", ConsoleColor.DarkYellow, action, uninstallOutput.StandardOutput);
            if (!string.IsNullOrEmpty(uninstallOutput.StandardError))
            {
                OutLineFormat("{0} error output:\r\n{1}", ConsoleColor.DarkMagenta, action, uninstallOutput.StandardError);
            }
        }

        private static void SetAppSettings(string host, string directoryPathOnRemote, string fileName, Dictionary<string, string> appSettings)
        {
            string configPath = Path.Combine(directoryPathOnRemote, $"{fileName}.config");
            FileInfo configFile = new FileInfo(configPath);
            string adminSharePath = configFile.GetAdminSharePath(host);
            OutLineFormat("Setting appsettings in {0}", ConsoleColor.DarkCyan, adminSharePath);
            DefaultConfiguration.SetAppSettings(adminSharePath, appSettings);
        }
        
        private static void DeployWindowsService(DirectoryInfo latestBinaries, WindowsServiceInfo svcInfo)
        {
            Args.ThrowIf(string.IsNullOrEmpty(svcInfo.Host), "Host not specified");
            Args.ThrowIf(string.IsNullOrEmpty(svcInfo.Name), "Name not specified");
            //      copy the latest binaries to \\computer\c$\bam\tools\{Name}
            string remoteDirectory = Path.Combine(Paths.Sys, svcInfo.Name);
            string remoteFile = Path.Combine(remoteDirectory, svcInfo.FileName);

            DirectoryInfo remoteDirectoryInfo = remoteDirectory.GetAdminShareDirectory(svcInfo.Host);
            if (remoteDirectoryInfo.Exists)
            {
                CallServiceExecutable(svcInfo, "Kill", remoteFile, "-k");
                CallServiceExecutable(svcInfo, "Un-install", remoteFile, "-u");
                string host = svcInfo.Host;
                TryDeleteDirectory(remoteDirectoryInfo, host);
            }

            OutLineFormat("Copying files for {0} to {1}", ConsoleColor.Cyan, svcInfo.Name, svcInfo.Host);
            latestBinaries.CopyTo(svcInfo.Host, remoteDirectory);
            CallServiceExecutable(svcInfo, "Install", remoteFile, "-i");

            if (svcInfo.AppSettings != null)
            {
                SetAppSettings(svcInfo.Host, remoteDirectory, svcInfo.FileName, svcInfo.AppSettings);
            }

            CallServiceExecutable(svcInfo, "Start", remoteFile, "-s");
        }

        private static void StopAndDeleteBamDaemonOnHosts(DeployInfo deployInfo)
        {
            string bamdLocalPathNotation = Path.Combine(Paths.Sys, "bamd", "bamd.exe");
            OutLineFormat("Stopping Bam Daemon on each host", ConsoleColor.DarkCyan);
            DirectoryInfo bamdLocalPath = new FileInfo(bamdLocalPathNotation).Directory;
            HashSet<string> daemonHosts = new HashSet<string>(deployInfo.Daemons.Select(di => di.Host).ToList());
            foreach (string host in daemonHosts)
            {
                DirectoryInfo remoteDirectoryInfo = bamdLocalPath.FullName.GetAdminShareDirectory(host);
                if (remoteDirectoryInfo.Exists)
                {
                    CallServiceExecutable(host, "bamd", "Kill", bamdLocalPathNotation, "-k");
                    CallServiceExecutable(host, "bamd", "Un-install", bamdLocalPathNotation, "-u");

                    TryDeleteDirectory(remoteDirectoryInfo, host);
                }
            }
        }

        private static void ConfigureAndStartBamDaemon(DirectoryInfo latestBinaries, string host, List<DaemonProcess> daemonProcesses)
        {
            string bamdLocalPathNotation = Path.Combine(Paths.Sys, "bamd", "bamd.exe");
            OutLineFormat("Copying bamd to remote: {0}", ConsoleColor.Cyan, host);
            //      copy new bamd
            latestBinaries.CopyTo(host, new FileInfo(bamdLocalPathNotation).Directory.FullName);
            OutLineFormat("Done copying bamd to remote: {0}", ConsoleColor.Cyan, host);

            CallServiceExecutable(host, "bamd", "Install", bamdLocalPathNotation, "-i");

            //      write DaemonProcessInfos using above
            string daemonConfig = Path.Combine(Paths.Conf, $"{typeof(DaemonProcess).Name.Pluralize()}.json");
            daemonProcesses.ToJsonFile(daemonConfig);

            //      start new bamd
            CallServiceExecutable(host, "bamd", "Start", bamdLocalPathNotation, "-s");
        }

        private static void PrepareDaemonFiles(DirectoryInfo latestBinaries, Dictionary<string, List<DaemonProcess>> daemonsByHost, DaemonInfo daemonInfo)
        {
            Args.ThrowIf(string.IsNullOrEmpty(daemonInfo.Host), "Host not specified");
            Args.ThrowIf(string.IsNullOrEmpty(daemonInfo.Name), "Name not specified");
            if (!daemonsByHost.ContainsKey(daemonInfo.Host))
            {
                daemonsByHost.Add(daemonInfo.Host, new List<DaemonProcess>());
            }
            //      copy the latest binaries to c:\bam\sys\{Name}
            //      prepare array of DaemonProcessInfos with appropriate settings derived from info to be written to bamd below
            string directoryPathOnRemote = Path.Combine(Paths.Sys, daemonInfo.Name);
            if (daemonInfo.Copy)
            {
                DirectoryInfo adminSharePath = directoryPathOnRemote.GetAdminShareDirectory(daemonInfo.Host);
                if (adminSharePath.Exists)
                {
                    adminSharePath.Delete(true);
                }
                daemonInfo.WorkingDirectory = directoryPathOnRemote;
                latestBinaries.CopyTo(daemonInfo.Host, directoryPathOnRemote);
                daemonsByHost[daemonInfo.Host].Add(daemonInfo.ToDaemonProcess(directoryPathOnRemote));
            }
            else
            {
                daemonsByHost[daemonInfo.Host].Add(daemonInfo.ToDaemonProcess());
            }

            if (daemonInfo.AppSettings != null)
            {
                //      update the appsettings to match what's in the info entry; Use "SetAppSettings"
                SetAppSettings(daemonInfo.Host, directoryPathOnRemote, daemonInfo.FileName, daemonInfo.AppSettings);
            }
        }

        private static void TryDeleteDirectory(DirectoryInfo remoteDirectoryInfo, string host)
        {
            try
            {
                OutLineFormat("Deleting {0} from {1}", ConsoleColor.DarkYellow, remoteDirectoryInfo.FullName, host);
                remoteDirectoryInfo.Delete(true);
            }
            catch (Exception ex)
            {
                OutLineFormat("Exception occurred deleting {0}: {1} ", ConsoleColor.DarkMagenta, remoteDirectoryInfo.FullName, ex.Message);
            }
        }
    }
}
