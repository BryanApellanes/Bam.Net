using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Presentation.Markdown;

namespace Bam.Net.Documentation.Markdown
{
    public class TypeReferenceDocumentation: ReferenceDocumentation
    {
        public TypeReferenceDocumentation():base()
        {
            TitleFormat = "{Name} Object";
        }

        public TypeReferenceDocumentation(string name, string description):this()
        {
            Name = name;
            Description = description;
        }

        public TypeReferenceDocumentation(Type type) : this()
        {
            Type = type;
            Name = type.Name;
        }

        Type _type;
        public Type Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnUpdated(new DocumentComponentEventArgs { DocumentComponent = this });
            }
        }
    }
}
