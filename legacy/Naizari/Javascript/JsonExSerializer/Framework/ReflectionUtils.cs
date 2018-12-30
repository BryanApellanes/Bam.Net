/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Naizari.Javascript.JsonExSerialization.Framework
{
    public class ReflectionUtils
    {
        /// <summary>
        /// Returns the first attribute matching <typeparamref name="T"/> or null if none exist.
        /// </summary>
        /// <typeparam name="T">the custom attribute type</typeparam>
        /// <param name="provider">member with custom attributes</param>
        /// <param name="inherit">When true look up the hierarchy chain for inherited attributes</param>
        /// <returns>the first attribute</returns>
        public static T GetAttribute<T>(ICustomAttributeProvider provider, bool inherit) where T : Attribute
        {
            if (provider.IsDefined(typeof(T), inherit))
                return (T) provider.GetCustomAttributes(typeof(T), inherit)[0];
            else
                return null;
        }

        /// <summary>
        /// Tests whether two types are equivalent.  Types are equivalent if the
        /// types are equal, or if one type is the nullable type of the other type
        /// </summary>
        /// <param name="a">first type to test</param>
        /// <param name="b">second type to test</param>
        /// <returns>true if equivalent</returns>
        public static bool AreEquivalentTypes(Type a, Type b)
        {
            if (a == b)
                return true;

            if (IsNullableType(a))
                return Nullable.GetUnderlyingType(a) == b;

            if (IsNullableType(b))
                return Nullable.GetUnderlyingType(b) == a;

            return false;
        }

        /// <summary>
        /// Checks to see if the type is a nullable type
        /// </summary>
        /// <param name="checkType">the type to check</param>
        /// <returns>true if the type is a nullable type</returns>
        public static bool IsNullableType(Type checkType)
        {
            return (checkType.IsGenericType && checkType.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
