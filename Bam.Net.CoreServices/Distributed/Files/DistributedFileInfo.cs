/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.CoreServices.Distributed.Files
{
	[Serializable]
	public class DistributedFileInfo
	{
		public DistributedFileInfo()
		{
			this.FileChunks = new List<FileChunk>();
		}
		public long ChunkByteSize { get; set; }
		public string FileName { get; set; }
		public string Md5 { get; set; }
		public long Length { get; private set; }
		public List<FileChunk> FileChunks { get; private set; }

		public static DistributedFileInfo Load(FileInfo file, long chunkByteSize = 512000)
		{
			DistributedFileInfo result = new DistributedFileInfo();
			long length = file.Length;
			result.FileName = file.Name;
			result.Md5 = file.Md5();
			result.Length = length;
			result.ChunkByteSize = chunkByteSize;

			decimal wholeChunkCount = Math.Floor((decimal)length / (decimal)chunkByteSize);
			long tailBytes = length % chunkByteSize;
			bool partialTail = tailBytes > 0;

			for (int i = 0; i < wholeChunkCount; i++)
			{
				throw new NotImplementedException();
			}

			return result;
		}
	}
}
