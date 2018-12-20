using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Schema.Json
{
    public class JsonSchemaProperties
    {
        public List<JsonSchemaProperty> Properties { get; set; }

        public string ToJson()
        {
            Dictionary<string, string> propertyDetails = new Dictionary<string, string>();
            foreach(JsonSchemaProperty property in Properties)
            {
                propertyDetails.Add(property.Name.ToLowerInvariant(), property.PropertyDetails.ToJson());
            }
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("properties", propertyDetails.ToJson());
        }
    }
}
