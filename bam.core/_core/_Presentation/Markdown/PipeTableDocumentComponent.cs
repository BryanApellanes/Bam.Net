using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation.Markdown
{
    public class PipeTableDocumentComponent: DocumentComponent
    {
        public PipeTableDocumentComponent(params string[] headers)
        {
            Headers = headers;
            _rows = new List<string[]>();
        }

        string[] _headers;
        public string[] Headers
        {
            get
            {
                return _headers;
            }
            set
            {
                _headers = value;
                OnUpdated(new DocumentComponentEventArgs { DocumentComponent = this });
            }
        }

        List<string[]> _rows;
        public int AddRow(params string[] cells)
        {
            _rows.Add(cells);
            OnUpdated(new DocumentComponentEventArgs { DocumentComponent = this });
            return _rows.Count;
        }

        public override string GetContent()
        {
            StringBuilder content = new StringBuilder();
            StringBuilder divider = new StringBuilder();
            content.Append("|");
            divider.Append("|");
            foreach(string header in Headers)
            {
                content.Append(header);
                content.Append("|");
                divider.Append(new string('-', 10));
                divider.Append("|");
            }
            content.AppendLine();
            content.AppendLine(divider.ToString());
            content.Append("|");
            foreach (string[] row in _rows)
            {
                foreach(string cell in row)
                {
                    content.Append(cell);
                    content.Append("|");
                }
            }
            return content.ToString();
        }
    }
}
