using Bam.Net.Caching.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class JournalEntryValueFlusher : IJournalEntryValueFlusher
    {
        public JournalEntryValueFlusher()
        {        
        }
        
        public virtual FileInfo Flush(Journal journal, JournalEntry journalEntry)
        {
            FileInfo propertyFile = journal.GetJournalEntryFileInfo(journalEntry);
            journalEntry.Value.SafeWriteToFile(propertyFile.FullName, true);

            return propertyFile;
        }
    }
}
