/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;
using System.Collections.Generic;
using Bam.Net.Services.Clients;

namespace Bam.Net.CoreServices
{
    public class ProxyAssemblyGenerator: ProxyAssemblyGenerationEventSource, IAssemblyGenerator
    {
        public ProxyAssemblyGenerator(ProxySettings settings, string workspaceDirectory = ".", ILogger logger = null, HashSet<Assembly> addedReferenceAssemblies = null)
        {
            AdditionalReferenceAssemblies = addedReferenceAssemblies;
            ServiceType = settings.ServiceType;
            ServiceSettings = settings;
            WorkspaceDirectory = workspaceDirectory;
            Code = new StringBuilder();
            Logger = logger ?? Log.Default;
        }

        public HashSet<Assembly> AdditionalReferenceAssemblies { get; set; }
        /// <summary>
        /// The logger used to log events for the current ProxyAssemblyGenerator
        /// </summary>
        public ILogger Logger { get; set; }
        public ProxySettings ServiceSettings { get; set; }
        public StringBuilder Code { get; set; }
        public string WorkspaceDirectory { get; set; }
        public Type ServiceType { get; set; }
        public string FileName
        {
            get
            {
                return Path.Combine(WorkspaceDirectory, "{0}_{1}_Proxy.dll"._Format(ServiceType.Name, ServiceSettings.ToString()));
            }
        }

        public void WriteSource(string writeSourceTo)
        {
            RenderCode();
            Code.ToString().SafeWriteToFile(Path.Combine(WorkspaceDirectory, "src", $"{ServiceType.Name}.Proxy.cs"));
        }

        public GeneratedAssemblyInfo GetAssembly()
        {
            return GeneratedAssemblyInfo.GetGeneratedAssembly(FileName, this);
        }

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
        
        public ProxyCode GenerateProxyCode()
        {
            Code = new StringBuilder();
            ProxyCode code = new ProxyCode
            {
                ProxyModel = RenderCode(),
                Code = Code.ToString()
            };
            return code;
        }

        private ProxyModel RenderCode()
        {
            EnsureWorkspace();
            SetClientCode();
            ProxyModel proxyModel = GetProxyModel();
            WarnNonVirtualMethods(proxyModel);
            Code.AppendLine(proxyModel.Render());
            return proxyModel;
        }

        private ProxyModel GetProxyModel()
        {
            HashSet<Assembly> referenceAssemblies = new HashSet<Assembly>(AdditionalReferenceAssemblies ?? new HashSet<Assembly>())
            {
                ServiceType.Assembly
            };
            return new ProxyModel(ServiceType, ServiceSettings.Protocol.ToString().ToLowerInvariant(), ServiceSettings.Host, ServiceSettings.Port, referenceAssemblies);
        }

        private void WarnNonVirtualMethods(ProxyModel model)
        {
            model.ServiceGenerationInfo.MethodGenerationInfos.Each(mgi =>
            {
                MethodInfo method = mgi.Method;
                if (!method.IsVirtual)
                {
                    Logger.AddEntry("The method {0}.{1} is not marked virtual and as a result the generated proxy will not delegate properly to the designated remote", LogEventType.Warning, method.DeclaringType.Name, method.Name);
                    OnMethodWarning(new ProxyAssemblyGenerationEventArgs { NonVirtualMethod = method });
                }
            });
        }

        private void SetClientCode()
        {
            if (ServiceSettings.DownloadClient)
            {
                Code = ServiceSettings.DownloadClientCode(ServiceType);
            }
            else
            {
                Code = ServiceProxySystem.GenerateCSharpProxyCode(ServiceSettings.Protocol.ToString(), ServiceSettings.Host, ServiceSettings.Port, ServiceType.Namespace, ServiceType);                
            }
        }

        private void EnsureWorkspace()
        {
            DirectoryInfo root = new DirectoryInfo(WorkspaceDirectory);
            if (!root.Exists)
            {
                root.Create();
            }
        }
    }
}
