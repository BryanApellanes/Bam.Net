using Bam.Net.Server.Streaming;
using Bam.Net.ServiceProxy.Secure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class DataReplicationServer : StreamingServer<DataReplicationRequest, DataReplicationResponse>
    {
        public DataReplicationServer(DataReplicationJournal journal)
        {
            Journal = journal;
        }

        public DataReplicationJournal Journal { get; set; }

        public override DataReplicationResponse ProcessRequest(StreamingContext<DataReplicationRequest> context)
        {
            try
            {
                DataReplicationRequest request = context.Request.Message;
                SecureSession session = SecureSession.Get(request.SessionId);
                string base64 = session.DecryptWithPrivateKey(request.Data);
                byte[] data = base64.FromBase64();
                DataReplicationJournalEntry journalEntry = data.FromBinaryBytes<DataReplicationJournalEntry>();
                Journal.WriteEntry(journalEntry);
                return new DataReplicationResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new DataReplicationResponse { Success = false, Message = ex.Message };
            }
        }
    }
}
