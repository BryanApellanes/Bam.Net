using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation.OpenApi.Data
{
    public class ApiDocumentation
    {
        public string ApiName { get; set; }
        public string Description { get; set; }
        public List<ObjectDocumentation> ObjectDocumentations { get; set; }
    }
}
