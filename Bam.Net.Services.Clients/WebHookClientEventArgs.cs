using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Clients
{
    public class WebHookClientEventArgs: EventArgs
    {
        public string WebHookName { get; set; }
        public string BoryOrBlank { get; set; }
    }
}
