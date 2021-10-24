using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Data.Common;
using System.Data;
using System.Collections;
using Bam.Net.Logging;
using Newtonsoft.Json.Linq;
using NLog.LayoutRenderers;

namespace Bam.Net.Data
{
    public static partial class DataExtensions
    {
        /// <summary>
        /// Create a json safe version of the object
        /// by creating a JObject representing
        /// properties on the object that are adorned
        /// with the ColumnAttribute custom attribute.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object ToJsonSafe(this object obj, int maxRecursion = 5)
        {
            return ToJsonSafe(obj, maxRecursion, 0);
        }
        
        /// <summary>
        /// Create a json safe version of the object
        /// by creating a JObject representing
        /// properties on the object that are adorned
        /// with the custom attribute of type T.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="maxRecursion"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static object ToJsonSafe<T>(this object obj, int maxRecursion = 5) where T: Attribute
        {
            return ToJsonSafe<T>(obj, maxRecursion, 0);
        }

        private static object ToJsonSafe(this object obj, int maxRecursion, int recursionThusFar)
        {
            return ToJsonSafe<ColumnAttribute>(obj, maxRecursion, recursionThusFar);
        }
        
        /// <summary>
        /// Create a json safe version of the object
        /// by creating a JObject that represents
        /// the properties on the original object
        /// that are adorned with the custom attribute of type T.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="maxRecursion"></param>
        /// <param name="recursionThusFar"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static object ToJsonSafe<T>(this object obj, int maxRecursion, int recursionThusFar) where T: Attribute
        {
            Args.ThrowIfNull(obj, "obj");

            if (recursionThusFar >= maxRecursion)
            {
                Log.Warn("{0}: Max recursion reached ({1}) for instance of type ({2})", nameof(ToJsonSafe), maxRecursion, obj.GetType().Name);
                return null;
            }
            JObject jobj = new JObject();
            Type type = obj.GetType();
            IEnumerable<PropertyInfo> properties = type.GetProperties().Where(pi => pi.HasCustomAttributeOfType<T>());
            foreach (PropertyInfo prop in properties)
            {
                object val = prop.GetValue(obj);
                if (val != null)
                {
                    if (prop.PropertyType.IsPrimitiveNullableOrString() || prop.PropertyType.IsNullable<DateTime>())
                    {
                        jobj.Add(prop.Name, new JValue(val));
                    }
                    else
                    {
                        jobj.Add(prop.Name, (JObject)ToJsonSafe(val, maxRecursion, ++recursionThusFar));                        
                    }
                }
                else
                {
                    jobj.Add(prop.Name, null);
                }
            }
            
            return jobj;
        }
    }
}
