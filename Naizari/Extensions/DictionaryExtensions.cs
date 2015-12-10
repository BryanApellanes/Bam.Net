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
    public static class DictionaryExtensions
    {
        public static void AddMissing<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Return the values of the specified dictionary as an array, the same as ValuesToArray.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static TValue[] ToArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            TValue[] retVal = new TValue[dictionary.Count];
            dictionary.Values.CopyTo(retVal, 0);
            return retVal;
        }

        /// <summary>
        /// Return the values of the specified dictionary as an array, the same as ToArray.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static TValue[] ValuesToArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return ToArray<TKey, TValue>(dictionary);
        }

        public static TKey[] KeysToArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            TKey[] retVal = new TKey[dictionary.Count];
            dictionary.Keys.CopyTo(retVal, 0);
            return retVal;
        }

        public static string ToDelimited<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, string entrySeparator, string nameValueSeparator)
        {
            string retVal = "";
            foreach (TKey key in dictionary.Keys)
            {
                retVal += key.ToString() + nameValueSeparator + dictionary[key] + entrySeparator;
            }

            return retVal;
        }

        public static Dictionary<string, string> FromDelimited(string delimitedInput, string entrySeparator, string nameValueSeparator)
        {
            return StringExtensions.ToDictionary(delimitedInput, entrySeparator, nameValueSeparator);
            //Dictionary<string, string> retVal = new Dictionary<string, string>();
            //string[] nameValuePairs = delimitedInput.Split(new string[] { entrySeparator }, StringSplitOptions.RemoveEmptyEntries);
            //foreach (string nameValuePair in nameValuePairs)
            //{
            //    string[] nameValue = nameValuePair.Split(new string[] { nameValueSeparator }, StringSplitOptions.RemoveEmptyEntries);
            //    if (nameValue.Length != 2)
            //        throw new InvalidOperationException("Invalid name value pair in specified dictionary string");

            //    retVal.Add(nameValue[0].Trim(), nameValue[1].Trim());
            //}

            //return retVal;
        }

        public static void AddRange<TValue>(Dictionary<string, TValue> dictionary, TValue[] values, string keyProperty)
        {
            PropertyInfo property = typeof(TValue).GetProperty(keyProperty);
            if (property == null)
                throw new InvalidOperationException(string.Format("The type {0} doesn't have a property named {1}", typeof(TValue).Name, keyProperty));

            foreach (TValue value in values)
            {
                string key = property.GetValue(value, null).ToString();
                dictionary.Add(key, value);
            }
        }
    }
}
