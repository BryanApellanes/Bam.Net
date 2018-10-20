using System;
using System.IO;
using System.Reflection;

namespace Bam.Net
{
    public class SQLiteBitMonitor
    {
        static object _monitorLock = new object();
        static bool _added;
        static SQLiteBitMonitor()
        {
            MonitorBitness();
        }

        public static void MonitorBitness()
        {
            if (!_added)
            {
                lock (_monitorLock)
                {
                    if (!_added)
                    {
                        _added = true;
                        AppDomain.CurrentDomain.AssemblyResolve += (o, a) =>
                        {
                            if (a.Name.StartsWith("System.Data.SQLite"))
                            {
                                string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                                string fileName = Path.Combine(assemblyDir, string.Format("SQLite\\{0}\\System.Data.SQLite.dll", IntPtr.Size == 4 ? "x86" : "x64"));
                                Console.WriteLine("Loading SQLite from: {0}"._Format(fileName));
                                return Assembly.LoadFrom(fileName);
                            }
                            return null;
                        };
                    }
                }
            }
        }
    }
}
