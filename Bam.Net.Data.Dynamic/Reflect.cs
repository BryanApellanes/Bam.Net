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
    public static class Reflect
    {
        /// <summary>
        /// Combines the current instance with the specified toMerge values
        /// creating a new type with all the properties of each and value 
        /// set to the last one in
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="toMerge"></param>
        /// <returns></returns>
        public static object Combine(this object instance, params object[] toMerge)
        {
            return Extensions.Combine(instance, toMerge);
        }

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
            return ddb.Execute(sql, parameters ?? new Dictionary<string, object>());
        }
    }
}
