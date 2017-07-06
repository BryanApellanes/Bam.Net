using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Repo = Bam.Net.Services.AssemblyManagement.Data.Dao.Repository;
using Bam.Net.Services.Files;
using Bam.Net.Services.AssemblyManagement.Data;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Services.Files.Data;

namespace Bam.Net.Services
{
    public class AssemblyService : Loggable, IAssemblyService // doesn't need to be remote accessible, can use FileService which can be
    {
        public AssemblyService(IFileService fileService, Repo.AssemblyServiceRepository repo, IApplicationNameProvider appNameProvider, 
            EventHandler currentRuntimePersisted = null,
            EventHandler applicationRestoredHandler = null)
        {
            FileService = fileService;
            AssemblyManagementRepository = repo;
            ApplicationNameProvider = appNameProvider;
            RuntimeRestored += applicationRestoredHandler ?? ((o, a) => { });
            LoadCurrentRuntimeDescriptorTask = LoadCurrentRuntimeDescriptor();
        }

        public event EventHandler CurrentRuntimePersisted;
        public event EventHandler RuntimeRestored;
        public string AssemblyDirectory { get; set; }
        public IFileService FileService { get; set; }
        public Repo.AssemblyServiceRepository AssemblyManagementRepository { get; set; }
        public IApplicationNameProvider ApplicationNameProvider { get; set; }
        

        public Assembly ResolveAssembly(string assemblyName, string assemblyDirectory = null)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            if(assembly == null)
            {
                FileInfo file = FileService.WriteFileToDirectory(assemblyName, assemblyDirectory ?? AssemblyDirectory);
                assembly = Assembly.LoadFrom(file.FullName);
            }
            return assembly;
        }

        public void RestoreApplicationRuntime(string applicationName, string directoryPath)
        {
            ProcessRuntimeDescriptor prd = ProcessRuntimeDescriptor.LoadByAppName(applicationName, AssemblyManagementRepository);
            if(prd == null)
            {
                Args.Throw<InvalidOperationException>("The specified application ({0}) was not found", applicationName);
            }
            RestoreProcessRuntime(prd, directoryPath);           
        }

        ProcessRuntimeDescriptor _current;
        public ProcessRuntimeDescriptor CurrentProcessRuntimeDescriptor
        {
            get
            {
                if(_current == null)
                {
                    _current = LoadCurrentRuntimeDescriptorTask.Result; 
                }
                return _current;
            }
            set
            {
                _current = value;
            }
        }

        protected internal Task<ProcessRuntimeDescriptor> LoadCurrentRuntimeDescriptorTask { get;  }
        /// <summary>
        /// Loads the current ProcessRuntimeDescriptor persisting it 
        /// if it isn't found
        /// </summary>
        /// <returns></returns>
        protected internal Task<ProcessRuntimeDescriptor> LoadCurrentRuntimeDescriptor()
        {
            return Task.Run(() =>
            {
                ProcessRuntimeDescriptor current = ProcessRuntimeDescriptor.GetCurrent();
                ProcessRuntimeDescriptor retrieved = LoadRuntimeDescriptor(current);

                if (retrieved == null)
                {
                    retrieved = PersistRuntimeDescriptor(current);
                }

                return retrieved;
            });
        }

        public ProcessRuntimeDescriptor PersistRuntimeDescriptor(ProcessRuntimeDescriptor runtimeDescriptor)
        {
            ProcessRuntimeDescriptor retrieved = ProcessRuntimeDescriptor.PersistToRepo(AssemblyManagementRepository, runtimeDescriptor);
            foreach (AssemblyDescriptor descriptor in retrieved.AssemblyDescriptors)
            {
                StoreAssemblyFileChunks(descriptor);
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
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            FireEvent(RuntimeRestored, new ProcessRuntimeDescriptorEventArgs { ProcessRuntimeDescriptor = prd, DirectoryPath = dir.FullName});
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
