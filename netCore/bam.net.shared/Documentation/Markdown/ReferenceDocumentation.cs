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
            TitleFormat = "{Name} Reference";
            _sections = new List<DocumentComponent>();
            DescriptionProvider = new DocInfoInferredDescriptionProvider();
        }
        public IDescriptionProvider DescriptionProvider { get; set; }
        string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnUpdated(new DocumentComponentEventArgs { DocumentComponent = this });
            }
        }

        List<DocumentComponent> _sections;
        public List<DocumentComponent> Sections
        {
            get
            {
                return _sections.Select(dc => dc).ToList();
            }
        }

        public void AddSection(DocumentComponent section)
        {
            _sections.Add(section);
        }

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

        string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnUpdated(new DocumentComponentEventArgs { DocumentComponent = this });
            }
        }

        string _titleFormat;
        public string TitleFormat
        {
            get
            {
                return _titleFormat;
            }
            set
            {
                _titleFormat = value;
                OnUpdated(new DocumentComponentEventArgs { DocumentComponent = this });
            }
        }

        public override string GetTitle()
        {
            return $"{new string('#', HeaderLevel)}{TitleFormat.NamedFormat(this)}";
        }
    }
}
