using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching.File
{
    public class FileMeta
    {
        public static implicit operator FileMeta(FileInfo file)
        {
            return new FileMeta(file);
        }
        public static implicit operator FileInfo(FileMeta meta)
        {
            return meta.File;
        }
        public FileMeta(FileInfo file)
        {
            FullName = file.FullName;
            File = file;
            if (File.Exists)
            {
                Task.Run(() => ContentHash = File.ContentHash(HashAlgorithms.MD5));                
            }
        }
        public string ContentHash { get; private set; }
        public string FullName { get; private set; }
        internal FileInfo File { get; set; }
        byte[] _gzippedBytes;
        object _gzippedByteLock = new object();
        public byte[] GetZippedBytes()
        {
            byte[] gzipped = _gzippedByteLock.DoubleCheckLock(ref _gzippedBytes, () => GetBytes().GZip());
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
        byte[] _gzippedText;
        object _gzippedTextLock = new object();
        public byte[] GetZippedText()
        {
            byte[] zippedText = _gzippedTextLock.DoubleCheckLock(ref _gzippedText, () => GetText().GZip());
            return zippedText; 
        }
        public override bool Equals(object obj)
        {
            FileMeta meta = obj as FileMeta;
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

        //protected void PrimeAsync()
        //{
        //    Task.Run(() =>
        //    {
        //        ContentHash = File.ContentHash(HashAlgorithms.MD5);
        //        byte[] tempBytes = System.IO.File.ReadAllBytes(FullName);
        //        byte[] tempZipped = tempBytes.GZip();
        //        string tempText = System.IO.File.ReadAllText(FullName);
        //        byte[] tempZippedText = tempText.GZip();

        //        _bytes = tempBytes;
        //        _text = tempText;
        //        _gzippedBytes = tempZipped;
        //        _gzippedText = tempZippedText;
        //    });
        //}
    }
}
