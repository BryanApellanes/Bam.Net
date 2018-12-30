using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching.File
{
    /// <summary>
    /// A class representing a cached binary file.
    /// </summary>
    /// <seealso cref="Bam.Net.Caching.File.FileCache" />
    public class BinaryFileCache : FileCache
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public override byte[] GetContent(string filePath)
        {
            return GetBytes(new System.IO.FileInfo(filePath));
        }

        /// <summary>
        /// Gets the content of the zipped file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public override byte[] GetZippedContent(string filePath)
        {
            return GetZippedBytes(new System.IO.FileInfo(filePath));
        }
    }
}
