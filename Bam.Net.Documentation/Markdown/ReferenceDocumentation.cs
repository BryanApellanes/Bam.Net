using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Presentation.Markdown;

namespace Bam.Net.Documentation.Markdown
{
    public class ReferenceDocumentation : DocumentComponent
    {
        public ReferenceDocumentation()
        {
           
        }

        public string Description { get; set; }
        public List<DocumentComponent> Sections { get; set; }

        public override string GetContent()
        {
            StringBuilder content = new StringBuilder();
            content.AppendLine(Description);
            Sections.Each(sec => content.AppendLine(sec.Render()));
            return content.ToString();
        }

        string _content;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                OnUpdated(new DocumentComponentEventArgs { DocumentComponent = this });
            }
        }
    }
}
