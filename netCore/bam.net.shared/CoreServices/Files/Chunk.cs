using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.Files
{
    internal class Chunk : IChunk, IChunkable
    {
        public string Hash { get; set; }
        public byte[] Data { get; set; }

        public IChunk ToChunk()
        {
            return this;
        }
    }

}
