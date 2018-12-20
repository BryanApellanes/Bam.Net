using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Schema.Json
{
    public class JsonSchemaPropertyInfo
    {
        public string Description { get; set; }
        public string Type { get; set; }

        public string ToJson()
        {
            return new { description = Description, type = Type }.ToJson();
        }
    }
}
