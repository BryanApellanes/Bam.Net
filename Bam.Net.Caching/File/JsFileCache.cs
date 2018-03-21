using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Javascript;

namespace Bam.Net.Caching.File
{
    public class JsFileCache: TextFileCache
    {
        Dictionary<string, MinifyResult> _minCache;
        Dictionary<string, string> _minText;
        Dictionary<string, byte[]> _minTextBytes;
        public JsFileCache(): base()
        {
            _minCache = new Dictionary<string, MinifyResult>();
            _minText = new Dictionary<string, string>();
            _minTextBytes = new Dictionary<string, byte[]>();
            FileExtension = ".js";
        }
        public bool Minify { get; set; }
        public override byte[] GetContent(string path)
        {
            if (Minify)
            {
                return Encoding.UTF8.GetBytes(GetMinText(new FileInfo(path)));
            }
            return Encoding.UTF8.GetBytes(GetText(new FileInfo(path)));
        }

        public override byte[] GetZippedContent(string path)
        {
            if (Minify)
            {
                return GetZippedMinText(new FileInfo(path));
            }
            return GetZippedText(new FileInfo(path));
        }

        public string GetMinText(FileInfo file)
        {
            if (!_minText.ContainsKey(file.FullName))
            {
                Load(file);
            }
            return _minText[file.FullName];
        }

        public byte[] GetZippedMinText(FileInfo file)
        {
            if (!_minTextBytes.ContainsKey(file.FullName))
            {
                SetCacheContent(file);              
            }
            return _minTextBytes[file.FullName];
        }

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

        public override void Reload(FileInfo file)
        {
            _minCache.Remove(file.FullName);
            _minText.Remove(file.FullName);
            _minTextBytes.Remove(file.FullName);
            base.Reload(file);
        }

        public override void Load(FileInfo file)
        {
            base.Load(file);
            _minCache.AddMissing(file.FullName, new MinifyResult(GetText(file)));
            _minText.AddMissing(file.FullName, _minCache[file.FullName].MinScript);
            _minTextBytes.AddMissing(file.FullName, _minText[file.FullName].GZip());
        }
    }
}
