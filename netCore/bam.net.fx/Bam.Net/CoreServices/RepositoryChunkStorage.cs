using Bam.Net.CoreServices.Files;
using Bam.Net.CoreServices.Files.Data;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// An IChunkStorage implementation that stores chunk data in an IRepository.
    /// </summary>
    /// <seealso cref="Bam.Net.CoreServices.Files.IChunkStorage" />
    public class RepositoryChunkStorage: IChunkStorage
    {
        public RepositoryChunkStorage()
        {
        }

        public RepositoryChunkStorage(DefaultDataDirectoryProvider dataSettings, ILogger logger = null)
        {
            DataSettings = dataSettings;
            Repository = new DaoRepository();
            Repository.AddType<ChunkData>();
        }

        public RepositoryChunkStorage(IRepository repository, DefaultDataDirectoryProvider dataSettings, ILogger logger = null):this(dataSettings, logger)
        {
            Repository = repository;
        }

        public DefaultDataDirectoryProvider DataSettings { get; set; }
        public IRepository Repository { get; set; }

        public IChunk GetChunk(string hash)
        {
            ChunkData data = Repository.Query(nameof(ChunkData.ChunkHash), hash).CopyAs<ChunkData>().FirstOrDefault();
            return data?.ToChunk();
        }

        public void SetChunk(IChunk chunk)
        {
            Args.ThrowIf(!chunk.Hash.Equals(chunk.Data.Sha256()), "Hash validation failed");
            Task.Run(() =>
            {
                ChunkData existingChunk = Repository.Query<ChunkData>(Filter.Where(nameof(ChunkData.ChunkHash)) == chunk.Hash).FirstOrDefault();
                if (existingChunk == null || !existingChunk.Data.FromBase64().Sha256().Equals(chunk.Data.Sha256()))
                {
                    Repository.Save(ChunkData.FromChunk(chunk));
                }
            });         
        }
    }
}
