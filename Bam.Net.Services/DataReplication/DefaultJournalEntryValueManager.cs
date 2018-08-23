using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class DefaultJournalEntryValueManager : IJournalEntryValueManager
    {
        public DefaultJournalEntryValueManager()
        {
            Flusher = new DefaultJournalEntryValueFlusher();
            Loader = new DefaultJournalEntryValueLoader();
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
