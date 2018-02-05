using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class CustomContentEventArgs: EventArgs
    {
        public string Uri { get; set; }
        public string Name
        {
            get
            {
                return CustomContentHandler.Name;
            }
        }
        public CustomContentHandler CustomContentHandler { get; set; }
    }
}
