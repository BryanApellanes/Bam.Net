using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation.Markdown
{
    public abstract class DocumentComponent : IDocumentComponent
    {
        public DocumentComponent()
        {
            HeaderLevel = 1;
            RenderTitle = true;
        }

        /// <summary>
        /// If true, renders Title
        /// </summary>
        public bool RenderTitle { get; set; }

        int _headerLevel;
        public int HeaderLevel
        {
            get
            {
                return _headerLevel;
            }
            set
            {
                _headerLevel = value;
                OnUpdated(new DocumentComponentEventArgs { DocumentComponent = this });
            }
        }

        string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                Updated?.Invoke(this, new DocumentComponentEventArgs { DocumentComponent = this });
            }
        }

        public virtual string Render()
        {
            StringBuilder output = new StringBuilder();
            if (RenderTitle)
            {
                output.AppendLine(GetTitle());
            }
            output.AppendLine(GetContent());
            return output.ToString();
        }

        public virtual string GetTitle()
        {
            return $"{new string('#', HeaderLevel)}{Title}";
        }

        public event EventHandler<DocumentComponentEventArgs> Updated;

        public abstract string GetContent();

        protected void OnUpdated(DocumentComponentEventArgs args)
        {
            Updated?.Invoke(this, args);
        }
    }
}
