using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Presentation.Markdown;

namespace Bam.Net.Documentation.Markdown
{
    public class TypeDocumentation: ReferenceDocumentation
    {
        public TypeDocumentation(Type type)
        {
            TitleFormat = "{Name} Object";
            Type = type;
        }

        public Type Type { get; set; }

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
