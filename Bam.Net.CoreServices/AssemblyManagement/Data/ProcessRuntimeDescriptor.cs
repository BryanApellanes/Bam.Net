using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Data.Repositories;
using Bam.Net.CoreServices.AssemblyManagement.Data.Dao.Repository;

namespace Bam.Net.CoreServices.AssemblyManagement.Data
{
    /// <summary>
    /// Represents through analysis all the assemblies recorded
    /// during the execution of a process
    /// </summary>
    [Serializable]
    public class ProcessRuntimeDescriptor: KeyHashRepoData
    {
        public ProcessRuntimeDescriptor()
        {
            MachineName = Environment.MachineName;
            CommandLine = Environment.CommandLine;
            FilePath = Assembly.GetEntryAssembly().GetFilePath();
            ApplicationName = DefaultConfigurationApplicationNameProvider.Instance.GetApplicationName();
        }

        [CompositeKey]
        public string ApplicationName { get; set; }
        [CompositeKey]
        public string MachineName { get; set; }
        [CompositeKey]
        public string CommandLine { get; set; }
        [CompositeKey]
        public string FilePath { get; set; }

        AssemblyDescriptor[] _assemblyDescriptors;

        /// <summary>
        /// All the assemblies identified as being 
        /// referenced by all assemblies in the current
        /// app domain minus System*, mscorlib, and Microsoft*
        /// </summary>
        public virtual AssemblyDescriptor[] AssemblyDescriptors
        {
            get
            {
                return _assemblyDescriptors ?? new AssemblyDescriptor[] { };
            }
            set
            {
                _assemblyDescriptors = value;
            }
        }

        public HashSet<string> AssemblyFileHashes
        {
            get
            {
                HashSet<string> results = new HashSet<string>();
                AssemblyDescriptors.Each(ad =>
                {
                    results.Add(ad.FileHash);
                    ad.AssemblyReferenceDescriptors.Each(r => results.Add(r.ReferencedHash));
                });
                return results;
            }
        }
        
        public static ProcessRuntimeDescriptor LoadByAppName(string appName, IRepository repo, string machineName = null)
        {
            machineName = machineName ?? Environment.MachineName;
            ProcessRuntimeDescriptor value = repo.Query<ProcessRuntimeDescriptor>(new { ApplicationName = appName, MachineName = machineName }).FirstOrDefault();
            if (value == null)
            {
                value = repo.Query<ProcessRuntimeDescriptor>(new { ApplicationName = appName }).FirstOrDefault();
            }
            if (value != null)
            {
                value = repo.Retrieve<ProcessRuntimeDescriptor>(value.Uuid);
                LoadAssemblyDescriptors(value, repo);
            }
            return value;
        }

        public static ProcessRuntimeDescriptor LoadFromRepo(string cuid, IRepository repo)
        {
            ProcessRuntimeDescriptor value = repo.Query<ProcessRuntimeDescriptor>(new { Cuid = cuid }).FirstOrDefault();
            if(value != null)
            {
                value = repo.Retrieve<ProcessRuntimeDescriptor>(value.Id);
                LoadAssemblyDescriptors(value, repo);
            }

            return value;
        }

        private static void LoadAssemblyDescriptors(ProcessRuntimeDescriptor value, IRepository repo)
        {
            List<AssemblyDescriptor> descriptors = new List<AssemblyDescriptor>();
            foreach (AssemblyDescriptor descriptor in value.AssemblyDescriptors)
            {
                descriptors.Add(repo.Retrieve<AssemblyDescriptor>(descriptor.Uuid));
            }
            value.AssemblyDescriptors = descriptors.ToArray();
        }

        /// <summary>
        /// Persist the current ProcessRuntimeDescriptor to the specified
        /// repo and return the result
        /// </summary>
        /// <param name="repo"></param>
        /// <returns></returns>
        public static ProcessRuntimeDescriptor PersistCurrentToRepo(IRepository repo)
        {
            ProcessRuntimeDescriptor current = GetCurrent();
            return PersistToRepo(repo, current);
        }
        
        /// <summary>
        /// Persist to the specified repository the specified ProcessRuntimeDescriptor
        /// and return the result
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static ProcessRuntimeDescriptor PersistToRepo(IRepository repo, ProcessRuntimeDescriptor descriptor)
        {
            Args.ThrowIfNull(repo, "repo");
            Args.ThrowIfNull(descriptor, "descriptor");

            List<AssemblyDescriptor> descriptors = new List<AssemblyDescriptor>();
            foreach (AssemblyDescriptor assDescriptor in descriptor.AssemblyDescriptors)
            {
                AssemblyDescriptor saved = assDescriptor.Save(repo);
                descriptors.Add(saved);
            }
            descriptor.AssemblyDescriptors = descriptors.ToArray();
            return repo.Save(descriptor); 
        }

        static ProcessRuntimeDescriptor _current;
        static object _currentLock = new object();

        /// <summary>
        /// Gets the current ProcessRuntimeDescriptor from
        /// the currently running process
        /// </summary>
        /// <returns></returns>
        public static ProcessRuntimeDescriptor GetCurrent()
        {
            return _currentLock.DoubleCheckLock(ref _current, () =>
            {
                ProcessRuntimeDescriptor descriptor = new ProcessRuntimeDescriptor()
                {
                    AssemblyDescriptors = AssemblyDescriptor.AllCurrent
                };
                return descriptor;
            });
        }
    }
}
