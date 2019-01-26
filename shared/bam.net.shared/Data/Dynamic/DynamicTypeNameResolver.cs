using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Yaml;
using Bam.Net.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bam.Net.Data.Dynamic
{
    public class DynamicTypeNameResolver : Loggable, IDynamicTypeNameResolver
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

        public static string ResolveYamlTypeName(string yaml, params string[] typeNameProperties)
        {
            DynamicTypeNameResolver resolver = new DynamicTypeNameResolver
            {
                TypeNameFields = new HashSet<string>(typeNameProperties)
            };
            return resolver.ResolveJsonTypeName(yaml.YamlToJson());
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

        public event EventHandler InvalidFormatSpecified;
        public event EventHandler JsonTypeNameResovled;
        public event EventHandler YamlTypeNameResolved;
        public event EventHandler CsvTypeNameResolved;

        public string ResolveTypeName(string input, DynamicTypeFormats format)
        {
            switch (format)
            {
                case DynamicTypeFormats.Invalid:
                    string hash = input.Sha256();
                    FireEvent(InvalidFormatSpecified, new DynamicTypeNameEventArgs { Input = input, Format = format, ResolvedTypeName = hash});
                    return hash;
                case DynamicTypeFormats.Json:
                    string jsonTypeName = ResolveJsonTypeName(input);
                    FireEvent(JsonTypeNameResovled, new DynamicTypeNameEventArgs { Input = input, Format = format, ResolvedTypeName = jsonTypeName });
                    return jsonTypeName;
                case DynamicTypeFormats.Yaml:
                    string yamlTypeName = ResolveYamlTypeName(input);
                    FireEvent(YamlTypeNameResolved, new DynamicTypeNameEventArgs { Input = input, Format = format, ResolvedTypeName = yamlTypeName });
                    return yamlTypeName;
                case DynamicTypeFormats.Csv:
                    string firstLine = input.DelimitSplit("\r", "\n").First();
                    string typeName = string.Join(",", GetPropertyDescriptors(firstLine).Select(pd => pd.ToString()).ToArray()).Sha256();
                    FireEvent(CsvTypeNameResolved, new DynamicTypeNameEventArgs { Input = input, Format = format, ResolvedTypeName = typeName });
                    return typeName;
                default:
                    break;
            }

            return input.Sha256();
        }

        public string ResolveJsonTypeName(string json)
        {
            return ResolveJsonTypeName(json, out bool ignore);
        }

        public string ResolveJsonTypeName(string json, out bool isDefault)
        {
            JObject jobject = (JObject)JsonConvert.DeserializeObject(json);
            return ResolveTypeName(jobject, out isDefault);
        }

        public string ResolveTypeName(JObject jobject)
        {
            return ResolveTypeName(jobject, out bool ignore);
        }

        /// <summary>
        /// Resolves the name of the type by looking for a property named in TypeNameFields
        /// and return the result.
        /// </summary>
        /// <param name="jobject">The jobject.</param>
        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
        /// <returns></returns>
        public string ResolveTypeName(JObject jobject, out bool isDefault)
        {
            isDefault = false;
            if(jobject.Type != JTokenType.Object)
            {
                return jobject.Type.ToString();
            }
            foreach(JProperty prop in jobject.Properties())
            {
                if (TypeNameFields.Contains(prop.Name))
                {
                    return (prop.Value)?.ToString();
                }
            }

            string defaultTypeName = string.Join(",", GetPropertyDescriptors(jobject).Select(pd => pd.ToString()).ToArray()).Sha256();
            isDefault = true;
            return defaultTypeName;
        }
        
        public string ResolveYamlTypeName(string yaml)
        {
            return ResolveJsonTypeName(yaml.YamlToJson());
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

        public PropertyDescriptor[] GetPropertyDescriptors(string csvHeader)
        {
            string[] split = csvHeader.DelimitSplit(",");
            return split.Select(pn => new PropertyDescriptor { Name = pn, Type = "string" }).ToArray();
        }
    }
}
