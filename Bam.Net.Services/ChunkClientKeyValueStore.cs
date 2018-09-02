using Bam.Net.CoreServices.Files;
using Bam.Net.Logging;
using Bam.Net.Services.Chunking;
using Bam.Net.Services.DataReplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    /// <summary>
    /// A key value store implementation that uses a ChunkClient to store
    /// key values as chunks.
    /// </summary>
    /// <seealso cref="Bam.Net.Services.DataReplication.IKeyValueStore" />
    /// <seealso cref="Bam.Net.CoreServices.Files.IChunkStorage" />
    public class ChunkClientKeyValueStore : IKeyValueStore, IChunkStorage
    {
        public ChunkClientKeyValueStore(string hostName, int port, Encoding encoding = null, ILogger logger = null) : this(new ChunkClient(hostName, port), encoding, logger)
        { }

        public ChunkClientKeyValueStore(ChunkClient chunkClient, Encoding encoding = null, ILogger logger = null)
        {
            ChunkClient = chunkClient;
            Encoding = encoding ?? Encoding.UTF8;
            Logger = logger ?? Log.Default;            
        }

        public ChunkClient ChunkClient { get; set; }
        public Encoding Encoding { get; set; }
        public ILogger Logger { get; set; }

        public string Get(string key)
        {
            IChunk chunk = ChunkClient.GetChunk(key);
            if(chunk != null)
            {
                return chunk.Data?.ToBase64() ?? string.Empty;
            }
            return string.Empty;
        }

        public bool Set(string key, string value)
        {
            try
            {
                SetChunk(new Chunk { Hash = key, Data = Encoding.GetBytes(value) });
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception setting key value key={0}, value={1}: {2}", ex, key, value, ex.Message);
                return false;
            }
        }

        public void SetChunk(IChunk chunk)
        {
            ChunkClient.SetChunk(chunk);
        }

        public IChunk GetChunk(string hash)
        {
            return ChunkClient.GetChunk(hash);
        }
    }
}
