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
	/// A caching mechanism for text files. 
	/// </summary>
	public abstract class FileCache: IFileCache
	{
        Dictionary<string, FileMeta> _metaCache;
        Dictionary<string, string> _textCache;
        Dictionary<string, byte[]> _zippedTextCache;
        Dictionary<string, byte[]> _byteCache;
        Dictionary<string, byte[]> _zippedByteCache;

        public FileCache()
        {
            _metaCache = new Dictionary<string, FileMeta>();
            _textCache = new Dictionary<string, string>();
            _zippedTextCache = new Dictionary<string, byte[]>();
            _byteCache = new Dictionary<string, byte[]>();
            _zippedByteCache = new Dictionary<string, byte[]>();
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
            _textCache.Remove(file.FullName);
            _byteCache.Remove(file.FullName);
            _zippedByteCache.Remove(file.FullName);
            _zippedTextCache.Remove(file.FullName);
            Load(file);
        }
        public virtual void Load(FileInfo file)
        {
            EnsureFileMeta(file);
            string fullName = file.FullName;
            _textCache.AddMissing(fullName, _metaCache[fullName].GetText());
            _byteCache.AddMissing(fullName, _metaCache[fullName].GetBytes());
            _zippedByteCache.AddMissing(fullName, _metaCache[fullName].GetZippedBytes());
            _zippedTextCache.AddMissing(fullName, _metaCache[fullName].GetZippedText());
        }
        object _lock = new object();
        private void EnsureFileMeta(FileInfo file)
        {
            lock (_lock)
            {
                _metaCache.AddMissing(file.FullName, new FileMeta(file));
            }
        }
    }
}
