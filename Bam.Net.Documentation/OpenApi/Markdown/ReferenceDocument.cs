using Bam.Net.Documentation.OpenApi.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation.OpenApi.Markdown
{
    public class ReferenceDocument : DocumentComponent
    {
        public string Description { get; set; }
        public List<DocumentSection> Sections { get; set; }

        public override string GetContent()
        {
            StringBuilder content = new StringBuilder();
            content.AppendLine(Description);
            Sections.Each(sec => content.AppendLine(sec.Render()));
            return content.ToString();
        }
    }
}
