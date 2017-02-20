using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching.File
{
    public interface IFileCache
    {
        byte[] GetZippedBytes(FileInfo file);
        byte[] GetBytes(FileInfo file);
        string GetText(FileInfo file);
        void Load(FileInfo file);
    }
}
