using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public interface IJournalEntryFlusher
    {
        FileInfo Flush(DataReplicationJournal journal, DataReplicationJournalEntry entry);

        void Cleanup();
    }
}
