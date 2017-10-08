using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation.OpenApi.Data
{
    public class FieldDocumentation
    {
        public string FieldName { get; set; }
        public DataType DataType { get; set; }
        public bool IsRequired { get; set; }
    }
}
