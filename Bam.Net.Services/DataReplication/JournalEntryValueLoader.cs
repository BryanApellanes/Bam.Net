using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class JournalEntryValueLoader : IJournalEntryValueLoader
    {
        public JournalEntryValueLoader()
        {
            Values = new Dictionary<string, string>();
        }

        // TODO: use this as cache
        public Dictionary<string, string> Values { get; set; }
        public string LoadValue(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
