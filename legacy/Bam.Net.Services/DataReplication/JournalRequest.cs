using Bam.Net.Server.Streaming;
using Bam.Net.Services.DataReplication.Data;
using Bam.Net.UserAccounts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class JournalRequest<T>: SecureStreamingRequest<T>
    {                
    }
}
