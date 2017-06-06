using System.Threading.Tasks;
using Bam.Net.Services.AssemblyManagement.Data;

namespace Bam.Net.Services
{
    public interface IAssemblyManagementService
    {
        Task<ProcessRuntimeDescriptor> PersistCurrentProcessRuntimeDescriptorTask { get; set; }

        ProcessRuntimeDescriptor LoadCurrentRuntimeDescriptor();
        ProcessRuntimeDescriptor LoadRuntimeDescriptor(ProcessRuntimeDescriptor likeThis);
        ProcessRuntimeDescriptor LoadRuntimeDescriptor(string filePath, string commandLine, string machineName, string applicationName);
        void RestoreApplicationRuntime(string applicationName, string directoryPath);
    }
}