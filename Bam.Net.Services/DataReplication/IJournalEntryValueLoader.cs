using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public interface IJournalEntryValueLoader
    {
        string LoadValue(string filePath);
    }
}
