using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Encryption;

namespace Bam.Net.CoreServices.Services
{
    public class YandexApiKeyVaultInfo: VaultInfo
    {
        public YandexApiKeyVaultInfo(string vaultPath):base(new FileInfo(vaultPath))
        {
        }
    }
}
