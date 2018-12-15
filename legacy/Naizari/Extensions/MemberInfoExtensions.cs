/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Naizari.Helpers;

namespace Naizari.Extensions
{
    public static class MemberInfoExtensions
    {
        public static PropertyInfo[] GetPropertiesWithAttribute<T>(this Type type) where T : Attribute
        {
            var returnValue = from item in type.GetProperties()
                              where item.HasCustomAttributeOfType<T>()
                              select item;

            return returnValue.ToArray<PropertyInfo>();
        }
    }
}
