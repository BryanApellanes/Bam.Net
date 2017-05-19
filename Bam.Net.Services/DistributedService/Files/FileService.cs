/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Services.DistributedService.Data;
using Bam.Net.Services.DistributedService.Data.Files;

namespace Bam.Net.Services.DistributedService.Files
{
    [RoleRequired("/", "Admin")]
    [Proxy("fileSvc")]
    public class FileService: ProxyableService
    {
        public FileService(IRepository repository, FileServiceSettings settings) : this(repository)
        {
            this.CopyProperties(settings);
            ChunkDirectory = new FileInfo(ChunkDirectory).FullName;
        }

        public FileService(IRepository repository)
        {
            Repository = repository;
            Repository.AddTypes(new Type[]
            {
                typeof(ChunkedFileDescriptor),
                typeof(ChunkDataDescriptor),
                typeof(ChunkData)
            });
            ChunkDataBatchSize = 10;
            ChunkLength = 256000;
            ChunkDirectory = new FileInfo(".\\ChunkData").FullName;

            SetChunkDataDescriptorRetriever();
        }

        public override object Clone()
        {
            FileService clone = new FileService(Repository);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        public Func<string, int, int, IEnumerable<ChunkDataDescriptor>> ChunkDataDescriptorRetriever;
        public string ChunkDirectory { get; internal set; }
        public int ChunkDataBatchSize { get; internal set; }
        public int ChunkLength { get; internal set; }

        public virtual ChunkDataDescriptor SaveChunkDataDescriptor(ChunkDataDescriptor xref)
        {
            ChunkDataDescriptor existingXref = Repository
                               .Query<ChunkDataDescriptor>(
                                       Filter.Where(nameof(ChunkDataDescriptor.FileHash)) == xref.FileHash &&
                                       Filter.Where(nameof(ChunkDataDescriptor.ChunkHash)) == xref.ChunkHash &&
                                       Filter.Where(nameof(ChunkDataDescriptor.ChunkIndex)) == xref.ChunkIndex)
                                   .FirstOrDefault();
            if (existingXref == null)
            {
                existingXref = Repository.Save(xref);
            }
            return existingXref;
        }

        public virtual ChunkedFileDescriptor SaveFileDescriptor(ChunkedFileDescriptor fileDescriptor)
        {
            Args.ThrowIfNull(fileDescriptor, "fileDescriptor");
            ChunkedFileDescriptor existing = Repository.Query<ChunkedFileDescriptor>(
                Filter.Where(nameof(ChunkedFileDescriptor.FileHash)) == fileDescriptor.FileHash &&
                Filter.Where(nameof(ChunkedFileDescriptor.FileName)) == fileDescriptor.FileName &&
                Filter.Where(nameof(ChunkedFileDescriptor.OriginalDirectory)) == fileDescriptor.OriginalDirectory).FirstOrDefault();
            if (existing == null)
            {
                existing = Repository.Save(fileDescriptor);
            }
            return existing;
        }

        public virtual ChunkData SaveChunkData(ChunkData chunk)
        {
            Args.ThrowIf(!chunk.ChunkHash.Equals(chunk.Data.FromBase64().Sha256()), "Hash validation failed");
            ChunkData existingChunk = Repository.Query<ChunkData>(Filter.Where(nameof(ChunkData.ChunkHash)) == chunk.ChunkHash).FirstOrDefault();
            if (existingChunk == null)
            {
                existingChunk = Repository.Save(chunk);
            }
            return existingChunk;
        }

        public virtual ChunkedFileDescriptor GetFileDescriptor(string fileHashOrName)
        {
            ChunkedFileDescriptor descriptor = GetFileDescriptorByFileHash(fileHashOrName);
            if (descriptor == null)
            {
                descriptor = GetFileDescriptorsByFileName(fileHashOrName).SingleOrDefault();
            }
            return descriptor;
        }

        public virtual ChunkedFileDescriptor GetFileDescriptorByFileHash(string fileHash)
        {
            return Repository.Query<ChunkedFileDescriptor>(Filter.Where(nameof(ChunkedFileDescriptor.FileHash)) == fileHash).SingleOrDefault();
        }

        public virtual IEnumerable<ChunkedFileDescriptor> GetFileDescriptorsByFileName(string fileName, string originalDirectory = null)
        {
            QueryFilter filter = Filter.Where(nameof(ChunkedFileDescriptor.FileName)) == fileName;
            if (!string.IsNullOrEmpty(originalDirectory))
            {
                filter = filter && Filter.Where(nameof(ChunkedFileDescriptor.OriginalDirectory)) == originalDirectory;
            }
            return Repository.Query<ChunkedFileDescriptor>(filter);
        }

        public virtual List<FileChunk> GetFileChunks(string fileHash, int lastIndex, int batchSize)
        {
            IEnumerable<ChunkDataDescriptor> xrefs = ChunkDataDescriptorRetriever(fileHash, lastIndex, batchSize);
            List<FileChunk> chunks = new List<FileChunk>();
            foreach (ChunkDataDescriptor xref in xrefs)
            {
                ChunkData data = GetChunkData(xref.ChunkHash);
                chunks.Add(new FileChunk
                {
                    FileHash = xref.FileHash,
                    ChunkHash = xref.ChunkHash,
                    ChunkIndex = xref.ChunkIndex,
                    StreamIndex = xref.StreamIndex,
                    ChunkLength = data.ChunkLength,
                    Data = data.Data
                });
            }
            chunks.Sort((x, y) => x.ChunkIndex.CompareTo(y.ChunkIndex));
            return chunks;
        }

        public virtual ChunkData GetChunkData(string chunkHash)
        {
            ChunkData result = new ChunkData { ChunkHash = chunkHash };
            string chunkDataFile = Path.Combine(ChunkDirectory, chunkHash);
            if (File.Exists(chunkDataFile))
            {
                string data = chunkDataFile.SafeReadFile();
                result = new ChunkData
                {
                    ChunkHash = chunkHash,
                    Data = data,
                    ChunkLength = data.FromBase64().Length
                };
            }
            else
            {
                ChunkData data = Repository.Query<ChunkData>(Filter.Where(nameof(ChunkData.ChunkHash)) == chunkHash).FirstOrDefault();
                Args.ThrowIf(data == null, "Chunk not found with hash of ({0})", chunkHash);
                result = data;
                Task.Run(() => chunkDataFile.SafeWriteFile(result.Data, true));
            }
            return result;
        }

        /// <summary>
        /// Save the specified file into the repository as chunks
        /// </summary>
        /// <param name="file"></param>
        /// <param name="chunkLength"></param>
        /// <returns></returns>
        [Exclude]
        public ChunkedFileDescriptor StoreFileChunksInRepo(FileInfo file, string description = null)
        {
            ChunkedFile chunked = new ChunkedFile(file, ChunkLength);
            ChunkedFileDescriptor chunkedFileDescriptor = chunked.ToChunkedFileDescriptor(description);
            SaveFileDescriptor(chunkedFileDescriptor);

            foreach (ChunkedFileDataDescriptor chunkFileDataDescriptor in chunked.ToChunkedFileDataDescriptor())
            {
                ChunkData chunk = chunkFileDataDescriptor.ChunkData;
                SaveChunkData(chunk);
                ChunkDataDescriptor xref = chunkFileDataDescriptor.ChunkDataDescriptor;
                SaveChunkDataDescriptor(xref);
            }
            return chunkedFileDescriptor;
        }

        [Exclude]
        public FileInfo RestoreFile(ChunkedFileDescriptor fileDescriptor, string localPath = null)
        {
            return RestoreFile(fileDescriptor.FileHash, localPath ?? Path.Combine(fileDescriptor.OriginalDirectory, fileDescriptor.FileName));
        }

        [Exclude]
        public FileInfo RestoreFile(string fileHash, string localPath, bool overwrite = true)
        {
            HandleExistingFile(localPath, overwrite);

            using (FileStream fs = new FileStream(localPath, FileMode.Create))
            {
                List<FileChunk> chunks = GetFileChunks(fileHash, -1);
                while (chunks.Count > 0)
                {
                    foreach (FileChunk chunk in chunks)
                    {
                        fs.Write(chunk.ByteData, 0, chunk.ByteData.Length);
                    }
                    chunks = GetFileChunks(fileHash, (int)(chunks[chunks.Count - 1].ChunkIndex));
                }
            }

            return new FileInfo(localPath);
        }

        private List<FileChunk> GetFileChunks(string fileHash, int lastIndex)
        {
            return GetFileChunks(fileHash, lastIndex, ChunkDataBatchSize);
        }

        private static void HandleExistingFile(string localPath, bool overwrite)
        {
            if (File.Exists(localPath))
            {
                if (overwrite)
                {
                    File.Delete(localPath);
                }
                else
                {
                    throw new InvalidOperationException($"File already exists: {localPath}");
                }
            }
        }

        private void SetChunkDataDescriptorRetriever()
        {
            DaoRepository daoRepo = Repository as DaoRepository;
            if (daoRepo != null)
            {
                ChunkDataDescriptorRetriever = (fileHash, lastIndex, chunkDataBatchSize) =>
                {
                    return daoRepo.Top<ChunkDataDescriptor>(chunkDataBatchSize,
                                    Filter.Where(nameof(ChunkDataDescriptor.FileHash)) == fileHash &&
                                    Filter.Where(nameof(ChunkDataDescriptor.ChunkIndex)) > lastIndex);
                };
            }
            else
            {
                ChunkDataDescriptorRetriever = (fileHash, lastIndex, chunkDataBatchSize) =>
                {
                    return Repository.Query<ChunkDataDescriptor>(
                                        Filter.Where(nameof(ChunkDataDescriptor.FileHash)) == fileHash &&
                                        Filter.Where(nameof(ChunkDataDescriptor.ChunkIndex)) > lastIndex)
                                    .Take(chunkDataBatchSize);
                };
            }
        }
    }
}
