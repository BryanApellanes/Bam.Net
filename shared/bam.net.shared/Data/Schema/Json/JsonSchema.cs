using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Schema.Json
{
    public class JsonSchema
    {
        public const string IdBase = "http://schema.bamapps.net";
        public const string DefaultSchema = "http://json-schema.org/draft-07/schema#";

        public JsonSchema()
        {
            Required = new List<string>();
        }

        public string Schema { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public JsonSchemaTypes Type { get; set; }
        public Dictionary<string, JsonSchemaProperty> Properties { get; set; }
        public List<string> Required { get; set; }
        
        public virtual string ToJson(bool pretty = false)
        {
            // TODO: define a JsonSchemaTypeDefinition which is a dictionary like object
            // define JsonSchema as {string Schema, JsonSchemaTypeDefinition[] Definitions}
            // move all JsonSchema properties into JsonSchemaTypeDefinition except $schema and $id
            // https://json-schema.org/understanding-json-schema/structuring.html#structuring
            Dictionary<string, object> result = new Dictionary<string, object>()
            {
                { "$schema", Schema},
                { "$id", Id },
                { "title", Title },
                { "description", Description },
                { "type", Type.ToString().ToLowerInvariant() },
                { "properties", Properties },
                { "required", Required }
            };
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            return result.ToJson(settings);
        }

        public static JsonSchema FromDao<T>() where T: Dao
        {
            Type daoType = typeof(T);
            return FromDao(daoType);
        }

        public static JsonSchema FromInstance(object instance)
        {
            Args.ThrowIfNull(instance);
            return FromType(instance.GetType());
        }

        public static JsonSchema FromType(Type type)
        {
            return new JsonSchema()
            {
                Schema = DefaultSchema,
                Id = GetSchemaId(type),
                Title = type.Name,
                Type = JsonSchemaProperty.TranslateType(type),
                Properties = JsonSchemaProperty.PropertyDictionaryFromType(type)
            };
        }

        public static JsonSchema FromDao(object instance)
        {
            Args.ThrowIfNull(instance);
            return FromDao(instance.GetType());
        }
        
        public static JsonSchema FromDao(Type daoType)
        {
            string tableName = Dao.TableName(daoType);
            return new JsonSchema()
            {
                Schema = DefaultSchema,
                Id = GetSchemaId(daoType),
                Title = tableName,
                Type = JsonSchemaProperty.TranslateType(daoType),
                Properties = JsonSchemaProperty.FromDaoType(daoType)
            };
        }

        public static string GetSchemaId(Type type)
        {
            string name = Dao.TableName(type).Or(type.Name);
            return $"{IdBase}/{name}.schema.json";
        }
    }
}
