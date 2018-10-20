using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class ContentEventArgs: EventArgs
    {
        public string Uri { get; set; }
        public string Name
        {
            get
            {
                return ContentHandler.Name;
            }
        }
        public ContentHandler ContentHandler { get; set; }
    }
}
