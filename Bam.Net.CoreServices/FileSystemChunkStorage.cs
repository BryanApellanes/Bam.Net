using Bam.Net.CoreServices.Files;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// An IChunkStorage implementation that stores chunks in the file system.
    /// </summary>
    /// <seealso cref="Bam.Net.CoreServices.Files.IChunkStorage" />
    public class FileSystemChunkStorage: IChunkStorage
    {
        public FileSystemChunkStorage()
        {
            DataSettings = DefaultDataDirectoryProvider.Current;
            Logger = Log.Default;
        }

        public FileSystemChunkStorage(DefaultDataDirectoryProvider dataSettings, ILogger logger = null)
        {
            DataSettings = dataSettings;
            Logger = logger;
        }

        public IDataDirectoryProvider DataSettings { get; set; }
        public ILogger Logger { get; set; }
        public void SetChunk(IChunk chunk)
        {
            SetChunk(chunk, true);
        }

        public IChunk GetChunk(string chunkHash)
        {
            if (ChunkExists(chunkHash, out IChunk chunk))
            {
                return chunk;
            }
            else
            {
                Task.Run(() => Logger.AddEntry("Chunk not found: {0}", LogEventType.Warning, chunkHash));
            }
            return null;
        }

        protected IChunk SetChunk(IChunk chunk, bool force)
        {
            if (ChunkExists(chunk.Hash, out IChunk result) && !force)
            {
                return result;
            }

            FileInfo file = new FileInfo(GetChunkFilePath(chunk.Hash));
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            File.WriteAllBytes(file.FullName, chunk.Data);
            return chunk;
        }

        private bool ChunkExists(string hash, out IChunk chunk)
        {
            string filePath = GetChunkFilePath(hash);
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

        private string GetChunkFilePath(string hash)
        {
            return Path.Combine(GetChunkDirectoryPath(hash), "chunk");
        }

        private string GetChunkDirectoryPath(string hash)
        {
            DirectoryInfo chunksDir = DataSettings.GetChunksDirectory();
            return Path.Combine(chunksDir.FullName, Path.Combine(hash.SplitByLength(2).ToArray()));
        }        
    }
}
