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

        public static IEnumerable<dynamic> Query(string sqlQuery, Database db, object dynamicDbParameters, string typeName = null)
        {
            return db.Query(sqlQuery, dynamicDbParameters);
        }

        public static IEnumerable<dynamic> Query(string sqlQuery, Database db, Dictionary<string, object> dictDbParameters, string typeName = null)
        {
            return db.Query(sqlQuery, dictDbParameters);
        }
        public static IEnumerable<dynamic> Query(string sqlQuery, Database db, DbParameter[] dbParameters, string typeName = null)
        {
            return db.Query(sqlQuery, dbParameters, typeName);
        }

        public static IEnumerable<T> Query<T>(this string sqlQuery, Database db, object dynamicDbProperties)
        {
            return db.Query<T>(sqlQuery, dynamicDbProperties);
        }

        public static IEnumerable<T> Query<T>(this string sqlQuery, Database db, Dictionary<string, object> dbParameters)
        {
            return db.Query<T>(sqlQuery, dbParameters);
        }

        public static IEnumerable<T> Query<T>(this string sqlQuery, Database db, params DbParameter[] dbParameters)
        {
            return db.Query<T>(sqlQuery, dbParameters);
        }

        public static IEnumerable<T> Query<T>(string sqlQuery, Database db, Func<DataRow, T> rowProcessor, params DbParameter[] dbParameters)
        {
            return db.Query<T>(sqlQuery, rowProcessor, dbParameters);
        }

        public static IEnumerable<T> ToEnumerableOf<T>(this DataTable table, bool throwIfColumnPropertyNotFound = false)
        {
            foreach(DataRow row in table.Rows)
            {
                yield return row.ToInstanceOf<T>(throwIfColumnPropertyNotFound);
            }
        }

        public static List<T> ToListOf<T>(this DataTable table, bool throwIfColumnPropertyNotFound = false)
		{
            return ToEnumerableOf<T>(table, throwIfColumnPropertyNotFound).ToList();
		}

        public static T ToInstanceOf<T>(this DataRow row, bool throwIfColumnPropertyNotFound = false)
		{
			return (T)row.ToInstanceOf(typeof(T), throwIfColumnPropertyNotFound);
		}
        public static IEnumerable<object> ToEnumerableOf(this DataTable table, Type type, bool throwIfColumnPropertyNotFound = false)
        {
            foreach(DataRow row in table.Rows)
            {
                yield return row.ToInstanceOf(type, throwIfColumnPropertyNotFound);
            }
        }
        public static List<object> ToListOf(this DataTable table, Type type, bool throwIfColumnPropertyNotFound = false)
        {
            return ToEnumerableOf(table, type, throwIfColumnPropertyNotFound).ToList();
        }
        public static object ToInstanceOf(this DataRow row, Type type, bool throwIfColumnPropertyNotFound = false)
		{
			object result = type.Construct();
			foreach(DataColumn column in row.Table.Columns)
			{
				object value = row[column];
				result.Property(type, column.ColumnName, value, throwIfColumnPropertyNotFound);
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

        public static string Sha1(this DbParameter[] dbParameters, Encoding encoding = null)
        {
            return Hash(dbParameters, HashAlgorithms.SHA1, encoding);
        }

        public static string Md5(this DbParameter[] dbParameters, Encoding encoding = null)
        {
            return Hash(dbParameters, HashAlgorithms.MD5, encoding);
        }

        public static string Hash(this DbParameter[] dbParameters, HashAlgorithms algorithm, Encoding encoding = null)
        {
            string infoString = ToInfoString(dbParameters, encoding);
            return infoString.Hash(algorithm, encoding);
        }

        public static string ToInfoString(this DbParameter[] dbParameters, Encoding encoding = null)
        {
            List<DbParameter> sorted = new List<DbParameter>(dbParameters);
            sorted.Sort((left, right) => left.ParameterName.CompareTo(right.ParameterName));
            StringBuilder info = new StringBuilder();
            sorted.ForEach(p =>
            {
                info.AppendLine(ToInfoString(p, encoding));
            });
            string infoString = info.ToString();
            return infoString;
        }

        public static string ToInfoString(this DbParameter dbParameter, Encoding encoding = null)
        {
            return $"--{dbParameter.ParameterName}={dbParameter.Value.ToString()}";
        }
        public static string Sha1(this DbParameter dbParameter, Encoding encoding = null)
        {
            return Hash(dbParameter, HashAlgorithms.SHA1, encoding);
        }

        public static string Md5(this DbParameter dbParameter, Encoding encoding = null)
        {
            return Hash(dbParameter, HashAlgorithms.MD5, encoding);
        }

        public static string Hash(this DbParameter dbParameter, HashAlgorithms algorithm, Encoding encoding = null)
        {
            return $"{dbParameter.ParameterName}={dbParameter.Value.ToString()}".Hash(algorithm, encoding);
        }

        public static IEnumerable<DbParameter> ToDbParameters(this object dynamicDbParameters, Database db)
        {
            Args.ThrowIfNull(dynamicDbParameters, "parameters");
            Type type = dynamicDbParameters.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                //DbParameter parameter = db.ServiceProvider.Get<DbProviderFactory>().CreateParameter();
                //parameter.ParameterName = pi.Name;
                //parameter.Value = pi.GetValue(parameters);
                yield return db.CreateParameter(pi.Name, pi.GetValue(dynamicDbParameters));
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
