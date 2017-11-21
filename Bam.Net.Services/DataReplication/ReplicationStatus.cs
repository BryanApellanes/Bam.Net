using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public enum ReplicationStatus
    {
        Invalid,
        Pending,
        Warning,
        Failed,
        Success
    }
}
