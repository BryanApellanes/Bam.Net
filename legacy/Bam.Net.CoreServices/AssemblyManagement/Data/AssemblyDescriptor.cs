using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.CoreServices.AssemblyManagement.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data
{
    [Serializable]
    public class AssemblyDescriptor: KeyHashRepoData
    {
        public AssemblyDescriptor()
        { }

        public AssemblyDescriptor(Assembly assembly)
        {
            FileInfo assemblyFile = assembly.GetFileInfo();
            Name = assemblyFile.Name;
            FileHash = assemblyFile.Sha256();
            AssemblyFullName = assembly.FullName;
            SetReferenceDescriptors(assembly);
        }

        public AssemblyDescriptor Save(IRepository repo)
        {
            Task.Run(() =>
            {
                int number = FileHash.ToSha1Int();
                AssemblyRevision rev = repo.Query<AssemblyRevision>(new { FileHash = FileHash, FileName = Name, Number = number }).FirstOrDefault();
                if (rev == null)
                {
                    rev = new AssemblyRevision
                    {
                        FileHash = FileHash,
                        FileName = Name,
                        Number = number
                    };
                    repo.Save(rev);
                }
            });
            return repo.Save(this);
        }
        /// <summary>
        /// Set the AssemblyReferenceDescriptors property by
        /// calling assembly.GetReferencedAssemblies()
        /// </summary>
        /// <param name="assembly"></param>
        protected void SetReferenceDescriptors(Assembly assembly)
        {
            HashSet<AssemblyReferenceDescriptor> referenceDescriptors = new HashSet<AssemblyReferenceDescriptor>();

            foreach (AssemblyName name in assembly.GetReferencedAssemblies().Where(AssemblyNameFilter))
            {
                try
                {
                    Assembly referenced = Assembly.Load(name);
                    FileInfo file = referenced.GetFileInfo();
                    referenceDescriptors.Add(new AssemblyReferenceDescriptor
                    {
                        ReferencerName = Name,
                        ReferencedName = file.Name,
                        ReferencerHash = FileHash,
                        ReferencedHash = file.Sha256()
                    });
                }
                catch (Exception ex)
                {
                    Log.Default.AddEntry("Exception setting reference descriptors", ex);
                }
            };
            AssemblyReferenceDescriptors = referenceDescriptors.ToList();
        }

        static AssemblyDescriptor[] _allCurrent;
        static object _allCurrentLock = new object();
        protected internal static AssemblyDescriptor[] AllCurrent
        {
            get
            {
                return _allCurrentLock.DoubleCheckLock(ref _allCurrent, () => GetCurrentAppDomainDescriptors().ToArray());
            }
        }

        public virtual List<ProcessRuntimeDescriptor> ProcessRuntimeDescriptors { get; set; }
        
        /// <summary>
        /// The name of the assembly file as reported by Assembly.GetFileInfo().Name
        /// (where GetFileInfo() is the extension method Bam.Net.Extensions.GetFileInfo(Assembly assembly)
        /// which returns a FileInfo instance representing the specified assembly)
        /// </summary>
        [CompositeKey]
        public string Name { get; set; }
        /// <summary>
        /// Sha256 of the file content
        /// </summary>
        [CompositeKey]
        public string FileHash { get; set; }
        /// <summary>
        /// The full name of the assembly as reported by Assembly.FullName
        /// </summary>
        [CompositeKey]
        public string AssemblyFullName { get; set; }
        
        /// <summary>
        /// Association descriptors of the assemblies referenced by the assembly
        /// described by the current descriptor
        /// </summary>
        public virtual List<AssemblyReferenceDescriptor> AssemblyReferenceDescriptors
        {
            get;
            set;
        }

        public static HashSet<AssemblyDescriptor> GetCurrentAppDomainDescriptors()
        {
            HashSet<AssemblyDescriptor> result = new HashSet<AssemblyDescriptor>();
            Recurse(Assembly.GetEntryAssembly(), result);
            return result;
        }

        private static void Recurse(Assembly ass, HashSet<AssemblyDescriptor> soFar)
        {
            AssemblyDescriptor descr = new AssemblyDescriptor(ass);
            if (!soFar.Contains(descr))
            {
                soFar.Add(descr);
                IEnumerable<AssemblyName> assemblyNames = ass.GetReferencedAssemblies()
                    .Where(AssemblyNameFilter);
                foreach(AssemblyName assName in assemblyNames)
                {
                    try
                    {
                        Assembly next = Assembly.Load(assName);
                        if (next == null)
                        {
                            Log.Default.Warning("Assembly not found: ({0})", assName.FullName);
                        }
                        else
                        {
                            Recurse(next, soFar);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Default.AddEntry("Exception loading assembly ({0}) referenced by ({1})", ex, assName.FullName, ass.FullName);
                    }
                };
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is AssemblyDescriptor ad)
            {
                return FileHash.Equals(ad.FileHash);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return FileHash.ToSha1Int();
        }

        protected static internal bool AssemblyNameFilter(AssemblyName assemblyName)
        {
            return !assemblyName.Name.StartsWith("System") &&
                    !assemblyName.Name.StartsWith("mscorlib") &&
                    !assemblyName.Name.StartsWith("Microsoft");
        }
    }
}
