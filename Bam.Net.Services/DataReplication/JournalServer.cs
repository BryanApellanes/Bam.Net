using Bam.Net.Server.Streaming;
using Bam.Net.ServiceProxy.Secure;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class JournalServer : SecureStreamingServer<JournalRequest<JournalEntry[]>, JournalResponse>
    {
        public JournalServer(JournalManager journalManager)
        {
            JournalManager = journalManager;
        }

        public IJournalManager JournalManager { get; set; }

        public override JournalResponse ProcessRequest(StreamingContext<JournalRequest<JournalEntry[]>> context)
        {
            throw new NotImplementedException();
        }

        public override JournalResponse ProcessDecryptedRequest(JournalRequest<JournalEntry[]> request)
        {
            JournalManager.Enqueue(request.Body);
            return new JournalResponse { Success = true };
        }
    }
}
