using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation.Markdown
{
    public class DocumentComponentEventArgs: EventArgs
    {
        public IDocumentComponent DocumentComponent { get; set; }
    }
}
