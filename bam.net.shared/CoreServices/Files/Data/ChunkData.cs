using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.Files.Data
{
    /// <summary>
    /// Represent an arbitrary chunk of data (base 64 encoded)
    /// identified by its hash (Sha256)
    /// </summary>
    [Serializable]
    public class ChunkData: RepoData, IChunkable
    {
        /// <summary>
        /// The Sha256 hash of the base 64 decoded
        /// value of this chunks Data
        /// </summary>
        public string ChunkHash { get; set; }

        /// <summary>
        /// The length of the base 64 decoded
        /// value of this chunks Data
        /// </summary>
        public int ChunkLength { get; set; }

        /// <summary>
        /// Base64 encoded data
        /// </summary>
        public string Data { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ChunkData data)
            {
                return data.ChunkHash.Equals(ChunkHash);
            }
            return false;
        }

        public IChunk ToChunk()
        {
            return new Chunk { Hash = ChunkHash, Data = Data.FromBase64() };
        }

        public static ChunkData FromChunk(IChunk chunk)
        {
            return new ChunkData { ChunkHash = chunk.Hash, Data = chunk.Data.ToBase64(), ChunkLength = chunk.Data.Length };
        }

        public override int GetHashCode()
        {
            return ChunkHash.GetHashCode();
        }
    }
}
