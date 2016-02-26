using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Encryption;
using System.IO;

namespace Bam.Net.Messaging
{
    public static class SmtpSettingsVault
    {
        public static Vault Load(string filePath)
        {
            return Load(new FileInfo(filePath));
        }
        public static Vault Load(FileInfo file)
        {
            return Vault.Load(file, "SmtpSettings");
        }
    }
}
