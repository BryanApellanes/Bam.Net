using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    /// <summary>
    /// Represents information about a journal entry with the value.
    /// </summary>
    public class DataReplicationJournalEntryInfo
    {    
        /// <summary>
        /// Gets or sets the type id determined by DataReplicationtypeMap.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public long TypeId { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public ulong InstanceId { get; set; }

        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        /// <value>
        /// The sequence number.
        /// </value>
        public ulong Seq { get; set; }

        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>
        /// The property identifier.
        /// </value>
        public long PropertyId { get; set; }

        public DataReplicationJournalEntry Load(DataReplicationJournal journal)
        {
            return Load(journal.JournalDirectory, journal.TypeMap);
        }

        public DataReplicationJournalEntry Load(DirectoryInfo journalDirectory, DataReplicationTypeMap typeMap)
        {
            DataReplicationJournalEntry entry = this.CopyAs<DataReplicationJournalEntry>();
            return entry.LoadLatestValue(journalDirectory, typeMap);            
        }
    }
}
