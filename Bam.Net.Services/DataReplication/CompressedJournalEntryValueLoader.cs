using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class CompressedJournalEntryValueLoader : IJournalEntryValueLoader
    {
        public CompressedJournalEntryValueLoader()
        {
            Encoding = Encoding.UTF8;
        }

        public Encoding Encoding { get; set; }

        public string LoadValue(string filePath)
        {
            if (File.Exists(filePath))
            {
                string base64 = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(base64))
                {
                    return Encoding.GetString(base64.FromBase64().GUnzip());
                }
            }
            return string.Empty;
        }
    }
}
