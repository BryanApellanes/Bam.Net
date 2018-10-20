/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic
{
    /// <summary>
    /// A convenience class for accessing common ReflectionExtensions.
    /// This is specifically to allow dynamic instances to use the
    /// reflection extension methods in a non extension sort of way with
    /// less typing.
    /// </summary>
    public static partial class Reflect
    {
        /// <summary>
        /// Get the property of the current instance with the specified name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyName">The name of the property value to retrieve</param>
        /// <returns></returns>
        public static T Property<T>(this object instance, string propertyName, bool throwIfPropertyNotFound = true)
        {
            return ReflectionExtensions.Property<T>(instance, propertyName, throwIfPropertyNotFound);
        }

        public static object Property(this object instance, string propertyName, object value, bool throwIfPropertyNotFound = true)
        {
            return ReflectionExtensions.Property(instance, propertyName, value, throwIfPropertyNotFound);
        }

        /// <summary>
        /// Returns true if the specified instance is of a type
        /// that has the specified propertyName
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool HasProperty(this object instance, string propertyName)
        {
            return ReflectionExtensions.HasProperty(instance, propertyName);
        }

        /// <summary>
        /// Get the property of the current instance with the specified name
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object Property(this object instance, string propertyName, bool throwIfPropertyNotFound = true)
        {
            return ReflectionExtensions.Property(instance, propertyName, throwIfPropertyNotFound);
        }

        /// <summary>
        /// Invoke the specified method on the specified instance 
        /// using the specified arguments
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        public static void Invoke(this object instance, string methodName, params object[] args)
        {
            ReflectionExtensions.Invoke(instance, methodName, args);
        }

        /// <summary>
        /// Invoke the specified method on the specified instance 
        /// using the specified arguments
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        public static T Invoke<T>(this object instance, string methodName, params object[] args)
        {
            return ReflectionExtensions.Invoke<T>(instance, methodName, args);
        }

        public static void ExecuteSql(this string sql, Database db, Dictionary<string, object> parameters)
        {
            db.ExecuteSql(sql, System.Data.CommandType.Text, parameters.ToDbParameters(db).ToArray());
        }
        /// <summary>
        /// Execute the specified sql using the specified parameters
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ExecuteSqlQuery(this string sql, Database db, Dictionary<string, object> parameters = null)
        {
            DynamicDatabase ddb = new DynamicDatabase(db);
            return ddb.Query(sql, parameters ?? new Dictionary<string, object>());
        }
    }
}
