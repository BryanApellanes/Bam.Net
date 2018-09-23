using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class JournalEntriesEnqueuedEventArgs: EventArgs
    {
        public JournalEntry[] JournalEntries { get; set; }
    }
}
