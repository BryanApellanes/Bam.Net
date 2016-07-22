/*
	Copyright © Bryan Apellanes 2015  
*/
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

namespace Bam.Net.Data
{
    public static class DataExtensions
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
            Type jsonSafeType = obj.CreateDynamicType<ColumnAttribute>(false);
            ConstructorInfo ctor = jsonSafeType.GetConstructor(new Type[] { });
            object jsonSafeInstance = ctor.Invoke(null);
            jsonSafeInstance.CopyProperties(obj);
            return jsonSafeInstance;
        }

        public static object[] ToJsonSafe(this IEnumerable e)
        {
            List<object> returnValues = new List<object>();
            foreach (object o in e)
            {
                returnValues.Add(o.ToJsonSafe());
            }

            return returnValues.ToArray();
        }

		public static List<object> ToListOf(this DataTable table, Type type, bool throwIfColumnPropertyNotFound = false)
		{
			List<object> result = new List<object>();
			foreach (DataRow row in table.Rows)
			{
				result.Add(row.ToInstanceOf(type, throwIfColumnPropertyNotFound));
			}

			return result;
		}

		public static List<T> ToListOf<T>(this DataTable table, bool throwIfColumnPropertyNotFound = false)
		{
			List<T> result = new List<T>();
			foreach(DataRow row in table.Rows)
			{
				result.Add(row.ToInstanceOf<T>(throwIfColumnPropertyNotFound));
			}

			return result;
		}

		public static T ToInstanceOf<T>(this DataRow row, bool throwIfColumnPropertyNotFound = false)
		{
			return (T)row.ToInstanceOf(typeof(T), throwIfColumnPropertyNotFound);
		}

		public static object ToInstanceOf(this DataRow row, Type type, bool throwIfColumnPropertyNotFound = false)
		{
			object result = type.Construct();
			foreach(DataColumn column in row.Table.Columns)
			{
				object value = row[column];
				result.Property(column.ColumnName, value, throwIfColumnPropertyNotFound);
			}
			return result;
		}
		
		public static DataRow ToDataRow(this object instance, string tableName = null)
		{
			Type instanceType = instance.GetType();
			tableName = tableName ?? instanceType.Name;
			PropertyInfo[] properties = instanceType.GetProperties();

			DataTable table = new DataTable(tableName);
			List<object> rowValues = new List<object>();
			foreach (PropertyInfo property in properties)
			{
				ColumnAttribute column;
				if (property.HasCustomAttributeOfType<ColumnAttribute>(true, out column))
				{
					table.Columns.Add(column.Name);
					rowValues.Add(property.GetValue(instance, null));
				}
			}

			return table.Rows.Add(rowValues.ToArray());
		}
        
        public static IEnumerable<DbParameter> ToDbParameters(this object parameters, Database db, string prefix = "@")
        {
            Args.ThrowIfNull(parameters, "parameters");
            Type type = parameters.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                DbParameter parameter = db.ServiceProvider.Get<DbProviderFactory>().CreateParameter();
                parameter.ParameterName = pi.Name;
                parameter.Value = pi.GetValue(parameters);
                yield return parameter;
            }
        }

        public static IEnumerable<DbParameter> ToDbParameters(this Dictionary<string, object> parameters, Database db)
        {
            Args.ThrowIfNull(parameters, "parameters");
            foreach (string key in parameters.Keys)
            {
                DbParameter parameter = db.ServiceProvider.Get<DbProviderFactory>().CreateParameter();
                parameter.ParameterName = key;
                parameter.Value = parameters[key];
                yield return parameter;
            }         
        }
    }
}
