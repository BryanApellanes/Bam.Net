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
using Newtonsoft.Json.Linq;

namespace Bam.Net.Data
{
    public static partial class DataExtensions
    {
        /// <summary>
        /// Create a json safe version of the object
        /// by creating a dynamic type that represents
        /// the properties on the original object
        /// that are addorned with the ColumnAttribute
        /// custom attribute.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object ToJsonSafe(this object obj)
        {
            Args.ThrowIfNull(obj, "obj");
            JObject jobj = new JObject();
            Type type = obj.GetType();
            IEnumerable<PropertyInfo> properties = type.GetProperties().Where(pi => pi.HasCustomAttributeOfType<ColumnAttribute>());
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
                        jobj.Add(prop.Name, (JObject)ToJsonSafe(val));                        
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
