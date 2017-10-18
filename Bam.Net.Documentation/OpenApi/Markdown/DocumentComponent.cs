using Bam.Net.Presentation.Markdown;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation.OpenApi.Markdown
{
    public abstract class DocumentComponent: MarkdownDocument
    {
        public virtual string Render()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine(GetTitle());
            output.AppendLine(GetContent());
            return output.ToString();
        }
    }
}
