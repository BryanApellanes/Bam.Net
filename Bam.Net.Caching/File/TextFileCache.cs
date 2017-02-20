using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching.File
{
    public class TextFileCache: FileCache
    {
        public TextFileCache():base()
        {
            FileExtension = ".txt";
        }
        public TextFileCache(string fileExtension)
        {
            FileExtension = fileExtension;
        }
        public override byte[] GetBytes(FileInfo file)
        {
            throw new InvalidOperationException("Use BinaryFileCache instead");
        }

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

        public override byte[] GetContent(string path)
        {
            return Encoding.UTF8.GetBytes(GetText(new FileInfo(path)));
        }

        public override byte[] GetZippedContent(string path)
        {
            return GetZippedText(new FileInfo(path));
        }
    }
}
