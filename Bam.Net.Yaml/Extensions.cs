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

namespace Bam.Net.Yaml
{
    public static class Extensions
    {
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

        public static T FromYamlFile<T>(this FileInfo file, params Type[] expectedTypes)
        {
            return FromYaml<T>(File.ReadAllText(file.FullName));
        }

        public static T FromYaml<T>(this string yaml)
        {
            return yaml.ArrayFromYaml<T>().FirstOrDefault();
        }

        public static Stream ToYamlStream(this object value)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(value.ToYaml());
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
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

        public static T[] ArrayFromYaml<T>(this string yaml)
        {
            YamlSerializer ser = new YamlSerializer();
            YamlConfig c = new YamlConfig();
            object[] des = ser.Deserialize(yaml, typeof(T));
            return des.Each((o) => (T)o);
        }
    }
}
