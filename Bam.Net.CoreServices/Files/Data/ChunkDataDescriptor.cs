using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.Files.Data
{
    /// <summary>
    /// A descriptor for the relationship between
    /// a ChunkedFileDescriptor and a ChunkData instance.  There
    /// will be multiple instances of these
    /// relating one file to many ChunkData instances
    /// </summary>
    [Serializable]
    public class ChunkDataDescriptor: RepoData
    {
        /// <summary>
        /// The Sha256 hash of the file this
        /// chunk is from
        /// </summary>
        public string FileHash { get; set; }
        /// <summary>
        /// The Sha256 hash of this chunks
        /// byte[] data
        /// </summary>
        public string ChunkHash { get; set; }
        /// <summary>
        /// The index of this chunk relative to 
        /// all file chunks
        /// </summary>
        public long ChunkIndex { get; set; }
        /// <summary>
        /// The index in the file stream
        /// where this chunk begins
        /// </summary>
        public long StreamIndex { get; set; }

        public byte[] GetData(FileService fileService)
        {
            return fileService.GetChunkData(ChunkHash).Data.FromBase64();
        }
    }
}
