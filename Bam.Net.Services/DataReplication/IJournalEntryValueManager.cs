using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public interface IJournalEntryValueManager
    {
        IJournalEntryValueFlusher Flusher { get; }
        IJournalEntryValueLoader Loader { get; }
    }
}
