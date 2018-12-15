using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class EncryptedJournal: Journal
    {
        public EncryptedJournal(SystemPaths paths, JournalTypeMap typeMap, ISequenceProvider sequenceProvider, ITypeConverter typeConverter = null, ILogger logger = null) : base(paths, typeMap, sequenceProvider, new EncryptedJournalEntryValueFlusher(), new EncryptedJournalEntryValueLoader(), typeConverter, logger)
        { }
    }
}
