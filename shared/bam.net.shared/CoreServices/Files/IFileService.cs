using System.Collections.Generic;
using System.IO;
using Bam.Net.CoreServices.Files.Data;

namespace Bam.Net.CoreServices.Files
{
    /// <summary>
    /// When implemented provides a mechanism for saving and restoring
    /// files of any kind.
    /// </summary>
    public interface IFileService
    {
        int ChunkDataBatchSize { get; }
        string ChunkDirectory { get; }
        int ChunkLength { get; }

        ChunkData GetChunkData(string chunkHash);
        ChunkData GetChunkDataFromFileSystem(string chunkHash);
        ChunkData GetChunkDataFromRepository(string chunkHash);
        List<FileChunk> GetFileChunks(string fileHash, int fromIndex);
        List<FileChunk> GetFileChunks(string fileHash, int fromIndex, int batchSize);
        ChunkedFileDescriptor GetFileDescriptor(string fileHashOrName);
        ChunkedFileDescriptor GetFileDescriptorByFileHash(string fileHash);
        IEnumerable<ChunkedFileDescriptor> GetFileDescriptorsByFileName(string fileName, string originalDirectory = null);
        ChunkedFileWriter GetFileWriter(string fileHash);
        FileInfo RestoreFile(ChunkedFileDescriptor fileDescriptor, string localPath = null);
        FileInfo RestoreFile(string fileHash, string localPath, bool overwrite = true);
        void SaveChunkData(ChunkData chunk);
        ChunkDataDescriptor SaveChunkDataDescriptor(ChunkDataDescriptor xref);
        ChunkedFileDescriptor SaveFileDescriptor(ChunkedFileDescriptor fileDescriptor);
        ChunkedFileDescriptor StoreFileChunks(FileInfo file, string description = null);
        void WriteFileHashToStream(string fileHash, Stream fs);
        FileInfo WriteFileDataToDirectory(string fileNameOrHash, string directoryPath);
        void WriteFileToStream(string fileNameOrHash, Stream stream);
        IEnumerable<FileChunk> YieldFileChunks(string fileHash, int fromIndex, int batchSize);
    }
}