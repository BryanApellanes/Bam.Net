/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;

namespace Bam.Net.Incubation
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Intended to delimit the specified array of T using the
        /// specified ToDelimitedDelegate.  Each item will be represented
        /// by the return value of the specified ToDelimitedDelegate.
        /// </summary>
        /// <typeparam name="T">The type of objects in the specified array</typeparam>
        /// <param name="objectsToStringify">The objects</param>
        /// <param name="toDelimiteder">The ToDelimitedDelegate used to represent each object</param>
        /// <returns>string</returns>
        public static string ToDelimited<T>(this T[] objectsToStringify, ToDelimitedDelegate<T> toDelimiteder)
        {
            return ToDelimited(objectsToStringify, toDelimiteder, ", ");
        }

        /// <summary>
        /// Intended to delimit the specified array of T using the
        /// specified ToDelimitedDelegate.  Each item will be represented
        /// by the return value of the specified ToDelimitedDelegate.
        /// </summary>
        /// <typeparam name="T">The type of objects in the specified array</typeparam>
        /// <param name="objectsToStringify">The objects</param>
        /// <param name="toDelimiteder">The ToDelimitedDelegate used to represent each object</param>
        /// <returns>string</returns>
        public static string ToDelimited<T>(this T[] objectsToStringify, ToDelimitedDelegate<T> toDelimiteder, string delimiter)
        {
            string retVal = string.Empty;
            bool first = true;
            foreach (T obj in objectsToStringify)
            {
                if (!first)
                {
                    retVal += delimiter;
                }
                retVal += toDelimiteder(obj);
                first = false;
            }
            return retVal;
        }
    }
}
