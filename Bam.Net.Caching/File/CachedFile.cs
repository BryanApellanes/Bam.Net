using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Caching.File
{
    public class CachedFile
    {
        public static implicit operator CachedFile(FileInfo file)
        {
            return new CachedFile(file);
        }

        public static implicit operator FileInfo(CachedFile cached)
        {
            return cached.File;
        }

        public CachedFile(FileInfo file, ILogger logger = null)
        {
            FullName = file.FullName;
            File = file;
            if (File.Exists)
            {
                Task.Run(() => Load(logger ?? Log.Default));
                file.OnChange(async (s, a) => 
                {
                    _zippedText = null;
                    _zippedBytes = null;
                    _text = null;
                    _bytes = null;
                    await Load(logger);
                });
            }
        }

        public async Task<bool> Load(ILogger logger = null)
        {
            try
            {
                logger = logger ?? Log.Default;
                await Task.Run(() => ContentHash = File.ContentHash(HashAlgorithms.MD5));
                await Task.Run(() => GetZippedText());
                await Task.Run(() => GetZippedBytes());
                return true;
            }
            catch (Exception ex)
            {
                logger.AddEntry("Error loading file {0}: {1}", ex, File?.FullName ?? "<null>", ex.Message);
                return false;
            }
        }

        public bool Loaded { get; set; }
        protected internal Exception LoadException { get; set; }
        public string ContentHash { get; private set; }
        public string FullName { get; private set; }
        internal FileInfo File { get; set; }
        byte[] _zippedBytes;
        object _zippedByteLock = new object();
        public byte[] GetZippedBytes()
        {
            byte[] gzipped = _zippedByteLock.DoubleCheckLock(ref _zippedBytes, () => GetBytes().GZip());
            return gzipped;
        }
        byte[] _bytes;
        object _byteLock = new object();
        public byte[] GetBytes()
        {
            byte[] bytes = _byteLock.DoubleCheckLock(ref _bytes, () => System.IO.File.ReadAllBytes(FullName));
            return bytes;
        }
        string _text;
        object _textLock = new object();
        public string GetText()
        {
            string text = _textLock.DoubleCheckLock(ref _text, () => System.IO.File.ReadAllText(FullName));
            return text;
        }
        byte[] _zippedText;
        object _zippedTextLock = new object();
        public byte[] GetZippedText()
        {
            byte[] zippedText = _zippedTextLock.DoubleCheckLock(ref _zippedText, () => GetText().GZip());
            return zippedText; 
        }
        public override bool Equals(object obj)
        {
            CachedFile meta = obj as CachedFile;
            if (meta == null)
            {
                return false;
            }
            return meta.ContentHash.Equals(ContentHash) && meta.FullName.Equals(FullName);
        }
        public override string ToString()
        {
            return $"{FullName}:{ContentHash}";
        }
        public override int GetHashCode()
        {
            return ContentHash.GetHashCode();
        }        
    }
}
