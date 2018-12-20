using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Schema.Json
{
    public class JsonSchema<T> : JsonSchema where T : class, new()
    {
        public T Data { get; set; }
        public override string ToJson()
        {
            JObject baseJson = JObject.Parse(base.ToJson());
            //JObject properties = JObject.Parse(Data.PropertiesToDictionary().ToJson());
            throw new NotImplementedException();
        }
    }

    public class JsonSchema
    {
        public string Schema { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string[] Required { get; set; }

        public virtual string ToJson()
        {
            return new Dictionary<string, string>()
            {
                { "$schema", Schema},
                { "$id", Id },
                { "title", Title },
                { "description", Description },
                { "type", Type }
            }.ToJson();
        }
    }
}
