/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using System.Reflection;
using System.IO;
using System.CodeDom.Compiler;
using Bam.Net.Logging;
using Bam.Net.Configuration;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// A generator that will create Dto's from Dao's.
    /// Intended primarily to enable backup of
    /// Daos to an ObjectRepository
    /// </summary>
    public class DaoToDtoGenerator : Loggable, IAssemblyGenerator, IWriteSource
    {
        public DaoToDtoGenerator() { }

        public DaoToDtoGenerator(Assembly daoAssembly)
        {
            this.DaoAssembly = daoAssembly;
        }

        public DaoToDtoGenerator(Dao daoInstance)
            : this(daoInstance.GetType().Assembly)
        { }

        public Assembly DaoAssembly
        {
            get;
            set;
        }

        [Verbosity(VerbosityLevel.Warning, MessageFormat = "Unable to delete temp source directory: {TempDir}\r\n{ExceptionMessage}")]
        public event EventHandler DeleteTempSourceDirectoryFailed;

        /// <summary>
        /// Read by Loggable messages if deleting temp directory fails
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Read by Loggable messages if deleting temp directory fails
        /// </summary>
        public string TempDir { get; set; }

        public void WriteSource(string writeSourceTo)
        {
            WriteDtoSource(GetNamespace(), writeSourceTo);
        }
        /// <summary>
        /// Implements IAssemblyGenerator.GenerateAssembly by delegating
        /// to GenerateDtoAssembly
        /// </summary>
        /// <returns></returns>
        public GeneratedAssemblyInfo GenerateAssembly()
        {
            return GenerateDtoAssembly();
        }

        /// <summary>
        /// Generates a Dto assembly
        /// </summary>
        /// <returns></returns>
		public GeneratedAssemblyInfo GenerateDtoAssembly()
        {
            string nameSpace = GetNamespace();
            return GenerateDtoAssembly("{0}.Dtos"._Format(nameSpace));
        }

        public GeneratedAssemblyInfo GenerateDtoAssembly(string nameSpace)
        {
            return GenerateDtoAssembly(nameSpace, GetDefaultFileName());
        }

        public GeneratedAssemblyInfo GenerateDtoAssembly(string nameSpace, string fileName)
        {
            Type oneDao = DaoAssembly.GetTypes().FirstOrDefault(t => t.HasCustomAttributeOfType<TableAttribute>());
            string writeSourceTo = Path.Combine(RuntimeSettings.AppDataFolder, "DtoTemp_{0}"._Format(Dao.ConnectionName(oneDao)));
            DirectoryInfo sourceDir = SetSourceDir(writeSourceTo);

            WriteDtoSource(nameSpace, writeSourceTo);

            CompilerResults results;
            sourceDir.ToAssembly(fileName, out results);
            GeneratedAssemblyInfo result = new GeneratedAssemblyInfo(fileName, results);
            result.Save();
            return result;
        }

        /// <summary>
        /// Write dto source code to the specified directory
        /// </summary>
        /// <param name="dir"></param>
        public void WriteDtoSource(DirectoryInfo dir)
        {
            WriteDtoSource(dir.FullName);
        }

        /// <summary>
        /// Write dto source code to the specified directory
        /// </summary>
        /// <param name="writeSourceTo"></param>
        public void WriteDtoSource(string writeSourceTo)
        {
            WriteDtoSource($"{GetNamespace()}.Dtos", writeSourceTo);
        }

        /// <summary>
        /// Write dto source code into the specified namespace placing files into the specified directory
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="writeSourceTo"></param>
        public void WriteDtoSource(string nameSpace, string writeSourceTo)
        {
            Args.ThrowIfNull(DaoAssembly, "DaoAssembly");

            foreach (Type daoType in DaoAssembly.GetTypes()
                .Where(t => t.HasCustomAttributeOfType<TableAttribute>())
                .Select(t => t.BuildDynamicType<ColumnAttribute>()).ToArray())
            {
                Dto.WriteRenderedDto(nameSpace, writeSourceTo, daoType);
            }
        }

        public string GetDefaultFileName()
        {
            return "_{0}_{1}_.dll"._Format(
                GetNamespace(),
                DaoAssembly.GetTypes()
                .Where(t => t.HasCustomAttributeOfType<TableAttribute>())
                .ToInfoHash()
            ); // this fluent stuff is setting the fileName to the Md5 hash of all the table names comma delimited
        }

        private string GetNamespace()
        {
            Args.ThrowIfNull(DaoAssembly, "DaoToDtoGenerator.DaoAssembly");

            Type oneTable = DaoAssembly.GetTypes().FirstOrDefault(t => t.HasCustomAttributeOfType<TableAttribute>());
            if (oneTable == null)
            {
                oneTable = DaoAssembly.GetTypes().FirstOrDefault();
                if (oneTable == null)
                {
                    Args.Throw<InvalidOperationException>("The specified DaoAssembly has no types defined");
                }
            }
            string nameSpace = oneTable.Namespace;
            return nameSpace;
        }
        private DirectoryInfo SetSourceDir(string writeSourceTo)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(writeSourceTo);
            if (sourceDir.Exists)
            {
                try
                {
                    sourceDir.Delete(true);
                }
                catch (Exception ex)
                {
                    TempDir = sourceDir.FullName;
                    ExceptionMessage = Args.GetMessageAndStackTrace(ex);
                    FireEvent(DeleteTempSourceDirectoryFailed, EventArgs.Empty);
                    throw ex;
                }
            }

            sourceDir.Create();
            return sourceDir;
        }
    }
}
