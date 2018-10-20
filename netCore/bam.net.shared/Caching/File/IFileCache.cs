using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching.File
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFileCache
    {
        /// <summary>
        /// Gets the zipped bytes.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        byte[] GetZippedBytes(FileInfo file);
        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        byte[] GetBytes(FileInfo file);
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        string GetText(FileInfo file);
        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        void Load(FileInfo file);
    }
}
