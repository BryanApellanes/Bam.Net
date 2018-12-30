using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class JournalEntryWrittenEventArgs: EventArgs
    {
        public JournalEntry JournalEntry { get; set; }
        public FileInfo File { get; set; }
    }
}
