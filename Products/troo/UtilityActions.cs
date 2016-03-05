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

namespace troo
{
    [Serializable]
    public class UtilityActions : CommandLineTestInterface
    {
        [ConsoleAction("generateDtosFromDaos", "Generate Dtos from a dao assembly")]
        public static void GenerateDtosFromDaos()
        {
            Assembly daoAssembly = Assembly.LoadFrom(GetArgument("daoAssembly", "Please enter the path to the Dao assembly to generate Dtos for"));
            DaoToDtoGenerator gen = new DaoToDtoGenerator(daoAssembly);

            if (Arguments.Contains("compile"))
            {
                GeneratedAssemblyInfo info = gen.GenerateAssembly();
                FileInfo assemblyFile = new FileInfo(info.AssemblyFilePath);
                File.Copy(info.AssemblyFilePath, Path.Combine(".", assemblyFile.Name), true);
                Directory.Delete(gen.TempDir, true);                
            }
            else
            {
                string sourceDirPath = GetArgument("sourceDir", "Please enter the path to the directory to write source files to");
                DirectoryInfo sourceDir = new DirectoryInfo(sourceDirPath);
                if (!sourceDir.Exists)
                {
                    sourceDir.Create();
                }
                gen.WriteDtoSource(sourceDir.FullName);
            }
        }
    }
}
