/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Naizari.Extensions
{
    public static class CustomAttributeExtension
    {
        /// <summary>
        /// Gets the MethodInfo for the first method found in the specified typeToAnalyze
        /// that has a custom attribute of the specified type T.  Returns null if none
        /// are found.
        /// </summary>
        /// <typeparam name="T">the type of the custom attribute to look for</typeparam>
        /// <param name="typeToAnalyze">the type to analyze</param>
        /// <returns>MethodInfo or null</returns>
        public static MethodInfo GetFirstMethodWithAttributeOfType<T>(this Type typeToAnalyze) where T : Attribute, new()
        {
            MethodInfo[] methods = typeToAnalyze.GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (HasCustomAttributeOfType<T>(method))
                    return method;
            }

            return null;
        }

        /// <summary>
        /// Gets the first PropertyInfo with a custom attribute of the specified generic type T
        /// </summary>
        /// <typeparam name="T">The generic type of the attribute to search for</typeparam>
        /// <param name="typeToAnalyze"></param>
        /// <returns></returns>
        public static PropertyInfo GetFirstProperyWithAttributeOfType<T>(this Type typeToAnalyze) where T : Attribute
        {
            T ignore;
            return GetFirstProperyWithAttributeOfType<T>(typeToAnalyze, out ignore);
        }

        /// <summary>
        /// Gets the first PropertyInfo with a custom attribute of the specified generic type T
        /// </summary>
        /// <typeparam name="T">The generic type of the attribute to search for</typeparam>
        /// <param name="typeToAnalyze">The type to analyze</param>
        /// <param name="attribute">The first attribute found of the specified generic type.</param>
        /// <returns></returns>
        public static PropertyInfo GetFirstProperyWithAttributeOfType<T>(this Type typeToAnalyze, out T attribute) where T : Attribute
        {
            PropertyInfo[] properties = typeToAnalyze.GetProperties();
            List<PropertyInfo> retVals = new List<PropertyInfo>();
            attribute = null;
            foreach (PropertyInfo property in properties)
            {
                object[] attributes = property.GetCustomAttributes(typeof(T), true);
                if (attributes.Length > 0)
                {
                    attribute = (T)attributes[0];
                    return property;
                }
            }

            return null;
        }

        public static MethodInfo[] GetMethodsWithAttributeOfType<T>(this Type typeToAnalyze) where T : Attribute, new()
        {
            return GetMethodsWithAttributeOfType<T>(typeToAnalyze, true);
        }

        public static MethodInfo[] GetMethodsWithAttributeOfType<T>(this Type typeToAnalyze, bool inherit ) where T : Attribute, new()
        {
            var returnValues = from item in typeToAnalyze.GetMethods()
                               where item.HasCustomAttributeOfType<T>(inherit)
                               select item;
            return returnValues.ToArray<MethodInfo>();
        }

        /// <summary>
        /// Get all the PropertyInfos that have the specified Attribute T addorning them.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to that must be addorning the property</typeparam>
        /// <param name="typeToAnalyze">The type to search for properties on</param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertiesWithAttributeOfType<T>(this Type typeToAnalyze) where T : Attribute
        {
            return GetPropertiesWithAttributeOfType<T>(typeToAnalyze, null);
        }

        /// <summary>
        /// Get all the PropertyInfos that have the specified Attribute T addorning them.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to that must be addorning the property</typeparam>
        /// <param name="typeToAnalyze">The type to search for properties on</param>
        /// <param name="limitToType">The type that the property must be of for it to be returned</param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertiesWithAttributeOfType<T>(this Type typeToAnalyze, Type limitToType) where T : Attribute
        {
            return GetPropertiesWithAttributeOfType<T>(typeToAnalyze, limitToType, true);
        }

        /// <summary>
        /// Get all the PropertyInfos that have the specified Attribute T addorning them.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to that must be addorning the property</typeparam>
        /// <param name="typeToAnalyze">The type to search for properties on</param>
        /// <param name="limitToType">The of that the property must be of for it to be returned</param>
        /// <param name="inherit">Specifies whether to search the inheritance chain.</param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertiesWithAttributeOfType<T>(this Type typeToAnalyze, bool inherit) where T : Attribute
        {
            return GetPropertiesWithAttributeOfType<T>(typeToAnalyze, null, inherit);
        }

        /// <summary>
        /// Get all the PropertyInfos that have the specified Attribute T addorning them.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to that must be addorning the property</typeparam>
        /// <param name="typeToAnalyze">The type to search for properties on</param>
        /// <param name="limitToType">The of that the property must be of for it to be returned</param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertiesWithAttributeOfType<T>(this Type typeToAnalyze, Type limitToType, bool inherit) where T : Attribute
        {
            if (limitToType == null)
            {
                var returnValues = from item in typeToAnalyze.GetProperties()
                                   where item.HasCustomAttributeOfType<T>(inherit)
                                   select item;

                return returnValues.ToArray<PropertyInfo>();
            }
            else
            {
                var returnValues = from item in typeToAnalyze.GetProperties()
                                   where item.HasCustomAttributeOfType<T>(inherit) && item.PropertyType == limitToType
                                   select item;

                return returnValues.ToArray<PropertyInfo>();
            }            
        }

        public static bool HasCustomAttributeOfType<T>(this PropertyInfo property) where T : Attribute
        {
            return HasCustomAttributeOfType<T>(property, true);
        }

        public static bool HasCustomAttributeOfType<T>(this PropertyInfo property, bool inherit) where T : Attribute
        {
            return property.GetCustomAttributes(typeof(T), inherit).Length > 0;
        }

        public static bool ClassHasAttributeOfType<T>(this object interrogant) where T : Attribute, new()
        {
            return HasCustomAttributeOfType<T>(interrogant.GetType());
        }

        public static bool HasCustomAttributeOfType<T>(this MemberInfo method) where T : Attribute, new()
        {
            return HasCustomAttributeOfType<T>(method, true);
        }

        public static bool HasCustomAttributeOfType<T>(this MemberInfo member, bool inherit, bool concrete) where T : Attribute, new()
        {
            T outT;
            return HasCustomAttributeOfType<T>(member, inherit, out outT, concrete);
        }

        public static bool HasCustomAttributeOfType<T>(this MemberInfo method, bool inherit) where T : Attribute, new()
        {
            T outT = null;
            return HasCustomAttributeOfType<T>(method, inherit, out outT);
        }

        public static T GetCustomAttributeOfType<T>(this MemberInfo memberInfo) where T : Attribute, new()
        {
            T retVal = null;
            HasCustomAttributeOfType<T>(memberInfo, out retVal);
            return retVal;
        }

        public static bool HasCustomAttributeOfType<T>(this MemberInfo memberInfo, out T attribute) where T : Attribute, new()
        {
            return HasCustomAttributeOfType<T>(memberInfo, true, out attribute);
        }

        public static bool HasCustomAttributeOfType<T>(this MemberInfo memberInfo, bool inherit, out T attribute) where T : Attribute, new()
        {
            return HasCustomAttributeOfType<T>(memberInfo, inherit, out attribute, false);
        }

        public static bool HasCustomAttributeOfType<T>(this MemberInfo memberInfo, bool inherit, out T attribute, bool concreteAttribute) where T : Attribute, new()
        {
            attribute = null;
            if (memberInfo == null)
            {
                return false;
            }
            object[] customAttributes = memberInfo.GetCustomAttributes(typeof(T), inherit);
            if (concreteAttribute)
            {
                foreach (object foundAttribute in customAttributes)
                {
                    if (foundAttribute.GetType() == typeof(T))
                    {
                        attribute = (T)foundAttribute;
                        break;
                    }
                }

                if (attribute == null)
                {
                    attribute = new T();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (customAttributes.Length > 0)
                {
                    attribute = (T)customAttributes[0];
                }
                else
                {
                    attribute = new T();
                }
                return customAttributes.Length > 0;
            }
        }

        public static bool HasCustomAttributeOfType(this MemberInfo memberInfo, Type type)
        {
            object ignore;
            return HasCustomAttributeOfType(memberInfo, type, true, out ignore);
        }

        public static bool HasCustomAttributeOfType(this MemberInfo memberInfo, Type type, out object attribute)
        {
            return HasCustomAttributeOfType(memberInfo, type, true, out attribute);
        }
        
        public static bool HasCustomAttributeOfType(this MemberInfo memberInfo, Type type, bool inherit, out object attribute)
        {
            attribute = null;
            object[] customAttributes = memberInfo.GetCustomAttributes(type, inherit);
            foreach (object attr in customAttributes)
            {
                if (attr.GetType() == type)
                {
                    attribute = attr;
                }
            }

            return attribute != null;
        }
    }
}
