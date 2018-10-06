using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Yaml;
using System.Reflection;

namespace Bam.Net.Yaml
{
    public static class YamlExtensions
    {
        public static T GetValue<T>(this YamlMapping mapping, string key)
        {
            object value = GetValue(mapping, key);
            if(value is T typed)
            {
                return typed;
            }
            if (value is YamlMapping childMapping)
            {
                return Convert<T>(childMapping);
            }
            return default(T);
        }

        public static object GetValue(this YamlMapping mapping, Type asType, string key)
        {
            object value = GetValue(mapping, key);
            if(value != null)
            {
                return System.Convert.ChangeType(value, asType);
            }
            return null;
        }

        public static object GetValue(this YamlMapping mapping, string key, object ifKeyNotPresent = null)
        {
            if (!mapping.ContainsKey(key))
            {
                return ifKeyNotPresent;
            }
            object value = mapping[key];
            if(value is YamlScalar scalar)
            {
                return scalar.Value;
            }
            return value;
        }   

        public static IEnumerable<T> Convert<T>(this YamlSequence sequence)
        {
            foreach(YamlMapping mapping in sequence)
            {
                yield return Convert<T>(mapping);
            }
        }

        public static T Convert<T>(this YamlMapping mapping)
        {
            return (T)Convert(mapping, typeof(T));
        }

        public static object Convert(this YamlMapping mapping, Type type) 
        {
            object result = type.Construct();
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (mapping.ContainsKey(property.Name))
                {
                    YamlNode node = mapping[property.Name];
                    if (node is YamlScalar value)
                    {
                        result.Property(property.Name, System.Convert.ChangeType(value.Value, property.PropertyType));
                    }
                    else if (node is YamlMapping childMapping)
                    {
                        result.Property(property.Name, childMapping.Convert(property.PropertyType));
                    }
                }
            }
            return result;
        }
    }
}
