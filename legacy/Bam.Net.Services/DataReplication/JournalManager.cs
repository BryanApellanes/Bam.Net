using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class JournalManager: Loggable, IJournalManager
    {
        public JournalManager(ISequenceProvider sequenceProvider, IJournalEntryValueFlusher flusher, IJournalEntryValueLoader loader, ITypeConverter typeConverter, ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            Journal = new Journal(sequenceProvider, flusher, loader, typeConverter, logger);
            Flusher = Journal.Flusher;
            Loader = Journal.Loader;
            Groomer = new JournalGroomer(Journal, 1000 * 60 * 3 /* three minutes */, logger);
            Journal.Subscribe(logger);
        }

        public bool CompressValues { get; set; }

        public Journal Journal { get; protected set; }
        public JournalGroomer Groomer { get; set; }

        public void Enqueue(KeyHashAuditRepoData data, Action<JournalEntry[]> onFlushed = null)
        {
            onFlushed = onFlushed ?? ((j) => { });
            Journal.Enqueue(data, onFlushed);
        }

        public void Enqueue(IEnumerable<JournalEntry> journalEntries, Action<JournalEntry[]> onFlushed = null)
        {
            onFlushed = onFlushed ?? ((j) => { });
            Journal.Enqueue(journalEntries.ToArray(), onFlushed);
        }

        public T Load<T>(ulong id) where T : KeyHashAuditRepoData, new()
        {
            return Journal.LoadInstance<T>(id);
        }

        IJournalEntryValueFlusher _flusher;
        public IJournalEntryValueFlusher Flusher
        {
            get
            {
                if(_flusher == null)
                {
                    if (CompressValues)
                    {
                        _flusher = new CompressedJournalEntryValueFlusher();
                    }
                    else
                    {
                        _flusher = new JournalEntryValueFlusher();
                    }
                }
                return _flusher;
            }
            set
            {
                _flusher = value;
                Journal.Flusher = _flusher;
            }
        }

        IJournalEntryValueLoader _loader;
        public IJournalEntryValueLoader Loader
        {
            get
            {
                if(_loader == null)
                {
                    if (CompressValues)
                    {
                        _loader = new CompressedJournalEntryValueLoader();
                    }
                    _loader = new JournalEntryValueLoader();
                }
                return _loader;
            }
            set
            {
                _loader = value;
                Journal.Loader = _loader;
            }
        }
    }
}
