using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class CompressionJournalEntryValueLoader : IJournalEntryValueLoader
    {
        public string LoadValue(string filePath)
        {
            byte[] value = File.ReadAllBytes(filePath);
            return value.GUnzip().ToBase64();
        }
    }
}
