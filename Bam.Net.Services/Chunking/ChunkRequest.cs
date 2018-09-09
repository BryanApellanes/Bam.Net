using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Files.Data;
using Bam.Net.Server.Streaming;

namespace Bam.Net.Services.Chunking
{
    public class ChunkRequest: StreamingRequest
    {
        public Chunk Chunk { get; set; }
        public string Hash { get; set; }
        public ChunkOperation Operation { get; set; }


    }
}
