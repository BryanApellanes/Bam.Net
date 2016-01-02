/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Helpers
{
    public class DataSlots<TKey, TValue> : Dictionary<TKey, TValue>
    {
    }

    /// <summary>
    /// Session specific data slots, type safe version of HttpContext.Current.Session.
    /// </summary>
    public static class SessionDataSlots
    {
        public static void Set<TKey, TValue>(TKey key, TValue value)
        {
            DataSlots<TKey, TValue> dataSlots = SingletonHelper.GetSessionSingleton<DataSlots<TKey, TValue>>();
            dataSlots.Add(key, value);
        }

        public static TValue Get<TKey, TValue>(TKey key)
        {
            DataSlots<TKey, TValue> dataSlots = SingletonHelper.GetSessionSingleton<DataSlots<TKey, TValue>>();
            if (dataSlots.ContainsKey(key))
                return dataSlots[key];

            return default(TValue);
        }
    }
}
