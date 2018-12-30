using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Caching.File
{
    /// <summary>
    /// A class representing a cached file.
    /// </summary>
    public class CachedFile
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="FileInfo"/> to <see cref="CachedFile"/>.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator CachedFile(FileInfo file)
        {
            return new CachedFile(file);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="CachedFile"/> to <see cref="FileInfo"/>.
        /// </summary>
        /// <param name="cached">The cached.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator FileInfo(CachedFile cached)
        {
            return cached.File;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedFile"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="logger">The logger.</param>
        public CachedFile(FileInfo file, ILogger logger = null)
        {
            FullName = file.FullName;
            File = file;
            if (File.Exists)
            {
                ContentHash = File.ContentHash(HashAlgorithms.MD5);
                Task.Run(() => Load(logger ?? Log.Default));
                file.OnChange(async (s, a) => await Reload(logger));
            }
        }

        /// <summary>
        /// Reloads the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <returns></returns>
        public async Task<bool> Reload(ILogger logger = null)
        {
            Thread.Sleep(300);
            _zippedText = null;
            _zippedBytes = null;
            _text = null;
            _bytes = null;
            return await Load(logger);
        }

        /// <summary>
        /// Loads the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <returns></returns>
        public async Task<bool> Load(ILogger logger = null)
        {
            try
            {
                if (File.Exists)
                {
                    logger = logger ?? Log.Default;
                    await Task.Run(() => GetText());
                    await Task.Run(() => ContentHash = File.ContentHash(HashAlgorithms.MD5));
                    await Task.Run(() => GetZippedText());
                    await Task.Run(() => GetZippedBytes());
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.AddEntry("Error loading file {0}: {1}", ex, File?.FullName ?? "<null>", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CachedFile"/> is loaded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if loaded; otherwise, <c>false</c>.
        /// </value>
        public bool Loaded { get; set; }

        /// <summary>
        /// Gets or sets the load exception.
        /// </summary>
        /// <value>
        /// The load exception.
        /// </value>
        protected internal Exception LoadException { get; set; }

        /// <summary>
        /// Gets the content hash.
        /// </summary>
        /// <value>
        /// The content hash.
        /// </value>
        public string ContentHash { get; private set; }
        /// <summary>
        /// Gets the full name of the file.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; private set; }

        internal FileInfo File { get; set; }

        byte[] _zippedBytes;
        object _zippedByteLock = new object();
        /// <summary>
        /// Gets the zipped bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] GetZippedBytes()
        {
            byte[] gzipped = _zippedByteLock.DoubleCheckLock(ref _zippedBytes, () => GetBytes().GZip());
            return gzipped;
        }

        byte[] _bytes;
        object _byteLock = new object();
        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            byte[] bytes = _byteLock.DoubleCheckLock(ref _bytes, () => System.IO.File.ReadAllBytes(FullName));
            return bytes;
        }

        string _text;
        object _textLock = new object();
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            string text = _textLock.DoubleCheckLock(ref _text, () => System.IO.File.ReadAllText(FullName));
            return text;
        }

        byte[] _zippedText;
        object _zippedTextLock = new object();
        /// <summary>
        /// Gets the zipped text.
        /// </summary>
        /// <returns></returns>
        public byte[] GetZippedText()
        {
            byte[] zippedText = _zippedTextLock.DoubleCheckLock(ref _zippedText, () => GetText().GZip());
            return zippedText; 
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            CachedFile meta = obj as CachedFile;
            if (meta == null)
            {
                return false;
            }
            return meta.ContentHash.Equals(ContentHash) && meta.FullName.Equals(FullName);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{FullName}:{ContentHash}";
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return ContentHash.GetHashCode();
        }        
    }
}
