using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Encryption;

namespace Bam.Net.Services
{
    public class ApiKeyVaultInfo: VaultInfo
    {
        public ApiKeyVaultInfo(string vaultPath):base(new FileInfo(vaultPath))
        {
        }
    }
}
