using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Yaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bam.Net.Data.Dynamic
{
    public class DynamicTypeNameResolver : IDynamicTypeNameResolver
    {
        public DynamicTypeNameResolver()
        {
            TypeNameFields = new HashSet<string>(new string[] { "typeName", "TypeName", "class", "Class", "className", "ClassName" });
        }

        public HashSet<string> TypeNameFields
        {
            get;
            set;
        }

        public static string[] ResolveYamlTypeNames(string yaml, params string[] typeNameProperties)
        {
            DynamicTypeNameResolver resolver = new DynamicTypeNameResolver();
            if(typeNameProperties.Length > 0)
            {
                resolver.TypeNameFields = new HashSet<string>(typeNameProperties);
            }
            return resolver.ResolveYamlTypeNames(yaml);
        }

        /// <summary>
        /// Resolves the name of the type for the specified json by first checking for a value
        /// in any of the properties specified by typeNameProperties.  If the specified json 
        /// doesn't have any of the specified properties, the Sha256 hash of the comma concatenated list of
        /// property descriptors is returned.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="typeNameProperties">The type name properties.</param>
        /// <returns></returns>
        public static string ResolveJsonTypeName(string json, params string[] typeNameProperties)
        {
            DynamicTypeNameResolver resolver = new DynamicTypeNameResolver();
            if(typeNameProperties.Length > 0)
            {
                resolver.TypeNameFields = new HashSet<string>(typeNameProperties);
            }
            return resolver.ResolveJsonTypeName(json);
        }

        public string ResolveJsonTypeName(string json)
        {
            JObject jobject = (JObject)JsonConvert.DeserializeObject(json);
            return ResolveTypeName(jobject);
        }

        public string ResolveTypeName(JObject jobject)
        {
            if(jobject.Type != JTokenType.Object)
            {
                return jobject.Type.ToString();
            }
            foreach(JProperty prop in jobject.Properties())
            {
                if (TypeNameFields.Contains(prop.Name))
                {
                    return prop.Name;
                }
            }

            return string.Join(",", GetPropertyDescriptors(jobject).Select(pd => pd.ToString()).ToArray()).Sha256();
        }
        
        public string[] ResolveYamlTypeNames(string yaml)
        {
            List<string> typeNames = new List<string>();
            foreach(YamlNode node in YamlNode.FromYaml(yaml))
            {
                typeNames.Add(ResolveTypeName(node));
            }
            return typeNames.ToArray();
        }

        public string ResolveTypeName(YamlNode yamlNode)
        {
            return ResolveJsonTypeName(yamlNode.ToString());
        }

        public PropertyDescriptor[] GetPropertyDescriptors(JObject jObject)
        {
            List<PropertyDescriptor> properties = new List<PropertyDescriptor>();
            foreach(JProperty prop in jObject.Properties())
            {
                string propType = prop.Type.ToString();
                if(prop.Type == JTokenType.Object)
                {
                    propType = ResolveJsonTypeName(prop.Value.ToString());
                }
                properties.Add(new PropertyDescriptor { Name = prop.Name, Type = propType });
            }
            return properties.ToArray();
        }
    }
}
