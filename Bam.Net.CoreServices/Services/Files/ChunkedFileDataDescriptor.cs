using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Files.Data;

namespace Bam.Net.CoreServices.Files
{
    /// <summary>
    /// An intermediate class used to describe the relationships
    /// between ChunkDataDescriptor and ChunkData
    /// </summary>
    public class ChunkedFileDataDescriptor
    {
        public ChunkDataDescriptor ChunkDataDescriptor { get; set; }
        public ChunkData ChunkData { get; set; }
    }
}
