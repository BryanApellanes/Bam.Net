using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class RequestRoute
    {
        public string PathName { get; set; }

        public string Protocol { get; set; }
        public string Domain { get; set; }
        public string PathAndQuery { get; set; }
        protected internal Uri OriginalUrl { get; set; }
        protected internal Dictionary<string, string> ParsedValues { get; set; }

    }
}
