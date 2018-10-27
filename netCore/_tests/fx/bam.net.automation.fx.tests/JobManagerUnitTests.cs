using Bam.Net.Configuration;
using Bam.Net.Data.Repositories;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class JobManagerUnitTests : CommandLineTestInterface
    {
        [UnitTest("Func Compiler Test")]
        public void FuncCompilerTest()
        {
            string code = @"
using System;
public class FuncProvider
{ 
    public Func<bool> GetFunc()
    {
        return (Func<bool>)(() => $Code$);
    }
}";
            
            CompilerResults results = AdHocCSharpCompiler.CompileSource(code.Replace("$Code$", "true"), "TestAssembly");
            Func<bool> o = (Func<bool>)results.CompiledAssembly.GetType("FuncProvider").Construct().Invoke("GetFunc");
            Expect.IsTrue(o());
        }
    }
}
