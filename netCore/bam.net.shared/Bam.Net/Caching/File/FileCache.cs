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

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
        public string FileExtension { get; protected set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public abstract byte[] GetContent(string filePath);

        /// <summary>
        /// Gets the content of the zipped file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public abstract byte[] GetZippedContent(string filePath);

        /// <summary>
        /// The result of zipping ReadAllBytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual byte[] GetZippedBytes(FileInfo file)
        {
            return _cachedFiles[file.FullName].GetZippedBytes();
        }

        /// <summary>
        /// The result of ReadAllBytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual byte[] GetBytes(FileInfo file)
        {
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
            return _cachedFiles[file.FullName].GetZippedText();
        }

        /// <summary>
        /// Ensures the file is loaded.
        /// </summary>
        /// <param name="file">The file.</param>
        public void EnsureFileIsLoaded(FileInfo file)
        {
            if (!_cachedFiles.ContainsKey(file.FullName) || HashChanged(file))
            {
                Load(file);
            }
        }

        /// <summary>
        /// Determines if the hash of the specified file has changed.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        protected bool HashChanged(FileInfo file)
        {
            if (_hashes.TryGetValue(file.FullName, out string hash) && 
                _cachedFiles.TryGetValue(file.FullName, out CachedFile cachedFile))
            {
                return !string.IsNullOrEmpty(hash) && cachedFile.ContentHash.Equals(hash);
            }
            return false;
        }

        /// <summary>
        /// Removes the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void Remove(FileInfo file)
        {
            _cachedFiles.TryRemove(file.FullName, out CachedFile value);
        }

        /// <summary>
        /// Reloads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public virtual void Reload(FileInfo file)
        {
            _cachedFiles.TryRemove(file.FullName, out CachedFile value);
            Load(file);
        }

        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
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
