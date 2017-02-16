using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching.File
{
    public class BinaryFileCache : FileCache
    {
        public override byte[] GetContent(string filePath)
        {
            return GetBytes(new System.IO.FileInfo(filePath));
        }

        public override byte[] GetZippedContent(string filePath)
        {
            return GetZippedBytes(new System.IO.FileInfo(filePath));
        }
    }
}
