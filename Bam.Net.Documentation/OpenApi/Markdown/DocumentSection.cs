using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation.OpenApi.Markdown
{
    public class DocumentSection: DocumentComponent
    {
        public DocumentSection()
        {
            TitleFormat = "{Name} Object";
        }
        public string TitleFormat { get; set; }

        string _title;
        public virtual string Title
        {
            get
            {
                if (string.IsNullOrEmpty(_title))
                {
                    _title = TitleFormat.NamedFormat(this);
                }
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        public string Content { get; set; }
        public override string GetContent()
        {
            return Content;
        }
    }
}
