using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public interface IJournalManager
    {
        Journal Journal { get; }
        IJournalEntryValueFlusher Flusher { get; }
        IJournalEntryValueLoader Loader { get; }

        void Enqueue(IEnumerable<JournalEntry> journalEntries, Action<JournalEntry[]> onFullyFlushed = null);

        T Load<T>(ulong id) where T : KeyHashAuditRepoData, new();
    }
}
