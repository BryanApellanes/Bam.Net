using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class CompressedJournalEntryValueFlusher : IJournalEntryValueFlusher
    {
        public CompressedJournalEntryValueFlusher()
        {
            Encoding = Encoding.UTF8;
        }

        public Encoding Encoding { get; set; }

        public FileInfo Flush(Journal journal, JournalEntry entry)
        {
            FileInfo propertyFile = journal.GetJournalEntryFileInfo(entry);
            if (!string.IsNullOrEmpty(entry.Value))
            {                
                Encoding.GetBytes(entry.Value).GZip().ToBase64().SafeWriteToFile(propertyFile.FullName, true);
            }
            return propertyFile;
        }
    }
}
