using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Bam.Net.Data.Schema.Json
{
    public class JsonSchemaObjectProperty : JsonSchemaProperty
    {
        public JsonSchemaObjectProperty(Type systemType, Func<PropertyInfo, bool>  requiredPredicate = null)
        {
            SystemType = systemType;
            Type = TranslateType(systemType).ToString().ToLowerInvariant();
            if(requiredPredicate != null)
            {
                Required = SystemType.GetProperties().Where(requiredPredicate).Select(pi => pi.Name).ToList();
            }
        }

        protected internal Type SystemType { get; set; }

        Dictionary<string, JsonSchemaProperty> _properties;
        object _propLock = new object();
        public Dictionary<string, JsonSchemaProperty> Properties
        {
            get
            {
                return _propLock.DoubleCheckLock(ref _properties, () => PropertyDictionaryFromType(SystemType));
            }
            set
            {
                _properties = value;
            }
        }

        public List<string> Required { get; set; }
    }
}
