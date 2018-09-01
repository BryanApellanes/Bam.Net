using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class CompressedJournalEntryValueLoader : JournalEntryValueLoader
    {
        public CompressedJournalEntryValueLoader()
        {
            Encoding = Encoding.UTF8;
        }

        public Encoding Encoding { get; set; }

        public override string LoadValue(string filePath)
        {
            return Encoding.GetString(base.LoadValue(filePath).FromBase64().GUnzip());
        }
    }
}
