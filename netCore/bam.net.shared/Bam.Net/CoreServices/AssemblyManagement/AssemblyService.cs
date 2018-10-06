using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Repo = Bam.Net.CoreServices.AssemblyManagement.Data.Dao.Repository;
using Bam.Net.CoreServices.Files;
using Bam.Net.CoreServices.AssemblyManagement.Data;
using System.Collections.Generic;
using Bam.Net.Data.Repositories;
using Bam.Net.Data;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// Service responsible for saving assemblies, especially those that are dynamically
    /// generated, and making them available to consuming processes.  
    /// </summary>
    [Proxy("assemblySvc")]
    public class AssemblyService : ApplicationProxyableService, IAssemblyService
    {
        public AssemblyService(IDataDirectoryProvider dataSettings, IFileService fileService, Repo.AssemblyServiceRepository repo, IApplicationNameProvider appNameProvider)
        {
            DataSettings = dataSettings;
            FileService = fileService;
            AssemblyManagementRepository = repo;
            ApplicationNameProvider = appNameProvider;
            LoadCurrentRuntimeDescriptorTask = RegisterCurrentRuntimeDescriptor();
        }

        public override object Clone()
        {
            AssemblyService result = new AssemblyService(DataSettings, FileService, AssemblyManagementRepository, ApplicationNameProvider);
            result.CopyProperties(this);
            result.CopyEventHandlers(this);
            return result;
        }
        public event EventHandler CurrentRuntimePersisted;
        public event EventHandler ExceptionPersistingCurrentRuntime;
        public event EventHandler RuntimeRestored;
        public IDataDirectoryProvider DataSettings { get; set; }
        public string AssemblyDirectory
        {
            get
            {
                return DataSettings.GetSysAssemblyDirectory().FullName;
            }
        }
        public IFileService FileService { get; set; }
        public Repo.AssemblyServiceRepository AssemblyManagementRepository { get; set; }    

        public virtual List<AssemblyDescriptor> GetDescriptors(string assemblyFulName)
        {
            return AssemblyManagementRepository.AssemblyDescriptorsWhere(ad => ad.AssemblyFullName == assemblyFulName).ToList();
        }

        [Local]
        public Assembly ResolveAssembly(string assemblyName, string assemblyDirectory = null)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            if(assembly == null)
            {
                FileInfo file = FileService.WriteFileDataToDirectory(assemblyName, assemblyDirectory ?? AssemblyDirectory);
                assembly = Assembly.LoadFrom(file.FullName);
            }
            return assembly;
        }

        [Local]
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

        public ProcessRuntimeDescriptor PersistRuntimeDescriptor(ProcessRuntimeDescriptor runtimeDescriptor)
        {
            try
            {
                ProcessRuntimeDescriptor retrieved = ProcessRuntimeDescriptor.PersistToRepo(AssemblyManagementRepository, runtimeDescriptor);
                foreach (AssemblyDescriptor descriptor in retrieved?.AssemblyDescriptors)
                {
                    StoreAssemblyFileChunks(descriptor);
                }
                FireEvent(CurrentRuntimePersisted, new ProcessRuntimeDescriptorEventArgs { ProcessRuntimeDescriptor = retrieved });

                return retrieved;
            }
            catch(Exception ex)
            {
                FireEvent(ExceptionPersistingCurrentRuntime, new ProcessRuntimeDescriptorEventArgs { ProcessRuntimeDescriptor = runtimeDescriptor, Message = ex.Message });
                return runtimeDescriptor;
            }
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

        protected internal Task<ProcessRuntimeDescriptor> LoadCurrentRuntimeDescriptorTask { get; }
        /// <summary>
        /// Loads the current ProcessRuntimeDescriptor persisting it 
        /// if it isn't found
        /// </summary>
        /// <returns></returns>
        protected internal Task<ProcessRuntimeDescriptor> RegisterCurrentRuntimeDescriptor()
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
            FileService.StoreFileChunks(fileInfo, assemblyDescriptor.Name);
        }
    }
}
