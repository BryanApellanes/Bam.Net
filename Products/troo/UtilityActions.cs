using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Data.Repositories;
using System.Reflection;
using Bam.Net.Data;
using System.IO;
using Bam.Net;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;

namespace troo
{
    [Serializable]
    public class UtilityActions : CommandLineTestInterface
    {
        static FileInfo LastGenerationInfo = new FileInfo(".\\troo_generation_info.json");
        [ConsoleAction("generateSchemaRepository", "Generate a schema specific DaoRepository")]
        public static void GenerateSchemaRepository()
        {
            GenerationInfo genInfo = GetGenerationInfo();
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            SchemaRepositoryGenerator schemaGen = new SchemaRepositoryGenerator(genInfo.Assembly, genInfo.FromNameSpace, logger);            
            schemaGen.CheckIdField = GetArgument("checkForIds", "Check for Id field?").IsAffirmative();
            schemaGen.BaseRepositoryType = GetArgument("useInheritanceSchema", "Use inheritance schema?").IsAffirmative() ? "DatabaseRepository" : "DaoRepository";
            string targetDir = GetArgument("writeSrc", "Please enter the directory to write source to");
            if (Directory.Exists(targetDir))
            {
                Directory.Move(targetDir, targetDir.GetNextDirectoryName());
            }
            schemaGen.GenerateRepositorySource(
                targetDir,
                GetArgument("schemaRepositoryName", "Please enter the name of the schema to assign to the repository"));

            if(schemaGen.Warnings.MissingKeyColumns.Length > 0)
            {
                OutLine("Missing key/id columns", ConsoleColor.Yellow);
                schemaGen.Warnings.MissingKeyColumns.Each(kc =>
                {
                    OutLineFormat("\t{0}", kc.TableClassName, ConsoleColor.DarkYellow);
                });
            }
            if(schemaGen.Warnings.MissingForeignKeyColumns.Length > 0)
            {
                OutLine("Missing ForeignKey columns", ConsoleColor.Cyan);
                schemaGen.Warnings.MissingForeignKeyColumns.Each(fkc =>
                {
                    OutLineFormat("\t{0}.{1}", ConsoleColor.DarkCyan, fkc.TableClassName, fkc.Name);
                });
            }
        }

        [ConsoleAction("generateDaoAssemblyForTypes", "Generate Dao Assembly for types")]
        public static void GenerateDaoForTypes()
        {
            GenerationInfo genInfo = GetGenerationInfo();
            Assembly typeAssembly = genInfo.Assembly;
            string schemaName = genInfo.SchemaName;
            string fromNameSpace = genInfo.FromNameSpace;
            string toNameSpace = genInfo.ToNameSpace;

            DaoRepository repo = new DaoRepository(new SQLiteDatabase(".", schemaName), new ConsoleLogger(), schemaName);
            repo.Namespace = toNameSpace;
            repo.AddNamespace(typeAssembly, fromNameSpace);
            Assembly daoAssembly = repo.GenerateDaoAssembly(false);
            FileInfo fileInfo = daoAssembly.GetFileInfo();
            string copyTo = Path.Combine(GetArgument("writeTo", "Please enter the directory to copy the resulting assembly to"), fileInfo.Name);
            fileInfo.CopyTo(copyTo, true);
            OutLineFormat("File generated:\r\n{0}", copyTo);
            Pause("Press enter to continue...");
        }

        [ConsoleAction("generateDaoCodeForTypes", "Generate Dao code for types")]
        public static void GenerateDaoCodeForTypes()
        {
            GenerationInfo genInfo = GetGenerationInfo();
            Assembly typeAssembly = genInfo.Assembly;
            string schemaName = genInfo.SchemaName;
            string fromNameSpace = genInfo.FromNameSpace;
            string toNameSpace = genInfo.ToNameSpace;
            string defaultPath = $".\\{schemaName}_Generated";
            DirectoryInfo defaultDir = new DirectoryInfo(defaultPath);
            defaultPath = defaultDir.FullName;
            string writeTo = GetArgument("writeTo", $"Please enter the path to write code to (default ({defaultPath}))").Or(defaultPath);
            DirectoryInfo writeToDir = new DirectoryInfo(writeTo);
            if (writeToDir.Exists)
            {
                Directory.Move(writeToDir.FullName, $"{writeToDir.FullName}_{DateTime.Now.ToJulianDate()}");
            }
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            TypeDaoGenerator generator = new TypeDaoGenerator(typeAssembly, fromNameSpace, logger);
            generator.WarningsAsErrors = true;
            generator.ThrowWarningsIfWarningsAsErrors();
            generator.Namespace = toNameSpace;
            generator.GenerateSource(writeTo);
            logger.BlockUntilEventQueueIsEmpty(1000);
        }

        [ConsoleAction("generateDtosForDaos", "Generate Dtos for Daos")]
        public static void GenerateDtosForDaos()
        {
            DaoToDtoGenerator generator = new DaoToDtoGenerator();
            string assemblyPath = GetArgument("assemblyPath", "Please enter the path to the dao assembly");
            FileInfo file = new FileInfo(assemblyPath);
            if (!file.Exists)
            {
                OutLineFormat("File not found: {0}", ConsoleColor.Magenta, file.FullName);
                Exit(1);
            }
            string defaultPath = $".\\{file.Name}_Dto_Generated\\";
            string sourcePath = GetArgument("srcPath", $"Please enter the path to the write source to [{defaultPath}]").Or(defaultPath);
            bool keepSource = Confirm("Keep source files?");
            bool compile = Confirm("Generate assembly?");
            generator.DaoAssembly = Assembly.LoadFrom(file.FullName);
            generator.WriteDtoSource(sourcePath);
            DirectoryInfo srcDir = new DirectoryInfo(sourcePath);
            if (compile)
            {
                Assembly result = srcDir.ToAssembly($"{file.Name}_Dtos.dll");
                OutLineFormat("Created assembly {0}", ConsoleColor.Cyan, result.GetFilePath());
            }
            if (!keepSource)
            {
                Directory.Delete(sourcePath, true);
            }
        }

        private static GenerationInfo GetGenerationInfo()
        {
            Assembly typeAssembly = Assembly.LoadFrom(GetArgument("typeAssembly", "Please enter the path to the assembly containing the types to generate daos for"));
            string schemaName = GetArgument("schemaName", "Please enter the schema name to use").Replace(".", "_");
            string fromNameSpace = GetArgument("fromNameSpace", "Please enter the namespace containing the types to generate daos for");
            string toNameSpace = $"{fromNameSpace}.Dao";
            GenerationInfo result = new GenerationInfo { Assembly = typeAssembly, SchemaName = schemaName, FromNameSpace = fromNameSpace, ToNameSpace = toNameSpace };           
            return result;
        }
    }
}
