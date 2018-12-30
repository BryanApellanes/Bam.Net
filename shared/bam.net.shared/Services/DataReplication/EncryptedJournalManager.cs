using Bam.Net.Encryption;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class EncryptedJournalManager : JournalManager
    {
        public EncryptedJournalManager(Journal journal, ISequenceProvider sequenceProvider, ITypeConverter typeConverter, ILogger logger = null) 
            : this(journal, KeySet.ForApplication, sequenceProvider, typeConverter, logger)
        {
        }

        public EncryptedJournalManager(Journal journal, KeySet keySet, ISequenceProvider sequenceProvider, ITypeConverter typeConverter, ILogger logger = null)
            : base(sequenceProvider, new EncryptedJournalEntryValueFlusher(keySet), new EncryptedJournalEntryValueLoader(keySet), typeConverter, logger)
        {
            Journal = Journal;
            logger = logger ?? Log.Default;
            KeySet = keySet;
            Groomer = new JournalGroomer(journal, 1000 * 60 * 3 /* three minutes */, logger);
            journal.Flusher = Flusher;
            journal.Loader = Loader;
        }
        
        public KeySet KeySet { get; }
    }
}
