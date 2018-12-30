using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.CoreServices.Files.Data;

namespace Bam.Net.CoreServices.Files
{
    public class ChunkedFileWriter: IChunkedFileDescriptor
    {
        internal ChunkedFileWriter()
        {

        }

        internal ChunkedFileWriter(FileService fileService)
        {
            FileService = fileService;
        }

        public FileService FileService { get; set; }

        public long ChunkCount { get; set; }

        public int ChunkLength { get; set; }

        public string FileHash { get; set; }

        public long FileLength { get; set; }

        public string FileName { get; set; }

        public string OriginalDirectory { get; set; }
        public ILogger Logger { get; set; }

        public static ChunkedFileWriter FromFileHash(FileService svc, string fileHash, ILogger logger = null)
        {
            ChunkedFileDescriptor descriptor = svc.GetFileDescriptor(fileHash);
            ChunkedFileWriter writer = new ChunkedFileWriter(svc)
            {
                FileHash = fileHash,
                ChunkCount = descriptor.ChunkCount,
                ChunkLength = descriptor.ChunkLength,
                FileLength = descriptor.FileLength,
                FileName = descriptor.FileName,
                OriginalDirectory = descriptor.OriginalDirectory, 
                Logger = logger
            };
            return writer;
        }
        
        public Task Write(string localPath)
        {
            return Task.Run(() => FileService.RestoreFile(FileHash, localPath));
        }
    }
}
