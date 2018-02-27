using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    /// <summary>
    /// When implemented, provides available
    /// worker types.
    /// </summary>
    public interface IWorkerTypeProvider
    {
        Type[] GetWorkerTypes();
    }
}
