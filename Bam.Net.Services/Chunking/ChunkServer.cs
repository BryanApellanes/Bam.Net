using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Files.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Server.Streaming;
using Bam.Net.CoreServices.Files;

namespace Bam.Net.Services.Chunking
{
    public class ChunkServer : StreamingServer<ChunkRequest, ChunkResponse>
    {
        public ChunkServer(IChunkStorage chunkStorage, ILogger logger = null)
        {
            ChunkStorage = chunkStorage;
            Logger = logger ?? Log.Default;
        }
        public IChunkStorage ChunkStorage { get; set; }
        public override ChunkResponse ProcessRequest(StreamingContext<ChunkRequest> context)
        {
            try
            {
                Args.ThrowIfNull(context?.Request.Body, "context.Request.Message");
                ChunkRequest msg = context.Request.Body;
                IChunk chunk = null;
                switch (msg.Operation)
                {
                    case ChunkOperation.Invalid:
                        throw new InvalidOperationException("Invalid ChunkOperation specified");
                    case ChunkOperation.Get:
                        chunk = ChunkStorage.GetChunk(msg.Hash);                        
                        break;
                    case ChunkOperation.Set:
                        ChunkStorage.SetChunk(msg.Chunk);
                        break;
                    default:
                        break;
                }

                return new ChunkResponse { Success = true, Chunk = chunk };
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception processing request: {0}", ex, ex.Message);
                return new ChunkResponse { Success = false, Message = ex.Message };
            }
        }
    }
}
