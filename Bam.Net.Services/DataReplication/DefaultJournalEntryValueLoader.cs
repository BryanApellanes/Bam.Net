using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class DefaultJournalEntryValueLoader : IJournalEntryValueLoader
    {
        public string LoadValue(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
