/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using System.IO;

namespace Bam.Net.Server
{
    /// <summary>
    /// Hash of file content
    /// </summary>
    public class FileContentHash
    {
        public FileContentHash() { }
        public FileContentHash(string filePath)
        {
            this.FilePath = filePath;
            this.Sha1 = new FileInfo(filePath).Sha1();
        }

        public string FilePath { get; set; }
        public string Sha1 { get; set; }

        public override int GetHashCode()
        {
            return Sha1.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            FileContentHash hash = obj as FileContentHash;
            if (hash != null)
            {
                return hash.FilePath.Equals(this.FilePath) && hash.Sha1.Equals(this.Sha1);
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }
}
