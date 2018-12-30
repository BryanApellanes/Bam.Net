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
			Root = ".\\";
		}

		public GeneratedAssemblyInfo(string infoFileName)
			: this()
		{
			InfoFileName = infoFileName;
		}

        public GeneratedAssemblyInfo(string infoFileName, CompilerResults compilerResults)
            : this(infoFileName)
        {
            if (compilerResults.Errors != null &&
                compilerResults.Errors.Count > 0)
            {
                throw new CompilationException(compilerResults);
            }

            AssemblyFilePath = new FileInfo(compilerResults.PathToAssembly).FullName;
            Assembly = compilerResults.CompiledAssembly;
        }

        internal GeneratedAssemblyInfo(Assembly assembly)
        {
            FileInfo assemblyFileInfo = assembly.GetFileInfo();
            InfoFileName = assemblyFileInfo.Name;
            Assembly = assembly;
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

		public Assembly Assembly
		{
			get
			{
				return _assembly;
			}
			internal set
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
                return new FileInfo(Path.Combine(Root, string.Format("{0}.genInfo.json", InfoFileName))).FullName;
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
        /// <param name="infoFileName"></param>
        /// <param name="generator"></param>
        /// <returns></returns>
        public static GeneratedAssemblyInfo GetGeneratedAssembly(string infoFileName, IAssemblyGenerator generator)
        {
            GeneratedAssemblyInfo assemblyInfo = GeneratedAssemblies.GetGeneratedAssemblyInfo(infoFileName);

            if (assemblyInfo == null)
            {
                assemblyInfo = new GeneratedAssemblyInfo(infoFileName);
                // check for the info file
                if (assemblyInfo.InfoFileExists) // load it from file if it exists
                {
                    assemblyInfo = assemblyInfo.InfoFilePath.FromJsonFile<GeneratedAssemblyInfo>();
                    if (assemblyInfo == null /* the file was empty for some reason */ || !assemblyInfo.InfoFileName.Equals(infoFileName) || !assemblyInfo.AssemblyExists) // regenerate if the names don't match
                    {
                        assemblyInfo = generator.GenerateAssembly();
                    }
                }
                else
                {
                    assemblyInfo = generator.GenerateAssembly();
                }
            }

            GeneratedAssemblies.SetAssemblyInfo(infoFileName, assemblyInfo);
            return assemblyInfo;
        }
	}
}
