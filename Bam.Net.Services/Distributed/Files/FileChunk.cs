/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Services.Distributed.Files
{
	[Serializable]
	public class FileChunk
	{
		public FileChunk()
		{

		}

		public long ChunkIndex { get; set; }

		public long StreamStartIndex { get; set; }		

		public int ChunkLength
		{
			get;
			set;
		}


		string _data;
		public string Data
		{
			get
			{
				if(string.IsNullOrEmpty(_data))
				{
					SetData();
				}
				return _data;
			}
			set
			{
				_data = value;
			}
		}
		
		protected internal FileInfo File { get; set; }

		byte[] _byteData;
		protected internal byte[] ByteData 
		{
			get
			{
				if (_byteData == null)
				{
					SetData();
				}
				return _byteData;
			}
		}

		private void SetData()
		{
			Args.ThrowIfNull(File, "File");

			byte[] buffer = new byte[ChunkLength];
			using (FileStream fs = new FileStream(File.FullName, FileMode.Open))
			{
				fs.Seek(StreamStartIndex, SeekOrigin.Begin);
				fs.Read(buffer, 0, ChunkLength);
			}
			_byteData = buffer;
			Data = _byteData.ToBase64();
		}
	}
}
