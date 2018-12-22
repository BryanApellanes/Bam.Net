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
        public string Type { get; set; }

        public string ToJson()
        {
            return new { description = Description, type = Type }.ToJson();
        }

        public static Dictionary<string, JsonSchemaProperty> PropertyDictionaryFromType<T>()
        {
            return PropertyDictionaryFromType(typeof(T));
        }

        public static Dictionary<string, JsonSchemaProperty> PropertyDictionaryFromType(Type type)
        {
            Args.ThrowIfNull(type);
            return PropertyDictionaryFromType(type, pi => Extensions.PropertyDataTypeFilter(pi) || pi.PropertyType.IsForEachable());
        }

        public static Dictionary<string, JsonSchemaProperty> FromDaoType<T>() where T : Dao
        {
            return FromDaoType(typeof(T));
        }

        public static Dictionary<string, JsonSchemaProperty> FromDaoType(Type type)
        {
            Args.ThrowIfNull(type);
            Args.ThrowIf(!type.IsSubclassOf(typeof(Dao)), "Specified type {0} is not a subclass of Dao", type.Name);
            return PropertyDictionaryFromType(type, pi => pi.HasCustomAttributeOfType<ColumnAttribute>());
        }

        public static Dictionary<string, JsonSchemaProperty> PropertyDictionaryFromType(Type type, Func<PropertyInfo, bool> predicate)
        {
            Dictionary<string, JsonSchemaProperty> result = new Dictionary<string, JsonSchemaProperty>();
            foreach (PropertyInfo prop in type.GetProperties().Where(predicate))
            {
                result.Add(prop.Name, FromPropertyInfo(prop));
            }
            return result;
        }

        public static JsonSchemaProperty FromPropertyInfo(PropertyInfo prop)
        {
            Type type = prop.PropertyType;
            if (type.IsNullable(out Type underlyingType))
            {
                type = underlyingType;
            }
            string description = $"{type.Namespace}.{type.Name}";
            JsonSchemaTypes jsonSchemaTypes = TranslateType(type, out Type elementType);
            if(jsonSchemaTypes == JsonSchemaTypes.Array)
            {
                return new JsonSchemaArrayProperty
                {
                    Description = description,
                    Type = jsonSchemaTypes.ToString().ToLowerInvariant(),
                    Items = GetArrayItemType(elementType)
                };
            }
            return new JsonSchemaProperty
            {
                Description = $"{type.Namespace}.{type.Name}",
                Type = jsonSchemaTypes.ToString().ToLowerInvariant()
            };
        }

        public static string GetArrayItemType(Type elementType)
        {
            JsonSchemaTypes primitiveType = TranslateType(elementType);
            if(primitiveType == JsonSchemaTypes.Object)
            {
                return JsonSchema.GetSchemaId(elementType);
            }
            return primitiveType.ToString().ToLowerInvariant();
        }

        public static JsonSchemaTypes TranslateType(Type type)
        {
            return TranslateType(type, out Type ignore);
        }

        public static JsonSchemaTypes TranslateType(Type type, out Type elementType)
        {
            elementType = null;
            if (type.IsForEachable(out elementType))
            {
                return JsonSchemaTypes.Array;
            }

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
