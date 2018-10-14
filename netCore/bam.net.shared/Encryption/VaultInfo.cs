/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Configuration;

namespace Bam.Net.Encryption
{
    /// <summary>
    /// A descriptor for a vault instance providing name and file path.
    /// </summary>
    public class VaultInfo
    {
        public VaultInfo(string name)
        {
            Name = name;
            FilePath = new FileInfo(Path.Combine(RuntimeSettings.AppDataFolder, $"{name}.vault.sqlite")).FullName;
        }

        public VaultInfo(FileInfo siblingFile)
        {
            Name = Path.GetFileNameWithoutExtension(siblingFile.Name);
            FilePath = Path.Combine(siblingFile.Directory.FullName, string.Format("{0}.vault.sqlite", this.Name));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VaultInfo"/> class using the default
        /// application name form the config file or "UNKOWN".
        /// </summary>
        public VaultInfo() : this(DefaultConfigurationApplicationNameProvider.Instance.GetApplicationName())
        {
        }

        public string FilePath { get; set; }
        public string Name { get; set; }

        public Vault Load()
        {
            return Vault.Load(new FileInfo(FilePath), Name);
        }
    }
}
