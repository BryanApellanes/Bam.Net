using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// CoreApplicationRegistryServiceConfig providing
    /// IDatabaseProvider, WorkspacePath and ILogger
    /// </summary>
    public class CoreApplicationRegistryServiceConfig
    {
        public IDatabaseProvider DatabaseProvider { get; set; }
        public string WorkspacePath { get; set; }
        public ILogger Logger { get; set; }
    }
}
