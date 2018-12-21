using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Bam.Net;

namespace Bam.Net.Data.Schema.Json
{
    public class JsonSchemaProperty
    {
        public string Description { get; set; }
        public JsonSchemaTypes Type { get; set; }

        public string ToJson()
        {
            return new { description = Description, type = Type }.ToJson();
        }

        public static JsonSchemaProperty FromPropertyInfo(PropertyInfo prop)
        {
            Type type = prop.PropertyType;
            if(type.IsNullable(out Type underlyingType))
            {
                type = underlyingType;
            }
            return new JsonSchemaProperty
            {
                Description = $"{type.Namespace}.{type.Name}",
                Type = TranslateType(type)
            };
        }

        public static Dictionary<string, JsonSchemaProperty> FromDaoType<T>() where T: Dao
        {
            return FromDaoType(typeof(T));
        }

        public static Dictionary<string, JsonSchemaProperty> FromDaoType(Type type)
        {
            Args.ThrowIfNull(type);
            Args.ThrowIf(!type.IsSubclassOf(typeof(Dao)), "Specified type {0} is not a subclass of Dao", type.Name);
            return FromType(type, pi => pi.HasCustomAttributeOfType<ColumnAttribute>());
        }

        public static Dictionary<string, JsonSchemaProperty> FromType(Type type, Func<PropertyInfo, bool> predicate)
        {
            Dictionary<string, JsonSchemaProperty> result = new Dictionary<string, JsonSchemaProperty>();
            foreach (PropertyInfo prop in type.GetProperties().Where(predicate))
            {
                result.Add(prop.Name, FromPropertyInfo(prop));
            }
            return result;
        }
        
        public static JsonSchemaTypes TranslateType(Type type)
        {
            if(type.IsNullable(out Type underlyingType))
            {
                type = underlyingType;
            }

            if (type == null)
            {
                return JsonSchemaTypes.Null;
            }
            else if (type == typeof(int) || type == typeof(uint))
            {
                return JsonSchemaTypes.Integer;
            }
            else if (type.IsNumberType())
            {
                return JsonSchemaTypes.Number;
            }
            else if(type == typeof(bool))
            {
                return JsonSchemaTypes.Boolean;
            }
            else if(type == typeof(string))
            {
                return JsonSchemaTypes.String;
            }
            return JsonSchemaTypes.Object;
        }
    }
}
