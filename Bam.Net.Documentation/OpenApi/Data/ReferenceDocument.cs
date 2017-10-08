using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation.OpenApi.Data
{
    public class ReferenceDocument
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public List<ObjectDocumentation> ObjectDocumentations { get; set; }
    }
}
