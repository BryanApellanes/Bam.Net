using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;

namespace Bam.Net.Automation
{
    public class ScanningWorkerTypeProvider : IWorkerTypeProvider
    {
        public ScanningWorkerTypeProvider(ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
        }
        public ILogger Logger { get; set; }
        public Type[] GetWorkerTypes()
        {
            return TypeScan.Result;
        }

        Task<Type[]> _typeScan;
        object _typeScanLock = new object();
        protected Task<Type[]> TypeScan
        {
            get
            {
                return _typeScanLock.DoubleCheckLock(ref _typeScan, () =>
                {
                    return Task.Run(() =>
                    {
                        DirectoryInfo entryDir = Assembly.GetEntryAssembly().GetFileInfo().Directory;
                        DirectoryInfo sysAssemblies = DefaultDataDirectoryProvider.Current.GetSysAssemblyDirectory();
                        DirectoryInfo[] dirs = new DirectoryInfo[] { entryDir, sysAssemblies };
                        HashSet<Type> types = new HashSet<Type>();
                        foreach (DirectoryInfo dir in dirs)
                        {
                            try
                            {
                                FileInfo[] dlls = dir.GetFiles("*.dll");
                                FileInfo[] exes = dir.GetFiles("*.exe");
                                dlls.SelectMany(dll => ScanForWorkerTypes(dll)).Each(type => types.Add(type));
                                exes.SelectMany(exe => ScanForWorkerTypes(exe)).Each(type => types.Add(type));
                            }
                            catch (Exception ex)
                            {
                                Logger.AddEntry("Exception occurred scanning for worker types: {0}", ex, ex.Message);
                            }
                        }

                        return types.ToArray();
                    });
                });
            }
        }

        protected Type[] ScanForWorkerTypes(FileInfo file)
        {
            Assembly assembly = Assembly.LoadFrom(file.FullName);
            return assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Worker)) || typeof(IWorker).IsAssignableFrom(type)).ToArray();
        }
    }
}
