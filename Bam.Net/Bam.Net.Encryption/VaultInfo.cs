/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Encryption
{
    public class VaultInfo
    {
        public VaultInfo() { }

        public VaultInfo(string name)
        {
            this.Name = name;
            this.FilePath = new FileInfo(".\\{0}.vault.sqlite"._Format(name)).FullName;
        }

        public VaultInfo(FileInfo file)
        {
            this.Name = Path.GetFileNameWithoutExtension(file.Name);
            this.FilePath = Path.Combine(file.Directory.FullName, string.Format("{0}.vault.sqlite", this.Name));
        }

        public string FilePath { get; set; }
        public string Name { get; set; }

        public Vault Load()
        {
            return Vault.Load(new FileInfo(FilePath), Name);
        }
    }
}
