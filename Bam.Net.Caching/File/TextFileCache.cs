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
    /// <seealso cref="Bam.Net.Caching.File.FileCache" />
    public class TextFileCache: FileCache
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextFileCache"/> class.
        /// </summary>
        public TextFileCache():base()
        {
            FileExtension = ".txt";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFileCache"/> class.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        public TextFileCache(string fileExtension)
        {
            FileExtension = fileExtension;
        }

        /// <summary>
        /// The result of ReadAllBytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Use BinaryFileCache instead</exception>
        public override byte[] GetBytes(FileInfo file)
        {
            throw new InvalidOperationException("Use BinaryFileCache instead");
        }

        /// <summary>
        /// The result of zipping ReadAllBytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Use BinaryFileCache instead</exception>
        public override byte[] GetZippedBytes(FileInfo file)
        {
            throw new InvalidOperationException("Use BinaryFileCache instead");
        }

        /// <summary>
        /// The result of ReadAllText
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public override string GetText(FileInfo file)
        {
            Args.ThrowIf(!Path.GetExtension(file.FullName).ToLowerInvariant().Equals(FileExtension), "File must be a text file with the extension {0}", FileExtension);
            return base.GetText(file);
        }

        /// <summary>
        /// The result of zipping ReadAllText
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public override byte[] GetZippedText(FileInfo file)
        {
            Args.ThrowIf(!Path.GetExtension(file.FullName).ToLowerInvariant().Equals(FileExtension), "File must be a text file with the extension {0}", FileExtension);
            return base.GetZippedText(file);
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override byte[] GetContent(string path)
        {
            return Encoding.UTF8.GetBytes(GetText(new FileInfo(path)));
        }

        /// <summary>
        /// Gets the content of the zipped.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override byte[] GetZippedContent(string path)
        {
            return GetZippedText(new FileInfo(path));
        }
    }
}
