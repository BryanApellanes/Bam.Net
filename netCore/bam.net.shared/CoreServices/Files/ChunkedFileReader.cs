using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Bam.Net.Services.Data;
using Bam.Net.CoreServices.Files.Data;

namespace Bam.Net.CoreServices.Files
{
    /// <summary>
    /// Represents a local file and its chunks.  Should
    /// not be persisted use ChunkedFileDescriptor instead
    /// </summary>
    public class ChunkedFileReader : IChunkedFileDescriptor
    {
        public ChunkedFileReader(FileInfo file, int chunkLength = 256000)
        {
            Args.ThrowIfNull(file, "file");
            Args.ThrowIf(!file.Exists, "File {0} doesn't exist", file.FullName);
            File = file;
            FileName = file.Name;
            OriginalDirectory = file.Directory.FullName;
            ChunkLength = chunkLength;
            FileHash = file.Sha256();
            FileLength = file.Length;
            WholeChunkCount = Math.Floor((decimal)file.Length / (decimal)chunkLength);
            TailLength = FileLength % ChunkLength;
            PartialTail = TailLength > 0;
        }

        public string FileName { get; set; }
        /// <summary>
        ///  The original directory the file was in
        ///  when chunked
        /// </summary>
        public string OriginalDirectory { get; }
        public int ChunkLength { get; }
        
        public string FileHash { get; }

        public long FileLength { get; }

        public long ChunkCount
        {
            get
            {
                if (PartialTail)
                {
                    return (long)WholeChunkCount + 1;
                }
                return (long)WholeChunkCount;
            }
        }

        public FileChunk this[int chunkIndex]
        {
            get
            {
                FileChunk chunk = new FileChunk()
                {
                    ChunkIndex = chunkIndex,
                    FileHash = FileHash,
                    ByteData = ReadChunk(chunkIndex, out long streamIndex),
                    StreamIndex = streamIndex
                };
                chunk.ChunkLength = chunk.ByteData.Length;
                return chunk;
            }
        }

        public byte[] ReadChunk(int chunkIndex, out long streamIndex)
        {
            Args.ThrowIf<ArgumentOutOfRangeException>(chunkIndex < 0 || chunkIndex > (ChunkCount - 1), "ChunkIndex out of range: {0}", chunkIndex);
            if (PartialTail && chunkIndex == ChunkCount - 1)
            {
                return ReadTailFileSystemChunk(out streamIndex);
            }
            else
            {
                return ReadWholeFileSystemChunk(chunkIndex, out streamIndex);
            }
        }

        public ChunkedFileDescriptor ToChunkedFileDescriptor(string description = null)
        {
            return new ChunkedFileDescriptor
            {
                FileHash = FileHash,
                FileName = FileName,
                Description = description,
                OriginalDirectory = OriginalDirectory,
                FileLength = FileLength,
                ChunkLength = ChunkLength,
                ChunkCount = ChunkCount
            };
        }

        public IEnumerable<ChunkedFileDataDescriptor> ToChunkedFileDataDescriptor()
        {
            for(int i = 0; i < ChunkCount; i++)
            {
                FileChunk chunk = this[i];
                yield return new ChunkedFileDataDescriptor
                {
                    ChunkData = chunk.ToChunkData(),
                    ChunkDataDescriptor = chunk.ToChunkDataDescriptor()
                };
            }
        }

        protected internal decimal WholeChunkCount { get; set; }
        protected internal long TailLength { get; set; }
        protected internal bool PartialTail { get; set; }
        protected long TailStreamIndex
        {
            get
            {
                return (long)WholeChunkCount * ChunkLength;
            }
        }

        protected FileInfo File { get; set; }
        private byte[] ReadWholeFileSystemChunk(int chunkIndex, out long streamIndex)
        {
            streamIndex = chunkIndex * ChunkLength;
            byte[] buffer = new byte[ChunkLength];
            using (FileStream fs = new FileStream(File.FullName, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(streamIndex, SeekOrigin.Begin);
                fs.Read(buffer, 0, ChunkLength);
            }

            return buffer;
        }

        private byte[] ReadTailFileSystemChunk(out long streamIndex)
        {
            streamIndex = TailStreamIndex;
            byte[] buffer = new byte[TailLength];
            using (FileStream fs = new FileStream(File.FullName, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(TailStreamIndex, SeekOrigin.Begin);
                fs.Read(buffer, 0, (int)TailLength);
            }
            return buffer;            
        }
    }
}
