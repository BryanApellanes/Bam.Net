using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Encryption;

namespace Bam.Net.CoreServices.Services
{
    public class TranslationInfo
    {
        public VaultInfo VaultInfo { get; set; }
        public Database LanguageDatabase { get; set; }
        public Database TranslationDatabase { get; set; }
    }
}
