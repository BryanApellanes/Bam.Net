using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class CompressionJournalEntryValueFlusher : IJournalEntryValueFlusher
    {
        public CompressionJournalEntryValueFlusher()
        {
            Encoding = Encoding.UTF8;
        }

        public Encoding Encoding { get; set; }

        public void Cleanup()
        {
            this.ClearFileAccessLocks();
        }

        public FileInfo Flush(Journal journal, JournalEntry entry)
        {
            FileInfo propertyFile = journal.GetJournalEntryFileInfo(entry);
            Encoding.GetBytes(entry.Value).GZip().ToBase64().SafeWriteToFile(propertyFile.FullName);
            return propertyFile;
        }
    }
}
