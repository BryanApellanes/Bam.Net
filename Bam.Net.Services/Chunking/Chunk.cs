using Bam.Net.CoreServices.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Chunking
{
    public class Chunk: IChunk
    {
        public string Hash { get; set; }
        public byte[] Data { get; set; }
    }
}
