/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CSharp;
using Naizari.Extensions;
using System.Reflection;
using System.IO;

namespace Naizari.Helpers
{
    public static class CompileHelper
    {
        public static CompilerResults Compile(this DirectoryInfo compileTarget, string fileName, string[] referenceAssemblies)
        {
            return Compile(compileTarget, fileName, referenceAssemblies, false);
        }

        public static CompilerResults Compile(this DirectoryInfo compileTarget, string fileName, string[] referenceAssemblies, bool executable)
        {
            return Compile(new DirectoryInfo[] { compileTarget }, fileName, referenceAssemblies, executable);
        }

        public static CompilerResults Compile(this DirectoryInfo[] compileTargets, string fileName, string[] referenceAssemblies, bool executable)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = executable;
            parameters.OutputAssembly = fileName;
            List<string> compilerOptions = new List<string>();

            foreach (string referenceAssembly in referenceAssemblies)
            {
                compilerOptions.Add("/reference:" + referenceAssembly);
            }
            parameters.CompilerOptions = compilerOptions.ToArray().ToDelimited(" ");

            List<string> fileNames = new List<string>();
            foreach (DirectoryInfo targetDirectory in compileTargets)
            {
                foreach (FileInfo fileInfo in FsUtil.GetFilesWithExtension(targetDirectory.FullName, ".cs"))
                {
                    fileNames.Add(fileInfo.FullName);
                }
            }

            return codeProvider.CompileAssemblyFromFile(parameters, fileNames.ToArray());//.CompileAssemblyFromFileBatch(parameters, fileNames.ToArray());
        }
    }
}
