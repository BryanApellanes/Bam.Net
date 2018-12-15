using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public partial class ProxyAssemblyGenerator //fx
    {
        object _generateLock = new object();
        public GeneratedAssemblyInfo GenerateAssembly()
        {
            lock (_generateLock)
            {
                OnAssemblyGenerating(new ProxyAssemblyGenerationEventArgs { ServiceType = ServiceType, ServiceSettings = ServiceSettings });

                ProxyModel proxyModel = RenderCode();

                CompilerResults compileResult = AdHocCSharpCompiler.CompileSource(Code.ToString(), FileName, proxyModel.ReferenceAssemblies);
                if (compileResult.Errors.Count > 0)
                {
                    throw new CompilationException(compileResult);
                }

                GeneratedAssemblyInfo result = new GeneratedAssemblyInfo(FileName, compileResult);
                result.Save();
                OnAssemblyGenerated(new ProxyAssemblyGenerationEventArgs { ServiceType = ServiceType, ServiceSettings = ServiceSettings, Assembly = compileResult.CompiledAssembly });
                return result;
            }
        }
    }
}
