using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Javascript;

namespace Bam.Net.Caching.File
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bam.Net.Caching.File.TextFileCache" />
    public class JsFileCache: TextFileCache
    {
        Dictionary<string, MinifyResult> _minCache;
        Dictionary<string, string> _minText;
        Dictionary<string, byte[]> _minTextBytes;
        /// <summary>
        /// Initializes a new instance of the <see cref="JsFileCache"/> class.
        /// </summary>
        public JsFileCache(): base()
        {
            _minCache = new Dictionary<string, MinifyResult>();
            _minText = new Dictionary<string, string>();
            _minTextBytes = new Dictionary<string, byte[]>();
            FileExtension = ".js";
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="JsFileCache"/> is minified.
        /// </summary>
        /// <value>
        ///   <c>true</c> if minified; otherwise, <c>false</c>.
        /// </value>
        public bool Minify { get; set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override byte[] GetContent(string path)
        {
            if (Minify)
            {
                return Encoding.UTF8.GetBytes(GetMinText(new FileInfo(path)));
            }
            return Encoding.UTF8.GetBytes(GetText(new FileInfo(path)));
        }

        /// <summary>
        /// Gets the content of the zipped.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override byte[] GetZippedContent(string path)
        {
            if (Minify)
            {
                return GetZippedMinText(new FileInfo(path));
            }
            return GetZippedText(new FileInfo(path));
        }

        /// <summary>
        /// Gets the minified text.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public string GetMinText(FileInfo file)
        {
            if (!_minText.ContainsKey(file.FullName))
            {
                Load(file);
            }
            return _minText[file.FullName];
        }

        /// <summary>
        /// Gets the zipped minimum text.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public byte[] GetZippedMinText(FileInfo file)
        {
            if (!_minTextBytes.ContainsKey(file.FullName))
            {
                SetCacheContent(file);              
            }
            return _minTextBytes[file.FullName];
        }

        /// <summary>
        /// Sets the content of the cache.
        /// </summary>
        /// <param name="file">The file.</param>
        protected void SetCacheContent(FileInfo file)
        {
            if (HashChanged(file))
            {
                Reload(file);
            }
            else
            {
                Load(file);
            }
        }

        /// <summary>
        /// Reloads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public override void Reload(FileInfo file)
        {
            _minCache.Remove(file.FullName);
            _minText.Remove(file.FullName);
            _minTextBytes.Remove(file.FullName);
            base.Reload(file);
        }

        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public override void Load(FileInfo file)
        {
            base.Load(file);
            _minCache.AddMissing(file.FullName, new MinifyResult(GetText(file)));
            _minText.AddMissing(file.FullName, _minCache[file.FullName].MinScript);
            _minTextBytes.AddMissing(file.FullName, _minText[file.FullName].GZip());
        }
    }
}
