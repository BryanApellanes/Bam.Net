using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Bam.Net.Data.Schema.Json
{
    public class JsonSchemaType
    {
        public JsonSchemaType(Type systemType, Func<PropertyInfo, bool>  requiredPredicate = null)
        {
            SystemType = systemType;
            Type = JsonSchemaProperty.TranslateType(systemType);
            if(requiredPredicate != null)
            {
                Required = SystemType.GetProperties().Where(requiredPredicate).Select(pi => pi.Name).ToList();
            }
        }

        protected internal Type SystemType { get; set; }
        public JsonSchemaTypes Type { get; set; }

        Dictionary<string, JsonSchemaProperty> _properties;
        object _propLock = new object();
        public Dictionary<string, JsonSchemaProperty> Properties
        {
            get
            {
                return _propLock.DoubleCheckLock(ref _properties, () => JsonSchemaProperty.PropertyDictionaryFromType(SystemType));
            }
            set
            {
                _properties = value;
            }
        }

        public List<string> Required { get; set; }
    }
}
