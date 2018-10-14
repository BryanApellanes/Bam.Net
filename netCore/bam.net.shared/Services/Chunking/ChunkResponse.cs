using Bam.Net.CoreServices.Files;
using Bam.Net.Server.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Chunking
{
    public class ChunkResponse: StreamingResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IChunk Chunk { get; set; }
        /// <summary>
        /// Throw an exception if the Data.Hash does not
        /// match Hash
        /// </summary>
        public void Validate()
        {
            if (!Chunk.Data.Sha256().Equals(Chunk.Hash))
            {
                throw new InvalidOperationException("Hash check failed");
            }
        }
    }
}
