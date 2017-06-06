﻿namespace Bam.Net.Services.Distributed.Files
{
    public interface IChunkedFileDescriptor
    {
        long ChunkCount { get; }
        int ChunkLength { get; }
        string FileHash { get; }
        long FileLength { get; }
        string FileName { get; set; }
        string OriginalDirectory { get; }
    }
}