using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Files.Data;
using Bam.Net.Server.Binary;
using Bam.Net.CoreServices.Files;

namespace Bam.Net.Services.Chunking
{
    public class ChunkClient : BinaryClient<ChunkRequest, ChunkResponse>, IChunkStorage
    {
        public ChunkClient(string hostName, int port) : base(hostName, port)
        {
            ExceptionMode = ChunkExceptionMode.EmitEvents;
        }
        public event EventHandler GetChunkException;
        public event EventHandler SetChunkException;
        public ChunkExceptionMode ExceptionMode { get; set; }

        public IChunk GetChunk(string hash)
        {
            BinaryResponse<ChunkResponse> response = SendRequest(new ChunkRequest
            {
                Operation = ChunkOperation.Get,
                Hash = hash
            });

            if (!response.Data.Success)
            {
                HandleException(GetChunkException, hash, response);
            }

            return response.Data.Chunk;            
        }

        public void SetChunk(IChunk chunkData)
        {
            BinaryResponse<ChunkResponse> response = SendRequest(new ChunkRequest
            {
                Operation = ChunkOperation.Set,
                Chunk = new Chunk
                {
                    Hash = chunkData.Hash,
                    Data = chunkData.Data
                }
            });

            if (!response.Data.Success)
            {
                HandleException(SetChunkException, chunkData.Hash, response);
            }
        }

        private void HandleException(EventHandler toFire, string hash, BinaryResponse<ChunkResponse> response)
        {
            switch (ExceptionMode)
            {
                case ChunkExceptionMode.Throw:
                    throw new ChunkException(response.Data);
                case ChunkExceptionMode.EmitEvents:
                    FireEvent(toFire, new ChunkExceptionEventArgs { Hash = hash });
                    break;
            }
        }
    }
}
