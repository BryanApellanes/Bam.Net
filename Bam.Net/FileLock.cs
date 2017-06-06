using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    internal static class FileLock
    {
        static Dictionary<string, object> _locks = new Dictionary<string, object>();
        public static Dictionary<string, object> Locks
        {
            get
            {
                return _locks;
            }
        }

        public static object Named(string name)
        {
            lock (_locks)
            {
                if (!_locks.ContainsKey(name))
                {
                    _locks.Add(name, new object());
                }
                return _locks[name];
            }
        }  
        
        public static void ClearLocks()
        {
            lock (_locks)
            {
                _locks.Clear();
            }
        }
    }
}
