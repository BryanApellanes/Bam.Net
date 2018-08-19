using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class DataReplicationJournalEntryWrittenEventArgs: EventArgs
    {
        public DataReplicationJournalEntry JournalEntry { get; set; }
        public FileInfo File { get; set; }
    }
}
