using Bam.Net.Encryption;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    /// <summary>
    /// A journal entry value flusher that encrypts values prior to 
    /// flushing to disk.
    /// </summary>
    /// <seealso cref="Bam.Net.Services.DataReplication.IJournalEntryValueFlusher" />
    public class EncryptedJournalEntryValueFlusher : IJournalEntryValueFlusher
    {
        public EncryptedJournalEntryValueFlusher() : this(KeySet.ForApplication)
        { }

        public EncryptedJournalEntryValueFlusher(KeySet keySet)
        {
            KeySet = keySet;
        }

        public KeySet KeySet { get; set; }

        public FileInfo Flush(Journal journal, JournalEntry entry)
        {
            FileInfo propertyFile = journal.GetJournalEntryFileInfo(entry);
            if (!string.IsNullOrEmpty(entry.Value))
            {
                KeySet.Encrypt(entry.Value).SafeWriteToFile(propertyFile.FullName, true);
            }
            return propertyFile;
        }
    }
}
