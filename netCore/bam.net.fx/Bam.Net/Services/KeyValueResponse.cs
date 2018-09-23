using Bam.Net.Server.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    [Serializable]
    public class KeyValueResponse
    {
        public bool Success { get; set; }
        public string Value { get; set; }
        public string Message { get; set; }
    }
}
