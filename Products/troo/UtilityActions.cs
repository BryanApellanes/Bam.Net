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

        [ConsoleAction("generateDaoAssemblyForTypes", "Generate Dao Assebly for types")]
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

        private static GenerationInfo GetGenerationInfo()
        {
            GenerationInfo result = null;
            //if (LastGenerationInfo.Exists)
            //{
            //    result = LastGenerationInfo.FromJson<GenerationInfo>();
            //    OutLine(result.PropertiesToString());
            //    if(Confirm("Use previous configuration?"))
            //    {
            //        return result;
            //    }
            //}
            Assembly typeAssembly = Assembly.LoadFrom(GetArgument("typeAssembly", "Please enter the path to the assembly containing the types to generate daos for"));
            string schemaName = GetArgument("schemaName", "Please enter the schema name to use").Replace(".", "_");
            string fromNameSpace = GetArgument("fromNameSpace", "Please enter the namespace containing the types to generate daos for");
            string toNameSpace = $"{fromNameSpace}._Dao_";
            result = new GenerationInfo { Assembly = typeAssembly, SchemaName = schemaName, FromNameSpace = fromNameSpace, ToNameSpace = toNameSpace };
            result.ToJsonFile(LastGenerationInfo);
            return result;
        }
    }
}
