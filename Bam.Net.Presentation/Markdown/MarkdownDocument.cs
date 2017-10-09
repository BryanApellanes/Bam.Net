using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation.Markdown
{
    public abstract class MarkdownDocument : IDocumentComponent
    {
        public string Title { get; set; }
        public abstract string GetContent();

        public virtual string GetTitle()
        {
            return Title;
        }
    }
}
