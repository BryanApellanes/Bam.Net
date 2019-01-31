/*
	Copyright © Bryan Apellanes 2015  
*/
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace Bam.Net
{
    public static partial class AdHocCSharpCompiler
    {        
        static string[] _referenceAssemblies = new string[] { };
        public static void SetReferenceAssemlbies(string[] value)
        {
            _referenceAssemblies = value;
        }

        /// <summary>
        /// Compile all .cs files found in the specified directory to 
        /// the specified assemblyFileName and return the assembly.  Does
        /// a recursive search for .cs files
        /// </summary>
        /// <param name="direcotry"></param>
        /// <param name="assemblyFileName"></param>
        /// <returns></returns>
        public static Assembly ToAssembly(this DirectoryInfo direcotry, string assemblyFileName)
        {
            CompilerResults ignore;
            return ToAssembly(direcotry, assemblyFileName, out ignore);
        }

        /// <summary>
        /// Compile all .cs files found in the specified directory to 
        /// the specified assemblyFileName and return the assembly.  Does
        /// a recursive search for .cs files
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="assemblyFileName"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public static Assembly ToAssembly(this DirectoryInfo directory, string assemblyFileName, out CompilerResults results)
        {
            return ToAssembly(directory, assemblyFileName, out results, true);
        }


        /// <summary>
        /// Compile all .cs files found in the specified directory to 
        /// the specified assemblyFileName and return the assembly.  Does
        /// a recursive search for .cs files
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="assemblyFileName"></param>
        /// <param name="results"></param>
        /// <param name="throwOnError"></param>
        /// <returns></returns>
        public static Assembly ToAssembly(this DirectoryInfo directory, string assemblyFileName, out CompilerResults results, bool throwOnError = true)
        {
            if (_referenceAssemblies.Length == 0)
            {
                _referenceAssemblies = DefaultReferenceAssemblies;
            }

            results = CompileDirectory(directory, assemblyFileName, _referenceAssemblies, false);

            if (results.Errors.Count > 0 && throwOnError)
            {
                throw new CompilationException(results);
            }

            return results.CompiledAssembly;
        }

        public static Assembly ToAssembly(this FileInfo file, string assemblyFileName)
        {
            return ToAssembly(new FileInfo[] { file }, assemblyFileName, out CompilerResults results, true);
        }

        /// <summary>
        /// Compile the specified files containing csharp source into the assembly of the 
        /// specified assemblyFileName
        /// </summary>
        /// <param name="files"></param>
        /// <param name="assemblyFileName"></param>
        /// <returns></returns>
        public static Assembly ToAssembly(this FileInfo[] files, string assemblyFileName)
        {
            return ToAssembly(files, assemblyFileName, out CompilerResults results, true);
        }

        public static Assembly ToAssembly(this FileInfo[] files, string assemblyFileName, out CompilerResults results, bool throwOnError = true)
        {
            if(_referenceAssemblies.Length == 0)
            {
                _referenceAssemblies = DefaultReferenceAssemblies;
            }

            results = CompileFiles(files, assemblyFileName, _referenceAssemblies);
            if(results.Errors.Count > 0 && throwOnError)
            {
                throw new CompilationException(results);
            }
            return results.CompiledAssembly;
        }

        public static CompilerResults CompileDirectory(DirectoryInfo directory, string assemblyFileName, Assembly[] referenceAssemblies, bool executable = false)
        {
            return CompileDirectory(directory, assemblyFileName, referenceAssemblies.Select(a => a.GetFilePath()).ToArray(), executable);
        }

        public static CompilerResults CompileDirectory(DirectoryInfo directory, string assemblyFileName, string[] referenceAssemblies, bool executable)
        {
            return CompileDirectories(new DirectoryInfo[] { directory }, assemblyFileName, referenceAssemblies, executable);
        }

        public static CompilerResults CompileDirectories(DirectoryInfo[] directories, string assemblyFileName, string[] referenceAssemblies, bool executable)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = GetCompilerParameters(assemblyFileName, referenceAssemblies, executable);

            HashSet<string> fileNames = new HashSet<string>();

            foreach (DirectoryInfo directory in directories)
            {
                foreach (FileInfo fileInfo in directory.GetFiles("*.cs", SearchOption.AllDirectories))
                {
                    if (!fileNames.Contains(fileInfo.FullName))
                    {
                        fileNames.Add(fileInfo.FullName);
                    }
                }
            }
            return codeProvider.CompileAssemblyFromFile(parameters, fileNames.ToArray());
        }

        public static CompilerResults CompileFile(FileInfo file, string assemblyFileName, Assembly[] referenceAssemblies = null, bool executable = false)
        {
            return CompileFiles(new FileInfo[] { file }, assemblyFileName, referenceAssemblies, executable);
        }

        public static CompilerResults CompileFiles(FileInfo[] files, string assemblyFileName, Assembly[] referenceAssemblies = null, bool executable = false)
        {
            string[] refAssemblies = referenceAssemblies == null ? _defaultReferenceAssemblies : referenceAssemblies.Select(a => a.GetFilePath()).ToArray();
            return CompileFiles(files, assemblyFileName, refAssemblies, executable);
        }

        public static CompilerResults CompileFile(FileInfo file, string assemblyFileName, string[] referenceAssemblies, bool executable = false)
        {
            return CompileFiles(new FileInfo[] { file }, assemblyFileName, referenceAssemblies, executable);
        }

        public static CompilerResults CompileFiles(FileInfo[] files, string assemblyFileName, string[] referenceAssemblies, bool executable = false)
        {
            if(referenceAssemblies.Length == 0)
            {
                referenceAssemblies = _defaultReferenceAssemblies;
            }
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = GetCompilerParameters(assemblyFileName, referenceAssemblies, executable);
            return codeProvider.CompileAssemblyFromFile(parameters, files.Select(f => f.FullName).ToArray());
        }
        public static CompilerResults CompileSource(string source, string assemblyFileName)
        {
            return CompileSource(source, assemblyFileName, DefaultReferenceAssemblies.ToArray(), false);
        }
        public static CompilerResults CompileSource(string source, string assemblyFileName, Assembly[] referenceAssemblies, bool executable = false)
        {
            return CompileSource(source, assemblyFileName, referenceAssemblies.Select(a => a.GetFilePath()).ToArray(), executable);
        }

        public static CompilerResults CompileSource(string source, string assemblyFileName, string[] referenceAssemblies, bool executable)
        {
            return CompileSource(new string[] { source }, assemblyFileName, referenceAssemblies, executable);
        }

        public static CompilerResults CompileSource(string[] sources, string assemblyFileName, string[] referenceAssemblies, bool executable)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = GetCompilerParameters(assemblyFileName, referenceAssemblies, executable);
            return codeProvider.CompileAssemblyFromSource(parameters, sources);
        }

        public static CompilerParameters GetCompilerParameters(string assemblyFileName, string[] referenceAssemblies, bool executable)
        {
            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = executable,
                OutputAssembly = assemblyFileName
            };

            SetCompilerOptions(referenceAssemblies, parameters);
            return parameters;
        }

        public static void SetCompilerOptions(string[] referenceAssemblies, CompilerParameters parameters)
        {
            StringBuilder compilerOptions = new StringBuilder();
            string[] refAssemblies = referenceAssemblies;
            if(refAssemblies.Length == 0)
            {
                refAssemblies = DefaultReferenceAssemblies;
            }
            foreach (string referenceAssembly in refAssemblies)
            {
                compilerOptions.AppendFormat("/reference:\"{0}\" ", referenceAssembly);
            }
            parameters.CompilerOptions = compilerOptions.ToString();
        }

        public static string GetMessage(CompilerResults compilerResults)
        {
            StringBuilder message = new StringBuilder();

            foreach (CompilerError error in compilerResults.Errors)
            {
                message.AppendFormat("File=>{0}\r\n", error.FileName);
                message.AppendFormat("Line {0}, Column {1}::{2}\r\n", error.Line, error.Column, error.ErrorText);
            }

            return message.ToString();
        }
    }
}
