using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching
{
    /// <summary>
    /// When implemented, provides a mechanism to report
    /// the size of an object in memory.
    /// </summary>
    public interface IMemorySize
    {
        /// <summary>
        /// Get the size of the object in memory.
        /// </summary>
        /// <returns></returns>
        int MemorySize();
    }
}
