using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Files.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using Bam.Net.Server.Binary;

namespace Bam.Net.Services.Chunking
{
    public class ChunkServer : BinaryServer<ChunkRequest, ChunkResponse>
    {
        public ChunkServer(DataSettings dataSettings = null, ILogger logger = null)
        {
            DataSettings = dataSettings ?? DataSettings.Default;
            Logger = logger ?? Log.Default;
        }
        
        public DataSettings DataSettings { get; set; }

        public override ChunkResponse ProcessRequest(BinaryContext<ChunkRequest> context)
        {
            try
            {
                Args.ThrowIfNull(context?.Request.Message, "context.Request.Message");
                ChunkRequest msg = context.Request.Message;
                Chunk chunk = null;
                switch (msg.Operation)
                {
                    case ChunkOperation.Invalid:
                        throw new InvalidOperationException("Invalid ChunkOperation specified");
                    case ChunkOperation.Get:
                        chunk = GetChunk(msg.Hash);                        
                        break;
                    case ChunkOperation.Set:
                        SetChunk(msg.Chunk, false);
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

        protected Chunk SetChunk(Chunk chunk, bool force)
        {
            if(ChunkExists(chunk.Hash, out Chunk result) && !force)
            {
                return result;
            }

            FileInfo file = new FileInfo(GetFilePath(chunk.Hash));
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            File.WriteAllBytes(file.FullName, chunk.Data);
            return chunk;
        }

        protected Chunk GetChunk(string chunkHash)
        {
            if (ChunkExists(chunkHash, out Chunk chunk))
            {
                return chunk;
            }
            else
            {
                Task.Run(() => Logger.AddEntry("Chunk not found: {0}", LogEventType.Warning, chunkHash));
            }
            return null;
        }

        private bool ChunkExists(string hash, out Chunk chunk)
        {
            string filePath = GetFilePath(hash);
            bool result = File.Exists(filePath);
            if (!result)
            {
                chunk = null;
                return result;
            }

            chunk = new Chunk
            {
                Hash = hash,
                Data = File.ReadAllBytes(filePath)
            };
            return result;
        }

        private string GetFilePath(string hash)
        {
            return Path.Combine(GetDirectoryPath(hash), "chunk");
        }

        private string GetDirectoryPath(string hash)
        {
            DirectoryInfo chunksDir = DataSettings.GetChunksDirectory();
            return Path.Combine(chunksDir.FullName, Path.Combine(hash.SplitByLength(2).ToArray()));
        }
    }
}
