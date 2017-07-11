using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.Files
{
    public class CoreFileServiceSettings // Used by Copy from within CoreFileService
    {
        public string ChunkDirectory { get; set; }
        public int ChunkDataBatchSize { get; set; }
        public int ChunkLength { get; set; }
        public TimeSpan AsyncCheckTimeout { get; set; }
    }
}
