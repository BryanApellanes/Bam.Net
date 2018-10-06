using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    /// <summary>
    /// A class that provides a mapping between numeric (long) id values and Types.
    /// </summary>
    public class JournalTypeMap
    {
        public JournalTypeMap(SystemPaths paths) : this()
        {
            if(paths != null)
            {
                Directory = new DirectoryInfo(Path.Combine(paths.Data.AppData));
            }
        }
        
        protected JournalTypeMap()
        {
            TypeMappings = new ConcurrentDictionary<long, string>();
            PropertyMappings = new ConcurrentDictionary<long, string>();
        }

        protected internal DirectoryInfo Directory { get; set; }
        
        public string Save()
        {
            string path = Path.Combine(Directory.FullName, nameof(JournalTypeMap));
            this.ToJsonFile(path);
            return path;
        }

        public static JournalTypeMap Load(string path)
        {
            JournalTypeMap typeMap = path.FromJsonFile<JournalTypeMap>();
            typeMap.Directory = new FileInfo(path).Directory;
            return typeMap;
        }

        public ConcurrentDictionary<long, string> TypeMappings { get; set; }
        public ConcurrentDictionary<long, string> PropertyMappings { get; set; }

        /// <summary>
        /// Gets the name of the type using TypeMappings, if the mapping is not found the string representation of 
        /// the specified typeId is returned.
        /// </summary>
        /// <param name="typeId">The type identifier.</param>
        /// <returns></returns>
        public string GetTypeName(long typeId)
        {
            if(TypeMappings.TryGetValue(typeId, out string value))
            {
                return value;
            }
            return typeId.ToString();
        }

        public string GetTypeName(KeyHashAuditRepoData instance)
        {
            return GetTypeName(GetTypeId(instance));
        }

        public string GetTypeShortName(KeyHashAuditRepoData instance)
        {
            return GetTypeShortName(GetTypeId(instance));
        }

        public string GetTypeShortName(long typeId)
        {
            return GetTypeName(typeId).DelimitSplit(".").Last();
        }

        /// <summary>
        /// Gets the name of the property using PropertyMappings, if the mapping is not found the string representation
        /// of the specified property is returned.
        /// </summary>
        /// <param name="propertyId">The property identifier.</param>
        /// <returns></returns>
        public string GetPropertyName(long propertyId)
        {
            if(PropertyMappings.TryGetValue(propertyId, out string value))
            {
                return value;
            }
            return propertyId.ToString();
        }

        public string GetPropertyShortName(long propertyId)
        {
            string propAndType = GetPropertyName(propertyId).DelimitSplit(".").Last();
            return propAndType.DelimitSplit("__").First();
        }

        public void AddMapping(KeyHashAuditRepoData instance)
        {
            long typeId = GetTypeId(instance, out object dynamicInstance, out Type dynamicType);
            AddTypeMapping(dynamicType);
            foreach (PropertyInfo property in dynamicType.GetProperties())
            {
                AddPropertyMapping(property);
            }
        }
        
        protected long AddPropertyMapping(PropertyInfo property)
        {
            long key = GetPropertyId(property, out string name);
            PropertyMappings.TryAdd(key, name);
            return key;
        }

        protected long AddTypeMapping(Type type)
        {
            long key = GetTypeId(type, out string name);
            TypeMappings.TryAdd(key, name);
            return key;
        }

        public static PropertyInfo[] GetTypeProperties<T>() where T: KeyHashAuditRepoData, new()
        {
            GetTypeId(new T(), out object ignore, out Type dynamicType);
            return dynamicType.GetProperties();
        }

        public static long GetTypeId(KeyHashAuditRepoData instance)
        {
            return GetTypeId(instance, out object ignore1, out Type ignore2);
        }

        public static long GetTypeId(KeyHashAuditRepoData instance, out object dynamicInstance, out Type dynamicType)
        {
            dynamicInstance = instance.ToDynamic(p => p.PropertyType.IsValueType || p.PropertyType == typeof(string), out dynamicType);
            return GetTypeId(dynamicType, out string ignore);
        }

        public static long GetTypeId(Type type, out string name)
        {
            name = NormalizeName(type);
            return name.ToSha256Long();
        }

        public static long GetPropertyId(PropertyInfo prop, out string name)
        {
            name = NormalizeName(prop);
            return name.ToSha256Long();
        }

        private static string NormalizeName(Type type)
        {
            return $"{type.Namespace}.{type.Name}";
        }

        private static string NormalizeName(PropertyInfo prop)
        {
            return $"{prop.DeclaringType.Namespace}.{prop.DeclaringType.Name}.{prop.Name}__{prop.PropertyType.Name}";
        }
    }
}
