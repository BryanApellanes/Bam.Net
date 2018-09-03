using Bam.Net.Server.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class KeyValueResponse: SecureStreamingResponse
    {
        public string Message { get; set; }
        public string Value { get; set; }
    }
}
