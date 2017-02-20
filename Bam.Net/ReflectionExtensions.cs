/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Returns a hash representing the specified
        /// types using the specified HashAlgorithm 
        /// and encoding
        /// </summary>
        /// <param name="types"></param>
        /// <param name="algorithm"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToInfoHash(this IEnumerable<Type> types, HashAlgorithms algorithm = HashAlgorithms.SHA1, Encoding encoding = null)
        {
            return types.ToInfoString().Hash(algorithm, encoding);
        }

        public static string ToInfoString(this IEnumerable<Type> types)
        {
            StringBuilder output = new StringBuilder();
            types.Each(type =>
            {
                output.AppendLine(type.ToInfoString());
            });

            return output.ToString();
        }

        public static string ToInfoHash(this Type type, HashAlgorithms algorithm = HashAlgorithms.SHA1, Encoding encoding = null)
        {
            return type.ToInfoString().Hash(algorithm, encoding);
        }

        /// <summary>
        /// Return a string representation of the specified 
        /// type.  This is primarily used for hashing the
        /// type for the purpose of uniquely identifying
        /// it across processes.  The resulting
        /// string cannot be easily converted back to 
        /// the original type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToInfoString(this Type type)
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine($"{type.FullName}");
            List<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());
            props.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
            props.Each(pi =>
            {
                output.AppendLine($"\t{pi.ToInfoString()}");
            });
            return output.ToString();
        }

        /// <summary>
        /// Return a string representation of the prop. This 
        /// is primarily used for hashing the property and
        /// the resulting string cannot be easily converted 
        /// back to the original PropertyInfo
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static string ToInfoString(this PropertyInfo prop)
        {
            return $"{{{prop.Name}:{prop.PropertyType.ToTypeString(false)}}}";
        }
        /// <summary>
        /// Invoke the specified static method of the 
        /// specified (extension method "current") type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T InvokeStatic<T>(this Type type, string methodName, params object[] args)
        {
            Args.ThrowIfNull(type);
            Args.ThrowIfNull(methodName);
            return (T)type.GetMethod(methodName).Invoke(null, args);
        }

        public static bool HasGenericArguments(this Type type)
        {
            Type[] ignore;
            return type.HasGenericArguments(out ignore);
        }
        public static bool HasGenericArguments(this Type type, out Type[] genericArgumentTypes)
        {
            genericArgumentTypes = type.GetGenericArguments();
            return genericArgumentTypes.Length > 0;
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
            Args.ThrowIfNull(instance, "instance");
            Args.ThrowIfNull(methodName, "methodName");
            return (T)instance.GetType().GetMethod(methodName, args.Select(a => a.GetType()).ToArray()).Invoke(instance, args);
        }

        public static T InvokeGeneric<T, TArg>(this object instance, string methodName, params object[] args)
        {
            Args.ThrowIfNull(instance, "instance");
            Args.ThrowIfNull(methodName, "methodName");
            MethodInfo method = instance.GetType().GetMethod(methodName, args.Select(a => a.GetType()).ToArray());
            MethodInfo genericMethod = method.MakeGenericMethod(typeof(TArg));
            return (T)genericMethod.Invoke(instance, args);
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
            Args.ThrowIfNull(instance, "instance");
            Args.ThrowIfNull(methodName, "methodName");
            instance.GetType().GetMethod(methodName).Invoke(instance, args);
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
            Args.ThrowIfNull(instance, "instance");
            PropertyInfo ignore;
            return HasProperty(instance, propertyName, out ignore);
        }

        /// <summary>
        /// Returns true if the specified instance is of a type
        /// that has the specified propertyName
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static bool HasProperty(this object instance, string propertyName, out PropertyInfo prop)
        {
            Args.ThrowIfNull(instance, "instance");
            Type type = instance.GetType();
            prop = type.GetProperty(propertyName);
            return prop != null;
        }

        /// <summary>
        /// Subscribe the specified handler to the specified event.  This
        /// is mostly useful when the type is ambiguous because
        /// the underlying implementation uses reflection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T On<T>(this T instance, string eventName, EventHandler handler)
        {
            return Subscribe<T>(instance, eventName, handler);
        }

        /// <summary>
        /// Subscribe the specified handler to the specified event 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T Subscribe<T>(this T instance, string eventName, EventHandler handler)
        {
            return Subscribe(instance, eventName, (Delegate)handler);
        }

        public static T On<T>(this T instance, string eventName, Delegate handler)
        {
            return Subscribe<T>(instance, eventName, handler);
        }

        public static T Subscribe<T>(this T instance, string eventName, Delegate handler)
        {
            EventInfo eventInfo = typeof(T).GetEvent(eventName);
            Args.ThrowIfNull(eventInfo, "eventName");
            eventInfo.AddEventHandler(instance, handler);
            return instance;
        }
        public static T SubscribeOnce<T>(this T instance, string eventName, EventHandler handler)
        {
            return SubscribeOnce(instance, eventName, (Delegate)handler);
        }
        public static T SubscribeOnce<T>(this T instance, string eventName, Delegate handler)
        {
            EventSubscription ignore;
            return instance.SubscribeOnce<T>(eventName, handler, out ignore);
        }
        public static T SubscribeOnce<T>(this T instance, string eventName, EventHandler handler, out EventSubscription subscription)
        {
            return SubscribeOnce(instance, eventName, (Delegate)handler, out subscription);
        }
        public static T SubscribeOnce<T>(this T instance, string eventName, Delegate handler, out EventSubscription subscription)
        {
            T result = instance.UnSubscribe(eventName, handler).Subscribe(eventName, handler);
            subscription = instance.GetEventSubscriptions(eventName).FirstOrDefault(es => es.Delegate.Equals(handler));
            return result;
        }
        public static object SubscribeOnce(this object instance, string eventName, EventHandler handler)
        {
            return SubscribeOnce(instance, eventName, (Delegate)handler);
        }
        public static object SubscribeOnce(this object instance, string eventName, Delegate handler)
        {
            EventSubscription ignore;
            return instance.SubscribeOnce(eventName, handler, out ignore);
        }
        public static object SubscribeOnce(this object instance, string eventName, EventHandler handler, out EventSubscription subscription)
        {
            return SubscribeOnce(instance, eventName, (Delegate)handler, out subscription);
        }
        public static object SubscribeOnce(this object instance, string eventName, Delegate handler, out EventSubscription subscription)
        {
            object result = instance.UnSubscribe(eventName, handler).Subscribe(eventName, handler);
            subscription = instance.GetEventSubscriptions(eventName).FirstOrDefault(es => es.Delegate.Equals(handler));
            return result;
        }

        public static object Subscribe(this object instance, string eventName, EventHandler handler)
        {
            return Subscribe(instance, eventName, (Delegate)handler);
        }
        /// <summary>
        /// Subscribe the specified handler to the specified event
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static object Subscribe(this object instance, string eventName, Delegate handler)
        {
            EventInfo eventInfo = instance.GetType().GetEvent(eventName);
            Args.ThrowIfNull(eventInfo, "eventName");
            eventInfo.AddEventHandler(instance, handler);
            return instance;
        }

        public static T Off<T>(this T instance, string eventName, EventHandler handler)
        {
            return Off<T>(instance, eventName, (Delegate)handler);
        }
        /// <summary>
        /// Unsubscribe the specified handler from the specified event 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T Off<T>(this T instance, string eventName, Delegate handler)
        {
            return UnSubscribe<T>(instance, eventName, handler);
        }

        /// <summary>
        /// Unsubscribe the specified handler from the specified event 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static object UnSubscribe(this object instance, string eventName, EventHandler handler)
        {
            EventInfo eventInfo = instance.GetType().GetEvent(eventName);
            Args.ThrowIfNull(eventInfo, "eventName");
            eventInfo.RemoveEventHandler(instance, handler);
            return instance;
        }
        public static T UnSubscribe<T>(this T instance, string eventName, EventHandler handler)
        {
            return UnSubscribe<T>(instance, eventName, (Delegate)handler);
        }
        /// <summary>
        /// Unsubscribe the specified handler from the specified event 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T UnSubscribe<T>(this T instance, string eventName, Delegate handler)
        {
            EventInfo eventInfo = typeof(T).GetEvent(eventName);
            Args.ThrowIfNull(eventInfo, "eventName");
            eventInfo.RemoveEventHandler(instance, handler);
            return instance;
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
            object value = Property(instance, propertyName, throwIfPropertyNotFound);
            return value == null ? default(T) : (T)value;
        }

        /// <summary>
        /// Get the property of the current instance with the specified name
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object Property(this object instance, string propertyName, bool throwIfPropertyNotFound = true)
        {
            Args.ThrowIfNull(instance, "instance");
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperties().FirstOrDefault(pi => pi.Name.ToLowerInvariant().Equals(propertyName.ToLowerInvariant()));
            if (property == null)
            {
                if (throwIfPropertyNotFound)
                {
                    PropertyNotFound(propertyName, type);
                }
                else
                {
                    return null;
                }
            }
            return property.GetValue(instance);
        }

        /// <summary>
        /// Set the specified property if it is 
        /// null or an empty string
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="throwIfPropertyNotFound"></param>
        /// <returns></returns>
        public static object PropertyIfNullOrBlank(this object instance, string propertyName, object value, bool throwIfPropertyNotFound = true)
        {
            object currentValue = instance.Property(propertyName, throwIfPropertyNotFound);
            string stringValue = currentValue as string;
            return instance.PropertyIf(currentValue == null || string.IsNullOrEmpty(stringValue), propertyName, value, throwIfPropertyNotFound);
        }

        /// <summary>
        /// Set the specified property if the specified
        /// condition is true
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="condition"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="throwIfPropertyNotFound"></param>
        /// <returns></returns>
        public static object PropertyIf(this object instance, bool condition, string propertyName, object value, bool throwIfPropertyNotFound = true)
        {
            if (condition)
            {
                instance.Property(propertyName, value, throwIfPropertyNotFound);
            }
            return null;
        }

        /// <summary>
        /// Set the property with the specified name and return the instance 
        /// to enable chaining
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object Property(this object instance, string propertyName, object value, bool throwIfPropertyNotFound = true)
        {
            Args.ThrowIfNull(instance, "instance");
            Type type = instance.GetType();
            PropertyInfo property = GetPropertyOrThrow(type, propertyName, throwIfPropertyNotFound);
            SetProperty(property, instance, value);
            return instance;
        }
        /// <summary>      
        /// Set the property with the specified name and return the instance 
        /// to enable chaining
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="instanceType"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="throwIfPropertyNotFound"></param>
        /// <returns></returns>
        public static object Property(this object instance, Type instanceType, string propertyName, object value, bool throwIfPropertyNotFound = true)
        {
            Args.ThrowIfNull(instance, "instance");
            Args.ThrowIfNull(instanceType, "instanceType");
            PropertyInfo property = GetPropertyOrThrow(instanceType, propertyName, throwIfPropertyNotFound);
            SetProperty(property, instance, value);
            return instance;
        }

        /// <summary>      
        /// Set the property with the specified name and return the instance 
        /// to enable chaining
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="throwIfPropertyNotFound"></param>
        /// <returns></returns>
        public static object Property<T>(this T instance, string propertyName, object value, bool throwIfPropertyNotFound = true)
        {
            Args.ThrowIfNull(instance, "instance");
            Type type = instance.GetType(); // get the actual instance type since it may be an extender of T
            PropertyInfo property = GetPropertyOrThrow(type, propertyName, throwIfPropertyNotFound);
            SetProperty(property, instance, value);
            return instance;
        }

        private static PropertyInfo GetPropertyOrThrow(Type type, string propertyName, bool throwIfPropertyNotFound)
        {
            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null && throwIfPropertyNotFound)
            {
                PropertyNotFound(propertyName, type);
            }

            return property;
        }

        private static void SetProperty(PropertyInfo property, object instance, object value)
        {
            if (property != null)
            {
                if (value == DBNull.Value)
                {
                    value = null;
                }
                property.SetValue(instance, value, null);
            }
        }


        public static void EachPropertyInfo(this object instance, Action<PropertyInfo> action)
        {
            Type type = instance.GetType();
            PropertyInfo[] properties = type.GetProperties();
            properties.Each(action);
        }

        public static void EachPropertyInfo(this object instance, Action<PropertyInfo, int> action)
        {
            Type type = instance.GetType();
            PropertyInfo[] properties = type.GetProperties();
            properties.Each(action);
        }

        public static void EachPropertyValue(this object instance, Action<PropertyInfo, object> action)
        {
            instance.EachPropertyInfo(pi =>
            {
                object value = pi.GetValue(instance, null);
                action(pi, value);
            });
        }

        public static void EachPropertyValue(this object instance, Action<PropertyInfo, object, int> action)
        {
            instance.EachPropertyInfo((pi, i) =>
            {
                object value = pi.GetValue(instance, null);
                action(pi, value, i);
            });
        }
        /// <summary>
        /// Return the results of the eacher by passing the PropertyInfo and value
        /// of each of the properties of instance where the property type is one
        /// of the base supported data types; bool, int, long, decimal, string, 
        /// byte[], DateTime or their nullable equivalent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="eacher"></param>
        /// <returns></returns>
        public static IEnumerable<T> EachDataProperty<T>(this object instance, Func<PropertyInfo, object, T> eacher)
        {
            return EachDataProperty(instance, instance.GetType(), (pi) => true, eacher);
        }
        /// <summary>
        /// Return the results of the eacher by passing the PropertyInfo and value
        /// of each of the properties of instance where the property type is one
        /// of the base supported data types; bool, int, long, decimal, string, 
        /// byte[], DateTime or their nullable equivalent 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="type"></param>
        /// <param name="eacher"></param>
        /// <returns></returns>
        public static IEnumerable<T> EachDataProperty<T>(this object instance, Type type, Func<PropertyInfo, object, T> eacher)
        {
            return EachDataProperty(instance, type, (pi) => true, eacher);
        }
        /// <summary>
        /// Return the results of the eacher by passing the PropertyInfo and value
        /// of each of the properties of instance where the property type is one
        /// of the base supported data types; bool, int, long, decimal, string, 
        /// byte[], DateTime or their nullable equivalent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyPredicate"></param>
        /// <param name="eacher"></param>
        /// <returns></returns>
        public static IEnumerable<T> EachDataProperty<T>(this object instance, Func<PropertyInfo, bool> propertyPredicate, Func<PropertyInfo, object, T> eacher)
        {
            return EachDataProperty<T>(instance, instance.GetType(), propertyPredicate, eacher);
        }

        /// <summary>
        /// Return the results of the eacher by passing the PropertyInfo and value
        /// of each of the properties of instance where the property is declared
        /// on the specified type and the property type is one of the base 
        /// supported data types; bool, int, long, decimal, string, byte[], 
        /// DateTime or their nullable equivalent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="type"></param>
        /// <param name="propertyPredicate"></param>
        /// <param name="eacher"></param>
        /// <returns></returns>
        public static IEnumerable<T> EachDataProperty<T>(this object instance, Type type, Func<PropertyInfo, bool> propertyPredicate, Func<PropertyInfo, object, T> eacher)
        {
            Args.ThrowIfNull(propertyPredicate, nameof(propertyPredicate));
            foreach (PropertyInfo pi in type.GetProperties().Where(pi => pi.DeclaringType == type && pi.PropertyType.In(typeof(bool?), typeof(bool), typeof(int), typeof(int?), typeof(long), typeof(long?), typeof(decimal), typeof(decimal?), typeof(string), typeof(byte[]), typeof(DateTime), typeof(DateTime?))))
            {
                if (propertyPredicate(pi))
                {
                    yield return eacher(pi, pi.GetValue(instance));
                }
            };
        }
        /// <summary>
        /// Return the Type as the string that can be used to 
        /// declare it in code
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToTypeString(this Type type, bool includeNamespace = true)
        {
            StringBuilder output = new StringBuilder();
            if (includeNamespace)
            {
                output.AppendFormat("{0}.", type.Namespace);
            }
            output.Append(type == typeof(int) || type == typeof(long) ? type.Name: type.Name.DropTrailingNonLetters());
            if (type.IsGenericType)
            {
                output.AppendFormat("<{0}>", type.GetGenericArguments().ToDelimited(t => includeNamespace ? "{0}.{1}"._Format(t.Namespace, t.ToTypeString(false)): t.ToTypeString(false)));
            }
            if (type.IsArray)
            {
                output.Append("[]");
            }
            return output.ToString();
        }

        public static bool IsOverridable(this MethodInfo method)
        {
            //https://msdn.microsoft.com/en-us/library/system.reflection.methodbase.isvirtual(v=vs.110).aspx
            return method.IsVirtual && !method.IsFinal;
        }

        private static void PropertyNotFound(string propertyName, Type type)
        {
            Args.Throw<InvalidOperationException>("Specified property ({0}) was not found on object of type ({1})", propertyName, type.Name);
        }

    }
}
