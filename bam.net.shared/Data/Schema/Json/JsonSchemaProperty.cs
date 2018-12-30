using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Bam.Net;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Data.Schema.Json
{
    public class JsonSchemaProperty
    {
        static JsonSchemaProperty()
        {
            RequiredPropertyFilters = new Dictionary<Type, Func<PropertyInfo, bool>>();
        }

        public string Description { get; set; }
        public string Type { get; set; }
        
        public static Dictionary<Type, Func<PropertyInfo, bool>> RequiredPropertyFilters
        {
            get;
            set;
        }

        public static Dictionary<string, JsonSchemaProperty> PropertyDictionaryFromType<T>()
        {
            return PropertyDictionaryFromType(typeof(T));
        }

        public static Dictionary<string, JsonSchemaProperty> PropertyDictionaryFromType(Type type)
        {
            Args.ThrowIfNull(type);
            return PropertyDictionaryFromType(type, pi => 
                pi.PropertyType.HasCustomAttributeOfType<JsonSchemaAttribute>() || 
                Extensions.PropertyDataTypeFilter(pi) || 
                pi.PropertyType.IsForEachable() ||
                pi.PropertyType.IsSubclassOf(typeof(RepoData))
            );
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

        public static Dictionary<string, JsonSchemaProperty> PropertyDictionaryFromType(Type type, Func<PropertyInfo, bool> propertyPredicate)
        {
            Dictionary<string, JsonSchemaProperty> result = new Dictionary<string, JsonSchemaProperty>();
            foreach (PropertyInfo prop in type.GetProperties().Where(propertyPredicate))
            {
                result.Add(prop.Name, FromPropertyInfo(prop));
            }
            return result;
        }

        public static JsonSchemaProperty FromPropertyInfo(PropertyInfo prop, Func<PropertyInfo, bool> requiredPropertyPredicate = null)
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
            if(jsonSchemaTypes == JsonSchemaTypes.Object)
            {
                if(requiredPropertyPredicate == null)
                {
                    if (RequiredPropertyFilters.ContainsKey(type))
                    {
                        requiredPropertyPredicate = RequiredPropertyFilters[type];
                    }
                }
                return new JsonSchemaObjectProperty(type, requiredPropertyPredicate)
                {
                    Description = description,
                    Type = jsonSchemaTypes.ToString().ToLowerInvariant(),                    
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
            else if (type == typeof(int) || type == typeof(uint) || type == typeof(int?) || type == typeof(uint?))
            {
                return JsonSchemaTypes.Integer;
            }
            else if (type.IsNumberType() || type.IsNullableNumberType())
            {
                return JsonSchemaTypes.Number;
            }
            else if(type == typeof(bool))
            {
                return JsonSchemaTypes.Boolean;
            }
            else if(type == typeof(string) || type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return JsonSchemaTypes.String;
            }

            return JsonSchemaTypes.Object;
        }
    }
}
