using Bam.Net.Encryption;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class EncryptedJournalEntryValueLoader : IJournalEntryValueLoader
    {
        public EncryptedJournalEntryValueLoader() : this(KeySet.ForApplication)
        { }

        public EncryptedJournalEntryValueLoader(KeySet keySet)
        {
            KeySet = keySet;        
        }

        public KeySet KeySet { get; set; }

        // TODO: add dictionary cache (Dictionary<filePath:string, value:string>)
        public string LoadValue(string filePath)
        {
            if (File.Exists(filePath))
            {
                string cipher = File.ReadAllText(filePath);
                return KeySet.Decrypt(cipher);
            }
            return string.Empty;
        }
    }
}
