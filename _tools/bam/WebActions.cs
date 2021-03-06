﻿using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Messaging;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Automation.MSBuild;
using Bam.Net.Presentation.Handlebars;
using System.Yaml;
using System.Threading;
using System.Reflection;
using Bam.Net.Data.Dynamic;
using Bam.Net.Data.Dynamic.Data;

namespace Bam.Net.Application
{
    [Serializable]
    public class WebActions : CommandLineTestInterface
    {
        public const string AppDataFolderName = "AppData";
        public const string GenerationOutputFolderName = "_gen";
        
        static DirectoryInfo _appData;
        static object _appDataLock = new object();
        static DirectoryInfo AppData
        {
            get
            {
                return _appDataLock.DoubleCheckLock(ref _appData, () => new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, AppDataFolderName)));
            }
        }        

        [ConsoleAction("init", "Add BamFramework to the current csproj")]
        public void Init()
        {
            // find the first csproj file by looking first in the current directory then going up
            // using the parent of the csproj as the root
            // - clone bam.js into wwwroot/bam.js
            // - add Pages/Api/V1.cshtml
            // - add Pages/Api/V1.cshtml.cs
            DirectoryInfo projectParent = FindProjectParent(out FileInfo csprojFile);
            if(csprojFile == null)
            {
                OutLine("Can't find csproj file", ConsoleColor.Magenta);
                
                Thread.Sleep(3000);
                Exit(1);
            }
            DirectoryInfo wwwroot = new DirectoryInfo(Path.Combine(projectParent.FullName, "wwwroot"));
            if (!wwwroot.Exists)
            {
                Warn("{0} doesn't exist, creating...", wwwroot.FullName);
                wwwroot.Create();
            }
            string bamJsPath = Path.Combine(wwwroot.FullName, "bam.js");
            if (!Directory.Exists(bamJsPath))
            {
                OutLineFormat("Cloning bam.js to {0}", ConsoleColor.Yellow, bamJsPath);
                string cloneCommand = "git clone https://github.com/BryanApellanes/bam.js.git wwwroot/bam.js";
                cloneCommand.Run((output) => OutLine(output, ConsoleColor.Cyan));
            }

            WriteStartupCs(csprojFile);
            WriteBaseAppModules(csprojFile);          
        }

        [ConsoleAction("addPage", "Add a page to the current BamFramework project")]
        public void AddPage()
        {
            string pageName = GetArgument("addPage", "Please enter the name of the page to create");
            if (string.IsNullOrEmpty(pageName))
            {
                OutLine("Page name not specified", ConsoleColor.Magenta);
                Exit(1);
            }

            // find the first csproj file by looking first in the current directory then going up
            // using the parent of the csproj as the root, add the files
            // - Pages/[pagePath].cshtml
            // - Pages/[pagePath].cshtml.cs
            // - wwwroot/bam.js/pages/[pagePath].js
            // - wwwroot/bam.js/configs/[pagePath]/webpack.config.js
            DirectoryInfo projectParent = FindProjectParent(out FileInfo csprojFile);
            if (csprojFile == null)
            {
                OutLine("Can't find csproj file", ConsoleColor.Magenta);
                Exit(1);
            }
            AddPage(csprojFile, pageName);
        }

        [ConsoleAction("gen", "src|bin|dbjs|repo|all", "Generate a dynamic type assembly for json and yaml data")]
        public void GenerateDataModels()
        {   
            GenerationTargets target = Arguments["gen"].ToEnum<GenerationTargets>();
            switch (target)
            {
                case GenerationTargets.Invalid:
                    throw new InvalidOperationException("Invalid generation target specified");
                case GenerationTargets.src:
                    GenerateDynamicTypeSource();
                    break;
                case GenerationTargets.bin:
                    GenerateDynamicTypeAssemblies();
                    break;
                case GenerationTargets.dbjs:
                    GenerateDaoFromDbJsFiles();
                    break;
                case GenerationTargets.repo:
                    GenerateSchemaRepository();
                    break;
                case GenerationTargets.all:
                default:
                    GenerateDynamicTypeSource();
                    GenerateDaoFromDbJsFiles();
                    GenerateDynamicTypeAssemblies();
                    GenerateSchemaRepository();
                    break;
            }

        }

        [ConsoleAction("clean", "Clear all dynamic types and namespaces from the dynamic type manager")]
        public void CleanGeneratedTypes()
        {
            OutLine("Deleting ALL dynamic types from the local DynamicTypeManager", ConsoleColor.Yellow);
            DynamicTypeManager mgr = new DynamicTypeManager();
            mgr.DynamicTypeDataRepository.Query<DynamicTypePropertyDescriptor>(p => p.Id > 0).Each(p => mgr.DynamicTypeDataRepository.Delete(p));
            mgr.DynamicTypeDataRepository.Query<DynamicTypeDescriptor>(d => d.Id > 0).Each(d => mgr.DynamicTypeDataRepository.Delete(d));
            mgr.DynamicTypeDataRepository.Query<DynamicNamespaceDescriptor>(d => d.Id > 0).Each(d => mgr.DynamicTypeDataRepository.Delete(d));
            OutLine("Done", ConsoleColor.DarkYellow);
        }
        
        [ConsoleAction("webpack", "WebPack each bam.js page found in wwwroot/bam.js/pages using corresponding configs found in wwwroot/bam.js/configs")]
        public void WebPack()
        {
            // find the first csproj file by looking first in the current directory then going up
            // using the parent of the csproj as the root
            // change directories into wwwroot/bam.js
            // for every webpack.config.js file in ./configs/ call
            // npx  webpack --config [configPath]

            string startDir = Environment.CurrentDirectory;
            DirectoryInfo projectParent = FindProjectParent(out FileInfo csprojFile);
            if (csprojFile == null)
            {
                OutLine("Can't find csproj file", ConsoleColor.Magenta);
                Exit(1);
            }
            DirectoryInfo wwwroot = new DirectoryInfo(Path.Combine(projectParent.FullName, "wwwroot"));
            if (!wwwroot.Exists)
            {
                OutLineFormat("{0} doesn't exist", ConsoleColor.Magenta, wwwroot.FullName);
                Exit(1);
            }
            DirectoryInfo bamJs = new DirectoryInfo(Path.Combine(wwwroot.FullName, "bam.js"));
            if (!bamJs.Exists)
            {
                OutLineFormat("{0} doesn't exist", ConsoleColor.Magenta, bamJs.FullName);
            }
            Environment.CurrentDirectory = bamJs.FullName;
            DirectoryInfo configs = new DirectoryInfo(Path.Combine(bamJs.FullName, "configs"));
            foreach (FileInfo config in configs.GetFiles("webpack.config.js", SearchOption.AllDirectories))
            {
                string configPath = config.FullName.Replace(configs.FullName, "");
                if (configPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    configPath = configPath.TruncateFront(1);
                }
                OutLineFormat("Packing {0}", ConsoleColor.Green, configPath);
                $"npx webpack --config {configPath}".Run(output => OutLine(output, ConsoleColor.Cyan));
            }
            Environment.CurrentDirectory = startDir;
        }

        private void GenerateDaoFromDbJsFiles()
        {
            //laotze.exe / root:[PATH-TO-DIRECTORY-CONTAINING-DBJS] /keep /s
            string writeTo = new DirectoryInfo(Path.Combine(AppData.FullName, "_gen", "src", "dao")).FullName;
            ProcessOutput output = $"laotze.exe /root:\"{AppData.FullName}\" /gen:\"{writeTo}\" /keep /s".Run(o => OutLine(o, ConsoleColor.DarkCyan), 100000);

            if (output.ExitCode != 0)
            {
                OutLineFormat("Dao generation from *.db.js files exited with code {0}: {1}", ConsoleColor.Yellow, output.ExitCode, output.StandardError.Substring(output.StandardError.Length - 300));
            }
        }

        private void GenerateSchemaRepository()
        {
            OutLineFormat("Generating Dao repository for AppModels", ConsoleColor.Cyan);
            FileInfo csprojFile = FindProjectFile();
            string schemaName = $"{Path.GetFileNameWithoutExtension(csprojFile.Name).Replace("_", "").Replace("-", "").Replace(".", "")}Schema";
            string dotnetTemp = new DirectoryInfo(Path.Combine(csprojFile.Directory.FullName, "AppData", "_gen", "dotnet")).FullName;
            ProcessOutput dotnetOutput = $"dotnet publish --output \"{dotnetTemp}\"".Run(o => OutLine(o, ConsoleColor.DarkGray), 100000);
            Assembly dotnetAssembly = Assembly.LoadFile(Path.Combine(dotnetTemp, $"{Path.GetFileNameWithoutExtension(csprojFile.Name)}.dll"));

            string fromNamespace = GetAppModelsNamespace(dotnetAssembly);
            if (string.IsNullOrEmpty(fromNamespace))
            {
                return;
            }
            string toNamespace = $"{fromNamespace}.GeneratedDao";
            GenerationSettings generationSettings = new GenerationSettings
            {
                Assembly = dotnetAssembly,
                SchemaName = schemaName,
                UseInheritanceSchema = false,
                FromNameSpace = fromNamespace,
                ToNameSpace = toNamespace,
                WriteSourceTo = Path.Combine(AppData.FullName, "_gen", "src", $"{schemaName}_Dao")
            };
            GenerateSchemaRepository(generationSettings);
        }

        private void GenerateSchemaRepository(GenerationSettings generationSettings)
        {
            ProcessOutput output = $"troo.exe /generateSchemaRepository /typeAssembly:\"{generationSettings.Assembly.GetFilePath()}\" /schemaName:{generationSettings.SchemaName} /fromNameSpace:{generationSettings.FromNameSpace} /checkForIds:yes /useInhertianceSchema:{generationSettings.UseInheritanceSchema.ToString()} /writeSource:\"{generationSettings.WriteSourceTo}\"".Run(o => OutLine(o, ConsoleColor.DarkGreen), 100000);

            if (output.ExitCode != 0)
            {
                OutLineFormat("Schema generation exited with code {0}: {1}", ConsoleColor.Yellow, output.ExitCode, output.StandardError.Substring(output.StandardError.Length - 300));
            }
        }

        private void GenerateDynamicTypeSource()
        {
            OutLineFormat("Generating dynamic types from json ({0}) and yaml ({1}).", Path.Combine(AppData.FullName, "json"), Path.Combine(AppData.FullName, "yaml"));
            DynamicTypeManager dynamicTypeManager = new DynamicTypeManager();
            FileInfo csprojFile = FindProjectFile();
            if (csprojFile == null)
            {
                throw new InvalidOperationException("Couldn't find project file");
            }
            string source = dynamicTypeManager.GenerateSource(AppData, Path.GetFileNameWithoutExtension(csprojFile.Name));
            Expect.IsNotNullOrEmpty(source, "Source was not generated");

            OutLineFormat("Generated source: {0}", ConsoleColor.DarkCyan, source.Sha256());
        }

        private void GenerateDynamicTypeAssemblies()
        {
            DynamicTypeManager dynamicTypeManager = new DynamicTypeManager();
            Assembly assembly = dynamicTypeManager.GenerateAssembly(AppData);

            Expect.IsNotNull(assembly, "Assembly was not generated");
            Expect.IsGreaterThan(assembly.GetTypes().Length, 0, "No types were found in the generated assembly");

            foreach (Type type in assembly.GetTypes())
            {
                OutLineFormat("{0}.{1}", ConsoleColor.Cyan, type.Namespace, type.Name);
            }
        }

        private void AddPage(FileInfo csprojFile, string pageName)
        {
            DirectoryInfo projectParent = csprojFile.Directory;
            string appName = Path.GetFileNameWithoutExtension(csprojFile.Name);
            DirectoryInfo pagesDirectory = new DirectoryInfo(Path.Combine(projectParent.FullName, "Pages"));
            PageRenderModel pageRenderModel = new PageRenderModel { BaseNamespace = $"{appName}", PageName = pageName };

            HandlebarsDirectory handlebarsDirectory = GetHandlebarsDirectory();

            string csHtmlFilePath = Path.Combine(pagesDirectory.FullName, $"{pageName}.cshtml");            
            if (!File.Exists(csHtmlFilePath))
            {
                EnsureDirectoryExists(csHtmlFilePath);
                string pageContent = handlebarsDirectory.Render("Page.cshtml", pageRenderModel);
                pageContent.SafeWriteToFile(csHtmlFilePath, true);
            }

            string csHtmlcsFilePath = $"{csHtmlFilePath}.cs";
            if (!File.Exists(csHtmlcsFilePath))
            {
                EnsureDirectoryExists(csHtmlcsFilePath);
                string codeBehindContent = handlebarsDirectory.Render("Page.cshtml.cs", pageRenderModel);
                codeBehindContent.SafeWriteToFile(csHtmlcsFilePath, true);
            }

            AddWebPackConfig(csprojFile, pageName);
        }

        private void EnsureDirectoryExists(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
        }

        private void AddWebPackConfig(FileInfo csprojFile, string pageName)
        {
            DirectoryInfo wwwroot = new DirectoryInfo(Path.Combine(csprojFile.Directory.FullName, "wwwroot"));
            DirectoryInfo projectParent = csprojFile.Directory;
            string appName = Path.GetFileNameWithoutExtension(csprojFile.Name);
            string wwwrootPath = wwwroot.FullName;
            if (!wwwrootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                wwwrootPath += Path.DirectorySeparatorChar.ToString();
            }
            PageRenderModel pageRenderModel = new PageRenderModel { BaseNamespace = $"{appName}", PageName = pageName, WwwRoot = wwwrootPath };

            HandlebarsDirectory handlebarsDirectory = GetHandlebarsDirectory();
            string pageJsPath = Path.Combine(wwwroot.FullName, "bam.js", "pages", $"{pageName}.js");
            string webPackConfigPath = Path.Combine(wwwroot.FullName, "bam.js", "configs", pageName, "webpack.config.js");
            if (!File.Exists(pageJsPath))
            {
                handlebarsDirectory.Render("PageJs", pageRenderModel).SafeWriteToFile(pageJsPath, true);
            }
            if (!File.Exists(webPackConfigPath))
            {
                handlebarsDirectory.Render("WebpackConfig", pageRenderModel).SafeWriteToFile(webPackConfigPath, true);
            }
        }

        private void WriteBaseAppModules(FileInfo csprojFile)
        {
            DirectoryInfo projectParent = csprojFile.Directory;
            DirectoryInfo appModules = new DirectoryInfo(Path.Combine(projectParent.FullName, "AppModules"));
            HandlebarsDirectory handlebarsDirectory = GetHandlebarsDirectory();
            string appName = Path.GetFileNameWithoutExtension(csprojFile.Name);

            AppModuleRenderModel model = new AppModuleRenderModel { BaseNamespace = $"{appName}", AppModuleName = appName };
            foreach(string moduleType in new string[] { "AppModule", "ScopedAppModule", "SingletonAppModule", "TransientAppModule" })
            {
                string moduleContent = handlebarsDirectory.Render($"{moduleType}.cs", model);
                if (string.IsNullOrEmpty(moduleContent))
                {
                    OutLineFormat("{0}: Template for {1} is empty", handlebarsDirectory.Directory.FullName, moduleType);
                }
                string filePath = Path.Combine(appModules.FullName, $"{appName}{moduleType}.cs");
                if (!File.Exists(filePath))
                {
                    moduleContent.SafeWriteToFile(filePath, true);
                    OutLineFormat("Wrote file {0}...", ConsoleColor.Green, filePath);
                }
            }            
        }

        private void WriteStartupCs(FileInfo csprojFile)
        {
            DirectoryInfo projectParent = csprojFile.Directory;
            FileInfo startupCs = new FileInfo(Path.Combine(projectParent.FullName, "Startup.cs"));
            if (startupCs.Exists)
            {
                string moveTo = startupCs.FullName.GetNextFileName();
                File.Move(startupCs.FullName, moveTo);
                OutLineFormat("Moved existing Startup.cs file to {0}", ConsoleColor.Yellow, moveTo);
            }

            HandlebarsDirectory handlebarsDirectory = GetHandlebarsDirectory();
            handlebarsDirectory.Render("Startup.cs", new { BaseNamespace = Path.GetFileNameWithoutExtension(csprojFile.Name) }).SafeWriteToFile(startupCs.FullName, true);
        }

        private HandlebarsDirectory GetHandlebarsDirectory()
        {
            DirectoryInfo bamDir = Assembly.GetExecutingAssembly().GetFileInfo().Directory;
            return new HandlebarsDirectory(Path.Combine(bamDir.FullName, "Templates"));
        }

        private FileInfo FindProjectFile()
        {
            FindProjectParent(out FileInfo csprojFile);
            return csprojFile;
        }

        private DirectoryInfo FindProjectParent(out FileInfo csprojFile)
        {
            string startDir = Environment.CurrentDirectory;
            DirectoryInfo startDirInfo = new DirectoryInfo(startDir);
            DirectoryInfo projectParent = startDirInfo;
            FileInfo[] projectFiles = projectParent.GetFiles("*.csproj", SearchOption.TopDirectoryOnly);
            while (projectFiles.Length == 0)
            {
                if(projectParent.Parent != null)
                {
                    projectParent = projectParent.Parent;
                    projectFiles = projectParent.GetFiles("*.csrpoj", SearchOption.TopDirectoryOnly);
                }
                else
                {
                    break;
                }
            }
            csprojFile = null;
            if (projectFiles.Length > 0)
            {
                csprojFile = projectFiles[0];
            }

            if (projectFiles.Length > 1)
            {
                Warn("Multiple csproject files found, using {0}\r\n{1}", csprojFile.FullName, string.Join("\r\n\t", projectFiles.Select(p => p.FullName).ToArray()));
            }
            return projectParent;
        }

        private string GetAppModelsNamespace(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.Namespace.EndsWith("AppModels"))
                {
                    return type.Namespace;
                }
            }
            OutLineFormat("No AppModels namespaces found", ConsoleColor.Yellow);
            return string.Empty;
        }

    }
}
