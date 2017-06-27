using System;
using System.Reflection;
using System.Threading.Tasks;
using Bam.Net.Services.AssemblyManagement.Data;

namespace Bam.Net.Services
{
    public interface IAssemblyService
    {
        ProcessRuntimeDescriptor CurrentProcessRuntimeDescriptor { get; set; }
        ProcessRuntimeDescriptor LoadRuntimeDescriptor(ProcessRuntimeDescriptor likeThis);
        ProcessRuntimeDescriptor LoadRuntimeDescriptor(string filePath, string commandLine, string machineName, string applicationName);
        void RestoreApplicationRuntime(string applicationName, string directoryPath);
        Assembly ResolveAssembly(string assemblyName, string assemblyDirectory = null);
    }
}