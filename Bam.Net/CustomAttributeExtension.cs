/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bam.Net
{
    public static class CustomAttributeExtension
    {
        public static MethodInfo GetFirstMethodWithAttributeOfType<T>(this Type typeToAnalyze) where T : Attribute
        {
            return GetFirstMethodWithAttributeOfType<T>(typeToAnalyze, out T attr);
        }
        /// <summary>
        /// Gets the MethodInfo for the first method found in the specified typeToAnalyze
        /// that has a custom attribute of the specified type T.  Returns null if none
        /// are found.
        /// </summary>
        /// <typeparam name="T">the type of the custom attribute to look for</typeparam>
        /// <param name="typeToAnalyze">the type to analyze</param>
        /// <param name="attr"></param>
        /// <returns>MethodInfo or null</returns>
        public static MethodInfo GetFirstMethodWithAttributeOfType<T>(this Type typeToAnalyze, out T attr) where T : Attribute
        {
            attr = null;
            MethodInfo[] methods = typeToAnalyze.GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (HasCustomAttributeOfType<T>(method, out attr))
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

        public static MethodInfo[] GetMethodsWithAttributeOfType<T>(this Type typeToAnalyze) where T : Attribute
        {
            return GetMethodsWithAttributeOfType<T>(typeToAnalyze, true);
        }

        public static MethodInfo[] GetMethodsWithAttributeOfType<T>(this Type typeToAnalyze, bool inherit ) where T : Attribute
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

        public static PropertyInfo[] GetPropertiesWithAttributeOfType(this Type typeToAnalyze, Type attributeType, Type limitToType, bool inherit)
        {
            if (limitToType == null)
            {
                var returnValues = from item in typeToAnalyze.GetProperties()
                                   where item.HasCustomAttributeOfType(attributeType, inherit)
                                   select item;

                return returnValues.ToArray<PropertyInfo>();
            }
            else
            {
                var returnValues = from item in typeToAnalyze.GetProperties()
                                   where item.HasCustomAttributeOfType(attributeType, inherit) && item.PropertyType == limitToType
                                   select item;

                return returnValues.ToArray<PropertyInfo>();
            }
        }

        public static bool ClassHasAttributeOfType<T>(this object interrogant) where T : Attribute
        {
            return HasCustomAttributeOfType<T>(interrogant.GetType());
        }

        public static bool HasCustomAttributeOfType<T>(this MemberInfo member) where T : Attribute
        {
            return HasCustomAttributeOfType<T>(member, true);
        }

        public static bool HasCustomAttributeOfType<T>(this MemberInfo member, bool inherit, bool concrete) where T : Attribute
        {
            T outT;
            return HasCustomAttributeOfType<T>(member, inherit, out outT, concrete);
        }

        public static bool HasCustomAttributeOfType<T>(this MemberInfo member, bool inherit) where T : Attribute
        {
            T outT = null;
            return HasCustomAttributeOfType<T>(member, inherit, out outT);
        }

        public static T GetCustomAttributeOfType<T>(this MemberInfo memberInfo) where T : Attribute
        {
            T retVal = null;
            HasCustomAttributeOfType<T>(memberInfo, out retVal);
            return retVal;
        }

        public static bool HasCustomAttributeOfType<T>(this MemberInfo memberInfo, out T attribute) where T : Attribute
        {
            return HasCustomAttributeOfType<T>(memberInfo, true, out attribute);
        }

        public static bool HasCustomAttributeOfType<T>(this MemberInfo memberInfo, bool inherit, out T attribute) where T : Attribute
        {
            return HasCustomAttributeOfType<T>(memberInfo, inherit, out attribute, false);
        }

        /// <summary>
        /// Determine if the MemberInfo is addorned with the specified generic attribute type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="memberInfo"></param>
        /// <param name="inherit"></param>
        /// <param name="attribute"></param>
        /// <param name="concreteAttribute">If true, must be the attribute specified and not an attribute that extends the specified attribute</param>
        /// <returns></returns>
        public static bool HasCustomAttributeOfType<T>(this MemberInfo memberInfo, bool inherit, out T attribute, bool concreteAttribute) where T : Attribute
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
        
        public static bool HasCustomAttributeOfType(this MemberInfo memberInfo, Type type, bool inherit)
        {
            object ignore;
            return HasCustomAttributeOfType(memberInfo, type, inherit, out ignore);
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
