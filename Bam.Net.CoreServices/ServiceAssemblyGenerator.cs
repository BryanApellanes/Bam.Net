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

namespace Bam.Net.CoreServices
{
    public class ServiceAssemblyGenerator: AssemblyGenerationEventEmitter, IAssemblyGenerator
    {
        public ServiceAssemblyGenerator(ServiceSettings settings, string workspaceDirectory = ".", ILogger logger = null)
        {
            this.ServiceType = settings.ServiceType;
            this.ServiceSettings = settings;
            this.WorkspaceDirectory = workspaceDirectory;
            this.Code = new StringBuilder();
            this.Logger = logger ?? Log.Default;
        }

        /// <summary>
        /// The logger used to log events for the current ServiceAssemblyGenerator
        /// </summary>
        public ILogger Logger { get; set; }
        public ServiceSettings ServiceSettings { get; set; }
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

        public GeneratedAssemblyInfo GetAssembly()
        {
            return GeneratedAssemblyInfo.GetGeneratedAssembly(FileName, this);
        }

        public GeneratedAssemblyInfo GenerateAssembly()
        {
            OnAssemblyGenerating(new AssemblyGenerationEventArgs { ServiceType = ServiceType, ServiceSettings = ServiceSettings });
            
            EnsureWorkspace();
            SetClientCode();

            ServiceModel serviceModel = new ServiceModel(ServiceType, ServiceSettings.Protocol.ToString().ToLowerInvariant(), ServiceSettings.Host, ServiceSettings.Port);
            WarnNonVirtualMethods(serviceModel);
            Code.AppendLine(serviceModel.Render());
            
            CompilerResults compileResult = AdHocCSharpCompiler.CompileSource(Code.ToString(), FileName, serviceModel.ReferenceAssemblies);
            if (compileResult.Errors.Count > 0)
            {
                throw new CompilationException(compileResult);
            }

            GeneratedAssemblyInfo result = new GeneratedAssemblyInfo(FileName, compileResult);
            result.Save();
            OnAssemblyGenerated(new AssemblyGenerationEventArgs { ServiceType = ServiceType, ServiceSettings = ServiceSettings });
            return result;
        }

        private void WarnNonVirtualMethods(ServiceModel model)
        {
            model.ServiceGenerationInfo.MethodGenerationInfos.Each(mgi =>
            {
                MethodInfo method = mgi.Method;
                if (!method.IsVirtual)
                {
                    Logger.AddEntry("The method {0}.{1} is not marked virtual and as a result the generated proxy will not delegate properly to the designated remote", LogEventType.Warning, method.DeclaringType.Name, method.Name);
                    OnMethodWarning(new AssemblyGenerationEventArgs { NonVirtualMethod = method });
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
                Code = ServiceProxySystem.GenerateCSharpProxyCode(ServiceType.Namespace, ServiceType);                
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
