using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation.OpenApi.Data
{
    public class ObjectDocumentation: DocumentSection
    {
        public ObjectDocumentation()
        {
            Fields = new List<FieldDocumentation>();            
        }

        public string ObjectName { get; set; }
        public string Description { get; set; }
        public List<FieldDocumentation> Fields { get; set; }
    }
}
