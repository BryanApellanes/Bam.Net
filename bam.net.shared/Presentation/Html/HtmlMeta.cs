using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Presentation.Html
{
    public class HtmlMeta
    {
        public HtmlMeta()
        {
            ViewPort = "width=device-width, initial-scale=1.0";
            Generator = "bamapps";
        }
        public string ViewPort { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Keyword { get; set; }
        public string Generator { get; set; }
    }
}
