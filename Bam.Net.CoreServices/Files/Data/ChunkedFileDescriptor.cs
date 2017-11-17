using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.Files.Data
{
    [Serializable]
    public class ChunkedFileDescriptor: RepoData
    {
        public string FileHash { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string OriginalDirectory { get; set; }
        public long FileLength { get; set; }
        public long ChunkCount { get; set; }
        /// <summary>
        /// The specified ChunkLength at the time
        /// of chunking
        /// </summary>
        public int ChunkLength { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is ChunkedFileDescriptor o)
            {
                return o.FileHash.Equals(FileHash);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return FileHash.GetHashCode();
        }
    }
}
