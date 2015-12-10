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

namespace Bam.Net.Data.Repositories
{
	/// <summary>
	/// A generator that will create Dto's from Dao's.
	/// Intended primarily to enable backup of
	/// Daos to an ObjectRepository
	/// </summary>
	public class DaoToDtoGenerator: Loggable, IAssemblyGenerator
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

		[Verbosity(VerbosityLevel.Warning, MessageFormat="Unable to delete temp source directory: {TempDir}\r\n{ExceptionMessage}")]
		public event EventHandler DeleteTempSourceDirectoryFailed;

		/// <summary>
		/// Read by Loggable messages if deleting temp directory fails
		/// </summary>
		public string ExceptionMessage { get; set; }
		
		/// <summary>
		/// Read by Loggable messages if deleting temp directory fails
		/// </summary>
		public string TempDir { get; set; }

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

			return GenerateDtoAssembly("{0}.Dtos"._Format(oneTable.Namespace));
		}

		public GeneratedAssemblyInfo GenerateDtoAssembly(string nameSpace)
		{
			return GenerateDtoAssembly(nameSpace, GetDefaultFileName());
		}

		public GeneratedAssemblyInfo GenerateDtoAssembly(string nameSpace, string fileName)
		{
			Type oneDao = DaoAssembly.GetTypes().FirstOrDefault(t => t.HasCustomAttributeOfType<TableAttribute>());
			string writeSourceTo = Path.Combine("".GetAppDataFolder(), "DtoTemp_{0}"._Format(Dao.ConnectionName(oneDao)));
			DirectoryInfo sourceDir = SetSourceDir(writeSourceTo);

			foreach (Type dynamicDtoType in DaoAssembly.GetTypes()
				.Where(t => t.HasCustomAttributeOfType<TableAttribute>())
				.Select(t => t.CreateDynamicType<ColumnAttribute>()).ToArray())
			{
				Dto.WriteRenderedDto(nameSpace, writeSourceTo, dynamicDtoType);
			}

			CompilerResults results;
			sourceDir.ToAssembly(fileName, out results);
			GeneratedAssemblyInfo result = new GeneratedAssemblyInfo(fileName, results);
			result.Save();
			return result;
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
		
		public string GetDefaultFileName()
		{
			return "_{0}_.dll"._Format( 
									DaoAssembly.GetTypes()
									.Where(t => t.HasCustomAttributeOfType<TableAttribute>())
									.Select(t => t.Name)
									.ToArray()
									.ToDelimited(n => n, ", ")
									.Md5()
								); // this fluent stuff is setting the fileName to the Md5 hash of all the table names comma delimited
		}

	}
}
