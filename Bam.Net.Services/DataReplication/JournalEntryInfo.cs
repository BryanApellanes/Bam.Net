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
    public class JournalEntryInfo
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

        public JournalEntry Load(Journal journal)
        {
            return Load(journal.JournalDirectory, journal.TypeMap);
        }

        public JournalEntry Load(DirectoryInfo journalDirectory, JournalTypeMap typeMap)
        {
            JournalEntry entry = this.CopyAs<JournalEntry>();
            return entry.LoadLatestValue(journalDirectory, typeMap);            
        }
    }
}
