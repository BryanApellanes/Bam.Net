/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Yaml.Serialization;
using System.Yaml;
using System.IO;
using Bam.Net;
using System.Reflection;
using System.Collections;
using YamlDotNet.Serialization;

namespace Bam.Net
{
    // TOOD: replace all uses of YamlSerializer (System.Yaml) with YamlDotNet.  
    // Convert yaml to json (yaml.YamlToJson) for all significant operations
    public static class YamlExtensions
    {
        public static string YamlToJson(this string yaml)
        {
            Deserializer deserializer = new Deserializer();
            object dict = deserializer.Deserialize(new StringReader(yaml));
            return dict.ToJson();
        }

        public static YamlSequence ToYamlSequence(this IEnumerable enumerable)
        {
            YamlSequence seq = new YamlSequence();
            foreach(object obj in enumerable)
            {
                seq.Add(obj.ToYamlNode());
            }
            return seq;
        }

        public static YamlNode ToYamlNode(this object val)
        {
            Type type = val.GetType();
            YamlMapping node = new YamlMapping();
            List<PropertyInfo> properties = new List<PropertyInfo>(type.GetProperties());
            properties.Sort((l, r) => l.MetadataToken.CompareTo(r.MetadataToken));
            foreach (PropertyInfo property in properties)
            {
                object propVal = property.GetValue(val);
                if(propVal != null)
                {
                    if (property.IsEnumerable())
                    {
                        YamlSequence yamlSequence = new YamlSequence();
                        node.Add(property.Name, yamlSequence);
                        foreach (object v in ((IEnumerable)propVal))
                        {
                            yamlSequence.Add(v.ToYamlNode());
                        }
                    }
                    else
                    {
                        if (property.PropertyType == typeof(bool) ||
                            property.PropertyType == typeof(bool?) ||
                            property.PropertyType == typeof(int) ||
                            property.PropertyType == typeof(int?) ||
                            property.PropertyType == typeof(long) ||
                            property.PropertyType == typeof(long?) ||
                            property.PropertyType == typeof(decimal) ||
                            property.PropertyType == typeof(decimal?) ||
                            property.PropertyType == typeof(double) ||
                            property.PropertyType == typeof(double?) ||
                            property.PropertyType == typeof(string) ||
                            property.PropertyType == typeof(byte[]) ||
                            property.PropertyType == typeof(DateTime) ||
                            property.PropertyType == typeof(DateTime?))
                        {
                            node.Add(property.Name, new YamlScalar(propVal.ToString()));
                        }
                        else
                        {
                            node.Add(property.Name, propVal.ToYamlNode());
                        }
                    }
                }
            }
            return node;
        }

        /// <summary>
        /// Serialize the specified object to yaml
        /// </summary>
        /// <param name="val"></param>
        /// <param name="conf"></param>
        /// <returns></returns>
        public static string ToYaml(this object val, YamlConfig conf = null)
        {
            YamlSerializer ser = conf == null ? new YamlSerializer() : new YamlSerializer(conf);
            return ser.Serialize(val);
        }

        public static void ToYamlFile(this object val, string filePath, YamlConfig conf = null)
        {
            ToYamlFile(val, new FileInfo(filePath), conf);
        }

        public static void ToYamlFile(this object val, FileInfo file, YamlConfig conf = null)
        {
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            using (StreamWriter sw = new StreamWriter(file.FullName))
            {
                sw.Write(val.ToYaml());
            }
        }
        
        public static object[] FromYaml(this string yaml, params Type[] expectedTypes)
        {
            YamlSerializer ser = new YamlSerializer();
            return ser.Deserialize(yaml, expectedTypes);
        }

        public static object[] FromYamlFile(this string filePath, params Type[] expectedTypes)
        {
            YamlSerializer ser = new YamlSerializer();
            return ser.DeserializeFromFile(filePath, expectedTypes);
        }
        
		public static object[] FromYamlFile(this FileInfo file, params Type[] expectedTypes)
		{
			return File.ReadAllText(file.FullName).FromYaml(expectedTypes);
		}

        public static T FromYamlFile<T>(this string filePath)
        {
            return new FileInfo(filePath).FromYamlFile<T>();
        }

        public static T FromYamlFile<T>(this FileInfo file, params Type[] expectedTypes)
        {
            return FromYaml<T>(File.ReadAllText(file.FullName));
        }

        public static T FromYaml<T>(this string yaml)
        {
            return yaml.ArrayFromYaml<T>().FirstOrDefault();
        }

        public static object FromYaml(this string yaml, Type type)
        {
            return yaml.ArrayFromYaml(type).FirstOrDefault();
        }

        public static Stream ToYamlStream(this object value)
        {
            MemoryStream stream = new MemoryStream();
            ToYamlStream(value, stream);
            return stream;
        }

        public static void ToYamlStream(this object value, Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(value.ToYaml());
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
        }

        public static object FromYamlStream(this Stream stream, Type type)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            using (StreamReader sr = new StreamReader(ms))
            {
                return sr.ReadToEnd().FromYaml();
            }
        }

        public static T FromYamlStream<T>(this Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            using (StreamReader sr = new StreamReader(ms))
            {
                return sr.ReadToEnd().FromYaml<T>();
            }
        }

        public static object[] ArrayFromYaml(this string yaml, Type type)
        {
            return new YamlSerializer().Deserialize(yaml, type);
        }

        public static T[] ArrayFromYaml<T>(this string yaml)
        {
            YamlSerializer ser = new YamlSerializer();
            object[] des = ser.Deserialize(yaml, typeof(T));
            return des.Each((o) => (T)o);
        }
    }
}
