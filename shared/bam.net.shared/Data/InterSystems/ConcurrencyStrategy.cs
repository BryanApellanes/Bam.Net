using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.InterSystems
{
    public enum ConcurrencyStrategy
    {
        Invalid = 0,
        None = 1,
        Check = 2, // check if anyone has an exclusive lock
        Read = 3,
        Write = 4 // 
    }
}
