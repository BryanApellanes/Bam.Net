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
        static ConcurrentDictionary<string, string> _hashes;

        public FileCache()
        {
            _lock.DoubleCheckLock(ref _cachedFiles, () => new ConcurrentDictionary<string, CachedFile>());
            _lock.DoubleCheckLock(ref _hashes, () => new ConcurrentDictionary<string, string>());
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
            EnsureFileIsLoaded(file);
            return _cachedFiles[file.FullName].GetZippedBytes();
        }

        /// <summary>
        /// The result of ReadAllBytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual byte[] GetBytes(FileInfo file)
        {
            EnsureFileIsLoaded(file);
            return _cachedFiles[file.FullName].GetBytes();
        }

        /// <summary>
        /// The result of ReadAllText
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual string GetText(FileInfo file)
        {
            if (_cachedFiles.ContainsKey(file.FullName))
            {
                return _cachedFiles[file.FullName].GetText();
            }
            return System.IO.File.ReadAllText(file.FullName);
        }
        
        /// <summary>
        /// The result of zipping ReadAllText
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual byte[] GetZippedText(FileInfo file)
        {
            EnsureFileIsLoaded(file);
            return _cachedFiles[file.FullName].GetZippedText();
        }

        protected void EnsureFileIsLoaded(FileInfo file)
        {
            if (!_cachedFiles.ContainsKey(file.FullName) || HashChanged(file))
            {
                Load(file);
            }
        }

        protected bool HashChanged(FileInfo file)
        {
            if (_hashes.TryGetValue(file.FullName, out string hash) && 
                _cachedFiles.TryGetValue(file.FullName, out CachedFile cachedFile))
            {
                return !string.IsNullOrEmpty(hash) && cachedFile.ContentHash.Equals(hash);
            }
            return false;
        }

        public void Remove(FileInfo file)
        {
            _cachedFiles.TryRemove(file.FullName, out CachedFile value);
        }

        public virtual void Reload(FileInfo file)
        {
            _cachedFiles.TryRemove(file.FullName, out CachedFile value);
            Load(file);
        }

        public virtual void Load(FileInfo file)
        {
            string fullName = file.FullName;

            CachedFile cachedFile = new CachedFile(file);
            if (_cachedFiles.TryAdd(fullName, cachedFile))
            {
                _hashes[fullName] = cachedFile.ContentHash;
            }
        }      
    }
}
