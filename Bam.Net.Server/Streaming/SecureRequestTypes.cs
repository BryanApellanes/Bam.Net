using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Streaming
{
    public enum SecureRequestTypes
    {
        Invalid,
        StartSession,
        SetKey,
        Message
    }
}
