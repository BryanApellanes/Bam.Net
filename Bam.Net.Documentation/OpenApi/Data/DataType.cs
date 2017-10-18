using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation.OpenApi.Markdown
{
    public class DataType
    {
        public string CommonName { get; set; }
        public string Type { get; set; }
        protected string ClrTypeName { get; set; }
    }
}
