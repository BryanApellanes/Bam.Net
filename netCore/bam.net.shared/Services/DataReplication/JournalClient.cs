using Bam.Net.Server.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class JournalClient: SecureStreamingClient<JournalRequest<JournalEntry[]>, JournalResponse>
    {
        public JournalClient(string hostName, int port) : base(hostName, port)
        { }
    }
}
