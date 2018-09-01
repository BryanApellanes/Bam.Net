using Bam.Net.Encryption;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class EncryptedJournalEntryValueLoader : JournalEntryValueLoader
    {
        public EncryptedJournalEntryValueLoader() : this(KeySet.ForApplication)
        { }

        public EncryptedJournalEntryValueLoader(KeySet keySet)
        {
            KeySet = keySet;        
        }

        public KeySet KeySet { get; set; }

        public override string LoadValue(string filePath)
        {
            string cipher = base.LoadValue(filePath);
            return KeySet.Decrypt(cipher);
        }
    }
}
