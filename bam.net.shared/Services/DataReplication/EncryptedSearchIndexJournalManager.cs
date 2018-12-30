using Bam.Net.Encryption;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class EncryptedSearchIndexJournalManager: JournalManager
    {
        public EncryptedSearchIndexJournalManager(IKeyValueStore publicKeyValueStore, Journal journal, ISequenceProvider sequenceProvider, ITypeConverter typeConverter, ILogger logger = null)
            : this(publicKeyValueStore, journal, KeySet.ForApplication, sequenceProvider, typeConverter, logger)
        {
        }

        public EncryptedSearchIndexJournalManager(IKeyValueStore publicKeyValueStore, Journal journal, KeySet keySet, ISequenceProvider sequenceProvider, ITypeConverter typeConverter, ILogger logger = null)
            : base(sequenceProvider, new EncryptedJournalEntryValueFlusher(keySet), new EncryptedJournalEntryValueLoader(keySet), typeConverter, logger)
        {
            Journal = Journal;
            logger = logger ?? Log.Default;
            KeySet = keySet;
            EncryptedSearchIndex = new EncryptedSearchIndex(publicKeyValueStore, keySet, logger);
            Flusher = new EncryptedSearchIndexJournalEntryValueFlusher(publicKeyValueStore, keySet, logger);
            Loader = new EncryptedSearchIndexJournalEntryValueLoader(publicKeyValueStore, keySet, logger);
            Groomer = new JournalGroomer(journal, int.MaxValue, logger); // don't groom 
            journal.Flusher = Flusher;
            journal.Loader = Loader;
        }

        public KeySet KeySet { get; }

        public EncryptedSearchIndex EncryptedSearchIndex { get; set; }
    }
}
