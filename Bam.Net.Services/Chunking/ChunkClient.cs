using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Files.Data;
using Bam.Net.Server.Binary;

namespace Bam.Net.Services.Chunking
{
    public class ChunkClient : BinaryClient<ChunkRequest, ChunkResponse>
    {
        public ChunkClient(string hostName, int port) : base(hostName, port)
        {            
        }

        public ChunkData GetChunk(string hash)
        {
            BinaryResponse<ChunkResponse> response = SendRequest(new ChunkRequest
            {
                Operation = ChunkOperation.Get,
                Hash = hash
            });

            if (!response.Data.Success)
            {
                throw new Exception(response.Data.Message);
            }

            Chunk chunk = response.Data.Chunk;
            return new ChunkData
            {
                ChunkHash = hash,
                Data = chunk.Data.ToBase64(),
                ChunkLength = chunk.Data.Length
            };
        }

        public void SetChunk(ChunkData chunkData)
        {
            BinaryResponse<ChunkResponse> response = SendRequest(new ChunkRequest
            {
                Operation = ChunkOperation.Set,
                Chunk = new Chunk
                {
                    Hash = chunkData.ChunkHash,
                    Data = chunkData.Data.FromBase64()
                }
            });

            if (!response.Data.Success)
            {
                throw new Exception(response.Data.Message);
            }
        }
    }
}
