/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bam.Net.Html
{
    public static class Extensions
    {
        private static List<Type> _types = new List<Type>();
        static Extensions()
        {
            //prop.PropertyType == typeof(string) || 
            //prop.PropertyType == typeof(int) || 
            //prop.PropertyType == typeof(long)

            _types.Add(typeof(string));
            _types.Add(typeof(int));
            _types.Add(typeof(long));
            _types.Add(typeof(int?));
            _types.Add(typeof(long?));
            _types.Add(typeof(bool));
            _types.Add(typeof(bool?));
        }
        
        public static Tag PropertiesToTable<T>(this IEnumerable<T> values, Action<Tag, T> forEachRow = null)
        {
            return PropertiesToTable<T>(values.ToArray(), _types, forEachRow);
        }

        public static Tag PropertiesToTable<T>(this IEnumerable<T> values)
        {
            return PropertiesToTable<T>(values.ToArray());
        }
        
        public static Tag PropertiesToTable<T>(this T[] values, List<Type> validPropertyTypes = null, Action<Tag, T> forEachRow = null)
        {
            if(validPropertyTypes == null)
            {
                validPropertyTypes = _types;
            }

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            int l = values.Length;
            
            
            Tag result = new Tag("table");
            Tag header = new Tag("thead");
            Tag headerRow = new Tag("tr");
            
            foreach(PropertyInfo prop in properties)
            {
                if (validPropertyTypes.Contains(prop.PropertyType))
                {
                    headerRow.Child(new Tag("th").Text(prop.Name));
                }
            }
            if (forEachRow != null)
            {
                forEachRow(headerRow, default(T));
            }
            header.Child(headerRow);            
            result.Child(header);

            for (int i = 0; i < l; i++)
            {
                T current = values[i];
                Tag row = new Tag("tr");
                foreach (PropertyInfo prop in properties)
                {
                    if (validPropertyTypes.Contains(prop.PropertyType))
                    {
                        object val = prop.GetValue(current, null);
                        string propVal = val != null ? val.ToString() : "null";
                        row.Child("td", propVal);
                    }
                }
                if (forEachRow != null)
                {
                    forEachRow(row, current);
                }
                result.Child(row);
            }

            return result;
        }
    }
}
