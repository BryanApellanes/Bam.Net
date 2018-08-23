using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class CompressionJournalEntryValueManager : IJournalEntryValueManager
    {
        public CompressionJournalEntryValueManager()
        {
            Flusher = new CompressionJournalEntryValueFlusher();
            Loader = new CompressionJournalEntryValueLoader();
        }

        public IJournalEntryValueFlusher Flusher
        {
            get;
        }

        public IJournalEntryValueLoader Loader
        {
            get;
        }
    }
}
