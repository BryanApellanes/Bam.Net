/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.CodeDom.Compiler;

namespace Bam.Net
{
    /// <summary>
    /// Provides information about dynamically generated assemblies
    /// </summary>
	public class GeneratedAssemblyInfo
	{
		public static implicit operator Assembly(GeneratedAssemblyInfo gen)
		{
			return gen.GetAssembly();
		}

		public GeneratedAssemblyInfo()
		{
			this.Root = ".\\";
		}

		public GeneratedAssemblyInfo(string infoFileName)
			: this()
		{
			this.InfoFileName = infoFileName;
		}

		public GeneratedAssemblyInfo(string infoFileName, CompilerResults compilerResults)
			: this(infoFileName)
		{
			if (compilerResults.Errors != null &&
				compilerResults.Errors.Count > 0)
			{
				throw new CompilationException(compilerResults);
			}

			this.AssemblyFilePath = new FileInfo(compilerResults.PathToAssembly).FullName;
			this.Assembly = compilerResults.CompiledAssembly;
		}

		public string InfoFileName { get; set; }
		
		/// <summary>
		/// The path to the Assembly (.dll)
		/// </summary>
		public string AssemblyFilePath { get; set; }

		public bool AssemblyExists
		{
			get
			{
				return File.Exists(AssemblyFilePath);
			}
		}

		Assembly _assembly;

		internal Assembly Assembly
		{
			get
			{
				return _assembly;
			}
			set
			{
				_assembly = value;
			}
		}

		public Assembly GetAssembly()
		{
			if (_assembly == null)
			{
				_assembly = Assembly.LoadFrom(AssemblyFilePath);
			}

			return _assembly;
		}

		public string Root { get; set; }

		public string InfoFilePath
		{
			get
			{
				return Path.Combine(Root, string.Format("{0}.genInfo.json", InfoFileName));
			}
		}

		public bool InfoFileExists
		{			
			get
			{
				return File.Exists(InfoFilePath);
			}
		}

		public void Save()
		{			
			this.ToJsonFile(InfoFilePath);
		}
        
        /// <summary>
        /// Get the generated assembly for the specified fileName using the
        /// specified generator to generate it if necessary
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="generator"></param>
        /// <returns></returns>
        public static GeneratedAssemblyInfo GetGeneratedAssembly(string fileName, IAssemblyGenerator generator)
        {
            GeneratedAssemblyInfo assemblyInfo = GeneratedAssemblies.GetGeneratedAssemblyInfo(fileName);

            if (assemblyInfo == null)
            {
                assemblyInfo = new GeneratedAssemblyInfo(fileName);
                // check for the info file
                if (assemblyInfo.InfoFileExists) // load it from file if it exists
                {
                    assemblyInfo = assemblyInfo.InfoFilePath.FromJsonFile<GeneratedAssemblyInfo>();
                    if (!assemblyInfo.InfoFileName.Equals(fileName) || !assemblyInfo.AssemblyExists) // regenerate if the names don't match
                    {
                        assemblyInfo = generator.GenerateAssembly();
                    }
                }
                else
                {
                    assemblyInfo = generator.GenerateAssembly();
                }
            }

            GeneratedAssemblies.SetAssemblyInfo(fileName, assemblyInfo);
            return assemblyInfo;
        }
	}
}
