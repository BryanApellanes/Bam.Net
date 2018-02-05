/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net;
using System.Collections.Concurrent;

namespace Bam.Net.Caching.File
{
	/// <summary>
	/// A caching mechanism for files. 
	/// </summary>
	public abstract class FileCache: IFileCache
	{
        static object _lock = new object();
        static ConcurrentDictionary<string, CachedFile> _cachedFiles;        
        static ConcurrentDictionary<string, string> _textCache;
        static ConcurrentDictionary<string, byte[]> _zippedTextCache;
        static ConcurrentDictionary<string, byte[]> _byteCache;
        static ConcurrentDictionary<string, byte[]> _zippedByteCache;

        public FileCache()
        {
            _lock.DoubleCheckLock(ref _cachedFiles, () => new ConcurrentDictionary<string, CachedFile>());
            _lock.DoubleCheckLock(ref _textCache, () => new ConcurrentDictionary<string, string>());
            _lock.DoubleCheckLock(ref _zippedTextCache, () => new ConcurrentDictionary<string, byte[]>());
            _lock.DoubleCheckLock(ref _byteCache, () => new ConcurrentDictionary<string, byte[]>());
            _lock.DoubleCheckLock(ref _zippedByteCache, () => new ConcurrentDictionary<string, byte[]>());
        }
        public string FileExtension { get; protected set; }
        public abstract byte[] GetContent(string filePath);
        public abstract byte[] GetZippedContent(string filePath);

        /// <summary>
        /// The result of zipping ReadAllBytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual byte[] GetZippedBytes(FileInfo file)
        {
            if (!_zippedByteCache.ContainsKey(file.FullName))
            {
                Load(file);
            }
            return _zippedByteCache[file.FullName];
        }

        /// <summary>
        /// The result of ReadAllBytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual byte[] GetBytes(FileInfo file)
        {
            if (!_byteCache.ContainsKey(file.FullName))
            {
                Load(file);
            }
            return _byteCache[file.FullName];
        }

        /// <summary>
        /// The result of ReadAllText
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual string GetText(FileInfo file)
        {
            if (!_textCache.ContainsKey(file.FullName))
            {
                Load(file);
            }
            return _textCache[file.FullName];
        }
        
        /// <summary>
        /// The result of zipping ReadAllText
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual byte[] GetZippedText(FileInfo file)
        {
            if (!_zippedTextCache.ContainsKey(file.FullName))
            {
                Load(file);
            }
            return _zippedTextCache[file.FullName];
        }
        public virtual void Reload(FileInfo file)
        {
            string text;
            byte[] bytes;
            byte[] zipped;
            byte[] zippedText;
            _textCache.TryRemove(file.FullName, out text);
            _byteCache.TryRemove(file.FullName, out bytes);
            _zippedByteCache.TryRemove(file.FullName, out zipped);
            _zippedTextCache.TryRemove(file.FullName, out zippedText);
            Load(file);
        }
        public virtual void Load(FileInfo file)
        {
            string fullName = file.FullName;

            CachedFile cachedFile = new CachedFile(file);
            if (_cachedFiles.TryAdd(fullName, cachedFile))
            {
                string text = _cachedFiles[fullName].GetText();
                byte[] bytes = _cachedFiles[fullName].GetBytes();
                byte[] zippedBytes = _cachedFiles[fullName].GetZippedBytes();
                byte[] zippedText = _cachedFiles[fullName].GetZippedText();

                _textCache.TryAdd(fullName, text);
                _byteCache.TryAdd(fullName, bytes);
                _zippedByteCache.TryAdd(fullName, zippedBytes);
                _zippedTextCache.TryAdd(fullName, zippedText);
            }
        }      
    }
}
