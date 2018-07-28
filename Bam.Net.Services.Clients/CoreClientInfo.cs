using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Clients
{
    public class CoreClientInfo
    {
        public CoreClientInfo()
        {
            CoreHostName = "core.bamapps.net";
            CorePort = 80;
            ContentRoot = "C:\\bam\\content";
        }
        public string CoreHostName { get; set; }
        public int CorePort { get; set; }
        public string ContentRoot { get; set; }
    }
}
