using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public interface IJournalEntryValueFlusher
    {
        FileInfo Flush(Journal journal, JournalEntry entry);
    }
}
