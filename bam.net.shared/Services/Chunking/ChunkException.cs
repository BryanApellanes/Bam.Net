using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Chunking
{
    public class ChunkException: Exception
    {
        public ChunkException(ChunkResponse response): base(response.Message)
        {
            ChunkResponse = response;
        }
        public ChunkResponse ChunkResponse { get; set; }
    }
}
