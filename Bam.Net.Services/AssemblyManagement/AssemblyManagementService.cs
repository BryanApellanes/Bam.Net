using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Repo = Bam.Net.Services.AssemblyManagement.Data.Dao.Repository;
using Bam.Net.Services.Distributed.Files;
using Bam.Net.Services.AssemblyManagement.Data;

namespace Bam.Net.Services
{
    public class AssemblyManagementService : Loggable, IAssemblyManagementService
    {
        public AssemblyManagementService(FileService fileService, Repo.AssemblyManagementRepository repo, IApplicationNameProvider appNameProvider, 
            EventHandler currentRuntimePersisted = null,
            EventHandler applicationRestoredHandler = null)
        {
            FileService = fileService;
            AssemblyManagementRepository = repo;
            ApplicationNameProvider = appNameProvider;
            RuntimeRestored += applicationRestoredHandler ?? ((o, a) => { });
            PersistCurrentProcessRuntimeDescriptorTask = Task.Run(() => LoadCurrentRuntimeDescriptor());
        }

        public event EventHandler CurrentRuntimePersisted;
        public event EventHandler RuntimeRestored;

        public FileService FileService { get; set; }
        public Repo.AssemblyManagementRepository AssemblyManagementRepository { get; set; }
        public IApplicationNameProvider ApplicationNameProvider { get; set; }

        public Task<ProcessRuntimeDescriptor> PersistCurrentProcessRuntimeDescriptorTask { get; set; }


        public void RestoreApplicationRuntime(string applicationName, string directoryPath)
        {
            ProcessRuntimeDescriptor prd = ProcessRuntimeDescriptor.LoadByAppName(applicationName, AssemblyManagementRepository);
            if(prd == null)
            {
                Args.Throw<InvalidOperationException>("The specified application ({0}) was not found", applicationName);
            }
            RestoreProcessRuntime(prd, directoryPath);           
        }

        protected internal void CloneCurrentRuntime(string directoryPath)
        {
            RestoreProcessRuntime(LoadCurrentRuntimeDescriptor(), directoryPath);
        }        

        public ProcessRuntimeDescriptor LoadCurrentRuntimeDescriptor()
        {
            ProcessRuntimeDescriptor current = ProcessRuntimeDescriptor.GetCurrent();
            ProcessRuntimeDescriptor retrieved = LoadRuntimeDescriptor(current);

            if (retrieved == null)
            {
                retrieved = ProcessRuntimeDescriptor.PersistToRepo(AssemblyManagementRepository, current);
                foreach (AssemblyDescriptor descriptor in retrieved.AssemblyDescriptors)
                {
                    StoreAssemblyFileChunks(descriptor);
                }
            }
            FireEvent(CurrentRuntimePersisted, new ProcessRuntimeDescriptorEventArgs { ProcessRuntimeDescriptor = retrieved });
            return retrieved;
        }
        
        public ProcessRuntimeDescriptor LoadRuntimeDescriptor(ProcessRuntimeDescriptor likeThis)
        {
            return LoadRuntimeDescriptor(
                likeThis.FilePath, 
                likeThis.CommandLine, 
                likeThis.MachineName, 
                string.IsNullOrEmpty(likeThis.ApplicationName) ? ApplicationNameProvider.GetApplicationName(): likeThis.ApplicationName);
        }

        public ProcessRuntimeDescriptor LoadRuntimeDescriptor(string filePath, string commandLine, string machineName, string applicationName)
        {
            return AssemblyManagementRepository.ProcessRuntimeDescriptorsWhere(c =>
                            c.FilePath == filePath &&
                            c.CommandLine == commandLine &&
                            c.MachineName == machineName &&
                            c.ApplicationName == applicationName)
                        .FirstOrDefault();
        }

        protected void RestoreProcessRuntime(ProcessRuntimeDescriptor prd, string directoryPath)
        {
            foreach (AssemblyDescriptor ad in prd.AssemblyDescriptors)
            {
                string filePath = Path.Combine(directoryPath, ad.Name);
                FileService.RestoreFile(ad.FileHash, filePath);
            }
            FireEvent(RuntimeRestored, new ProcessRuntimeDescriptorEventArgs { ProcessRuntimeDescriptor = prd });
        }

        protected void StoreAssemblyFileChunks(AssemblyDescriptor assemblyDescriptor)
        {
            Assembly assembly = Assembly.Load(assemblyDescriptor.AssemblyFullName);
            FileInfo fileInfo = assembly.GetFileInfo();
            Args.ThrowIf(!fileInfo.Sha256().Equals(assemblyDescriptor.FileHash), "FileHash validation failed: {0}", assemblyDescriptor.AssemblyFullName);
            FileService.StoreFileChunksInRepo(fileInfo, assemblyDescriptor.Name);
        }
    }
}
