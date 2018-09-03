using Bam.Net.Server.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    [Serializable]
    public class KeyValueResponse: SecureStreamingResponse
    {
        public string Value { get; set; }
    }
}
